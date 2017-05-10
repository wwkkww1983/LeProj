import sippingIndex from './../../layouts/Work/Sipping/index.vue'
import sipping from './../../layouts/Work/Sipping/Sipping.vue'
import newSipping from './../../layouts/Work/Sipping/NewSipping.vue'
import detailSipping from './../../layouts/Work/Sipping/DetailSipping.vue'
import detailSippingTable from './../../layouts/Work/Sipping/DetailSippingTable.vue'
const routes = {
    path: 'sipping',
    component: sippingIndex,
    children: [
        {path: "", component: sipping, name: "发货"},
        {path: "newSipping", component: newSipping, name: "新建单据"},
        {path: "detailSipping", component: detailSipping, name: "发货单"},
        {path: "detailSippingTable", component: detailSippingTable, name: "打印单据"}
    ]
}
export default routes;
