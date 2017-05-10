<template>
    <div class="dialog-w540-h320-hn">
        <el-dialog v-model="popup.openPopup">
            <component :is="view" @changeView="choeeseView"  @childData="childmsg"></component>
        </el-dialog>  
    </div>
</template>
<script>
import tep1 from './../components/teps/Tep1'
import tep2 from './../components/teps/Tep2'
import tep3 from './../components/teps/Tep3'
import tep4 from './../components/teps/Tep4'
import {operateAddBackBuyProductToOrder} from './../../../../Api/commonality/operate'
import {seekSellProductDetail} from './../../../../Api/commonality/seek'
    export default {
        data () {
            return {
                popup: { // 弹窗
                    openPopup: true
                },
                view: tep1,
                choosetype: {
                    "operateType": this.chooseMsg,
                    "productTypeId": {},
                    "colorId": {},
                    "jewelId": {},
                    "JewelryId": {}
                }
            }
        },
        props: ['keepMsg', 'chooseMsg'],
        watch: {
            'keepMsg': function () {
                this.popup.openPopup = true;
            }
        },
        created () {
            this.getPopup();
        },
        methods: {
            getPopup () {
                this.popup.openPopup = true;
                console.log(this.chooseMsg)
            },
            choeeseView (i) {
                switch (i) {
                    case 1: // 销售
                        this.popup.openPopup = true;
                        this.view = tep1
                        break
                    case 2: // 退换
                        this.view = tep2
                        break
                    case 3: // 回收
                        this.view = tep3
                        break
                    case 4: // 首饰名称
                        this.view = tep4
                        break
                    case 0: // 首饰名称
                        this.popup.openPopup = false;
                        this.$emit('addInputShow', true);
                        break
                    }
            },
            childmsg (msg) {
                switch (msg.childIndex) {
                    case 1: // 产品
                        this.choosetype.productTypeId = msg;
                        break
                    case 2: // 退换
                        this.choosetype.colorId = msg;
                        break
                    case 3: // 回收
                        this.choosetype.jewelId = msg;
                        break
                    case 4: // 首饰名称
                        this.choosetype.JewelryId = msg;
                        let options = {
                            "orderNum": sessionStorage.getItem("orderNumber"),
                            "operateType": this.choosetype.operateType, // 操作类型1 退换2 回收
                            "productTypeId": this.choosetype.productTypeId.chooseSmall, // 产品类别ID
                            "colorId": this.choosetype.colorId.chooseSmall, // 成色名称ID
                            "jewelId": this.choosetype.jewelId.chooseSmall, // 宝石名称ID
                            "JewelryId": this.choosetype.JewelryId.chooseSmall, // 首饰名称ID
                            "calcMethod": "1", // 计价方式1 计重2 计件
                            "paymentPrice": 1, // 手工费
                            "productProperty": "1", // 商品属性1成品2旧料
                            "goldWeight": 2, // 金重
                            "recycleGoldPrice": 1, // 回购金价
                            "totalWeight": 1, // 总重
                            "abrasion": 0, // 损耗
                            "recyclePrice": 1 // 回购价
                        }
                        sessionStorage.setItem("childDa", JSON.stringify(options));
                        // var bb = sessionStorage.getItem("childDa");
                        // console.log(bb)
                        operateAddBackBuyProductToOrder(options).then((response) => {
                            // console.log(123)
                            console.log(response)
                            let _barcode = response.data.data.barcode
                            let option = {
                                "barcode": _barcode
                            }
                            // console.log(option)
                            seekSellProductDetail(option).then((response) => {
                                console.log(_barcode)
                                console.log(response)
                            }, (response) => {
                                console.log(response)
                            })
                        }, (response) => {
                            console.log(234)
                            console.log(response)
                        })
                        this.popup.openPopup = false;
                        this.$emit('addInputShow', true);
                        break;
                }
            }
        }
    }
</script>
<style></style>