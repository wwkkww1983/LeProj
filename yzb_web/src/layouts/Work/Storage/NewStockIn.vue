<template>
    <div class="new-storage">
        <section class="body-box">
            <div class="body-header">     
                <ul class="header-nav">
                    <router-link tag="li" to="/work/storage">入库单据<span>&nbsp>&nbsp</span></router-link>
                   <!--  <router-link tag="li" to="/work/sell/sellReceiptsList">入库单据<span>&nbsp>&nbsp</span></router-link> -->
                    <li class="on">产品列表</li>
                </ul>
                <ul class="header-btn" v-if="crudData.operationCut">
                    <li @click="appendRow">添加商品</li>
                    <li @click="popup.copys = true">复制</li>
                    <li class="to-lead">
                        导入表格
                        <input ref="tableList" type="file" name="file" @change="uploadingOne($event)"/>
                    </li>
                    <li @click="popup.amendings = true">批量修改</li>
                    <li>打印单据</li>
                    <li>打印标签</li>
                    <li @click="crudData.operationCut = false">删除</li>
                    <li @click="addSingleAll" v-loading.fullscreen.lock="popup.fullscreenLoading" element-loading-text="拼命进行中...">保存</li>
                    <li @click="closeStorage">关闭</li>
                </ul>
                <ul class="header-btn footer-affirm" v-if="!crudData.operationCut">
                    <li @click="crudData.operationCut = true">取消</li>
                    <li @click="batchDelect">确认</li>
                </ul>
            </div>
            <div class="body-wrap" :style="bodyWrap">
            <div class="table-wrap">
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
                           <!--  <th rowspan="2">当前状态</th> -->
                            <th :class="{cursorPointer: item.name !== '首饰名称'}" v-for="item in littleClass" :colspan="item.colspan" @click="cutIncident(item)">
                                <span>{{item.name}}</span>
                                <i v-if="item.name !== '首饰名称'" class="open-icon" v-bind:class="{ 'el-icon-arrow-right': !item.stateOpen, 'el-icon-arrow-left': item.stateOpen }"></i>
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
                                    <th :rowspan="showData.length + 1">
                                        <ul class="header-state">
                                            <li v-for="n in showData.length + 1"></li>
                                        </ul>
                                    </th>
                                </template>
                            </template> 
                        </tr>
                        <tr v-if="showData" v-for="(AllData,index) in showData" @click="pitchOn(AllData,index,$event)" :class="{'table-pitch-on': operate.selectedRow[index] }">
                            <td v-if="!crudData.operationCut">
                                <!-- <el-checkbox :label='index+((crudData.currentPage - 1) * 30)'></el-checkbox> -->
                                <el-checkbox :label='AllData.productId'></el-checkbox>
                            </td>
                            <td>{{index+1+((crudData.currentPage - 1) * 30)}}</td>
                            <template v-for="littleClassKey in getLittleClassKey(Object.keys(AllData))">
                                <!-- 只读（包含生成和状态类的） -->
                                <template v-if="getReadOnly(littleClassKey)">
                                    <!-- 后面的汇总金料额，证书费，主石额，副石额，工费额，配件额、其他费用额 -->
                                    <template v-if="littleClassKey === 'costPrice'">
                                        <td>{{AllData.goldE}}</td>
                                        <td>{{AllData.certifiFee}}</td>
                                        <td>{{AllData.mainPrice}}</td>
                                        <td>{{AllData.deputyPrice}}</td>
                                        <td>{{AllData.inMoney}}</td>
                                        <td>{{AllData.price}}</td>
                                        <td>{{AllData.otherFee}}</td>
                                    </template>
                                    <td>
                                        <p v-text="getComputed(littleClassKey, AllData, index)"></p>
                                    </td>
                                </template>
                                <template v-else>
                                    <td class="table-pull select-w80"><!-- 选中时才给table-pull后面写 -->
                                          <p v-if="!operate.selectedRow[index]">{{AllData[littleClassKey]}}</p>
                                          <template v-else>
                                                <!-- 输入(这个容器划分输入框的限制内容) -->
                                                <template v-if="!noPullDown([littleClassKey])">
                                                    <template v-if='littleClassKey === "barcode"'>        
                                                            <bar-code
                                                                :value = "AllData.barcode"
                                                                :maxLengthSix = "limitInput[littleClassKey].maxlength"
                                                                :pureNumber = "limitInput[littleClassKey].pureNumber"
                                                                v-model.number="AllData.barcode"
                                                            ></bar-code>
                                                    </template>
                                                    <template v-else>
                                                        <!-- 售价，倍率的联动 -->
                                                        <template v-if="getSpecialComputed(littleClassKey)">
                                                            <input-ratio
                                                                v-if="littleClassKey === 'ratio'"
                                                                v-model="AllData.ratio"
                                                                :ratio=showData[index].ratio
                                                                :costPrice=showData[index].costPrice
                                                                :soldPrice = showData[index].soldPrice
                                                            >
                                                            </input-ratio>
                                                            <input-sold-price
                                                                v-else
                                                                v-model="AllData.soldPrice"
                                                                :ratio=showData[index].ratio
                                                                :costPrice=showData[index].costPrice
                                                                :soldPrice = showData[index].soldPrice
                                                            >   
                                                            </input-sold-price>
                                                        </template>
                                                        <!-- 特定输入 -->
                                                        <template v-else>
                                                            <!-- 纯数字（当前值，纯数字，最大输入值） -->
                                                            <pure-numbers
                                                                :value = "AllData[littleClassKey]"
                                                                :pureNumber = "limitInput[littleClassKey].pureNumber"
                                                                :maxLengthSix = "limitInput[littleClassKey].maxlength"
                                                                v-model.number="AllData[littleClassKey]"
                                                            ></pure-numbers>
                                                            <!-- 整数（当前值，整数，最大输入值） -->
                                                            <integer
                                                                :value = "AllData[littleClassKey]"
                                                                :integer = "limitInput[littleClassKey].integer"
                                                                :maxLengthSix = "limitInput[littleClassKey].maxlength"
                                                                v-model.number="AllData[littleClassKey]"
                                                            ></integer>
                                                            <!-- 小数（当前值，小数，最大输入值） -->
                                                            <slice-number
                                                                :value = "AllData[littleClassKey]"
                                                                :sliceNumber = "limitInput[littleClassKey].sliceNumber"
                                                                :maxLengthSix = "limitInput[littleClassKey].maxlength"
                                                                v-model.number="AllData[littleClassKey]"
                                                            ></slice-number>
                                                            <input v-if="limitInput[littleClassKey].onlyMaxlength" class="form-input" :maxlength="limitInput[littleClassKey].maxlength" v-model="AllData[littleClassKey]"></input>
                                                        </template>
                                                    </template>
                                                </template>
                                                <!-- 下拉 -->
                                                <template v-else>
                                                    <el-select filterable v-model="AllData[littleClassKey]" placeholder="">
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
                                        <template v-if="getCertificate(AllData.certifiName)" v-for="item in getCertificate(AllData.certifiName)">
                                            <td>{{item.organizationName}}</td>
                                            <td>{{item.website}}</td>
                                            <td>{{item.phone}}</td>
                                            <td>{{item.stand1}}</td>
                                            <td>{{item.stand2}}</td>
                                            <td>{{item.stand3}}</td>
                                        </template>
                                        <template v-if="!getCertificate(AllData.certifiName)">
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
                    </table>
                </el-checkbox-group>    
            </div>
            <div class="right-receipts-data-list">
                <receipts-intro :toRouter='"/work/storage"'></receipts-intro>
            </div>
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
        <!-- 弹窗 -->
        <div>
            <!-- 复制弹窗 -->
            <div class="dialog-w380">
                <el-dialog title="复制当前产品信息" v-model="popup.copys">
                    <section class="copy-body p-lf30 p-tb30">
                        <p>复制行数</p>
                        <el-input
                            placeholder="请输入复制行数"
                            v-model="popup.copyNumber">
                        </el-input>
                    </section>
                    <span slot="footer">
                        <a href="javascript: void(0)" class="cancel-btn-w120" @click="popup.copys = false">取 消</a>
                        <a href="javascript: void(0)" class="confirm-btn-w120 m0" @click="copyRowData">确 定</a>
                    </span>
                </el-dialog>
            </div>
            <!-- 批量修改 -->
            <div class="dialog-w420">
                <el-dialog title="批量修改" v-model="popup.amendings">
                    <section class="p-lr-30">
                        <div class="rectangle-icon">
                            <i></i>
                            <span>请选择需要修改的内容</span>
                        </div>
                        <ul class="amending-select">
                            <li>请选择大类别：</li>
                            <li class="select-w200">
                                <el-select filterable v-model="popup.amendingLargeClass" placeholder="大类别">
                                <el-option v-for="item in littleClass"
                                :label="item.name"
                                :value="item.englishName">
                                <span @click="popup.admentUrl = item.admentUrl">{{item.name}}</span>
                                </el-option>
                                </el-select>
                            </li>
                            <li>请选择小类别：</li>
                            <li class="select-w200">
                                <el-select filterable ref='littleClass' v-model="popup.amendingLittleClass" placeholder="小类别">
                                    <el-option v-for="item in getLittleClass(littleClass, popup.amendingLargeClass)"
                                    :label="item.name"
                                    :value="item.englishName">
                                    </el-option>
                                </el-select>
                            </li>
                            <li>请选择或输入内容：</li>
                            <li class="select-w200">
                                <div v-if="!getAmendData(popup.amendingLittleClass)" class="input-w200">                         
                                    <el-input v-model="popup.amendingData"></el-input>
                                </div>
                                <el-select filterable v-else v-model="popup.amendingData">
                                <el-option v-for="item in getAmendData(popup.amendingLittleClass)"
                                :label="item.name || item.brandName || item.certificateName || item.classesName"
                                :value="item.name || item.brandName || item.certificateName || item.classesName">
                                </el-option>
                                </el-select>
                            </li>
                        </ul>
                        <div class="rectangle-icon">
                            <i></i>
                            <span>请选择需要修改的范围</span>
                        </div>
                        <ul class="amending-scope">
                            <el-radio-group v-model="popup.amendingPitchOn">
                                <li>                                        
                                    <el-radio :label='popup.amendingCustomRow'>调整行范围：</el-radio>
                                </li>
                                <li>
                                    <div class="input-w100">
                                        <el-input v-model="popup.amendingStartRow" :disabled='popup.amendingPitchOn === "2"'></el-input>
                                    </div>
                                    <span class="m-lr-10 font14">至</span>
                                    <div class="input-w100">
                                        <el-input v-model="popup.amendingEndRow" :disabled='popup.amendingPitchOn === "2"'></el-input>
                                    </div>
                                </li>
                                <li class="all-check">
                                    <el-radio :label='popup.amendingAllRow'>全选</el-radio>
                                </li>
                            </el-radio-group>
                        </ul>
                    </section>
                    <span slot="footer">
                        <a href="javascript: void(0)" class="cancel-btn-w120 mr70" @click="popup.amendings = false">取 消</a>
                        <a href="javascript: void(0)" class="confirm-btn-w120 m0" @click="batchAmending">确 定</a>
                    </span>
                </el-dialog>
            </div>
            <div class="dialog-w380">
                <el-dialog title="错误提示" v-model="popup.error">
                    <section class="error-body">
                        <p>{{popup.errorMsg}}</p>
                    </section>
                    <span slot="footer">
                        <a href="javascript: void(0)" class="cancel-btn-w120" @click="popup.error = false">取 消</a>
                        <a href="javascript: void(0)" class="confirm-btn-w120 m0" @click="popup.error = false">删 除</a>
                    </span>
                </el-dialog>
            </div>
            <make-success
            :routerUrl='"/work/storage/detailStorage"'
            :successData="popup.successData"
            v-if="popup.successData"
            ></make-success>
        </div>
    </div>
