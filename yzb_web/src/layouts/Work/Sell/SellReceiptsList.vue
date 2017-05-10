<template>
    <div class="sell-main">
        <!-- ***** 左边 ***** -->
        <div class="sell-receipt-left"> 
            <div class="sell-header">
                <ul class="sell-nav">
                    <router-link tag="li" to="/work/sell">销售单据<span>&nbsp>&nbsp</span></router-link>
                    <router-link tag="li" to="/work/sell/sellReceiptsList" :class="{on: !newRoute}">产品列表<span v-if="newRoute">&nbsp>&nbsp</span></router-link>
                    <!--  <router-link tag="li" to="/work/sell/sellReceiptsList/sellDetail" class="on"></router-link> -->
                    <li v-if="newRoute" :class="{on: newRoute}">产品明细</li>
                </ul>
                <ul class="options-btn">
                    <!-- 按钮 -->
                    <li class="add-btns" v-if="cutOptions">
                        <a v-for="(item, i) in type" @click="clickItem(i)">{{item.typeName}}</a>
                    </li>
                    <!-- 输入框 -->
                    <li v-else> 
                        <barcode-input v-on:sellProductListChange="sellProductListChange"></barcode-input>
                        <p v-if=>手工录入</p>
                    </li>
                </ul>
            </div>
            <div class="sell-list">
                <!-- 产品列表 -->
                <product-list></product-list>
            </div>
        </div>
        <!-- ***** 右边 ***** -->
        <div class="sell-receipt-right">
            <div class="plain-receipts-detail" @click="showId">
                <!-- 单据信息 -->
                <plain-receipts-detail
                v-on:closeIncomeMoney="isIncomeMoney"
                v-bind:receiptsIntroList="receiptsIntroList"
                ></plain-receipts-detail>
            </div>
            <div class="plain-money-data">
                <!-- 收银信息 -->
                <plain-money-data :collectMoneyData='collectMoneyData'></plain-money-data>
            </div>
        </div>
        <!-- ***** 弹窗 ***** -->
        <!-- 收钱 -->
        <income-money
        v-on:closeIncomeMoney="isIncomeMoney"
        :v-if='popup.incomeMoneyPopup'
        :incomeMoneyPopup='popup.incomeMoneyPopup'
        :receiptsIntroList='receiptsIntroList'
        ></income-money>      
    </div>
