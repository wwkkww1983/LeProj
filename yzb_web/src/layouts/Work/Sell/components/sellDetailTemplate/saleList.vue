<template>
    <div class="sell-template">
        <ul class="sell-template-top">
            <el-form :model="ruleForm" :rules="rules" ref="ruleForm" class="demo-ruleForm">
            <li>
                <span class="main-color">产品名称：</span>
                <span class="main-color">{{watchProductList[0].jewelryName}}</span>
            </li>
            <li>
                <span class="main-color">条形码：</span>
                <span>{{watchProductList[0].barcode}}</span>
            </li>
            <li>
                <span class="main-color">销售金价：</span>
                <div class="w320">
                    <!-- <el-input v-if="operationProduct" type="text" v-model.number="watchProductList[0].saleGoldPrice"></el-input>-->
                    <el-form-item v-if="operationProduct" prop="saleGoldPrice">
                        <el-input v-model.number="ruleForm.saleGoldPrice"></el-input>
                    </el-form-item>
                    <div v-else>{{watchProductList[0].saleGoldPrice + "g/元"}}</div>
                </div>
            </li>
            <li>
                <span class="main-color">折扣：</span>
                <div class="w320">
                    <el-form-item v-if="operationProduct" prop="discount">
                        <el-input v-model.number="ruleForm.discount" ></el-input>
                    </el-form-item>
                    <div v-else>{{watchProductList[0].discount}}</div>
                </div>
            </li>
            <li>
                <span class="main-color">手工费：</span>
                <div class="w320">
                    <el-form-item v-if="operationProduct" prop="paymentPrice">
                        <el-input v-model.number="ruleForm.paymentPrice" ></el-input>
                    </el-form-item>
                    <div v-else>{{value2 ? watchProductList[0].paymentPrice + '元' : watchProductList[0].paymentPrice + 'g/元'}}</div>
                </div>
                <el-switch v-model="value2" on-text="件" v-show="operationProduct" off-text="重" on-color="#199ED8" off-color="#199ED8"></el-switch>
            </li>
            <li>
                <span class="main-color">实售价：</span>
                <div class="w320">
                    <el-form-item v-if="operationProduct" prop="newPrice">
                        <el-input v-model.number="ruleForm.newPrice" ></el-input>
                    </el-form-item>
                    <div v-else>{{watchProductList[0].newPrice}}</div>
                </div>
            </li>
            <li>
                {{watchProductList[0]}}
            </li>
            </el-form>
        </ul>
        <div class="sell-template-bottom">
            <h5>产品参数</h5>
            <ul class="one">
                <li>
                    <span>证书号：</span>
                    <span>{{watchProductList[0].certifiNo}}</span>
                </li>
            </ul>
            <ul class="two">
                <li>
                    <span>宝石名称：</span>
                    <span>{{watchProductList[0].jewelName}}</span>
                </li>
                <li>
                    <span>首饰名称：</span>
                    <span>{{watchProductList[0].jewelryName}}</span>
                </li>
                
                <li>
                    <span>成色名称：</span>
                    <span>{{watchProductList[0].colorName}}</span>
                </li>
            </ul>
            <ul class="three">
                <li>
                    <span>总件重：</span>
                    <span>{{watchProductList[0].totalWeight}}</span>
                </li>
                <li>
                    <span>金重：</span>
                    <span>{{watchProductList[0].goldWeight}}</span>
                </li>
            </ul>
        </div>
    </div>
