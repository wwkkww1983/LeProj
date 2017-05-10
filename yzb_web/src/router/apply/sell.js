import sellIndex from './../../layouts/Work/Sell/index.vue'
import sell from './../../layouts/Work/Sell/Sell.vue'
// import sellDetail from './../../layouts/Work/Sell/components/SellDetail.vue' // 产品明细
import sellReceiptsList from './../../layouts/Work/Sell/SellReceiptsList'
import productList from "./../../layouts/Work/Sell/components/ProductList" // 产品列表
// import newSipping from './../../layouts/Work/Sipping/NewSipping.vue'
// import detailSipping from './../../layouts/Work/Sipping/DetailSipping.vue'
const routes = {
    path: 'sell',
    component: sellIndex,
    name: "销售单据",
    children: [
        {path: "", component: sell, name: "销售单据"},
        {
            path: "sellReceiptsList",
            component: sellReceiptsList,
            name: "产品列表",
            children: [
                {path: "", component: productList, name: "产品列表"}
                // {path: "sellDetail", component: sellDetail, name: "产品明细"}
            ]
        }
        // {path: "sellDetail", component: sellDetail, name: "产品明细"}
        // {path: "newSipping", component: newSipping, name: "新建单据"},
        // {path: "detailSipping", component: detailSipping, name: "发货单"}
    ]
}
export default routes;
