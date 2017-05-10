// 弹窗
import * as types from "./../mutation-types.js"
const store = {
    state: {
        popupAudit: false, // 审核弹窗
        popupError: false, // 错误提示弹窗
        saveSuccess: false, // 保存弹窗
        saveSuccessData: null, // 保存弹窗数据
        affirmAudit: false // 审核的确认弹窗
    },
    mutations: {
        [types.WORK_POPUP_AUDIT] (state, parm) { // 审核弹窗
            state.popupAudit = parm
        },
        [types.WORK_POPUP_ERROR] (state, parm) { // 错误提示弹窗
            state.popupError = parm
        },
        [types.WORK_POPUP_SAVE] (state, parm) { // 保存弹窗
            state.saveSuccess = parm.saveSuccess;
            state.saveSuccessData = parm.saveSuccessData;
        },
        [types.WORK_POPUP_AFFIRM] (state, parm) { // 审核的确认弹窗
            state.affirmAudit = parm
        }
    }
}
export default store;
