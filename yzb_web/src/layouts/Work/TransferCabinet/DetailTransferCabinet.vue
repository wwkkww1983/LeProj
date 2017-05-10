<template>
    <div class="storage">
        <div class="sipping-list-left">
            <div class="main-box">
                <div class="sipping-header">
                    <ul class="sipping-nav">
                        <router-link tag="li" to="/work/transferCabinet">调库单据<span>&nbsp>&nbsp</span></router-link>
                        <!--<router-link tag="li" to="/work/sell/sellReceiptsList" :class="{on: !newRoute}">产品列表<span v-if="newRoute">&nbsp>&nbsp</span></router-link>-->
                        <li v-if="!newRoute" :class="{on: !newRoute}">产品列表</li>
                        <!--  <router-link tag="li" to="/work/sell/sellReceiptsList/sellDetail" class="on"></router-link> 
                        <li v-if="newRoute" :class="{on: newRoute}">产品明细</li> -->
                    </ul>
                    <ul class="header-btns">
                        <li>打印单据</li>
                        <li v-if="jurisdiction.single" @click="appendRow">单个添加</li>
                        <li v-if="jurisdiction.single" @click="appendRow">批量添加</li>
                        <li v-if="jurisdiction.single" @click="batchDelect">删除单据</li>
                        <li v-if="jurisdiction.single" @click="addSingleAll">保存</li>
                        <li @click="close">关闭</li>           
                    </ul>
                </div>
        <!--<div class="main-box">
            <header class="header-box">
                <div class="header-seek mt20">
                    <ul class="header-operation-btn">
                        <li>
                            <span>单号：<span class="main-color" v-if="receiptDetail">{{receiptDetail.orderNum}}</span></span>
                        </li>
                        <li>
                            <span>调出柜组：<span class="main-color" v-if="receiptDetail">{{receiptDetail.outShopGroup}}</span></span>
                        </li>
                        <li>
                            <span>商品属性：<span class="main-color" v-if="receiptDetail">{{receiptDetail.productTpye}}</span></span>
                        </li>
                        <li>
                            <span>调入柜组：<span class="main-color" v-if="receiptDetail">{{receiptDetail.shopGroup}}</span></span>
                        </li>
                        <li>
                            <span>备注：</span>
                            <div class="input-w300">     
                                <el-input v-model="header.orderRemark"></el-input>
                            </div>
                        </li>
                    </ul>
                </div>
            </header>-->
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
                                                    <input class="input-default" v-model="showData[key]" @keyup.enter="queryBarCode(showData.barcode, index)"></input>
                                                </template>
                                                <p v-else>{{showData[key]}}</p>
                                            </td>
                                        </template>
                                    </template>
                                    <td v-if="jurisdiction.single" @click="delProduct(index)">删除</td>
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
            <!--<footer class="footer-box">
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
            -->
                <!-- 弹窗 -->
                <error></error>
                <affirm></affirm>
            </div>
        </div>
        <div class="sipping-list-right">            
            <div class="plain-receipts-detail">
                <!-- 单据信息 -->
                <plain-receipts-detail
                v-bind:receiptsIntroList="receiptsIntroList"
                :receiptDescription="receiptDescription"
                ></plain-receipts-detail>
            </div>
            <!--<div class="plain-money-data"> 备注信息
                <remark-template
                :receiptRemark="receiptRemark"
                :receiptDescription="receiptDescription"
                ></remark-template>
            </div>-->
        </div>
    </div>
