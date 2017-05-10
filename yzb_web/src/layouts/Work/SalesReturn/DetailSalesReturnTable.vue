  <template>
    <table border="1">
        <caption>退货明细表</caption>
        <tr>
            <td>单据号</td>
            <td colspan="2">{{StorageData.base.orderNumber}}</td>
            <td>商品属性</td>
            <td colspan="2">{{StorageData.base.rjProductType}}</td>
            <td>退货库位</td>
            <td colspan="4">{{StorageData.base.rjRepository}}</td>
        </tr>
        <tr>
            <td>收货状态</td>
            <td colspan="2" >{{StorageData.base.rjStatus}}</td>
            <td>分销商</td>
            <td colspan="7">{{StorageData.base.rjDistributor}}</td>
        </tr>
        <tr>
            <td>序号</td>
            <td>产品类别</td>
            <td>首饰名称</td>
            <td>条码号</td>
            <td>总件重</td>
            <td>净金重</td>
            <td>证书号</td>
            <td>主石</td>
            <td>副石</td>
            <td>售价</td>
            <td>成本(如有)</td>
        </tr>
        <tr v-for="(item, index) in StorageData.list">
            <td>{{index+1}}</td>
            <td v-if="index==0" :rowspan="lastIndex">{{item.classesName}}</td>
            <td>{{item.subName}}</td>
            <td>{{item.barcode}}</td>
            <td>{{item.countNum}}</td>
            <td>{{item.totalWeight}}</td>
            <td>{{item.certifiNo}}</td>
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
                <p>接收人：{{StorageData.base.receiver}}</p>
                <p>第1页/共10页</p>
            </td>
        </tr>
    </table>
  </template>

<script>
export default {
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
                    "orderNo": 'TH20170323010'
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
            // console.log(this.shopStorage)
            var url = INTERFACE_URL_9083 + "/v1/export/exportData";
            this.$http.post(url, data).then((response) => {
                // console.log("1111111111111111111111111");
                // console.log(response);
                // this.thisShopId = response.data.data.counterList;
                this.StorageData.base = response.data.data.base;
                this.StorageData.list = response.data.data.list;
                this.lastIndex = this.StorageData.list.length
                console.log(this.StorageData.base);
                console.log(11111111111111111111111111111)
                console.log(this.StorageData.list);
                // sessionStorage.setItem("xiaohuaSHUAIGE", JSON.stringify(data));
                // this.getLength()
            }, (response) => {
                console.log(response);
            })
        }
        // getLength () {
        //     this.colspan = response.data.data.list.length;
        // }
    }
}
  </script>

  <style lang="scss" scoped>
    table{
            width: 1100px;
            border-collapse: collapse;
            margin: 0 auto;
            font-size: 12px;
        }
        caption{
            height: 60px;
            line-height: 60px;
        }
        td{
            width: 9%;
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
  </style>
