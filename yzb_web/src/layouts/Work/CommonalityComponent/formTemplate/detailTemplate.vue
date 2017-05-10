<template>
    <div class="main-wrap">
        <section class="body-box">
                <div class="body-table table-template">
<el-checkbox-group v-model="crudData.checkList"> 
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tr-header">
                                <th v-if="!crudData.operationCut">
                                    <label class="el-checkbox checkbox-font">
                                        <span class="el-checkbox__input is-focus" v-bind:class="{'is-checked': crudData.checked}">
                                            <span class="el-checkbox__inner"></span>
                                            <input type="checkbox" class="el-checkbox__original" v-model="crudData.checked" @click="cutSelect(crudData.checked)"/>
                                        </span>
                                    </label>
                                </th>
                                <th v-if="tittleThs" v-for="tittleTh in tittleThs">{{tittleTh.name}}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-if="showDatas" v-for="(showData, index) in showDatas">
                                <td v-if="!crudData.operationCut"><el-checkbox :label='index+((crudData.currentPage - 1) * 30)' class="checkbox-font"></el-checkbox></td>
                                <td>{{index+1+((crudData.currentPage - 1) * 30)}}</td>
                                <template v-for="key in Object.keys(showData)">
                                    <template v-if='key !== "productId"'>        
                                        <td v-if='key === "status"'>
                                            <p>{{getAuditStatus(showData[key])}}</p>
                                        </td>
                                        <td v-else>
                                            <template v-if='key === "barcode"'>            
                                                <input class="input-default" v-model="showData.barcode" @keyup.enter="queryBarCode(showData.barcode, index)"></input>
                                            </template>
                                            <p v-else>{{showData[key]}}</p>
                                        </td>
                                    </template>
                                </template>
                                <td class="del-icon" @click="delProduct(showData)">删除</td>
                            </tr>
                        </tbody>
                    </table>
</el-checkbox-group>
                </div>
                <div class="body-page">
                    <div class="paga-30">
                        <el-pagination
                            @size-change="handleSizeChange"
                            @current-change="handleCurrentChange"
                            :current-page="crudData.currentPage"
                            :page-sizes="[30]"
                            :page-size="30"
                            layout="total, sizes, prev, pager, next, jumper"
                            :total="paginationSize">
                        </el-pagination>
                    </div>
                </div>
        </section>    
        <footer class="footer-box">
            <div class="footer-affirm">
                <ul v-if="!crudData.operationCut">
                    <li class="cancel-btn-small" @click="crudData.operationCut = true">取消</li>
                    <li class="confirm-btn-small" @click="batchDelect">确定</li>
                </ul>
                <ul v-else>
                    <li class="confirm-btn-small" @click="appendRow">新增</li>
                    <li class="confirm-btn-small" @click="appendRow">批量新增</li>
                    <li class="confirm-btn-small" @click="crudData.operationCut = false">删除</li>
                    <li class="confirm-btn-small" @click="addSingleAll">保存</li>
                    <li class="confirm-btn-small red" @click="close">关闭</li>
                </ul>
            </div>
        </footer>
        <affirm></affirm>
        <error></error>
    </div>
