<template>
    <div class="new-template">
        <ul class="header-wrap">
            <li class="confirm-btn-small" @click="appendRow">单个添加</li>
            <li class="confirm-btn-small" @click="appendBatchRow">批量添加</li>
            <li class="confirm-btn-small" @click="printReceipts">打印单据</li>
            <li class="confirm-btn-small" @click="auditFun">{{getOptionsName}}</li>
            <li class="confirm-btn-small" @click="delectReceipts">删除</li>
            <li class="confirm-btn-small red" @click="close">关闭</li>
        </ul>
        <section class="body-box">
            <div class="table-template">
    <el-checkbox-group v-model="crudData.checkList"> 
                <table cellpadding="0" cellspacing="0">
                    <thead>
                        <tr class="tr-header">
                            <th v-if="tittleThs" v-for="tittleTh in tittleThs">{{tittleTh.name}}</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-if="showDatas" v-for="(showData, index) in showDatas">
                            <td>{{index+1+((crudData.currentPage - 1) * pageSize)}}</td>
                            <template v-for="key in Object.keys(showData)">
                                <td v-if='key === "status"'>
                                    <p>{{getAuditStatus(showData[key])}}</p>
                                </td>
                                <td v-else>
                                    <template v-if='key === "barcode"'>            
                                        <input :ref='"xh" + index' class="form-input" v-model="showData[key]" @keyup.enter="queryBarCode(showData.barcode, index, '1')"></input>
                                    </template>
                                    <p v-else>{{showData[key]}}</p>
                                </td>
                            </template>
                            <td @click="delProduct(showData.barcode, index, '2', showData)" class="del-product">删除</td>
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
                        :page-size="pageSize"
                        layout="total, sizes, prev, pager, next, jumper"
                        :total="paginationSize">
                    </el-pagination>
                </div>
            </div>
        </section>
        <!-- 删除单据 -->
        <delect-receipts
        v-on:delectReceiptsState="delectReceiptsState"
        :delPopup='popup.deleteReceipts'
        :receiptsOrderNum='popup.receiptsOrderNum'
        ></delect-receipts>
        <!-- 审核弹窗 -->
        <audit-receipts
        v-on:delectReceiptsState="delectReceiptsState"
        :auditPopup='popup.auditPopup'
        :receiptsOrderNum='popup.receiptsOrderNum'
        :auditsReceiptsForm='popup.auditsReceiptsForm'
        ></audit-receipts>
        <!-- 错误弹窗 -->
        <error></error>
    </div>
