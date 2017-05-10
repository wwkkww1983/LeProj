export const getLastC2CHistoryMsgs = function (options, cbOk, cbError) {

    if (selType === webim.SESSION_TYPE.GROUP) {
        alert('当前的聊天类型为群聊天，不能进行拉取好友历史消息操作');
        return;
    }
    webim.getC2CHistoryMsgs(
            options,
            function (resp) {
                var complete = resp.Complete; // 是否还有历史消息可以拉取，1-表示没有，0-表示有

                if (resp.MsgList.length === 0 && complete) {
                    alert('没有历史消息')
                    webim.Log.error("没有历史消息了:data=" + JSON.stringify(options));
                    return;
                }
                getPrePageC2CHistroyMsgInfoMap[selToID] = { // 保留服务器返回的最近消息时间和消息Key,用于下次向前拉取历史消息
                    'LastMsgTime': resp.LastMsgTime,
                    'MsgKey': resp.MsgKey
                };
                // lastMsgTime = resp.LastMsgTime
                if (cbOk) {
                  cbOk(resp.MsgList)
                }
            },
            cbError
    )
}