</template>
<script>
import {mapActions} from 'vuex'
import {seekSellProductList, seekSellcollectMoney, seekSellReceiptsIntro} from './../../../Api/commonality/seek'
import plainReceiptsDetail from './components/PlainReceiptsDetail.vue'
import plainMoneyData from './components/PlainMoneyData.vue'
import incomeMoney from './components/IncomeMoney.vue'
import sale from './components/Sale'
import replacement from './components/Replacement'
import recovery from './components/Recovery'
import replaceSkip from './components/ReplaceSkip'
import productList from './components/ProductList'
import barcodeInput from './components/BarcodeInput'
export default {
    data () {
        return {
            "xhceshi": false, // 测试代码
            "orderNumber": sessionStorage.getItem("orderNumber") || "",
            "receiptsIntroList": "", // 单据简介列表
            "collectMoneyData": "", // 收银信息
            "barCode": "", // 搜索产品的条形码
            "popup": {
                "incomeMoneyPopup": false
            },
            "productListData": "", // 商品列表-销售
            "view": null, // 销售类型显示窗口
            "details": null, // 详细
            "addNew": {
                "sale": {
                    "proName": "黄金钻戒",
                    "orderNum": "FD20012200",
                    "saleGoldPrice": "300元/克",
                    "paymentPrice": "200元/克",
                    "discount": "90%",
                    "newPrice": "6000.00元"
                },
                "exchange": {
                    "proName": "黄金钻戒",
                    "orderNum": "FD20012200",
                    "saleGoldPrice": "300元/克",
                    "paymentPrice": "200元/克",
                    "discount": "90%",
                    "newPrice": "6000.00元"
                }
            },
            newRoute: false, // 监听路由是否在产品明细
            type: [{ // 销售类型
                "typeName": "销售"
            }, {
                "typeName": "退换"
            }, {
                "typeName": "回收"
            }],
            popupStatus: false, // 弹窗
            chooseTep: null,
            addShow: true,
            cutOptions: true // 新建的输入切换
        }
    },
    components: {
        plainReceiptsDetail,
        plainMoneyData,
        incomeMoney,
        productList,
        sale,
        replacement,
        recovery,
        replaceSkip,
        barcodeInput
    },
    created () {
        this.getProductList(); // 获取商品列表
        this.getcollectMoney(); // 收银信息
    },
    watch: {
        '$route' (to, from) {
            if (to.name === "产品明细") {
                this.getSeekSellReceiptsIntro();
                this.newRoute = true;
            } else {
                this.newRoute = false;
            }
            console.log("获取商品列表 + 收银信息");
            console.log(to);
            console.log(from);
        }
    },
    methods: {
        ...mapActions([
            'sellProductListFun'
        ]),
        sellProductListChange (parm) { // 监听扫码成功
            console.log("监听扫码成功");
            console.log(parm);
        },
        getProductList () { // 获取商品列表
            let options = {
                "orderNum": this.orderNumber,
                "page": 1,
                "pageSize": 30
            }
            seekSellProductList(options).then((response) => { // 商品列表-销售
                console.log("商品列表-销售");
                console.log(response);
                this.sellProductListFun(response.data.data);
                sessionStorage.setItem("商品列表-销售", JSON.stringify(response.data.data));
            }, (response) => {
                console.log(response);
            })
            this.getSeekSellReceiptsIntro();
        },
        getSeekSellReceiptsIntro () { // 5.35单据简介-销售
            let options = {
                "orderNum": this.orderNumber
            }
            seekSellReceiptsIntro(options).then(response => {
                console.log("产品列表");
                console.log(response);
                if (response.data.state === 200) {
                    // sessionStorage.setItem("产品列表", JSON.stringify(response.data.data));
                    this.receiptsIntroList = response.data.data;
                }
            }, response => {
                console.log(response);
            })
        },
        isIncomeMoney (parm) { // 监听关闭弹窗事件
            this.popup.incomeMoneyPopup = parm;
        },
        getcollectMoney () { // 收银信息
            var options = {
                "orderNum": sessionStorage.getItem("orderNumber")
            }
            seekSellcollectMoney(options).then((response) => {
                console.log("收银信息");
                console.log(response)
            }, (response) => {
                console.log(response);
            })
            // this.collectMoneyData = seekSellcollectMoney(options); // 收银信息
        },
        // delectProduct (parm) { // 删除商品
        //     let options = {
        //         "productList": [
        //             {
        //                 "bracode": parm // 条码号
        //             }
        //         ],
        //         "orderNum": "", // 单据号
        //         "operate": "" // 操作类型 1.新增 2.删除 3.全部删除
        //     }
        //     receiptsOperation(options)
        //     alert(parm)
        // },
        clickItem (i) {
            this.cutOptions = false;
            switch (i) {
                case 1: // 退换
                    this.view = replacement;
                    this.chooseTep = "1";
                    this.addShow = false;
                    break
                case 2: // 回收

                    break
            }
        },
        manual () { // 手动输入
            this.view = replaceSkip
            // this.popup.newProduct = false;
            this.chooseTep = "1";
        },
        lookDetail () { // 显示详细
            this.details = true;
        },
        addReceipt () { // 新建单据
            this.xhceshi = true;
        },
        showId () {
            console.log(this.orderNumber)
            var data = {
                "data": {
                    "orderNum": this.orderNumber
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
            var url = INTERFACE_URL_9083 + "/v1/auth/getUserRoleList";
            this.$http.post(url, data).then((response) => {
                console.log("出来看看")
                console.log(response.data);
            }, (response) => {
                console.log(response);
            })
        },
        addInputShow (parm) { // 添加输入框
            this.addShow = parm;
        }
    }
}
</script>
<style lang="scss" scoped>
.sell-main{
    width: 100%;
    height: 100%;
    padding-bottom: 20px;
    background-color: #ebedef;
    .sell-receipt-left{
        float: left;
        width: 1230px;
        // height: 100%;
        // overflow: hidden;
        padding-bottom: 100px;
        margin-left: 100px;
    }
    .sell-receipt-right{
        float: left;
        margin: 20px 0 0 40px;
        width: 300px;
        height: 100%;
        .plain-receipts-detail{
            background-color: #fff;
            width: 300px;
            padding-bottom: 10px;
        }
        .plain-money-data{
            width: 300px;
            height: 400px;
            margin-top: 20px;
            background-color: #fafbfc;
        }
    }
    .sell-header { // 头部导航
        background-color: #fafbfc;
        height: 60px;
        margin-top: 20px;
        font-size: 0;
        position: relative;
        .sell-nav{
            display: inline-block;
            vertical-align: top;
            border: 1px solid #000;
            font-size: 0;
            padding-left: 20px;
            li{
                font-size: 14px;
                height: 60px;
                line-height: 60px;
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
                width: 120px;
                margin-right: 40px;
            }
            .time-selects{
                width: 190px;
            }
        }
        .options-btn{ // 右边新增操作
            position: absolute;
            right: 20px;
            height: 60px;
            vertical-align: top;
            font-size: 14px;
            display: inline-block;
            .add-btns{
                padding-top: 10px;
                a{
                    display: inline-block;
                    margin: 0 5px;
                    width: 90px;
                    height: 38px;
                    line-height: 38px;
                    text-align: center;
                    background: #83bbf7;
                    border-radius: 4px;
                    cursor: pointer;
                    color: #fff;
                    &:hover{
                        background: #4580bf;
                    }
                }
            }
        }
    }
    .sell-list { // 商品列表
        margin-top: 40px;
        .sell-list-body{ // 每条数据
            position: relative;
            margin-top: 10px;
            background-color: #fff;
            font-size: 0;
            .sell-list-left{                
                padding: 20px 0 0 40px;
                span{
                    display: inline-block;
                    font-size: 16px;
                    line-height: 16px;
                }
                p:nth-child(1){
                    span:nth-child(1){
                        margin-right: 10px;
                    }
                }
                p:nth-child(2),p:nth-child(3){
                    color: #666;
                }
                .time-color{
                    margin-left: 20px;
                    color: #666;
                }
            }
            .sell-list-bottom{
                // border: 1px solid red;
                padding-left: 40px;
                height: 60px;
                line-height: 60px;
                background-color: #f6f7f8;
                span{
                    color: #333;
                    margin-top: 21px;
                    width: 190px;
                    font-size: 16px;
                    margin-right: 50px;
                }
            }
            .sell-list-operation{
                position: absolute;
                height: 40px;
                top: 0;
                bottom: 0;
                margin: auto;
                right: 30px;
                font-size: 0;
                a{
                    display: inline-block;
                    width: 90px;
                    height: 40px;
                    line-height: 40px;
                    text-align: center;
                    margin-left: 20px;
                    font-size: 16px;
                    // border: 1px solid red;
                    border-radius: 4px;
                }
                a:nth-child(1){
                    border: 1px solid #e58585;
                    color: #e58585;
                }
                a:nth-child(2){
                    background-color: #ffba42;
                    color: #fff;
                }
            }
        }
    }
    .dialog-w450-h250-hn{ // 弹框
        .el-dialog__wrapper{
            .el-dialog.el-dialog--small{
                background:red !important;
                ul{
                    padding: 70px 40px;
                    li{
                        width: 100px;
                        height: 36px;
                        line-height: 36px;
                        display: inline-block;
                        text-align: center;
                        background: #fff;
                        border: 1px solid #d6d6d6;
                        border-radius: 4px;
                        color: #999;
                        margin: 0 35px 0 -3px;
                        cursor: pointer;
                        font-size: "Microsoft YaHei";
                        font-size: 14px;
                    }
                    li:hover{
                        background: #ffba42;
                        border: 1px solid #ffba42;
                        color: #fff;
                    }
                    li:last-child{                
                        margin-right: 0;
                    }
                }
                .cancelBtn{ // 取消按钮
                    width: 200px;
                    height: 40px;
                    line-height: 40px;
                    border-radius: 4px;
                    display: block;
                    background:#4FDCCB;
                    text-align: center;
                    color:#fff;
                    cursor: pointer;
                    margin: 0 auto;
                    font-size: "Microsoft YaHei";
                    font-size: 14px;
                }
            }
        }
    }
}
</style>