</template>
<script>
import {productDetailStatus} from './../../../../Api/commonality/status'
import {operateProductList} from './../../../../Api/commonality/operate'
import {receiptOptionsName} from '../../../../Api/commonality/status'
import error from "./../../CommonalityComponent/popupTemplate/error"
import affirm from "./../../CommonalityComponent/popupTemplate/affirm"
import {seekGoodsList} from './../../../../Api/commonality/seek'
import {mapActions, mapGetters} from 'vuex'
import auditReceipts from './../../../Work/CommonalityComponent/popupTemplate/AuditReceipts'
import delectReceipts from './../../../Work/CommonalityComponent/popupTemplate/DelectReceipts'
import sippingReceiptsIntro from '../../../../components/work/SippingReceiptsIntro.vue' // 单据简介
export default {
    props: [
        'checkType', // 审核状态
        'newDatas', // 上一页带过来的数据
        'closeRouterUrl' // 关闭的路由地址
    ],
    data () {
        return {
            tittleThs: [
                {"name": '序号'},
                {"name": '商品编码'},
                {"name": '产品类别'},
                {"name": '首饰名称'},
                {"name": '总件重（g）'},
                {"name": '净金重'},
                {"name": '证书号'},
                {"name": '主石'},
                {"name": '颜色'},
                {"name": '净度'},
                {"name": '副石'},
                {"name": '售价'}
            ],
            configData: {
                "barcode": null, // 条形码
                "cargoType": null, // 产品类别
                "jewelryName": null, // 首饰名称
                "totalWeight": null, // 总件重
                "netWeight": null, // 净金重
                "certifiNo": null, // 证书号
                "mainName": null, // 主石
                "color": null, // 颜色
                "neatNess": null, // 净度
                "deputyName": null, // 副石
                "soldPrice": null // 售价
            },
            "pageSize": 10,
            "datas": {
                "postDataList": [], // 请求回来的数据集合(配置好的)
                "newGapDatas": [] // 新的空白数据
            },
            "popup": {
                "fullscreenLoading": false, // 保存过程的过场动画
                "successData": null, // 制单成功数据
                "deleteReceipts": false, // 删除单据弹窗
                "receiptsOrderNum": null, // 操作的单据号
                "auditsReceiptsForm": null, // 操作1 删除 3 取消审核 4 驳回审核 5 通过审核 6 撤销审核
                "auditPopup": false // 审核弹窗
            },
            "crudData": { // 增查更删数据
                "currentPage": 1, // 当前页
                "checked": false, // 监听全选
                "checkList": [], // 选择按钮（选中的数据集合）删除
                "operationCut": true // 操作按钮切换
            }
        }
    },
    components: {
        error,
        affirm,
        auditReceipts,
        sippingReceiptsIntro,
        delectReceipts
    },
    computed: {
        ...mapGetters([
            "userInfo", // 用户基本信息
            "saveSuccess", // 保存弹窗
            "saveSuccessData" // 保存弹窗数据
        ]),
        paginationSize () { // 分页的大小
            return this.allDataList.length;
        },
        allDataList () { // 总数据集合
            return [...this.datas.postDataList, ...this.datas.newGapDatas];
        },
        showDatas () { // 最终显示的数据
            let state = ((this.crudData.currentPage - 1) * this.pageSize);
            let end = (this.crudData.currentPage * this.pageSize);
            return this.allDataList.slice(state, end);
        },
        getCheckedPage () { // 监听当页的所有做选择的变量（删除选择）
            var CheckedPage = [];
            let Num = ((this.crudData.currentPage - 1) * this.pageSize);
            for (var i = 0; i < this.showDatas.length; i++) {
                CheckedPage.push(Num)
                Num += 1;
            }
            return CheckedPage;
        },
        currentPageChecked () { // 抽出当页已经选中的数据集合
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
        },
        newPage () { // 总页数
            return Math.ceil(this.paginationSize / this.pageSize);
        },
        getOptionsName () { // 操作名字
            return receiptOptionsName(this.checkType);
        }
    },
    created () {
        this.getGoodsList();
    },
    methods: {
        ...mapActions([
            "receiptAddOrCheck", // 单据新增/提交审核/保存（销售除外）
            "workProductDetail", // 商品列表
            "workPopupError" // 错误弹窗
        ]),
        handleSizeChange (val) {
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange (val) {
            this.crudData.currentPage = val;
            if (this.newPage <= val) { // 最后一页才给选择
                if (this.datas.newGapDatas.length > 0) { // 有空白数据的情况下
                    this.getFocus();
                }
            }
            console.log(`当前页: ${val}`);
        },
        delectReceiptsState (parm) { // 监听确认删除弹窗和操作成功
            if (parm.type === "2") { // 单据操作成功/删除成功
                if (this.popup.deleteReceipts) { // 删除单据
                    this.close();
                } else {
                    this.$emit("getSeekSellReceiptsIntro"); // 单据操作成功
                }
                // this.filterFun();
            }
            this.popup.deleteReceipts = parm.name;
            this.popup.auditPopup = parm.name;
        },
        getFocus () {
            var endIndex = this.allDataList.length - 1;
            if (this.newPage > 1) {
                endIndex = endIndex - ((this.newPage - 1) * this.pageSize);
            }
            var endFacus = "xh" + endIndex;
            console.log("当前的抗");
            console.log(endFacus);
            this.$refs[endFacus][0].focus()
        },
        getGoodsList () { // 获取商品列表
            var options = {
                "orderNum": sessionStorage.getItem("orderNumber"),
                "page": this.crudData.currentPage,
                "pageSize": "30"
            }
            seekGoodsList(options).then((response) => {
                console.log("商品列博鳌123");
                console.log(response);
                if (response.data.state === 200) {
                    if (response.data.data.productList.length > 0) {
                        for (let j = 0; j < response.data.data.productList.length; j++) {
                            var datas = this.getconfigData(response.data.data.productList[j]);
                            this.datas.postDataList.push(datas);
                        }
                    } else {
                        this.appendRow();
                    }
                } else {
                    this.workPopupError(response.data.msg)
                }
            }, (response) => {
                console.log(response);
            })
        },
        appendRow () { // 新增
            var rowData = JSON.stringify(this.configData);
            var newRowData = JSON.parse(rowData);
            this.datas.newGapDatas.push(newRowData);
            this.controlPage();
        },
        appendBatchRow () { // 批量新增
            alert("特别功能敬请期待");
        },
        printReceipts () { // 打印单据
            alert("特别功能敬请期待");
        },
        auditFun () { // 提交审核
            this.popup.receiptsOrderNum = sessionStorage.getItem("orderNumber");
            this.popup.auditsReceiptsForm = sessionStorage.getItem("checkType");
            this.popup.auditPopup = true;
        },
        controlPage () { // 控制分页
            var _self = this;
            if (this.newPage > this.crudData.currentPage) {
                this.crudData.currentPage = this.newPage; // 控制分页
            }
            setTimeout(function () {
                _self.getFocus();
            }, 100);
        },
        getconfigData (parm) { // 得到配置代码
            let configDatas = {};
            for (let key in this.configData) {
                configDatas[key] = parm[key] || null
            }
            return configDatas;
        },
        delProduct (barcode, index, operate, showData) {
            if (showData.barcode) {
                this.queryBarCode(barcode, index, operate);
            } else {
                this.datas.newGapDatas.splice((index - this.datas.postDataList.length), 1);
            }
        },
        queryBarCode (barcode, index, operate) { // 通过条码号操作数据
            if (barcode) {
                let options = {
                    "productList": [
                        {
                            "barcode": barcode
                        }
                    ],
                    "operate": operate,
                    "orderNum": sessionStorage.getItem("orderNumber")
                }
                operateProductList(options).then((response) => {
                    console.log("条形码查商品出错bug");
                    console.log(response);
                    if (response.data.state === 200) {
                        this.$emit("closePopup", false)
                        this.$emit("getSeekSellReceiptsIntro")
                        if (operate === "1") { // 新增
                            this.savaOperateSuccess(response.data.data, index);
                        } else if (operate === "2") { // 删除
                            this.delOperateSuccess(index);
                        }
                    } else {
                        alert(response.data.msg);
                    }
                }, (response) => {
                    console.log(response)
                })
            }
        },
        savaOperateSuccess (parm, index) { // 保存成功后的回调
            if (this.datas.newGapDatas.length < 2) {
                this.datas.newGapDatas[0].barcode = null;
            } else {
                for (var i = 0; i < this.datas.newGapDatas.length; i++) {
                    if (this.datas.newGapDatas[i].barcode === parm.barcode) {
                        this.datas.newGapDatas.splice(i, 1);
                    }
                }
            }
            var datas = this.getconfigData(parm);
            this.datas.postDataList.push(datas);
            this.controlPage();
        },
        delOperateSuccess (index) { // 删除成功后的回调
            this.datas.postDataList.splice(index, 1);
        },
        getAuditStatus (parm) { // 审核状态
            return productDetailStatus(parm);
        },
        delectReceipts (parm) {
            this.popup.deleteReceipts = true;
            this.popup.receiptsOrderNum = sessionStorage.getItem("orderNumber");
        },
        close () { // 关闭
            this.$router.push(this.closeRouterUrl);
        }
    }
}
</script>
<style src="../../../../assets/css/_table.scss" lang="scss" scoped></style>
<style lang="scss" scoped>
.new-template{
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    border: 2px solid #000;
    h1{
        font-size: 20px;
        color: red;
    }
    .header-wrap{
        height: 60px;
        li{
            margin-top: 20px;
            cursor: pointer;
        }
    }
}
.right-receipts-data-list{
    display: inline-block;
    width: 300px;
    background-color: #fff;
}
.table-template{
    min-width: 100%;
    height: 600px;
    overflow: auto;
    border: 2px solid #000;
}
</style>