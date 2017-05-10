// 权限
import * as types from "./../mutation-types.js"
const store = {
    state: {
        userType: 2, // 1.部门，2.店铺（部门可以编辑，店铺不可以编辑）监听编辑权限
        productStatus: 2 // 1 待审核 2 审核中 3 已审核
    },
    mutations: {
        [types.WORK_USER_TYPE] (state, parm) { // 监听编辑权限
            state.userType = parm;
        },
        [types.WORK_PRODUCT_STATUS] (state, parm) { // 监听单据是否审核
            state.productStatus = parm;
        }
    }
}
export default store;
