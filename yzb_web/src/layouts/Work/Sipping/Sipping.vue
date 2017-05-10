<template>
    <div class="receipts-main">
        <div class="header-options">
            <!-- 头部的上 -->
            <div class="header-options-top">
                <div class="header-seslect" :class="{'header-seslect-on': selectPopup}" v-on:mouseover="openSelect"><span>全部分类</span><i></i></div>
                <div class="seek-wrap">
                    <input type="text" placeholder="请输入单据号查询" v-model="seekNumber">
                    <div @click="seekFun">
                        <img src="./../../../assets/img/work/seek-icon.png" alt="">
                    </div>
                    <i @click="closeSeek"></i>
                </div>
                <div v-if="selectPopup" class="header-seslect-body" v-on:mouseleave="outHeaderSeslectBody">
                    <div class="header-select-wrap">
                        <div class="one-line-data">
                            <h3>发货库位</h3>
                            <div class="one-line-right">
                                <h5 :class="{'all-on': selectData.warehouse.length < 1}" @click="getAllquery('warehouse')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in repositoryList">                              
                                        <li @click="getWarehouseId(item, index)">
                                            <span :class="{on: selectData.warehouse[index]}">{{item.repositoryName}}</span>
                                        </li>
                                    </template>
                                </ul>
                            </div>
                        </div>
                        <div class="one-line-data">
                            <h3>审核状态</h3>
                            <div class="one-line-right">
                                <h5 :class="{'all-on': selectData.auditState.length < 1}" @click="getAllquery('auditState')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in datas.audits">                          
                                        <li @click="getSort(item, index)">
                                            <span :class="{on: selectData.auditState[index]}">{{item.name}}</span>
                                        </li>
                                    </template>
                                </ul>
                            </div>
                        </div>
                        <div class="one-line-data">
                            <h3>收货店铺</h3>
                            <div class="one-line-right">
                                <h5 :class="{'all-on': selectData.supplier.length < 1}" @click="getAllquery('supplier')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in shopListByCo">                 
                                        <li @click="getsupplier(item, index)">
                                            <span :class="{on: selectData.supplier[index]}">{{item.shopName}}</span>
                                        </li>
                                    </template>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <ul class="header-seslect-bottom">
                        <li @click="resetQuery">重置</li>
                        <li @click="confirm">完成</li>
                    </ul>
                </div>
                <div class="sell-new-data" @click="newReceipt">
                    <i>+</i><span>新建发库单</span>
                </div>
            </div>
             <!-- 头部的下 -->
            <div class="header-options-bottom" v-if="this.queryList.length > 0">
                <a @click="emptyQuery" class="bottom-empty" href="javascrit:void(0)">清空查询条件</a>
                <ul>
                    <li :class="{'li-on': selectData.littleSelect[index]}" v-on:mouseover="openList(index)" v-for="(item, index) in queryList" v-on:mouseleave="outList(index)">
                        <span class="main-color">{{item.typeName}}</span>
                        <img src="./../../../assets/img/work/arrows-right-main.png" alt="">
                        <span>{{item.name}}</span>
                        <i class="trl"></i>
                        <i class="del-icon" @click="delectQuery(index, item)"></i>
                        <i :class="{'blank-color': selectData.littleSelect[index]}"></i>
                        <template v-if='item.typeName === "发货库位"'>
                            <div v-on:mouseover="openList(index)" class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in repositoryList">                                      
                                    <span @click="cutWarehouseData(item2, index2, index, item.typeName)" :class="{on: selectData.auditState[index]}">
                                        {{item2.repositoryName}}
                                    </span>
                                </template>
                            </div>
                        </template>
                        <template v-if='item.typeName === "审核状态"' v-on:mouseover="openList(index)">
                            <div  class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in datas.audits">                                      
                                    <span @click="cutAuditsData(item2, index2, index, item.typeName)" :class="{on: selectData.auditState[index]}">
                                        {{item2.name}}
                                    </span>
                                </template>
                            </div>
                        </template>
                        <template v-if='item.typeName === "收货店铺"'>
                            <div v-on:mouseover="openList(index)" class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in shopListByCo">                                      
                                    <span @click="cutShopByCo(item2, index2, index, item.typeName)" :class="{on: selectData.auditState[index]}">
                                        {{item2.shopName}}
                                    </span>
                                </template>
                            </div>
                        </template>
                    </li>
                </ul>
            </div>
        </div>
        <div id="xiaoH" class="receipts-list" @scroll="onScroll($event)">
            <div class="receipts-list-body" v-if="item" v-for="item in showList">
                <div class="receipts-list-body-top">
                    <div class="options-left" @click="goDetail(item.orderNum, item.checkType)">
                        <p class="main-color">单号：{{item.orderNum}}</p>
                        <p>制单人：{{item.makeOrderManName}}</p>
                        <p>
                            制单时间：
                            <span v-text="preciseSun(item.createDate)"></span>
                            <span class="ml10" v-text="preciseMinute(item.createDate)"></span>
                        </p>
                    </div>
                    <div class="options-btn">
                        <template v-if='item.checkType !== "3"'>
                            <a href="javascript: void(0)" @click="auditFun(item.checkType, item.orderNum)">
                                {{getOptionsName(item.checkType, item.receiptStatus)}}
                            </a>
                            <a v-if="item.checkType === '2'" class="del-btn" href="javascript: void(0)" @click="auditFun('4', item.orderNum)">驳回审核</a>
                            <a v-if="item.checkType === '1'" class="del-bg del-btn" href="javascript: void(0)" @click="delectReceipts(item.orderNum)">删除</a>
                        </template>
                        <template v-else>
                            <a href="javascript: void(0)" @click="auditFun(item.checkType, item.orderNum)">
                                {{getOptionsName(item.checkType, item.receiptStatus)}}
                            </a>
                             <a class="extra-color" v-if='item.receiptStatus === "1"' href="javascript: void(0)" @click="operateTakeOrderFun(item.orderNum)">
                                确认收货
                            </a>
                        </template>
                    </div>
                </div>
                <div class="receipts-list-body-bottom" @click="goDetail(item.orderNum, item.checkType)">
                    <ul class="condition-two">
                        <!-- <li>{{item}}</li> -->
                        <li>发货库位：{{item.storageName}}</li>
                        <li>收货店铺：{{item.shopName}}</li>
                        <li>商品属性：{{item.totalNum}} 件</li>
                        <li>总件数：{{item.totalWeight}} g</li>
                        <li>总重量：{{item.totalWeight}} g</li>
                        <li>总售价：{{item.totalPrice}} 元</li>
                    </ul>
                    <div class="audit-state">
                        <img :src="getState(item.checkType, item.receiptStatus)" alt="">
                    </div>
                </div>
            </div>
        </div>
        <!-- 弹窗 -->
        <!-- 新建单据 -->
        <div class="new-receipts-wrap" v-model="newPopup.main">
            <new-sipping-receipts
                :newPopup="newPopup.main"
                v-on:closePopup="closePopup"
            ></new-sipping-receipts>
        </div>
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
<!--         <take-delivery-of-goods-receipts
            v-on:delectReceiptsState="delectReceiptsState"
            :takeDeliveryOfGoodsPopup='popup.takeDeliveryOfGoodsPopup'
            :receiptsOrderNum='popup.receiptsOrderNum'
            :auditsReceiptsForm='popup.auditsReceiptsForm'
        >
        </take-delivery-of-goods-receipts> -->
    </div>
