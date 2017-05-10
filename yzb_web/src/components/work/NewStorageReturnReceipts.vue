<template>
    <el-dialog class="dialog-w540-h320-hn" title="" v-model="isShow">
        <!-- 退库库位 -->
        <div class="list" v-if="cutData.one">
            <p class="popTitle">
                <a href="javascript:void(0)" class="prePage" @click="toPageFun('one', 'two')">上一步</a>
                选择退库库位
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
                选择选择供应商
                <a href="javascript:void(0)" class="skip" @click="toPageFun('two', 'one')">下一步</a>
            </p>
            <ul>
                <li :class="{on: newDatas.supplierId === item.supplierId}" v-for="item in supplierListData" @click="getSupplierId(item.supplierId)">
                    {{item.supplierName}}
                </li>
            </ul>
        </div>
        <!-- 商品属性 -->
        <div class="list" v-if="cutData.three">
            <p class="popTitle">
                <a href="javascript:void(0)" class="prePage" @click="toPageFun('one', 'two')">上一步</a>
                选择商品属性
            </p>
            <ul>
                <li :class="{on: newDatas.productType === item.type}" v-for="item in productTypeList" @click="getType(item.type)">
                    {{item.name}}
                </li>
            </ul>
        </div>
    </el-dialog>
</template>
<script>
import {mapGetters, mapActions} from "vuex"
import {operateCreateTKReceipt} from './../../Api/commonality/operate'
export default {
    data () {
        return {
            "cutData": {
                "one": true,
                "two": false
            },
            "newDatas": { // 新增数据
                "repositoryId": "", // 库位id
                "supplierId": "", // 供应商ID
                "productType": "" // 调入柜组ID
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
                this.newDatas.productType = ""; // 商品属性ID
                this.newDatas.repositoryId = ""; // 入库库位ID
                this.newDatas.repositoryOutId = ""; // 出货库位ID
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
            "repositoryList", // 库位列表
            "supplierListData" // 供应商
        ])
    },
    methods: {
        ...mapActions([
            "workRepositoryList", // 库位列表
            "workSupplierList" // 供应商
        ]),
        toPageFun (to, from) { // 去到的， 目前的
            this.cutData[to] = true;
            this.cutData[from] = false;
        },
        getRepositoryId (parm) { // 退货库位
            this.newDatas.repositoryId = parm;
            if (this.supplierListData.length > 0) { // 有店铺
                this.cutData.one = false;
                this.cutData.two = true;
            } else {
                this.cutData.one = false;
                this.cutData.three = true;
            }
        },
        getSupplierId (parm) { // 绑定供应商
            this.newDatas.supplierId = parm;
            this.cutData.two = false;
            this.cutData.three = true;
        },
        getType (parm) { // 绑定入库库位Id
            console.log("人品问题2");
            console.log(parm);
            var options = {
                "storageId": this.newDatas.repositoryId, // 退货库位
                "supplierId": this.newDatas.supplierId, // 供应商
                "productProperty": parm, // 调入柜组
            }
            console.log(this.newDatas)
            sessionStorage.setItem("确定新建入库", JSON.stringify(options));
            operateCreateTKReceipt(options).then((response) => { // 新建单据
                console.log("新建单据");
                console.log(response);
                if (response.data.state === 200) {
                    sessionStorage.setItem("orderNumber", response.data.data.orderNum);
                    this.$router.push("/work/storageReturn/newStorageReturn");
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
<style src="../../assets/css/template/newReceiptPopup.scss" lang="scss" scoped></style>