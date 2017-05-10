<template>
    <div class="storage-wrap storage">
        <div class="scroll-box">
            <header class="storage-header">
                    <div class="operation">
                          <div class="button-panel inline-block">
                              <el-button-group>
                                <el-button @click="getProductList">获取商品列表（测试用）</el-button>
                                <el-button @click="getconfigData">得到配置数据（测试用）</el-button>
                              </el-button-group>
                         </div>
                    </div>
                    <div class="filter-seek">
                        <ul>
                            <li>
                                <p>商品属性：{{returnSelectData.productTpye === 1 ? "成品" : "旧料"}}</p>
                            </li>
                            <li>退货库位：
                                <el-select class="custom-select" filterable v-model="header.putRepository" placeholder="">
                                  <el-option v-for="item in headerPullDown.putRepositoryPullDown"
                                  :label="item.repositoryName"
                                  :value="item.repositoryId">
                                    <span @click="header.putReposiId = item.id">{{item.repositoryName}}</span>
                                  </el-option>
                                </el-select>
                            </li>
                            <li>
                                <p>备注：</p>
                            </li>
                        </ul>
                    </div>
            </header>
            <section class="storage-body section">         
                <el-checkbox-group v-model="crudData.checkList">  
                    <table border="1" cellpadding="0" cellspacing="0">
                        <tr class="tr-header">
                            <td v-if="!crudData.operationCut"><input type="checkbox" v-model="crudData.checked" @click="cutSelect(crudData.checked)"/></td>
                            <td>序号</td>
                            <td>条形码</td>
                            <td>当前状态</td>
                            <td>产品类别</td>
                            <td>首饰名称</td>
                            <td>总件重（g）</td>
                            <td>净金重</td>
                            <td>证书号</td>
                            <td>主石</td>
                            <td>颜色</td>
                            <td>净度</td>
                            <td>副石</td>
                            <td>售价（元）</td>
                            <td>备注</td>
                        </tr>
                        <tr v-if="showData.showStorageList" v-for="(showStorageList, index) in showData.showStorageList">
                            <td v-if="!crudData.operationCut"><el-checkbox :label="index"></el-checkbox></td>
                            <td>{{index + 1}}</td>
                            <template v-for="key in Object.keys(showStorageList)">
                                <td v-if='key !== "status"'>
                                    <input v-if="key === 'barcode'" v-model="showStorageList[key]" @keyup.enter="queryBarCode(index)">
                                    <p v-else>{{showStorageList[key]}}</p>
                                </td>
                                <td v-else>
                                    <p>{{getAuditStatus(showStorageList[key])}}</p>
                                </td>
                            </template>
                        </tr>
                    </table>
                </el-checkbox-group>
            </section>
        </div>
        <footer class="storage-footer">
            <!-- 分页 -->
            <div class="block">
                <el-pagination
                    @size-change="handleSizeChange"
                    @current-change="handleCurrentChange"
                    :current-page="footer.currentPage"
                    :page-sizes="[30]"
                    :page-size="30"
                    layout="total, sizes, prev, pager, next, jumper"
                    :total="paginationSize">
                </el-pagination>
            </div>
            <!-- 功能按钮 -->
            <div class="block-btn">
                  <div class="all-confirm" v-show="crudData.operationCut">
                        <el-button type="success" @click="appendRow">新增</el-button>
                        <el-button type="info" @click="addSingleAll">批量新增</el-button>
                        <el-button type="info" @click="crudData.operationCut = false">删除</el-button>
                        <el-button type="info" @click="addSingleAll">保存</el-button>
                        <el-button type="info" @click="addSingleAll">关闭</el-button>
                  </div>
                  <div class="confirm-operation" v-show="!crudData.operationCut">
                        <el-button type="primary" @click="crudData.operationCut = true">取消</el-button>
                        <el-button type="danger" @click="batchDelect">确认</el-button>
                  </div>
            </div>
        </footer>
    </div>
