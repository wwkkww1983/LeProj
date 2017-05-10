import * as types from "./../mutation-types.js"
const store = {
    state: {
        sellProductList: "" // 商品列表-销售
    },
    mutations: {
        [types.SELL_PRODUCT_LIST] (state, parm) {
            state.sellProductList = parm;
        },
        [types.SELL_PRODUCT_LIST_DISCOUNT] (state, payload) { // 销售--》折扣
            state.sellProductList.saleList[payload.index].discount = payload.discount;
        },
        [types.SELL_PRODUCT_LIST_NEWPRICE] (state, payload) { // 销售--》新价
            state.sellProductList.saleList[payload.index].newPrice = payload.newPrice;
        },
        [types.SELL_PRODUCT_LIST_SALEGOLDPRICE] (state, payload) { // 销售--》销售金价
            state.sellProductList.saleList[payload.index].saleGoldPrice = payload.saleGoldPrice;
        },
        [types.SELL_PRODUCT_LIST_PAYMENTPRICE] (state, payload) { // 销售--》折扣价
            state.sellProductList.saleList[payload.index].paymentPrice = payload.paymentPrice;
        },
        [types.SELL_PRODUCT_LIST_CALCMETHOD] (state, payload) { // 销售--》手工费
            state.sellProductList.saleList[payload.index].calcMethod = payload.calcMethod;
        },
        [types.SELL_PRODUCT_LIST_OLDPRICE] (state, payload) { // 销售--》原售价
            state.sellProductList.saleList[payload.index].oldPrice = payload.oldPrice;
        },
        // 销售---退还
        [types.SELL_PRODUCT_LIST_EX_EXCHANGEGOLDPRICE] (state, payload) { // 退换--》回收金价
            state.sellProductList.exchangeList[payload.index].exchangeGoldPrice = payload.exchangeGoldPrice;
        },
        [types.SELL_PRODUCT_LIST_EX_ABRASION] (state, payload) { // 退换--》损耗
            state.sellProductList.exchangeList[payload.index].abrasion = payload.abrasion;
        },
        [types.SELL_PRODUCT_LIST_EX_ESTIMATEPRICE] (state, payload) { // 退换--》估价
            state.sellProductList.exchangeList[payload.index].estimatePrice = payload.estimatePrice;
        },
        [types.SELL_PRODUCT_LIST_EX_PAYMENTPRICE] (state, payload) { // 退换--》手工费
            state.sellProductList.exchangeList[payload.index].paymentPrice = payload.paymentPrice;
        },
        [types.SELL_PRODUCT_LIST_EX_NEWPRICE] (state, payload) { // 退换--》退换价
            state.sellProductList.exchangeList[payload.index].newPrice = payload.newPrice;
        },
        [types.SELL_PRODUCT_LIST_EX_CALCMETHOD] (state, payload) { // 退换--》计价
            state.sellProductList.exchangeList[payload.index].calcMethod = payload.calcMethod;
        },
        // 销售------回收
        [types.SELL_PRODUCT_LIST_RE_RECYCLEGOLDPRICE] (state, payload) { // 退换--》回收金价
            state.sellProductList.recycleList[payload.index].recycleGoldPrice = payload.recycleGoldPrice;
        },
        [types.SELL_PRODUCT_LIST_RE_ABRASION] (state, payload) { // 退换--》损耗
            state.sellProductList.recycleList[payload.index].abrasion = payload.abrasion;
        },
        [types.SELL_PRODUCT_LIST_RE_ESTIMATEPRICE] (state, payload) { // 退换--》估价
            state.sellProductList.recycleList[payload.index].estimatePrice = payload.estimatePrice;
        },
        [types.SELL_PRODUCT_LIST_RE_PAYMENTPRICE] (state, payload) { // 退换--》手工费
            state.sellProductList.recycleList[payload.index].paymentPrice = payload.paymentPrice;
        },
        [types.SELL_PRODUCT_LIST_RE_NEWPRICE] (state, payload) { // 退换--》退换价
            state.sellProductList.recycleList[payload.index].newPrice = payload.newPrice;
        },
        [types.SELL_PRODUCT_LIST_RE_CALCMETHOD] (state, payload) { // 退换--》计价
            state.sellProductList.recycleList[payload.index].calcMethod = payload.calcMethod;
        }
    }
}
export default store;
