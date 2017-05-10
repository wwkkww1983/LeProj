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
                                <h5 :class="{'all-on': selectData.warehouse.length < 1}" @click="getAllquery('warehouse')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in shopListByCo">                              
                                        <li @click="getShopId(item, index)">
                                            <span :class="{on: selectData.warehouse[index]}">{{item.shopName}}</span>
                                        </li>
                                    </template>
                                </ul>
                            </div>
                        </div>
                        <div class="one-line-data">
                            <h3>排序</h3>
                            <div class="one-line-right">
                                <h5 :class="{'all-on': selectData.sortSelect.length < 1}" @click="getAllquery('sortSelect')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in datas.times">                          
                                        <li @click="getSort(item, index)">
                                            <span :class="{on: selectData.sortSelect[index]}">{{item.name}}</span>
                                        </li>
                                    </template>
                                </ul>
                            </div>
                        </div>
                        <div class="one-line-data">
                            <h3>收银</h3>
                            <div class="one-line-right">
                                <h5 :class="{'all-on': selectData.monnyState.length < 1}" @click="getAllquery('monnyState')">全部</h5>
                                <ul>
                                    <template v-for="(item, index) in datas.monnyStateList">                          
                                        <li @click="getMonnyState(item, index)">
                                            <span :class="{on: selectData.monnyState[index]}">{{item.name}}</span>
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
                    <i>+</i><span>新建销售单</span>
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
                        <template v-if='item.typeName === "店铺"'>
                            <div v-on:mouseover="openList(index)" class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in shopListByCo">                                      
                                    <span @click="cutShopData(item2, index2, index, item.typeName)" :class="{on: selectData.sortSelect[index]}">
                                        {{item2.shopName}}
                                    </span>
                                </template>
                            </div>
                        </template>
                        <template v-if='item.typeName === "排序"'>
                            <div v-on:mouseover="openList(index)" class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in datas.times">                                      
                                    <span @click="cutSortData(item2, index2, index, item.typeName)" :class="{on: selectData.sortSelect[index]}">
                                        {{item.name}}
                                    </span>
                                </template>
                            </div>
                        </template>
                        <template v-if='item.typeName === "收银"'>
                            <div v-on:mouseover="openList(index)" class="open-list" :class="{on: selectData.littleSelect[index]}" v-if="selectData.littleSelect[index]">
                                <template v-for="(item2, index2) in datas.monnyStateList">                                      
                                    <span @click="cutMonny(item2, index2, index, item.typeName)" :class="{on: selectData.sortSelect[index]}">
                                        {{item2.name}}
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
                            {{getOptionsName(item.checkType)}}789
                        </a>
<!--                         <a v-if="item.checkType === '2'" class="del-btn" href="javascript: void(0)" @click="auditFun('4', item.orderNum)">驳回审核</a> -->
                        <a class="del-bg del-btn" href="javascript: void(0)" @click="delectReceipts(item.orderNum)">删除</a>
                    </div>
                </div>
                <div class="receipts-list-body-bottom" @click="goDetail(item.orderNum)">
                    <ul class="condition-one">
                        <li>入库库位：{{item.storageName}}</li>
                        <li>供应商：</li>
                        <li>总件数：{{item.totalNum}} 件</li>
                        <li>总重量：{{item.totalWeight}} g</li>
                        <li>总售价：{{item.totalPrice}} 元</li>
                    </ul>
