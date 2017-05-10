<template>
    <div class="w350">
        <div class="w270">
            <el-input
            placeholder="请输入/扫描产品编码"
            v-model="barCode"
            @keyup.enter.native="gainProduct">
            </el-input>
        </div>
        <a class="btn" @click="cancelPop">取消</a>
    </div>
</template>
<script>
import { operateAddProductToOrder } from './../../../../Api/commonality/operate'
export default {
    data () {
        return {
            "barCode": ""
        }
    },
    methods: {
        cancelPop () {
            this.$emit('addInputShow', true);
        },
        gainProduct () { // 获取产品信息(5.32单据操作-新增销售商品)
            let options = {
                "orderNum": sessionStorage.getItem("orderNumber"),
                "barcode": this.barCode
            }
            operateAddProductToOrder(options).then((response) => {
                console.log("5.32单据操作-新增销售商品");
                console.log(response);
                if (response.data.state === 200) {
                    sessionStorage.setItem("barcode", this.barCode);
                    this.$emit("sellProductListChange", "数据改变了");
                } else {
                    alert(response.data.msg)
                }
                console.log(response);
            }, (response) => {
                console.log(response);
            })
        }
    }
}
</script>

<style lang="scss" scoped>
.w350{
    width: 350px;
    position: absolute;
    background:#fff;
    // z-index:2;
    right: 20px;
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
        margin:12px 0 0 0;          
    }
    .comfirmBtn{
        color: #fff;
        background: #4FDCCB;
        border-color: #4FDCCB;
        margin-right: 0;
    }
}
</style>