</template>
<script>
import Vue from 'vue'
import {mapActions, mapGetters} from 'vuex'
import { downloadTable, seekProductClassList, seekProductPropertyList, seekCertificateList, seekNewGoodsInfoList } from './../../../Api/commonality/seek'
import makeSuccess from './../CommonalityComponent/popupTemplate/makeSuccess'
import {productDetailStatus} from './../../../Api/commonality/status'
// import {operateOWhourse} from './../../../Api/commonality/operate'operateAddProductToRKOrder
import {operateAddProductToRKOrder, delectProductDetail} from './../../../Api/commonality/operate'
import {readOnly} from './../../../Api/commonality/filter' // 过滤器
import {computedStorageData} from "./../../../Api/commonality/computed" // 计算器
import inputRatio from "./../CommonalityComponent/input/computed/inputRatio" // 倍率计算
import inputSoldPrice from "./../CommonalityComponent/input/computed/inputSoldPrice" // 售价计算
import pureNumbers from "./../CommonalityComponent/input/pureNumbers.vue" // 纯数字
import integer from "./../CommonalityComponent/input/integer.vue" // 整数
import sliceNumber from "./../CommonalityComponent/input/sliceNumber.vue" // 小数
import barCode from "./../CommonalityComponent/input/BarCode.vue" // 条形码
import inputComputed from "./inputComputed.vue"
import receiptsIntro from "./../../../components/work/ReceiptsIntro.vue"
export default {
    components: {
        inputComputed,
        pureNumbers,
        integer,
        sliceNumber,
        barCode,
        inputRatio,
        inputSoldPrice,
        makeSuccess,
        receiptsIntro
    },
    data () {
        var checkAge = (rule, value, callback) => {
        if (!value) {
            return callback(new Error('不能为空'));
        }
        setTimeout(() => {
            if (!/^[0-9]/.test(value)) {
                callback(new Error('请输入整数'));
            } else {
                callback();
            }
        }, 500);
    };
    return {
        "getGoodsInfoList": [],
        "bodyWrap": {},
        "operate": { // 操作的状态
            selectedRow: [], // 记录当前点中，待后期优化
            openState: true // 伸缩列表总开关按钮
        },
        "rules2": { // 表单验证
            amendingStartRow: [{
                type: 'number',
                validator: checkAge,
                trigger: 'blur,change'
            }],
            region: [{
                required: true,
                message: '',
                trigger: 'change'
            }]
        },
        "header": {
            supplierId: null, // 供应商Id
            shopId: null, // 计划分销商inWarehouse
            warehouseId: null, // 入库库位Id
            orderRemark: null, // 单据备注
            excelUrl: null // 表格地址
        },
        "crudData": { // 增查更删数据
            checked: false, // 监听全选
            checkedPage: null, // 当页的所有数据（用作全选操作）
            checkList: [], // 选择按钮 删除
            currentPage: 1, // 当前页
            operationCut: true, // 操作按钮切换
            newAddData: [], // 新增数据
            selectedData: null // 选中数据
        },
        "popup": { // 弹窗
            // success: false, // 成功（制单成功）
            successData: null, // 制单成功数据
            // orderNumber: null, // 单号
            // createTime: null, // 制单时间
            // orderId: null, // 制单Id
            orderUerName: sessionStorage.getItem("nickname"), // 制单人
            fullscreenLoading: false, // 操作的过程弹窗(保存)
            copys: false, // 复制弹窗
            copyNumber: 0, // 复制数量
            amendings: false, // 批量修改弹窗
            amendingLargeClass: null, // 批量修改的大类别
            amendingLittleClass: null, // 批量修改的小类别
            amendingLittlePull: null, // 绑定传给输入内容的过滤器参数
            amendingData: null, // 批量修改的输入内容
            amendingStartRow: null, // 批量修改的开始行数
            amendingEndRow: null, // 批量修改的结束行数
            amendingCustomRow: "1", // 批量修改的自定义输入 // 单选框
            amendingAllRow: "2", // 批量修改的全选行数  // 单选框
            amendingPitchOn: "1", // 批量修改的全选或自定义输入 // 单选框汇总
            admentUrl: null, // 批量修改的地址
            error: false, // 错误信息弹窗
            errorMsg: null // 错误信息
        },
        "aloneAmendingData": { // 独立修改(修改保存后的数据)
            initialValue: [], // 初始值
            changeValue: [] // 变化值
        },
        // vuex管理
        'littleClass': [], // 小类
        "configPullDownData": null, // 配置的下拉数据
        "downUrl": null // 下载地址
    }
    },
    computed: {
        ...mapGetters([
            // "repositoryList", // 库位列表
            "rowConfigData", // 一行数据
            "littleClassList", // 小类
            "limitInput", // 输入限制
            "configPullDownDataList", // 配置的下拉数据
            "supplierListData", // 供应商
            "applyDepartementList", // 计划分销商
            "productClass" // 产品类别（商品类型列表）
            // "applyOnlyShop" // 只有店铺
        ]),
        paginationSize: function () { // 分页的大小
            return this.allData.length;
        },
        allData: function () { // 总数据
            return [...this.getGoodsInfoList, ...this.crudData.newAddData];
        },
        showData: function () { // 存在一个大bug，如果中间不请求
            let state = ((this.crudData.currentPage - 1) * 30);
            let end = (this.crudData.currentPage * 30);
            // return this.crudData.newAddData.slice(state, end);
            return this.allData.slice(state, end);
        },
        getCheckedPage: function () { // 监听当页的所有做选择的变量（删除选择）
            var CheckedPage = [];
            for (var i = 0; i < this.showData.length; i++) {
                CheckedPage.push(i + ((this.crudData.currentPage - 1) * 30))
            }
            return CheckedPage;
        }
    },
    watch: {
        'crudData.currentPage': function () { //  监听分页
            this.pageJudgeFun();
            this.getNewGoodsInfoList(this.crudData.currentPage, '30');
        },
        'crudData.checkList': function () { // 监听全选
            this.pageJudgeFun();
        },
        'popup.amendingLargeClass': function () { // 切换大类别时，清空其它选项
            this.popup.amendingLittleClass = null;
            this.popup.amendingData = ""
        },
        'popup.amendingLittleClass': function () { // 切换小类别时，清空批量修改的输入内容
            this.popup.amendingData = "";
        }
    },
    created () { // 获取下拉数据
        // this.workRepositoryList(); // 库位列表
        this.getNewGoodsInfoList('1', '30');
        this.appendRow(); // 进来默认新增一条
        this.getDownUrl(); // 获取模板地址
        this.workSupplierList(); // 获取供应商列表
        this.workApplyDepartement(); // 计划分销商
        this.productClassList(); // 获取商品大小类的下拉列表
        this.productPropertyList(); // 商品属性下拉列表
        this.productCertificateList(); // 证书下拉列表
        this.getConfigInto(); // 初始化一些配置数据
    },
    methods: {
        ...mapActions([
            // "workRepositoryList", // 库位列表
            "workSupplierList", // 供应商列表
            "workApplyDepartement", // 计划分销商
            "workProductDetail", // 商品列表
            "workProductStatus" // 审核状态
        ]),
        handleSizeChange (val) {
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange (val) {
            this.crudData.currentPage = val;
            console.log(`当前页: ${val}`);
        },
        getconfigData (parm) { // 得到配置代码
            var configData = [];
            if (parm) {
                for (let i = 0; i < parm.length; i++) {
                    let newAddDatas = {};
                    for (let key in this.rowConfigData) {
                        newAddDatas[key] = parm[i][key] || null;
                    }
                    configData.push(newAddDatas)
                }
            }
            return configData;
        },
        getNewGoodsInfoList (page, pageSize) {
            var options = {
                "page": page,
                "pageSize": pageSize,
                "orderNum": sessionStorage.getItem("orderNumber")
            }
            seekNewGoodsInfoList(options).then((response) => {
                console.log("获取入库详情列表");
                console.log(response);
                if (response.data.state === 200) {
                    this.getGoodsInfoList = this.getconfigData(response.data.data.rowDataList);
                }
            }, (response) => {
                console.log(response);
            })
        },
        getConfigInto () { // 初始化一些配置数据
            if (this.littleClassList) { // 初始化小类
                this.littleClass = this.littleClassList;
            }
            if (this.configPullDownDataList) { // 配置下拉数据
                this.configPullDownData = this.configPullDownDataList;
            }
        },
        // 删除的选择监听
        cutSelect (parm) { // 切换全选
            var _self = this;
            setTimeout(function() {
                _self.cutSelectFun();
            }, 13);
        },
        cutSelectFun () {
            function sortNumber (a, b) {
                return b - a;
            }
            if (this.crudData.checked === true) { // 全选
                  for (let i of this.getCheckedPage) {
                      this.crudData.checkList.push(i)
                  }
                  this.crudData.checkList = [...new Set(this.crudData.checkList)];
                  console.log(this.crudData.checkList)
            } else { // 取消
                let nameListFalse = [];
                for (let i of this.getCheckedPage) {
                    if (this.crudData.checkList.indexOf(i) || this.crudData.checkList.indexOf(i) === 0) {
                        nameListFalse.push(i);
                    }
                }
                let delectNew = nameListFalse.sort(sortNumber);
                console.log(delectNew);
                for (let i of delectNew) { // 删除新增数据
                    this.crudData.checkList.splice(i, 1);
                }
                nameListFalse = [];
            }
         },
         pageJudgeFun () { // 判断是否全选
            var judgeChecked = []
            for (let i of this.crudData.checkList) {
                for (let j of this.getCheckedPage) {
                    if (i === j) {
                        judgeChecked.push(i)
                    }
                }
            }
            if (judgeChecked.length === this.getCheckedPage.length) {
                this.crudData.checked = true;
            } else {
                this.crudData.checked = false;
            }
         },
        // ///////////   查询数据   /////////////
        // ///////////////(获取下拉数据)////////////////////
        getDownUrl () { // 获取模板地址
            downloadTable().then((response) => {
                if (response.data.state === 200) {
                    this.downUrl = response.data.data.url;
                }
            }, (response) => {
                console.log(response);
            })
        },
        productClassList () { // 获取商品大小类的下拉列表
            var _self = this;
            var requestNumber = 1;
            function cbFunction () {
                seekProductClassList(requestNumber).then((response) => {
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
                          break;
                    }
                    requestNumber += 1;
                    if (requestNumber < 5) {
                        cbFunction();
                    }
                    console.log(response)
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
                }, (response) => {
                    console.log(response + "获取商品属性下拉列表");
                })
            }
            cbFunction();
        },
        productCertificateList () { // 证书下拉列表
            seekCertificateList().then((response) => {
                console.log("证书下拉列表");
                console.log(response);
                console.log("证书下拉列表");
                if (response.data.state === 200) {
                    this.configPullDownData.certifiName = response.data.data.list
                } else {
                    console.log(response)
                }
            })
        },
        // ///////////   下拉数据   /////////////
        noPullDown (parm) { // 没有下拉数据
            if (parm) {
                if (this.configPullDownData[parm]) {
                    return true;
                }
            }
            return false;
        },
        getSpecialComputed (parm) { // 过滤联动的计算值
            var filterName = new Set(['ratio', 'soldPrice']); // 倍率，售价
            let getName = [parm].filter(x => filterName.has(x))
            return getName[0];
        },
        // ///////////   处理数据 （保存后的数据）  /////////////
        cutIncident (parm) { // 伸缩总开关按钮
            if (parm.name !== '首饰名称') {
                if (parm.colspan === 1) { // 从闭合恢复展开
                    parm.colspan = parm.colspanOpen;
                    parm.propertyDatas = parm.open;
                    parm.stateOpen = parm.englishName;
                } else { // 从展开恢复闭合
                    parm.colspan = 1;
                    parm.propertyDatas = [{"name": ""}];
                    parm.stateOpen = "";
                }
            }
        },
        getStateOpen (parm, parm2) { // 大类过滤的开关
            if (parm2 !== "") {
                return parm.propertyDatas;
            } else {
                return false;
            }
        },
        getReadOnly (littleClassKey) { // 过滤只读值
            return readOnly(littleClassKey);
        },
        getComputed (littleClassKey, AllData, index) { // 得到计算值
            var Result = computedStorageData(littleClassKey, index, this.showData[index]);
            AllData[littleClassKey] = Result;
            return Result;
        },
        // ///////////   核心过滤器   /////////////
        getStatus (parm) { // 审核状态
            return productDetailStatus(parm);
        },
        getLittleClassKey (parm) { // 开关过滤器
            var getOpenName = ['barcode', 'status', 'remark'];
            // var getOpenName = [];
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
        getCertificate (parm) { // 选择证书
            for (let i = 0; i < this.configPullDownData.certifiName.length; i++) {
                if (this.configPullDownData.certifiName[i].certificateName === parm) {
                    return [this.configPullDownData.certifiName[i]];
                }
            }
            return false;
        },
        getLittleClass (littleDatas, currentData) { // 自定义过滤器 小类别
            if (littleDatas && currentData) {
              let arr = littleDatas.filter(function (littleData) {
                  return littleData.englishName === currentData; // || littleData.name === currentData
              })
              let data = arr[0].open.filter(x => !readOnly(x.englishName));
              console.log(data);
              return data;
            }
        },
        getAmendData (amendData) { // 自定义过滤器  得到修改的内容 小类别
            for (let i in this.configPullDownData) {
                if (i === amendData) {
                    return this.configPullDownData[i];
                }
            }
            return false;
        },
        // ///////////   核心功能   /////////////
        uploadingOne (e) { // 表格导入数据 one
            var files = e.currentTarget.files[0];
            var fileData = new FormData(); // 创建表单数据对象(本地上传服务器文件对象)
            fileData.append("excelFile", files); // 将文件对象append进去
            let data = {
                "data": {
                    "orderNum": sessionStorage.getItem("orderNumber")
                },
                "unit": {
                    "companyId": sessionStorage.getItem("companyId"),
                    "channel": 3,
                    "os": "string",
                    "ip": "string",
                    "userId": sessionStorage.getItem("id"),
                    "tokenId": sessionStorage.getItem("tokenId")
                }
            }
            fileData.append("data", JSON.stringify(data)); // 用户id
            // 第一页获得的数据
            var url = INTERFACE_URL_9083 + "/v1/warehouse/uploadProductByExcel"
            this.$http.post(url, fileData).then((response) => {
                this.$refs.tableList.value = null;
                if (response.data.state === 200) {
                    this.getNewGoodsInfoList('1', '30');
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log("导入表格失败" + response)
            })
        },
        pitchOn (parm, index, e) { // 选中样式和数据
            this.operate.selectedRow = [];
            Vue.set(this.operate.selectedRow, index, true);
            this.crudData.selectedData = parm;
            if (parm.barcode) { // 如果有条形码
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
        appendRow () { // 新增
            var rowData = JSON.stringify(this.rowConfigData);
            var newRowData = JSON.parse(rowData);
            this.crudData.newAddData.push(newRowData);
        },
        copyRowData () { // 复制数据
            this.popup.copys = false;
            var copyRows = [];
            if (this.crudData.selectedData <= 0) { // 如果没有选中数据
                return;
            }
            var selectedData = JSON.stringify(this.crudData.selectedData);
            for (let i = 0; i < this.popup.copyNumber; i++) {
                copyRows.push(JSON.parse(selectedData));
            }
            for (let i = 0; i < copyRows.length; i++) { // 清除条形码
                copyRows[i].barcode = null;
            }
            this.crudData.newAddData = [...this.crudData.newAddData, ...copyRows];
        },
        batchAmending () { // 批量修改
            var amendingLittleClass = this.popup.amendingLittleClass;
            if (this.popup.amendingPitchOn === "2") {
                for (let i = 0; i < this.crudData.newAddData.length; i++) { // 全选
                    this.crudData.newAddData[i][amendingLittleClass] = this.popup.amendingData;
                }
            } else {
                for (var i = this.popup.amendingStartRow - 1; i < this.popup.amendingEndRow; i++) {
                    this.crudData.newAddData[i][amendingLittleClass] = this.popup.amendingData;
                }
            }
            this.popup.amendings = false;
        },
        batchDelect () { // 删除
            // 删除成功后，再提示要不要保存新增数据(如果要，就直接保存新增的，不要，就把刚才新建的给删除)
            // 第一个判断，是不是只删除新增的，如果是，就直接删除
            // 第二个判断，是不是删除里有保存了的，是，就请求后台，成功后，就把新增选中的删除，然后判断还有新增数据，如果有就询问顾客要不要保存之前新增的
            function sortNumber (a, b) {
                return b - a;
            }
            let delectNew = this.crudData.checkList.sort(sortNumber);
            console.log(delectNew);
            for (let i of delectNew) { // 删除新增数据
                console.log(i);
                this.crudData.newAddData.splice(i, 1);
            }
            var productIdList = [];
            for (var i = 0; i < this.crudData.checkList.length; i++) {
                var oneObject = {};
                oneObject.productId = this.crudData.checkList[i];
                productIdList.push(oneObject);
            }
            let options = {
                "orderNum": sessionStorage.getItem("orderNumber"),
                "productIdList": productIdList
            }
            sessionStorage.setItem("商品操作（删）", JSON.stringify(options));
            delectProductDetail(options).then((response) => {
                console.log("商品操作（删）")
                console.log(response)
                console.log("商品操作（删）")
                if (response.data.state === 200) {
                    this.getNewGoodsInfoList('1', '30');
                    // alert("删除成功")
                } else {
                    alert(response.data.msg)
                }
            }, (response) => {
                console.log(response);
            })
            this.crudData.checkList = [];
            this.crudData.operationCut = true;
        },
        addSingleAll () { // 保存（单个添加/批量添加）
            this.popup.fullscreenLoading = true;
            this.brandNewAdd();
        },
        brandNewAdd () { // 全新添加
            let options = {
                "orderNum": sessionStorage.getItem("orderNumber"),
                "rowDataList": this.crudData.newAddData // 新增数据集合
            }
            sessionStorage.setItem("新增数据集合", JSON.stringify(options));
            operateAddProductToRKOrder(options).then((response) => {
                console.log("新接口，全新添加");
                console.log(response);
                if (response.data.state === 200) {
                    this.popup.fullscreenLoading = false;
                    this.crudData.newAddData = [];
                    this.getNewGoodsInfoList('1', '30');
                } else {
                    this.popup.fullscreenLoading = false;
                    alert(response.data.msg)
                }
            })
        },
        closeStorage () { // 关闭
            this.$router.push("/work/storage")
        }
    }
}
</script>
<style src="./../../../assets/css/_table.scss" lang="scss"></style>
<style lang="scss" scoped>
.new-storage{
    .body-box{
        height: 100%;
        position: relative;
        display: -webkit-flex; /* Safari */
        display: flex;
        flex-direction: column;
        .body-header{
            height: 60px;
            // border: 1px solid #000;
        }
        .body-wrap{
            flex: 1;
            white-space : nowrap;
            font-size: 0;
            margin-top: 20px;
            overflow-y: auto;
            // background-color: #ccc;
            // border: 2px solid blue;
            .table-wrap{ // 表格的外壳
                min-width: 1230px;
                vertical-align: top;
                margin-right: 54px;
                display: inline-block;
                background-color: #fff;
                height: 98%;
                // border: 5px solid #f9f9f9;
            }
        }
        .body-page{
            height: 60px;
            .paga-30{
                margin-top: 15px;
            }
        }
    }
}
.right-receipts-data-list{
    display: inline-block;
    min-width: 300px;
    background-color: #fff;
}
.body-header { // 头部导航
    background-color: #fafbfc;
    height: 60px;
    margin-top: 20px;
    overflow: hidden;
    .header-nav{
        float: left;
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
        .sell-new-data{
            background-color: #46c4b4;
            float: right;
            color: #fff;
            width: 130px;
            height: 40px;
            line-height: 40px;
            border-radius: 4px;
            margin-right: 40px;
            margin-top: 10px;
            i{
                // vertical-align: center;
                height: 40px;
                line-height: 37px;
                font-style:normal;
                margin-right: 6px;
                font-size: 30px;
            }
            span{
                height: 40px;
                line-height: 40px;
                font-size: 16px;
                display: inline-block;
                vertical-align: top;
            }
            &:hover{
                background-color: #44beae;
            }
        }
        .shop-selects{
            // vertical-align: top;
            width: 120px;
            margin-right: 40px;
        }
        .time-selects{
            //  vertical-align: top;
            width: 190px;
        }
    }
    .header-btn{
        float: right;
        // width: 300px;
        height: 60px;
        font-size: 0;
        li{
            display: inline-block;
            height: 38px;
            line-height: 38px;
            width: 90px;
            border-radius: 4px;
            color: #fff;
            text-align: center;
            background-color: #83bbf7;
            font-weight: 600;
            cursor: pointer;
            margin-top: 10px;
            font-size: 14px;
            margin-left: 20px;
            cursor: pointer;
            &:hover{
                background-color: #4580bf;
            }
        }
        li:last-child{
            color: #e58585;
            border: 1px solid #e58585;
            background-color: #fff;
            margin-right: 40px;
            &:hover{
                background-color: #fff;
            }
        }
        .to-lead{ // 导入表格
            position: relative;
            cursor: pointer;
            input{
                position: absolute;
                display: inline-block;
                height: 38px;
                line-height: 38px;
                width: 90px;
                top: 0;
                left: 0;
                cursor: pointer;
                opacity: 0;
            }
        }
    }
}
.cursorPointer{
    cursor: pointer;
}
</style>