// 待删除
import Vue from 'vue'
import resource from 'vue-resource'
Vue.use(resource)

// 创建api接口方法
function apiCall (parm, URL) {
    let data = {
        "data": parm,
        "unit": {
            "companyId": sessionStorage.getItem("companyId"),
            "channel": 3,
            "os": "string",
            "ip": "string",
            "userId": sessionStorage.getItem("id"),
            "tokenId": sessionStorage.getItem("tokenId")
        }
    }
    sessionStorage.setItem("ApiData", JSON.stringify(data));
    let foo = function* (data) {
        console.log(data);
        yield Vue.http.post(URL, data)
    }
    return foo(data).next().value
}

export default apiCall
