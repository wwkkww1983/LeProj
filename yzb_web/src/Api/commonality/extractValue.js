// 提取返回值
// 通过初始值和改变值提取改变的key: value,配上相应的商品Id
export const productOrId = function (initialValue, changeValue) { // 初始值，改变值
    let admendList = [];
    sessionStorage.setItem("AAA", JSON.stringify(initialValue));
    sessionStorage.setItem("BBB", JSON.stringify(changeValue));
    for (let i = 0; i < initialValue.length; i++) {
        var admendObj = {};
        for (let initialKey in initialValue[i]) {
            if (initialValue[i][initialKey] !== changeValue[i][initialKey]) {
                admendObj[initialKey] = changeValue[i][initialKey];
                if (changeValue[i].productId && !admendObj.productId) { // 添加个商品id
                    admendObj.productId = changeValue[i].productId;
                }
            }
        }
        admendList.push(admendObj);
    }
    return admendList;
}
