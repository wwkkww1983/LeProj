<template>
    <div class="main-wrap">
        <section class="body-box">
                            <div class="body-table table-template">     
<el-checkbox-group v-model="crudData.checkList"> 
                <table cellpadding="0" cellspacing="0">
                    <tr class="tr-header">
                        <th rowspan="2" v-if="!crudData.operationCut">
                            <label class="el-checkbox checkbox-font">
                                <span class="el-checkbox__input is-focus mr0" v-bind:class="{'is-checked': crudData.checked}">
                                <span class="el-checkbox__inner mr0"></span>
                                <input type="checkbox" class="el-checkbox__original" v-model="crudData.checked" @click="cutSelect(crudData.checked)"/>
                                </span>
                            </label>
                        </th>
                        <th rowspan="2">序号</th>
                        <th rowspan="2">条码号</th>
                        <th rowspan="2">当前状态</th>
                        <th rowspan="2">修改状态</th>
                        <th v-for="item in littleClass" :colspan="item.colspan">
                            <span>{{item.name}}</span>
                            <i v-if="item.name !== '首饰名称'" class="open-icon" v-bind:class="{ 'el-icon-arrow-right': !item.stateOpen, 'el-icon-arrow-left': item.stateOpen }" @click="cutIncident(item)"></i>
                        </th>
                        <th rowspan="2" class="border-r-no">商品备注</th>
                    </tr>
                    <tr class="tr-header">
                        <template v-for="(item, index) in littleClass">
                            <template v-for="names in getStateOpen(item, item.stateOpen)">
                                <th>{{names.name}}</th>
                            </template>
                            <!-- 隐藏时显示 -->
                            <template v-if='item.stateOpen === ""'>
                                <th :rowspan="showDatas.length * 2 + 1">
                                    <ul class="header-state">
                                        <li v-for="n in showDatas.length * 2 + 1"></li>
                                    </ul>
                                </th>
                            </template>
                        </template>
                    </tr>
                    <template v-if="showDatas" v-for="(AllData, index) in showDatas">
                        <!-- 原始数据 -->
                        <tr :class="{'table-pitch-on': operate.selectedRow[index], 'table-mousemove-style': operate.mousemoveStyle[index]}" v-on:mousemove="setMousemove(AllData,index,$event)" v-on:mouseout="setMouseout">
                            <!-- 选择按钮 -->
                            <td rowspan="2" v-if="!crudData.operationCut"><el-checkbox :label='index+((crudData.currentPage - 1) * 30)' class="checkbox-font"></el-checkbox></td>
                            <td rowspan="2">{{index+1+((crudData.currentPage - 1) * 30)}}</td>
                            <td rowspan="2">
                                <input class="form-input" v-model="AllData.hostList.barcode" @keyup.enter="queryBarCode(AllData.hostList.barcode)">
                            </td>
                            <td rowspan="2">
                                状态
                            </td>
                            <td>原始</td>
                            <template v-for="hostKey in getLittleClassKey(Object.keys(AllData.hostList))">
                                <td>{{AllData.hostList[hostKey]}}</td>
                            </template>
                        </tr>
                        <!-- 修改数据 -->
                        <tr :class="{'table-pitch-on': operate.selectedRow[index], 'table-mousemove-style': operate.mousemoveStyle[index]}" @click="pitchOn(AllData.newList,index,$event)" v-on:mousemove="setMousemove(AllData,index,$event)" v-on:mouseout="setMouseout">
                            <td>修改</td>
                            <template v-for="littleClassKey in getLittleClassKey(Object.keys(AllData.newList))">
                                <!-- 只读（包含生成和状态类的） -->
                                <template v-if="getReadOnly(littleClassKey)">
                                    <!-- 后面的汇总金料额，证书费，主石额，副石额，工费额，配件额、其他费用额 -->
                                    <template v-if="littleClassKey === 'costPrice'">
                                          <td>{{AllData.newList.goldE}}</td>
                                          <td>{{AllData.newList.certifiFee}}</td>
                                          <td>{{AllData.newList.mainPrice}}</td>
                                          <td>{{AllData.newList.deputyPrice}}</td>
                                          <td>{{AllData.newList.inMoney}}</td>
                                          <td>{{AllData.newList.price}}</td>
                                          <td>{{AllData.newList.otherFee}}</td>
                                    </template>
                                    <td>
                                        <p v-text="getComputed(littleClassKey, AllData.newList, index)"></p>
                                    </td>
                                </template>
                                <template v-else>
                                    <td class="table-pull select-w80"><!-- 选中时才给table-pull后面写 -->
                                        <p v-if="!operate.selectedRow[index]">{{AllData.newList[littleClassKey]}}</p>
                                        <template v-else>
                                            <!-- 输入(这个容器划分输入框的限制内容) -->
                                            <template v-if="!noPullDown([littleClassKey])">
                                                <!-- 售价，倍率的联动 -->
                                                <template v-if="getSpecialComputed(littleClassKey)">
                                                    <!-- 倍率 -->
                                                    <input-ratio
                                                        v-if="littleClassKey === 'ratio'"
                                                        v-model.number="showDatas[index].newList[littleClassKey]"
                                                        :ratio=showDatas[index].newList.ratio
                                                        :costPrice=showDatas[index].newList.costPrice
                                                        :soldPrice = showDatas[index].newList.soldPrice
                                                    >
                                                    </input-ratio>
                                                    <!-- 售价 -->
                                                    <input-sold-price
                                                        v-else
                                                        v-model.number="AllData.newList[littleClassKey]"
                                                        :ratio=showDatas[index].newList.ratio
                                                        :costPrice=showDatas[index].newList.costPrice
                                                        :soldPrice = showDatas[index].newList.soldPrice
                                                    >   
                                                    </input-sold-price>
                                                </template>
                                                <!-- 特定输入 -->
                                                <template v-else>
                                                    <!-- 纯数字（当前值，纯数字，最大输入值） -->
                                                    <pure-numbers
                                                        :value = "AllData.newList[littleClassKey]"
                                                        :pureNumber = "limitInput[littleClassKey].pureNumber"
                                                        :maxLengthSix = "limitInput[littleClassKey].maxlength"
                                                        v-model.number="AllData.newList[littleClassKey]"
                                                    ></pure-numbers>
                                                    <!-- 整数（当前值，整数，最大输入值） -->
                                                    <integer
                                                        :value = "AllData.newList[littleClassKey]"
                                                        :integer = "limitInput[littleClassKey].integer"
                                                        :maxLengthSix = "limitInput[littleClassKey].maxlength"
                                                        v-model.number="AllData.newList[littleClassKey]"
                                                    ></integer>
                                                    <!-- 小数（当前值，小数，最大输入值） -->
                                                    <slice-number
                                                        :value = "AllData.newList[littleClassKey]"
                                                        :sliceNumber = "limitInput[littleClassKey].sliceNumber"
                                                        :maxLengthSix = "limitInput[littleClassKey].maxlength"
                                                        v-model.number="AllData.newList[littleClassKey]"
                                                    ></slice-number>
                                                    <!-- 只限制长度（当前值，最大输入值） -->
                                                    <input v-if="limitInput[littleClassKey].onlyMaxlength" class="form-input" :maxlength="limitInput[littleClassKey].maxlength" v-model="AllData.newList[littleClassKey]"></input>
                                                </template>
                                            </template>
                                            <!-- 下拉 -->
                                            <template v-else>
                                                <el-select filterable v-model="AllData.newList[littleClassKey]" placeholder="">
                                                    <el-option v-for="item in configPullDownData[littleClassKey]"
                                                     :label="item.name || item.classesName || item.certificateName || item.name"
                                                     :value="item.classesName || item.certificateName || item.name"
                                                     ></el-option>
                                                </el-select>
                                            </template>
                                          </template>
                                    </td>
                                    <!-- 证书 -->
                                    <template v-if="littleClassKey === 'certifiName'">
                                        <template v-if="getCertificate(AllData.newList.certifiName)" v-for="item in getCertificate(AllData.newList.certifiName)">
                                            <td>{{item.organizationName}}</td>
                                            <td>{{item.website}}</td>
                                            <td>{{item.phone}}</td>
                                            <td>{{item.stand1}}</td>
                                            <td>{{item.stand2}}</td>
                                            <td>{{item.stand3}}</td>
                                        </template>
                                        <template v-if="!getCertificate(AllData.newList.certifiName)">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </template>
                                    </template>
                                </template>
                            </template>
                        </tr>
                    </template>
                </table>
