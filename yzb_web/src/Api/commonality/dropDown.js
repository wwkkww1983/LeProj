// 下拉数据
// 暂时放的计算属性
// export const computedStorageData = function (littleClassKey, index, alLData) { // 入库的字段计算
    // let computedVal = null;
    // switch (littleClassKey) {
    //     case "totalWeight": // 总件量
    //         computedVal = ((Number(alLData[index].heavyCode) || 0) + (Number(alLData[index].mainWeight) || 0) + (Number(alLData[index].deputyWeight) || 0)).toFixed(3);
    //         break;
    //     case "heavyCode": // 含配金重 净金重(productWeight.netWeight) + 配件重(parts.dpartWeight)
    //         computedVal = ((Number(alLData[index].netWeight) || 0) + (Number(alLData[index].partWeight) || 0)).toFixed(3);
    //         break;
    //     case "goldE": // 金料额(1 + 金耗(goldCost)) * 金价(goldPrice)
    //         computedVal = (Number(alLData[index].netWeight) * (1 + Number(alLData[index].goldCost)) * Number(alLData[index].goldPrice)).toFixed(2);
    //         break;
    //     case "mainPrice": // (主石额)
    //         if (alLData[index].mainCalcMethod === "计件") {
    //             computedVal = ((Number(alLData[index].unitPrice) || 0) * (Number(alLData[index].count) || 0)).toFixed(2);
    //         } else if (alLData[index].mainCalcMethod === "计重") {
    //             computedVal = ((Number(alLData[index].unitPrice) || 0) * (Number(alLData[index].mainWeight) || 0)).toFixed(2);
    //         }
    //         break;
    //     case "deputyPrice": // (副石额)
    //         if (alLData[index].deputyUnit === "计件") {
    //             computedVal = ((Number(this.allDatas[index].deputyUnitPrice) || 0) + (Number(this.allDatas[index].deputyCount) || 0)).toFixed(2);
    //         } else if (alLData[index].deputyUnit === "计重") {
    //             computedVal = ((Number(this.allDatas[index].deputyUnitPrice) || 0) + (Number(this.allDatas[index].deputyWeight) || 0)).toFixed(2);
    //         }
    //         break;
    //     case "inMoney": // 进货工费额
    //         if (alLData[index].inMethod === "计件") {
    //             computedVal = ((Number(alLData[index].inFee)) || 0).toFixed(2);
    //         } else if (alLData[index].inMethod === "计重") {
    //             computedVal = (Number(alLData[index].netWeight) * Number(alLData[index].inFee)).toFixed(2);
    //         }
    //         break;
    //     case "price": // (配件额)
    //         if (alLData[index].calcMethod === "计件") {
    //             computedVal = (Number(alLData[index].partCount) * Number(alLData[index].partPrice)).toFixed(2);
    //         } else if (alLData[index].calcMethod === "计重") {
    //             computedVal = (Number(alLData[index].partWeight) * Number(alLData[index].partPrice)).toFixed(2);
    //         }
    //         break;
    //     case "costPrice": // 成本 （证书费没处理）
    //         computedVal = Math.round((Number(alLData[index].goldE) || 0) + (Number(alLData[index].certifiFee) || 0) + (Number(alLData[index].mainPrice) || 0) + (Number(alLData[index].deputyPrice) || 0) + (Number(alLData[index].inMoney) || 0) + (Number(alLData[index].price) || 0) + (Number(alLData[index].otherFee) || 0));
    //         break;
    //     default:
    //         computedVal = alLData[index][littleClassKey];
    // }
//     return computedVal;
// }