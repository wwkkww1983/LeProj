<template>
    <div class="storage">
        <div class="main-box">
            <header class="header-box">
                <div class="header-seek mt20">
                    <ul class="header-operation-btn">
                        <li>
                            <span>单号：<span class="main-color" v-if="receiptDetail">{{receiptDetail.orderNum}}</span></span>
                        </li>
                        <li>
                            <span>商品属性：<span class="main-color" v-if="receiptDetail">{{getproductTpye(receiptDetail.productTpye)}}</span></span>
                        </li>
                        <li>
                            <span>退货库位：<span class="main-color" v-if="receiptDetail">{{receiptDetail.warehouse}}</span></span>
                        </li>
                        <li>
                            <span>备注：<span class="main-color" v-if="receiptDetail">{{receiptDetail.orderRemark}}</span></span>
                        </li>
                    </ul>
                </div>
            </header>
            <section class="body-box">
                <div class="body-table table-template">
<el-checkbox-group v-model="crudData.checkList"> 
                    <table cellpadding="0" cellspacing="0">
                        <thead>
                            <tr class="tr-header">
                                <th v-if="!crudData.operationCut">
                                    <label class="el-checkbox checkbox-font">
                                        <span class="el-checkbox__input is-focus" v-bind:class="{'is-checked': crudData.checked}">
                                            <span class="el-checkbox__inner"></span>
                                        </span>
                                    </label>
                                </th>
                                <th v-if="tittleThs" v-for="tittleTh in tittleThs">{{tittleTh.name}}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-if="showDatas" v-for="(showData, index) in showDatas">
                                <td v-if="!crudData.operationCut"><el-checkbox :label='index+((crudData.currentPage - 1) * 30)' class="checkbox-font"></el-checkbox></td>
                                <td>{{index+1+((crudData.currentPage - 1) * 30)}}</td>
                                <template v-for="key in Object.keys(showData)">
                                    <template v-if='key !== "productId"'>        
                                        <td v-if='key === "status"'>
                                            <p>{{getAuditStatus(showData[key])}}</p>
                                        </td>
                                        <td v-else>
                                            <template v-if='key === "barcode"'>
                                                <p>{{showData[key]}}</p>
                                            </template>
                                            <p v-else>{{showData[key]}}</p>
                                        </td>
                                    </template>
                                </template>
                            </tr>
                        </tbody>
                    </table>
</el-checkbox-group>
                </div>
                <div class="body-page">
                    <div class="paga-30">
                        <el-pagination
                            @size-change="handleSizeChange"
                            @current-change="handleCurrentChange"
                            :current-page="crudData.currentPage"
                            :page-sizes="[30]"
                            :page-size="30"
                            layout="total, sizes, prev, pager, next, jumper"
                            :total="paginationSize">
                        </el-pagination>
                    </div>
                </div>
            </section>
            <footer class="footer-box">
                <div class="footer-affirm">
                    <ul>
                        <li class="confirm-btn-small red" @click="close">关闭</li>
                    </ul>
                </div>
            </footer>
        </div>
    </div>
