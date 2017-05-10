<template>
    <div class="sell-template">
        <ul class="sell-template-top">
            <li>
                <span class="main-color">产品名称：</span>
                <span class="main-color">{{watchProductList.sale.jewelryName}}</span>
            </li>
            <li>
                <span class="main-color">条形码：</span>
                <span>{{watchProductList.sale.barCode}}</span>
            </li>
            <li>
                <span class="main-color">销售金价：</span>
                <input v-if="operationProduct" type="text" v-model="watchProductList.sale.saleGoldPrice">
                <span v-else>{{watchProductList.sale.saleGoldPrice}}</span>
            </li>
            <li>
                <span class="main-color">手工费：</span>
                <input v-if="operationProduct" type="text" v-model="watchProductList.sale.paymentPrice">
                <span v-else>{{watchProductList.sale.paymentPrice}}</span>
            </li>
            <li>
                <span class="main-color">折扣：</span>
                <input v-if="operationProduct" type="text" v-model="watchProductList.sale.discount">
                <span v-else>{{watchProductList.sale.discount}}</span>
            </li>
            <li>
                <span class="main-color">实售价：</span>
                <input v-if="operationProduct" type="text" v-model="watchProductList.sale.newPrice">
                <span v-else>{{watchProductList.sale.newPrice}}</span>
            </li>
        </ul>
        <div class="sell-template-bottom">
            <h5>产品参数</h5>
            <ul class="one">
                <li>
                    <span>证书号：</span>
                    <span>{{}}</span>
                </li>
            </ul>
            <ul class="two">
                <li>
                    <span>宝石名称：</span>
                    <span>{{}}</span>
                </li>
                <li>
                    <span>首饰名称：</span>
                    <span>{{}}</span>
                </li>
                
                <li>
                    <span>成色名称：</span>
                    <span>{{}}</span>
                </li>
            </ul>
            <ul class="three">
                <li>
                    <span>总件重：</span>
                    <span>{{}}</span>
                </li>
                <li>
                    <span>金重：</span>
                    <span>{{}}</span>
                </li>
            </ul>
        </div>
    </div>
</template>
<script>
export default {
    props: [
        "operationProduct",
        "showProductList",
        "saveBtn"
    ],
    data () {
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
                }
            }
    },
    created () {
        this.initData();
    },
    watch: {
        'operationProduct': function () {
            if (this.operationProduct === false) {
                this.initData();
            }
        },
        'saveBtn': function () {
            if (this.saveBtn === true) {
                this.saveDataFun();
                console.log("点击了保存");
            }
        }
    },
    methods: {
        initData () {
            let watchProductList = JSON.stringify(this.showProductList);
            this.watchProductList = JSON.parse(watchProductList);
        },
        sellSaves () {
            alert("ppp");
        },
        saveDataFun () {
            // let change = [];
            console.log(this.watchProductList.sale);
            for (let i in this.watchProductList.sale) {
                // console.log("iiiiiiiiiiiiiiiiiiiii");
                if (this.watchProductList.sale[i] !== this.showProductList.sale[i]) {
                    this.filterAmend(i, this.watchProductList.sale[i]);
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
        }
    }
}
</script>
<style lang="scss" scoped>
.sell-template{
    margin-top: 20px;
    ul{
        li{
            height: 14px;
            span{
                font-size: 14px;
                line-height: 14px;
            }
        }
    }
    .sell-template-top{
        padding-left: 20px;
        // margin-top: 34px;
        padding-top: 34px;
        // padding-left: 20px;
        //border: 1px solid red;
        li:first-child{
            margin-top: 0;
        }
        li{
            margin-top: 34px;
        }
        // li{
        //     height: 30px;
        //     line-height: 30px;
        // }
    }
    .sell-template-bottom{
        margin-top: 34px;
        padding-bottom: 20px;
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