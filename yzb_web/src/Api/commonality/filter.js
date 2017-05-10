// 过滤器
export const readOnly = function (parm) {
    let onlyReady = new Set(['status', 'totalWeight', 'heavyCode', 'goldE', 'mainPrice', 'deputyPrice', 'inMoney', 'price', 'costPrice']);
    let onlyState = [parm].filter(x => onlyReady.has(x));
    if (onlyState.length > 0) {
        return true
    } else {
        return false
    }
}
