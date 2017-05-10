// 查询接口
// import Vue from 'vue'
import apiCall from './ApiCall.js'
export const seekProductDetail = function (parm) { // 商品列表 // 带废除
    let productIdList = [];
    if (parm.productIdList) { // 商品ID列表
        for (let i of parm.productIdList) {
            let productId = {};
            productId.productId = i;
            productIdList.push(productId);
            productId = null;
        }
    }
    let data = {
        "depShopType": parm.depShopType || "",
        "objId": parm.objId || "", // 单据id
        "obj2Id": parm.obj2Id, // (type=5 and applyType=8)时为柜组id
        "page": parm.page || "1",
        // "page": "2",
        "pageSize": parm.pageSize || "30",
        "sellType": "", // 销售类型1 销售 2 退换  3 截金 4 回收 5 兑换 6 典当
        "applyType": parm.applyType || "", // 当前应用类型：1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜（销售除外）
        "type": parm.type, // 操作类型 1 单据号 2 产品类别ID 3 商品ID列表 4 模糊查询 5 条码号
        "productIdList": productIdList || "", // 商品ID列表
        "productId": ""
    }
    sessionStorage.setItem("看你报错", JSON.stringify(data));
    let URL = INTERFACE_URL_9083 + "/v1/goods/getGoodsInfoList";
    return apiCall(data, URL);
}
export const seekGoodsList = function (parm) { // 商品列表
    let URL = INTERFACE_URL_9083 + "/v1/order/goodsList"
    return apiCall(parm, URL);
}
export const seekShowCounterList = function (parm) { // 柜组列表
    // let data = {
    //     "shopId": parm // 店铺ID
    // }
    let URL = INTERFACE_URL_9083 + "/v1/headquarter/showCounterList"
    return apiCall(parm, URL);
}
export const seekDepartmentList = function (parm) { // 单据列表
    let data = {
        "orderByType": "", // 1 时间 2 库位（发货、退货等） 3 商品属性 4 制单人 5 柜组（调入、销售等） 6 部门 7 分销商 8 收货人 9 单据类型 10 销售员 11 产品类别
        "page": parm.page || "1", // 当前页
        "pageSize": parm.pageSize || "30", // 每页显示数
        "type": parm.type, // 模块类型：1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜9 销售/回购
        "showView": parm.showView, // 显示列表：1 显示列表 2 编辑列表 3 查询结果 4 可收货列表
        "operationUnit": parm.operationUnit, // 操作单位 1 部门 2 店铺
        "operationUnitId": parm.operationUnitId, // 操作单位ID
        "queryData": {
            "startTime": parm.startTime, // 开始时间
            "endTime": parm.endTime, // 结束时间
            "orderNumber": parm.orderNumber, // 单号
            "auditorIdList": [ // 审核人ID列表
                {
                    "auditorId": ""
                }
            ],
            "classesIdList": [
                {
                    "classesId": parm.classesId || "" // 产品类别ID
                }
            ],
            "handleDeptIdList": [ // 操作部门ID列表(店铺应用用到)
                {
                    "handleDeptId": parm.handleDeptId || ""
                }
            ],
            "makeUserIdList": [ // 制单人ID列表
                {
                    "makeUserId": ""
                }
            ],
            "supplierIdList": [ // 供应商ID列表
                {
                    "supplierId": parm.supplierId || ""
                }
            ],
            "shopIdList": [ // 分销商ID列表
                {
                    "shopId": parm.shopId || ""
                }
            ],
            "warehouseIdList": [ // 库位ID（入库等）列表
                {
                    "warehouseId": parm.warehouseId || ""
                }
            ]
        }
    }
    let URL = INTERFACE_URL_9083 + "/v1/submodule/showSubmoduleData";
    return apiCall(data, URL);
}
export const receiptHistoryList = function (parm) { // 单据历史记录
    let data = {
        "orderId": parm // 店铺ID
    }
    let URL = INTERFACE_URL_9083 + "/v1/myWorkApply/receiptHistoryList"
    return apiCall(data, URL);
}
export const downloadTable = function (parm) { // 模板下载
    let data = {
        "type": "2", // 1 URL 2 文件 3 视频
        "infoType": "1" // 1 入库模板下载地址
    }
    let URL = INTERFACE_URL_9083 + "/v1/public/getResourcesUrl"
    return apiCall(data, URL);
}
export const seekProductClassList = function (parm) { // 获取商品大小类的下拉列表
    let data = {
        "type": parm
    }
    let URL = INTERFACE_URL_9083 + "/v1/headquarter/showProductClassesList";
    return apiCall(data, URL);
}
export const seekProductPropertyList = function (parm) { // 获取商品属性下拉列表
    let data = {
        "type": parm
    }
    let URL = INTERFACE_URL_9083 + "/v1/headquarter/showProductPropertyList";
    return apiCall(data, URL);
}
export const seekCertificateList = function (parm) { // 证书下拉列表
    let data = null;
    let URL = INTERFACE_URL_9083 + "/v1/headquarter/showhCertificateList"
    return apiCall(data, URL)
}
export const seekRepositoryList = function (parm) { // 库位列表
    let data = null;
    let URL = INTERFACE_URL_9083 + "/v1/headquarter/showRepositoryList"
    return apiCall(data, URL);
}
export const getProductTypeList = function (parm) { // 商品类型列表
    let data = {
        "type": 1
    }
    let URL = INTERFACE_URL_9083 + "/v1/goods/getProductTypeList";
    return apiCall(data, URL);
}
export const seekShowProviderList = function (parm) { // 供应商列表
    let data = {};
    let URL = INTERFACE_URL_9083 + "/v1/headquarter/showProviderList";
    return apiCall(data, URL);
}
export const getUpdateGoodsList = function (parm) { // 商品修改列表
    let URL = INTERFACE_URL_9083 + "/v1/goods/getUpdateGoodsList";
    return apiCall(parm, URL);
}
export const seekGetShopListByCo = function (parm) { // 8.2店铺列表
    let URL = INTERFACE_URL_9083 + "/v1/public/getShopListByCo";
    return apiCall(parm, URL);
}
export const seekReceiptRemark = function (parm) { // 5.48单据备注
    let URL = INTERFACE_URL_9083 + "/v1/auth/receiptRemark";
    return apiCall(parm, URL);
}
/* --------单据列表------- */
export const seekInStorageList = function (parm) { // 5.14单据列表-入库
    let URL = INTERFACE_URL_9083 + "/v1/order/inStorageList";
    return apiCall(parm, URL);
}
export const seekOutStorageList = function (parm) { // 5.15单据列表-退库
    let URL = INTERFACE_URL_9083 + "/v1/order/outStorageList";
    return apiCall(parm, URL);
}
export const seekTuneStorageList = function (parm) { // 5.16单据列表-调库
    let URL = INTERFACE_URL_9083 + "/v1/order/tuneStorageList";
    return apiCall(parm, URL);
}
export const seekSendStorageList = function (parm) { // 5.17单据列表-发货
    let URL = INTERFACE_URL_9083 + "/v1/order/sendStorageList";
    return apiCall(parm, URL);
}
export const seekOutCargoList = function (parm) { // 5.18单据列表-退货
    let URL = INTERFACE_URL_9083 + "/v1/order/outCargoList";
    return apiCall(parm, URL);
}
export const seekTuneCounterList = function (parm) { // 5.19单据列表-调柜
    let URL = INTERFACE_URL_9083 + "/v1/order/tuneCounterList";
    return apiCall(parm, URL);
}
export const seekGoodsSellOrder = function (parm) { // 5.21单据列表 - 销售
    let URL = INTERFACE_URL_9083 + "/v1/order/goodsSellOrder";
    return apiCall(parm, URL);
}
/* --------单据简介------- */
export const seekReceiptRKSynopsis = function (parm) { // 5.38单据简介-入库
    let URL = INTERFACE_URL_9083 + "/v1/sell/receiptRKSynopsis";
    return apiCall(parm, URL);
}
export const seekReceiptTKSynopsis = function (parm) { // 5.39单据简介-退库
    let URL = INTERFACE_URL_9083 + "/v1/order/receiptTKSynopsis";
    return apiCall(parm, URL);
}
export const seekReceiptFHSynopsis = function (parm) { // 5.40单据简介-发货
    let URL = INTERFACE_URL_9083 + "/v1/order/receiptFHSynopsis";
    return apiCall(parm, URL);
}
export const seekReceiptDKSynopsis = function (parm) { // 单据简介-调库
    let URL = INTERFACE_URL_9083 + "/v1/order/receiptDKSynopsis";
    return apiCall(parm, URL);
}
export const seekReceiptTHSynopsis = function (parm) { // 5.41单据简介-退货
    let URL = INTERFACE_URL_9083 + "/v1/order/receiptTHSynopsis";
    return apiCall(parm, URL);
}
export const seekSellReceiptsIntro = function (parm) { // 5.37单据简介-销售
    let URL = INTERFACE_URL_9083 + "/v1/sell/getOrderInfo";
    return apiCall(parm, URL);
}
// 销售
export const seekNewGoodsInfoList = function (parm) { // 6.3 商品列表（入库）
    let URL = INTERFACE_URL_9083 + "/v1/goods/newGoodsInfoList";
    return apiCall(parm, URL);
}
export const seekSellProductList = function (parm) { // 商品列表-销售 6.45
    let URL = INTERFACE_URL_9083 + "/v1/order/goodsSellList";
    return apiCall(parm, URL);
}
export const seekSellcollectMoney = function (parm) { // 收银信息
    let URL = INTERFACE_URL_9083 + "/v1/sell/getOrderPayInfo";
    return apiCall(parm, URL);
}
export const seekSellProductDetail = function (parm) { // 商品明细 - 销售6.47
    let URL = INTERFACE_URL_9083 + "/v1/order/getGoodsListByBarcode"
    return apiCall(parm, URL);
}
export const seekAddProductToOrder = function (parm) { // 5.33单据操作-新增销售商品
    let URL = INTERFACE_URL_9083 + "/v1/sell/addProductToOrder";
    return apiCall(parm, URL);
}
export const seekGetProductByBarcode = function (parm) { // 6.51商品查询-条形码
    let URL = INTERFACE_URL_9083 + "/v1/public/getProductByBarcode";
    return apiCall(parm, URL);
}