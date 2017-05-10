// 计算
export const computedStorageData = function (littleClassKey, index, datas) { // 入库的字段计算
    let computedVal = null;
    switch (littleClassKey) {
        case "totalWeight": // 总件量
            if (datas.unit === "ct") {
                computedVal = ((Number(datas.heavyCode) || 0) + ((Number(datas.mainWeight) || 0) * 0.02) + ((Number(datas.deputyWeight) || 0) * 0.02)).toFixed(3);
                // computedVal = ((Number(datas.heavyCode) || 0) +  ((Number(datas.mainWeight) || 0) * 0.02) + ((Number(datas.deputyWeight) || 0) * 0.02)).toFixed(3);
            } else if (datas.unit === "g") {
                computedVal = ((Number(datas.heavyCode) || 0) + (Number(datas.mainWeight) || 0) + (Number(datas.deputyWeight) || 0)).toFixed(3);
            }
            break;
        case "heavyCode": // 含配金重 净金重(productWeight.netWeight) + 配件重(parts.dpartWeight)
            computedVal = ((Number(datas.netWeight) || 0) + (Number(datas.partWeight) || 0)).toFixed(3);
            break;
        case "goldE": // 金料额(1 + 金耗(goldCost)) * 金价(goldPrice)
            computedVal = (Number(datas.netWeight) * (1 + Number(datas.goldCost)) * Number(datas.goldPrice)).toFixed(2);
            break;
        case "mainPrice": // (主石额)
            if (datas.mainCalcMethod === "计件") {
                computedVal = ((Number(datas.unitPrice) || 0) * (Number(datas.count) || 0)).toFixed(2);
            } else if (datas.mainCalcMethod === "计重") {
                computedVal = ((Number(datas.unitPrice) || 0) * (Number(datas.mainWeight) || 0)).toFixed(2);
            }
            break;
        case "deputyPrice": // (副石额)
            if (datas.deputyCalcMethod === "计件") {
                computedVal = ((Number(datas.deputyUnitPrice) || 0) * (Number(datas.deputyCount) || 0)).toFixed(2);
            } else if (datas.deputyCalcMethod === "计重") {
                computedVal = ((Number(datas.deputyUnitPrice) || 0) * (Number(datas.deputyWeight) || 0)).toFixed(2);
            }
            break;
        case "inMoney": // 进货工费额
            if (datas.inMethod === "计件") {
                computedVal = ((Number(datas.inFee)) || 0).toFixed(2);
            } else if (datas.inMethod === "计重") {
                computedVal = (Number(datas.netWeight) * Number(datas.inFee)).toFixed(2);
            }
            break;
        case "price": // (配件额)
            if (datas.calcMethod === "计件") {
                computedVal = (Number(datas.partCount) * Number(datas.partPrice)).toFixed(2);
            } else if (datas.calcMethod === "计重") {
                computedVal = (Number(datas.partWeight) * Number(datas.partPrice)).toFixed(2);
            }
            break;
        case "costPrice": // 成本 （证书费没处理）
            computedVal = ((Number(datas.goldE) || 0) + (Number(datas.certifiFee) || 0) + (Number(datas.mainPrice) || 0) + (Number(datas.deputyPrice) || 0) + (Number(datas.inMoney) || 0) + (Number(datas.price) || 0) + (Number(datas.otherFee) || 0)).toFixed(2);
            break;
            // computedVal = Math.round((Number(datas.goldE) || 0) + (Number(datas.certifiFee) || 0) + (Number(datas.mainPrice) || 0) + (Number(datas.deputyPrice) || 0) + (Number(datas.inMoney) || 0) + (Number(datas.price) || 0) + (Number(datas.otherFee) || 0));
            // break;
        default:
            computedVal = datas[littleClassKey];
    }
    return computedVal;
}
