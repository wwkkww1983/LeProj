// 退库
import StorageReturnIndex from './../../layouts/Work/StorageReturn/index.vue'
import StorageReturn from './../../layouts/Work/StorageReturn/StorageReturn.vue'
import NewStorageReturn from './../../layouts/Work/StorageReturn/NewStorageReturn.vue'
import DetailStorageReturn from './../../layouts/Work/StorageReturn/DetailStorageReturn.vue'
import DetailStorageReturnTable from './../../layouts/Work/StorageReturn/DetailStorageReturnTable.vue'

const routes = {
    path: 'storageReturn',
    component: StorageReturnIndex,
    children: [
        {path: "", component: StorageReturn},
        {path: "newStorageReturn", component: NewStorageReturn},
        {path: "detailStorageReturn", component: DetailStorageReturn},
        {path: "detailStorageReturnTable", component: DetailStorageReturnTable}
    ]
}
export default routes;
