import Work from "./../layouts/Work"
import ApplyIndex from "./../layouts/Work/ApplyIndex.vue"
import storage from "./apply/storage.js"
import sipping from "./apply/sipping.js"
import amend from "./apply/amend.js"
import salesReturn from "./apply/salesReturn.js"
import transferCabinet from "./apply/transferCabinet.js"
import transferStorage from "./apply/transferStorage.js"
import storageReturn from "./apply/storageReturn.js"
import receiving from "./apply/receiving.js"
import batchAdd from "./apply/batchAdd.js"
import companySetting from "./apply/companySetting.js"
import shopSetting from "./apply/shopSetting.js"
import sell from "./apply/sell.js"
import label from "./apply/label.js"
import template from './apply/template'
const work = {
    path: '/work',
    component: Work,
    children: [{
        path: "",
        component: ApplyIndex,
        name: "工作"
    }]
}
work.children.push(storage);
work.children.push(sipping);
work.children.push(amend);
work.children.push(salesReturn);
work.children.push(transferCabinet);
work.children.push(transferStorage);
work.children.push(storageReturn); // 退库
work.children.push(receiving); // 收货
work.children.push(batchAdd); // 批量添加弹窗
work.children.push(companySetting); // 公司设置
work.children.push(shopSetting); // 店铺设置
work.children.push(sell); // 销售
work.children.push(label); // 标签模板
work.children.push(template); // 模板
export default work;