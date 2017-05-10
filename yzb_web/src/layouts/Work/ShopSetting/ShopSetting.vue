<template>
    <div class="storage">
        <div class="main-box">
            <header class="main-box-title">
                店铺设置
                <el-select v-model="shopStorage" placeholder="请选择店铺" @change="getGroupList">
                    <el-option
                    v-for="item in thisShopList"
                    :label="item.shopName"
                    :value="item.shopId">
                    </el-option>
                </el-select>
            </header>
            <div class="main-box-info">
                <div class="noList" v-if="isShopList">
                您没有加入任何店铺
                </div>
                <div v-else>
                    <ul class="aside">
                        <el-menu default-active="0" class="el-menu-vertical-demo" @select="selectItem">
                            <div v-for="(item, index) in catalog">
                            <el-menu-item v-if="item.catalogList === ''" :index="index + ''">{{item.name}}</el-menu-item>
                            <el-submenu v-else  :index="index + ''">
                                <template slot="title">{{item.name}}</template>
                                <el-menu-item  v-for="(name, i) in item.catalogList" :index="index + '-' + i">{{name}}</el-menu-item>
                            </el-submenu>
                            </div>
                        </el-menu>
                    </ul>
                    <div class="main-box-content">
                        <component :is="panel" :shopStorage='shopStorage'></component> 
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex'
import GroupSetting from "./settings/GroupSetting"
import menu from './menu.json'
export default {
    data () {
        return {
            catalog: menu.catalog,
            thisShopList: null,
            isEidt: false,
            isShow: true,
            panel: null,
            shopStorage: this.thisShopList, // 店铺
            isShopList: null
        }
    },
    created () {
        this.getShopList(); // 获取店铺列表
    },
    components: {
        GroupSetting
    },
    computed: {
        ...mapGetters([
            "repositoryList", // 库位列表
            "productClass", // 产品类别
            "supplierListData", // 供应商列表
            // "userType", // 监听编辑权限
            "applyUserList" // 应用用户列表
        ]),
        productClassPull: function () {
            var productClass = [];
            for (var i = 0; i < this.productClass.length; i++) {
                productClass.push(...this.productClass[i].typeList)
            }
            return productClass;
        }
    },
    methods: {
        ...mapActions([
            "workRepositoryList" // 库位列表
        ]),
        selectItem (index) {
            switch (index) {
                case '0':
                    this.panel = GroupSetting
                    break
            }
        },
        addFun () { // 添加
            this.isEidt = true;
            this.isShow = !this.isEidt;
        },
        finishFun () { // 完成
            this.isEidt = false;
            this.isShow = !this.isEidt;
        },
        seekFun () { // 查询
            this.workProductList({
            })
            alert("查询")
        },
        getShopList () {
            var data = {
                "data": {
                    "type": "1",
                    "userId": sessionStorage.getItem("id"),
                    "shopId": this.thisShopId // 店铺ID
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showShopList";
            this.$http.post(url, data).then((response) => {
                console.log("所有店铺");
                console.log(response.data.data.shopList);
                this.thisShopList = response.data.data.shopList;
                if (this.thisShopList[0]) {
                    this.isShopList = false;
                } else {
                    this.isShopList = true;
                }
            }, (response) => {
                console.log(response);
            })
        },
        getGroupList () {
            console.log(this.shopStorage)
            this.panel = null
        }
    }
}
</script>
<style src="../CompanySetting/CompanySetting.scss" lang="scss" scoped></style>
