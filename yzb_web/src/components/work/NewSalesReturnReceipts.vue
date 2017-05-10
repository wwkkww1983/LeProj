<template>
    <el-dialog class="dialog-w540-h320-hn" title="" v-model="isShow">
        <!-- 收货库位 -->
        <div class="list" v-if="cutData.one">
            <p class="popTitle">
                退货库位
                <a v-if="newDatas.repositoryId" href="javascript:void(0)" class="skip" @click="toPageFun('two', 'one')">下一步</a>
            </p>
            <ul>
                <li :class="{on: newDatas.repositoryId === item.repositoryId}" v-for="item in repositoryList" @click="getRepositoryId(item.repositoryId)">
                    {{item.repositoryName}}
                </li>
            </ul>
        </div>
        <!-- 店铺列表 -->
        <div class="list" v-if="cutData.two">
            <p class="popTitle">
                <a href="javascript:void(0)" class="prePage" @click="toPageFun('one', 'two')">上一步</a>
                选择店铺
            </p>
            <ul>
                <li v-for="item in shopListByCo" @click="getProductType(item.shopId)">
                    {{item.shopName}}
                </li>
            </ul>
        </div>
    </el-dialog>
</template>
<script>
import {mapGetters, mapActions} from "vuex"
import {operateCreateTHReceipt} from './../../Api/commonality/operate'
export default {
    data () {
        return {
            "cutData": {
                "one": true,
                "two": false
            },
            "newDatas": { // 新增数据(其它页面也用到的)
                "productType": "", // 商品属性
                "repositoryId": "" // 收货库位
            },
            "productTypeList": [
                {
                    "name": "成品",
                    "type": "1"
                },
                {
                    "name": "旧料",
                    "type": "2"
                }
            ],
            "isShow": false
        }
    },
    props: [
        'newPopup'
    ],
    watch: {
        'newPopup': function () {
            if (this.newPopup === true) {
                this.isShow = this.newPopup;
            }
        },
        'isShow': function () {
            if (this.isShow === false) {
                this.$emit("closePopup", false);
                this.cutData.one = true;
                this.cutData.two = false;
                this.cutData.three = false;
                this.newDatas.productType = ""; // 商品属性ID
                this.newDatas.repositoryId = ""; // 入库库位ID
                this.newDatas.repositoryOutId = ""; // 出货库位ID
            }
        }
    },
    created () {
        this.workRepositoryList();
        // this.workSupplierList();
        this.getShopListByCo();
        this.isShow = this.newPopup;
    },
    computed: {
        ...mapGetters([
            "shopListByCo", // 店铺列表
            "repositoryList", // 库位列表
            // "supplierListData" // 供应商
        ])
    },
    methods: {
        ...mapActions([
            "workRepositoryList", // 库位列表
            "getShopListByCo" // 店铺列表
            // "workSupplierList" // 供应商
        ]),
        toPageFun (to, from) { // 去到的， 目前的
            this.cutData[to] = true;
            this.cutData[from] = false;
        },
        getProductType (parm) { // 绑定店铺Id
            var options = {
                "storageId": this.newDatas.repositoryId, // 收货库位
                "shopId": parm
            }
            sessionStorage.setItem("确定新建入库", JSON.stringify(options));
            operateCreateTHReceipt(options).then((response) => { // 新建单据
                console.log("新建单据");
                console.log(response);
                if (response.data.state === 200) {
                    sessionStorage.setItem("orderNumber", response.data.data.orderNum);
                    this.$router.push("/work/salesReturn/newSalesReturn");
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
            this.cutData.one = false;
            this.cutData.two = false;
        },
        // getRepositoryOutId (parm) { // 绑定出库库位id
        //     this.newDatas.repositoryId = parm;
        //     this.cutData.one = false;
        //     this.cutData.two = true;
        // },
        getRepositoryId (parm) { // 绑定入库库位Id
            this.newDatas.repositoryId = parm;
            this.cutData.one = false;
            this.cutData.two = true;
        }
    }
}
</script>
<style src="../../assets/css/template/newReceiptPopup.scss" lang="scss" scoped></style>