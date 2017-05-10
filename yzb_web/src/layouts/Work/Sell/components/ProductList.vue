<template>
    <div class="product-list">
        <!-- 销售  -->
        <div class="sell-list-body mt20" @click="beginEdit(i, item)" v-if="sellProductList.saleList" v-for="(item, i) in sellProductList.saleList">
            <div>          
                <ul>
                    <li class="main-color"><span>产品名称：{{item.jewelryName}}</span><img src="./../../../../assets/img/work/saleList.png" alt=""></li>
                    <li>条码号：{{item.barcode}}</li>
                </ul>
                <ul>                    
                    <li>总件重：{{item.totalWeight}}</li>
                    <li v-show="item.goldWeight">金重：{{item.goldWeight}}g</li>
                    <li v-show="!item.goldWeight">成色名称：{{item.colorName}}</li>
                </ul>
                <ul>                    
                    <li>证书号：{{item.certifiNo}}</li>
                </ul>
                <ul>
                    <li v-show="item.goldWeight">销售金价：
                        <div class="hide-input" v-show="!isInput">{{item.saleGoldPrice}}</div>
                        <input type="text" v-show="isInput" :value="item.saleGoldPrice" @change="setSaleGoldPrice($event, i)" />
                    </li>
                    <li v-show="!item.goldWeight">原售价：
                        <div class="hide-input" v-show="!isInput">{{item.oldPrice}}</div>
                        <input type="text" v-show="isInput" :value="item.oldPrice" @change="setOldPrice($event, i)" />元/件
                    </li>
                    <li>折扣：
                        <div class="hide-input" v-show="!isInput">{{item.discount}}</div>
                        <input type="text" v-show="isInput" :value="item.discount" @change="setDiscount($event, i)" />
                    </li>
                    <li>手工费：
                        <div class="hide-input" v-show="!isInput">
                            {{item.paymentPrice}}                            
                        </div>
                        <input type="text" v-show="isInput" :value="item.paymentPrice" @change="setPaymentPrice($event, i)" />
                        <span v-show="cloneSellProductList[i] !== true">元/g</span>
                        <span v-show="cloneSellProductList[i] === true">元/件</span>                        
                    </li>
                    <li>实售价：
                        <div class="hide-input" v-show="!isInput">{{item.newPrice}}</div>
                        <input type="text" v-show="isInput" :value="item.newPrice" @change="setNewPrice($event, i)" />元
                    </li>
                </ul>
                <div class="sell-switch">
                    <el-switch v-show="item.saleGoldPrice" v-model="cloneSellProductList[i]" @change="setCalcMethod($event, i, 1)" on-text="计件" off-text="计重" on-color="#199ED8" off-color="#199ED8"></el-switch>
                </div>
                <a class="sell-delect-btn" href="javascript:void(0)" @click.stop="delectProduct(item.barcode ,i)">删除</a>
            </div>   
        </div>      
        <!-- 退换 -->
        <div class="sell-list-body mt20" @click="beginEdit((sellProductList.saleList.length+i), item)" v-if="sellProductList.exchangeList.length > 0" v-for="(item, i) in sellProductList.exchangeList">
            <div>
                <ul>
                    <li class="main-color"><span>产品类别：{{item.jewelryName+item.colorName+item.jewelName}}</span><img src="./../../../../assets/img/work/exchangeList.png" alt=""></li>
                    <li>条码号：{{item.barcode}}</li>
                </ul>
                <ul>
                    <li>首饰名称：{{item.jewelryName}}</li>
                    <li>成色名称：{{item.colorName}}</li>
                    <li>珠宝名称：{{item.jewelName}}</li>
                </ul>
                <ul>
                    <li>总件重：{{item.totalWeight||0}}</li>
                    <li v-show="item.goldWeight">金重：{{item.goldWeight}}</li>
                </ul>
                <ul>
                    <li v-show="item.goldWeight">回购金价：
                        <div class="hide-input" v-show="!isInput">{{item.exchangeGoldPrice||0}}</div>
                        <input type="text" v-show="isInput" :value="item.exchangeGoldPrice||0" @change="setExGoldPrice($event, i)"/>
                    </li>
                    <li v-show="!item.goldWeight">估价：
                        <div class="hide-input" v-show="!isInput">{{item.estimatePrice||0}}</div>
                        <input type="text" v-show="isInput" :value="item.estimatePrice||0" @change="setEstimatePrice($event, i)"/>
                    </li>
                    <li>损耗：
                        <div class="hide-input" v-show="!isInput">{{item.abrasion||0}}</div>
                        <input type="text" v-show="isInput" :value="item.abrasion||0" @change="setExAbrasion($event, i)"/>
                    </li>
                    <li>手工费：
                        <div class="hide-input" v-show="!isInput">{{item.paymentPrice||0}}</div>
                        <input type="text" v-show="isInput" :value="item.paymentPrice||0" @change="setExPaymentPrice($event, i)"/>
                        <span v-show="cloneExchangeList[i] !== true && item.goldWeight">元/g</span>
                        <span v-show="cloneExchangeList[i] !== true && !item.goldWeight">元/件</span>
                        <span v-show="cloneExchangeList[i] === true">元/件</span>
                    </li>
                    <li>退换价：
                        <div class="hide-input" v-show="!isInput">{{item.newPrice||0}}</div>
                        <input type="text" v-show="isInput" :value="item.newPrice||0" @change="setExNewPrice($event, i)"/>元
                    </li>
                </ul>
            </div>
            <div class="sell-switch">
                <el-switch v-show="item.goldWeight" v-model="cloneExchangeList[i]" @change="setCalcMethod($event, i, 2)" on-text="计件" off-text="计重" on-color="#199ED8" off-color="#199ED8"></el-switch>
            </div>
            <a class="sell-delect-btn" href="javascript:void(0)" @click.stop="delectProduct(item.barcode)">删除</a>
        </div>
        <!-- 回收 -->
        <div class="sell-list-body mt20" @click="beginEdit((sellProductList.saleList.length + sellProductList.exchangeList.length + i), item)" v-if="sellProductList.recycleList.length > 0" v-for="(item, i) in sellProductList.recycleList">
            <div>
                <ul>
                    <li class="main-color"><span>产品类别：{{item.jewelryName+item.colorName+item.jewelName}}</span><img src="./../../../../assets/img/work/recycleList.png" alt=""></li>
                    <li>条形码：{{item.barcode}}</li>                    
                </ul>
                <ul>
                    <li>首饰名称：{{item.jewelryName}}</li>
                    <li>成色名称：{{item.colorName}}</li>
                    <li>珠宝名称：{{item.jewelName}}</li>        
                </ul>
                <ul>
                    <li>总件重：
                        <div class="hide-input" v-show="!isInput">{{item.totalWeight}}</div>
                        <input type="text" v-show="isInput" v-model="item.totalWeight"/>
                    </li>
                    <li v-show="item.goldWeight">金重：{{item.goldWeight}}</li>
                </ul>
                <ul>
                    <li v-show="item.goldWeight">回购金价：
                        <div class="hide-input" v-show="!isInput">{{item.recycleGoldPrice}}</div>
                        <input type="text" v-show="isInput" :value="item.recycleGoldPrice" @change="setReGoldPrice($event, i)"/>
                    </li>
                    <li v-show="!item.goldWeight">估价：
                        <div class="hide-input" v-show="!isInput">{{item.estimatePrice}}</div>
                        <input type="text" v-show="isInput" :value="item.estimatePrice" @change="setReEstimatePrice($event, i)"/>
                    </li>
                    <li>损耗：
                        <div class="hide-input" v-show="!isInput">{{item.abrasion||0}}</div>
                        <input type="text" v-show="isInput" :value="item.abrasion||0" @change="setReAbrasion($event, i)"/>
                    </li>
                    <li>手工费：
                        <div class="hide-input" v-show="!isInput">{{item.paymentPrice}}</div>
                        <input type="text" v-show="isInput" :value="item.paymentPrice" @change="setRePaymentPrice($event, i)"/>
                        <span v-show="cloneRecycleList[i] !== true">元/g</span>
                        <span v-show="cloneRecycleList[i] === true">元/件</span>
                    </li>
                    <li>回收价：
                        <div class="hide-input" v-show="!isInput">{{item.newPrice}}</div>
                        <input type="text" v-show="isInput" :value="item.newPrice" @change="setReNewPrice($event, i)"/>元
                    </li>
                </ul>
            </div>
            <div class="sell-switch">
                <el-switch v-model="cloneRecycleList[i]" @change="setCalcMethod($event, i, 3)" on-text="计件" off-text="计重" on-color="#199ED8" off-color="#199ED8"></el-switch>
            </div>      
            <a class="sell-delect-btn" href="javascript:void(0)" @click="delectProduct(item.barcode)">删除</a>
        </div>
    </div>
