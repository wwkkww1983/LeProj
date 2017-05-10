<template>
    <div class="chat-frame-main">
        <div class="chat-frame-body">
            <h3 @click="xiaoHua">{{imCutFriendGroup.SessionNick}}</h3>
<!--             <div>{{imCutFriendGroup}}</div> -->
            <div class="message-frame" ref="scrollState">        
                <!-- <div class="message-wrap" v-for="item in messageList" v-if="messageList"> -->
                <template v-for="item in messageList" v-if="messageList">
                    <!-- 自己 -->
                    <div class="message-default my-right" v-if="item.isSend">
                        <ul>
                            <li><img :src="userInfo.avatarUrl" alt=""></li>
                            <li>
                                <!-- 文字 -->
                                <template v-if="item.elems[0].content.text">
                                    <pre class="text">
                                        <div>{{item.elems[0].content.text}}</div>
                                    </pre>
                                </template>
                                <!-- 表情 -->
                                <template v-if="item.elems[0].content.data">
                                    <pre class="text">
                                        <div><img :src="readIcon(item.elems[0].content.data)"></div>
                                    </pre>
                                </template>
                                <!-- 图片 -->
                                <template v-if="item.elems[0].content.ImageInfoArray">
                                    <pre class="text">
                                        <div><img class="message-img" :src="readImg(item.elems[0])"></div>
                                    </pre>
                                </template>
                                <i class="icon-triangle"></i>
                            </li>
                        </ul>
                    </div>
                    <!-- 好友 -->
                    <div class="message-default my-left" v-else>
                        <ul>
                            <li><img :src="getFriendImg(imCutFriendGroup.SessionType, item)" alt=""></li>
                            <li>
                                <!-- 文字 -->
                                <template v-if="item.elems[0].content.text">
                                    <pre class="text">
                                        <div>{{item.elems[0].content.text}}</div>
                                    </pre>
                                </template>
                                <!-- 表情 -->
                                <template v-if="item.elems[0].content.data">
                                    <pre class="text">
                                        <div><img :src="readIcon(item.elems[0].content.data)"></div>
                                    </pre>
                                </template>
                                <!-- 图片 -->
                                <template v-if="item.elems[0].content.ImageInfoArray">
                                    <pre class="text">
                                        <div><img class="message-img" :src="readImg(item.elems[0].content.ImageInfoArray[2].url)"></div>
                                    </pre>
                                </template>
                                <i class="icon-triangle"></i>
                            </li>
                        </ul>
                    </div>
                </template>
                <!-- </div> -->
            </div>
        </div>
        <div class="chat-frame-footer">
            <div class="im-cf-operation">
                <div class="im-operation-img">       
                    <img @click="openIconList" src="../../assets/img/Chat/face.png">
                    <img @click="uploadingImg" src="../../assets/img/Chat/photo.png">
                </div>
                <div class="icon-list" v-show="iconTier">
                    <!-- 表情包 -->
                    <template v-for="item in emotionsList">
                        <img :src="item.src" @click="getIcon(item)">
                    </template>
                </div>
            </div>
            <div class="im-cf-content">
                <textarea class="im-msbc-box" isreply="false" v-model="messageContext" @keyup.enter="onSendMsg"></textarea>
            </div>
        </div>
    </div>