</template>
<script>
import error from "./../CommonalityComponent/popupTemplate/error"
import affirm from "./../CommonalityComponent/popupTemplate/affirm"
import {mapActions, mapGetters} from 'vuex'
// import {delectProductDetail} from './../../../Api/commonality/operate'
import {operateSubmitAudit, operateReceiptOperation, deleteByOrderNumber} from './../../../Api/commonality/operate'
import {productDetailStatus} from './../../../Api/commonality/status'
import {seekProductDetail, seekReceiptDGSynopsis} from './../../../Api/commonality/seek'
import plainReceiptsDetail from "./../CommonalityComponent/billDetials/PlainTransferStoragel"
export default {
    data () {
        return {
            pickerOptions0: {
                disabledDate(time) {
                    return time.getTime() < Date.now() - 8.64e7;
                }
            },
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
                {"name": '售价'},
                {"name": '操作'}
            ],
            configData: {
                "productId": null, // 商品id（删除用）
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
            newDatas: JSON.parse(sessionStorage.getItem("newDatas")), // 上一步带过来的数据
            header: {
                orderRemark: null, // 单据备注
                shopId: null, // 分销商Id
                shopIdWatch: null, // 监听shopId
                shopName: null, // 分销商名
                orderNumber: null, // 单号
                warehouse: null, // 发货库位
                warehouseId: null // 发货库位Id
            },
            datas: {
                postDatas: [], // 请求回来的原始数据
                postDataList: [], // 请求回来的数据集合(配置好的)
                newGapDatas: [] // 新的空白数据
                // allDataList: [] // 总数据
            },
            crudData: { // 增查更删数据
                currentPage: 1, // 当前页
                checked: false, // 监听全选
                checkList: [], // 选择按钮（选中的数据集合）删除
                operationCut: true // 操作按钮切换
            },
            newRoute: false,
            jurisdiction: { // 权限
                "single": false // 制单人
            },
            receiptsIntroList: "", // 单据详细
            receiptDescription: "" // 单据信息
            // receiptRemark: ""
        }
    },
    components: {
        affirm, error, plainReceiptsDetail
    },
    computed: {
        ...mapGetters([
            // "shopLists", // 分销商
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
                CheckedPage.push(Num)
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
        this.header.orderRemark = this.receiptDetail.orderRemark;
        if (this.getProductDetail) {
            console.log("拿到");
            console.log(this.getProductDetail);
            console.log("拿到");
        }
        this.workShopList(); // 店铺列表（计划分销）
        this.judgeProductDetail(); // 如果没有商品列表重新获取一次(避免用户刷新)
        this.workReceiptDetail({
            "orderNumber": this.newDatas.orderNumber
        }); // 单据详情
    },
    watch: {
        'crudData.checkList': function () { // 监听全选
            console.log(this.crudData.checkList.length);
            console.log(this.showDatas.length);
            if (this.currentPageChecked.length === this.showDatas.length) {
                this.crudData.checked = true;
            } else {
                this.crudData.checked = false;
            }
        },
        'crudData.currentPage': function () { // 分页后重新判断全选按钮是否选中
            if (this.currentPageChecked.length === this.showDatas.length) {
                this.crudData.checked = true;
            } else {
                this.crudData.checked = false;
            }
        },
        'getProductDetail': function () { // 保存成功后，清掉请求回来新增数据
            this.datas.postDataList = [];
        }
        // 'receiptDetail': function () { // 监听单据详情
        //     this.header.shopId = this.receiptDetail.shopId;
        // }
    },
    methods: {
        ...mapActions([
            "workProductDetail", // 商品列表
            "workShopList", // 店铺列表（计划分销）
            "receiptAddOrCheck", // 单据新增/提交审核/保存（销售除外）
            "workReceiptDetail", // 单据详情
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
            if (!this.getProductDetail.productList) {
                this.workProductDetail({
                    "applyType": "8",
                    "objId": this.newDatas.orderNumber,
                    "type": "1"
                });
            }
            this.getAjaxDteil();
            // this.getReceiptRemark();
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
                this.workProductDetails(parm, index);
            }
        },
        workProductDetails (parm, index) { // 获取商品列表
            var data = {
                // "routerUrl": this.routerUrl,
                // "page": 1,
                "depShopType": this.newDatas.userType, // 操作部门
                "pageSize": "1",
                "objId": parm, // 单据id
                "obj2Id": this.receiptDetail.outShopGroupId, // (type=5 and applyType=8)时为柜组id
                // "sellType": "", // 销售类型1 销售 2 退换  3 截金 4 回收 5 兑换 6 典当
                "type": "5", // 操作类型 1 单据号 2 产品类别ID 3 商品ID列表 4 模糊查询 5 条码号
                // "productIdList": null // 商品ID列表
                "applyType": "8" // 当前应用类型：1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜（销售除外）
            };
            sessionStorage.setItem("JSON", JSON.stringify(data));
            seekProductDetail(data).then((response) => {
                console.log(response);
                if (response.data.state === 200) {
                    if (response.data.data.rowDataList) {
                        if (this.datas.newGapDatas.length < 2) {
                            this.datas.newGapDatas[0].barcode = null;
                        } else {
                            for (var i = 0; i < this.datas.newGapDatas.length; i++) {
                                if (this.datas.newGapDatas[i].barcode === parm) {
                                    this.datas.newGapDatas.splice(i, 1);
                                }
                            }
                        }
                        var datas = this.getconfigData(response.data.data.rowDataList[0]);
                        this.datas.postDataList.push(datas);
                        this.datas.postDatas.push(response.data.data.rowDataList[0]);
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
        // batchDelect () { // 确认删除（全空白的，新赠的有条形码，请求回来的已经保存了的）
        //     console.log("删除");
        //     let deleteList = []; // 新增
        //     let productIdList = []; // 商品Id
        //     for (let i of this.crudData.checkList) { // 如果没有条形码就push,带回加上去（删除新增的）
        //         if (!this.allDataList[i].barcode) {
        //             deleteList.push(this.allDataList[i]);
        //         } else {
        //             var ObjectId = { // 商品Id
        //                 "productId": this.allDataList[i].productId
        //             }
        //             productIdList.push(ObjectId);
        //         }
        //     }
        //     console.log("删除的收集");
        //     console.log(productIdList);
        //     let data = {
        //         "orderNum": this.receiptDetail.orderNum, // 单据号
        //         "productIdList": productIdList, // 商品ID列表
        //         "type": "1" // 操作类型：1 删除
        //     }
        //     delectProductDetail(data).then((response) => {
        //         console.log("删除商品列表999999999999999999999999");
        //         console.log(response);
        //         if (response.data.state === 200) {
        //             this.workProductDetail({ // 重新获取商品列表
        //                 "applyType": "8",
        //                 "objId": this.newDatas.orderNumber,
        //                 "type": "1"
        //             });
        //             alert("删除成功了")
        //         } else {
        //             alert(response.data.msg);
        //         }
        //     }, (response) => {
        //         console.log("删除商品列表cuo");
        //         console.log(response);
        //     })
        //     for (let i = 0; i < this.datas.newGapDatas.length; i++) { // 删除新的空白数据
        //         for (let j of deleteList) {
        //             if (this.datas.newGapDatas[i] === j) {
        //                 this.datas.newGapDatas.splice(i, 1)
        //             }
        //         }
        //     }
        //     this.crudData.checkList = [];
        // },
        getShopName (parm) { // 绑定分销商名称
            this.header.shopName = parm;
            console.log(parm);
        },
        addSingleAll () { // 保存，目前不做分页保存
            console.log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
            console.log(this.datas.postDatas);
            let productId = [];
            for (let i of this.datas.postDatas) {
                console.log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<")
                console.log(i.productId);
                productId.push(i.productId);
            }
            let data = {
                "orderId": sessionStorage.getItem("orderId"), // 单据ID
                "operationUnit": this.newDatas.userType, // 操作单位 1 部门 2 店铺
                "operationUnitId": this.newDatas.handleDeptId, // 操作单位ID
                "operationName": this.newDatas.handleDept, // 操作单位名称
                // "shopGroupId": this.newDatas, // 当前/调入柜组ID
                // "shopGroupName": this.newDatas, // 当前/调入柜组名
                // "outShopGroupId": this.newDatas.outShopGroupId, // 调出柜组ID
                // "outShopGroupName": this.newDatas.outShopGroupName, // 调出柜组名
                // "makeOrderUserId": sessionStorage.getItem("id"), // 制单人ID
                // "makeOrderUserName": sessionStorage.getItem("nickname"), // 制单人
                "orderRemark": this.header.orderRemark, // 单据备注
                "productTpye": "1", // 产品类型:1 成  品2 旧料
                // "supplierId": this.receiptDetail.supplierId, // 供应商ID
                // "supplierName": this.receiptDetail.supplier, // 供应商
                // "shopId": this.header.shopId, // 分销商ID
                "shopName": this.header.shopName, // 分销商名称
                // "warehouseId": this.newDatas.newLocationId, // 当前库位/发货库位/退货库位ID
                // "warehouseName": this.newDatas.newLocation, // 当前库位/发货库位/退货库位
                "productIdList": productId, // 商品ID 列表
                "productStatus": "1", // 1新增2删除
                "handleType": "1", // 操作类型 1 保存 2 提交审核 3 修改
                "type": "8" // 单据类型:1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜
            }
            sessionStorage.setItem("测试传入", JSON.stringify(data));
            this.receiptAddOrCheck(data);
        },
        close () { // 关闭
            this.$router.push("/work/transferCabinet");
        },
        getAjax () { // 权限设置
            if (sessionStorage.getItem("id") === this.receiptDescription.makeOrderManId) {  // 制单人
                this.jurisdiction.single = true;
                console.log("我是制单人")
            } else {
                this.tittleThs.splice(this.tittleThs.length - 1, 1)
                console.log("我没有删除权限")
            }
            console.log("我的天，要疯了")
            console.log(this.newDatas)
        },
        getAjaxDteil () { // 获取单据简介
            console.log(1234)
            console.log(this.newDatas.orderNumber)
            let options = {
                "orderNum": this.newDatas.orderNumber
            }
            seekReceiptDGSynopsis(options).then((response) => {
                // if (response.data.state === 200) {
                //     this.downUrl = response.data.data.url;
                // }
                // alert(options.orderNum)
                this.receiptDescription = response.data.data;
                console.log(this.receiptDescription)
                sessionStorage.setItem("receiptDes", JSON.stringify(this.receiptDescription))
                this.getAjax();
            }, (response) => {
                console.log(response);
            })
        },
        // getReceiptRemark () { // 获取单据备注
        //     let options = {
        //         "orderNum": this.newDatas.orderNumber
        //     }
        //     seekReceiptRemark(options).then((response) => {
        //         // if (response.data.state === 200) {
        //         //     this.downUrl = response.data.data.url;
        //         // }
        //         console.log(response);
        //         console.log("备注出来看看");
        //         this.receiptRemark = response.data.data;
        //     }, (response) => {
        //         console.log(response);
        //     })
        // },
        delProduct (index) { // 删除
            console.log(index)
            this.datas.newGapDatas.splice(index, 1)
        },
        submitAudit () { // 提交审核
            let options = {
                "orderNum": this.newDatas.orderNumber
            }
            operateSubmitAudit(options).then((response) => {
                console.log("提交审核");
                console.log(response);
            }, (response) => {
                console.log(response);
            })
        },
        passAudit () { // 通过审核
            this.AuditFunction("5")
        },
        refuseAudit () { // 驳回审核
            this.AuditFunction("4")
        },
        cancelAudit () { // 撤销审批
            this.AuditFunction("6")
        },
        AuditFunction (type) { // 审批方法
            let options = {
                "handleType": type, // 操作1 删除 3 取消审核 4 驳回审核 5 通过审核 6 撤销审核
                "orderIdList": [{"orderNum": this.newDatas.orderNumber}], // 单据号
                "checkRemark": "" // 审核批注
            }
            sessionStorage.setItem("审批", JSON.stringify(options))
            operateReceiptOperation(options).then((response) => {
                console.log(response);
                console.log(".....");
            }, (response) => {
                console.log(response);
            })
        },
        batchDelect () { // 删除单据
            let options = {
                "orderNum": this.newDatas.orderNumber // 单据号
            }
            deleteByOrderNumber(options).then((response) => {
                console.log(response);
                alert("删除成功")
                this.$router.push("/work/transferCabinet");
            }, (response) => {
                console.log(response);
            })
        }
    }
}
</script>
<style lang="scss" scoped>
$oa: #0abfab; // 主色
$d6: #d6d6d6; // 分割线颜色
$c3: #333333; // 导航栏文字颜色
$c2: #333742; // 标签栏底色
$es: #e5f5fd; // 标签设置编辑文本 出现旋转框
$f7: #f74c31; // 消息红点颜色
$a8: #a8a8a8; // 搜索页面icon 为选中的底色
$fb: #fb6155; // 删除按钮未选中的底色
$c9: #c9e5e2; // 按钮没选中状态
$f0: #f0f0f0; // 细线颜色
$c9: #999; // 列表内容文字色值
$c6: #666; // 写在标签栏块上的文字图标选中加深颜色
$e6: #e6f8f6; // 选中列表时的状态色
$f6: #f6f6f6; // 没选中列表时的状态色（偶）
$fd: #fd9526; // 按钮图标颜色
$bf: #49bbf3; // 标签设置编辑文本 出现旋转框描边 
$e8: #e8e9eb; // 表格头部的颜色
.storage{
    width: 1750px;
    height: 100%;
    padding: 0 20px;
    overflow-y: auto;
}
.new-product-class{ // 新建产品类别弹窗
    ul{
        font-size: 0;
        margin-bottom: 10px;
        border-bottom: 1px solid $d6;
        margin-top: 10px;
        li:first-child{
            width: 100%;
            height: 16px;
            line-height: 16px;
            font-size: 14px;
            display: block;
            color: $oa;
        }
        li{
            width: 25%;
            height: 50px;
            line-height: 50px;
            font-size: 0;
            display: inline-block;
        }
    }
}
.table-template{
    min-width: 100%;
    height: 100%;
    table{
        width: 100%;
        .tr-header{
            background-color: $e8!important;
            &:hover{
                background-color: $e8;
            }
        }
        tr{
            height: 40px;
            width: 100%;
        }
        tbody{
          tr:nth-child(odd){
              background-color: #f6f7f8;
          }
          tr:nth-child(even){
              background-color: #fff;
          }
          tr{
              &:hover{
                  background-color: $e6;
              }
          }
        }
        th,td{
            text-align: center;
            border-right: none;
            border-bottom: none;
            font-size: 12px;
            min-width: 70px;
            white-space:nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            line-height: 60px;
            padding: 0;
        }
        td:last-child{
            border-right: 0;
        }
        th{
            color: $c6;
             background: #fff;
        }
        td{
            color: $c3;
        }
    }
}
.input-default{
    padding: 10px;
    width: 100px;
    height: 20px;
    text-align: center;
    border: none;
    outline: none;
    background-color: transparent;
}
.sipping-list-left{
    float: left;
    width: 1230px;
    height: 100%;
    margin-left: 125px;
    // padding-bottom: 100px;
    .main-box{
        padding-top:20px;
    }
}
.sipping-list-right{
    float: left;
    margin: 20px 0 0 40px;
    width: 300px;
    // height: 100%;
    .plain-receipts-detail{
        background-color: #fff;
        width: 300px;
        padding-bottom: 10px;
        // height: 350px;
    }
    .plain-money-data{
        width: 300px;
        height: 400px;
        margin-top: 20px;
        background-color: #fafbfc;
    }
}
.sipping-header { // 头部导航
    background-color: #fafbfc;
    height: 60px;
    margin-bottom: 20px;
    overflow: hidden;
    position: relative;
    .header-btns{
        position: absolute;
        top: 11px;
        right: 0;
        li{
            display: inline-block;
            width: 90px;
            height: 38px;
            line-height: 38px;
            background: #83bbf7;
            border: 1px solid #83bbf7;
            text-align: center;
            border-radius: 4px;
            margin: 0 20px 0 -3px;
            font-size: 16px;
            color: #fff;
            &:hover{
                background: #4580bf;
                border: 1px solid #4580bf;
            }
            &:last-child{
                background: #fff;
                border: 1px solid #e58585;
                color: #e58585;
            }
        }            
    }
    .sipping-nav{
        font-size: 0;
        padding-left: 20px;
        li{
            // border-radius: 4px;
            font-size: 14px;
            height: 60px;
            line-height: 60px;
            // padding: 0 30px;
            // width: 140px;
            color: #333;
            text-align: center;
            display: inline-block;
            color: #46c4b4;
            &:hover{
                cursor: pointer;
            }
        }
        .on{
            color: #999;
                &:hover{
                cursor: default;
            }
        }
    }
}
</style>