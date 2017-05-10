<template>
    <div class="storage">
        <div class="main-box">
            <header class="header-box">
<!--                 <div class="header-seek mt20">
                    <ul class="header-operation-btn">
                        <li>
                            <span>单号：</span>
                            <span class="main-color" v-if="receiptDetail">{{receiptDetail.orderNum}}</span>
                        </li>
                        <li>
                            <span>发货库位：</span>
                            <span class="main-color" v-if="receiptDetail">{{receiptDetail.outWarehouse}}</span>
                        </li>
                        <li>
                            <span>备注：</span>
                            <div class="input-w300">     
                                <el-input v-model="header.orderRemark"></el-input>
                            </div>
                        </li>
                    </ul>
                </div> -->
            </header>
            <div class="body-box">
                <!-- 表格头部，内容配置，上一页带过来的数据，头部备注，应用类型，关闭路由 -->
                <detail-template
                    :tittleThs='tittleThs'
                    :configData='configData'
                    :applyType="3"
                    :closeRouterUrl='"/work/sipping"'
                 ></detail-template>
            </div>
        </div>
    </div>
</template>
<script>
import detailTemplate from "./../CommonalityComponent/formTemplate/detailTemplate"
import {mapGetters} from 'vuex'
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
                {"name": '操作'}
            ],
            configData: {
                "productId": "", // 商品id（删除用）
                "barcode": "", // 条形码
                "productType": "", // 产品类别
                "jewelryName": "", // 首饰名称
                "weight": "", // 总件重
                "goldWeight": "", // 净金重
                "certifiNo": "", // 证书号
                "mainName": "", // 主石
                "color": "", // 颜色
                "neatNess": "", // 净度
                "deputyName": "", // 副石
                "price": "" // 售价
            }
        }
    },
    components: {
        detailTemplate
    },
    computed: {
        ...mapGetters([
            "shopLists", // 分销商
            "getProductDetail" // 商品列表（详细）
        ])
    }
}
</script>
<style lang="scss">
.storage{
    width: 100%;
    height: 100%;
    padding: 0 20px;
}
.new-product-class{ // 新建产品类别弹窗
    ul{
        font-size: 0;
        margin-bottom: 10px;
        border-bottom: 1px solid #d6d6d6;
        margin-top: 10px;
        li:first-child{
            width: 100%;
            height: 16px;
            line-height: 16px;
            font-size: 14px;
            display: block;
            color: #0abfab;
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