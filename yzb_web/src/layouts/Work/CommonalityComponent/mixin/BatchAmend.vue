<template>
    <!-- 批量修改 -->
    <div class="dialog-w420">
        <el-dialog title="批量修改" v-model="popupBatchAmend.amendings">
            <section class="p-lr-30">
                <div class="rectangle-icon">
                    <i></i>
                    <span>请选择需要修改的内容</span>
                </div>
                <ul class="amending-select">
                    <li>请选择大类别：</li>
                    <li class="select-w200">
                        <el-select filterable v-model="popupBatchAmend.amendingLargeClass" placeholder="大类别">
                        <el-option v-for="item in littleClass"
                        :label="item.name"
                        :value="item.englishName">
                       <!--  <span @click="popupBatchAmend.admentUrl = item.admentUrl">{{item.name}}</span> -->
                        </el-option>
                        </el-select>
                    </li>
                    <li>请选择小类别：</li>
                    <li class="select-w200">
                        <el-select filterable ref='littleClass' v-model="popupBatchAmend.amendingLittleClass" placeholder="小类别">
                        <el-option v-for="item in getLittleClass(littleClass, popupBatchAmend.amendingLargeClass)"
                        :label="item.name"
                        :value="item.englishName">
                        </el-option>
                        </el-select>
                    </li>
                    <li>请选择或输入内容：</li>
                    <li class="select-w200">
                        <div v-if="!getAmendData(popupBatchAmend.amendingLittleClass)" class="input-w200">           
                            <el-input v-model="popupBatchAmend.amendingData"></el-input>
                        </div>
                        <el-select filterable v-else v-model="popupBatchAmend.amendingData">
                            <el-option v-for="item in getAmendData(popupBatchAmend.amendingLittleClass)"
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
                    <el-radio-group v-model="popupBatchAmend.amendingPitchOn">
                        <li>                                        
                            <el-radio :label='popupBatchAmend.amendingCustomRow'>调整行范围：</el-radio>
                        </li>
                        <li>
                            <div class="input-w100">
                                <el-input v-model="popupBatchAmend.amendingStartRow" :disabled='popupBatchAmend.amendingPitchOn === "2"'></el-input>
                            </div>
                            <span class="m-lr-10 font14">至</span>
                            <div class="input-w100">
                                <el-input v-model="popupBatchAmend.amendingEndRow" :disabled='popupBatchAmend.amendingPitchOn === "2"'></el-input>
                            </div>
                        </li>
                        <li class="all-check">
                            <el-radio :label='popupBatchAmend.amendingAllRow'>全选</el-radio>
                        </li>
                    </el-radio-group>
                </ul>
            </section>
            <span slot="footer">
                <a href="javascript: null" class="cancel-btn-w120 mr70" @click="popupBatchAmend.amendings = false">取 消</a>
                <a href="javascript: null" class="confirm-btn-w120 m0" @click="batchAmending">确 定</a>
            </span>
        </el-dialog>
    </div>
