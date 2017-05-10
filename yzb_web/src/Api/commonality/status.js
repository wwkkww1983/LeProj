// 状态
export const productDetailStatus = function (parm) { // 商品列表
    switch (parm) {
        case "1":
            return "入库中";
        case "2":
            return "已入库";
        case "3":
            return "退库中";
        case "4":
            return "已退库";
        case "5":
            return "修改中";
        case "6":
            return "已修改";
        case "7":
            return "调库中";
        case "8":
            return "已调库";
        case "9":
            return "发货中";
        case "10":
            return "已发货";
        case "11":
            return "退货中";
        case "12":
            return "已退货";
        case "13":
            return "调柜中";
        case "14":
            return "已调柜";
        case "15":
            return "销售中";
        case "16":
            return "已销售";
        case "17":
            return "发货审核";
        case "18":
            return "退货审核";
        case "19":
            return "已退换";
        case "20":
            return "退换中";
    }
}
export const receiptStatus = function (parm) { // 审核状态
    switch (parm) {
        case "1":
            return "待审核";
        case "2":
            return "审核中";
        case "3":
            return "已审核";
    }
}
export const receiptStatusTwo = function (parm) {
    switch (parm) {
        case "1":
            return "未收货";
        case "2":
            return "已收货";
    }
}
export const receiptOptionsName = function (parm) { // 审核操作的按钮名字
    switch (parm) {
        case "1":
            return "提交审核";
        case "2":
            return "审核通过";
        case "3":
            return "撤销审核";
    }
}
export const historyListStatus = function (parm) { // 审核状态
    switch (parm) {
        case "1":
            return "新建单据";
        case "2":
            return "提交审核";
        case "3":
            return "取消审核";
        case "4":
            return "驳回审核";
        case "5":
            return "通过审核";
        case "6":
            return "撤消审核";

    }
}
export const receivedStatus = function (parm) { // 收货情况
    switch (parm) {
        case "1":
            return "未收货";
        case "2":
            return "已收货";
        case "3":
            return "无";
    }
}
export const productTpyeState = function (parm) { // 商品属性
    switch (parm) {
        case "1":
            return "成品";
        case "2":
            return "旧料";
    }
}
export const statusCashStatus = function (parm) { // 收银状态
    switch (parm) {
        case "1":
            return "已收银";
        case "2":
            return "待收银";
    }
}