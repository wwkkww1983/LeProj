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
                            <h3>店铺</h3>
                            <div class="one-line-right">
                                <h5 :class="{'all-on': selectData.warehouseOut.length < 1}" @click="getAllquery('warehouseOut')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in shopListByCo">                          
                                        <li @click="getSort(item, index)">
                                            <span :class="{on: selectData.warehouseOut[index]}">{{item.shopName}}</span>
                                        </li>
                                    </template>
                                </ul>
                            </div>
                        </div>
                        <div class="one-line-data">
                            <h3>调出柜组</h3>
                            <div class="one-line-right">
                                <h5 :class="{'all-on': selectData.warehouse.length < 1}" @click="getAllquery('warehouse')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in datas.counterList">                              
                                        <li @click="getWarehouseId(item, index)">
                                            <span :class="{on: selectData.warehouse[index]}">{{item.counterName}}</span>
                                        </li>
                                    </template>
                                </ul>
                            </div>
                        </div>
                        <div class="one-line-data">
                            <h3>调入柜组</h3>
                            <div class="one-line-right">
                                <h5 :class="{'all-on': selectData.productType.length < 1}" @click="getAllquery('supplier')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in datas.counterList">                 
                                        <li @click="getsupplier(item, index)">
                                            <span :class="{on: selectData.productType[index]}">{{item.counterName}}</span>
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
                    <i>+</i><span>新建入库单</span>
                </div>
            </div>
             <!-- 头部的下 -->
            <div class="header-options-bottom" v-if="this.queryList.length > 0">
                <a @click="emptyQuery" class="bottom-empty" href="javascrit:void(0)">清空查询条件</a>
                <ul>
                    <li :class="{'li-on': selectData.littleSelect[index]}" v-on:mouseover="openList(index)" v-for="(item, index) in queryList" v-on:mouseleave="outList(index)">
                        <span class="main-color">{{item.typeName}}</span>
                        <img src="./../../../assets/img/work/arrows-right-main.png" alt="">
                        <span>{{item.name || item.counterName || item.shopName}}</span>
                        <i class="trl"></i>
                        <i class="del-icon" @click="delectQuery(index, item)"></i>
                        <i :class="{'blank-color': selectData.littleSelect[index]}"></i>
                        <template v-if='item.typeName === "调出柜组"'>
                            <div v-on:mouseover="openList(index)" class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in datas.counterList">                                      
                                    <span @click="cutWarehouseData(item2, index2, index, item.typeName)" :class="{on: selectData.warehouseOut[index]}">
                                        {{item2.counterName}}
                                    </span>
                                </template>
                            </div>
                        </template>
                        <template v-if='item.typeName === "调入柜组"' v-on:mouseover="openList(index)">
                            <div  class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in datas.counterList">                                      
                                    <span @click="cutAuditsData(item2, index2, index, item.typeName)" :class="{on: selectData.warehouseOut[index]}">
                                        {{item2.counterName}}
                                    </span>
                                </template>
                            </div>
                        </template>
                        <template v-if='item.typeName === "店铺"'>
                            <div v-on:mouseover="openList(index)" class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in shopListByCo">                                      
                                    <span @click="cutShopByCo(item2, index2, index, item.typeName)" :class="{on: selectData.warehouseOut[index]}">
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
                    <div class="options-left" @click="goDetail(item.orderNum)">
                        <p class="main-color">单号：{{item.orderNum}}</p>
                        <p>制单人：{{item.makeOrderManName}}</p>
                        <p>
                            制单时间：
                            <span v-text="preciseSun(item.createDate)"></span>
                            <span class="ml10" v-text="preciseMinute(item.createDate)"></span>
                        </p>
                    </div>
                    <div class="options-btn">
                        <a href="javascript: void(0)" @click="auditFun(item.checkType, item.orderNum)">
                            {{getOptionsName(item.checkType)}}
                        </a>
                        <a v-if="item.checkType === '2'" class="del-btn" href="javascript: void(0)" @click="auditFun('4', item.orderNum)">驳回审核</a>
                        <a v-if="item.checkType === '1'" class="del-bg del-btn" href="javascript: void(0)" @click="delectReceipts(item.orderNum)">删除</a>
                    </div>
                </div>
                <div class="receipts-list-body-bottom" @click="goDetail(item.orderNum)">
                    <ul class="condition-two">
                        <li>店铺：{{item.shopName}}</li>
                        <li>调入柜组：{{item.groupName}}</li>
                        <li>调出柜组：{{item.groupName2}}</li>
                        <li>总件数：{{item.totalNum || 0}} 件</li>
                        <li>总重量：{{item.totalWeight || 0}} g</li>
                        <li>总售价：{{item.totalPrice || 0}}元</li>
                    </ul>
                    <div class="audit-state">
                        <img :src="getState(item.checkType)" alt="">
                    </div>
                </div>
            </div>
        </div>
        <!-- 弹窗 -->
        <!-- 新建 -->
        <div class="new-receipts-wrap" v-model="newPopup.main" v-if="newPopup.main">
            <new-transfer-cabinet-receipts
                :newPopup="newPopup.main"
                v-on:closePopup="closePopup"
            ></new-transfer-cabinet-receipts>
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
    </div>