</template>
<script>
import {mapGetters} from 'vuex'
export default {
    data () {
        return {
            messageList: [], // 历史消息
            messageContext: '', // 发送的消息内容
            groupList: [], // 群成员
            iconTier: false // 表情框
        }
    },
    computed: {
        ...mapGetters([
            "userInfo", // 用户的基本信息
            "imOnMsgNotify", // 监听新消息
            "imCutFriendGroup" // 切换好友和群的当前数据
        ]),
        emotionsList: function () {
            let emotionsList = [];
            for (let index in webim.Emotions) {
                let emotions = {
                    "id": webim.Emotions[index][0],
                    "src": webim.Emotions[index][1]
                }
                emotionsList.push(emotions);
            }
            return emotionsList;
        }
    },
    created () {
        selToID = this.imCutFriendGroup.SessionId; // 当前聊天会话对象id
        this.messageList = []; // 清空历史记录
        this.messageContext = ''; // 清空输入框的内容
        // this.getLastC2CHistoryMsgs();
        this.onSelSess(this.imCutFriendGroup.SessionType, this.imCutFriendGroup.SessionId);
    },
    watch: {
        'imCutFriendGroup': function () { // 监听切换
            selToID = this.imCutFriendGroup.SessionId; // 当前聊天会话对象id
            this.messageList = []; // 清空历史记录
            this.messageContext = ''; // 清空输入框的内容
            // this.getLastC2CHistoryMsgs();
            this.onSelSess(this.imCutFriendGroup.SessionType, this.imCutFriendGroup.SessionId);
        },
        'messageList': function () { // 检测历史消息
            var _self = this;
            setTimeout(function () {
                _self.$refs.scrollState.scrollTop = _self.$refs.scrollState.scrollHeight;
                // console.log(_self.$refs.scrollState.scrollTop);
                // console.log(_self.$refs.scrollState.scrollHeight);
            }, 300);
        },
        'imOnMsgNotify': function () { // 有新消息到达
            console.log("来啦");
            console.log(this.imOnMsgNotify[0].fromAccount);
            console.log(this.imOnMsgNotify);
            this.msgFilter(this.imOnMsgNotify);
        }
    },
    methods: {
        // 切换好友或群组聊天对象
        onSelSess(sess_type, to_id) {
            if (selToID != null) {

                // 将之前选中用户的样式置为未选中样式
                // setSelSessStyleOff(selToID);

                // 设置之前会话的已读消息标记
                // webim.setAutoRead(selSess, false, false);

                // 保存当前的消息输入框内容到草稿
                // 获取消息内容
                // var msgtosend = document.getElementsByClassName("msgedit")[0].value;
                // var msgLen = webim.Tool.getStrBytes(msgtosend);
                // if (msgLen > 0) {
                //     webim.Tool.setCookie("tmpmsg_" + selToID, msgtosend, 3600);
                // }

                // 清空聊天界面
                // document.getElementsByClassName("msgflow")[0].innerHTML = "";

                selToID = to_id;
                // 设置当前选中用户的样式为选中样式
                // setSelSessStyleOn(to_id);

                // var tmgmsgtosend = webim.Tool.getCookie("tmpmsg_" + selToID);
                // if (tmgmsgtosend) {
                //     $("#send_msg_text").val(tmgmsgtosend);
                // } else {
                //     $("#send_msg_text").val('');
                // }

                // bindScrollHistoryEvent.reset();

                if (sess_type === webim.SESSION_TYPE.GROUP) { // 群聊
                    if (selType === webim.SESSION_TYPE.C2C) {
                        selType = webim.SESSION_TYPE.GROUP;
                    }
                    selSess = null;
                    webim.MsgStore.delSessByTypeId(selType, selToID);
                    // alert("群聊");
                    this.getGroupMemberInfo(this.imCutFriendGroup.SessionId, this.configGroupImg);
                    this.getLastGroupHistoryMsgs();
                    // getLastGroupHistoryMsgs(function(msgList) {
                    //     getHistoryMsgCallback(msgList);
                    //     bindScrollHistoryEvent.init();
                    // }, function(err) {
                    //     alert(err.ErrorInfo);
                    // });
                } else { // C2C
                    if (selType === webim.SESSION_TYPE.GROUP) {
                        selType = webim.SESSION_TYPE.C2C;
                    }
                    // alert("c2c");
                    this.getLastC2CHistoryMsgs();
                    // 如果是管理员账号，则为全员推送，没有历史消息
                    // if (selToID == AdminAcount) {
                    //     var sess = webim.MsgStore.sessByTypeId(selType, selToID);
                    //     if (sess && sess.msgs() && sess.msgs().length > 0) {
                    //         getHistoryMsgCallback(sess.msgs());
                    //     } else {
                    //         getLastC2CHistoryMsgs(function(msgList) {
                    //             getHistoryMsgCallback(msgList);
                    //             bindScrollHistoryEvent.init();
                    //         }, function(err) {
                    //             alert(err.ErrorInfo);
                    //         });
                    //     }
                    //     return;
                    // }

                    // 拉取漫游消息
                    // getLastC2CHistoryMsgs(function(msgList) {
                    //     getHistoryMsgCallback(msgList);
                    //     // 绑定滚动操作
                    //     bindScrollHistoryEvent.init();
                    // }, function(err) {
                    //     alert(err.ErrorInfo);
                    // });
                }
            }
        },
        // 获取最新的c2c历史消息,用于切换好友聊天，重新拉取好友的聊天消息
        getLastC2CHistoryMsgs (cbOk, cbError) {
            var _self = this;
            var lastMsgTime = 0; // 第一次拉取好友历史消息时，必须传0
            var msgKey = '';
            var options = {
                'Peer_Account': this.imCutFriendGroup.SessionId, // 好友id
                'MaxCnt': reqMsgCount, // 拉取消息条数
                'LastMsgTime': lastMsgTime, // 最近的消息时间，即从这个时间点向前拉取历史消息
                'MsgKey': msgKey
            };
            selSess = null;
            webim.MsgStore.delSessByTypeId(selType, selToID);

            webim.getC2CHistoryMsgs(
                options,
                function (resp) {
                    // 消息已读上报，并将当前会话的消息设置成自动已读
                    // selSess = webim.MsgStore.sessByTypeId(selType, selToID); // 初始化当前会话对象
                    console.log("消息已读上报");
                    console.log(selSess);
                    // webim.setAutoRead(selSess, true, true);
                    // console.log("测试代码");
                    // console.log(selSess);
                    // console.log("获取历史消息");
                    // console.log(resp);
                    // console.log(resp.MsgList[0].elems[0].content.text)
                    // console.log(resp.MsgList[0])
                    for (let i = 0; i < resp.MsgList.length; i++) {
                        _self.messageList.push(resp.MsgList[i])
                    }
                    // _self.messageList = resp.MsgCount;
                    // 是否还有历史消息可以拉取，1-表示没有，0-表示有
                    // var complete = resp.Complete;
                    if (resp.MsgList.length === 0) {
                        alert("没有历史消息了");
                        webim.Log.warn("没有历史消息了:data=" + JSON.stringify(options));
                        return;
                    }
                    getPrePageC2CHistroyMsgInfoMap[selToID] = { // 保留服务器返回的最近消息时间和消息Key,用于下次向前拉取历史消息
                        'LastMsgTime': resp.LastMsgTime,
                        'MsgKey': resp.MsgKey
                    };
                    // 清空聊天界面
                    // document.getElementsByClassName("msgflow")[0].innerHTML = "";
                    // if (cbOk) {
                    //     cbOk(resp.MsgList);
                    // }
                },
                cbError
            );
        },
        // 获取最新的群历史消息,用于切换群组聊天时，重新拉取群组的聊天消息
        getLastGroupHistoryMsgs (cbOk, cbError) {
            var _self = this;
            if (selType === webim.SESSION_TYPE.C2C) {
                alert('当前的聊天类型为好友聊天，不能进行拉取群历史消息操作');
                return;
            }
            // this.getGroupInfo(this.imCutFriendGroup.SessionId, function (resp) {
            //     console.log(resp);
            // });
            this.getGroupInfo(this.imCutFriendGroup.SessionId, function (resp) {
                // 拉取最新的群历史消息
                var options = {
                    'GroupId': _self.imCutFriendGroup.SessionId, // 群id
                    'ReqMsgSeq': resp.GroupInfo[0].NextMsgSeq - 1,
                    'ReqMsgNumber': reqMsgCount
                };
                // if (options.ReqMsgSeq === null || options.ReqMsgSeq === undefined || options.ReqMsgSeq <= 0) {
                //     webim.Log.warn("该群还没有历史消息:options=" + JSON.stringify(options));
                //     return;
                // }
                selSess = null;
                webim.MsgStore.delSessByTypeId(selType, selToID);
                recentSessMap[webim.SESSION_TYPE.GROUP + "_" + selToID].MsgGroupReadedSeq = resp.GroupInfo[0].MsgSeq;
                webim.syncGroupMsgs(
                    options,
                    function (msgList) {
                        if (msgList.length === 0) {
                            webim.Log.warn("该群没有历史消息了:options=" + JSON.stringify(options));
                            return;
                        }
                        console.log("获取群聊天消息");
                        console.log(msgList);
                        for (let i = 0; i < msgList.length; i++) {
                            _self.messageList.push(msgList[i])
                        }
                        // var msgSeq = msgList[0].seq - 1;
                        // getPrePageGroupHistroyMsgInfoMap[selToID] = {
                        //     "ReqMsgSeq":  msgSeq
                        // };
                        // 清空聊天界面
                        // document.getElementsByClassName("msgflow")[0].innerHTML = "";
                        if (cbOk) {
                            cbOk(msgList);
                        }
                    },
                    function (err) {
                        alert(err.ErrorInfo);
                    }
                );
            });
        },
        // 读取群组基本资料-高级接口
        getGroupInfo (group_id, cbOK, cbErr) {
            var options = {
                'GroupIdList': [
                    group_id
                ],
                'GroupBaseInfoFilter': [
                    'Type',
                    'Name',
                    'Introduction',
                    'Notification',
                    'FaceUrl',
                    'CreateTime',
                    'Owner_Account',
                    'LastInfoTime',
                    'LastMsgTime',
                    'NextMsgSeq',
                    'MemberNum',
                    'MaxMemberNum',
                    'ApplyJoinOption'
                ],
                'MemberInfoFilter': [
                    'Account',
                    'Role',
                    'JoinTime',
                    'LastSendMsgTime',
                    'ShutUpUntil'
                ]
            };
            webim.getGroupInfo(
                options,
                function (resp) {
                    // console.log("读取群组基本资料-高级接口");
                    // console.log(resp);
                    if (cbOK) {
                        cbOK(resp);
                    }
                },
                function (err) {
                    alert(err.ErrorInfo);
                }
            );
        },
        // 发送消息(文本或者表情)
        onSendMsg () {
            if (!this.imCutFriendGroup.SessionType) {
                alert("你还没有选中好友或者群组，暂不能聊天");
                $("#send_msg_text").val('');
                return;
            }
            // 发消息处理
            this.handleMsgSend(this.messageContext);
        },
        handleMsgSend (msgContent) { // 发消息处理
            var _self = this;
            if (!selSess) {
                var selSess = new webim.Session(selType, selToID, selToID, friendHeadUrl, Math.round(new Date().getTime() / 1000));
            }
            var isSend = true; // 是否为自己发送
            var seq = -1; // 消息序列，-1表示sdk自动生成，用于去重
            var random = Math.round(Math.random() * 4294967296); // 消息随机数，用于去重
            var msgTime = Math.round(new Date().getTime() / 1000); // 消息时间戳
            var subType; // 消息子类型
            if (selType === webim.SESSION_TYPE.C2C) {
                subType = webim.C2C_MSG_SUB_TYPE.COMMON;
            } else {
                 // webim.GROUP_MSG_SUB_TYPE.COMMON-普通消息,
                 // webim.GROUP_MSG_SUB_TYPE.LOVEMSG-点赞消息，优先级最低
                 // webim.GROUP_MSG_SUB_TYPE.TIP-提示消息(不支持发送，用于区分群消息子类型)，
                 // webim.GROUP_MSG_SUB_TYPE.REDPACKET-红包消息，优先级最高
                subType = webim.GROUP_MSG_SUB_TYPE.COMMON;
            }
            console.log(loginInfo);
            var msg = new webim.Msg(selSess, isSend, seq, random, msgTime, loginInfo.identifier, subType, loginInfo.identifierNick);

            var text_obj, face_obj, tmsg, emotionIndex, emotion, restMsgIndex;
             // 解析文本和表情
            var expr = /\[[^[\]]{1,3}\]/mg;
            var emotions = msgContent.match(expr);
            if (!emotions || emotions.length < 1) {
                text_obj = new webim.Msg.Elem.Text(msgContent);
                msg.addText(text_obj);
            } else {
                for (var i = 0; i < emotions.length; i++) {
                    tmsg = msgContent.substring(0, msgContent.indexOf(emotions[i]));
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
                    restMsgIndex = msgContent.indexOf(emotions[i]) + emotions[i].length;
                    msgContent = msgContent.substring(restMsgIndex);
                }
                if (msgContent) {
                    text_obj = new webim.Msg.Elem.Text(msgContent);
                    msg.addText(text_obj);
                }
            }
            msg.sending = 1;
            msg.originContent = msgContent;
            console.log("提取的历史消息");
            console.log(msg);
            // var msgData = { // 匹配历史消息的数组结构
            //     elems: [
            //         {
            //             content: {
            //                 text: msg
            //             }
            //         }
            //     ]
            // }
            // console.log("匹配结构");
            // console.log(msgData);
            _self.messageList.push(msg);
            _self.messageContext = ''; // 清空输入框的内容
            // addMsg(msg); // 页面新增一条消息
            // $("#send_msg_text").val('');
            // turnoffFaces_box(); // 关闭表情框
            webim.sendMsg(msg, function (resp) {
                console.log("消息发送成功")
                console.log(resp)
            }, function (err) {
                alert(err.ErrorInfo);
                console.log("消息发送失败");
                // $("#send_msg_text").val('');
            })
            // webim.sendMsg(msg, function (resp) {
            //     // 发送成功，把sending清理
            //     $("#id_"+msg.random).find(".spinner").remove();
            //     webim.Tool.setCookie("tmpmsg_" + selToID, '', 0);
            // }, function (err) {
            //     // alert(err.ErrorInfo);
            //     // 提示重发
            //     showReSend(msg);
            // });
            // webim.sendMsg(msg, function (resp) {
            //      // 发送成功，把sending清理
            //     // $("#id_" + msg.random).find(".spinner").remove();
            //     // webim.Tool.setCookie("tmpmsg_" + selToID, '', 0);
            // }, function (err) {
            //     console.log("提示重发");
            //     // alert(err.ErrorInfo);
            //     // 提示重发
            //     // showReSend(msg);
            // });
        },
        msgFilter (parm) {
            if (parm[0].fromAccount === this.imCutFriendGroup.SessionId) { // 判断是否为当前聊天
                this.messageList.push(parm[0]);
            } else if (parm[0].sess._impl.id === this.imCutFriendGroup.SessionId) { // 群聊时
                this.messageList.push(parm[0]);
            }
        },
        // 搜索用户
        setProfilePortraitClick (parm) {
            console.log("搜索用户");
            var _self = this;
            var tag_list = [
                // "Tag_Profile_IM_Nick",
                // "Tag_Profile_IM_Gender",
                // "Tag_Profile_IM_AllowType",
                "Tag_Profile_IM_Image"
            ];
            var options = {
                'To_Account': [parm],
                'TagList': tag_list
            };
            webim.getProfilePortrait(
                options,
                function (resp) {
                    console.log("搜索用户请求结果");
                    console.log(resp);
                    console.log(_self);
                    for (let i = 0; i < _self.groupList.length; i++) {
                        if (_self.groupList[i].Member_Account === parm) {
                            console.log("图片匹配了");
                            console.log(resp.UserProfileItem[0].ProfileItem[0].Value);
                            _self.groupList[i].img = resp.UserProfileItem[0].ProfileItem[0].Value;
                            console.log(_self.groupList);
                        }
                    }
                    // return resp;
                },
                function (err) {
                    alert(err.ErrorInfo);
                }
            );
        },
        configGroupImg (parm) {
            for (let i of parm) {
                console.log("循环导出群成员");
                console.log(i.Member_Account);
                this.setProfilePortraitClick(i.Member_Account);
            }
        },
        // 读取群组成员
        getGroupMemberInfo (group_id, cbok) {
            // initGetGroupMemberTable([]);
            var _self = this;
            var options = {
                'GroupId': group_id,
                'Offset': 0, // 必须从0开始
                'Limit': totalCount,
                'MemberInfoFilter': [
                    'Account',
                    'Role',
                    'JoinTime',
                    'LastSendMsgTime',
                    'ShutUpUntil'
                ]
            };
            webim.getGroupMemberInfo(
                options,
                function (resp) {
                    if (resp.MemberNum <= 0) {
                        alert('该群组目前没有成员');
                        return;
                    }
                    console.log("读取群成员");
                    console.log(resp);
                    // var data = [];
                    for (var i in resp.MemberList) {
                        var account = resp.MemberList[i].Member_Account;
                        var role = webim.Tool.groupRoleEn2Ch(resp.MemberList[i].Role);
                        var join_time = webim.Tool.formatTimeStamp(
                        resp.MemberList[i].JoinTime);
                        var shut_up_until = webim.Tool.formatTimeStamp(
                        resp.MemberList[i].ShutUpUntil);
                        if (shut_up_until === 0) {
                            shut_up_until = '-';
                        }
                        _self.groupList.push({
                            GroupId: group_id,
                            Member_Account: account,
                            Role: role,
                            JoinTime: join_time,
                            ShutUpUntil: shut_up_until,
                            img: null // 搜索用户时加上去
                        });
                    }
                    console.log("配置群成员");
                    console.log(_self.groupList);
                    if (cbok) {
                        cbok(_self.groupList);
                    }
                    // if (cbOK) {
                    //     console.log("执行了");
                    //     cbOK(_self.groupList)
                    // }
                    // console.log(data);
                    // $('#get_group_member_table').bootstrapTable('load', data);
                    // $('#get_group_member_dialog').modal('show');
                },
                function (err) {
                    console.log("获取群成员失败");
                    console.log(err)
                    alert(err.ErrorInfo);
                }
            );
        },
        getFriendImg (Type, item) { // 提取好友图片
            if (Type === "C2C") {
                console.log("单聊");
                return this.imCutFriendGroup.SessionImage;
            } else {
                for (let i of this.groupList) {
                    if (i.Member_Account === item.fromAccount) {
                        return i.img;
                    }
                }
            }
        },
        openIconList () { // 打开表情包
            this.iconTier = true;
            // alert("打开表情包");
        },
        getIcon (parm) {
            this.iconTier = false;
            this.messageContext = this.messageContext + parm.id
            console.log(parm);
            // alert(parm);
        },
        readIcon (parm) { // 读取消息表情
            for (let i of this.emotionsList) {
                // console.log(i)
                if (i.id === parm) {
                    // console.log(i.src)
                    return i.src;
                }
            }
        },
        readImg (parm) { // 读取图片
           // console.log("读取图片");
           // console.log(parm);
            return parm;
        },
        getText (parm) {
            console.log("消息真假");
            console.log(parm);
            if (parm) {
                console.log("zhen")
                return true;
            } else {
                return false;
            }
        },
        uploadingImg () { // 上传图片
            alert("上传图片功能暂时不做");
        },
        xiaoHua () {
            console.log(this.$refs.XHceshi);
        }
    }
}
</script>
<style lang="scss" scoped>
.chat-frame-main{
    // border: 1px solid #000;
    height: 100%;
    width: 100%;
    display: flex;
    flex-direction: column;
    overflow: hidden;
    .chat-frame-body{
        // display: flex;
        // flex-direction: column;
        flex: 1;
        // background-color: red;
        display: flex;
        flex-direction: column;
        h3{
            font-weight: 500;
            color: #666;
            text-align: center;
            border-bottom: 1px solid #f0f0f0;
            font-size: 18px;
            height: 48px;
            line-height: 48px;
        }
        // height: 700px;
        // border: 1px solid red;
    }
    .chat-frame-footer{
        height: 194px;
        border-top: 1px solid #f0f0f0;
        .im-cf-operation{
            padding-top: 5px;
            height: 30px;
            position: relative;
            // background-color: blue;
            .im-operation-img{
                height: 20px;
                // margin-top: 5px;
                // border: 1px solid red;
                img{
                    width: 20px;
                    height: 20px;
                    margin-left: 20px;
                }
            }
            .icon-list{ // 表情框
                position: absolute;
                bottom: 30px;
                border-radius: 4px;
                left: 0;
                border: 1px solid red;
                width: 400px;
                height: 300px;
                padding-top: 26px;
                overflow: auto;
                background-color: #fff;
               // padding: 26px 14px;
                img{
                    width: 34px;
                    height: 34px;
                    // border: 1px solid red;
                    // padding: 10px;
                    margin-left: 14px;
                }
            }
            // background-color: blue;
        }
        .im-msbc-box{
            text-align: 10px;
            outline: none;
            width: 100%;
            height: 170px;
            display: block;
            border: 0;
            padding: 0 38px 0 14px;
            font-size: 13px;
            line-height: 20px;
            color: #3d464a;
            background-color: #f6f8f9;
        }
    }
}
// .message-wrap { // 消息框外层
    
