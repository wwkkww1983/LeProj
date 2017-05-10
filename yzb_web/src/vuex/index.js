import Vue from 'vue'
import Vuex from 'vuex'
import * as actions from './actions'
import * as getters from './getters'
// modules
import base from './modules/base'
import im from './modules/im'
import IM from './IM/im'
// work
import sipping from './work/sipping'
import salesReturn from './work/salesReturn'
import workCommonality from './work/workCommonality'
import popup from './work/popup' // 弹窗
import permissions from './work/permissions'
import sell from './work/sell' // 销售
import template from './work/template'
Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        base,
        im,
        IM,
        sell,
        sipping,
        salesReturn,
        workCommonality,
        popup,
        permissions,
        template
    },
    actions,
    getters
})