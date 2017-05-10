<template>
    <div class="new-storage">
        <div class="main-box">
            <header class="header-box">
                <div class="header-seek">
                    <ul>
                        <li>
                            <span>产品类别：</span>
                            <span class="main-color" v-if="receiptDetail.cargoType">{{receiptDetail.cargoType}}</span>
                        </li>
                        <li>
                            <span>供应商：</span>
                            <div class="select-w100">
                                <el-select ref="supplierDefault" filterable v-model="header.supplierId" placeholder="">
                                    <el-option v-for="item in supplierListData"
                                    :label="item.supplierName"
                                    :value="item.supplierId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <span>入库库位：</span>
                            <div class="select-w100">
                                <el-select ref="warehouseDefault" filterable v-model="header.warehouseId" placeholder="">
                                    <el-option v-for="item in repositoryList"
                                    :label="item.repositoryName"
                                    :value="item.repositoryId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <span>计划分销：</span>
                            <div class="select-w100">
                                <el-select ref="shopDefault" filterable v-model="header.shopId" placeholder="">
                                    <el-option v-for="item in applyOnlyShop"
                                    :label="item.depShopName"
                                    :value="item.depShopId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <span>单据备注：</span>
                            <div class="input-w100">
                            <el-input placeholder="" v-model="header.orderRemark"></el-input>
                            </div>
                        </li>
                    </ul>
                </div>
            </header>
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
                            <!-- 状态 -->
                            <p v-if='littleClassKey === "status"' v-text="getStatus(AllData[littleClassKey])">status</p>
                            <p v-else v-text="getComputed(littleClassKey, AllData, index)"></p>
                        </td>
                    </template>
                    <template v-else>
                        <td class="table-pull select-w80"><!-- 选中时才给table-pull后面写 -->
                              <template v-if="!operate.selectedRow[index]">
                                  <p>{{AllData[littleClassKey]}}</p>
                              </template>
                              <template v-else>
                                    <!-- 输入(这个容器划分输入框的限制内容) -->
                                    <template v-if="!noPullDown([littleClassKey])">
                                        <!-- 售价，倍率的联动 -->
                                        <template v-if="getSpecialComputed(littleClassKey)">
                                            <input-computed
                                                v-model="AllData[littleClassKey]"
                                                :costPrice = showData[index].costPrice
                                                :ratio = showData[index].ratio
                                                :soldPrice = showData[index].soldPrice
                                                :littleClassKey = littleClassKey
                                            ></input-computed>
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
                                            <!-- 只限制长度（当前值，最大输入值） -->
                                            <input v-if="limitInput[littleClassKey].onlyMaxlength" class="form-input" :maxlength="limitInput[littleClassKey].maxlength" v-model="AllData[littleClassKey]"></input>
                                        </template>
                                    </template>
                                    <!-- 下拉 -->
                                    <template v-else>
                                        <el-select filterable v-model="AllData[littleClassKey]" placeholder="">
                                            <el-option v-for="item in configPullDownData[littleClassKey]"
                                             :label="item.name || item.classesName || item.certificateName || item.name"
                                             :value="item.type || item.classesName || item.certificateName || item.name"
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
                <div>
                    <ul class="footer-operation" v-if="crudData.operationCut">
                        <template>       
                            <li class="confirm-btn-small" @click="appendRow">新增</li>
                            <li class="confirm-btn-small" @click="popup.copys = true">复制</li>
                            <li class="confirm-btn-small" @click="popup.amendings = true">批量修改</li>
                            <li class="confirm-btn-small" @click="crudData.operationCut = false">删除</li>
                            <li class="confirm-btn-small" @click="addSingleAll">保存</li>
                            <li class="confirm-btn-small red" @click="closeStorage">关闭</li>
                        </template>
                        <!-- <li v-else class="confirm-btn-small red" @click="closeStorage">关闭</li> -->
                    </ul>
                </div>
                <div class="footer-affirm" v-if="!crudData.operationCut">
                    <a href="javascript: null" class="cancel-btn-lg" @click="crudData.operationCut = true">取 消</a>
                    <a href="javascript: null" class="confirm-btn-lg" @click="batchDelect">删 除</a>
                </div>
            </footer>
            <!-- 弹窗 -->
            <div>
                <div class="dialog-w380">
                      <!-- 复制弹窗 -->
                      <el-dialog title="复制当前产品信息" v-model="popup.copys">
                          <section class="copy-body">
                              <p>复制行数</p>
                              <el-input
                                placeholder="请输入复制行数"
                                v-model="popup.copyNumber">
                              </el-input>
                          </section>
                          <span slot="footer">
                              <a href="javascript: null" class="cancel-btn-w120" @click="popup.copys = false">取 消</a>
                              <a href="javascript: null" class="confirm-btn-w120 m0" @click="copyRowData">确 定</a>
                          </span>
                      </el-dialog>
                </div>
                <!-- 批量修改 -->
                <div class="dialog-w420">
                    <el-dialog title="批量修改" v-model="popup.amendings">
                        <section>
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
                            <a href="javascript: null" class="cancel-btn-w120 mr70" @click="popup.amendings = false">取 消</a>
                            <a href="javascript: null" class="confirm-btn-w120 m0" @click="batchAmending">确 定</a>
                        </span>
                    </el-dialog>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Vue from 'vue'