<!--                     <div class="audit-state">
                        <img :src="getState(item.checkType)" alt="">
                    </div> -->
                </div>
            </div>
        </div>
        <!-- 弹窗 -->
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
import {seekGetShopListByCo, seekGoodsSellOrder} from './../../../Api/commonality/seek'
import {operateDelReceipt, operateCreateSellOrder} from './../../../Api/commonality/operate'
import {receiptOptionsName} from './../../../Api/commonality/status'
import delectReceipts from './../../Work/CommonalityComponent/popupTemplate/DelectReceipts'
import auditReceipts from './../../Work/CommonalityComponent/popupTemplate/auditReceipts'
import receiptsClass from '../../../components/work/ReceiptsClass' // 产品类别
export default {
    data () {
        return {
            "selectPopup": false, // 选择下拉框
            "selectData": { // 选择样式
                "monnyState": [], // 收银
                "sortSelect": [], // 排序
                "warehouse": [], // 店铺
                "supplier": [], // 供应商
                "littleSelect": [] // 小类的下拉框
            },
            "queryList": [], // 查询列表
            "selectObject": { // 选择数据
                "monnyState": null, // 收银
                "sortSelect": null, // 排序
                "warehouse": null, // 店铺
                "supplier": null // 供应商
            },
            "onData": { // 下拉选中数据
                "shopId": "", // 店铺
                "timeType": "" // 时间
            },
            "popupData": { // 过滤
                "shipmentsId": null, // 发货id
                "auditType": null // 审核状态
            },
            "onClass": { // 选中的class
                "one": false,
                "two": false,
                "three": false
            },
            "newDatas": { // 新增数据(其它页面也用到的)newDatas
                "shopId": "", // 分销商ID
                "supplierId": "", // 供应商ID
                "productTypeId": "", // 产品类别ID
                "repositoryId": "", // 库位ID
                "orderNumber": null // 单据号
            },
            "showList": [], // 单据列表
            "datas": {
                "shopList": [], // 店铺列表
                "times": [
                    {
                        "name": "制单时间顺序",
                        "type": "1"
                    },
                    {
                        "name": "制单时间倒叙",
                        "type": "2"
                    },
                    {
                        "name": "收银时间顺序",
                        "type": "3"
                    },
                    {
                        "name": "收银时间倒叙",
                        "type": "4"
                    }
                ],
                "monnyStateList": [
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
                "main": false, // 新建的弹窗
                "isShow": "receiptsClass" // 默认是产品类别
            },
            "popup": { // 弹窗
                "productClass": false, // 产品类型
                "supplierAnRepository": false, // 供应商和库位
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
        receiptsClass
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
        seekFun () { // 通过单据号查询
            this.showList = [];
            this.filterFun();
        },
        onScroll (e) { // 滚动加载
            var scrollHeight = e.currentTarget.scrollHeight; // 元素可以滚动的高度
            var clientHeight = e.currentTarget.clientHeight; // 元素的高度
            var scrollTop = e.currentTarget.scrollTop; // 滚动了的距离
            if (clientHeight + scrollTop >= scrollHeight) {
                this.filterFun();
            }
        },
        newReceipt (parm) { // 新建单据
            // this.newPopup.main = true;
            this.affirmNew();
            // this.newPopup.main = true;
            // this.$router.push("/work/sell/sellReceiptsList");
        },
        delectReceiptsState (parm) { // 监听确认删除弹窗
            if (parm.type === "2") { // 单据删除成功
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
            this.$router.push("/work/sell/sellReceiptsList");
        },
        affirmNew () { // 确定新建销售
            var options = {
                "shopId": this.shopListByCo[0].shopId
            }
            sessionStorage.setItem("确定新建入库", JSON.stringify(options));
            operateCreateSellOrder(options).then((response) => {
                console.log("新建销售");
                console.log(this.shopListByCo);
                console.log(response);
                if (response.data.state === 200) {
                    sessionStorage.setItem("orderNumber", response.data.data.orderNum);
                    this.$router.push("/work/sell/sellReceiptsList");
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
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
        getState (parm) { // 审核状态
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
            this.selectData.sortSelect = []; // 排序
            this.selectObject.auditState = [];
            this.selectData.warehouse = []; // 店铺
            // this.selectObject.warehouse = null;
        },
        closeSeek () { // 关闭查询
            this.seekNumber = "";
            this.filterFun();
        },
        filterFun () { // 定位选择的过滤数据
            var warehouse = null; // 店铺
            var sortSelect = null; // 排序
            var monnyState = null; // 收银
            if (this.selectObject.warehouse) { // 店铺
                warehouse = this.selectObject.warehouse.id;
            }
            if (this.selectObject.sortSelect) { // 排序
                sortSelect = this.selectObject.sortSelect.type;
            }
            if (this.selectObject.monnyState) { // 供应商
                monnyState = this.selectObject.monnyState.id;
            }
            let options = {
                "shopId": warehouse || "-1", // 店铺ID；若为-1，则为全部店铺
                "cashStatus": monnyState || "-1", // 收银状态：-1全部1 已收银2 未收银
                "orderBy": sortSelect || "2",
                "page": "1",
                "pageSize": "30"
            }
            seekGoodsSellOrder(options).then((response) => {
                console.log("定位选择的过滤数据");
                console.log(response);
                if (response.data.state === 200) {
                    this.showList.push(...response.data.data.orderList);
                    if (response.data.data.orderList.length > 0) {
                        this.page += 1;
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
        cutShopData (item2, index2, index, typeName) { // 切换店铺(小类)
            console.log("切换店铺(小类)");
            console.log(item2);
            this.queryList[index].name = item2.shopName;
            this.queryList[index].id = item2.shopId;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        cutMonny (item2, index2, index, typeName) { // 切换收银状态
            this.queryList[index].name = item2.name;
            this.queryList[index].id = item2.type;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        cutSortData (item2, index2, index, typeName) { // 切换排序(小类)
            this.queryList[index].name = item2.name;
            this.queryList[index].id = item2.type;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        cutShopByCo (item2, index2, index, typeName) { // 切换供应商
            this.queryList[index].name = item2.shopName;
            this.queryList[index].id = item2.shopId;
            this.amendFun(item2, index2, typeName);
            this.filterFun();
            this.outList();
        },
        amendFun (item2, index2, typeName) { // 修改选择数据item2, index, typeName
            switch (typeName) {
                case "排序":
                    this.selectData.sortSelect = [];
                    Vue.set(this.selectData.sortSelect, index2, true);
                    this.selectObject.sortSelect = item2;
                    break;
                case "入库库位":
                    this.selectData.warehouse = [];
                    Vue.set(this.selectData.warehouse, index2, true);
                    this.selectObject.warehouse = item2;
                    break;
                case "供应商":
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
                case "供应商":
                    cashStatus = "-1";
                    this.selectData.supplier = [];
                    this.selectObject.supplier = null;
                    break;
                case "入库库位":
                    shopId = "-1";
                    this.selectData.warehouse = [];
                    this.selectObject.warehouse = null;
                    break;
                case "排序":
                    this.selectData.sortSelect = [];
                    this.selectObject.sortSelect = null;
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
        getShopId (parm, index) { // 选择店铺
            let selectObject = {
                "typeName": "店铺",
                "name": parm.shopName,
                "id": parm.shopId
            }
            this.selectObject.warehouse = selectObject;
            this.selectData.warehouse = [];
            Vue.set(this.selectData.warehouse, index, true)
        },
        // getWarehouseId (parm, index) { // 选择入库库位
        //     let selectObject = {
        //         "typeName": "入库库位",
        //         "name": parm.repositoryName,
        //         "repositoryId": parm.repositoryId
        //     }
        //     this.selectObject.warehouse = selectObject;
        //     this.selectData.warehouse = [];
        //     Vue.set(this.selectData.warehouse, index, true)
        // },
        getAllquery (parm) { // 全选
            this.selectData[parm] = [];
            this.selectObject[parm] = null;
        },
        getSort (parm, index) { // 排序
            let selectObject = {
                "typeName": "排序",
                "name": parm.name,
                "id": parm.type
            }
            this.selectObject.sortSelect = selectObject;
            this.selectData.sortSelect = [];
            Vue.set(this.selectData.sortSelect, index, true)
        },
        getMonnyState (parm, index) { // 收银状态
            let selectObject = {
                "typeName": "收银",
                "name": parm.name,
                "id": parm.type
            }
            this.selectObject.monnyState = selectObject;
            this.selectData.monnyState = [];
            Vue.set(this.selectData.monnyState, index, true)
        },
        getsupplier (parm, index) { // 供应商
            let selectObject = {
                "typeName": "供应商",
                "name": parm.supplierName,
                "id": parm.supplierId
            }
            this.selectObject.supplier = selectObject;
            this.selectData.supplier = [];
            Vue.set(this.selectData.supplier, index, true)
        },
        getShopList () { // 店铺列表
            let options = "";
            let _self = this;
            seekGetShopListByCo(options).then((response) => {
                console.log("店铺列表")
                console.log(response)
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
            this.$router.push("/work/sell/sellReceiptsList");
        },
        collectMoney () { // 待收银
            alert("待收银")
        },
        auditFun (checkType, orderNum) { // 提交审核
            console.log("kkkkkkkkkkkkkkkkkkkkkk");
            console.log(checkType);
            console.log(orderNum);
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
<style lang="scss" scoped>
.receipts-main{
    // width: 100%;
    width: 1570px;
    margin: 0 auto;
    height: 100%;
    padding-bottom: 120px;
    overflow: hidden;
    .header-options{
        position: relative;
        margin-top: 20px;
        width: 1570px;
        margin-bottom: 40px;
        z-index: 1000;
        .header-options-top{
            position: relative;
            height: 60px;
            font-size: 0;
            background-color: #f6f7f8;
            .header-seslect{
                position: relative;
                display: inline-block;
                width: 132px;
                height: 60px;
                line-height: 60px;
                color: #666;
                text-align: center;
                cursor: pointer;
                font-weight: 600;
                &:hover{
                    color: #fff;
                    background-color: #4fdcca;
                    i{
                        border-top: 0px solid #fff;
                        border-bottom: 6px solid #fff;
                    }
                }
                span{
                    font-size: 16px;
                    position: absolute;
                    top: 0;
                    bottom: 0;
                    line-height: 60px;
                    right: 0;
                    left: 0;
                    margin: auto;
                    line-height: 55px;
                }
                i{
                    position: absolute;
                    height: 0;
                    width: 0;
                    line-height: 0;
                    font-size: 0;
                    border: 6px dashed transparent;
                    color: #666;
                    top: 28px;
                    right: 16px;
                    border-top: 6px solid #666;
                    border-width: 6px;
                }
            }
            .seek-wrap{ // 搜索框
                display: inline-block;
                vertical-align: top;
                margin-left: 50px;
                height: 36px;
                margin-top: 12px;
                width: 400px;
                border: 2px solid #4fdcca;
                border-radius: 4px;
                background-color: #fff;
                input{
                    height: 32px;
                    font-size: 14px;
                    color: #999;
                    width: 300px;
                    padding-left: 20px;
                }
                i{
                    width: 20px;
                    height: 20px;
                    margin-top: 6px;
                    float: right;
                    margin-right: 20px;
                    cursor: pointer;
                    border-radius: 50%;
                    background: url("./../../../assets/img/work/del-gray.png");
                }
                div{
                    width: 50px;
                    height: 32px;
                    float: right;
                    background-color: #4fdcca;
                    cursor: pointer;
                    img{
                        width: 24px;
                        height: 24px;
                        margin-top: 4px;
                        margin-left: 13px;
                    }
                }
            }
            .header-seslect-on{ // 选中
                color: #fff;
                background-color: #4fdcca;
                i{
                    border-top: 0px solid #fff;
                    border-bottom: 6px solid #fff;
                }
            }
            .header-seslect-body{ // 搜索框
                position: absolute;
                top: 60px;
                margin-top: 0;
                height: 420px;
                width: 750px;
                background-color: #fff;
                box-shadow: 0 0 20px #ccc;
                padding-bottom: 20px;
                z-index: 1000;
                .header-select-wrap{
                    height: 350px;
                    overflow: auto;      
                    .one-line-data{
                        overflow: auto;
                        font-size: 0;
                        h3{
                            margin-top: 15px;
                            vertical-align: top;
                            display: inline-block;
                            font-size: 16px;
                            font-weight: 600; 
                            padding-left: 40px;
                            color: #666;
                            width: 160px;
                        }
                        .one-line-right{
                            display: inline-block;
                            width: 560px;
                            border-bottom: 1px solid #d6d6d6;
                            h5{
                                display: inline-block;
                                vertical-align: top;
                                height: 30px;
                                line-height: 30px;
                                padding: 0;
                                width: 50px;
                                border-radius: 4px;
                                text-align: center;
                                font-size: 16px;
                                font-weight: 600;
                                margin-right: 30px;
                                margin-top: 15px;
                                cursor: pointer;
                            }
                            .all-on{
                                color: #fff;
                                background-color: #4fdcca;
                            }            
                            ul{
                                display: inline-block;
                                width: 480px;
                                li{
                                    display: inline-block;
                                    height: 60px;
                                    padding: 0 25px;
                                    color: #666;
                                    span{
                                        display: inline-block;
                                        height: 30px;
                                        margin-top: 15px;
                                        padding: 0 10px; 
                                        line-height: 30px;
                                        font-size: 16px;
                                        border-radius: 4px;
                                        color: #666;
                                    }
                                    .on{ // 选中的状态
                                        background-color: #4fdcca;
                                        color: #fff;
                                    }
                                    &:hover{
                                        span{
                                            color: #fff;
                                            background-color: #4fdcca;
                                            cursor: pointer;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                .header-seslect-bottom{
                    height: 70px;
                    background-color: #f6f7f8;
                    li{
                        height: 40px;
                        line-height: 40px;
                        width: 90px;
                        text-align: center;
                        border-radius: 4px;
                        font-size: 16px;
                        font-weight: 600;
                        margin-top: 15px;
                        cursor: pointer;
                    }
                    li:nth-child(1){
                        margin-left: 40px;
                        float: left;
                        color: #999;
                        border: 1px solid #d6d6d6;
                        background-color: #fff;
                    }

                    li:nth-child(2){
                        float: right;
                        color: #fff;
                        margin-right: 40px;
                        background-color: #4fdcca;
                        &:hover{
                            background-color: #44beae;
                        }
                    }
                }
            }
        }
        .header-options-bottom{
            position: relative;
            height: 60px;
            background-color: #fff;
            ul{
                height: 60px;
                font-size: 0;
                margin-left: 20px;
                li{
                    position: relative;
                    display: inline-block;
                    border: 1px solid #d6d6d6;
                    height: 36px;
                    margin-top: 12px;
                    margin-right: 40px;
                    padding: 0 38px 0 10px;
                    vertical-align: top;
                    border-radius: 4px;
                    cursor: pointer;
                    span{
                        display: inline-block;
                        vertical-align: top;
                        font-size: 16px;
                        line-height: 36px;
                        color: #999;
                    }
                    img{
                        vertical-align: top;
                        display: inline-block;
                        width: 6px;
                        height: 6px;
                        margin: 15px 6px;
                    }
                    .trl{
                        position: absolute;
                        height: 0;
                        width: 0;
                        line-height: 0;
                        font-size: 0;
                        border: 6px dashed transparent;
                        color: #666;
                        top: 15px;
                        right: 20px;
                        border-top: 6px solid #666;
                        border-width: 6px;
                    }
                    .del-icon{
                        position: absolute;
                        right: -10px;
                        top: 8px;
                        width: 20px;
                        height: 20px;
                        border-radius: 50%;
                        background: #fff url('./../../../assets/img/work/del.png') no-repeat;
                    }
                    .blank-color{
                        position: absolute;
                        bottom: -2px;
                        z-index: 1002;
                        left: 0;
                        height: 3px;
                        width: 100%;
                        background-color: #fff;
                    }
                    .open-list{
                        position: absolute;
                        top: 34px;
                        left: -1px;
                        border: 1px solid #d6d6d6;
                        width: 300px;
                        background-color: #fff;
                        padding: 20px;
                        z-index: 1000;
                        border-radius: 4px;
                        span{
                            padding: 0 10px;
                            background-color: #46c4b4;
                            margin: 0 10px 10px 0;
                            border-radius: 4px;
                            color: #fff;
                            cursor: pointer;
                        }
                    }
                    .on{
                        border-top-left-radius: 0;
                    }
                }
                .li-on{
                    border-bottom-left-radius: 0;
                    border-bottom-right-radius: 0;
                }
            }
            .bottom-empty{ // 清空样式
                position: absolute;
                display: inline-block;
                top: 23px;
                right: 40px;
                font-size: 14px;
                height: 14px;
                line-height: 14px;
                color: #666;
            }
        }
    }
    .receipts-list{ // 单据列表
        height: 100%;
        // padding-bottom: 20px;
        overflow-x: hidden;
        overflow-y: auto;
        .receipts-list-body{
            // height: 200px;
            // border: 5px solid #000;
            background-color: #fff;
            margin-bottom: 20px;
            .receipts-list-body-top{
                font-size: 0;
                height: 60px;
                padding-left: 20px;
                background-color: #f6f7f8;
                .options-left{
                    display: inline-block;
                    font-size: 0;
                    p{
                        display: inline-block;
                        height: 60px;
                        font-size: 18px;
                        width: 300px;
                        line-height: 60px;
                        font-size: 18px;
                        color: #999;
                        font-weight: 600;
                    }
                }
                .options-btn{
                    display: inline-block;
                    height: 40px;
                    margin-top: 10px;
                    float: right;
                    margin-right: 40px;
                    a{
                        display: inline-block;
                        height: 40px;
                        line-height: 40px;
                        width: 90px;
                        text-align: center;
                        font-size: 16px;
                        border-radius: 4px;
                        font-weight: 600;
                    }
                    a:nth-child(1){
                        background-color: #83bbf7;
                        color: #fff;
                    }
                    .del-btn{
                        border: 1px solid #d6d6d6;
                        margin-left: 20px;
                        color: #999;
                        background-color: #fff;
                    }
                    .del-bg{
                        background-color: #fff;
                    }
                }
            }
            .receipts-list-body-bottom{
                position: relative;
                font-size: 0;
                padding-bottom: 32px;
                border: 1px solid #fff;
                .condition-one{
                    width: 900px;
                    margin-top: 32px;
                    margin-left: 60px;
                    // margin-bottom: 32px;
                    li{
                        display: inline-block;
                        width: 300px;
                        font-size: 16px;
                        line-height: 16px;
                        color: #333;
                    }
                    li:nth-child(2){
                        margin-right: 30px;
                    }
                    li:nth-child(1), li:nth-child(2){
                        margin-bottom: 25px;
                    }
                }
                .audit-state{
                    position: absolute;
                    top: 0;
                    bottom: 0;
                    right: 40px;
                    margin: auto;
                    width: 81px;
                    height: 24px;
                    color: #83bbf7;
                    img{
                        width: 81px;
                        height: 24px;
                    }
                }
            }
        }
    }
    .sell-new-data{ // 新增
        position: absolute;
        background-color: #46c4b4;
        top: 0;
        right: 40px;
        color: #fff;
        width: 130px;
        height: 40px;
        border-radius: 4px;
        margin-top: 10px;
        text-align: center;
        font-weight: 600;
        cursor: pointer;
        i{
            vertical-align: top;
            height: 40px;
            line-height: 40px;
            font-style:normal;
            margin-right: 6px;
            margin-top: 2px;
            font-size: 25px;
        }
        span{
            display: inline-block;
            vertical-align: top;
            height: 40px;
            line-height: 40px;
            font-size: 16px;
        }
        &:hover{
            background-color: #44beae;
        }
    }
    .new-product-class{ // 新建产品类别弹窗
        padding: 0 30px;
        width: 600px;
        height: 400px;
        overflow: auto;
        ul{
            font-size: 0;
            margin-bottom: 10px;
            border-bottom: 1px solid #d6d6d6;
            margin-top: 10px;
            li:first-child{
                width: 100%;
                height: 16px;
                line-height: 16px;
                font-size: 14px;
                display: block;
                color: #46c4b4;
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
}
</style>