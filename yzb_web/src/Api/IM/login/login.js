// sdk登录
export const webimLogin = function (parm, cbOk) {
    var listeners = {
        'onConnNotify': onConnNotify, // 监听连接状态回调变化事件,必填
        'jsonpCallback': jsonpCallback, // IE9(含)以下浏览器用到的jsonp回调函数，
        'onMsgNotify': parm // 监听新消息，必填
    }
    webim.login(
        loginInfo, listeners, options,
        function (resp) {
            loginInfo.identifierNick = resp.identifierNick; // 设置当前用户昵称
    // 获取所有聊天会话
    var sessMap = webim.MsgStore.sessMap();
    console.log("获取所有聊天会话");
    console.log(sessMap);
    for (let i in sessMap) {
        var sess = sessMap[i];
        console.log(sess.type());
          console.log(sess.id());
          console.log(sess.name());
          console.log(sess.unread());
        if (selToID !== sess.id()) { // 更新其他聊天对象的未读消息数
          console.log(sess.type());
          console.log(sess.id());
          console.log(sess.name());
          console.log(sess.unread());
          // stopPolling = true;
        }
    }
            if (cbOk) {
                cbOk();
            }
        },
        function (err) {
            alert(err.ErrorInfo);
        }
    );
}