import {mapActions, mapGetters} from 'vuex'
import { seekProductClassList, seekProductPropertyList, seekCertificateList } from './../../../Api/commonality/seek'
import {productDetailStatus} from './../../../Api/commonality/status'
import {delectProductDetail} from './../../../Api/commonality/operate'
import pureNumbers from "./../CommonalityComponent/input/pureNumbers.vue" // 纯数字
import integer from "./../CommonalityComponent/input/integer.vue" // 整数
import sliceNumber from "./../CommonalityComponent/input/sliceNumber.vue" // 小数
import inputComputed from "./inputComputed.vue"
export default {
    components: {
        inputComputed,
        pureNumbers,
        integer,
        sliceNumber
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
        operate: { // 操作的状态
            selectedRow: [], // 记录当前点中，待后期优化
            openState: true // 伸缩列表总开关按钮
        },
        rules2: { // 表单验证
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
        littleClass: [ // 小类
            { // 首饰名称
                "name": "首饰名称",
                "colspan": 4,
                "colspanOpen": 4,
                "englishName": "jewelryCategory",
                "admentUrl": "batchUpdateJewelryCategory",
                "stateOpen": "jewelryCategory", // 开和关状态
                "propertyDatas": [{
                    "name": "成色名称",
                    "englishName": "metalColor"
                }, {
                    "name": "含金量",
                    "englishName": "partGold"
                }, {
                    "name": "宝石名称",
                    "englishName": "gemName"
                }, {
                    "name": "首饰类别",
                    "englishName": "jewelryName"
                }],
                "open": [{
                    "name": "成色名称",
                    "englishName": "metalColor"
                }, {
                    "name": "含金量",
                    "englishName": "partGold"
                }, {
                    "name": "宝石名称",
                    "englishName": "gemName"
                }, {
                    "name": "首饰类别",
                    "englishName": "jewelryName"
                }]
            }, { // 基本信息
                "name": "基本信息",
                "colspan": 1,
                "colspanOpen": 4,
                "englishName": "baseInfo",
                "admentUrl": "batchUpdateBaseInfo",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                  "name": "品牌",
                  "englishName": "brand"
                }, {
                  "name": "款号",
                  "englishName": "modelNo"
                }, {
                  "name": "手寸",
                  "englishName": "handInch"
                }, {
                  "name": "手寸单位",
                  "englishName": "handInchUnit"
                }]
            }, { // 重量
                "name": "重量",
                "colspan": 1,
                "colspanOpen": 7,
                "englishName": "productWeight",
                "admentUrl": "batchUpdateProductWeight",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                    "name": "总件重（g）",
                    "englishName": "totalWeight"
                }, {
                    "name": "净金重（g）",
                    "englishName": "netWeight"
                }, {
                    "name": "含配金重（g）",
                    "englishName": "heavyCode"
                }, {
                    "name": "金耗（%）",
                    "englishName": "goldCost"
                }, {
                    "name": "金价（元）",
                    "englishName": "goldPrice"
                }, {
                    "name": "金属颜色",
                    "englishName": "goldColor"
                }, {
                    "name": "金料额（元）",
                    "englishName": "goldE"
                }]
            }, { // 证书
                "name": "证书",
                "colspan": 1,
                "colspanOpen": 10,
                "englishName": "certificate",
                "admentUrl": "batchUpdateCertifite",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                    "name": "证书号",
                    "englishName": "certifiNo"
                }, {
                    "name": "验证码",
                    "englishName": "authCode"
                }, {
                    "name": "证书名",
                    "englishName": "certifiName"
                }, { // 选择后生成的
                    "name": "检验机构",
                    "englishName": ""
                }, {
                    "name": "网址",
                    "englishName": ""
                }, {
                    "name": "电话",
                    "englishName": ""
                }, {
                    "name": "检测标准1",
                    "englishName": ""
                }, {
                    "name": "检测标准2",
                    "englishName": ""
                }, {
                    "name": "检测标准3",
                    "englishName": ""
                }, {
                    "name": "证书费（元）",
                    "englishName": "certifiFee"
                }]
            }, { // 主石
                "name": "主石",
                "colspan": 1,
                "colspanOpen": 15,
                "englishName": "deputyStoneZhu",
                "admentUrl": "batchUpdateDeputyStone",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                    "name": "主石名",
                    "englishName": "mainName"
                }, {
                    "name": "主石规格",
                    "englishName": "stand"
                }, {
                    "name": "主石粒数",
                    "englishName": "count"
                }, {
                    "name": "主石重",
                    "englishName": "mainWeight"
                }, {
                    "name": "主石单位",
                    "englishName": "unit"
                }, {
                    "name": "主石单价（元）",
                    "englishName": "unitPrice"
                }, {
                    "name": "计价方式",
                    "englishName": "mainCalcMethod"
                }, {
                    "name": "形状",
                    "englishName": "shape"
                }, {
                    "name": "颜色",
                    "englishName": "color"
                }, {
                    "name": "净度",
                    "englishName": "neatNess"
                }, {
                    "name": "切工",
                    "englishName": "blackout"
                }, {
                    "name": "抛光",
                    "englishName": "polishing"
                }, {
                    "name": "对称",
                    "englishName": "symmetry"
                }, {
                    "name": "荧光",
                    "englishName": "fluorescent"
                }, {
                    "name": "主石额",
                    "englishName": "mainPrice"
                }]
            }, { // 副石
                "name": "副石",
                "colspan": 1,
                "colspanOpen": 8,
                "englishName": "deputyStoneFus",
                "admentUrl": "batchUpdateDeputyStone",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                    "name": "副石名",
                    "englishName": "deputyName"
                }, {
                    "name": "副石规格",
                    "englishName": "deputyStand"
                }, {
                    "name": "副石粒数",
                    "englishName": "deputyCount"
                }, {
                    "name": "副石重",
                    "englishName": "deputyWeight"
                }, {
                    "name": "副石单位",
                    "englishName": "deputyUnit"
                }, {
                    "name": "副石单价（元）",
                    "englishName": "deputyUnitPrice"
                }, {
                    "name": "计价方式",
                    "englishName": "deputyCalcMethod"
                }, {
                    "name": "副石额（元）",
                    "englishName": "deputyPrice"
                }]
            }, { // 工费
                "name": "工费",
                "colspan": 1,
                "colspanOpen": 5,
                "englishName": "operateCost",
                "admentUrl": "batchUpdateOperateCost",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                    "name": "销售工费（元）",
                    "englishName": "soldFee"
                }, {
                    "name": "销售工费方式",
                    "englishName": "soldMethod"
                }, {
                    "name": "进货工费（元）",
                    "englishName": "inFee"
                }, {
                    "name": "进货工费方式",
                    "englishName": "inMethod"
                }, {
                    "name": "进货工费额（元）",
                    "englishName": "inMoney"
                }]
            }, { // 配件
                "name": "配件",
                "colspan": 1,
                "colspanOpen": 6,
                "englishName": "parts",
                "admentUrl": "batchUpdateParts",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                    "name": "配件名",
                    "englishName": "partName"
                }, {
                    "name": "配件数",
                    "englishName": "partCount"
                }, {
                    "name": "配件重（g）",
                    "englishName": "partWeight"
                }, {
                    "name": "单价（元）",
                    "englishName": "partPrice"
                }, {
                    "name": "计价方式",
                    "englishName": "calcMethod"
                }, {
                    "name": "配件额（元）",
                    "englishName": "price"
                }]
            }, { // 其它费用
                "name": "其它费用",
                "colspan": 1,
                "colspanOpen": 2,
                "englishName": "otherFee",
                "admentUrl": "batchUpdateOtherFee",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                    "name": "其它费用名",
                    "englishName": "otherFeeName"
                }, {
                    "name": "其它费用额（元）",
                    "englishName": "otherFee"
                }]
            }, { // 标价
                "name": "标价",
                "colspan": 1,
                "colspanOpen": 10,
                "englishName": "priceTag",
                "admentUrl": "batchUpdatePriceTag",
                "stateOpen": "", // 开和关状态
                "propertyDatas": [],
                "open": [{
                    "name": "金料额",
                    "englishName": ""
                }, {
                    "name": "证书费",
                    "englishName": ""
                }, {
                    "name": "主石额",
                    "englishName": ""
                }, {
                    "name": "副石额",
                    "englishName": ""
                }, {
                    "name": "工费额",
                    "englishName": ""
                }, {
                    "name": "配件额",
                    "englishName": ""
                }, {
                    "name": "其它费用额",
                    "englishName": ""
                }, {
                    "name": "成本",
                    "englishName": "costPrice"
                }, {
                    "name": "倍率",
                    "englishName": "ratio"
                }, {
                    "name": "售价",
                    "englishName": "soldPrice"
                }]
            }
        ],
        header: {
            currentProduct: null, // 当前的单据数据
            supplierId: null, // 供应商Id
            shopId: null, // 计划分销商
            warehouseId: null, // 入库库位Id
            orderRemark: null, // 单据备注
            excelUrl: null // 表格地址
        },
        getPullDownData: null,
        crudData: { // 增查更删数据
            checked: false, // 监听全选
            checkList: [], // 选择按钮 删除
            currentPage: 1, // 当前页
            operationCut: true, // 操作按钮切换
            newStockData: JSON.parse(sessionStorage.getItem("newStockData")), // 上一页的数据
            // saveDatas: null, // 保存了的数据
            newAddData: [], // 新增数据
            selectedData: [] // 选中数据
        },
        newDatas: JSON.parse(sessionStorage.getItem("newDatas")), // 上一步带过来的数据
        popup: { // 弹窗
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
            admentUrl: null // 批量修改的地址
        },
        aloneAmendingData: { // 独立修改(修改保存后的数据)
            initialValue: [], // 初始值
            changeValue: [] // 变化值
        },
        configPullDownData: null // 配置的下拉数据
    }
    },
  computed: {
    ...mapGetters([
        "rowConfigData", // 一行数据
        "configPullDownDataList", // 配置的下拉数据
        "limitInput", // 输入限制
        "getProductDetail", // 商品列表
        "supplierListData", // 供应商
        "applyDepartementList", // 计划分销商
        "repositoryList", // 库位列表
        "productClass", // 产品类别（商品类型列表）
        "receiptDetail", // 单据详情
        "applyOnlyShop" // 只有店铺
    ]),
    paginationSize: function () { // 分页的大小
        return parseInt(this.getProductDetail.totalNum) + this.crudData.newAddData.length;
    },
    allDatas: function () { // 总数据
        let getConfig = this.getconfigData(this.getProductDetail.rowDataList);
        return [...getConfig, ...this.crudData.newAddData]
        // return [...this.getProductDetail, ...this.crudData.newAddData]
    },
    showData: function () { // 存在一个大bug，如果中间不请求
        // let state = ((this.crudData.currentPage - 1) * 30);
        // let end = (this.crudData.currentPage * 30);
        // return this.allDatas.slice(state, end);
        // 获取最后一页的数量，并得到最后一页是哪一页
        return this.allDatas.slice(0, 30);
    },
    getCheckedPage: function () { // 监听当页的所有做选择的变量（删除选择）
        var CheckedPage = [];
        for (var i = 0; i < this.showData.length; i++) {
            CheckedPage.push(this.showData[i].productId)
        }
        return CheckedPage;
    }
    },
    watch: {
        'popup.amendingLargeClass': function () { // 切换大类别时，清空其它选项
            this.popup.amendingLittleClass = null;
            this.popup.amendingData = ""
        },
        'popup.amendingLittleClass': function () { // 切换小类别时，清空批量修改的输入内容
            this.popup.amendingData = "";
        },
        'receiptDetail': function () { // 监听单据详情
            this.$refs.supplierDefault.selectedLabel = this.receiptDetail.supplier; // 供应商
            this.header.supplierId = this.receiptDetail.supplierId; // 供应商Id
            this.$refs.warehouseDefault.selectedLabel = this.receiptDetail.warehouse; // 入库库位
            this.header.warehouseId = this.receiptDetail.warehouseId // 入库库位Id
            this.$refs.shopDefault.selectedLabel = this.receiptDetail.shop; // 计划分销商
            this.header.shopId = this.receiptDetail.shopId; // 计划分销商Id
            this.header.orderRemark = this.receiptDetail.orderRemark;
        },
        'crudData.checkList': function () {
            if (this.crudData.checkList.length === this.showData.length) { // 全选中
                console.log("全选进来了")
                this.crudData.checked = true;
            } else {
                console.log("全选消失了进来了")
                this.crudData.checked = false;
            }
        },
        'workProductDetail': function () { // 删除成功
            this.crudData.operationCut = true;
        }
    },
    created () { // 获取下拉数据
        this.header.orderRemark = this.receiptDetail.orderRemark;
        this.initData(); // 初始化数据（默认数据）
        this.workSupplierList(); // 获取供应商列表
        this.workApplyDepartement(); // 计划分销商
        this.workRepositoryList(); // 库位列表
        this.productClassList(); // 获取商品大小类的下拉列表
        this.productPropertyList(); // 商品属性下拉列表
        this.productCertificateList(); // 证书下拉列表
        this.judgeProductDetail(); // 如果没有商品列表重新获取一次(避免用户刷新)
        this.workReceiptDetail({
            "orderNumber": sessionStorage.getItem("orderNumber")
        }); // 单据详情
        this.getConfigInto(); // 初始化一些配置数据
    },
  methods: {
    ...mapActions([
        "workProductDetail", // 商品列表
        "workSupplierList", // 供应商列表
        "workApplyDepartement", // 计划分销商
        "workRepositoryList", // 库位列表
        // "delectProductDetail", // 删除商品
        "workReceiptDetail" // 单据详情
    ]),
    initData () { // 初始化数据（默认数据）
        this.header.currentProduct = this.receiptDetail.cargoType; // 当前的单据数据
        this.header.supplierId = this.receiptDetail.supplier; // 供应商Id
        this.header.shopId = this.receiptDetail.shop; // 计划分销商
        this.header.warehouseId = this.receiptDetail.warehouse; // 入库库位Id
        this.header.orderRemark = this.receiptDetail.orderRemark; // 单据备注
    },
    judgeProductDetail () { // 如果没有商品列表重新获取一次(避免用户刷新)
        if (!this.getProductDetail.rowDataList) {
            this.workProductDetail({
                "page": "1",
                "pageSize": "30",
                "objId": this.newDatas.orderNumber,
                // "objId": this.newDatas.orderNumber,
                "type": "1",
                "applyType": 4
            });
        }
    },
    handleSizeChange (val) {
        console.log(`每页 ${val} 条`);
    },
    handleCurrentChange (val) {
        var data = {
            "objId": this.newDatas.orderNumber,
            "type": "1",
            "applyType": "4",
            "page": val
        }
        this.workProductDetail(data);
        this.crudData.currentPage = val;
        console.log(`当前页: ${val}`);
    },
    getConfigInto () { // 初始化一些配置数据
        if (this.littleClassList) { // 初始化小类
            this.littleClass = this.littleClassList;
        }
        if (this.configPullDownDataList) { // 下拉数据模板
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
    cutSelectFun () { // 切换全选方法
        // function sortNumber (a, b) {
        //     return b - a;
        // }
        if (this.crudData.checked === true) { // 全选
              for (let i of this.getCheckedPage) {
                  this.crudData.checkList.push(i)
              }
              this.crudData.checkList = [...new Set(this.crudData.checkList)];
              console.log(this.crudData.checkList)
        } else { // 取消
            this.crudData.checkList = [];
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
    // ///////////   查询数据   /////////////
    // ///////////(获取下拉数据)//////////////
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
                      console.log("测试商品属性");
                      console.log(_self.configPullDownData);
                      console.log("测试商品属性");
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
    getStateOpen (parm, parm2) { // 大类过滤的开关
        if (parm2 !== "") {
          return parm.propertyDatas;
        } else {
          return false;
        }
    },
    clearExcess (largeClassKey, littleClassKey) { // 清掉后台多余数据  可以去掉
        if (largeClassKey === "deputyStoneFus") {
          let littleClassKeyFilter = new Set(['shape', 'color', 'neatness', 'blackout', 'polishing', 'symmetry', 'fluorescent']);
          let littleClass = [littleClassKey].filter(x => !littleClassKeyFilter.has(x));
          return littleClass[0];
        }
        return true;
    },
    getReadOnly (littleClassKey) { // 过滤只读值
        let onlyReady = new Set(['status', 'checkCode', 'supervisionCode', 'totalWeight', 'heavyCode', 'goldE', 'mainPrice', 'deputyPrice', 'inMoney', 'price', 'costPrice']);
        let onlyState = [littleClassKey].filter(x => onlyReady.has(x));
        if (onlyState.length > 0) {
          return true
        } else {
          return false
        }
    },
    getStringClass (littleClassKey) { // 筛选字符串
        let stringName = new Set(['modelNo', 'certifiNo', 'authCode', 'otherFeeName']); // 款号，证书号，验证码，其它费用名
        let getName = [littleClassKey].filter(x => stringName.has(x));
        if (getName.length > 0) {
        return true;
        } else {
        return false;
        }
    },
    computedData (littleClassKey, index) { // 进行计算操作
        let computedVal = null;
        switch (littleClassKey) { // 定位
            case "totalWeight": // 总件量
                // if (this.allDatas[index].totalWeight) { // 如果有值，直接拿来显示
                //     computedVal = this.allDatas[index].totalWeight
                // } else {
                //     computedVal = ((Number(this.allDatas[index].heavyCode) || 0) + (Number(this.allDatas[index].mainWeight) || 0) + (Number(this.allDatas[index].deputyWeight) || 0)).toFixed(3);
                // }
                 let mainWeight = 0;
                 let deputyWeight = 0;
                 if (this.allDatas[index].unit === "ct") { // 主石重
                    mainWeight = (Number(this.allDatas[index].mainWeight) || 0) * 0.2;
                 } else {
                    mainWeight = Number(this.allDatas[index].mainWeight);
                 }
                 if (this.allDatas[index].deputyUnit === "ct") { // 副石重
                    deputyWeight = (Number(this.allDatas[index].deputyWeight) || 0) * 0.2;
                 } else {
                    deputyWeight = Number(this.allDatas[index].deputyWeight);
                 }
                 computedVal = ((Number(this.allDatas[index].heavyCode) || 0) + mainWeight + deputyWeight).toFixed(3);
                break;
            case "heavyCode": // 含配金重 净金重(productWeight.netWeight) + 配件重(parts.dpartWeight)
                computedVal = ((Number(this.allDatas[index].netWeight) || 0) + (Number(this.allDatas[index].partWeight) || 0)).toFixed(3);
                break;
            case "goldE": // 金料额(1 + 金耗(goldCost)) * 金价(goldPrice)
                computedVal = (Number(this.allDatas[index].netWeight) * (1 + Number(this.allDatas[index].goldCost)) * Number(this.allDatas[index].goldPrice)).toFixed(2);
                break;
            case "mainPrice": // (主石额)
                if (this.allDatas[index].mainCalcMethod === "计件") {
                    computedVal = ((Number(this.allDatas[index].unitPrice) || 0) + (Number(this.allDatas[index].count) || 0)).toFixed(2);
                } else if (this.allDatas[index].mainCalcMethod === "计重") {
                    computedVal = ((Number(this.allDatas[index].unitPrice) || 0) + (Number(this.allDatas[index].mainWeight) || 0)).toFixed(2);
                }
                break;
            case "deputyPrice": // (副石额)
                if (this.allDatas[index].deputyCalcMethod === "计件") {
                    computedVal = ((Number(this.allDatas[index].deputyUnitPrice) || 0) * (Number(this.allDatas[index].deputyCount) || 0)).toFixed(2);
                } else if (this.allDatas[index].deputyCalcMethod === "计重") {
                    computedVal = ((Number(this.allDatas[index].deputyUnitPrice) || 0) * (Number(this.allDatas[index].deputyWeight) || 0)).toFixed(2);
                }
                break;
            case "inMoney": // 进货工费额
                if (this.allDatas[index].inMethod === "计件") {
                    computedVal = ((Number(this.allDatas[index].inFee)) || 0).toFixed(2);
                } else if (this.allDatas[index].inMethod === "计重") {
                    computedVal = (Number(this.allDatas[index].netWeight) * Number(this.allDatas[index].inFee)).toFixed(2);
                }
                break;
            case "price": // (配件额)
                if (this.allDatas[index].calcMethod === "计件") {
                  computedVal = (Number(this.allDatas[index].partCount) * Number(this.allDatas[index].partPrice)).toFixed(2);
                } else if (this.allDatas[index].calcMethod === "计重") {
                  computedVal = (Number(this.allDatas[index].partWeight) * Number(this.allDatas[index].partPrice)).toFixed(2);
                }
                break;
            case "costPrice": // 成本 （证书费没处理）
                computedVal = Math.round((Number(this.allDatas[index].goldE) || 0) + (Number(this.allDatas[index].certifiFee) || 0) + (Number(this.allDatas[index].mainPrice) || 0) + (Number(this.allDatas[index].deputyPrice) || 0) + (Number(this.allDatas[index].inMoney) || 0) + (Number(this.allDatas[index].price) || 0) + (Number(this.allDatas[index].otherFee) || 0));
                break;
            default:
                computedVal = this.allDatas[index][littleClassKey];
        }
        return computedVal;
    },
    getComputed (littleClassKey, AllData, index) { // 得到计算值
        var Result = this.computedData(littleClassKey, index);
        AllData[littleClassKey] = Result;
        return Result;
    },
    // ///////////   核心过滤器   /////////////
    getStatus (parm) { // 审核状态
        return productDetailStatus(parm);
    },
    getLittleClassKey (parm) { // 开关过滤器
        var getOpenName = ['barcode', 'status', 'remark'];
        for (let i = 0; i < this.littleClass.length; i++) {
            if (this.littleClass[i].stateOpen) {
                for (let j = 0; j < this.littleClass[i].open.length; j++) {
                    if (this.littleClass[i].open[j].englishName !== "") {
                        getOpenName.push(this.littleClass[i].open[j].englishName)
                    }
                }
            }
        }
        // getOpenName.push('remark');
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
          return arr[0].open;
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
        fileData.append("uesrId", sessionStorage.getItem("id")); // 用户id
        fileData.append("userName", sessionStorage.getItem("nickname")); // 用户名
        fileData.append("companyId", sessionStorage.getItem("companyId")); // 公司id
        // ////////
        fileData.append("handleDeptId", this.crudData.newStockData.handleDeptId); // 操作部门ID
        fileData.append("productType", this.crudData.newStockData.classesId); // 产品类别ID
        fileData.append("supplierId", this.header.supplierId); // 供应商ID
        fileData.append("shopId", this.header.shopId); // 计划分销商ID
        fileData.append("warehouseId", this.header.warehouseId); // 入库库位ID
        // ///////
        fileData.append("operateType", 1); // 操作单位类型
        fileData.append("remark", this.header.orderRemark); // 单据备注
        // 第一页获得的数据
        var url = INTERFACE_URL_9083 + ":9083/v1/warehouse/uploadWarehouseByExcel"
        this.$http.post(url, fileData).then((response) => {
            console.log(response);
             this.$refs.tableList.value = null;
             if (response.data.state === 200) {
                alert("上传表格成功")
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
        // 如果为备份两个值,一个为刚选中是的数据，一个为变化数据，如果两个数据里的某个值不同，就证明那个值改变了。抽出来（key）给后台
        if (parm.productId) { // 如果有条形码
            this.recordFun(parm);
        }
    },
    recordFun (parm) { // 记录变化
        var initialValue = JSON.stringify(parm);
        if (this.aloneAmendingData.initialValue.length < 1) { // 初始值为空时,直接添加
          this.aloneAmendingData.changeValue.push(parm);
          this.aloneAmendingData.initialValue.push(JSON.parse(initialValue));
        } else { // 初始值不为空时,得判断
            var isNew = true;
            for (var i = 0; i < this.aloneAmendingData.changeValue.length; i++) {
                // 引用类型，改变它里面单个值就直接影响到其它值，如果清空整个值，就不会影响
                if (this.aloneAmendingData.changeValue[i] === parm) {
                    isNew = false;
                }
            }
            if (isNew) {
                this.aloneAmendingData.changeValue.push(parm);
                this.aloneAmendingData.initialValue.push(JSON.parse(initialValue));
            }
        }
        console.log(this.aloneAmendingData.initialValue);
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
      var amendingLittleClass = this.popup.amendingLittleClass
        if (this.popup.amendingPitchOn === "2") { // 全选
            for (let i = 0; i < this.showData.length; i++) {
                if (!this.aloneAmendingData.changeValue.includes(this.showData[i])) { // 如果变化的值没有收集有这个值，就再收集一次
                    let initialValue = JSON.stringify(this.allDatas[i]);
                    this.aloneAmendingData.changeValue.push(this.allDatas[i]);
                    this.aloneAmendingData.initialValue.push(JSON.parse(initialValue)); // 晚了一步
                }
                this.allDatas[i][amendingLittleClass] = this.popup.amendingData;
            }
        } else { // 自定义选择
            let amendingList = [];
            for (var i = this.popup.amendingStartRow - 1; i < this.popup.amendingEndRow; i++) {
                if (!this.aloneAmendingData.changeValue.includes(this.showData[i])) { // 如果变化的值没有收集有这个值，就再收集一次
                    let initialValue = JSON.stringify(this.showData[i]);
                    this.aloneAmendingData.changeValue.push(this.showData[i]);
                    this.aloneAmendingData.initialValue.push(JSON.parse(initialValue)); // 晚了一步
                }
                this.allDatas[i][amendingLittleClass] = this.popup.amendingData;
                amendingList.push(this.allDatas[i]);
            }
        }
        this.popup.amendings = false;
    },
    batchDelect () { // 删除
        // 删除成功后，再提示要不要保存新增数据(如果要，就直接保存新增的，不要，就把刚才新建的给删除)
        // 第一个判断，是不是只删除新增的，如果是，就直接删除
        // 第二个判断，是不是删除里有保存了的，是，就请求后台，成功后，就把新增选中的删除，然后判断还有新增数据，如果有就询问顾客要不要保存之前新增的
        // var delectList = null;
        var productIdList = [];
        for (var i = 0; i < this.crudData.checkList.length; i++) {
            var oneObject = {};
            oneObject.productId = this.crudData.checkList[i];
            productIdList.push(oneObject);
        }
        let data = {
            "orderNumber": this.newDatas.orderNumber,
            "productIdList": productIdList
        }
        delectProductDetail(data).then((response) => {
            console.log("商品操作（删）")
            console.log(response)
            console.log("商品操作（删）")
            if (response.data.state === 200) {
                this.workProductDetail({
                    "page": "1",
                    "pageSize": "30",
                    "objId": this.newDatas.orderNumber,
                    "type": "1",
                    "applyType": 4
                });
            } else {
                alert(response.data.msg)
            }
        }, (response) => {
            console.log(response);
        })
    },
    admendFun (initialValue, changeValue) { // 修改商品列表
        let admendList = [];
        sessionStorage.setItem("AAA", JSON.stringify(initialValue));
        sessionStorage.setItem("BBB", JSON.stringify(changeValue));
        for (let i = 0; i < initialValue.length; i++) {
            var admendObj = {};
            for (let initialKey in initialValue[i]) {
                if (initialValue[i][initialKey] !== changeValue[i][initialKey]) {
                    admendObj[initialKey] = changeValue[i][initialKey];
                    if (changeValue[i].productId && !admendObj.productId) { // 添加个商品id
                        admendObj.productId = changeValue[i].productId;
                    }
                }
            }
            admendList.push(admendObj);
        }
        var data = {
            "data": {
                "orderNum": this.newDatas.orderNumber, // 单据号
                "confirmType": "",
                "alterList": admendList
            },
            "unit": {
                "userId": sessionStorage.getItem("id"),
                "companyId": sessionStorage.getItem("companyId"),
                "channel": 3,
                "os": "string", // 手机为当前手机使用系统与版本号、WEB为浏览器系统与版本号
                "tokenId": sessionStorage.getItem("tokenId")
            }
        }
        sessionStorage.setItem("xiugai", JSON.stringify(data))
        var url = INTERFACE_URL_9083 + "/v1/goods/updateGoods";
        Vue.http.post(url, data).then((response) => { // 单据列表（公共数据）
            if (response.data.state === 200) {
                this.aloneAmendingData.initialValue = [];
                this.aloneAmendingData.changeValue = [];
                alert("修改成功")
            } else {
                alert(response.data.msg)
            }
            console.log("修改商品");
            console.log(response);
            console.log("修改商品");
        }, (response) => {
            console.log(response);
        })
    },
    addSingleAll () { // 保存（单个添加/批量添加）
        console.log("进来了");
        console.log(this.aloneAmendingData.initialValue);
        if (this.aloneAmendingData.initialValue.length > 0) { // 如果有变化，就调用修改
            console.log("youdianbbia")
            this.admendFun(this.aloneAmendingData.initialValue, this.aloneAmendingData.changeValue);
        }
        if (this.crudData.newAddData.length > 0) { // 全新添加
            this.brandNewAdd();
        }
    },
    brandNewAdd (parm) { // 继续添加
        let data = {
            "data": {
                "type": 2, // 1 无单据号，新增单据 2已存在单据
                "orderNumber": this.receiptDetail.orderNum, // 单据号
                "handleDeptId": this.receiptDetail.handleDeptId, // 操作部门ID
                "productTypeId": this.receiptDetail.productTpyeName, // 产品类别名（这里特别点）
                "supplierId": this.receiptDetail.supplierId, // 供应商ID  productTypeId
                "shopId": this.receiptDetail.shopId, // 计划分销商ID
                "warehouseId": this.receiptDetail.warehouseId, // 入库库位ID
                "orderRemark": this.receiptDetail.orderRemark, // 单据备注
                // "status": null, // 单据状态：1 未审核 2 审核中 3 已审核 （请求操作类型为  单据号）
                "rowDataList": this.crudData.newAddData // 新增数据集合
            },
            "unit": {
                "userId": sessionStorage.getItem("id"),
                "companyId": sessionStorage.getItem("companyId"),
                "channel": 3,
                "os": "string",
                "tokenId": sessionStorage.getItem("tokenId")
            }
        }
        console.log("继续新加入库");
        // sessionStorage.setItem("qqqqqqqqqqqqqqq", JSON.stringify(data));
        console.log(data);
        console.log("继续新加入库");
        let url = INTERFACE_URL_9083 + "/v1/warehouse/operateOWhourse";
        this.$http.post(url, data).then((response) => {
            console.log(response);
            if (response.data.state === 200) {
                this.operate.selectedRow = []; // 记录当前点中，待后期优化
                this.crudData.newAddData = []; // 新增数据
                this.workProductDetail({
                    "objId": this.newDatas.orderNumber,
                    "type": "1",
                    "applyType": "4"
                })
                alert("保存成功");
            } else {
                console.log("继续添加失败")
            }
        }, (response) => {
            console.log(response);
        })
    },
    continueAdd () { // 继续添加（已经保存的情况下）
        const checkUrl = "http://119.29.138.104:9083/v1/product/kPutOrderDetail/addPutOrderDetailByWeb/" + sessionStorage.getItem("tokenId");
        this.$http.put(checkUrl, this.crudData.newAddData).then((response) => {
            console.log(response);
            if (response.data.state === 200) {
                var succeedSave = response.data.data;
                this.allDatas = [...this.allDatas, ...succeedSave];
            }
        }, (response) => {
            alert('请求失败!!!');
        })
    },
    closeStorage () { // 关闭
        this.$router.push('/work/storage');
    }
    // //////////////  测试代码  ////////////////////
  }
}
</script>
<style lang="scss">
$oa: #0abfab; // 主色
$d6: #d6d6d6; // 分割线颜色
$c3: #333333; // 导航栏文字颜色
$c2: #333742; // 标签栏底色
$es: #esfsfd; // 标签设置编辑文本 出现旋转框
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
.copy-body{ // 复制弹窗
    height: 104px;
    p{
        height: 20px;
        font-size: 14px;
        margin-bottom: 10px;
    }
}
.amending-select{ // 修改弹窗修改内容
    font-size: 0;
    margin-bottom: 30px;
    li:nth-child(odd){
        width: 150px;
        color: $c3;
        // margin-right: 20px;
    }
    li{
      margin-top: 20px;
        font-size: 14px;
        height: 30px;
        line-height: 30px;
        // border: 1px solid red;
        display: inline-block;
    }
}
.amending-scope{ // 修改的范围
    li{
        margin-top: 30px;
        height: 30px;
        line-height: 30px;
        display: inline-block;
    }
    .all-check{
        display: block;
        margin-bottom: 36px;
    }
}
.new-storage{
    // .table-template{
    //     min-width: 100%;
    //     height: 100%;
    //     // background-color: #f1f1f1;
    //     table{
    //         width: 100%;
    //         height: 100%;
    //         .tr-header{
    //             background-color: $e8!important;
    //             &:hover{
    //                 background-color: $e8;
    //             }
    //             .header-state{
    //                 position: absolute;
    //                 top: 0;
    //                 left: 0;
    //                 width: 100%;
    //                 height: 100%;
    //                 li:first-child{
    //                     background-color: $e8;
    //                 }
    //                 li{
    //                     height: 40px;
    //                     line-height: 40px;
    //                     border-bottom: 1px solid $d6;
    //                     background-color: #fff;
    //                 }
    //             }
    //         }
    //         tr{
    //             height: 40px;
    //             width: 100%;
    //         }
    //         tr:nth-child(odd){
    //             background-color: #fff;
    //         }
    //         tr:nth-child(even){
    //             background-color: $f6;
    //         }
    //         tr{
    //             &:hover{
    //                 background-color: $e6;
    //             }
    //         }
    //         th,td{
    //             padding: 10px;
    //             text-align: center;
    //             border-right: 1px solid $d6;
    //             border-bottom: 1px solid $d6;
    //             font-size: 12px;
    //             min-width: 70px;
    //             height: 40px;
    //             overflow: hidden;
    //             white-space:nowrap;
    //             overflow: hidden;
    //             text-overflow: ellipsis;
    //         }
    //         td:last-child{
    //             border-right: 0;
    //         }
    //         th{
    //             color: $c6;
    //             position: relative;
    //             span{
    //                 vertical-align: middle;
    //                 margin-right: 10px;
    //             }
    //             .open-icon{
    //                 vertical-align: middle;
    //                 position: absolute;
    //                 right: 0;
    //                 top: -10px;
    //                 padding-top: 10px;
    //                 width: 20px;
    //                 margin-top: 14px;
    //                 cursor: pointer;
    //             }
    //         }
    //         td{
    //             color: $c3;
    //         }
    //         .table-pull{
    //             padding: 0!important;
    //         }
    //     }
    // }
}
</style>