</template>
<script>
import {mapGetters} from "vuex"
import {operateProductList, operateUpdateSell} from "./../../../../Api/commonality/operate.js"
// import {seekSellProductDetail} from './../../../../Api/commonality/seek'
export default {
    data () {
        return {
            xiao: null,
            calcMethod: true,
            List: {
                // "exchangeList": [{ // 退换
                //     "jewelryName": "黄金戒指",
                //     "barcode": "12345678",
                //     "totalWeight": 3,
                //     "goldWeight": 2,
                //     "certifiNo": "ZS147587",
                //     "exchangeGoldPrice": 1000,
                //     "abrasion": 0.9,
                //     "paymentPrice": 15,
                //     "calcMethod": "1",
                //     "oldPrice": 5000,
                //     "newPrice": 4500,
                //     "price": 4500,
                //     "productType": "退换戒指"
                // }],
                // "saleList": [{ // 销售
                //     "jewelryName": "黄金戒指",
                //     "barcode": "12345678",
                //     "totalWeight": 3,
                //     "goldWeight": 2,
                //     "certifiNo": "ZS147587",
                //     "saleGoldPrice": 200,
                //     "discount": 0.9,
                //     "paymentPrice": 15,
                //     "calcMethod": "1",
                //     "oldPrice": 5000,
                //     "newPrice": 4500,
                //     "price": 4500
                // }],
                // "recycleList": [{ // 回收
                //     "jewelryName": "黄金戒指",
                //     "barcode": "112255",
                //     "totalWeight": 3,
                //     "goldWeight": 0,
                //     "certifiNo": "ZS147258369",
                //     "recycleGoldPrice": 200,
                //     "abrasion": 0.9,
                //     "paymentPrice": 15,
                //     "calcMethod": "2",
                //     "oldPrice": 5000,
                //     "newPrice": 4500,
                //     "price": 4500,
                //     "productType": "回收戒指",
                //     "estimatePrice": 2000
                // }]
            },
            isInput: false,
            todayGoldPrice: null
        }
    },
    created () {
        // this.yisha(i);
        this.getABC()
    },
    computed: {
        ...mapGetters([
            'sellProductList'
        ]),
        showData () {
            var bb = sellProductList;
            bb.unshift()
            return bb;
        },
        cloneExchangeList () {
            let tempArr = []
            let a = false;
            for (let item of this.sellProductList.exchangeList) {
                if (item.calcMethod === '2') {
                    a = true
                } else {
                    a = false
                }
                tempArr.push(a)
            }
            return tempArr
        },
        cloneRecycleList () {
            let tempArr = []
            let a = false;
            for (let item of this.sellProductList.recycleList) {
                if (item.calcMethod === '2') {
                    a = true
                } else {
                    a = false
                }
                tempArr.push(a)
            }
            return tempArr
        },
        cloneSellProductList () {
            let tempArr = []
            let a = false;
            for (let item of this.sellProductList.saleList) {
                if (item.calcMethod === '2') {
                    a = true
                } else {
                    a = false
                }
                tempArr.push(a)
            }
            return tempArr
        }
    },
    methods: {
        setCalcMethod (e, i, number) { // 设置计价方式
            let calcMethod = e ? "1" : "2"
            if (number === 1) {
                let item = this.sellProductList.saleList[i]
                this.$store.commit('SELL_PRODUCT_LIST_CALCMETHOD', {
                    index: i,
                    calcMethod: calcMethod
                })
                if (e) {
                    this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                        index: i,
                        newPrice: (((Number(item.saleGoldPrice) + Number(item.paymentPrice)) * item.goldWeight) * item.discount).toFixed(2)
                    })
                } else {
                    this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                        index: i,
                        newPrice: ((item.saleGoldPrice * item.goldWeight + item.paymentPrice) * item.discount).toFixed(2)
                    })
                }
            } else if (number === 2) {
                let item = this.sellProductList.exchangeList[i]
                this.$store.commit('SELL_PRODUCT_LIST_EX_CALCMETHOD', {
                    index: i,
                    calcMethod: calcMethod
                })
                if (e) {
                    this.$store.commit('SELL_PRODUCT_LIST_EX_NEWPRICE', {
                        index: i,
                        newPrice: (item.goldWeight * item.exchangeGoldPrice * Number(Number(1 - item.abrasion).toFixed(2)) - item.goldWeight * item.paymentPrice).toFixed(2)
                    })
                } else {
                    this.$store.commit('SELL_PRODUCT_LIST_EX_NEWPRICE', {
                        index: i,
                        newPrice: (item.goldWeight * item.exchangeGoldPrice * Number(Number(1 - item.abrasion).toFixed(2)) - item.paymentPrice).toFixed(2)
                    })
                }
            } else {
                let item = this.sellProductList.recycleList[i]
                this.$store.commit('SELL_PRODUCT_LIST_RE_CALCMETHOD', {
                    index: i,
                    calcMethod: calcMethod
                })
                if (e) {
                    this.$store.commit('SELL_PRODUCT_LIST_RE_NEWPRICE', {
                        index: i,
                        newPrice: (item.goldWeight * item.recycleGoldPrice * Number(Number(1 - item.abrasion).toFixed(2)) - item.goldWeight * item.paymentPrice).toFixed(2)
                    })
                } else {
                    this.$store.commit('SELL_PRODUCT_LIST_RE_NEWPRICE', {
                        index: i,
                        newPrice: (item.goldWeight * item.recycleGoldPrice * Number(Number(1 - item.abrasion).toFixed(2)) - item.paymentPrice).toFixed(2)
                    })
                }
            }
        },
        setDiscount (e, i) { // 设置折扣，得到实售价
            let temDiscount = Number(e.target.value).toFixed(2)
            let item = this.sellProductList.saleList[i]
            this.$store.commit('SELL_PRODUCT_LIST_DISCOUNT', {
                index: i,
                discount: Number(temDiscount)
            })
            if (item.goldWeight) {
                if (item.calcMethod === '1') {
                    this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                        index: i,
                        newPrice: ((item.saleGoldPrice + item.paymentPrice) * item.goldWeight * temDiscount).toFixed(2)
                    })
                } else {
                    this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                        index: i,
                        newPrice: ((item.saleGoldPrice * item.goldWeight + item.paymentPrice) * temDiscount).toFixed(2)
                    })
                }
            } else {
                this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                    index: i,
                    newPrice: ((Number(item.setOldPrice) + Number(item.paymentPrice)) * temDiscount).toFixed(2)
                })
            }
            // Number(item.setOldPrice)
        },
        setNewPrice (e, i) { // 设置实售价，得到折扣
            let temNewPrice = Number(e.target.value).toFixed(2)
            let item = this.sellProductList.saleList[i]
            this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                index: i,
                newPrice: Number(temNewPrice)
            })
            if (item.goldWeight) {
                if (item.calcMethod === '1') {
                    this.$store.commit('SELL_PRODUCT_LIST_DISCOUNT', {
                        index: i,
                        discount: (temNewPrice / (item.saleGoldPrice * item.goldWeight + item.paymentPrice)).toFixed(2)
                    })
                } else {
                    this.$store.commit('SELL_PRODUCT_LIST_DISCOUNT', {
                        index: i,
                        discount: (temNewPrice / ((item.saleGoldPrice + item.paymentPrice) * item.goldWeight)).toFixed(2)
                    })
                }
            } else {
                this.$store.commit('SELL_PRODUCT_LIST_DISCOUNT', {
                    index: i,
                    discount: (temNewPrice / (Number(item.oldPrice) + item.paymentPrice)).toFixed(2)
                })
            }
        },
        setSaleGoldPrice (e, i) { // 设置销售金价
            let temSaleGoldPrice = Number(e.target.value).toFixed(2)
            let item = this.sellProductList.saleList[i]
            this.$store.commit('SELL_PRODUCT_LIST_SALEGOLDPRICE', {
                index: i,
                saleGoldPrice: Number(temSaleGoldPrice)
            })
            if (item.calcMethod === '1') { // 记重
                this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                    index: i,
                    newPrice: ((item.saleGoldPrice * item.goldWeight + item.paymentPrice) * item.discount).toFixed(2)
                })
            } else { // 计件
                this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                    index: i,
                    newPrice: (((item.saleGoldPrice + item.paymentPrice) * item.goldWeight) * item.discount).toFixed(2)
                })
            }
        },
        setPaymentPrice (e, i) { // 设置手工费
            let temPaymentPrice = Number(e.target.value).toFixed(2)
            let item = this.sellProductList.saleList[i]
            this.$store.commit('SELL_PRODUCT_LIST_PAYMENTPRICE', {
                index: i,
                paymentPrice: Number(temPaymentPrice)
            })
            if (item.goldWeight) {
                if (item.calcMethod === '1') {
                    this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                        index: i,
                        newPrice: ((item.saleGoldPrice * item.goldWeight + item.paymentPrice) * item.discount).toFixed(2)
                    })
                } else {
                    this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                        index: i,
                        newPrice: ((item.saleGoldPrice + item.paymentPrice) * item.goldWeight * item.discount).toFixed(2)
                    })
                }
            } else {
                this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                    index: i,
                    newPrice: ((Number(item.oldPrice) + item.paymentPrice) * item.discount).toFixed(2)
                })
            }
        },
        setOldPrice (e, i) { // 销售--》设置原售价
            let temOldPrice = Number(e.target.value).toFixed(2)
            let item = this.sellProductList.saleList[i]
            this.$store.commit('SELL_PRODUCT_LIST_OLDPRICE', {
                index: i,
                oldPrice: Number(temOldPrice)
            })
            console.log(Number(temOldPrice) + Number(item.paymentPrice))
            this.$store.commit('SELL_PRODUCT_LIST_NEWPRICE', {
                index: i,
                newPrice: ((Number(item.oldPrice) + Number(item.paymentPrice)) * item.discount).toFixed(2)
            })
        },
        //退换
        setEstimatePrice (e, i) { //设置估价
            let item = this.sellProductList.exchangeList[i]
            let temEstimatePrice = Number(e.target.value).toFixed(2)
            let abrasion = Number(1 - item.abrasion).toFixed(2)
            this.$store.commit('SELL_PRODUCT_LIST_EX_ESTIMATEPRICE', {
                index: i,
                estimatePrice: Number(temEstimatePrice)
            })
            // item.newPrice = item.estimatePrice * Number(abrasion) + item.paymentPrice
            this.exchangeList(item, abrasion, i)
        },
        setExGoldPrice (e, i) { // 退换金价
            let item = this.sellProductList.exchangeList[i]
            let temExchangeGoldPrice = Number(e.target.value).toFixed(2)
            let abrasion = Number(1 - item.abrasion).toFixed(2)
            // item.exchangeGoldPrice = Number(temExchangeGoldPrice)
            this.$store.commit('SELL_PRODUCT_LIST_EX_EXCHANGEGOLDPRICE', {
                index: i,
                exchangeGoldPrice: Number(temExchangeGoldPrice)
            })
            this.exchangeList(item, abrasion, i)
        },
        setExAbrasion (e, i) { // 损耗
            let item = this.sellProductList.exchangeList[i]
            let temAbrasion = Number(e.target.value).toFixed(2)
            let abrasion = Number(1 - temAbrasion).toFixed(2)
            // item.abrasion = Number(temAbrasion)
            this.$store.commit('SELL_PRODUCT_LIST_EX_ABRASION', {
                index: i,
                abrasion: Number(temAbrasion)
            })
            this.exchangeList(item, abrasion, i)
        },
        setExNewPrice (e, i) { // 退换新价
            let temNewPrice = Number(e.target.value).toFixed(2)
            let item = this.sellProductList.exchangeList[i]
            // item.newPrice = Number(temNewPrice)
            this.$store.commit('SELL_PRODUCT_LIST_EX_NEWPRICE', {
                index: i,
                newPrice: Number(temNewPrice)
            })
            if (item.goldWeight) { // 素金
                if (item.calcMethod === '1') { // 计重
                    // item.abrasion = 1 - (item.goldWeight * item.paymentPrice + item.newPrice) / (item.goldWeight * item.exchangeGoldPrice)
                    this.$store.commit('SELL_PRODUCT_LIST_EX_ABRASION', {
                        index: i,
                        abrasion: Number(1 - (item.goldWeight * item.paymentPrice + item.newPrice) / (item.goldWeight * item.exchangeGoldPrice))
                    })
                } else { // 计件
                    // item.abrasion = (item.paymentPrice + item.newPrice) / (item.goldWeight * item.exchangeGoldPrice)
                    this.$store.commit('SELL_PRODUCT_LIST_EX_ABRASION', {
                        index: i,
                        abrasion: Number(1 - (item.paymentPrice + item.newPrice) / (item.goldWeight * item.exchangeGoldPrice))
                    })
                }
            } else { // 宝石
                // item.abrasion = (item.paymentPrice + item.newPrice) / item.estimatePrice
                this.$store.commit('SELL_PRODUCT_LIST_EX_ABRASION', {
                    index: i,
                    abrasion: Number(1 - (item.paymentPrice + item.newPrice) / item.estimatePrice)
                })
            }
        },
        setExPaymentPrice (e, i) { // 退换手工费
            let temPaymentPrice = Number(e.target.value).toFixed(2)
            let item = this.sellProductList.exchangeList[i]
            item.paymentPrice = Number(temPaymentPrice)
            let abrasion = Number(1 - item.abrasion).toFixed(2)
            this.exchangeList(item, abrasion, i)
        },
        exchangeList (item, abrasion, i) { // 设置其他，退还价改变公式
            if (item.goldWeight) { // 素金
                if (item.calcMethod === '1') { // 计重
                    // item.newPrice = item.goldWeight * item.exchangeGoldPrice * Number(abrasion) - item.goldWeight * item.paymentPrice
                    this.$store.commit('SELL_PRODUCT_LIST_EX_NEWPRICE', {
                        index: i,
                        newPrice: (item.goldWeight * item.exchangeGoldPrice * Number(abrasion) - item.goldWeight * item.paymentPrice).toFixed(2)
                    })
                } else { // 计件
                    // item.newPrice = item.goldWeight * item.exchangeGoldPrice * Number(abrasion) - item.paymentPrice
                    this.$store.commit('SELL_PRODUCT_LIST_EX_NEWPRICE', {
                        index: i,
                        newPrice: (item.goldWeight * item.exchangeGoldPrice * Number(abrasion) - item.paymentPrice).toFixed(2)
                    })
                }
            } else { // 宝石
                // item.newPrice = item.estimatePrice * Number(abrasion) - item.paymentPrice
                this.$store.commit('SELL_PRODUCT_LIST_EX_NEWPRICE', {
                    index: i,
                    newPrice: (item.estimatePrice * Number(abrasion) - item.paymentPrice).toFixed(2)
                })
            }
        },
        // 回收
        setReEstimatePrice (e, i) { //设置回购估价
            let item = this.sellProductList.recycleList[i]
            let temEstimatePrice = Number(e.target.value).toFixed(2)
            let abrasion = Number(1 - item.abrasion).toFixed(2)
            // item.estimatePrice = Number(temEstimatePrice)
            this.$store.commit('SELL_PRODUCT_LIST_RE_ESTIMATEPRICE', {
                index: i,
                estimatePrice: Number(temEstimatePrice)
            })
            this.recycleList(item, abrasion, i)
        },
        setReGoldPrice (e, i) { // 回购金价
            let item = this.sellProductList.recycleList[i]
            let temRecycleGoldPrice = Number(e.target.value).toFixed(2)
            let abrasion = Number(1 - item.abrasion).toFixed(2)
            // item.recycleGoldPrice = Number(temRecycleGoldPrice)
            this.$store.commit('SELL_PRODUCT_LIST_RE_RECYCLEGOLDPRICE', {
                index: i,
                recycleGoldPrice: Number(temRecycleGoldPrice)
            })
            this.recycleList(item, abrasion, i)
        },
        setReAbrasion (e, i) { // 损耗
            let item = this.sellProductList.recycleList[i]
            let temAbrasion = Number(e.target.value).toFixed(2)
            let abrasion = Number(1 - temAbrasion).toFixed(2)
            // item.abrasion = Number(temAbrasion)
            this.$store.commit('SELL_PRODUCT_LIST_RE_ABRASION', {
                index: i,
                abrasion: Number(temAbrasion)
            })
            this.recycleList(item, abrasion, i)
        },
        setReNewPrice (e, i) { // 退换价
            let item = this.sellProductList.recycleList[i]
            let temNewPrice = Number(e.target.value).toFixed(2)
            // item.newPrice = Number(temNewPrice)
            this.$store.commit('SELL_PRODUCT_LIST_RE_NEWPRICE', {
                index: i,
                newPrice: Number(temNewPrice)
            })
            if (item.goldWeight) { // 素金
                if (item.calcMethod === '1') { // 计重
                    // item.abrasion = (item.goldWeight * item.paymentPrice + item.newPrice) / (item.goldWeight * item.recycleGoldPrice)
                    this.$store.commit('SELL_PRODUCT_LIST_RE_ABRASION', {
                        index: i,
                        abrasion: Number(1 - (item.goldWeight * item.paymentPrice + item.newPrice) / (item.goldWeight * item.recycleGoldPrice))
                    })
                } else { // 计件
                    // item.abrasion = (item.paymentPrice + item.newPrice) / (item.goldWeight * item.recycleGoldPrice)
                    this.$store.commit('SELL_PRODUCT_LIST_RE_ABRASION', {
                        index: i,
                        abrasion: Number(1 - (item.paymentPrice + item.newPrice) / (item.goldWeight * item.recycleGoldPrice))
                    })
                }
            } else { // 宝石
                // item.abrasion = (item.paymentPrice + item.newPrice) / item.estimatePrice
                this.$store.commit('SELL_PRODUCT_LIST_RE_ABRASION', {
                    index: i,
                    abrasion: Number(1 - (item.paymentPrice + item.newPrice) / item.estimatePrice)
                })
            }
        },
        setRePaymentPrice (e, i) { // 退换手工费
            let item = this.sellProductList.recycleList[i]
            let temPaymentPrice = Number(e.target.value).toFixed(2)
            let abrasion = Number(1 - item.abrasion).toFixed(2)
            // item.paymentPrice = Number(temPaymentPrice)
            this.$store.commit('SELL_PRODUCT_LIST_RE_PAYMENTPRICE', {
                index: i,
                paymentPrice: Number(temPaymentPrice)
            })
            this.recycleList(item, abrasion, i)
        },
        recycleList (item, abrasion, i) { // 设置其他，回购价改变公式
            if (item.goldWeight) { // 素金
                if (item.calcMethod === '1') { // 计重
                    item.newPrice = item.goldWeight * item.recycleGoldPrice * Number(abrasion) - item.goldWeight * item.paymentPrice
                    this.$store.commit('SELL_PRODUCT_LIST_RE_NEWPRICE', {
                        index: i,
                        newPrice: (item.goldWeight * item.recycleGoldPrice * Number(abrasion) - item.goldWeight * item.paymentPrice).toFixed(2)
                    })
                } else { // 计件
                    // item.newPrice = item.goldWeight * item.recycleGoldPrice * Number(abrasion) - item.paymentPrice
                    this.$store.commit('SELL_PRODUCT_LIST_RE_NEWPRICE', {
                        index: i,
                        newPrice: (item.goldWeight * item.recycleGoldPrice * Number(abrasion) - item.paymentPrice).toFixed(2)
                    })
                }
            } else { // 宝石
                // item.newPrice = item.estimatePrice * Number(abrasion) - item.paymentPrice
                this.$store.commit('SELL_PRODUCT_LIST_RE_NEWPRICE', {
                    index: i,
                    newPrice: (item.estimatePrice * Number(abrasion) - item.paymentPrice).toFixed(2)
                })
            }
        },
        delectProduct (parm, i) { // 单据删除
            let sellList = document.getElementsByClassName("sell-list-body");
            let modal = document.getElementsByClassName("v-modal")[0];
            console.log(parm)
            console.log("我害怕")
            let options = {
                "productList": [{
                    "barcode": parm // 条形码
                }],
                "orderNum": sessionStorage.getItem("orderNumber"), // 单据号
                "operate": 2 // 1新增2 删除3 全部删除
            }
            operateProductList(options).then(response => {
                console.log("单据删除抗你");
                this.sellProductList.saleList.splice(i, 1);
                for (var i = 0; i < sellList.length; i++) {
                    sellList[i].className = "sell-list-body mt20";
                }
                if (modal) {
                    document.getElementsByTagName("html")[0].removeChild(modal);
                }
                alert("删除成功")
                console.log(response);
            }, response => {
                console.log(response)
            })
        },
        toSellDetail (parm) { // 去到销售明细
            sessionStorage.setItem("barcode", parm);
            this.$router.push("/work/sell/sellReceiptsList/sellDetail");
        },
        // getProductList () { // 获取商品列表
        // // 暂时屏蔽
        //     let options = {
        //         "orderNum": this.orderNumber,
        //         "page": "1",
        //         "pageSize": "10"
        //     }
        //     seekSellProductList(options).then((response) => { // 商品列表-销售
        //         console.log("商品列表-销售");
        //         console.log(response);
        //         // this.sellProductListFun(response.data.data);
        //         sessionStorage.setItem("商品列表-销售", JSON.stringify(response.data.data));
        //         // console.log(sessionStorage.getItem("商品列表-销售"))
        //     }, (response) => {
        //         console.log(response);
        //     })
        //     this.sellProductList = this.List;
        //     console.log('头疼')
        //     console.log(this.sellProductList)
        //     // this.productListData = seekSellProductList(options);
        //     // this.sellProductListFun(seekSellProductList(options)); // 产品列表
        //     // seekSellReceiptsIntro(options).then(response => {
        //     //     console.log("产品列表");
        //     //     console.log(response);
        //     //     if (response.data.state === 200) {
        //     //         sessionStorage.setItem("产品列表", JSON.stringify(response.data.data));
        //     //         this.receiptsIntroList = response.data.data;
        //     //         console.log("我就试试")
        //     //         console.log(this.receiptsIntroList)
        //     //     }
        //     // }, response => {
        //     //     console.log(response);
        //     // })
        //     // this.receiptsIntroList = seekSellReceiptsIntro(options); // 单据详情
        // },
        beginEdit (value, item) { // 编辑
            var sellList = document.getElementsByClassName("sell-list-body");
            for (var i = 0; i < sellList.length; i++) {
                sellList[i].className = "sell-list-body mt20";
            }
            this.isInput = true;
            sellList[value].className = "sell-list-body mt20 click-style hasborder";
            var modal = document.createElement('div');
            modal.className = 'v-modal';
            if (document.getElementsByTagName("html")[0].getElementsByClassName("v-modal").length === 0) {
                document.getElementsByTagName("html")[0].appendChild(modal);
                this.quitEdit(item);
            }
            // console.log(this.isInput)
        },
        quitEdit (item) { // 退出编辑状态
            let sellList = document.getElementsByClassName("sell-list-body");
            let ClickStyle = document.getElementsByClassName("click-style");
            let modal = document.getElementsByClassName("v-modal");
            if (ClickStyle.length !== 0) {
                let _this = this;
                document.onkeydown = function(event) {
                    var e = event || window.event;
                    if (e && e.keyCode === 27) { // 按 Esc
                    }
                    if (e && e.keyCode === 13) { // enter 键
                    // 要做的事情
                        for (var i = 0; i < sellList.length; i++) {
                            sellList[i].className = "sell-list-body mt20";
                        }
                        if (modal.length >= 1) {
                            document.getElementsByTagName("html")[0].removeChild(modal[0]);
                        }
                        let options = {
                            "orderNum": sessionStorage.getItem("orderNumber"),
                            "barCode": item.barcode,
                            "modifyList": [{
                                "modifyType": "01",
                                "dataType": "2",
                                "objectData": item.saleGoldPrice
                            },
                            {
                                "modifyType": "02",
                                "dataType": "2",
                                "objectData": item.paymentPrice
                            },
                            {
                                "modifyType": "11",
                                "dataType": "2",
                                "objectData": item.abrasion
                            },
                            {
                                "modifyType": "04",
                                "dataType": "2",
                                "objectData": item.newPrice
                            },
                            {
                                "modifyType": "09",
                                "dataType": "2",
                                "objectData": item.methods
                            }]
                        }
                        operateUpdateSell(options).then((response) => {
                            console.log(response)
                        }, (response) => {
                            console.log(response)
                        })
                        _this.isInput = false;
                    }
                };
            }
        },
        getValue (e, index) {
            e.click()
        },
        getABC () {
        }
        // getGoldPrice () {
        //     let options = {
        //         "objId": "66393b03202911e78cc6f48e3888bbef"
        //     }
        //     seekGetGoldPrice(options).then((response) => { // 当日金价
        //         console.log("当日金价");
        //         console.log(response);
        //         this.todayGoldPrice = response.data.data.dataList[0];
        //         sessionStorage.setItem("商品列表-销售", JSON.stringify(response.data.data));
        //     }, (response) => {
        //         console.log(response);
        //     })
        // }
    }
}
</script>
<style lang="scss" scoped>
.product-list{
    // overflow: auto;
    // height: 100%;
    .sell-list-body:first-child{
        margin-top: 0;
    }
    .sell-list-body{ // 每条数据
        position: relative;
        margin-top: 20px;
        padding-bottom: 10px;
        background-color: #fff;
        font-size: 0;
        .sell-delect-btn{ // 删除按钮
            position: absolute;
            height: 38px;
            line-height: 38px;
            top: 11px;
            right: 30px;
            font-size: 14px;
            border-radius: 10%;
            padding: 0 20px;
            border: 1px solid #d6d6d6;
            color: #999;
        }
        ul{
            padding-left: 60px;
            vertical-align: top;
            // border: 1px solid red;
            // display: inline-block;
            li{
                // margin: 16px 0;
                padding: 10px 0;
                // height: 16px;
                line-height: 16px;
                font-size: 16px;
                input{
                    border: 1px solid #fff;
                    width: 80px;
                    font-size: 16px;
                    &:focus{
                        border: 1px solid #999;
                    }
                }                
            }
        }
        ul:nth-child(1){
            // padding: 5px 5px 0 5px;
            // width: 350px;
            background-color: #f6f7f8;
            padding-left: 20px;
            font-weight: bolder;
            color: #999;
            margin-bottom: 10px;
            li{
                display: inline-block;
                width: 300px;
                margin: 22px 0;
                padding: 0;
            }
            .main-color{
                height: 16px;
                line-height: 16px;
                position: relative;
                // border: 1px solid #ccc;
                span{
                    display: inline-block;
                    height: 16px;
                    line-height: 16px;
                    vertical-align: top;
                    // border: 1px solid red;
                    margin-right: 20px;
                }
                img{
                    position: absolute;
                    top: -2px;
                }
            }
        }
        ul:nth-child(2),ul:nth-child(3){
            width: 260px;
            display: inline-block;
        }
        ul:nth-child(4){
            display: block;            
            li{
                display: inline-block;
                position: relative;
                width: 260px;
                input{
                    border: 1px solid #fff;
                    width: 80px;
                    font-size: 16px;
                    &:focus{
                        border: 1px solid #999;
                    }
                }
                &:last-child{
                    float: right;
                    margin-right: 20px;
                    color: red;
                    text-align: right;
                    input{
                        color: red;
                    }
                }                
            }
        }
        &.click-style{
            position: relative;
            z-index: 9999;
            box-shadow: 0 0 7px 3px rgba(0,0,0,.3);
        }
        &.hasborder{
            input{
                border: 1px solid #999 !important;
            }
        }
        .sell-switch{
            position: absolute;
            bottom: 11px;
            right: 300px;
            height: 30px;
            width: 60px;
        }
        .sell-switch-material{
            position: absolute;
            top: 0;
            bottom: 0;
            right: 30px;
            margin: auto;
            height: 30px;
            width: 60px;
        }
        .hide-input{
            display:inline-block;
            border:1px solid #fff;
            height: 23px;
            line-height: 23px;
        }
    }
}
</style>