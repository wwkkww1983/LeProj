import apiCall from './ApiCall.js'
 // 商品操作（删）
export const delectProductDetail = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/apply/productOperation";
    return apiCall(parm, URL);
}
/* ----------新建单据------- */
export const operateCreateRKReceipt = function (parm) { // 5.22单据操作-新建-入库
    let URL = INTERFACE_URL_9083 + "/v1/order/createRKReceipt";
    return apiCall(parm, URL);
}
export const operateCreateTKReceipt = function (parm) { // 5.23单据操作-新建-退库
    let URL = INTERFACE_URL_9083 + "/v1/order/createTKReceipt";
    return apiCall(parm, URL);
}
export const operateCreateDKReceipt = function (parm) { // 5.24单据操作-新建-调库
    let URL = INTERFACE_URL_9083 + "/v1/order/createDKReceipt";
    return apiCall(parm, URL);
}
export const operateCreateFHReceipt = function (parm) { // 5.25单据操作-新建-发货
    let URL = INTERFACE_URL_9083 + "/v1/order/createFHReceipt";
    return apiCall(parm, URL);
}
export const operateCreateTHReceipt = function (parm) { // 5.26单据操作-新建-退货
    let URL = INTERFACE_URL_9083 + "/v1/order/createTHReceipt";
    return apiCall(parm, URL);
}
export const operateCreateDGReceipt = function (parm) { // 5.28单据操作-新建-调柜
    let URL = INTERFACE_URL_9083 + "/v1/order/createDGReceipt";
    return apiCall(parm, URL);
}
export const operateAddProductToRKOrder = function (parm) { // 5.49单据操作-入库添加商品
    let URL = INTERFACE_URL_9083 + "/v1/warehouse/addProductToRKOrder";
    return apiCall(parm, URL);
}
 // 5.26单据操作-新建-销售
export const operateCreateSellOrder = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/sell/createSellOrder";
    return apiCall(parm, URL);
}
// export const operateProductList = function (parm) { // 5.30单据操作-增加/删除商品-通过商品条码号
//     let URL = INTERFACE_URL_9083 + "/v1/order/productList";
//     return apiCall(parm, URL);
// }
// 5.8 单据操作（驳回审核、通过审核、撤销审核）
export const operateReceiptOperation = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/auth/receiptOperation";
    return apiCall(parm, URL)
}
export const operateTakeOrder = function (parm) { // 5.10单据收货
    let URL = INTERFACE_URL_9083 + "/v1/order/takeOrder";
    return apiCall(parm, URL)
}
export const operateDeleteByOrderNumber = function (parm) { // 5.9单据删除
    let URL = INTERFACE_URL_9083 + "/v1/order/deleteByOrderNumber";
    return apiCall(parm, URL);
}
export const shTakeDelivery = function (parm) { // 收货
    let URL = INTERFACE_URL_9083 + "/v1/apply/shTakeDelivery";
    return apiCall(parm, URL);
}
// 单据新增/提交审核/保存（销售除外）
export const receiptAddOrChecks = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/order/submitAudit";
    return apiCall(parm, URL);
}
export const operateOWhourse = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/warehouse/operateOWhourse";
    return apiCall(parm, URL);
}
export const updateGoods = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/goods/updateGoods";
    return apiCall(parm, URL);
}
export const deleteUpdateGoods = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/goods/deleteUpdateGoods";
    return apiCall(parm, URL);
}
/* 销售 */
export const operateAddBackProductToOrder = function (parm) { // 5.33单据操作-新增销售商品
    let URL = INTERFACE_URL_9083 + "/v1/sell/addBackProductToOrder";
    return apiCall(parm, URL);
}
// 5.9单据删除(旧)
export const operateDelReceipt = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/receipt/delReceipt"
    return apiCall(parm, URL);
}
// 5.9 删除单据
export const deleteByOrderNumber = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/order/deleteByOrderNumber";
    return apiCall(parm, URL);
}
// 5.29单据操作/修改
export const operateHandleXGReceipt = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/order/handleXGReceipt"
    return apiCall(parm, URL);
}
// 5.30 提交审核
export const operateSubmitAudit = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/order/submitAudit"
    return apiCall(parm, URL);
}
// 5.32新增销售
export const operateAddProductToOrder = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/sell/addProductToOrder"
    return apiCall(parm, URL);
}
// 5.34单据操作-新增退换/回收商品-通过手工输入
export const operateAddBackBuyProductToOrder = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/sell/addBackBuyProductToOrder";
    return apiCall(parm, URL);
}
// 6.50 商品修改-销售
export const operateUpdateSell = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/auth/updateSell"
    return apiCall(parm, URL);
}
// 5.31单据操作-增加/删除商品-通过商品条码号
export const operateProductList = function (parm) {
    let URL = INTERFACE_URL_9083 + "/v1/order/productList";
    return apiCall(parm, URL);
}
