import * as types from "./../mutation-types.js"
const store = {
    state: {
        delcectSelect: null, // 删除的选择集合
        newSipping: {
            seekBarCodeIndex: null, // 搜索的索引
            seekBarCodeNumber: null // 用户输入的条形码
        }
    },
    mutations: {
        [types.DELECT_SELECTS] (state, parm) {
            state.delcectSelect = parm;
        },
        [types.SEEK_BARCODE] (state, parm) {
            state.newSipping.seekBarCodeIndex = parm;
        }
    }
}
export default store;