</template>
<script>
import {mapActions, mapGetters} from 'vuex'
import {productDetailStatus, productTpyeState} from './../../../Api/commonality/status'
export default {
    data () {
        return {
            pickerOptions0: {
                disabledDate(time) {
                    return time.getTime() < Date.now() - 8.64e7;
                }
            },
            tittleThs: [
                {"name": '序号'},
                {"name": '条码号'},
                {"name": '当前状态'},
                {"name": '产品类别'},
                {"name": '首饰名称'},
                {"name": '总件重（g）'},
                {"name": '净金重'},
                {"name": '证书号'},
                {"name": '主石'},
                {"name": '颜色'},
                {"name": '净度'},
                {"name": '副石'},
                {"name": '售价'},
                {"name": '备注'}
            ],
            configData: {
                "productId": null, // 商品id（删除用）
                "barcode": null, // 条形码
                "status": null, // 当前状态
                "cargoType": null, // 产品类别
                "jewelryName": null, // 首饰名称
                "totalWeight": null, // 总件重
                "netWeight": null, // 净金重
                "certifiNo": null, // 证书号
                "mainName": null, // 主石
                "color": null, // 颜色
                "neatNess": null, // 净度
                "deputyName": null, // 副石
                "soldPrice": null, // 售价
                "orderRemark": null// 备注
            },
            newDatas: JSON.parse(sessionStorage.getItem("newDatas")), // 上一步带过来的数据
            header: {
                orderRemark: null, // 单据备注
                shopIdWatch: null, // 监听shopId
                shopName: null, // 分销商名
                orderNumber: null, // 单号
                warehouse: null, // 发货库位
                warehouseId: null, // 发货库位Id
                productTpye: null // 商品属性
            },
            datas: {
                postDatas: [], // 请求回来的原始数据
                postDataList: [], // 请求回来的数据集合(配置好的)
                newGapDatas: [] // 新的空白数据
                // allDataList: [] // 总数据
            },
            crudData: { // 增查更删数据
                currentPage: 1, // 当前页
                checked: false, // 监听全选
                checkList: [], // 选择按钮（选中的数据集合）删除
                operationCut: true // 操作按钮切换
            }
        }
    },
    computed: {
        ...mapGetters([
            "shopLists", // 分销商
            "getProductDetail", // 商品列表（详细）
            "receiptDetail" // 单据详情
        ]),
        paginationSize: function () { // 分页的大小
            return this.allDataList.length;
        },
        allDataList: function () { // 总数据集合
            let productDetail = [];
            if (this.getProductDetail.rowDataList) {
                productDetail = this.configAarryData(this.getProductDetail.rowDataList);
            }
            return [...productDetail, ...this.datas.postDataList, ...this.datas.newGapDatas];
        },
        showDatas: function () { // 最终显示的数据
            let state = ((this.crudData.currentPage - 1) * 30);
            let end = (this.crudData.currentPage * 30);
            return this.allDataList.slice(state, end);
        },
        getCheckedPage: function () { // 监听当页的所有做选择的变量（删除选择）
            var CheckedPage = [];
            let Num = ((this.crudData.currentPage - 1) * 30);
            for (var i = 0; i < this.showDatas.length; i++) {
                CheckedPage.push(Num)
                Num += 1;
            }
            return CheckedPage;
        },
        currentPageChecked: function () { // 抽出当页已经选中的数据集合
            let PageChecked = [];
            for (var i of this.crudData.checkList) {
                var start = (this.crudData.currentPage - 1) * 30;
                var end = start + 29;
                if (start <= i && end >= i) {
                    console.log(i);
                    PageChecked.push(i);
                }
            }
            return PageChecked;
        }
    },
    created () {
        if (this.getProductDetail) {
            console.log("拿到");
            console.log(this.getProductDetail);
            console.log("拿到");
        }
        this.workShopList(); // 店铺列表（计划分销）
        this.judgeProductDetail(); // 如果没有商品列表重新获取一次(避免用户刷新)
        this.workReceiptDetail({
          "orderNumber": this.newDatas.orderNumber
        }); // 单据详情
    },
    watch: {
        'crudData.checkList': function () { // 监听全选
            console.log(this.crudData.checkList.length);
            console.log(this.showDatas.length);
            if (this.currentPageChecked.length === this.showDatas.length) {
                // console.log("zhen")
                this.crudData.checked = true;
            } else {
                // console.log("jia")
                this.crudData.checked = false;
            }
        },
        'crudData.currentPage': function () { // 分页后重新判断全选按钮是否选中
            if (this.currentPageChecked.length === this.showDatas.length) {
                // console.log("zhen")
                this.crudData.checked = true;
            } else {
                // console.log("jia")
                this.crudData.checked = false;
            }
        },
        'receiptDetail': function () { // 监听单据详情
            console.log("监听单据详情");
            this.header.orderNumber = this.receiptDetail.orderNum; // 单号
            this.header.warehouse = this.receiptDetail.warehouse; // 退货库位
            this.header.warehouseId = this.receiptDetail.warehouseId; // 退货库位Id
            this.header.productTpye = this.receiptDetail.productTpye; // 商品属性
        }
    },
    methods: {
        ...mapActions([
            "workProductDetail", // 商品列表
            "workShopList", // 店铺列表（计划分销）
            "receiptAddOrCheck", // 单据新增/提交审核/保存（销售除外）
            "workReceiptDetail" // 单据详情
        ]),
        handleSizeChange (val) {
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange (val) {
            this.crudData.currentPage = val;
            console.log(`当前页: ${val}`);
        },
        judgeProductDetail () { // 如果没有商品列表重新获取一次(避免用户刷新)
            if (!this.getProductDetail.productList) {
                this.workProductDetail({
                    "applyType": "5",
                    "objId": this.newDatas.orderNumber,
                    "type": "1"
                });
            }
        },
        getproductTpye: function (parm) { // 商品属性
            return productTpyeState(parm);
        },
        configAarryData (parm) { // 配置数据为数组
            var configData = [];
            for (let i = 0; i < parm.length; i++) {
                let newAddDatas = {};
                for (let key in this.configData) {
                    newAddDatas[key] = parm[i][key] || null;
                }
                configData.push(newAddDatas)
            }
            return configData;
        },
        getconfigData (parm) { // 得到配置代码
            let configDatas = {};
            for (let key in this.configData) {
                configDatas[key] = parm[key] || null
            }
            return configDatas;
        },
        getAuditStatus (parm) { // 审核状态
            return productDetailStatus(parm);
        },
        close () { // 关闭
            this.$router.push("/work/receiving");
        }
    }
}
</script>
<style lang="scss">
$oa: #0abfab; // 主色
$d6: #d6d6d6; // 分割线颜色
$c3: #333333; // 导航栏文字颜色
$c2: #333742; // 标签栏底色
$es: #e5f5fd; // 标签设置编辑文本 出现旋转框
$f7: #f74c31; // 消息红点颜色
$a8: #a8a8a8; // 搜索页面icon 为选中的底色
$fb: #fb6155; // 删除按钮未选中的底色
$c9: #c9e5e2; // 按钮没选中状态
$f0: #f0f0f0; // 细线颜色
$c9: #999; // 列表内容文字色值
$c6: #666; // 写在标签栏块上的文字图标选中加深颜色
$e6: #e6f8f6; // 选中列表时的状态色
$f6: #f6f6f6; // 没选中列表时的状态色（偶）
$fd: #fd9526; // 按钮图标颜色
$bf: #49bbf3; // 标签设置编辑文本 出现旋转框描边 
$e8: #e8e9eb; // 表格头部的颜色
.storage{
    width: 100%;
    height: 100%;
    padding: 0 20px;
}
.new-product-class{ // 新建产品类别弹窗
    ul{
        font-size: 0;
        margin-bottom: 10px;
        border-bottom: 1px solid $d6;
        margin-top: 10px;
        li:first-child{
            width: 100%;
            height: 16px;
            line-height: 16px;
            font-size: 14px;
            display: block;
            color: $oa;
        }
        li{
            width: 25%;
            height: 50px;
            line-height: 50px;
            font-size: 0;
            display: inline-block;
        }
    }
}
.table-template{
    min-width: 100%;
    height: 100%;
    table{
        width: 100%;
        .tr-header{
            background-color: $e8!important;
            &:hover{
                background-color: $e8;
            }
        }
        tr{
            height: 40px;
            width: 100%;
        }
        tbody{
          tr:nth-child(odd){
              background-color: #fff;
          }
          tr:nth-child(even){
              background-color: $f6;
          }
          tr{
              &:hover{
                  background-color: $e6;
              }
          }
        }
        th,td{
            text-align: center;
            border-right: 1px solid $d6;
            border-bottom: 1px solid $d6;
            font-size: 12px;
            min-width: 70px;
            white-space:nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        td:last-child{
            border-right: 0;
        }
        th{
            color: $c6;
        }
        td{
            color: $c3;
        }
    }
}
.input-default{
    padding: 10px;
    width: 100px;
    height: 20px;
    text-align: center;
    border: none;
    outline: none;
    background-color: transparent;
}
</style>