</template>
<script>
import Vue from 'vue'
import {mapGetters, mapActions} from "vuex"
import {seekGetShopListByCo, seekTuneCounterList, seekShowCounterList} from './../../../Api/commonality/seek'
import {operateDelReceipt, operateCreateRKReceipt} from './../../../Api/commonality/operate'
import {receiptOptionsName} from './../../../Api/commonality/status'
import delectReceipts from './../../Work/CommonalityComponent/popupTemplate/DelectReceipts'
import auditReceipts from './../../Work/CommonalityComponent/popupTemplate/auditReceipts'
import newTransferCabinetReceipts from '../../../components/work/NewTransferCabinetReceipts' // 新建弹窗
export default {
    data () {
        return {
            "selectPopup": false, // 选择下拉框
            "selectData": { // 选择样式
                "warehouse": [], // 调出柜组
                "warehouseOut": [], // 店铺
                "productType": [], // 调入柜组
                "littleSelect": [] // 小类的下拉框
            },
            "queryList": [], // 查询列表
            "selectObject": { // 选择数据
                "warehouse": null, // 调出柜组
                "warehouseOut": null, // 店铺
                "productType": null // 调入柜组
            },
            "onData": { // 下拉选中数据
                "shopId": "", // 店铺
                "timeType": "" // 时间
            },
            "popupData": { // 过滤
                "shipmentsId": null, // 发货id
                "auditType": null // 调出库位
            },
            "onClass": { // 选中的class
                "one": false,
                "two": false,
                "three": false
            },
            "newDatas": { // 新增数据(其它页面也用到的)newDatas
                "shopId": "", // 分销商ID
                "supplierId": "", // 调入柜组ID
                "productTypeId": "", // 产品类别ID
                "repositoryId": "", // 库位ID
                "orderNumber": null // 单据号
            },
            "showList": [], // 单据列表
            "datas": {
                "shopList": [], // 店铺列表
                "counterList": [], // 柜组列表
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
                "productTypeList": [
                    {
                        "name": "成品",
                        "type": "1"
                    },
                    {
                        "name": "旧料",
                        "type": "2"
                    }
                ],
                "receiptStatusList": [
                    {
                        "name": "未收货",
                        "type": "1"
                    },
                    {
                        "name": "已收货",
                        "type": "2"
                    }
                ]
            },
            "newPopup": { // 新建单据的弹窗
                "main": false, // 新建的弹窗
                "isShow": "receiptsClass" // 默认是产品类别
            },
            "popup": { // 弹窗
                "productClass": false, // 产品类型
                "supplierAnRepository": false, // 调入柜组和库位
                "deleteReceipts": false, // 删除单据弹窗
                "receiptsOrderNum": null, // 操作的单据号
                "auditsReceiptsForm": null, // 操作1 删除 3 取消审核 4 驳回审核 5 通过审核 6 撤销审核
                "auditPopup": false // 审核弹窗
            },
            "seekNumber": "", // 搜索单据号
            "monitorScroll": 0,
            "page": 1
        }
    },
    components: {
        delectReceipts,
        auditReceipts,
        newTransferCabinetReceipts
    },
    created () {
        this.filterFun(); // 获取单据列表
        // this.workRepositoryList(); // 库位列表
        this.getShopListByCo(); // 店铺列表
        // this.workProductClass(); // 产品类别
    },
    computed: {
        ...mapGetters([
            "repositoryList", // 库位列表
            "shopListByCo", // 店铺列表
            "productClass", // 产品类别
        ])
    },
    methods: {
        ...mapActions([
            "workRepositoryList", // 库位列表
            "getShopListByCo", // 店铺列表
            "workProductClass", // 产品类别
        ]),
        preciseSun (parm) { // 年
            if (parm) {
                var Year = parm.slice(0, 4);
                var Month = parm.slice(4, 6);
                var Sun = parm.slice(6, 8);
                var allTime = Year + "-" + Month + "-" + Sun;
                return allTime;
            }
        },
        preciseMinute (parm) { // 月
            if (parm) {
                var ConfigData = parm.slice(8, 10);
                var Mour = parm.slice(10, 12);
                var allTime = ConfigData + ":" + Mour;
                return allTime;
            }
        },
        closePopup (parm) { // 关闭新建弹窗
            this.newPopup.main = parm;
        },
        getCounterList (parm) { // 柜组列表
            let options = {
                "shopId": parm // 店铺ID
            }
            seekShowCounterList(options).then((response) => {
                console.log("柜组列表");
                console.log(response);
                if (response.data.state === 200) {
                    this.datas.counterList = response.data.data.counterList;
                }
            }, (response) => {
                console.log(response)
            })
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
        delectReceiptsState (parm) { // 监听确认删除弹窗
            console.log("收到删除成功回调")
            if (parm.type === "2") { // 单据删除成功
                console.log("下一步操作")
                this.showList = [];
                this.filterFun();
            }
            this.popup.deleteReceipts = parm.name;
            this.popup.auditPopup = parm.name;
        },
        getChildCalss (parmOne, parmTwo) { // 获取产品类别名字
            this.newDatas.classesId = parmOne;
            this.newDatas.classesName = parmTwo;
        },
        goDetail (orderNum) {
            sessionStorage.setItem("orderNumber", orderNum);
            this.$router.push("/work/transferCabinet/newTransferCabinet");
        },
        affirmNew () { // 确定新建入库
            if (this.newDatas.productTypeId && this.newDatas.repositoryId) {
                var options = {
                    "productTypeId": this.newDatas.productTypeId, // 产品类别ID
                    "repositoryId": this.newDatas.repositoryId // 库位ID
                }
                sessionStorage.setItem("确定新建入库", JSON.stringify(options));
                operateCreateRKReceipt(options).then((response) => {
                    if (response.data.state === 200) {
                        sessionStorage.setItem("orderNumber", response.data.data.orderNum);
                        this.$router.push("/work/transferCabinet/newTransferCabinet");
                    } else {
                        alert(response.data.msg);
                    }
                }, (response) => {
                    console.log(response);
                })
            } else if (!this.newDatas.productTypeId) {
                alert("请选择产品类别");
            } else if (!this.newDatas.repositoryId) {
                alert("请选择库位")
            }
        },
        getConfigValue (parm) { // 配置产品类别
            let configName = null;
            switch (parm) {
                case '1':
                    configName = "素金类（计重）";
                    break;
                case '2':
                    configName = "珠宝类（计件）";
                    break;
                case '3':
                    configName = "饰品类（银饰 / 饰品）";
                    break;
            }
            return configName;
        },
        getOptionsName (parm) { // 操作名字
            return receiptOptionsName(parm);
        },
        getState (parm) { // 调出库位
            switch (parm) {
                case "1":
                    return "./../../../../static/img/audit-1.png";
                case "2":
                    return "./../../../../static/img/audit-2.png";
                case "3":
                    return "./../../../../static/img/audit-3.png"
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
            this.selectData.warehouseOut = [];
            this.selectObject.warehouseOut = [];
            this.selectData.warehouse = [];
            this.selectObject.warehouse = null;
        },
        closeSeek () { // 关闭查询
            this.seekNumber = "";
            this.filterFun();
        },
        filterFun (cbFun) { // 定位选择的过滤数据
            // var warehouse = null; // 调入库位
            // var warehouseOut = null; // 调出库位
            // var productType = null; // 调入柜组
            var warehouse = null; // 调出柜组
            var warehouseOut = null; // 店铺
            var productType = null; // 调入柜组
            if (this.selectObject.warehouse) { // 调出柜组
                warehouse = this.selectObject.warehouse.counterId;
            }
            if (this.selectObject.warehouseOut) { // 店铺
                warehouseOut = this.selectObject.warehouseOut.shopId;
            }
            if (this.selectObject.productType) { // 调入柜组
                productType = this.selectObject.productType.counterId;
            }
            console.log("varwarehouseOutnull;");
            console.log(this.selectObject);
            let options = {
                "orderBy": "2", // 1智能排序2按制单时间倒序3按制单时间升序
                "groupId": productType || "-1", // 调入柜组
                "groupId2": warehouse || "-1", // 调出柜组
                "shopId": warehouseOut || "-1", // 店铺ID
                "orderNum": this.seekNumber || "",
                "page": this.page || "1",
                "pageSize": "30"
            }
            seekTuneCounterList(options).then((response) => {
                console.log("调柜单据列表");
                console.log(response);
                if (response.data.state === 200) {
                    this.showList.push(...response.data.data.orderList);
                    if (cbFun) {
                        cbFun();
                    }
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
        cutWarehouseData (item2, index2, index, typeName) { // 切换调出柜组(小类)
            this.queryList[index].counterName = item2.counterName;
            this.queryList[index].counterId = item2.counterId;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        cutAuditsData (item2, index2, index, typeName) { // 切换调入柜组(小类)
            this.queryList[index].counterName = item2.counterName;
            this.queryList[index].counterId = item2.counterId;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        cutShopByCo (item2, index2, index, typeName) { // 切换调入柜组
            this.queryList[index].shopName = item2.shopName;
            this.queryList[index].shopId = item2.shopId;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        amendFun (item2, index2, typeName) { // 修改选择数据item2, index, typeName
            switch (typeName) {
                case "店铺":
                    this.selectData.warehouseOut = [];
                    Vue.set(this.selectData.warehouseOut, index2, true);
                    this.selectObject.warehouseOut = item2;
                    break;
                case "调出柜组":
                    this.selectData.warehouse = [];
                    Vue.set(this.selectData.warehouse, index2, true);
                    this.selectObject.warehouse = item2;
                    break;
                case "调入柜组":
                    this.selectData.productType = [];
                    Vue.set(this.selectData.productType, index2, true);
                    this.selectObject.productType = item2;
                    break;
            }
        },
        delectQuery (parm, item) { // 删除一条选中数据
            Vue.delete(this.queryList, parm);
            console.log(item.typeName)
            sessionStorage.setItem("删除一条选中数据", JSON.stringify(this.queryList));
            var shopId = null;
            var cashStatus = null;
            switch (item.typeName) {
                case "调入柜组":
                    cashStatus = "-1";
                    this.selectData.productType = [];
                    this.selectObject.productType = null;
                    break;
                case "调出柜组":
                    shopId = "-1";
                    this.selectData.warehouse = [];
                    this.selectObject.warehouse = null;
                    break;
                case "店铺":
                    this.selectData.warehouseOut = [];
                    this.selectObject.warehouseOut = null;
                    break;
            }
            if (this.selectObject.warehouse) {
                shopId = this.selectObject.warehouse.id
            }
            if (this.selectObject.productType) {
                cashStatus = this.selectObject.productType.id
            }
            console.log(shopId);
            console.log(cashStatus);
        },
        emptyQuery () { // 清空查询筛选条件
            this.queryList = [];
        },
        getSort (parm, index) { // 店铺
            this.getCounterList(parm.shopId);
            let selectObject = {
                "typeName": "店铺",
                "shopName": parm.shopName,
                "shopId": parm.shopId
            }
            this.selectObject.warehouseOut = selectObject;
            this.selectData.warehouseOut = [];
            Vue.set(this.selectData.warehouseOut, index, true)
        },
        getWarehouseId (parm, index) { // 调出柜组
            let selectObject = {
                "typeName": "调出柜组",
                "counterName": parm.counterName,
                "counterId": parm.counterId
            }
            this.selectObject.warehouse = selectObject;
            this.selectData.warehouse = [];
            Vue.set(this.selectData.warehouse, index, true)
        },
        getsupplier (parm, index) { // 调入柜组
            let selectObject = {
                "typeName": "调入柜组",
                "counterName": parm.counterName,
                "counterId": parm.counterId
            }
            this.selectObject.productType = selectObject;
            this.selectData.productType = [];
            Vue.set(this.selectData.productType, index, true)
        },
        getAllquery (parm) { // 全选
            this.selectData[parm] = [];
            this.selectObject[parm] = null;
        },
        getShopList () { // 店铺列表
            let options = "";
            let _self = this;
            seekGetShopListByCo(options).then((response) => {
                if (response.data.state === 200) {
                    _self.datas.shopList = response.data.data.shopList;
                    if (response.data.data.shopList.length === 1) { // 只有一个店铺的情况下
                        _self.onData.shopId = response.data.data.shopList[0].shopId;
                    }
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log("cuo店铺列表")
                console.log(response);
            });
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
        toSellReceiptsList (parm) { // 去到单据列表
            sessionStorage.setItem("orderNumber", parm);
            this.$router.push("/work/transferCabinet/newTransferCabinet");
        },
        collectMoney () { // 待收银
            alert("待收银")
        },
        auditFun (checkType, orderNum) { // 提交审核
            this.popup.receiptsOrderNum = orderNum;
            this.popup.auditsReceiptsForm = checkType;
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