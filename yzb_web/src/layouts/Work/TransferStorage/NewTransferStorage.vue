<template>
    <div class="main-wrap">
        <div class="body-box-left">    
            <!-- 表格头部，内容配置，上一页带过来的数据，头部备注，应用类型，关闭路由 -->
            <new-template
                :checkType="receiptsIntroList.checkType"
                :applyType="3"
                :closeRouterUrl='"/work/transferStorage"'
                :detailRouterUrl='"/work/transferStorage/newTransferStorage"'
                v-on:getSeekSellReceiptsIntro="getSeekSellReceiptsIntro"
             ></new-template>
        </div>
        <div class="body-box-right">
            <transfer-storage-receipts-intro
                :receiptsIntroList="receiptsIntroList"
                :toRouter='"/work/transferStorage"'
            ></transfer-storage-receipts-intro>
            <receipts-remark></receipts-remark>
        </div>
    </div>
</template>
<script>
import newTemplate from './../CommonalityComponent/formTemplate/NewTemplate'
import TransferStorageReceiptsIntro from '../../../components/work/TransferStorageReceiptsIntro.vue'
import receiptsRemark from '../../../components/work/ReceiptsRemark.vue'
import {productTpyeState} from './../../../Api/commonality/status'
import {seekReceiptDKSynopsis, seekReceiptRemark} from './../../../Api/commonality/seek'
export default {
    data () {
        return {
            "receiptsIntroList": "", // 单据简介
            "receiptRemark": "" // 单据备注
        }
    },
    components: {
        newTemplate,
        TransferStorageReceiptsIntro,
        receiptsRemark
    },
    // computed: {
    //     ...mapGetters([
    //         "shopLists" // 分销商
    //     ])
    // },
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
            seekReceiptDKSynopsis(options).then((response) => {
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
        getReceiptRemark () {
            let options = {
                "orderNum": sessionStorage.getItem("orderNumber")
            }
            seekReceiptRemark(options).then((response) => {
                console.log("单据简介");
                console.log(response)
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