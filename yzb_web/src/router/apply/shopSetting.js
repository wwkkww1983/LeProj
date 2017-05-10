import ShopSettingIndex from './../../layouts/Work/ShopSetting/index.vue'
import ShopSetting from './../../layouts/Work/ShopSetting/ShopSetting.vue'
const routes = {
    path: 'shopSetting',
    component: ShopSettingIndex,
    children: [
        {path: "", component: ShopSetting}
    ]
}
export default routes;
