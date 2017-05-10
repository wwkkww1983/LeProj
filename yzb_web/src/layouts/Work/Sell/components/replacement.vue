<template>
    <div class="w370">
        <div class="w270">
            <el-input
            placeholder="请输入/扫描产品编码"
            v-model.number="barCode"
            @keyup.enter.native="gainProduct">
            </el-input>
        </div>
        <a class="btn" @click="cancelPop">取消</a>
        <a class="btn comfirmBtn" @click="confirmPop">手动录入</a>
    </div>    
</template>
<script>
import {operateAddBackProductToOrder} from './../../../../Api/commonality/operate'
    export default {
        data () {
            return {
                popup: { // 弹窗
                    openPopup: true
                },
                barCode: ''
            }
        },
        props: ['keepMsg', 'chooseMsg'],
        watch: {
            'keepMsg': function () {
                this.popup.openPopup = true;
            },
            'chooseMsg': function () {
                if (this.chooseMsg === "1") {
                    this.chooseMsg = "2";
                }
            }
        },
        methods: {
            cancelPop () {
                this.$emit('addInputShow', true);
            },
            confirmPop () { // 跳转
                this.$emit('skip');
            },
            gainProduct () { // 获取产品信息(5.32单据操作-新增销售商品)
                let options = {
                    "orderNum": sessionStorage.getItem("orderNumber"),
                    "barcode": this.barCode,
                    "saleGoldPrice": "", // 销售金价
                    "discount": "", // 折扣
                    "calcMethod": "", // 计价方式 1 计重 2 计件
                    "paymentPrice": "", // 手工费
                    "newPrice": "" // 实售价
                }
                operateAddBackProductToOrder(options).then((response) => {
                    console.log("5.32单据操作-新增销售商品");
                    sessionStorage.setItem("barcode", this.barCode);
                    this.xhceshi = false;
                    // this.$router.push("/work/sell/sellReceiptsList/sellDetail");
                    console.log(response);
                }, (response) => {
                    console.log(response);
                })
                alert("通过扫码获取产品信息" + this.barCode);
            }
        }
    }
</script>

<style lang="scss" scoped>
.w370{
    width: 370px;
    height:100px;
    position: absolute;
    background:#fff;
    // z-index:2;
    right: 0px;
    top: 0;
    .w270{
        width: 270px;
        float: left;
        margin: 12px 0;
    }
    .btn{
        width: 70px;
        height: 36px;
        line-height: 36px;
        border-radius: 4px;
        display: inline-block;
        background: #fff;
        text-align: center;
        color:#999;
        border: 1px solid #999;
        cursor: pointer;
        font-size: 14px;
        float: right;
        margin:12px 20px 0 0;          
    }
    .comfirmBtn{
        color: #fff;
        background: #4FDCCB;
        border-color: #4FDCCB;
        width:270px;
        float: left;
        margin: 0;
    }
}
</style>