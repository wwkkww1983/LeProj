import batchAdd from './../../layouts/Work/CommonalityComponent/batchAdd/BatchAdd'
import colorName from './../../layouts/Work/CommonalityComponent/batchAdd/ColorName'
import gemName from './../../layouts/Work/CommonalityComponent/batchAdd/GemName'
const routes = {
    path: 'batchAdd',
    component: batchAdd,
    children: [
        {path: "", component: batchAdd},
        {path: "colorName", component: colorName},
        {path: "gemName", component: gemName}
    ]
}
export default routes;
