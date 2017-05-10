<template>
    <div class="plain-money-data">
        <div class="header">
            <span>收银信息</span>
        </div>
        <div class="body">
            <ul>
                <li>收银员：{{collectMoneyData.makeOrderManName}}</li>
                <li>时间：{{collectMoneyData.createDate}}</li>
                <!-- <li>收银方式：{{collectMoneyData.priceList.priceType}}</li> -->
            </ul>
            <ul>
                <li><img src="./../../../../assets/img/work/other.png" alt=""><span>刷卡：52000.00</span></li>
                <li><img src="./../../../../assets/img/work/cash.png" alt=""><span>现金：52000.00</span></li>
                <li><img src="./../../../../assets/img/work/WeChat.png" alt=""><span>微信：52000.00</span></li>
                <li><img src="./../../../../assets/img/work/alipay.png" alt=""><span>支付宝：52000.00</span></li>
                <li><img src="./../../../../assets/img/work/other.png" alt=""><span>其它：52000.00</span></li>
                <li><img src="./../../../../assets/img/work/change.png" alt="">找零：52000.00</li>
            </ul>
        </div>
    </div>
</template>
<script>
export default {
    props: [
        'collectMoneyData'
    ]
}
</script>
<style lang="scss" scoped>
.plain-money-data{
    position: relative;
    .header{
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
        overflow-x: auto;
        overflow-y: hidden;
        ul:nth-child(1){
            // border: 1px solid blue;
            li:nth-child(1),li:nth-child(2){
                height: 16px;
                line-height: 16px;
                margin-top: 20px;
            }
        }
        ul:nth-child(2){
            margin-top: 30px;
            li{
                white-space:nowrap;
                height: 40px;
                line-height: 40px;
                background-color: #f6f7f8;
                img{
                    vertical-align: top;
                    margin-right: 10px;
                    margin-top: 8px;
                    width: 24px;
                    height: 24px;
                }
                span{
                    vertical-align: center;
                }
            }
        }
    }
}
</style>
