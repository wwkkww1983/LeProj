import Vue from 'vue'
import resource from 'vue-resource'
Vue.use(resource)

function apiCallClean (param, URL) {
    let IT_URL = INTERFACE_URL_9083 + URL;
    let foo = function* (up_data) {
        yield Vue.http.post(IT_URL, up_data)
    }
    return foo(param).next().value
}

export default apiCallClean