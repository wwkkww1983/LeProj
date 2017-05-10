<template>
    <el-dialog class="dialog-w540-h320-hn" title="" v-model="isShow">
        <!-- 库位列表 -->
<!--         <div class="list" v-if="cutData.one">
            <p class="popTitle">
                选择库位
            </p>
            <ul>
                <li v-for="item in repositoryList" @click="getRepositoryId(item.repositoryId)">
                    {{item.repositoryName}}
                </li>
            </ul>
        </div> -->
        <!-- 店铺列表 -->
        <div class="list">
            <p class="popTitle">
                <a href="javascript:void(0)" class="prePage" @click="toPageFun('one', 'two')">上一步</a>
                选择店铺
            </p>
            <ul>
                <li v-for="item in shopListByCo" @click="getShopId(item.shopId)">
                    {{item.shopName}}
                </li>
            </ul>
        </div>
    </el-dialog>
</template>
<script>
import {mapGetters, mapActions} from "vuex"
import {operateCreateSellOrder} from './../../Api/commonality/operate'
export default {
    data () {
        return {
            "productTypeList": null,
            "productTypeListSmall": null,
            "transform": true,
            "fourBig": false,
            "bigStyle": {},
            "viewBig": 2,
            "chooseData": {
                "childIndex": 1,
                "chooseBig": "",
                "chooseSmall": ""
            },
            "cutData": {
                "one": true,
                "two": false,
                "three": false,
                "four": false
            },
            "newDatas": { // 新增数据(其它页面也用到的)
                "shopId": "", // 店铺ID
                // "supplierId": "", // 供应商ID
                // "productTypeId": "", // 产品类别ID
                "repositoryId": "" // 库位ID
            },
            "isShow": false
        }
    },
    props: [
        'newPopup'
    ],
    watch: {
        'newPopup': function () {
            console.log(this.newPopup)
            if (this.newPopup === true) {
                this.isShow = this.newPopup;
            }
        },
        'isShow': function () {
            if (this.isShow === false) {
                this.$emit("closePopup", false)
            }
        }
    },
    created () {
        this.workRepositoryList();
        this.workSupplierList();
        this.isShow = this.newPopup;
    },
    computed: {
        ...mapGetters([
            "shopListByCo", // 店铺列表
            "repositoryList", // 库位列表
            "supplierListData" // 供应商
        ])
    },
    methods: {
        ...mapActions([
            "workRepositoryList", // 库位列表
            "getShopListByCo", // 店铺列表
            "workSupplierList" // 供应商
        ]),
        toPageFun (to, from) { // 去到的， 目前的
            this.cutData[to] = true;
            this.cutData[from] = false;
        },
        getShopId (parm) { // 绑定店铺Id
            let options = {
                shopId: parm
            }
            operateCreateSellOrder(options).then((response) => { // 获取单号
                console.log("获取单号")
                console.log(response);
                if (response.data.state === 200) {
                    sessionStorage.setItem("orderNumber", response.data.data.orderNum);
                    this.$router.push("/work/sell/sellReceiptsList");
                } else {
                    alert(response.data.msg)
                }
            }, (response) => {
                console.log(response);
            })
            // var options = {
            //     "storageId": this.newDatas.repositoryId, // 库位ID
            //     "shopId": parm // 店铺
            // }
            // sessionStorage.setItem("确定新建入库", JSON.stringify(options));
            // operateCreateSellOrder(options).then((response) => { // 新建单据
            //     console.log("新建单据");
            //     console.log(response);
            //     if (response.data.state === 200) {
            //         sessionStorage.setItem("orderNumber", response.data.data.orderNum);
            //         this.$router.push("/work/sipping/newSipping");
            //     } else {
            //         alert(response.data.msg);
            //     }
            // }, (response) => {
            //     console.log(response);
            // })
            // this.cutData.one = false;
            // this.cutData.two = false;
        },
        getRepositoryId (parm) { // 绑定库位Id
            this.newDatas.repositoryId = parm;
            this.cutData.one = false;
            this.cutData.two = true;
        }
    }
}
</script>
<style src="../../assets/css/template/newReceiptPopup.scss" lang="scss" scoped></style>