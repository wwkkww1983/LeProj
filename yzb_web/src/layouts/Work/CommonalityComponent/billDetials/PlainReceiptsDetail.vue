<template>
    <div class="plain-receipts-detail">
        <div class="header">
            <span>单据信息</span>
            <router-link to="./">返回单据列表&gt;</router-link>
        </div>
        <div class="body">
            <ul>
                <li class="main-color">
                    <span class="body-top">单号：</span><span class="body-top" v-if="receiptDescription">{{receiptDescription.orderNum}}</span></li>
                <li>
                    <span class="main-color body-top">制单人：</span>
                    <span class="main-color body-top" v-if="receiptDescription">{{receiptDescription.makeOrderManName}}</span>
                    <span class="main-9 body-top" v-if="receiptDescription">({{receiptDescription.createDate}})</span>                    
                </li>
                <li>
                    <span>状态：</span>
                    <span>{{showCheckType(receiptDescription.checkType)}}</span>
                </li>
                <li>
                    <span>发货库位：</span>
                    <span>{{receiptDescription.storageName}}</span>
                </li>
                <li>
                    <span>收货店铺：</span>
                    <span>{{receiptDescription.shopName}}</span>
                </li>
                <li>
                    <span>合计：</span>
                    <span>{{receiptDescription.totalNum}}</span>
                    <span>{{receiptDescription.totalWeight}}</span>
                    <span>{{receiptDescription.totalPrice}}</span>
                </li>
            </ul>
        </div>
        <!-- 简单版的单据详情 -->
    </div>
</template>
<script>
export default {
    props: [
        "receiptDescription"
    ],
    data () {
        return {
            userData: JSON.parse(sessionStorage.getItem("userData"))
        }
    },
    computed: {
        "this.receiptDescription.totalNum": function () { // 总件数
            return Number(this.receiptDescription.saleNum) || 0 + Number(this.receiptDescription.recycleNum) || 0 + Number(this.receiptDescription.exchangeNum) || 0;
        },
        "this.receiptDescription.totalWeight": function () { // 总克
            return Number(this.receiptDescription.saleWeight) || 0 + Number(this.receiptDescription.recycleWeight) || 0 + Number(this.receiptDescription.exchangeWeight) || 0;
        },
        "this.receiptDescription.totalPrice": function () {
            return Number(this.receiptDescription.salePrice) || 0 + Number(this.receiptDescription.recyclePrice) || 0 + Number(this.receiptDescription.exchangePrice) || 0;
        }
    },
    methods: {
        delectProduct (parm) { // 删除
            alert(parm);
        },
        showCheckType (parm) {
            let configName = null;
            switch (parm) {
                case '1':
                    configName = "未审核";
                    break;
                case '2':
                    configName = "审核中";
                    break;
                case '3':
                    configName = "已审核"
                    break;
            }
            return configName;
        }
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