</template>
<script>
    export default {
        data () {
            return {
                popupBatchAmend: {
                    "amendings": null, // 弹窗开关
                    "amendingLargeClass": null, // 大类别
                    "amendingLittleClass": null, // 小类别
                    "amendingData": null, // 修改的内容
                    "amendingCustomRow": "1", // 批量修改的自定义输入 // 单选框
                    "amendingAllRow": "2", // 批量修改的全选行数  // 单选框
                    "amendingPitchOn": "1", // 批量修改的全选或自定义输入 // 单选框汇总
                }
                // littleClass: [ // 小类
                //     { // 首饰名称
                //       "name": "首饰名称",
                //       "colspan": 4,
                //       "colspanOpen": 4,
                //       "englishName": "jewelryCategory",
                //       "admentUrl": "batchUpdateJewelryCategory",
                //       "stateOpen": "jewelryCategory", // 开和关状态
                //       "propertyDatas": [{
                //           "name": "成色名称",
                //           "englishName": "metalColor"
                //       }, {
                //           "name": "含金量",
                //           "englishName": "partGold"
                //       }, {
                //           "name": "宝石名称",
                //           "englishName": "gemName"
                //       }, {
                //           "name": "首饰类别",
                //           "englishName": "jewelryName"
                //       }],
                //       "open": [{
                //           "name": "成色名称",
                //           "englishName": "metalColor"
                //       }, {
                //           "name": "含金量",
                //           "englishName": "partGold"
                //       }, {
                //           "name": "宝石名称",
                //           "englishName": "gemName"
                //       }, {
                //           "name": "首饰类别",
                //           "englishName": "jewelryName"
                //       }]
                //     }, { // 基本信息
                //       "name": "基本信息",
                //       "colspan": 1,
                //       "colspanOpen": 4,
                //       "englishName": "baseInfo",
                //       "admentUrl": "batchUpdateBaseInfo",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //         "name": "品牌",
                //         "englishName": "brand"
                //       }, {
                //         "name": "款号",
                //         "englishName": "modelNo"
                //       }, {
                //         "name": "手寸",
                //         "englishName": "handInch"
                //       }, {
                //         "name": "手寸单位",
                //         "englishName": "handInchUnit"
                //       }]
                //     }, { // 重量
                //       "name": "重量",
                //       "colspan": 1,
                //       "colspanOpen": 7,
                //       "englishName": "productWeight",
                //       "admentUrl": "batchUpdateProductWeight",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //           "name": "总件重（g）",
                //           "englishName": "totalWeight"
                //       }, {
                //           "name": "净金重（g）",
                //           "englishName": "netWeight"
                //       }, {
                //           "name": "含配金重（g）",
                //           "englishName": "heavyCode"
                //       }, {
                //           "name": "金耗（%）",
                //           "englishName": "goldCost"
                //       }, {
                //           "name": "金价（元）",
                //           "englishName": "goldPrice"
                //       }, {
                //           "name": "金属颜色",
                //           "englishName": "goldColor"
                //       }, {
                //           "name": "金料额（元）",
                //           "englishName": "goldE"
                //       }]
                //     }, { // 证书
                //       "name": "证书",
                //       "colspan": 1,
                //       "colspanOpen": 10,
                //       "englishName": "certificate",
                //       "admentUrl": "batchUpdateCertifite",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //           "name": "证书号",
                //           "englishName": "certifiNo"
                //       }, {
                //           "name": "验证码",
                //           "englishName": "authCode"
                //       }, {
                //           "name": "证书名",
                //           "englishName": "certifiName"
                //       }, { // 选择后生成的
                //           "name": "检验机构",
                //           "englishName": ""
                //       }, {
                //           "name": "网址",
                //           "englishName": ""
                //       }, {
                //           "name": "电话",
                //           "englishName": ""
                //       }, {
                //           "name": "检测标准1",
                //           "englishName": ""
                //       }, {
                //           "name": "检测标准2",
                //           "englishName": ""
                //       }, {
                //           "name": "检测标准3",
                //           "englishName": ""
                //       }, {
                //           "name": "证书费（元）",
                //           "englishName": "certifiFee"
                //       }]
                //     }, { // 主石
                //       "name": "主石",
                //       "colspan": 1,
                //       "colspanOpen": 15,
                //       "englishName": "deputyStoneZhu",
                //       "admentUrl": "batchUpdateDeputyStone",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //           "name": "主石名",
                //           "englishName": "mainName"
                //       }, {
                //           "name": "主石规格",
                //           "englishName": "stand"
                //       }, {
                //           "name": "主石粒数",
                //           "englishName": "count"
                //       }, {
                //           "name": "主石重",
                //           "englishName": "mainWeight"
                //       }, {
                //           "name": "主石单位",
                //           "englishName": "unit"
                //       }, {
                //           "name": "主石单价（元）",
                //           "englishName": "unitPrice"
                //       }, {
                //           "name": "计价方式",
                //           "englishName": "mainCalcMethod"
                //       }, {
                //           "name": "形状",
                //           "englishName": "shape"
                //       }, {
                //           "name": "颜色",
                //           "englishName": "color"
                //       }, {
                //           "name": "净度",
                //           "englishName": "neatNess"
                //       }, {
                //           "name": "切工",
                //           "englishName": "blackout"
                //       }, {
                //           "name": "抛光",
                //           "englishName": "polishing"
                //       }, {
                //           "name": "对称",
                //           "englishName": "symmetry"
                //       }, {
                //           "name": "荧光",
                //           "englishName": "fluorescent"
                //       }, {
                //           "name": "主石额",
                //           "englishName": "mainPrice"
                //       }]
                //     }, { // 副石
                //       "name": "副石",
                //       "colspan": 1,
                //       "colspanOpen": 8,
                //       "englishName": "deputyStoneFus",
                //       "admentUrl": "batchUpdateDeputyStone",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //           "name": "副石名",
                //           "englishName": "deputyName"
                //       }, {
                //           "name": "副石规格",
                //           "englishName": "deputyStand"
                //       }, {
                //           "name": "副石粒数",
                //           "englishName": "deputyCount"
                //       }, {
                //           "name": "副石重",
                //           "englishName": "deputyWeight"
                //       }, {
                //           "name": "副石单位",
                //           "englishName": "deputyUnit"
                //       }, {
                //           "name": "副石单价（元）",
                //           "englishName": "deputyUnitPrice"
                //       }, {
                //           "name": "计价方式",
                //           "englishName": "deputyCalcMethod"
                //       }, {
                //           "name": "副石额（元）",
                //           "englishName": "deputyPrice"
                //       }]
                //     }, { // 工费
                //       "name": "工费",
                //       "colspan": 1,
                //       "colspanOpen": 5,
                //       "englishName": "operateCost",
                //       "admentUrl": "batchUpdateOperateCost",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //           "name": "销售工费（元）",
                //           "englishName": "soldFee"
                //       }, {
                //           "name": "销售工费方式",
                //           "englishName": "soldMethod"
                //       }, {
                //           "name": "进货工费（元）",
                //           "englishName": "inFee"
                //       }, {
                //           "name": "进货工费方式",
                //           "englishName": "inMethod"
                //       }, {
                //           "name": "进货工费额（元）",
                //           "englishName": "inMoney"
                //       }]
                //     }, { // 配件
                //       "name": "配件",
                //       "colspan": 1,
                //       "colspanOpen": 6,
                //       "englishName": "parts",
                //       "admentUrl": "batchUpdateParts",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //           "name": "配件名",
                //           "englishName": "partName"
                //       }, {
                //           "name": "配件数",
                //           "englishName": "partCount"
                //       }, {
                //           "name": "配件重（g）",
                //           "englishName": "partWeight"
                //       }, {
                //           "name": "单价（元）",
                //           "englishName": "partPrice"
                //       }, {
                //           "name": "计价方式",
                //           "englishName": "calcMethod"
                //       }, {
                //           "name": "配件额（元）",
                //           "englishName": "price"
                //       }]
                //     }, { // 其它费用
                //       "name": "其它费用",
                //       "colspan": 1,
                //       "colspanOpen": 2,
                //       "englishName": "otherFee",
                //       "admentUrl": "batchUpdateOtherFee",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //           "name": "其它费用名",
                //           "englishName": "otherFeeName"
                //       }, {
                //           "name": "其它费用额（元）",
                //           "englishName": "otherFee"
                //       }]
                //     }, { // 标价
                //       "name": "标价",
                //       "colspan": 1,
                //       "colspanOpen": 10,
                //       "englishName": "priceTag",
                //       "admentUrl": "batchUpdatePriceTag",
                //       "stateOpen": "", // 开和关状态
                //       "propertyDatas": [],
                //       "open": [{
                //           "name": "金料额",
                //           "englishName": ""
                //       }, {
                //           "name": "证书费",
                //           "englishName": ""
                //       }, {
                //           "name": "主石额",
                //           "englishName": ""
                //       }, {
                //           "name": "副石额",
                //           "englishName": ""
                //       }, {
                //           "name": "工费额",
                //           "englishName": ""
                //       }, {
                //           "name": "配件额",
                //           "englishName": ""
                //       }, {
                //           "name": "其它费用额",
                //           "englishName": ""
                //       }, {
                //           "name": "成本",
                //           "englishName": "costPrice"
                //       }, {
                //           "name": "倍率",
                //           "englishName": "ratio"
                //       }, {
                //           "name": "售价",
                //           "englishName": "soldPrice"
                //       }]
                //     }
                // ]
            }
        }
    }
</script>