</el-checkbox-group>
                </div>
                <div class="body-page">
                    <div class="paga-30">
                        <el-pagination
                          @size-change="handleSizeChange"
                          @current-change="handleCurrentChange"
                          :current-page="crudData.currentPage"
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
                <ul v-if="crudData.operationCut" class="footer-operation">
                    <li class="confirm-btn-small" @click="appendRow">新增</li>
                    <li class="confirm-btn-small" @click="batchAddFun">批量新增</li>
                    <li class="confirm-btn-small" @click="crudData.operationCut = false">删除</li>
                    <li class="confirm-btn-small" @click="addSingleAll" v-loading.fullscreen.lock="popup.fullscreenLoading" element-loading-text="拼命进行中...">保存</li>
                    <li class="confirm-btn-small red" @click="close">关闭</li>
                </ul>
            </div>
        </footer>
        <!-- 弹窗 -->
        <batch-add-index v-if="popup.batchAdd"></batch-add-index>
    <!--     <affirm></affirm> -->
        <error></error>
    </div>
</template>
<script>
// import * as types from './mutation-types.js'
import Vue from 'vue'
import {mapActions, mapGetters} from 'vuex'
import { seekProductClassList, seekProductPropertyList, seekCertificateList, getUpdateGoodsList } from './../../../../Api/commonality/seek'
import {updateGoods, deleteUpdateGoods} from './../../../../Api/commonality/operate' // 操作
import {productOrId} from './../../../../Api/commonality/extractValue' // 提取值
import {readOnly} from './../../../../Api/commonality/filter' // 过滤器
import {computedStorageData} from "./../../../../Api/commonality/computed" // 计算器

