import salesReturnIndex from './../../layouts/Work/SalesReturn/index.vue'
import salesReturn from './../../layouts/Work/SalesReturn/SalesReturn.vue'
import newSalesReturn from './../../layouts/Work/SalesReturn/NewSalesReturn.vue'
import detailSalesReturn from './../../layouts/Work/SalesReturn/DetailSalesReturn.vue'
import detailSalesReturnTable from './../../layouts/Work/SalesReturn/DetailSalesReturnTable.vue'
const routes = {
    path: 'salesReturn',
    component: salesReturnIndex,
    children: [
        {path: "", component: salesReturn},
        {path: "newSalesReturn", component: newSalesReturn},
        {path: "detailSalesReturn", component: detailSalesReturn},
        {path: "detailSalesReturnTable", component: detailSalesReturnTable}
    ]
}
export default routes;