</template>
<script>
import Vue from 'vue'
import {mapGetters, mapActions} from "vuex"
import {seekSendStorageList} from './../../../Api/commonality/seek'
import {operateDelReceipt} from './../../../Api/commonality/operate'
import {receiptOptionsName} from './../../../Api/commonality/status'
import delectReceipts from './../../Work/CommonalityComponent/popupTemplate/DelectReceipts'
import auditReceipts from './../../Work/CommonalityComponent/popupTemplate/AuditReceipts'
import newSippingReceipts from '../../../components/work/NewSippingReceipts' // 新建单据弹窗
// import takeDeliveryOfGoodsReceipts from '../../../components/work/TakeDeliveryOfGoodsReceipts' // 收货
export default {
    data () {
        return {
            "selectPopup": false, // 选择下拉框
            "selectData": { // 选择样式
                "warehouse": [], // 发货库位
                "auditState": [], // 审核状态
                "supplier": [], // 收货店铺
                "littleSelect": [] // 小类的下拉框
            },
            "queryList": [], // 查询列表
            "selectObject": { // 选择数据
                "warehouse": null, // 发货库位
                "auditState": null, // 审核状态
                "supplier": null // 收货店铺
            },
            "showList": [], // 单据列表
            "datas": {
                "shopList": [], // 店铺列表
                "audits": [
                    // {
                    //     "name": "全部状态",
                    //     "type": "-1"
                    // },
                    {
                        "name": "待审核",
                        "type": "1"
                    },
                    {
                        "name": "审核中",
                        "type": "2"
                    },
                    {
                        "name": "已审核",
                        "type": "3"
                    }
                ],
                "supplierList": [
                    {
                        "name": "已收银",
                        "type": "1"
                    },
                    {
                        "name": "待收银",
                        "type": "2"
                    }
                ]
            },
            "newPopup": { // 新建单据的弹窗
                "main": false // 新建的弹窗
            },
            "popup": { // 弹窗
                "productClass": false, // 产品类型
                "supplierAnRepository": false, // 供应商和库位
                "deleteReceipts": false, // 删除单据弹窗
                "receiptsOrderNum": null, // 操作的单据号
                "auditsReceiptsForm": null, // 操作1 删除 3 取消审核 4 驳回审核 5 通过审核 6 撤销审核
                "auditPopup": false, // 审核弹窗
                "takeDeliveryOfGoodsPopup": false // 收货弹窗
            },
            "seekNumber": "", // 搜索单据号
            "page": 1
        }
    },
    components: {
        delectReceipts,
        auditReceipts,
        newSippingReceipts
    },
    created () {
        this.filterFun(); // 获取单据列表
        this.workRepositoryList(); // 库位列表
        this.getShopListByCo(); // 店铺列表
        this.workProductClass(); // 产品类别
        this.workSupplierList(); // 供应商
    },
    computed: {
        ...mapGetters([
            "repositoryList", // 库位列表
            "shopListByCo", // 店铺列表
            "productClass", // 产品类别
            "supplierListData" // 供应商
        ])
    },
    methods: {
        ...mapActions([
            "workRepositoryList", // 库位列表
            "getShopListByCo", // 店铺列表
            "workProductClass", // 产品类别
            "workSupplierList" // 供应商
        ]),
        preciseSun (parm) { // 年
            var Year = parm.slice(0, 4);
            var Month = parm.slice(4, 6);
            var Sun = parm.slice(6, 8);
            var allTime = Year + "-" + Month + "-" + Sun;
            return allTime;
        },
        preciseMinute (parm) { // 月
            var ConfigData = parm.slice(8, 10);
            var Mour = parm.slice(10, 12);
            var allTime = ConfigData + ":" + Mour;
            return allTime;
        },
        closePopup (parm) { // 关闭新建弹窗
            this.newPopup.main = parm;
        },
        seekFun () { // 通过单据号查询
            this.showList = [];
            this.filterFun();
        },
        onScroll (e) { // 滚动加载
            var scrollHeight = e.currentTarget.scrollHeight; // 元素可以滚动的高度
            var clientHeight = e.currentTarget.clientHeight; // 元素的高度
            var scrollTop = e.currentTarget.scrollTop; // 滚动了的距离
            if (clientHeight + scrollTop >= scrollHeight) {
                this.filterFun(this.cbFun);
            }
        },
        cbFun () { // 滚动页数变动
            this.page += 1;
        },
        newReceipt (parm) { // 新建单据
            this.newPopup.main = true;
        },
        delectReceiptsState (parm) { // 监听确认删除弹窗和操作成功
            if (parm.type === "2") { // 单据操作成功/删除成功
                this.showList = [];
                this.filterFun();
            }
            this.popup.deleteReceipts = parm.name;
            this.popup.auditPopup = parm.name;
            this.popup.takeDeliveryOfGoodsPopup = parm.name
        },
        goDetail (orderNum, checkType) {
            sessionStorage.setItem("orderNumber", orderNum);
            this.$router.push("/work/sipping/newSipping");
        },
        getOptionsName (parm, receiptStatus) { // 操作名字
            return receiptOptionsName(parm);
        },
        getState (parm, receiptStatus) { // 审核状态
            switch (parm) {
                case "1":
                    return "./../../../../static/img/audit-1.png";
                case "2":
                    return "./../../../../static/img/audit-2.png";
                case "3":
                    if (receiptStatus === "1") {
                        return "./../../../../static/img/audit-5.png";
                    } else {
                        return "./../../../../static/img/audit-4.png";
                    }
            }
        },
        openSelect () { // 打开下拉框(全部)
            this.selectPopup = true;
        },
        openList (parm) { // 打开下拉框(小类的)
            this.outList();
            Vue.set(this.selectData.littleSelect, parm, true);
        },
        outList () { // 关闭下拉框(小类)
            this.selectData.littleSelect = [];
        },
        resetQuery () { // 重置
            this.selectData.auditState = [];
            this.selectObject.auditState = [];
            this.selectData.warehouse = [];
            this.selectObject.warehouse = null;
        },
        closeSeek () { // 关闭查询
            this.seekNumber = "";
            this.filterFun();
        },
        filterFun (cbFun) { // 定位选择的过滤数据
            var warehouse = null; // 发货库位
            var auditState = null; // 审核状态
            var supplier = null; // 收货店铺
            if (this.selectObject.warehouse) { // 发货库位
                warehouse = this.selectObject.warehouse.repositoryId;
            }
            if (this.selectObject.auditState) { // 审核状态
                auditState = this.selectObject.auditState.id;
            }
            if (this.selectObject.supplier) { // 收货店铺
                supplier = this.selectObject.supplier.id;
            }
            let options = {
                "orderBy": "2", // 1智能排序2按制单时间倒序3按制单时间升序4按审核状态排列
                "shopId": supplier || "-1", // 店铺ID
                "checkType": auditState || "-1", // 审核状态-1，全部状态1，待审核2，审核中3，已审核
                "storageId": warehouse || "-1", // 发货库位
                "receiptStatus": "-1", // 收货状态
                "orderNum": this.seekNumber || "",
                "page": this.page || "1",
                "pageSize": "30"
            }
            seekSendStorageList(options).then((response) => {
                console.log("重新获取了");
                console.log(response);
                if (response.data.state === 200) {
                    this.showList.push(...response.data.data.orderList);
                    if (cbFun) {
                        cbFun();
                    }
                } else {
                    alert(response.data.msg)
                }
            }, (response) => {
                console.log(response);
            })
        },
        outHeaderSeslectBody () { // 关闭选择框
            this.selectPopup = false;
        },
        confirm () { // 确定
            this.selectPopup = false;
            this.queryList = [];
            for (var i of Object.keys(this.selectObject)) {
                if (this.selectObject[i]) {
                    this.queryList.push(this.selectObject[i]);
                }
            }
            this.filterFun(); // 请求数据
        },
        cutAuditsData (item2, index2, index, typeName) { // 切换审核状态(小类)
            this.queryList[index].name = item2.name;
            this.queryList[index].type = item2.type;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        cutWarehouseData (item2, index2, index, typeName) { // 切换发货库位(小类)
            this.queryList[index].name = item2.repositoryName;
            this.queryList[index].id = item2.repositoryId;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        cutShopByCo (item2, index2, index, typeName) { // 切换收货店铺
            this.queryList[index].name = item2.shopName;
            this.queryList[index].id = item2.shopId;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        amendFun (item2, index2, typeName) { // 修改选择数据item2, index, typeName
            switch (typeName) {
                case "审核状态":
                    this.selectData.auditState = [];
                    Vue.set(this.selectData.auditState, index2, true);
                    this.selectObject.auditState = item2;
                    break;
                case "发货库位":
                    this.selectData.warehouse = [];
                    Vue.set(this.selectData.warehouse, index2, true);
                    this.selectObject.warehouse = item2;
                    break;
                case "收货店铺":
                    this.selectData.supplier = [];
                    Vue.set(this.selectData.supplier, index2, true);
                    this.selectObject.supplier = item2;
                    break;
            }
        },
        delectQuery (parm, item) { // 删除一条选中数据
            // console.log(parm);
            Vue.delete(this.queryList, parm);
            console.log(item.typeName)
            sessionStorage.setItem("删除一条选中数据", JSON.stringify(this.queryList));
            var shopId = null;
            var cashStatus = null;
            switch (item.typeName) {
                case "收货店铺":
                    cashStatus = "-1";
                    this.selectData.supplier = [];
                    this.selectObject.supplier = null;
                    break;
                case "发货库位":
                    shopId = "-1";
                    this.selectData.warehouse = [];
                    this.selectObject.warehouse = null;
                    break;
                case "审核状态":
                    this.selectData.auditState = [];
                    this.selectObject.auditState = null;
                    break;
            }
            if (this.selectObject.warehouse) {
                shopId = this.selectObject.warehouse.id
            }
            if (this.selectObject.supplier) {
                cashStatus = this.selectObject.supplier.id
            }
            console.log(shopId);
            console.log(cashStatus);
        },
        emptyQuery () { // 清空查询筛选条件
            this.queryList = [];
        },
        getWarehouseId (parm, index) { // 选择发货库位
            let selectObject = {
                "typeName": "发货库位",
                "name": parm.repositoryName,
                "repositoryId": parm.repositoryId
            }
            this.selectObject.warehouse = selectObject;
            this.selectData.warehouse = [];
            Vue.set(this.selectData.warehouse, index, true)
        },
        getAllquery (parm) { // 全选
            this.selectData[parm] = [];
            this.selectObject[parm] = null;
        },
        getSort (parm, index) { // 审核状态
            let selectObject = {
                "typeName": "审核状态",
                "name": parm.name,
                "id": parm.type
            }
            this.selectObject.auditState = selectObject;
            this.selectData.auditState = [];
            Vue.set(this.selectData.auditState, index, true)
        },
        getsupplier (parm, index) { // 收货店铺
            let selectObject = {
                "typeName": "收货店铺",
                "name": parm.shopName,
                "id": parm.shopId
            }
            this.selectObject.supplier = selectObject;
            this.selectData.supplier = [];
            Vue.set(this.selectData.supplier, index, true)
        },
        delectProduct (parm) { // 删除商品
            let _seft = this;
            let options = {
                "orderNum": parm
            }
            operateDelReceipt(options).then(response => {
                if (response.data.state === 200) {
                    alert("删除成功")
                    _seft.getAll(); // 待会改不请求
                    // alert("删除成功")
                } else {
                    alert(response.data.msg)
                }
            }, response => {
                console.log(response)
            })
        },
        auditFun (checkType, orderNum) { // 提交审核
            this.popup.receiptsOrderNum = orderNum;
            this.popup.auditsReceiptsForm = checkType;
            this.popup.auditPopup = true;
        },
        operateTakeOrderFun (orderNum) {
            this.popup.receiptsOrderNum = orderNum;
            this.popup.auditsReceiptsForm = "9";
            this.popup.auditPopup = true;
        },
        delectReceipts (parm) {
            this.popup.deleteReceipts = true;
            this.popup.receiptsOrderNum = parm;
        }
    }
}
</script>
<style src="../../../assets/css/template/receiptsList.scss" lang="scss" scoped></style>