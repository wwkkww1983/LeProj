<template>
    <div class="main-wrap">
        <div class="body-box-left">    
            <!-- 表格头部，内容配置，上一页带过来的数据，头部备注，应用类型，关闭路由 -->
            <new-template
                :checkType="receiptsIntroList.checkType"
                :applyType="3"
                :closeRouterUrl='"/work/sipping"'
                :detailRouterUrl='"/work/sipping/detailSipping"'
                v-on:getSeekSellReceiptsIntro="getSeekSellReceiptsIntro"
             ></new-template>
        </div>
        <div class="body-box-right">
            <storageReturnReceiptsIntro
                :receiptsIntroList="receiptsIntroList"
                :toRouter='"/work/storageReturn"'
            ></storageReturnReceiptsIntro>
            <receipts-remark></receipts-remark>
        </div>
    </div>
</template>
<script>
import newTemplate from './../CommonalityComponent/formTemplate/NewTemplate'
import storageReturnReceiptsIntro from '../../../components/work/StorageReturnReceiptsIntro.vue'
import receiptsRemark from '../../../components/work/ReceiptsRemark.vue'
import {productTpyeState} from './../../../Api/commonality/status'
import {seekReceiptRemark, seekReceiptTHSynopsis} from './../../../Api/commonality/seek'
export default {
    data () {
        return {
            "receiptsIntroList": "", // 单据简介
            "receiptRemark": "" // 单据备注
        }
    },
    components: {
        newTemplate,
        storageReturnReceiptsIntro,
        receiptsRemark
    },
    created () {
        this.getSeekSellReceiptsIntro(); // 单据简介
        this.getReceiptRemark(); // 单据备注
    },
    methods: {
        getproductTpye: function (parm) { // 商品属性
            return productTpyeState(parm);
        },
        getSeekSellReceiptsIntro () { // 单据简介
            let options = {
                "orderNum": sessionStorage.getItem("orderNumber")
            }
            seekReceiptTHSynopsis(options).then((response) => {
                console.log("单据简介");
                console.log(response)
                if (response.data.state === 200) {
                    this.receiptsIntroList = response.data.data;
                } else {
                    alert(response.data.msg)
                }
            }, (response) => {
                console.log(response);
            })
        },
        getReceiptRemark () { // 备注
            let options = {
                "orderNum": sessionStorage.getItem("orderNumber")
            }
            seekReceiptRemark(options).then((response) => {
                if (response.data.state === 200) {
                    this.receiptRemark = response.data.data.orderRemarkList;
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
        }
    }
}
</script>
<style lang="scss" scoped>
.main-wrap{
    width: 100%;
    height: 100%;
    padding: 0 20px;
    border: 2px solid red;
    font-size: 0;
    .body-box-left, .body-box-right{
        vertical-align: top;
        display: inline-block;
    }
    .body-box-left{
        width: 1300px;
        margin-right: 40px;
    }
    .body-box-right{
        width: 300px;
        height: 500px;
        border: 2px solid yellow;
        p{
            font-size: 20px;
        }
    }
}
</style>