</template>
<script>
// import {operateAddSale} from './../../../Api/commonality/operate'
import {seekgetGoldPrice} from './../../../../../Api/commonality/seek'
export default {
    props: [
        "operationProduct",
        "showProductList",
        "saveBtn"
    ],
    data () {
        var validateInput = (rule, value, callback) => {
            if (!value) {
                return callback(new Error('不能为空'));
            }
            setTimeout(() => {
                if (!Number.isInteger(value)) {
                    console.log(1111111111)
                    console.log(this.todayGoldPrice)
                    callback(new Error('请输入数字值'));
                }
                if (value < this.todayGoldPrice.lowestPrice) {
                    callback(new Error('低于最低金价'));
                }
                return callback();
            }, 1000);
        };
        return {
            "watchProductList": "", // 显示的数据
            "amendData": { // 修改后的值
                "modifyType": {
                        // "01": "", // 销售金价
                        // "02": "", // 手工费
                        // "03": "", // 折扣
                        // "04": "", // 实际售价
                        // "05": "", // 成色名称ID
                        // "06": "", // 宝石名称ID
                        // "07": "", // 首饰名称ID
                        // "08": "", // 产品类别ID
                        // "09": "", // 产品属性
                        // "10": "", // 回购价
                        // "11": "", // 磨损率
                        // "12": "", // 回收金重
                        // "13": "", // 计价方式
                        // "14": "", // 回收总件重
                        // "15": "" // 估价
                    },
                    "dataType": 1, // 字符类型1String2Double
                    "objectData": "" // 数据值 09 – 1 成品 2旧料13 – 1 计重 2 计件
                },
            "ruleForm": {
                "saleGoldPrice": 0,
                "discount": 0,
                "paymentPrice": 0,
                "newPrice": 0
            },
            "rules": {
                "saleGoldPrice": [
                    { "validator": validateInput, "trigger": 'blur' }
                ],
                "discount": [
                    { "validator": validateInput, "trigger": 'blur' }
                ],
                "paymentPrice": [
                    { "validator": validateInput, "trigger": 'blur' }
                ],
                "newPrice": [
                    { "validator": validateInput, "trigger": 'blur' }
                ]
            },
            "value2": false, // 选择计重还是计件
            "btn": {
                "show": false
            },
            "todayGoldPrice": {}
        }
    },
    created () {
        this.initData();
        this.initInput();
    },
    computed: {
        // xiaoxiao: function () {
        //     this.watchProductList[0].saleGoldPrice = this.ruleForm.saleGoldPrice
        //     return this.watchProductList[0].saleGoldPrice;
        // }
    },
    watch: {
        'operationProduct': function () {
            if (this.operationProduct === false) {
                // this.initData();
            }
        },
        'saveBtn': function () {
            if (this.saveBtn === true) {
                this.saveDataFun();
                console.log("点击了保存");
            }
        },
        'ruleForm.saleGoldPrice': function () {
            this.watchProductList[0].saleGoldPrice = this.ruleForm.saleGoldPrice
        },
        'ruleForm.discount': function () {
            this.watchProductList[0].discount = this.ruleForm.discount
            // if (this.value2 === true) {
            //     console.log("计件1")
            //     this.ruleForm.newPrice = this.ruleForm.saleGoldPrice + 1
            //     // this.ruleForm.newPrice = (this.watchProductList[0].newPrice / (this.watchProductList[0].saleGoldPrice * this.watchProductList[0].goldWeight + this.watchProductList[0].paymentPrice)).toFixed(3)
            // } else {
            //     console.log("记重")
            //     this.ruleForm.newPrice = (this.watchProductList[0].newPrice / (this.watchProductList[0].goldWeight * (this.watchProductList[0].saleGoldPrice + this.watchProductList[0].paymentPrice))).toFixed(3)
            // }
        },
        'ruleForm.paymentPrice': function () {
            this.watchProductList[0].paymentPrice = this.ruleForm.paymentPrice
        },
        'ruleForm.newPrice': function () {
            this.watchProductList[0].newPrice = this.ruleForm.newPrice
            if (this.value2 === true) {
                // console.log("计件")
                this.ruleForm.discount = ((this.watchProductList[0].saleGoldPrice * this.watchProductList[0].goldWeight + this.watchProductList[0].paymentPrice) * this.watchProductList[0].discount).toFixed(3)
            } else {
                this.ruleForm.discount = ((this.watchProductList[0].goldWeight * (this.watchProductList[0].saleGoldPrice + this.watchProductList[0].paymentPrice)) * this.watchProductList[0].discount).toFixed(3)
            }
        }
    },
    methods: {
        initData () {
            let watchProductList = JSON.stringify(this.showProductList);
            this.watchProductList = JSON.parse(watchProductList);
            this.getAjax()
        },
        initInput () {
            this.ruleForm.saleGoldPrice = this.showProductList[0].saleGoldPrice;
        },
        sellSaves () {
            alert("ppp");
        },
        saveDataFun () {
            // let change = [];
            console.log(this.watchProductList);
            for (let i in this.watchProductList[0]) {
                // console.log("iiiiiiiiiiiiiiiiiiiii");
                if (this.watchProductList[0][i] !== this.showProductList[0][i]) {
                    this.filterAmend(i, this.watchProductList[0][i]);
                }
            }
            // this.showProductList
            // let options = {
            //     "orderNum": "单据号",
            //     "barCode": "条码号",
            //     "modifyList": ""
            // }
        },
        filterAmend (Key, Value) {
            // console.log("进来的值");
            // console.log(Key);
            // console.log(Value);
            switch (Key) {
                case "saleGoldPrice": // 销售金价
                    this.amendData.modifyType["01"] = Value;
                    break;
                case "paymentPrice": // 手工费
                    this.amendData.modifyType["02"] = Value;
                    break;
                case "discount": // 折扣
                    this.amendData.modifyType["03"] = Value;
                    break;
                case "newPrice": // 实售价
                   this.amendData.modifyType["04"] = Value;
            }
            console.log("修改后的值");
            console.log(this.amendData.modifyType);
        },
        getAjax () {
            let options = {
                "objId": "66393b03202911e78cc6f48e3888bbef"
            }
            seekgetGoldPrice(options).then((response) => { // 当日金价
                console.log("当日金价");
                console.log(response);
                this.todayGoldPrice = response.data.data.dataList[0];
                sessionStorage.setItem("商品列表-销售", JSON.stringify(response.data.data));
            }, (response) => {
                console.log(response);
            })
        }
    }
}
</script>
<style lang="scss" scoped>
.sell-template{
    ul{
        li{
            height: 14px;
            span{
                font-size: 14px;
                line-height: 36px;
            }
        }
    }
    .sell-template-top{
        padding-left: 20px;
        padding-top: 20px;
        li:first-child{
            margin-top: 0;
        }
        li{
            margin-top: 28px;
            span{
                display: inline-block;
                width: 70px;
            }
            .w320{
                width: 320px;
                display: inline-block;
            }
        }
        // li{
        //     height: 30px;
        //     line-height: 30px;
        // }
    }
    .sell-template-bottom{
        margin-top: 34px;
        padding-bottom: 40px;
        font-size: 0;
        background-color: #f6f7f8;
        h5{
            padding: 20px 0;
            text-indent: 20px;
            font-size: 14px;
        }
        ul{
            display: inline-block;
            vertical-align: top;
            padding-left: 20px;
            font-size: 0;
            li{
                font-size: 14px;
                line-height: 14px;
                color: #333;
            }
        }
        .one{
            width: 300px;
            // border: 1px solid red;
            li{
                width: 300px;
            }
        }
        .two,.three{
            li{
                margin-top: 34px;
            }
            li:first-child{
                margin-top: 0;
            }
        }
        .two{
            width: 370px;
            // border: 1px solid blue;
        }
        .three{
            width: 300px;
            // border: 1px solid red;
        }
    }
}
</style>