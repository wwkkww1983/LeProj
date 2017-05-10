<template>
    <div class="main-wrap">
        <div class="main-box">
            <header class="header-box">
                <div class="header-seek mt20">
                    <ul class="header-operation-btn">
                        <li>
                            <span>调出柜组：</span>
                            <span class="main-color">{{newDatas.outShopGroupName}}</span>
                        </li>
                        <li>
                            <span>商品属性：</span>
                            <span class="main-color">{{getproductTpye(newDatas.productTypeName)}}</span>
                        </li>
                        <li>
                            <i class="must-star"></i>
                            <span>调入柜组：</span>
                            <div class="select-w100">
                                <el-select filterable v-model="header.outShopGroupId" placeholder="">
                                    <el-option v-for="item in counterList"
                                        :label="item.counterName"
                                        :value="item.counterId">
                                        <span @click="getName(item.counterName)">{{ item.counterName }}</span>
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <span>备注：</span>
                            <div class="input-w300">     
                                <el-input v-model="header.orderRemark"></el-input>
                            </div>
                        </li>
                    </ul>
                </div>
            </header>
            <div class="body-box">    
                <!-- 表格头部，内容配置，上一页带过来的数据，头部备注，应用类型，关闭路由 -->
                <new-template
                    :tittleThs='tittleThs'
                    :configData='configData'
                    :newDatas='newDatas'
                    :header='header'
                    :applyType="8"
                    :switchId="switchId"
                    :closeRouterUrl='"/work/transferCabinet"'
                    :detailRouterUrl='"/work/transferCabinet/detailTransferCabinet"'
                 ></new-template>
            </div>
        </div>
    </div>
</template>
<script>
import newTemplate from './../CommonalityComponent/formTemplate/NewTemplate'
import {productTpyeState} from './../../../Api/commonality/status'
import {mapActions, mapGetters} from 'vuex'
export default {
    data () {
        return {
            pickerOptions0: {
                disabledDate(time) {
                    return time.getTime() < Date.now() - 8.64e7;
                }
            },
            tittleThs: [
                {"name": "序号"},
                {"name": "条码号"},
                {"name": "当前状态"},
                {"name": "产品类别"},
                {"name": "首饰名称"},
                {"name": "总件重（g）"},
                {"name": "净金重（g）"},
                {"name": "证书号"},
                {"name": "主石"},
                {"name": "颜色"},
                {"name": "净度"},
                {"name": "副石"},
                {"name": "售价（元）"},
                {"name": "备注"}
            ],
            configData: {
                "barcode": null, // 条码号
                "status": null, // 当前状态
                "productType": null, // 产品类别
                "jewelryName": null, // 首饰名称
                "totalWeight": null, // 总件重
                "netWeight": null, // 净金重
                "certifiNo": null, // 证书号
                "mainName": null, // 主石
                "color": null, // 颜色
                "neatNess": null, // 净度
                "deputyName": null, // 副石
                "soldPrice": null, // 售价
                "remark": null// 备注
            },
            newDatas: JSON.parse(sessionStorage.getItem("newDatas")), // 上一步带过来的数据
            header: {
                orderRemark: null, // 单据备注
                outShopGroupId: null, // 调出柜组ID
                outShopGroupName: null // 调出柜组名
            }
        }
    },
    components: {
        newTemplate
    },
    computed: {
        ...mapGetters([
            "counterList" // 柜组列表
            // "shopLists" // 分销商
            // "repositoryList" // 库位列表
        ]),
        switchId: function () {
            return this.header.outShopGroupId;
        }
    },
    created () {
        this.workCounterList(this.newDatas.handleDeptId); // 柜组列表
        // this.workRepositoryList(); // 库位列表
    },
    // watch: {
    //     'header.outShopGroupId': function () {
    //         for (let i = 0; i < this.shopLists.length; i++) {
    //             if (this.shopLists[i].outShopGroupId === this.header.outShopGroupId) {
    //                 this.header.outShopGroupName = this.shopLists[i].outShopGroupName;
    //             }
    //         }
    //     }
    // },
    methods: {
        ...mapActions([
            "workCounterList" // 柜组列表
            // "workShopList" // 店铺列表（计划分销）
            // "workRepositoryList" // 库位列表
        ]),
        getproductTpye: function (parm) { // 商品属性
            return productTpyeState(parm);
        },
        getName (parm) { // 绑定分销商名称
            this.header.outShopGroupName = parm;
        }
    }
}
</script>
<style lang="scss" scoped>
.main-wrap{
    width: 100%;
    height: 100%;
    padding: 0 20px;
}
</style>