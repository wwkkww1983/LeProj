// 发送普通消息
export const sendMsg = (text, cb) => {
  if (!selToID) {
    console.log("你还没有选中好友或者群组，暂不能聊天");
    return;
  }
  let msgtosend = text
  // const msgLen = webim.Tool.getStrBytes(msgtosend);
  if (msgtosend.length < 1) {
    return;
  }
  if (!selSess) {
    selSess = new webim.Session(selType, selToID, selToID, friendHeadUrl, Math.round(new Date().getTime() / 1000));
    // console.log(selSess)
  }
  let isSend = true
  let random = Math.round(Math.random() * 4294967296)
  let msgTime = Math.round(new Date().getTime() / 1000);
  let subType;// 消息子类型
  if (selType === webim.SESSION_TYPE.C2C) {
    subType = webim.C2C_MSG_SUB_TYPE.COMMON;
  } else {
      // webim.GROUP_MSG_SUB_TYPE.COMMON-普通消息,
      // webim.GROUP_MSG_SUB_TYPE.LOVEMSG-点赞消息，优先级最低
      // webim.GROUP_MSG_SUB_TYPE.TIP-提示消息(不支持发送，用于区分群消息子类型)，
      // webim.GROUP_MSG_SUB_TYPE.REDPACKET-红包消息，优先级最高
    subType = webim.GROUP_MSG_SUB_TYPE.COMMON;
  }
  var seq = -1;
  let msg = new webim.Msg(selSess, isSend, seq, random, msgTime, loginInfo.identifier, subType, loginInfo.identifierNick);
 // console.log(msg)
  var text_obj, face_obj, tmsg, emotionIndex, emotion, restMsgIndex;
  // 解析文本和表情
  var expr = /\[[^[\]]{1,3}\]/mg; // eslint-disable-line
  var emotions = msgtosend.match(expr);
  if (!emotions || emotions.length < 1) {
    text_obj = new webim.Msg.Elem.Text(msgtosend);
    msg.addText(text_obj);
  } else {
    for (var i = 0; i < emotions.length; i++) {
      tmsg = msgtosend.substring(0, msgtosend.indexOf(emotions[i]));
      if (tmsg) {
        text_obj = new webim.Msg.Elem.Text(tmsg);
        msg.addText(text_obj);
      }
      emotionIndex = webim.EmotionDataIndexs[emotions[i]];
      emotion = webim.Emotions[emotionIndex];

      if (emotion) {
        face_obj = new webim.Msg.Elem.Face(emotionIndex, emotions[i]);
        msg.addFace(face_obj);
      } else {
        text_obj = new webim.Msg.Elem.Text(emotions[i]);
        msg.addText(text_obj);
      }
      restMsgIndex = msgtosend.indexOf(emotions[i]) + emotions[i].length;
      msgtosend = msgtosend.substring(restMsgIndex);
    }
    if (msgtosend) {
      text_obj = new webim.Msg.Elem.Text(msgtosend);
      msg.addText(text_obj);
    }
  }
  // let text_obj, face_obj, tmsg, emotionIndex, emotion, restMsgIndex;
  webim.sendMsg(msg, function (resp) {
    if (selType === webim.SESSION_TYPE.C2C) { // 私聊时，在聊天窗口手动添加一条发的消息，群聊时，长轮询接口会返回自己发的消息
    }
    if (cb instanceof Function) {
        cb(msg)
     }
    webim.Tool.setCookie("tmpmsg_" + selToID, '', 0);
  }, function (err) {
    console.log(err)
  })
}
