/* 基础信息 */
export const userInfo = state => state.base.userInfo // 用户信息
export const comInfo = state => state.base.comInfo // 公司信息
/* IM 新 */
export const imOnMsgNotify = state => state.IM.imOnMsgNotify // 好友列表
export const imCutFriendGroup = state => state.IM.imCutFriendGroup // 切换好友和群的当前数据
/* IM系统 旧 */
export const friendList = state => state.im.friendList // 好友列表
export const groupList = state => state.im.groupList // 群组列表
export const recentContacts = state => state.im.recentContacts // 最近联系人列表
export const sysMess = state => state.im.sysMess // 系统消息
export const stick = state => state.im.stick // 消息置顶
// 状态类
export const popupAudit = state => state.popup.popupAudit // 审核弹窗
export const popupError = state => state.popup.popupError // 审核弹窗
export const saveSuccess = state => state.popup.saveSuccess // 保存弹窗
export const saveSuccessData = state => state.popup.saveSuccessData // 保存弹窗数据
export const affirmAudit = state => state.popup.affirmAudit // 审核的确认弹窗
// 一些基本下拉数据
export const applyUserList = state => state.workCommonality.applyUserList // 应用用户列表
export const productClass = state => state.workCommonality.productClass // 产品类别（商品类型列表）
export const supplierListData = state => state.workCommonality.supplierList // 获取供应商列表
export const applyDepartementList = state => state.workCommonality.applyDepartementList // 计划分销商
export const repositoryList = state => state.workCommonality.repositoryList // 库位列表
export const productTypeList = state => state.workCommonality.productTypeList // 商品属性列表
export const counterList = state => state.workCommonality.counterList // 柜组列表
export const littleClassList = state => state.workCommonality.littleClass // 小类
export const limitInput = state => state.workCommonality.limitInput // 输入限制
export const configPullDownDataList = state => state.workCommonality.configPullDownData // 配置下拉数据
export const rowConfigData = state => state.workCommonality.rowConfigData // 一行数据

// 权限
export const userType = state => state.permissions.userType // 监听编辑权限（部门）
export const productStatus = state => state.permissions.productStatus // 监听编辑权限（审核情况）
// 核心操作
// export const getDelcectSelects = state => state.sipping.delcectSelect // 发货多选框（删除）
export const getSeekData = state => state.sipping.newSipping // 新建发货单的输入搜索
export const returnSelectData = state => state.salesReturn.selectDatas // 获取新建带过来的数据（退货）
export const getProductDetail = state => state.workCommonality.rowDataList // 商品列表
// export const goodsList = state => state.workCommonality.goodsList // 商品列表(手机)
export const workDelectSelectsData = state => state.workCommonality.delcectSelect // 删除数据的集合
export const receiptDetail = state => state.workCommonality.receiptDetail // 单据详情
export const workProductListData = state => state.workCommonality.productList // 单据列表
export const applyDepartmentList = state => state.workCommonality.applyDepartmentList // 获取操作部门(包含店铺)
export const applyOnlyDepartment = state => state.workCommonality.applyOnlyDepartment // 只有部门
export const applyOnlyShop = state => state.workCommonality.applyOnlyShop // 只有店铺
export const shopLists = state => state.workCommonality.shopLists // 店铺列表(分销商) 待删
export const shopListByCo = state => state.workCommonality.shopListByCo // 店铺列表
// 销售
export const sellProductList = state => state.sell.sellProductList // 商品列表-销售
