import * as types from '../mutation-types.js'
const store = {
  state: {
    userInfo: null,
    comInfo: null
  },
  mutations: {
    [types.SET_USER_INFO] (state, obj) {
      state.userInfo = obj
    },
    [types.SET_COM_INFO] (state, obj) {
      state.comInfo = obj
    }
  }
}
export default store;
