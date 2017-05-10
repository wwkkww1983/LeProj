import * as types from "./../mutation-types.js"
const store = {
    state: {
        // popupAudit: false, // 审核弹窗
        // errorPopup: false, // 错误弹窗
        applyUserList: [], // 应用用户列表
        // userType: 2, // 1.部门，2.店铺（部门可以编辑，店铺不可以编辑）监听编辑权限
        // productStatus: 2, // 1 待审核 2 审核中 3 已审核
        productClass: [], // 产品类别（商品类型列表）
        supplierList: [], // 供应商列表
        applyDepartementList: [], // 计划分销列表
        repositoryList: [], // 库位列表
        receiptDetail: [], // 单据详情
        productList: [], // 单据列表（公共数据）
        rowDataList: [], // 商品列表（销售）
        // goodsList: [], // 商品列表（销售除外）
        delcectSelect: [], // 删除的选择集合
        applyDepartmentList: [], // 获取操作部门（含有店铺）
        applyOnlyDepartment: [], // 只有操作部门
        applyOnlyShop: [], // 只有店铺
        shopLists: [], // 店铺列表(分销商) 待删
        shopListByCo: [], // 店铺列表
        counterList: [], // 柜组列表
        productTypeList: [ // 商品属性列表
            {
                "name": "成品",
                "productType": "1"
            },
            {
                "name": "旧料",
                "productType": "2"
            }
        ],
        littleClass: [ // 小类
            { // 首饰名称
              "name": "首饰名称",
              "colspan": 4,
              "colspanOpen": 4,
              "englishName": "jewelryCategory",
              "admentUrl": "batchUpdateJewelryCategory",
              "stateOpen": "jewelryCategory", // 开和关状态
              "propertyDatas": [{
                  "name": "成色名称",
                  "englishName": "metalColor"
              }, {
                  "name": "含金量",
                  "englishName": "partGold"
              }, {
                  "name": "宝石名称",
                  "englishName": "gemName"
              }, {
                  "name": "首饰类别",
                  "englishName": "jewelryName"
              }],
              "open": [{
                  "name": "成色名称",
                  "englishName": "metalColor"
              }, {
                  "name": "含金量",
                  "englishName": "partGold"
              }, {
                  "name": "宝石名称",
                  "englishName": "gemName"
              }, {
                  "name": "首饰类别",
                  "englishName": "jewelryName"
              }]
            }, { // 基本信息
              "name": "基本信息",
              "colspan": 1,
              "colspanOpen": 4,
              "englishName": "baseInfo",
              "admentUrl": "batchUpdateBaseInfo",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                "name": "品牌",
                "englishName": "brand"
              }, {
                "name": "款号",
                "englishName": "modelNo"
              }, {
                "name": "手寸",
                "englishName": "handInch"
              }, {
                "name": "手寸单位",
                "englishName": "handInchUnit"
              }]
            }, { // 重量
              "name": "重量",
              "colspan": 1,
              "colspanOpen": 7,
              "englishName": "productWeight",
              "admentUrl": "batchUpdateProductWeight",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                  "name": "总件重（g）",
                  "englishName": "totalWeight"
              }, {
                  "name": "净金重（g）",
                  "englishName": "netWeight"
              }, {
                  "name": "含配金重（g）",
                  "englishName": "heavyCode"
              }, {
                  "name": "金耗（%）",
                  "englishName": "goldCost"
              }, {
                  "name": "金价（元）",
                  "englishName": "goldPrice"
              }, {
                  "name": "金属颜色",
                  "englishName": "goldColor"
              }, {
                  "name": "金料额（元）",
                  "englishName": "goldE"
              }]
            }, { // 证书
              "name": "证书",
              "colspan": 1,
              "colspanOpen": 10,
              "englishName": "certificate",
              "admentUrl": "batchUpdateCertifite",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                  "name": "证书号",
                  "englishName": "certifiNo"
              }, {
                  "name": "验证码",
                  "englishName": "authCode"
              }, {
                  "name": "证书名",
                  "englishName": "certifiName"
              }, { // 选择后生成的
                  "name": "检验机构",
                  "englishName": ""
              }, {
                  "name": "网址",
                  "englishName": ""
              }, {
                  "name": "电话",
                  "englishName": ""
              }, {
                  "name": "检测标准1",
                  "englishName": ""
              }, {
                  "name": "检测标准2",
                  "englishName": ""
              }, {
                  "name": "检测标准3",
                  "englishName": ""
              }, {
                  "name": "证书费（元）",
                  "englishName": "certifiFee"
              }]
            }, { // 主石
              "name": "主石",
              "colspan": 1,
              "colspanOpen": 15,
              "englishName": "deputyStoneZhu",
              "admentUrl": "batchUpdateDeputyStone",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                  "name": "主石名",
                  "englishName": "mainName"
              }, {
                  "name": "主石规格",
                  "englishName": "stand"
              }, {
                  "name": "主石粒数",
                  "englishName": "count"
              }, {
                  "name": "主石重",
                  "englishName": "mainWeight"
              }, {
                  "name": "主石单位",
                  "englishName": "unit"
              }, {
                  "name": "主石单价（元）",
                  "englishName": "unitPrice"
              }, {
                  "name": "计价方式",
                  "englishName": "mainCalcMethod"
              }, {
                  "name": "形状",
                  "englishName": "shape"
              }, {
                  "name": "颜色",
                  "englishName": "color"
              }, {
                  "name": "净度",
                  "englishName": "neatNess"
              }, {
                  "name": "切工",
                  "englishName": "blackout"
              }, {
                  "name": "抛光",
                  "englishName": "polishing"
              }, {
                  "name": "对称",
                  "englishName": "symmetry"
              }, {
                  "name": "荧光",
                  "englishName": "fluorescent"
              }, {
                  "name": "主石额",
                  "englishName": "mainPrice"
              }]
            }, { // 副石
              "name": "副石",
              "colspan": 1,
              "colspanOpen": 8,
              "englishName": "deputyStoneFus",
              "admentUrl": "batchUpdateDeputyStone",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                  "name": "副石名",
                  "englishName": "deputyName"
              }, {
                  "name": "副石规格",
                  "englishName": "deputyStand"
              }, {
                  "name": "副石粒数",
                  "englishName": "deputyCount"
              }, {
                  "name": "副石重",
                  "englishName": "deputyWeight"
              }, {
                  "name": "副石单位",
                  "englishName": "deputyUnit"
              }, {
                  "name": "副石单价（元）",
                  "englishName": "deputyUnitPrice"
              }, {
                  "name": "计价方式",
                  "englishName": "deputyCalcMethod"
              }, {
                  "name": "副石额（元）",
                  "englishName": "deputyPrice"
              }]
            }, { // 工费
              "name": "工费",
              "colspan": 1,
              "colspanOpen": 5,
              "englishName": "operateCost",
              "admentUrl": "batchUpdateOperateCost",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                  "name": "销售工费（元）",
                  "englishName": "soldFee"
              }, {
                  "name": "销售工费方式",
                  "englishName": "soldMethod"
              }, {
                  "name": "进货工费（元）",
                  "englishName": "inFee"
              }, {
                  "name": "进货工费方式",
                  "englishName": "inMethod"
              }, {
                  "name": "进货工费额（元）",
                  "englishName": "inMoney"
              }]
            }, { // 配件
              "name": "配件",
              "colspan": 1,
              "colspanOpen": 6,
              "englishName": "parts",
              "admentUrl": "batchUpdateParts",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                  "name": "配件名",
                  "englishName": "partName"
              }, {
                  "name": "配件数",
                  "englishName": "partCount"
              }, {
                  "name": "配件重（g）",
                  "englishName": "partWeight"
              }, {
                  "name": "单价（元）",
                  "englishName": "partPrice"
              }, {
                  "name": "计价方式",
                  "englishName": "calcMethod"
              }, {
                  "name": "配件额（元）",
                  "englishName": "price"
              }]
            }, { // 其它费用
              "name": "其它费用",
              "colspan": 1,
              "colspanOpen": 2,
              "englishName": "otherFee",
              "admentUrl": "batchUpdateOtherFee",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                  "name": "其它费用名",
                  "englishName": "otherFeeName"
              }, {
                  "name": "其它费用额（元）",
                  "englishName": "otherFee"
              }]
            }, { // 标价
              "name": "标价",
              "colspan": 1,
              "colspanOpen": 10,
              "englishName": "priceTag",
              "admentUrl": "batchUpdatePriceTag",
              "stateOpen": "", // 开和关状态
              "propertyDatas": [],
              "open": [{
                  "name": "金料额",
                  "englishName": ""
              }, {
                  "name": "证书费",
                  "englishName": ""
              }, {
                  "name": "主石额",
                  "englishName": ""
              }, {
                  "name": "副石额",
                  "englishName": ""
              }, {
                  "name": "工费额",
                  "englishName": ""
              }, {
                  "name": "配件额",
                  "englishName": ""
              }, {
                  "name": "其它费用额",
                  "englishName": ""
              }, {
                  "name": "成本",
                  "englishName": "costPrice"
              }, {
                  "name": "倍率",
                  "englishName": "ratio"
              }, {
                  "name": "售价（元）",
                  "englishName": "soldPrice"
              }]
            }
        ],
        limitInput: { // 输入限制
            // 条形码
            barcode: {
                maxlength: 8,
                pureNumber: true // 纯数字
            },
            // 基本信息
            modelNo: { // 款号
                maxlength: 12,
                onlyMaxlength: true // 只限制长度
                // pureNumber: true // 纯数字
            },
            handInch: { // 手寸
                maxlength: 6,
                sliceNumber: 1
            },
            // 重量
            totalWeight: { // 总件量 小数点后3位
                sliceNumber: 3 // 小数
            },
            netWeight: { // 净金重量 小数点后3位 限12个字
                maxlength: 12,
                sliceNumber: 3
            },
            heavyCode: { // 含配金重 小数点后3位
                sliceNumber: 3
            },
            goldCost: { // 金耗 小数点后2位goldCost
                sliceNumber: 2,
                maxlength: 8
            },
            goldPrice: { // 金价 小数点后2位 限12个字
                maxlength: 12,
                sliceNumber: 2
            },
            goldE: { // 金料额 小数点后2位
                sliceNumber: 2
            },
            // 证书
            certifiNo: { // 证书号
                maxlength: 12,
                onlyMaxlength: true // 只限制长度
            },
            authCode: { // 验证码
                maxlength: 12,
                onlyMaxlength: true // 只限制长度
            },
            certifiFee: { // 证书费
                maxlength: 12,
                sliceNumber: 2
            },
            // 主石
            count: { // 主石粒数
                maxlength: 12,
                integer: true
            },
            mainWeight: { // 主石重
                maxlength: 12,
                sliceNumber: 3
            },
            unitPrice: { // 主石单价
                maxlength: 12,
                sliceNumber: 2
            },
            // 副石
            deputyCount: { // 副石粒数
                maxlength: 12,
                integer: true
            },
            deputyWeight: { // 副石重
                maxlength: 12,
                sliceNumber: 3
            },
            deputyUnitPrice: { // 副石单价
                maxlength: 12,
                sliceNumber: 2
            },
            // 工费
            soldFee: { // 销售工费
                maxlength: 6,
                sliceNumber: 2
            },
            inFee: { // 进货工费
                maxlength: 6,
                sliceNumber: 2
            },
            // 配件
            partCount: { // 配件数
                maxlength: 12,
                integer: true
            },
            partWeight: { // 配件重
                maxlength: 12,
                sliceNumber: 3
            },
            partPrice: { // 单价
                maxlength: 12,
                sliceNumber: 2
            },
            otherFeeName: { // 其它费用名
                onlyMaxlength: true, // 只限制长度
                maxlength: 12
            },
            otherFee: { // 其他费用额
                maxlength: 12,
                sliceNumber: 2
            },
            // 标价
            ratio: { // 倍率
                sliceNumber: 2
            },
            // soldPrice: { // 售价
            //   integer: true,
            //   sliceNumber: 2
            // },
            remark: {
                maxlength: 30,
                onlyMaxlength: true // 只限制长度
            }
            // barginPrice: { // 促销价(得特殊处理)
            //   sliceNumber: 2
            // }
        },
        configPullDownData: { // 配置下拉数据
            // 首饰名称
            "metalColor": [], // 成色名称
            "partGold": [], // 含金量
            "gemName": [], // 宝石名称
            "jewelryName": [], // 首饰类别
            // 基本信息
            "brand": [], // 品牌
            "handInchUnit": [ // 手寸单位
                {
                    "name": "#"
                }, {
                    "name": "寸"
                }, {
                    "name": "cm"
                }
            ],
            // 重量
            "goldColor": [], // 金属颜色
            // 证书
            "certifiName": [], // 证书名
            // 主石
            "mainName": [], // 主石名
            "stand": [], // 主石规格
            "unit": [ // 主石单位
                {
                    "name": "ct"
                }, {
                    "name": "g"
                }
            ],
            "mainCalcMethod": [ // 计价方式
                {
                    "name": "计件"
                },
                {
                    "name": "计重"
                }
            ],
            "shape": [], // 形状
            "color": [], // 颜色
            "neatNess": [], // 净度
            "blackout": [], // 切工
            "polishing": [], // 抛光
            "symmetry": [], // 对称
            "fluorescent": [], // 荧光
            // 副石
            "deputyName": [], // 副石名
            "deputyStand": [], // 副石规格
            "deputyUnit": [ // 副石单位
                {
                    "name": "ct"
                }, {
                    "name": "g"
                }
            ],
            "deputyCalcMethod": [ // 计价方式
                {
                    "name": "计件"
                }, {
                    "name": "计重"
                }
            ],
            // 工费
            "soldMethod": [ // 销售工费方式
                {
                    "name": "计件"
                }, {
                    "name": "计重"
                }
            ],
            "inMethod": [ // 进货工费方式
                {
                    "name": "计件"
                }, {
                    "name": "计重"
                }
            ],
            // 配件
            "partName": [], // 配件名
            "calcMethod": [ // 计价方式
                {
                    "name": "计件"
                }, {
                    "name": "计重"
                }
            ]
        },
        rowConfigData: { // 一行数据
            "productId": "", // 商品ID(用来做删除用的)
            "barcode": "", // 条形码
            // "status": "", // 当前状态
            "metalColor": "", // 成色名称
            "partGold": "", // 含金量
            "gemName": "", // 宝石名称
            "jewelryName": "", // 首饰名称
            // 基本信息
            "brand": "", // 品牌
            "modelNo": "", // 款号
            "handInch": "", // 手寸
            "handInchUnit": "#", // 手寸单位
            // 重量
            "totalWeight": "", // 总件量
            "netWeight": "", // 净金重量
            "heavyCode": "", // 含配金重
            "goldCost": "", // 金耗
            "goldPrice": "", // 金价
            "goldColor": "", // 金属颜色
            "goldE": "", // 金料额
            // 证书
            "certifiNo": "", // 证书号
            "authCode": "", // 验证码
            "certifiName": "", // 证书名
            // 插值2222222222222222222222222222（6）
            "certifiFee": "", // 证书费
            // 主石
            "mainName": "", // 主石名
            "stand": "", // 主石规格
            "count": "", // 主石粒数
            "mainWeight": "", // 主石重
            "unit": "ct", // 主石单位
            "unitPrice": "", // 主石单价
            "mainCalcMethod": "计重", // 计价方式
            "shape": "", // 形状deputyStoneZhu
            "color": "", // 颜色
            "neatNess": "", // 净度
            "blackout": "", // 切工fuStone
            "polishing": "", // 抛光
            "symmetry": "", // 对称
            "fluorescent": "", // 荧光
            "mainPrice": "", // 主石额
            // 副石
            "deputyName": "", // 副石名
            "deputyStand": "", // 副石规格
            "deputyCount": "", // 副石粒数
            "deputyWeight": "", // 副石重
            "deputyUnit": "ct", // 副石单位
            "deputyUnitPrice": "", // 副石单价
            "deputyCalcMethod": "计重", // 计价方式
            "deputyPrice": "", // 副石额
            // 工费
            "soldFee": "", // 销售工费
            "soldMethod": "计重", // 销售工费方式
            "inFee": "", // 进货工费
            "inMethod": "计重", // 进货工费方式
            "inMoney": "", // 进货工费额
            // 配件
            "partName": "", // 配件名
            "partCount": "", // 配件数
            "partWeight": "", // 配件重
            "partPrice": "", // 单价
            "calcMethod": "计重", // 计价方式
            "price": "", // 配件额
            // 其它费用
            "otherFeeName": "", // 其它费用名
            "otherFee": "", // 其他费用额
            // 标价
            // "goldFee": null, // 金料额
            // "totalCertifiFee": null, // 证书费
            // 插值2222222222222222222222222222（2）
            // "mainPrice": null, // 主石额
            // "deputyPrice": null, // 副石额
            // "operateFee": null, // 工费额
            // 插值2222222222222222222222222222（1）
            // "partsPrice": null, // 配件额
            // "partsPrice": null, // 配件额
            // "otherFeeTag": null, // 其他费用额 对应标价表other_fee字段
            "costPrice": "0.00", // 成本
            "ratio": "0.00", // 倍率
            "soldPrice": "0.00", // 售价
            // "barginPrice": null, // 促销价
            // 备注
            "remark": null
        }
    },
    mutations: {
        // [types.WORK_POPUP_AUDIT] (state, parm) { // 审核弹窗
        //     state.popupAudit = parm
        // },
        [types.WORK_COUNTTER_LIST] (state, parm) { // 柜组列表
            state.counterList = parm;
        },
        [types.WORK_APPLY_USER] (state, parm) { // 应用用户列表
            state.applyUserList = parm;
        },
        // [types.WORK_USER_TYPE] (state, parm) { // 监听编辑权限
        //     state.userType = parm;
        // },
        // [types.WORK_PRODUCT_STATUS] (state, parm) { // 监听单据是否审核
        //     state.productStatus = parm;
        // },
        [types.WORK_PRODUCT_CLASS] (state, parm) { // 产品类别（商品类型列表）
            state.productClass = parm;
        },
        [types.WORK_SUPPLIER_LIST] (state, parm) { // 供应商列表
            state.supplierList = parm;
        },
        [types.WORK_APPLY_DEPARTEMENT] (state, parm) { // 用户所有部门、店铺权限列表 (计划分销)
            state.applyDepartementList = parm;
        },
        [types.WORK_REPOSITORY_LIST] (state, parm) { // 用户所有部门、店铺权限列表 (计划分销)
            state.repositoryList = parm;
        },
        [types.WORK_RECEIPT_DETAIL] (state, parm) { // 单据详情
            state.receiptDetail = parm;
        },
        [types.WORK_PRODUCT_LIST] (state, parm) { // 单据列表
            state.productList = parm;
        },
        [types.WORK_PRODUCT_DETAIL] (state, parm) { // 商品列表(销售)
            state.rowDataList = parm;
        },
        // [types.SEEK_GOODS_LIST] (state, parm) { // 商品列表(销售除外)
        //     state.goodsList = parm;
        // },
        [types.WORK_DELECT_SELECTS] (state, parm) { // 删除的选择集合
            state.delcectSelect = parm;
        },
        [types.WORK_DEPARTMENT_LIST] (state, parm) { // 获取操作部门
            state.applyDepartmentList = parm;
        },
        [types.WORK_ONLY_DEPARTMENT] (state, parm) { // 只有操作部门
            state.applyOnlyDepartment = parm;
        },
        [types.WORK_ONLY_SHOP] (state, parm) { // 只有店铺
            state.applyOnlyShop = parm;
        },
        [types.WORK_SHOP_LIST] (state, parm) { // 店铺列表(分销商)
            state.shopLists = parm;
        },
        [types.WORK_SHOP_LIST_BY_CO] (state, parm) { // 店铺列表
            console.log("我看你抗咯");
            console.log(parm);
            state.shopListByCo = parm
        }
    }
}
export default store;
