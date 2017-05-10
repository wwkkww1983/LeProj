<template>
    <el-dialog class="dialog-w540-h320-hn" title="" v-model="isShow">
        <!-- 店铺列表 -->
        <div class="list" v-if="cutData.one">
            <p class="popTitle">
                选择店铺
                <a href="javascript:void(0)" class="skip" @click="toPageFun('two', 'one')">下一步1444</a>
            </p>
            <ul>
                <li :class="{on: newDatas.shopId === item.shopId}" v-for="item in shopListByCo" @click="getShopId(item.shopId)">
                    {{item.shopName}}
                </li>
            </ul>
        </div>
        <!-- 入库库位 -->
        <div class="list" v-if="cutData.two">
            <p class="popTitle">
                <a href="javascript:void(0)" class="prePage" @click="toPageFun('one', 'two')">上一步444</a>
                选择调出柜组
                <a v-if="newDatas.groupId2" href="javascript:void(0)" class="skip" @click="toPageFun('three', 'two')">下一步</a>
            </p>
            <ul>
            <!-- newDatas.repositoryId === item.repositoryId -->
                <li :class="{on: newDatas.groupId2 === item.counterId}" v-for="item in counterList" @click="getRepositoryOutId(item.counterId)">
                    {{item.counterName}}
                </li>
            </ul>
        </div>
         <!-- 出库库位 -->
        <div class="list" v-if="cutData.three">
            <p class="popTitle">
                <a href="javascript:void(0)" class="prePage" @click="toPageFun('two', 'three')">上一步</a>
                选择调入柜组
            </p>
            <ul>
                <li :class="{on: newDatas.groupId === item.counterId}" v-for="item in counterList" @click="getRepositoryId(item.counterId)">
                    {{item.counterName}}
                </li>
            </ul>
        </div>
    </el-dialog>
</template>
<script>
import {mapGetters, mapActions} from "vuex"
import {operateCreateDGReceipt} from './../../Api/commonality/operate'
import {seekShowCounterList} from '../../Api/commonality/seek'
export default {
    data () {
        return {
            "counterList": [], // 柜组列表
            "cutData": {
                "one": true,
                "two": false,
                "three": false
            },
            "newDatas": { // 新增数据
                "shopId": "", // 店铺列表
                // "productType": "", // 商品属性
                "groupId": "", // 调入柜组ID
                "groupId2": "" // 调出柜组ID
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
        },
        'shopListByCo': function () {
            if (this.shopListByCo.length === 1) {
                this.newDatas.shopId = this.shopListByCo[0].shopId;
                this.getCounterList(this.shopListByCo[0].shopId);
                this.cutData.one = false;
                this.cutData.two = true;
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
        getCounterList (parm) { // 柜组列表
            let options = {
                "shopId": parm // 店铺ID
            }
            seekShowCounterList(options).then((response) => {
                console.log("柜组列表");
                console.log(response);
                if (response.data.state === 200) {
                    this.counterList = response.data.data.counterList;
                }
            }, (response) => {
                console.log(response)
            })
        },
        getShopId (parm) {
            this.newDatas.shopId = parm;
            this.getCounterList(parm);
            this.cutData.one = false;
            this.cutData.two = true;
        },
        getRepositoryOutId (parm) { // 绑定出库库位id
            console.log("人品问题");
            console.log(parm);
            this.newDatas.groupId2 = parm;
            this.cutData.two = false;
            this.cutData.three = true;
        },
        getRepositoryId (parm) { // 绑定入库库位Id
            console.log("人品问题2");
            console.log(parm);
            var options = {
                "shopId": this.newDatas.shopId, // 店铺id
                "groupId": parm, // 调入柜组
                "groupId2": this.newDatas.groupId2 // 调出柜组
                // "storageId": this.newDatas.repositoryId, // 入库库位ID
                // "storageId2": this.newDatas.repositoryOutId, // 出货库位ID
                // "productProperty": parm
            }
            console.log(this.newDatas)
            sessionStorage.setItem("确定新建调柜", JSON.stringify(options));
            operateCreateDGReceipt(options).then((response) => { // 新建单据
                console.log("新建单据");
                console.log(response);
                if (response.data.state === 200) {
                    sessionStorage.setItem("orderNumber", response.data.data.orderNum);
                    this.$router.push("/work/transferStorage/newTransferStorage");
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
            // this.cutData.one = false;
            // this.cutData.two = false;
            // this.newDatas.repositoryId = parm;
            // this.cutData.two = false;
            // this.cutData.three = true;
        }
    }
}
</script>
<style src="../../assets/css/template/newReceiptPopup.scss" lang="scss" scoped></style>