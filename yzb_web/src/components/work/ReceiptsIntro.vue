<template>
    <div class="plain-detail-main">
        <div class="header">
            <span>单据信息</span>
            <router-link tag="p" :to='toRouter'>返回单据列表<img src="./../../assets/img/work/arrows-right-gray.png" alt=""></router-link>
        </div>
        <div class="body">
            <ul>
                <li class="main-color" v-if="receiptsIntroList.orderNum">
                    <span class="body-top">单<i class="space-between-w28"></i>号<i class="right-icon">：</i></span>
                    <span class="body-top" v-if="receiptsIntroList">{{receiptsIntroList.orderNum}}</span>
                </li>
                <li class="main-color" v-if="receiptsIntroList.productTypeName">
                    <span class="body-top">产品类别<i class="right-icon">：</i></span>
                    <span class="body-top" v-if="receiptsIntroList">{{receiptsIntroList.productTypeName}}</span>
                </li>
                <li v-if="receiptsIntroList.makeOrderManName">
                    <span class="main-color body-top">制<i class="space-between-w7"></i>单<i class="space-between-w7"></i>人<i class="right-icon">：</i></span>
                    <span class="main-color body-top" v-if="receiptsIntroList">{{receiptsIntroList.makeOrderManName}}</span>
                    <span class="main-9 body-top" v-if="receiptsIntroList">（{{preciseSun}}<i class="space-between-w8"></i>{{preciseMinute}}）</span>
                </li>
                <li v-if="receiptsIntroList.checkType">
                    <span>状<i class="space-between-w28"></i>态<i class="right-icon">：</i></span>
                    <span>{{getState(receiptsIntroList.checkType)}}</span>
                </li>
                <li v-if="receiptsIntroList.supplierName">
                    <span>供应商<i class="right-icon">：</i></span>
                    <span>{{receiptsIntroList.supplierName}}</span>
                </li>
                <li v-if="receiptsIntroList.shopName">
                    <span>计划分销<i class="right-icon">：</i></span>
                    <span v-if="receiptsIntroList">{{receiptsIntroList.shopName}}</span>
                </li>
                <li v-if="receiptsIntroList.storageName">
                    <span>入库库位<i class="right-icon">：</i></span>
                    <span v-if="receiptsIntroList">{{receiptsIntroList.storageName}}</span>
                </li>
                <li>
                    <span>合<i class="space-between-w28"></i>计<i class="right-icon">：</i></span>
                    <span class="all-count">{{receiptsIntroList.totalNum}}件、</span>
                    <span class="all-count">{{receiptsIntroList.totalWeight}}g、</span>
                    <span class="all-count">{{receiptsIntroList.totalPrice}}元</span>
                </li>
            </ul>
        </div>
    </div>
</template>
<script>
import {operateDelReceipt} from './../../Api/commonality/operate'
import {seekReceiptRKSynopsis} from './../../Api/commonality/seek'
import {receiptStatus} from './../../Api/commonality/status'
export default {
    props: [
        'toRouter'
    ],
    data () {
        return {
            "receiptsIntroList": []
        }
    },
    computed: {
        numberOfCases: function () { // 总件数
            return Number(this.receiptsIntroList.saleNum) || 0 + Number(this.receiptsIntroList.recycleNum) || 0 + Number(this.receiptsIntroList.exchangeNum) || 0;
        },
        allG: function () { // 总克
            return Number(this.receiptsIntroList.saleWeight) || 0 + Number(this.receiptsIntroList.recycleWeight) || 0 + Number(this.receiptsIntroList.exchangeWeight) || 0;
        },
        allMonny: function () {
            return Number(this.receiptsIntroList.salePrice) || 0 + Number(this.receiptsIntroList.recyclePrice) || 0 + Number(this.receiptsIntroList.exchangePrice) || 0;
        },
        preciseSun: function () { // 年
            let parm = this.receiptsIntroList.createDate;
            if (parm) {
                var Year = parm.slice(0, 4);
                var Month = parm.slice(4, 6);
                var Sun = parm.slice(6, 8);
                var allTime = Year + "-" + Month + "-" + Sun
                return allTime;
            }
        },
        preciseMinute: function () { // 分  20170414105213
            let parm = this.receiptsIntroList.createDate;
            if (parm) {
                var ConfigData = parm.slice(8, 10);
                var Mour = parm.slice(10, 12);
                var allTime = ConfigData + ":" + Mour
                return allTime;
            }
        }
    },
    created () {
        this.getSeekSellReceiptsIntro();
    },
    methods: {
        getState (parm) {
            return receiptStatus(parm);
        },
        getSeekSellReceiptsIntro () { // 5.35单据简介-销售
            let options = {
                "orderNum": sessionStorage.getItem("orderNumber")
            }
            seekReceiptRKSynopsis(options).then(response => {
                console.log("5.35单据简介-入库");
                console.log(response);
                if (response.data.state === 200) {
                    this.receiptsIntroList = response.data.data;
                }
            }, response => {
                console.log(response);
            })
        },
        collectMoney () {
            this.$emit('closeIncomeMoney', true);
        },
        delectProduct (parm) { // 删除商品
            let options = {
                "orderNum": parm
            }
            operateDelReceipt(options).then(response => {
                if (response.data.state === 200) {
                    alert("删除成功");
                    this.$router.push("/work/sell")
                } else {
                    alert(response.data.msg)
                }
            }, response => {
                console.log(response)
            })
        }
    }
}
</script>
<style lang="scss" scoped>
.plain-detail-main{
    position: relative;
    color: #333;
    padding-bottom: 20px;
    // background-color: #fff;
    .space-between-w6{
        display: inline-block;
        width: 6px;
    }
    .space-between-w8{
        display: inline-block;
        width: 8px;
    }
    .space-between-w7{
        display: inline-block;
        width: 7px;
    }
    .space-between-w28{
        display: inline-block;
        width: 28px;
    }
    .header{
        // border: 1px solid red;
        position: relative;
        height: 50px;
        line-height: 50px;
        margin: 0 10px;
        font-size: 16px;
        border-bottom: 1px solid #d6d6d6;
        span{
            display: inline-block;
        }
        p{
            position: absolute;
            top: 0;
            right: 0;
            font-size: 12px;
            cursor: pointer;
            img{
                margin-left: 5px;
            }
        }
    }
    .body{
        .body-top{
            display: inline-block;
            height: 14px;
            line-height: 14px;
        }
        ul{
            li{
                padding: 0 10px;
                white-space:nowrap;
                height: 16px;
                margin-top: 20px;
                span{
                    font-size: 14px;
                }
            }
            li{
                span:nth-child(1){
                    text-align: right;
                    display: inline-block;
                    width: 70px;
                    .right-icon{
                        font-style: normal;
                        text-align: right;
                    }
                }
            }
            li:nth-child(4){
                margin-top: 30px;
            }
            li:last-child{
                padding-left: 10px;
                height: 40px;
                line-height: 40px;
                margin-top: 10px;
                background-color: #f6f7f8;
            }
            .all-count{
                color: #e58585;
            }
        }
    }
    .bottom{
        padding: 0 10px;
        font-size: 0;
        height: 40px;
        width: 100%;
        margin-top: 10px;
        a{
            display: inline-block;
            height: 40px;
            line-height: 40px;
            border-radius: 4px;
            text-align: center;
        }
        a:nth-child(1){
            margin-right: 30px;
            font-size: 16px;
            width: 160px;
            color: #fff;
            background-color: #ffba42;
        }
        a:nth-child(2){
            width: 90px;
            font-size: 16px;
            border: 1px solid #e58585;
            color: #e58585;
        }
    }
}
</style>
