import CompanySettingIndex from './../../layouts/Work/CompanySetting/index.vue'
import CompanySetting from './../../layouts/Work/CompanySetting/CompanySetting.vue'
const routes = {
    path: 'companySetting',
    component: CompanySettingIndex,
    children: [
        {path: "", component: CompanySetting}
    ]
}
export default routes;
