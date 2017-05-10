<!-- 模块 -->
//  入库         Storage
//  发货         Sipping
//  修改         Amend
//  退货         SalesReturn
//  调柜         TransferCabinet
//  调库         TransferStorage
//  退库         StorageReturn
//  收货         Receiving
//  销售         Sell
//  公司设置     CompanySetting
//  店铺设置     ShopSetting
//  公共模板模块 CommonalityComponent
 

新建： 开关名 switchName ==》 getShopName（）=====>等待改  bindingName ()
       开关Id switchId（由于太耦合，废除了）

newDatas: { // 新增数据(其它页面也用到的)
    // "productTypeName": null, // 商品属性（成品/旧料）(删)
    productTpye: null, // 商品属性（成品/旧料）
    handleDept: null, // 操作部门名
    handleDeptId: null, // 操作部门Id
    userType: null, // 1.部门，2.店铺（部门可以编辑，店铺不可以编辑）监听编辑权限
    newLocationId: null, // 发货库位Id （当前库位）// 新建时选的
    newLocation: null, // 调出库位 （当前库位）// 新建时选的
    orderNumber: null // 单据号 (这里只不过是给tableTemplate模板用)
    "outShopGroupId":"", //调出柜组ID
    "outShopGroupName":"", //调出柜组名
    "shopGroupId":"", //当前/调入柜组ID
    "shopGroupName":"", //当前/调入柜组名
},
header: {
    switchId: null // 只给模板的input做开关限制
    startTime: '', // 开始时间
    endTime: '', // 结束时间
    orderNumber: null, // 开始单号
    depShopId: null, // 操作部门
    productRepository: null, // 商品库位
    makeUserId: null, // 制单人
    auditorId: null, // 审核人
    shopId: null, // 分销商Id
    shopName: null // 分销商名
}

// 柜组 （待确定）
    当前柜组 ===》 调入柜组

// 数据管理
   再新建页面，新建成功后，把orderId 存本地，好的继续舔加时取，
   从tableTempalte进去也要存，方便编辑

// 翻译
    调出库位 ===》 出货库位


// 方法 ///// 

状态
getproductTpye: function (parm) { // 商品属性
    return productTpyeState(parm);
},

过滤商品属性
getproductTpye() 