// 收货
import receivingIndex from './../../layouts/Work/Receiving/index.vue'
import receiving from './../../layouts/Work/Receiving/Receiving.vue'
import newReceiving from './../../layouts/Work/Receiving/NewReceiving.vue'
import detailReceiving from './../../layouts/Work/Receiving/DetailReceiving.vue'
const routes = {
    path: 'receiving',
    component: receivingIndex,
    children: [
        {path: "", component: receiving},
        {path: "newReceiving", component: newReceiving},
        {path: "detailReceiving", component: detailReceiving}
    ]
}
export default routes;
