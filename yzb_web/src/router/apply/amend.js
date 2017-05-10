import AmendIndex from './../../layouts/Work/Amend/index.vue'
import Amend from './../../layouts/Work/Amend/Amend.vue'
import NewAmend from './../../layouts/Work/Amend/NewAmend.vue'
import DetailAmend from './../../layouts/Work/Amend/DetailAmend.vue'
import DetailAmendTable from './../../layouts/Work/Amend/DetailAmendTable.vue'
const routes = {
    path: 'amend',
    component: AmendIndex,
    children: [
        {path: "", component: Amend},
        {path: "newAmend", component: NewAmend},
        {path: "detailAmend", component: DetailAmend},
        {path: "detailAmendTable", component: DetailAmendTable}
    ]
}
export default routes;