import pureNumbers from "./../../CommonalityComponent/input/pureNumbers" // 纯数字
import integer from "./../../CommonalityComponent/input/integer" // 整数
import sliceNumber from "./../../CommonalityComponent/input/sliceNumber" // 小数
import inputComputed from "./../../Storage/inputComputed"
import inputRatio from "./../../CommonalityComponent/input/computed/inputRatio" // 倍率计算
import inputSoldPrice from "./../../CommonalityComponent/input/computed/inputSoldPrice" // 售价计算

// 弹窗 ///////////////////////////////////////
import batchAddIndex from "./../../CommonalityComponent/batchAdd/index" // 批量添加
import error from "./../../CommonalityComponent/popupTemplate/error"
import affirm from "./../../CommonalityComponent/popupTemplate/affirm"
export default {
    components: {
        inputComputed,
        pureNumbers,
        integer,
        sliceNumber,
        inputRatio,
        inputSoldPrice,
        affirm,
        error,
        batchAddIndex
    },
    props: [
        'orderRemark',
        'goodsLists'
    ],
    data () {
        return {
            popup: {
                fullscreenLoading: false, // 操作的过程弹窗(保存)
                batchAdd: false // 批量添加弹窗
            },
            crudData: {
                currentPage: 1, // 当前页
                checked: false, // 头部选择圈圈
                checkList: [], // 选中的数据集合
                operationCut: true // 操作按钮切换
            },
            operate: { // 操作的状态
                selectedRow: [], // 记录当前点中，待后期优化
                mousemoveStyle: [], // 鼠标移过表格时的样式
                openState: true // 伸缩列表总开关按钮
            },
            newDatas: JSON.parse(sessionStorage.getItem("newDatas")), // 上一步带过来的数据
            datas: { // 数据源
                "hostList": [], // 历史数据
                "newList": [], // 修改数据
                "newBlandDatas": [] // 新增空白数据
            },
            aloneAmendingData: { // 独立修改(修改保存后的数据)
                "initialValue": [], // 初始值
                "changeValue": [] // 变化值
            },
            // vuex管理
            littleClass: [], // 小类
            configPullDownData: null // 配置的下拉数据
        }
    },
    computed: {
        ...mapGetters([
            "rowConfigData", // 一行数据
            "littleClassList", // 小类
            "limitInput", // 输入限制
            "rowCeshiData", // 一行测试数据（待删）
            "configPullDownDataList" // 配置的下拉数据
        ]),
        paginationSize: function () { // 分页的大小
            return this.showDataList.length;
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
        showDataList: function () { // 总数据
            let dataList = [];
            for (let i = 0; i < this.datas.hostList.length; i++) { // 添加历史数据
                let dataObject = {
                    hostList: null, // 历史
                    newList: null // 修改
                }
                dataObject.hostList = this.datas.hostList[i];
                dataList.push(dataObject);
                dataObject = null;
            }
            for (let j = 0; j < this.datas.newList.length; j++) { // 添加可修改数据
                dataList[j].newList = this.datas.newList[j];
            }
            dataList.push(...this.datas.newBlandDatas); // 添加空白数据
            return dataList;
        },
        showDatas: function () { // 最终显示的数据
            let state = ((this.crudData.currentPage - 1) * 30);
            let end = (this.crudData.currentPage * 30);
            return this.showDataList.slice(state, end);
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
        },
        productDetailList: function () { // 代替监听父组件传过来的商品列表
            if (this.goodsLists) {
                return this.goodsLists;
            }
            return null
        }
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
        'productDetailList': function () { // 监听父组件传过来的商品列表
            if (this.goodsLists) {
                this.datas.hostList = this.goodsLists.nowDataList;
                this.datas.newList = this.goodsLists.rowDataList;
            }
        }
    },
    created () { // 获取下拉数据
        if (!this.newDatas.orderNumber) { // 如果没有单据号，就是全新添加
            this.appendRow(); // 进来默认新增一条
        }
        this.getConfigInto(); // 初始化一些配置数据
        this.productClassList(); // 获取商品大小类的下拉列表
        this.productPropertyList(); // 商品属性下拉列表
        this.productCertificateList(); // 证书下拉列表
    },
    methods: {
        ...mapActions([
            "workProductDetail", // 商品列表
            "workPopupSave" // 保存（制单）成功弹窗
        ]),
        handleSizeChange (val) {
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange (val) {
            this.crudData.currentPage = val;
            console.log(`当前页: ${val}`);
        },
        cutSelect (parm) { // 切换全选
            console.log("999")
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
        setMousemove (parm, index, e) { // 鼠表移过事件（添加背景色）
            this.operate.mousemoveStyle = [];
            Vue.set(this.operate.mousemoveStyle, index, true);
        },
        setMouseout () { // 鼠表移开
            this.operate.mousemoveStyle = [];
        },
        getConfigInto () { // 初始化一些配置数据
            if (this.littleClassList) { // 初始化小类
                this.littleClass = this.littleClassList;
            }
            if (this.configPullDownDataList) { // 配置下拉数据
                this.configPullDownData = this.configPullDownDataList;
            }
        },
        // ///////////   配置数据 ///////////////
        configBlankData () { // 配置空白数据
            let dataObject = {
                hostList: this.rowConfigData,
                newList: this.rowConfigData
            }
            return dataObject;
        },
        getComputed (littleClassKey, AllData, index) { // 得到计算值
            var Result = computedStorageData(littleClassKey, index, this.showDataList[index].newList);
            AllData[littleClassKey] = Result;
            return Result;
        },
        // getShowData (parm) { // 过滤不显示key
        //     let noShowKey = new Set(["barcode", "status"]); // 条形码，当前状态
        //     let getData = [parm].filter(x => !noShowKey.has(x));
        //     if (getData.length > 0) {
        //         return true
        //     } else {
        //         return false
        //     }
        // },
        // ///////////   查询数据   /////////////
        // ///////////////(获取下拉数据)////////////////////
        productClassList () { // 获取商品大小类的下拉列表
            var _self = this;
            var requestNumber = 1;
            function cbFunction () {
                seekProductClassList(requestNumber).then((response) => {
                    if (response.data.state === 200) {
                        var pullDowndata = [];
                        for (let i = 0; i < response.data.data.list.length; i++) {
                            for (let j = 0; j < response.data.data.list[i].childrenList.length; j++) {
                                pullDowndata.push(response.data.data.list[i].childrenList[j])
                            }
                        }
                        switch (requestNumber) {
                            case 1: // 成色名称
                                _self.configPullDownData.metalColor = pullDowndata;
                                break;
                            case 2: // 宝石名称
                                _self.configPullDownData.gemName = pullDowndata; // 宝石名称
                                _self.configPullDownData.mainName = pullDowndata; // 主石
                                _self.configPullDownData.deputyName = pullDowndata; // 副石
                                break;
                            case 3: // 首饰类别
                                _self.configPullDownData.jewelryName = pullDowndata;
                                break;
                            case 4: // 商品属性
                                for (let i = 0; i < response.data.data.list.length; i++) {
                                    switch (response.data.data.list[i].classesName) { // 少一个规格没弄
                                        case "规格": // 主副石
                                          _self.configPullDownData.stand = response.data.data.list[i].childrenList;
                                          _self.configPullDownData.deputyStand = response.data.data.list[i].childrenList;
                                          break;
                                        case "形状":
                                          _self.configPullDownData.shape = response.data.data.list[i].childrenList;
                                          break;
                                        case "颜色":
                                          _self.configPullDownData.color = response.data.data.list[i].childrenList;
                                          break;
                                        case "净度":
                                          _self.configPullDownData.neatNess = response.data.data.list[i].childrenList;
                                          break;
                                        case "切工":
                                          _self.configPullDownData.blackout = response.data.data.list[i].childrenList;
                                          break;
                                        case "抛光":
                                          _self.configPullDownData.polishing = response.data.data.list[i].childrenList;
                                          break;
                                        case "对称":
                                          _self.configPullDownData.symmetry = response.data.data.list[i].childrenList;
                                          break;
                                        case "荧光":
                                          _self.configPullDownData.fluorescent = response.data.data.list[i].childrenList;
                                          break;
                                    }
                              }
                              console.log("测试商品属性");
                              console.log(_self.configPullDownData);
                              console.log("测试商品属性");
                              break;
                        }
                        requestNumber += 1;
                        if (requestNumber < 5) {
                            cbFunction();
                        }
                    } else {
                        alert(response.data.msg + "获取商品大小类的下拉列表")
                    }
                }, (response) => {
                    console.log(response)
                })
            }
            cbFunction();
        },
        productPropertyList () { // 获取商品属性下拉列表
            var _self = this;
            var requestNumber = 1;
            function cbFunction () {
                seekProductPropertyList(requestNumber).then((response) => {
                    if (response.data.state === 200) {
                        var pullDowndata = [];
                        for (let i = 0; i < response.data.data.list.length; i++) {
                            pullDowndata.push(response.data.data.list[i])
                        }
                        switch (requestNumber) {
                            case 1: // 金含量
                                _self.configPullDownData.partGold = pullDowndata;
                                break;
                            case 2: // 品牌
                                _self.configPullDownData.brand = pullDowndata;
                                break;
                            case 3: // 金属颜色
                                _self.configPullDownData.goldColor = pullDowndata;
                                break;
                            case 4: // 配件名称
                                _self.configPullDownData.partName = pullDowndata;
                                break;
                        }
                        requestNumber += 1;
                        if (requestNumber < 5) {
                            cbFunction();
                        }
                    } else {
                        alert(response.data.msg + "商品属性");
                    }
                }, (response) => {
                    console.log(response + "获取商品属性下拉列表");
                })
            }
            cbFunction();
        },
        productCertificateList () { // 证书下拉列表
            seekCertificateList().then((response) => {
                if (response.data.state === 200) {
                    this.configPullDownData.certifiName = response.data.data.list
                } else {
                    alert(response.data.msg);
                }
            })
        },
        noPullDown (parm) { // 没有下拉数据
            if (parm) {
                if (this.configPullDownData[parm]) {
                    return true;
                }
            }
            return false;
        },
        getCertificate (parm) { // 选择证书
            for (let i = 0; i < this.configPullDownData.certifiName.length; i++) {
                if (this.configPullDownData.certifiName[i].certificateName === parm) {
                    return [this.configPullDownData.certifiName[i]];
                }
            }
            return false;
        },
        getSpecialComputed (parm) { // 过滤联动的计算值
            var filterName = new Set(['ratio', 'soldPrice']); // 倍率，售价
            let getName = [parm].filter(x => filterName.has(x))
            return getName[0];
        },
        // ///////////   开关   /////////////////
        cutIncident (parm) { // 伸缩总开关按钮
            if (parm.colspan === 1) { // 从闭合恢复展开
                parm.colspan = parm.colspanOpen;
                parm.propertyDatas = parm.open;
                parm.stateOpen = parm.englishName;
            } else { // 从展开恢复闭合
                parm.colspan = 1;
                parm.propertyDatas = [{"name": ""}];
                parm.stateOpen = "";
            }
        },
        // ///////////   核心功能   /////////////
        appendRow () { // 新增
            let rowData = JSON.stringify(this.configBlankData());
            let newRowData = JSON.parse(rowData);
            this.datas.newBlandDatas.push(newRowData);
        },
        batchAddFun () { // 批量新增
            this.popup.batchAdd = true;
        },
        batchDelect () { // 确认删除
            console.log("删除");
            console.log(this.crudData.checkList);
            if (this.newDatas.orderNumber) { // 如果没有单据号，就是全新添加
                alert("如果没有单据号，就是全新添加");
                this.deletePost();
            }
            this.deleteLocality();
        },
        deletePost () { // 删除服务器的
            let deleteList = [];
            let productIdList = []; // 总商品Id（包括查询出来的）
            // let postIdList = []; // 服务器的Id
            for (let i of this.crudData.checkList) { // 提取总数据选中的值
                deleteList.push(this.showDataList[i])
            }
            sessionStorage.setItem("提取总数据选中的值", JSON.stringify(deleteList));
            for (let j of deleteList) {
                for (let i of this.goodsLists.rowDataList) {
                    if (j.newList.productId === i.productId) {
                        var productId = {
                            productId: j.newList.productId
                        }
                        productIdList.push(productId);
                    }
                }
            }
            let data = {
                "orderNum": this.newDatas.orderNumber, // 单据号
                "productList": productIdList // 商品Id列表
            }
            console.log("删除的请求参数");
            sessionStorage.setItem("删除的请求参数", JSON.stringify(data));
            console.log(data);
            deleteUpdateGoods(data).then((response) => {
                console.log("删除结果");
                console.log(response);
                if (response.data.state === 200) {
                    alert("删除成功")
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log("shibai")
            })
            console.log("删除服务器的数据集合");
            console.log(productIdList);
            console.log("删除服务器的数据集合");
        },
        deleteLocality () { // 删除本地的（基本删除）
            let deleteList = [];
            for (let i of this.crudData.checkList) { // 提取总数据选中的值
                deleteList.push(this.showDataList[i])
            }
            sessionStorage.setItem("提取总数据选中的值", JSON.stringify(deleteList));
            for (let i = 0; i < this.datas.hostList.length; i++) { // 删除请求回来的历史数据
                for (let j of deleteList) {
                    if (this.datas.hostList[i] === j.hostList) {
                        this.datas.hostList.splice(i, 1)
                    }
                }
            }
            for (let i = 0; i < this.datas.newList.length; i++) { // 删除请求回来的修改数据
                for (let j of deleteList) {
                    if (this.datas.newList[i] === j.newList) {
                        this.datas.newList.splice(i, 1)
                    }
                }
            }
            for (let i = 0; i < this.datas.newBlandDatas.length; i++) { // 删除空白的数据
                for (let j of deleteList) {
                    console.log(j)
                    console.log("/");
                    console.log(this.datas.newBlandDatas[i]);
                    if (this.datas.newBlandDatas[i] === j) {
                        console.log("警来");
                        console.log(this.datas.newBlandDatas[i])
                        this.datas.newBlandDatas.splice(i, 1)
                    }
                }
            }
            this.crudData.operationCut = true;
            this.crudData.checkList = [];
        },
        // ///////////////核心过滤器//////////////////////
        getLittleClassKey (parm) { // 开关过滤器
            // var getOpenName = ['barcode', 'status', 'remark'];
            var getOpenName = ['remark'];
            for (let i = 0; i < this.littleClass.length; i++) {
                if (this.littleClass[i].stateOpen) { // 通过小类的stateOpen来控制
                    for (let j = 0; j < this.littleClass[i].open.length; j++) {
                        if (this.littleClass[i].open[j].englishName !== "") {
                            getOpenName.push(this.littleClass[i].open[j].englishName)
                        }
                    }
                }
            }
            var filterNames = new Set(getOpenName);
            var state = parm.filter(x => filterNames.has(x));
            if (state !== 'productId') {
                return state;
            } else {
                return;
            }
        },
        getReadOnly (littleClassKey) { // 过滤只读值
            return readOnly(littleClassKey);
        },
        getStateOpen (parm, parm2) { // 大类过滤的开关
            if (parm2 !== "") {
                return parm.propertyDatas;
            } else {
                return false;
            }
        },
        getconfigData (parm) { // 得到配置代码
            let configDatas = {};
            for (let key in this.rowConfigData) {
                configDatas[key] = parm[key] || null
            }
            return configDatas;
        },
        queryBarCode (parm) { // 通过条码号搜索数据
            if (parm) {
                this.workProductDetails(parm);
            }
        },
        workProductDetails (parm) { // 获取商品列表
            let data = {
                "page": "1", // 当前页
                "pageSize": "1", // 每页显示数
                "objId": parm,
                "type": "5" // 操作类型 3=单据号; 5=条码号
            }
            getUpdateGoodsList(data).then((response) => { // 商品修改列表
                console.log("商品修改列表");
                console.log(response);
                console.log("商品修改列表");
                if (response.data.state === 200) {
                    this.datas.hostList.push(this.getconfigData(response.data.data.nowDataList[0])); // 历史数据
                    if (response.data.data.rowDataList[0]) { // 如果有修改数据
                        this.datas.newList.push(this.getconfigData(response.data.data.rowDataList[0])); // 修改数据
                    } else {
                        let rowConfigData = JSON.stringify(this.getconfigData(this.rowConfigData));
                        let rowData = JSON.parse(rowConfigData);
                        rowData.productId = response.data.data.nowDataList[0].productId;
                        sessionStorage.setItem("小华配置数据", JSON.stringify(rowData));
                        // console.log("小华配置数据");
                        // console.log(response.data.data.nowDataList[0].productId);
                        // console.log("小华配置数据");
                        this.datas.newList.push(rowData);
                    }
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response + "商品修改列表")
            })
            if (this.datas.newBlandDatas.length < 2) { // 如果只有一条空白数据，直接把条形码还原就可以了
                this.datas.newBlandDatas[0].hostList.barcode = null;
            } else { // 否则删除当前的
                for (var i = 0; i < this.datas.newBlandDatas.length; i++) {
                    if (this.datas.newBlandDatas[i].hostList.barcode === parm) {
                        this.datas.newBlandDatas.splice(i, 1);
                    }
                }
            }
            // this.datas.hostList.push(this.getconfigData(this.rowCeshiData.rowDataList)); // 历史数据
            // if (this.rowCeshiData.newProduct) { // 如果有修改数据
            //     this.datas.newList.push(this.getconfigData(this.rowCeshiData.newProduct)); // 修改数据
            // } else {
            //     let rowConfigData = JSON.stringify(this.getconfigData(this.rowConfigData));
            //     let rowData = JSON.parse(rowConfigData);
            //     this.datas.newList.push(rowData);
            // }
        },
        pitchOn (parm, index, e) { // 选中样式和数据
            this.operate.selectedRow = [];
            Vue.set(this.operate.selectedRow, index, true);
            this.crudData.selectedData = parm;
            sessionStorage.setItem("选中样式和数据", JSON.stringify(parm));
            if (parm.productId) { // 如果有商品id
                var initialValue = JSON.stringify(parm);
                if (this.aloneAmendingData.initialValue.length < 1) { // 为空时
                    this.aloneAmendingData.changeValue.push(parm);
                    this.aloneAmendingData.initialValue.push(JSON.parse(initialValue));
                } else {
                    var isNew = true;
                    for (var i = 0; i < this.aloneAmendingData.changeValue.length; i++) {
                        if (this.aloneAmendingData.changeValue[i] === parm) {
                            isNew = false;
                        }
                    }
                    if (isNew) {
                        this.aloneAmendingData.changeValue.push(parm);
                        this.aloneAmendingData.initialValue.push(JSON.parse(initialValue));
                    }
                }
            }
        },
        admendFun (admendList) { // 修改商品列表
            let data = {
                "orderNum": "", // 单据号,当confirmType=2/3/4时则必输
                "confirmType": 1, // 1=保存，2=提交审核，3=审核通过，4=取消审核
                "checkRemark": "", // 审核备注
                "applyRemark": this.orderRemark, // 提交备注
                "auditorId": "", // 审核人ID; 当confirmType=2时必输
                "handleDeptId": this.newDatas.handleDeptId, // 操作部门ID
                "operationType": this.newDatas.userType, // 操作类型：1=部门；2=店铺
                "alterList": admendList // 当confirmType=1是必输
            }
            updateGoods(data).then((response) => {
                if (response.data.state === 200) {
                    this.aloneAmendingData.initialValue = [];
                    this.aloneAmendingData.changeValue = [];
                    this.popup.fullscreenLoading = false;
                    console.log("添加修改商品");
                    console.log(response);
                    if (!this.newDatas.orderNumber) { // 如果没有单据号，就是全新添加
                        this.newDatas.orderNumber = response.data.data.orderNumber;
                        sessionStorage.setItem("newDatas", JSON.stringify(this.newDatas));
                        this.workPopupSave({
                            "saveSuccess": true,
                            "saveSuccessData": response.data.data
                        })
                    }
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
        },
        addSingleAll () { // 保存
            if (this.aloneAmendingData.initialValue.length > 0) { // 如果有变化，就调用修改
                this.popup.fullscreenLoading = true;
                let productListOrId = productOrId(this.aloneAmendingData.initialValue, this.aloneAmendingData.changeValue)
                this.admendFun(productListOrId);
            }
        },
        close () { // 关闭
            this.$router.push("/work/amend");
        },
        // //////////////  测试代码  ////////////////////
        xiaohua () {
            sessionStorage.setItem("初始值", JSON.stringify(this.aloneAmendingData.initialValue));
            sessionStorage.setItem("变化值", JSON.stringify(this.aloneAmendingData.changeValue));
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
.main-wrap{
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    h1{
        font-size: 20px;
        color: red;
    }
}
.new-storage{
    width: 100%;
    height: 100%;
    padding: 0 20px;
    .header-seek{
        margin-top: 20px;
        position: relative;
        overflow-y: hidden;
        overflow-x: hidden;
        ul{
          float: left;
          white-space: nowrap;
          overflow: hidden;
          li:first-child{
            border: none;
          }
        }
        .table-btn{
            position: absolute;
            right: 0;
            overflow: hidden;
            li{
                position: relative;
            }
            input{
                position: absolute;
                top: -10px;
                left: 0;
                width: 100%;
                height: 132%;
                font-size: 0;
                border: 0;
                background-color: transparent;
                // display: none;
            }
        }
    }
}
.rectangle-icon{
    height: 16px;
    i{
        vertical-align: middle;
        display: inline-block;
        width: 5px;
        height: 16px;
        background-color: $oa;
        margin-right: 10px;
    }
    span{
        vertical-align: middle;
        font-size: 14px;
    }
}
.new-storage{
    .table-template{
        min-width: 100%;
        height: 100%;
        table{
            width: 100%;
            height: 100%;
            background-color: $e8;
            .tr-header{
                background-color: $e8!important;
                &:hover{
                    background-color: $e8;
                }
                .header-state{
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;
                    li:first-child{
                        background-color: $e8;
                    }
                    li{
                        height: 40px;
                        line-height: 40px;
                        border-bottom: 1px solid $d6;
                        background-color: #fff;
                    }
                }
            }
            tr{
                height: 40px;
                width: 100%;
            }
            tr:nth-child(odd){
                background-color: #fff;
            }
            tr:nth-child(even){
                background-color: $f6;
            }
            tr{
                // &:hover{
                //     background-color: $e6;
                // }
            }
            th,td{
                padding: 10px;
                text-align: center;
                border-right: 1px solid $d6;
                border-bottom: 1px solid $d6;
                font-size: 12px;
                min-width: 70px;
                height: 40px;
                overflow: hidden;
                white-space:nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
            }
            td:last-child{
                border-right: 0;
            }
            th{
                color: $c6;
                position: relative;
                span{
                    vertical-align: middle;
                    // margin-right: 10px;
                }
                .open-icon{
                    vertical-align: middle;
                    position: absolute;
                    right: 5px;
                    top: 0;
                    bottom: 0;
                    margin-top: 14px;
                    cursor: pointer;
                }
            }
            td{
                color: $c3;
            }
            .table-pull{
                padding: 0!important;
            }
        }
    }
}
</style>