// }
.message-frame{ // 消息框
    width: 100%;
    flex: 1;
    overflow: auto;
    // border: 1px solid yellow;
    .text{ // 消息字体、图片、表情
        padding: 8px 10px;
        // Letter-spacing: 1px; // 字体间距
        // background: #def4ff;
        border: 1px solid #f0f0f0;
        // margin-right: 10px;
        max-width: 500px;
        // overflow: hidden;
        word-wrap: break-word;
        white-space: pre-wrap;
        border-radius: 4px;
        font-size: 0;
        div{
            // max-width: 500px;
            font-size: 12px;
            padding-right: 8px;
            line-height: 24px;
        }
    }
    .message-default{ // 整体消息框的默认样式
        // position: relative;
        // height: 100px;
        // border: 1px solid red;
        ul{
            padding: 16px;
            font-size: 0;
            // margin-top: 15px;
            // position: absolute;
            li{
                display: inline-block;
                vertical-align: top;
            }
        }
        .message-img{
            width: 200px;
            height: 200px;
            border-radius: 4px;
        }
    }
    img{
        width: 50px;
        height: 50px;
        border-radius: 50%;
    }
    .my-left {
        // display: block;
        .text{ // 消息字体、图片、表情
            margin-left: 10px;
        }
        ul{
            overflow: hidden;
            li{
                float: left;
            }
            li:nth-child(2){
                position: relative;        
                .icon-triangle{
                    box-sizing: border-box;
                    position: absolute;
                    left: -10px;
                    top: 10px;
                    width: 20px;
                    height: 20px;
                    // border: 1px solid red;
                }
            }
        }
    }
    .my-right {
        // display: block;
        .text{ // 消息字体、图片、表情
            margin-right: 10px;
        }
        ul{
            overflow: hidden;
            // right: 16px;
            li{
                float: right;
            }
            li:nth-child(1){
                position: relative;        
                .icon-triangle{
                    box-sizing:border-box;
                    position: absolute;
                    right: -5px;
                    top: 10px;
                    width: 20px;
                    height: 20px;
                    // border: 1px solid red;
                }
            }
        }
    }
}

</style>