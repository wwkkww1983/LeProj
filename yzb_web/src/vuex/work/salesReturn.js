import * as types from "./../mutation-types.js"
const store = {
    state: {
        selectDatas: null // 新建选择数据getSelectData
    },
    mutations: {
        [types.GET_RETURN_SELECT] (state, parm) { // 获取新建带过来的数据
            state.selectDatas = parm;
        }
    }
}
export default store;
