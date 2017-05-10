<template>
    <div class="plain-receipts-detail">
        <div class="header">
            <span>单据信息</span>
           <!--  <router-link to="/workll">返回单据列表</router-link> -->
        </div>
        <div class="body">
            <ul>
                <li class="main-color">
                    <span class="body-top">单号：</span><span class="body-top" v-if="receiptsIntroList">{{receiptsIntroList.orderNum}}</span></li>
                <li>
                    <span class="main-color body-top">制单人：</span>
                    <span class="main-color body-top" v-if="receiptsIntroList">{{receiptsIntroList.makeOrderManName}}</span>
                    <span class="main-9 body-top" v-if="receiptsIntroList">({{receiptsIntroList.createDate}})</span>                    
                </li>
                <li v-if="receiptsIntroList.saleNum">
                    <span>销售：</span>
                    <span>{{receiptsIntroList.saleNum}}件</span>
                    <span>{{receiptsIntroList.saleWeight}}g</span>
                    <span>{{receiptsIntroList.salePrice}}元</span>
                </li>
                <li v-if="receiptsIntroList.recycleNum">
                    <span>回收：</span>
                    <span>{{receiptsIntroList.recycleNum}}件</span>
                    <span>{{receiptsIntroList.recycleWeight}}g</span>
                    <span>{{receiptsIntroList.recyclePrice}}元</span>
                </li>
                <li v-if="receiptsIntroList.exchangeNum">
                    <span>退换：</span>
                    <span>{{receiptsIntroList.exchangeNum}}件</span>
                    <span>{{receiptsIntroList.exchangeWeight}}g</span>
                    <span>{{receiptsIntroList.exchangePrice}}元</span>
                </li>
                <li>
                    <span>合计：</span>
                    <span>{{numberOfCases}}</span>
                    <span>{{allG}}</span>
                    <span>{{allMonny}}</span>
                </li>
            </ul>
        </div>
        <div class="bottom">
            <a href="javascript:void(0)" @click="collectMoney">收银</a>
            <a href="javascript:void(0)" @click="delectProduct(receiptsIntroList.orderNum)">删除单据</a>
        </div>
        <!-- 简单版的单据详情 -->
    </div>
</template>
<script>
export default {
    props: [
        "receiptsIntroList"
    ],
    computed: {
        numberOfCases: function () { // 总件数
            return Number(this.receiptsIntroList.saleNum) || 0 + Number(this.receiptsIntroList.recycleNum) || 0 + Number(this.receiptsIntroList.exchangeNum) || 0;
        },
        allG: function () { // 总克
            return Number(this.receiptsIntroList.saleWeight) || 0 + Number(this.receiptsIntroList.recycleWeight) || 0 + Number(this.receiptsIntroList.exchangeWeight) || 0;
        },
        allMonny: function () {
            return Number(this.receiptsIntroList.salePrice) || 0 + Number(this.receiptsIntroList.recyclePrice) || 0 + Number(this.receiptsIntroList.exchangePrice) || 0;
        }

    },
    methods: {
        collectMoney () {
            this.$emit('closeIncomeMoney', true);
        },
        delectProduct (parm) { // 删除
            alert(parm);
        }
        // abc () { 获取创建人Id this.receiptsIntroList.makeOrderManId
        //     console.log(this.receiptsIntroList)
        // }
    }
}
</script>
<style lang="scss" scoped>
.plain-receipts-detail{
    color: #333;
    position: relative;
    .header{
        // border: 1px solid red;
        height: 50px;
        line-height: 50px;
        margin: 0 10px;
        border-bottom: 1px solid #d6d6d6;
        span{
            font-size: 16px;
        }
        a{
            float: right;
            font-size: 12px;
        }
    }
    .body{
        padding: 0 10px;
        .body-top{
            display: inline-block;
            height: 14px;
            line-height: 14px;
        }
        ul{
            overflow-x: auto;
            overflow-y: hidden;
            li{
                white-space:nowrap;
                height: 16px;
                margin-top: 20px;
                span{
                    font-size: 14px;
                }
            }
            li:nth-child(3){
                margin-top: 30px;
            }
            li:last-child{
                height: 40px;
                line-height: 40px;
                margin-top: 10px;
                background-color: #f6f7f8;
            }
        }
    }
    .bottom{
        padding: 0 10px;
        font-size: 0;
        height: 40px;
        width: 100%;
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
            margin-top: 5px;
            color: #fff;
            background-color: #ffba42;
        }
        a:nth-child(2){
            width: 90px;
            margin-top: 5px;
            font-size: 16px;
            border: 1px solid #e58585;
            color: #e58585;
        }
    }
}
</style>
