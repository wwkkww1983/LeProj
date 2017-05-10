import Vue from 'vue'
import elementUI from 'element-ui'
import 'element-ui/lib/theme-default/index.css'
import resource from 'vue-resource'
import App from './App'
import router from './router'
import store from './vuex'

Vue.use(elementUI)
Vue.use(resource)

/* eslint-disable no-new */
new Vue({
    el: '#app',
    router: router,
    store: store,
    template: '<App/>',
    components: {
        App
    }
})