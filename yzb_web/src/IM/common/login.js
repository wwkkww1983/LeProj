const login = function (onMsgNotify, cbOk) {
  let listeners = {
    'onConnNotify': onConnNotify, // 监听连接状态回调变化事件,必填
    'jsonpCallback': jsonpCallback, // IE9(含)以下浏览器用到的jsonp回调函数，
    'onMsgNotify': onMsgNotify // 监听新消息，必填
  }
  let options = {
    // 'isAccessFormalEnv': isAccessFormalEnv, // 是否访问正式环境，默认访问正式，选填
    'isLogOn': false // 是否开启控制台打印日志,默认开启，选填
  }
  webim.login(loginInfo, listeners, options,
    function (resp) { // 登录后回调
      console.log("登录IM成功回调");
      console.log(resp);
      cbOk(resp)
    },
    function (err) {
      console.log("登录IM成功回调");
      console.log(err);
      console.log(err.ErrorInfo);
    }
  )
}
export default login