</template>
<script>
import {mapActions, mapGetters} from 'vuex'
export default {
    data () {
        return {
            pickerOptions0: {
                disabledDate(time) {
                return time.getTime() < Date.now() - 8.64e7;
                }
            },
            header: {
                oddNumbers: '', // 开始单号
                putRepository: null, // 入库库位(商品库位)
                putReposiId: null, // 入库库位Id(商品库位Id)
                shopId: null, // 分销商Id
                typeＮame: null // 商品属性（自定义的下拉数据）
            },
            headerPullDown: {
                putRepositoryPullDown: null // 入库库位下拉数据(商品库位)
            },
            footer: {
                currentPage: 1 // 当前页
            },
            newDatas: { // 新增数据(其它页面也用到的)
                classesName: null, // 产品类别
                classesId: null, // 产品类别Id
                operateDepartment: null, // 所属单位
                opeDepartId: null, // 所属单位id
                RKReceiptNo: null // 入库单号
            },
            // newDataPullDown: { // 下拉数据
            //     // productPullDown: null, // 产品类别下拉数据
            //     DepartmentPullDown: null // 所属单位下拉数据
            // },
            showData: { // 请求回来的数据
                storageList: null, // 列表的数据
                showStorageList: [], // 配置好的渲染数据
                productClassList: [] // 产品类型数据
            },
            popup: { // 弹窗
                productClass: false, // 产品类型
                amendings: false, // 批量新增
                audit: [] // 审核弹窗
            },
            crudData: { // 增查更删数据
                checked: false, // 监听全选
                checkList: [], // 选择按钮（选中的数据集合）删除
                operationCut: true, // 操作按钮切换
                // saveDatas: [], // 保存了的数据
                // newAddData: [], // 新增数据
                selectedData: null // 选中数据
            },
            configData: {
                barcode: null, // 条码号
                status: null, // 当前状态
                productType: null, // 产品类别
                jewelryName: null, // 首饰名称
                totalWeight: null, // 总件重
                netWeight: null, // 净金重
                certifiNo: null, // 证书号
                mainName: null, // 主石
                color: null, // 颜色
                neatNess: null, // 净度
                deputyName: null, // 副石
                soldPrice: null, // 售价
                remark: null// 备注
            }
        }
    },
    computed: {
        ...mapGetters([
            "returnSelectData"
        ]),
        paginationSize: function () { // 分页的大小
            return this.showData.showStorageList.length;
        }
    },
    created () {
        this.appendRow();
        this.getRepositoryList();
    },
    watch: {
        'crudData.checkList': function () {
            if (this.crudData.checkList.length === this.showData.showStorageList.length) {
                this.crudData.checked = true;
            } else {
                this.crudData.checked = false;
            }
        }
    },
    methods: {
        ...mapActions([
            "setStockData"
        ]),
        handleSizeChange (val) {
          console.log(`每页 ${val} 条`);
        },
        handleCurrentChange (val) {
          this.footer.currentPage = val;
          console.log(`当前页: ${val}`);
        },
        getChildCategorie (parm) {
            this.newDatas.classesName = parm;
        },
        getRepositoryList () { // 获取库位列表
            var data = {
                  "data": {
                      // "companyId": sessionStorage.getItem("companyId"),
                      "operationUnit": "",
                      "operationUnitId": "",
                      "orderByType": "",
                      "page": "1",
                      "pageSize": "10",
                      "queryData": {
                          "auditorIdList": [
                              {
                                  "auditorId": ""
                              }
                          ],
                          "classesIdList": [
                              {
                                  "classesId": "" // 产品类别ID
                              }
                          ],
                          "endTime": "",
                          "handleDeptIdList": [ // 操作部门ID列表
                              {
                                  "handleDeptId": ""
                              }
                          ],
                          "makeUserIdList": [ // 制单人ID列表
                              {
                                  "makeUserId": ""
                              }
                          ],
                          "orderNumber": "",
                          "startTime": "",
                          "supplierIdList": [ // 供应商ID列表
                              {
                                  "supplierId": ""
                              }
                          ],
                          "timeType": "3",
                          "warehouseIdList": [ // 库位ID（入库等）列表
                              {
                                  "warehouseId": ""
                              }
                          ]
                      },
                      "showView": "1", // 显示列表：1 显示列表 2 编辑列表 3 查询结果
                      "type": "4",
                      "userId": sessionStorage.getItem("id")
                  },
                  "unit": {
                        "companyId": sessionStorage.getItem("companyId"),
                        "channel": "web",
                        "os": "string",
                        "tokenId": sessionStorage.getItem("tokenId")
                  }
            }
            let url = INTERFACE_URL_9083 + "/v1/headquarter/showRepositoryList";
            this.$http.post(url, data).then((response) => {
                this.headerPullDown.putRepositoryPullDown = response.data.data.repositoryList;
                // console.log(this.headerPullDown.supplierPullDown);
                console.log("获取库位列表");
                console.log(response);
                console.log("获取库位列表");
            }, (response) => {
                console.log(response)
            })
        },
        getAuditStatus (parm) { // 审核状态
            var statusName = null;
            switch (parm) {
                case '1':
                    statusName = "待审核";
                    break;
                case '2':
                    statusName = "审核中";
                    break;
                case '3':
                    statusName = "已审核"
            }
            return statusName;
        },
        cutSelect (parm) { // 切换全选
            var _self = this;
            setTimeout(function() {
                _self.cutSelectFun();
            }, 13);
        },
        cutSelectFun () {
            if (this.crudData.checked === true) {
                for (var i = 0; i < this.showData.showStorageList.length; i++) {
                    this.crudData.checkList.push(i);
                }
            } else {
                this.crudData.checkList = [];
            }
        },
        // 核心功能
        appendRow () { // 新增
            var createList = {};
            for (let keys in this.configData) {
                createList[keys] = null
            }
            this.showData.showStorageList.push(createList);
        },
        queryBarCode (index) { // 通过条码号搜索数据
            this.getProductList(index);
            // console.log("通过条码号搜索数据");
        },
        addSingleAll () { // 保存，目前不做分页保存
            console.log(this.showData.showStorageList);
        },
        batchDelect (parm) { // 删除
            var delectNew = [];
            for (let i = 0; i < this.crudData.checkList.length; i++) { // 收集新增和保存了的值
                delectNew.push(this.crudData.checkList[i])
            }
            // 请求成功后，做这个事
            function sortNumber (a, b) {
              return b - a;
            }
            delectNew = delectNew.sort(sortNumber);
            for (let i of delectNew) { // 删除新增数据
              this.showData.showStorageList.splice(i, 1);
            }
            this.crudData.checkList = [];
            this.crudData.operationCut = true;
        },
        // 测试代码////////////
        getconfigData (index) { // 得到配置代码
            for (let i = 0; i < this.showData.storageList.length; i++) {
                let newAddDatas = {};
                 for (let key in this.configData) {
                    newAddDatas[key] = this.showData.storageList[i][key] || null;
                }
                this.showData.showStorageList.splice(index, 0, newAddDatas);
            }
        },
        getProductList (index) { // 获取商品列表
            var _self = this;
            this.$http.get('/static/sipping.json').then((response) => {
                this.showData.storageList = response.data.sipping;
                this.showData.showStorageList.splice(index, 1);
                _self.getconfigData(index);
                _self.appendRow();
            }, (response) => {
                console.log(response)
            });
        }
    }
}
</script>
<style lang="scss">
    // 整体布局样式
    .storage-wrap {
        overflow: auto;
        height: 100%;
        display: flex;
        flex-direction: column;
        .scroll-box {
            flex: 1;
            display: flex;
            flex-direction: column;
            overflow-y: hidden;
            .storage-header {
                height: 130px;
                padding-top: 40px;
                padding: 40px 20px 0 0;
            }
            .storage-body {
                flex: 1;
                overflow-y: auto;
                border: 1px dashed #000;
            }
        }
        .storage-footer {
            height: 100px;
            background-color: #ccc;
        }
    }
    // 特定布局
    .storage {
        .storage-btn {
            display: inline-block;
            width: 90px;
            height: 30px;
            line-height: 30px;
            text-align: center;
            border-radius: 5px;
            background-color: #20a0ff;
        }
        .time-select{
            display: inline-block;
            width: 125px;
            height: 30px;
            input{
                height: 30px;
            }
        }
        .custom-select { // 修改默认下拉框
            display: inline-block;
            width: 90px;
            height: 30px;
            input {
                height: 30px;
            }
        }
        header {
            .operation {
                height: 39px;
                .cut-department {
                    position: absolute;
                    top: 30px;
                    left: 0;
                    right: 0;
                    margin: auto;
                    width: 300px;
                }
            }
            .filter-seek {
                height: 40px;
                line-height: 40px;
                ul {
                    display: table-row;
                    white-space: nowrap;
                    li {
                        display: inline-block;
                        margin-right: 20px;
                        height: 40px;
                        line-height: 40px;
                    }
                }
            }
        }
        .section {
            table{ // 表格
               // width: 100%;
                thead{
                    background-color: #ccc;
                    height: 30px;
                }
                tbody{
                    padding: 100px;
                    border: 20px solid blue;
                    overflow: hidden;
                }
               tr{
                    th{
                        height: 30px;
                        background-color: #c9c9c9;
                    }
                    th,td {
                        padding: 5px 20px;
                    }
                    td{
                        border-bottom: 1px solid #ccc;
                    }
                }
            }
        }
    }
    // 状态色
    .default-audit {
        display: inline-block;
        color: #fff;
        width: 80px;
        height: 30px;
        line-height: 30px;
        text-align: center;
        border-radius: 5px;
    }
    // 审核状态色
    .no-audit {
       background-color: #50bfff;
    }
    .underway-audit {
        background-color: red;
    }
    .succeed-audit {
        background-color: #ccc;
    }
    // 弹窗
    .popup-productClass{ // 产品类别
        .el-dialog--small{ // 弹窗宽高
            width: 538px;
            height: 438px;
        }
    }
    .new-stock-in-box{ // 产品类别
        ul {
            position: relative;
            border-radius: 5px;
            border: 1px solid red;
            padding: 20px;
            li:nth-child(1){
                position: absolute;
                top: -15px;
                height: 30px;
                line-height: 30px;
                left: 5px;
                padding: 0 10px;
                background-color : #fff;
                border: 1px solid red;
                width: auto;
            }
            li{
                width: 110px;
                height: 30px;
                line-height: 30px;
                border: 1px solid red;
                display: inline-block;

            }     
        }
    }
    footer{
      .pagination {
            margin-left: 100px;
            border: 1px solid blue;
            height: 30px;
            width: 100%;
      }
      .pagination-inner{
            padding: 0;
      }
      .block-btn {
          position: relative;
          width: 100%;
          height: 100%;
          .all-confirm,.confirm-operation{
              position: absolute;
              left: 0;
              top: 0;
              bottom: 0;
              right: 0;
              margin: auto;
          }
          .all-confirm{
              width: 900px;
              height: 40px;
              line-height: 40px;
              background-color: #ccc;
          }
          .confirm-operation{ // 确认操作
               width: 140px;
               height: 40px;
          }
      }
    }
</style>