<template>
    <div class="storage">
        <div class="main-box">
            <header class="main-box-title">
                公司设置
            </header>
            <div class="main-box-info">
                <div v-if="!companyId" class="noList">
                    您没有加入任何公司
                </div>
                <div v-else>
                    <div class="aside">
                        <el-menu default-active="0" class="el-menu-vertical-demo" @select="selectItem">
                            <div v-for="(item, index) in catalog">
                            <el-menu-item v-if="item.catalogList === ''" :index="index + ''">{{item.name}}</el-menu-item>
                            <el-submenu v-else  :index="index + ''">
                                <template slot="title">{{item.name}}</template>
                                <el-menu-item  v-for="(name, i) in item.catalogList" :index="index + '-' + i">{{name}}</el-menu-item>
                            </el-submenu>
                            </div>
                        </el-menu>
                    </div>
                    <div class="main-box-content">
                        <component :is="panel"></component> 
                    </div>
                </div>
            </div>
            <div class="dialog-w380-h420">
                <el-dialog title="新增产品类型" v-model="popup.newPopup">
                    <p class="m20">
                        <el-input v-model="input" placeholder="请输入 品牌" :maxlength="7"></el-input>                        
                    </p>
                    <span slot="footer">
                        <a href="javascript: null" class="cancel-btn-w120" @click="popup.newPopup=false">取 消</a>
                        <a href="javascript: null" class="confirm-btn-w120 mr0" @click="affirmNew" >确 定</a>
                    </span>
                </el-dialog>
            </div>
            <!-- 弹窗 -->
            <error></error>
            </div>
        </div>
    </div>
</template>
<script>
import {mapActions, mapGetters} from 'vuex'
import tableTemplate from "./../CommonalityComponent/tableTemplate"
import error from "./../CommonalityComponent/popupTemplate/error"
import ProductName from "./settings/ProductName"
import GemName from "./settings/GemName"
import ConditionName from "./settings/ConditionName"
import JewelryType from "./settings/JewelryType"
import GemAttribute from "./settings/GemAttribute"
import GoldContent from "./settings/GoldContent"
import PartsName from "./settings/PartsName"
import BrandList from "./settings/BrandList"
import MetalColor from "./settings/MetalColor"
import Certificate from "./settings/Certificate"
import LibrarySetting from "./settings/LibrarySetting"
import SupplierSetting from "./settings/SupplierSetting"
import menu from './menu.json'
export default {
    data () {
        return {
            catalog: menu.catalog,
            kwset: null,
            proList: null,
            isEidt: false,
            isShow: true,
            panel: null,
            newDatas: { // 新增数据(其它页面也用到的)
            },
            popup: { // 弹窗
                newPopup: false // 产品类型
            },
            input: "",
            proBrand: null // 品牌
        }
    },
    created () {
        this.workRepositoryList(); // 库位列表
        this.workProductClass(); // 产品类别
    },
    components: {
        tableTemplate,
        error,
        ProductName,
        GemName,
        ConditionName,
        JewelryType,
        GemAttribute,
        GoldContent,
        PartsName,
        BrandList,
        MetalColor,
        Certificate,
        LibrarySetting,
        SupplierSetting
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
        },
        companyId () {
            return sessionStorage.getItem('companyId')
        }
    },
    methods: {
        ...mapActions([
            "workProductClass", // 产品类别
            "workRepositoryList" // 库位列表
        ]),
        selectItem (index) {
            switch (index) {
                case '0-0':
                    this.panel = ProductName
                    break
                case '0-1':
                    this.panel = ConditionName
                    break
                case '0-2':
                    this.panel = GoldContent
                    break
                case '0-3':
                    this.panel = GemName
                    break
                case '0-4':
                    this.panel = JewelryType
                    break
                case '0-5':
                    this.panel = BrandList
                    break
                case '0-6':
                    this.panel = MetalColor
                    break
                case '0-7':
                    this.panel = Certificate
                    break
                case '0-8':
                    this.panel = GemAttribute
                    break
                case '0-9':
                    this.panel = PartsName
                    break
                case '1':
                    this.panel = SupplierSetting
                    break
                case '2':
                    this.panel = LibrarySetting
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
        resetFun () { // 重置
        },
        seekFun () { // 查询
            this.workProductList({
            })
            alert("查询")
        },
        checkCata () {
            this.productColor();
        },
        getReceiving () { // 获取可收获数据
            this.workProductList({ //  获取单据列表
            })
        },
        setUserType (parm) { // 监听编辑权限
            // this.newDatas.userType = parm;
        },
        // cancelDelect () { // 取消删除
        //     this.crudData.delectSelectSucces = true;
        //     this.crudData.operationCut = true;
        // },
        getReceivingOrderId () { // 获取所有收获Id
            console.log("999")
        },
        productList () {
            var data = {
                "data": {
                    "type": "2",
                    "userId": sessionStorage.getItem("id")
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
            // var url = INTERFACE_URL_9083 + "/v1/goods/getProductTypeList";
            sessionStorage.setItem("aaa", JSON.stringify(data));
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showProductClassesList";
            this.$http.post(url, data).then((response) => {
                console.log("产品名称");
                console.log(response.data.data.list);
                this.proList = response.data.data.list;
            }, (response) => {
                console.log(response);
            })
        },
        getConfigValue (parm) { // 配置产品类别
            let configName = null;
            switch (parm) {
                case '1':
                    configName = "素金类（计重）";
                    break;
                case '2':
                    configName = "珠宝类（计件）";
                    break;
                case '3':
                    configName = "饰品类（银饰 / 饰品）"
                    break;
            }
            return configName;
        },
        moreSetting () {
            var data = {
                "data": {
                    "type": "2",
                    "userId": sessionStorage.getItem("id")
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showRepositoryList";
            this.$http.post(url, data).then((response) => {
                console.log("库位设置");
                console.log(response.data.data.repositoryList);
                this.kwset = response.data.data.repositoryList;
            }, (response) => {
                console.log(response);
            })
        },
        productColor () {
            var data = {
                "data": {
                    "type": "1",
                    "userId": sessionStorage.getItem("id")
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showProductPropertyList";
            this.$http.post(url, data).then((response) => {
                console.log("品牌");
                // console.log(response.data.data.list);
                console.log(response.data.data.list);
                this.proBrand = response.data.data.list;
            }, (response) => {
                console.log(response);
            })
        },
        newStockIn () { // 新建产品类别
            this.popup.newPopup = true;
        },
        getsippingLocation (parm) { // 绑定库位名
            // this.newDatas.sippingLocation = parm;
            console.log(parm)
        },
        affirmNew () { // 确定新增
            if (!this.newDatas) {
                alert("此处不能为空")
                return;
            } else {
                this.popup.productClass = false;
                // 存本地可以防止刷新
                // this.newDatas.userType = this.userType; // 监听编辑权限（部门）
                sessionStorage.setItem("newStockData", JSON.stringify(this.newDatas));
                // this.$router.push('/work/sipping/newSipping');
            }
            this.popup.productClass = false;
        },
        submitAudit (parm) { // 提交审核
            console.log("提交审核");
            console.log(parm);
        }
    }
}
</script>
<style src="./CompanySetting.scss" lang="scss" scoped></style>