</template>
<script>
// import * as types from './mutation-types.js'
import error from "./../../CommonalityComponent/popupTemplate/error"
import affirm from "./../../CommonalityComponent/popupTemplate/affirm"
import {mapActions, mapGetters} from 'vuex'
import {delectProductDetail} from './../../../../Api/commonality/operate'
import { seekGoodsList, seekGetProductByBarcode } from './../../../../Api/commonality/seek'
import { productDetailStatus } from './../../../../Api/commonality/status'
export default {
    props: [
        'tittleThs', // 表格头部
        'configData', // 配置的数据
        'newDatas', // 上一页带过来的数据
        'orderRemark', // 单据备注
        'applyType', // 应用类型1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜（销售除外）
        'closeRouterUrl' // 关闭的路由地址
    ],
    data () {
        return {
            crudData: { // 增查更删数据
                currentPage: 1, // 当前页
                checked: false, // 监听全选
                checkList: [], // 选择按钮（选中的数据集合）删除
                operationCut: true // 操作按钮切换
            },
            datas: {
                postDatas: [], // 请求回来的原始数据
                postDataList: [], // 请求回来的数据集合(配置好的)
                newGapDatas: [] // 新的空白数据
            }
        }
    },
    components: {
        affirm, error
    },
    watch: {
        'getProductDetail.rowDataList': function () {
            this.datas.postDataList = [];
        }
    },
    computed: {
        ...mapGetters([
            "shopLists", // 分销商
            "getProductDetail", // 商品列表（详细）
            "receiptDetail" // 单据详情
        ]),
        paginationSize: function () { // 分页的大小
            return this.allDataList.length;
        },
        allDataList: function () { // 总数据集合
            let productDetail = [];
            if (this.getProductDetail.rowDataList) {
                productDetail = this.configAarryData(this.getProductDetail.rowDataList);
            }
            return [...productDetail, ...this.datas.postDataList, ...this.datas.newGapDatas];
        },
        showDatas: function () { // 最终显示的数据
            let state = ((this.crudData.currentPage - 1) * 30);
            let end = (this.crudData.currentPage * 30);
            return this.allDataList.slice(state, end);
        },
        getCheckedPage: function () { // 监听当页的所有做选择的变量（删除选择）
            var CheckedPage = [];
            let Num = ((this.crudData.currentPage - 1) * 30);
            for (var i = 0; i < this.showDatas.length; i++) {
                CheckedPage.push(Num);
                Num += 1;
            }
            return CheckedPage;
        },
        currentPageChecked: function () { // 抽出当页已经选中的数据集合
            let PageChecked = [];
            for (var i of this.crudData.checkList) {
                var start = (this.crudData.currentPage - 1) * 30;
                var end = start + 29;
                if (start <= i && end >= i) {
                    console.log(i);
                    PageChecked.push(i);
                }
            }
            return PageChecked;
        }
    },
    created () {
        this.judgeProductDetail(); // 如果没有商品列表重新获取一次(避免用户刷新)
        // this.appendRow();
    },
    methods: {
        ...mapActions([
            "workProductDetail", // 商品列表
            "receiptAddOrCheck", // 单据新增/提交审核/保存（销售除外）
            "workPopupError" // 错误弹窗
        ]),
        handleSizeChange (val) {
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange (val) {
            this.crudData.currentPage = val;
            console.log(`当前页: ${val}`);
        },
        judgeProductDetail () { // 如果没有商品列表重新获取一次(避免用户刷新)
            this.workProductDetails();
            // if (!this.getProductDetail.productList) {
            //     this.workProductDetail({
            //         "applyType": "1", // 调库
            //         "objId": this.newDatas.orderNumber,
            //         "type": "1" // 1 单据号 2 产品类别ID 3 商品ID列表 4 条码模糊查询 5 条码号
            //     });
            // }
        },
        appendRow () { // 新增
            var rowData = JSON.stringify(this.configData);
            var newRowData = JSON.parse(rowData);
            this.datas.newGapDatas.push(newRowData);
        },
        configAarryData (parm) { // 配置数据为数组
            var configData = [];
            for (let i = 0; i < parm.length; i++) {
                let newAddDatas = {};
                for (let key in this.configData) {
                    newAddDatas[key] = parm[i][key] || null;
                }
                configData.push(newAddDatas)
            }
            return configData;
        },
        getconfigData (parm) { // 得到配置代码
            let configDatas = {};
            for (let key in this.configData) {
                configDatas[key] = parm[key] || null
            }
            return configDatas;
        },
        queryBarCode (parm, index) { // 通过条码号搜索数据
            if (parm) {
                var data = {
                    "objId": "1",
                    "barcode": parm, // 条码号
                    "queryType": 1, // 1=返回字段少；0=返回所有字段
                    "type": "3"
                };
                sessionStorage.setItem("JSON", JSON.stringify(data));
                seekGetProductByBarcode(data).then((response) => {
                    console.log("获取商品列表");
                    console.log(response);
                    if (response.data.state === 200) {
                        if (response.data.data) {
                            // if (this.datas.newGapDatas.length < 2) {
                            //     this.datas.newGapDatas[0].barcode = null;
                            // } else {
                            //     for (var i = 0; i < this.datas.newGapDatas.length; i++) {
                            //         if (this.datas.newGapDatas[i].barcode === parm) {
                            //             this.datas.newGapDatas.splice(i, 1);
                            //         }
                            //     }
                            // }
                            var datas = this.getconfigData(response.data.data);
                            this.datas.postDataList.push(datas);
                            this.datas.postDatas.push(response.data.data);
                        }
                    } else {
                        this.workPopupError(response.data.msg)
                    }
                }, (err) => {
                    console.log(err);
                })
            }
        },
        delProduct (parm) { // 删除商品
            alert("功能敬请期待");
            // console.log(parm);
        },
        workProductDetails (parm, index) { // 获取商品列表
            var options = {
                "orderNum": sessionStorage.getItem("orderNumber"),
                "page": "1",
                "pageSize": "30"
            }
            sessionStorage.setItem("JSONssss", JSON.stringify(options));
            seekGoodsList(options).then((response) => {
                console.log("获取商品列表");
                console.log(response);
                if (response.data.state === 200) {
                    if (response.data.data.productList) {
                        if (this.datas.newGapDatas.length < 2) {
                            if (this.datas.newGapDatas[0]) { // 如果有新空白的情况下
                                this.datas.newGapDatas[0].barcode = null;
                            }
                        } else {
                            for (var i = 0; i < this.datas.newGapDatas.length; i++) {
                                if (this.datas.newGapDatas[i].barcode === parm) {
                                    this.datas.newGapDatas.splice(i, 1);
                                }
                            }
                        }
                        for (let j = 0; j < response.data.data.productList.length; j++) {
                            var datas = this.getconfigData(response.data.data.productList[j]);
                            this.datas.postDataList.push(datas);
                            this.datas.postDatas.push(response.data.data.productList[j]);
                        }
                    }
                } else {
                    this.workPopupError(response.data.msg)
                }
            }, (response) => {
                console.log(response);
            })
        },
        getAuditStatus (parm) { // 审核状态
            return productDetailStatus(parm);
        },
        cutSelect (parm) { // 切换全选
            var _self = this;
            setTimeout(function() {
                _self.cutSelectFun();
            }, 13);
        },
        cutSelectFun () {
            if (this.crudData.checked === true) { // 全选
                console.log("监听当前页");
                console.log(this.getCheckedPage);
                for (let i of this.getCheckedPage) {
                    this.crudData.checkList.push(i)
                }
                this.crudData.checkList = [...new Set(this.crudData.checkList)];
                console.log(this.crudData.checkList)
            } else { // 取消(bug,会把其它页选中的也清掉)
                this.crudData.checkList = [];
            }
        },
        batchDelect () { // 确认删除（全空白的，新赠的有条形码，请求回来的已经保存了的）
            console.log("删除");
            let deleteList = []; // 新增
            let productIdList = []; // 商品Id
            for (let i of this.crudData.checkList) { // 如果没有条形码就push,带回加上去（删除新增的）
                if (!this.allDataList[i].barcode) {
                    deleteList.push(this.allDataList[i]);
                } else {
                    var ObjectId = { // 商品Id
                        "productId": this.allDataList[i].productId
                    }
                    productIdList.push(ObjectId);
                }
            }
            console.log("删除的收集");
            console.log(productIdList);
            let data = {
                "orderNum": this.receiptDetail.orderNum, // 单据号
                "productIdList": productIdList, // 商品ID列表
                "type": "1" // 操作类型：1 删除
            }
            delectProductDetail(data).then((response) => {
                console.log("删除商品列表999999999999999999999999");
                console.log(response);
                if (response.data.state === 200) {
                    this.workProductDetail({ // 重新获取商品列表
                        "applyType": this.applyType,
                        "objId": this.newDatas.orderNumber,
                        "type": "1"
                    });
                    this.crudData.checked === false; // 关闭全选按钮
                    alert("删除成功了")
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log("删除商品列表cuo");
                console.log(response);
            })
            for (let i = 0; i < this.datas.newGapDatas.length; i++) { // 删除新的空白数据
                for (let j of deleteList) {
                    if (this.datas.newGapDatas[i] === j) {
                        this.datas.newGapDatas.splice(i, 1)
                    }
                }
            }
            this.crudData.checkList = [];
        },
        addSingleAll () { // 保存，目前不做分页保存
            let options = {
                "productList": {
                    "barcode": parm
                },
                "orderNum": sessionStorage.getItem("orderNumber"),
                "operate": 1
            }
            this.productListFun(options);
        },
        productListFun (parm) { // 5.31单据操作-增加/删除商品-通过商品条码号
            operateProductList(parm).then((response) => {
                console.log("5.31单据操作-增加/删除商品-通过商品条码号");
                console.log(response);
                this.workProductDetails();
            }, (response) => {
                console.log(response);
            })
        },
        close () { // 关闭
            this.$router.push(this.closeRouterUrl);
        }
    }
}
</script>
<style lang="scss" scoped>
.main-wrap{
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    h1{
        font-size: 20px;
        color: red;
    }
    .del-icon{
        cursor: pointer;
    }
}
</style>