<template>
    <div class="item-box">
        <table border="1" cellpadding="0" cellspacing="0" v-if="StorageData">
            <caption>入库明细表</caption>
            <!-- <print-from></print-from> -->
            <tr class="kph">
                <td>单据号99999999999999</td>
                <td colspan="2">{{StorageData.base.orderNumber}}</td>
                <td>操作部门</td>
                <td colspan="2">{{StorageData.base.operation}}</td>
                <td>供应商</td>
                <td colspan="4">{{StorageData.base.provider}}</td>
            </tr>
            <tr>
                <td>入库库位</td>
                <td colspan="2" >{{StorageData.base.repository}}</td>
                <td>产品类别</td>
                <td colspan="2">{{StorageData.base.productCategory}}</td>
                <td>计划分销</td>
                <td colspan="4">{{StorageData.base.planSold}}</td>
            </tr>
            <tr>
                <td>序号</td>
               <!--  <td>产品类别</td> -->
                <td>首饰名称</td>
                <td>条码号</td>
                <td>总计件</td>
                <td>净金重</td>
                <td>证书号</td>
                <td>主石</td>
                <td>副石</td>
                <td>售价</td>
                <td>成本(如有)</td>
            </tr>
            <tr v-for="(item, index) in StorageData.list">
                <td>{{index+1}}</td>
                <td>{{item.subName}}</td>
                <td>{{item.barcode}}</td>
                <td>{{item.countNum}}</td>
                <td>{{item.totalWeight}}</td>
                <td>{{item.netWeight}}</td>
                <td>{{item.count1}}</td>
                <td>{{item.count2}}</td>
                <td>{{item.soldPrice}}</td>
                <td>{{item.costPrice}}</td>
            </tr>
            <tr>
                <td colspan="11" class="text-l">
                    备注：{{StorageData.base.remarks}}
                    <span>制单时间：{{StorageData.base.makeTime}}</span>
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <p>制单人：{{StorageData.base.makeMan}}</p>
                    <p>审核人：{{StorageData.base.auditor}}</p>
                </td>
            </tr>
        </table>
        <a class="print-btn" href="javascript: null" @click="xiaohua">打印</a> 
    </div>
</template>
<script>
import printFrom from '../CommonalityComponent/formTemplate/printFrom'
export default {
    components: {
        printFrom
    },
    data () {
        return {
            configData: {
                "classesName": null,
                "subName": null,
                "barcode": null,
                "countNum": null,
                "totalWeight": null,
                "netWeight": null,
                "count1": null,
                "count2": null,
                "soldPrice": null,
                "costPrice": null
            },
            StorageData: {
                "base": {},
                "list": {}
            },
            lastIndex: 0,
            rowspan: 0
        }
    },
    created () {
        this.getAjaxData()
    },
    methods: {
        getAjaxData () {
            var data = {
                "data": {
                    "orderNo": sessionStorage.getItem("orderNumber")
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
            // sessionStorage.setItem("请求参数", JSON.stringify(data));
            // console.log(this.shopStorage)
            var url = INTERFACE_URL_9083 + "/v1/export/exportData";
            this.$http.post(url, data).then((response) => {
                this.StorageData.base = response.data.data.base;
                this.StorageData.list = response.data.data.list;
                this.lastIndex = this.StorageData.list.length
                console.log(this.StorageData.base);
                console.log(11111111111111111111111111111)
                console.log(this.StorageData.list);
                sessionStorage.setItem("abc", data);
                sessionStorage.setItem("bcde", JSON.stringify(response.data.data));
                // alert(this.orderNo)
            }, (response) => {
                console.log(response);
            })
        },
        xiaohua () {
            window.print();
        }
    }
}
</script> 
<style lang="scss" scoped>
.item-box{
    width: 100%;
    height: 100%;
    overflow-y: auto;
    table{
        width: 700px;
        // height: 595px;
        border-collapse: collapse;
        margin: 0 auto;
        font-size: 12px;
        page-break-inside: avoid;
    }
    tr{
        page-break-inside: avoid;
    }
    td{
        page-break-inside: avoid;
    }
    caption{
        height: 60px;
        line-height: 60px;
    }
    td{
        // width: 9%;
        text-align: center;
        height: 28px;
        line-height: 28px;
    }
    td p{
        float: left;
        width: 25%;
        text-align: left;
        padding-left:10px;
    }
    td p:last-child{
        float: right;
        text-align: right;
        padding-right: 10px;
    }
    td.text-l{
        text-align: left;
        padding:0 10px;
    }
    td span{
        float: right;
    }
    .print-btn{
        display: inline-block;
        height: 60px;
        line-height: 60px;
        text-align: center;
        width: 200px;
        border-radius: 5px;
        background-color: red;
    }
}
</style>
