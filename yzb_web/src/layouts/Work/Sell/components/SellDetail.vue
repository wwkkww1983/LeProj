<template>
    <div class="product-list-detail">
        <div class="detail-header">
            <ul class="header-operation">
                <li v-show="!operationProduct" @click="oprerationProduct">编辑</li>
                <li v-show="operationProduct" @click="saveOperation">保存</li>
                <li v-show="operationProduct" @click="cancelOperation">取消</li>
                <li @click="delectProduct">删除</li>
            </ul>
            <ul class="header-page">
                <li :class="{on: 'pageNum > 1'}" @click="previousPage">上一件</li>
                <li>{{newPage}}<span>/</span>{{pageNum}}</li>
                <li :class="{on: 'pageNum > 1'}" @click="nextPage">下一件</li>
            </ul>
        </div>
        <div class="detail-body">
            <component
            :is="sellDetailTemplate"
            :saveBtn="saveBtn"
            :operationProduct="operationProduct"
            :showProductList="showProductList"
            ></component>
        </div>
    </div>
</template>
<script>
import {seekSellProductDetail} from "./../../../../Api/commonality/seek"
import sellTemplate from "./SellTemplate" // 销售显示
import saleList from './sellDetailTemplate/saleList'
import exchangeList from './sellDetailTemplate/exchangeList'
import recycleList from './sellDetailTemplate/recycleList'
export default{
    data () {
        return {
            "sellDetailTemplate": "", // 默认显示的产品列表
            "barCodeList": [], // 条形码列表
            "newBarCode": sessionStorage.getItem("barcode") || "", // 当前页的条形码
            "pageNum": 1, // 总页数
            "newPage": 1, // 当前页
            "showProductList": "", // 要显示的产品列表
            "saveBtn": "", // 监听点击保存
            "operationProduct": false // 编辑商品
        }
    },
    components: {
        sellTemplate,
        saleList,
        exchangeList,
        recycleList
    },
    created () {
        this.initData();
    },
    beforeDestroy () { // 组件销毁
        console.log("组件呗销毁");
    },
    methods: {
        initData () { // 初始化数据
            // this.barCodeList = JSON.parse(sessionStorage.getItem("barCodeList")); // 之前需求分析错误，现在砍了
            // this.newBarCode = sessionStorage.getItem("newBarCode");
            // this.pageNum = this.barCodeList.length;
            // this.newPage = this.barCodeList.indexOf(this.newBarCode) + 1;
            // 通过返回的list的sale和exchange进行动态切换
            if (this.newBarCode) { // 查询、条形码进来
                this.getSellProductDetail(this.newBarCode);
            } else { // 手工输入进来
                let choosetype = JSON.parse(sessionStorage.getItem("choosetype"));
                this.filterData(choosetype.productType, choosetype);
            }
            // console.log("9")
        },
        getSellProductDetail (parm) { // 商品明细 - 销售
            let options = {
                "barCode": parm, // 条形码
                "orderNum": sessionStorage.getItem("orderNumber") // 单据号
            }
            // this.showProductList = seekSellProductDetail(options);
            seekSellProductDetail(options).then((response) => {
                console.log("商品明细 -9销售");
                console.log(response);
                // this.showProductList = response.data.data;
                if (response.data.state === 200) {
                    // switch (response.data.data.productType) {
                    //     case "1":
                    //         console.log("过滤销售")
                    //         this.sellDetailTemplate = "saleList";
                    //         this.showProductList = response.data.data.sale;
                    //         break;
                    //     case "2":
                    //         this.sellDetailTemplate = "exchangeList"
                    //         this.showProductList = response.data.data.exchange;
                    //         console.log("过滤退换")
                    //         break;
                    //     case "3":
                    //         this.sellDetailTemplate = "recycleList"
                    //         this.showProductList = response.data.data.recycle;
                    //         console.log("过滤回收")
                    //         break;
                    // }
                    this.filterData(response.data.data.productType, response.data.data);
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
        },
        filterData (productType, allData) { // 筛选数据 (类型，数据)
            alert("筛选数据 (类型，数据)");
            switch (productType) {
                case "1":
                    alert("1")
                    console.log("过滤销售")
                    this.sellDetailTemplate = "saleList";
                    this.showProductList = allData.sale;
                    break;
                case "2":
                    alert("2")
                    this.sellDetailTemplate = "exchangeList"
                    this.showProductList = allData.exchange;
                    console.log("过滤退换")
                    break;
                case "3":
                    alert("3")
                    this.sellDetailTemplate = "recycleList"
                    this.showProductList = allData.recycle;
                    console.log("过滤回收")
                    break;
            }
        },
        oprerationProduct () { // 编辑单据
            this.operationProduct = true;
        },
        delectProduct () { // 删除单据
            var cc = "01";
            this.amendData[cc] = "99";
            console.log(this.amendData);
            alert("删除功能，敬请期待")
        },
        saveOperation () { // 保存
            // this.$children("xi");
            this.saveBtn = true;
            alert("保存功能，敬请期待")
        },
        cancelOperation () { // 取消
            this.operationProduct = false;
            // this.$emit.xiaok("ppp")
        },
        xiaoH () {
            alert("9999")
        },
        previousPage () { // 上一件
            if (this.newPage > 1) {
                this.newPage -= 1;
                let BarCode = this.barCodeList[this.newPage - 1];
                this.getSellProductDetail(BarCode);
            }
        },
        nextPage () { // 下一件
            if (this.newPage < this.pageNum) {
                this.newPage += 1;
                let BarCode = this.barCodeList[this.newPage - 1];
                this.getSellProductDetail(BarCode);
            }
        }
    }
}
</script>
<style lang="scss" scoped>
.product-list-detail{
    width: 100%;
    height: 100%;
    // background-color: red;
    .detail-header{
        position: relative;
        height: 60px;
        overflow: hidden;
        background-color: #fff;
        .header-operation{
            position: absolute;
            top: 10px;
            left: 20px;
            li{
                display: inline-block;
                width: 90px;
                height: 40px;
                line-height: 40px;
                text-align: center;
                border-radius: 5px;
                margin-right: 20px;
                cursor: pointer;
            }
            li:nth-child(1){
                color: #42abff;
                border: 1px solid #42abff;
            }
            li:nth-child(4){
                color: #e58585;
                border: 1px solid #e58585;
            }
        }
        .header-page{
            position: absolute;
            top: 20px;
            right: 20px;
            li{
                display: inline-block;
                background-color: #fff;
                margin-left: 20px;
                height: 14px;
                line-height: 14px;
                cursor: pointer;
            }
        }
        .on{
            color: #999;
            &:hover{
                cursor: default;
            }
        }
    }
    .detail-body{
        // margin-top: 0;
        // margin-top: 20px;
        background-color: #fff;
        // border: 1px solid red;
    }
}
</style>