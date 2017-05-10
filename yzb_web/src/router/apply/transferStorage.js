// 调柜
import transferStorageIndex from './../../layouts/Work/TransferStorage/index.vue'
import transferStorage from './../../layouts/Work/TransferStorage/TransferStorage.vue'
import newTransferStorage from './../../layouts/Work/TransferStorage/NewTransferStorage.vue'
import detailTransferStorage from './../../layouts/Work/TransferStorage/DetailTransferStorage.vue'
import detailTransferStorageTable from './../../layouts/Work/TransferStorage/DetailTransferStorageTable.vue'
const routes = {
    path: 'transferStorage',
    component: transferStorageIndex,
    children: [
        {path: "", component: transferStorage},
        {path: "newTransferStorage", component: newTransferStorage},
        {path: "detailTransferStorage", component: detailTransferStorage},
        {path: "detailTransferStorageTable", component: detailTransferStorageTable}
    ]
}
export default routes;
