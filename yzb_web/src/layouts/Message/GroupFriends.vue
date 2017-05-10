<template>
    <div class="group-friends-main">
        <!-- 群列表和好友列表 -->
        <div class="group-friends-left">
            <ul v-if="recentContactList" v-for="Key in Object.keys(recentContactList)" @click="onName(recentContactList[Key], Key)" :class="{on: presentFriendGrounp[recentContactList[Key].SessionId]}">
                <li><img :src='getUrl(recentContactList[Key].SessionImage)'></li>
                <li>
                    <p>
                        {{recentContactList[Key].SessionNick}}
                    </p>
                    <p>
                        {{recentContactList[Key].MsgShow}}
                    </p>
  <!--                   <p>{{recentContactList[Key].sign}}</p> -->
                </li>
                <i v-if="recentContactList[Key].sign"></i>
<!--                 <li>
                    <img :src="staffDefaultImg">
                </li> -->
            </ul>
        </div>
        <!-- 聊天框 -->
<!--         <div class="group-friends-right">
            <chat-frame :newImData='newImData'></chat-frame>
        </div> -->
    </div>
</template>
<script>
// import {webimLogin} from './ceshi.js'
import Vue from 'vue'
import {mapActions, mapGetters} from 'vuex'
import chatFrame from './ChatFrame' // 聊天框
import {webimLogin} from './../../Api/IM/login/login'
export default {
    components: {
        chatFrame
    },
    data () {
        return {
            recentContactList: [], // 最近联系人提取全局的最后结果
            initRecentContactList: [], // 最近联系人
            newImData: [], // 切换好友和群的当前数据
            staffDefaultImg: './../../../static/img/staffDefaultImg.png', // 默认好友图片
            presentFriendGrounp: [] // 选中样式
        }
    },
    created () {
        this.initFrient();
       // this.IMinitInfoMapByMyFriends(this.IMinitInfoMapByMyGroups(this.IMinitRecentContactList()));
    },
    computed: {
        ...mapGetters([
            "imOnMsgNotify" // 监听新消息
        ])
    },
    watch: {
        'imOnMsgNotify': function () { // 如果有新消息变化
            console.log("如果有新消息变化");
            console.log(this.recentContactList);
            for (let i in this.recentContactList) { // 给最近联系人添加未读标记(c2c)
                if (this.newImData.SessionType === "C2C") {
                    if (this.recentContactList[i].SessionId === this.imOnMsgNotify[0].fromAccount) { // 定位发来消息的人
                        if (this.recentContactList[i].SessionId !== this.newImData.SessionId) { // c2c(不是当前会话)
                            Vue.set(this.recentContactList[i], "sign", true);
                        }
                    }
                } else {
                    if (this.recentContactList[i].SessionId === this.imOnMsgNotify[0].sess._impl.id) { // 定位群
                        if (this.recentContactList[i].SessionId !== this.newImData.SessionId) { // 群（不是当前会话）
                            console.log("id来了");
                            console.log(this.newImData);
                            console.log(this.imOnMsgNotify[0].sess._impl.id);
                            Vue.set(this.recentContactList[i], "sign", true);
                        }
                    }
                }
                // if (this.recentContactList[i].SessionId === this.imOnMsgNotify[0].fromAccount) { // 定位发来消息的人
                //     if (this.recentContactList[i].SessionId !== this.newImData.SessionId) { // c2c(不是当前会话)
                //         Vue.set(this.recentContactList[i], "sign", true);
                //     }
                // } else if (this.recentContactList[i].SessionId === this.imOnMsgNotify[0].sess._impl.id) { // 定位群
                //     if (this.recentContactList[i].SessionId !== this.newImData.SessionId) { // 群（不是当前会话）
                //         console.log("id来了");
                //         console.log(this.newImData);
                //         console.log(this.imOnMsgNotify[0].sess._impl.id);
                //         Vue.set(this.recentContactList[i], "sign", true);
                //     }
                // }
                // console.log("检测输出值");
                // console.log(i);
            }
        }
    },
    methods: {
        ...mapActions([
            'IMonMsgNotify',
            'IMCutFriendGroup'
        ]),
        // haha () { // 测试代码
        //     console.log(selSess);
        // },
        signFun (parm) {
            // console.log("抗");
            // console.log(parm);
            if (parm) {
                return parm
            } else {
                return false;
            }
        },
        getUrl (parm) { // 提取图片
            if (!parm) {
                return this.staffDefaultImg;
            }
            return parm;
        },
        onName (parm, Key) { // 当前选中的好友或群的信息
            // console.log("xiaohua设置之前会话的已读消息标记")
            selType = parm.SessionType; // 当前聊天类型
            selToID = parm.SessionId; // 当前选中聊天id（当聊天类型为私聊时，该值为好友帐号，否则为群号）
        // webim.setAutoRead(selSess, true, true);
            this.newImData = parm;
            this.IMCutFriendGroup(parm); // vuex存一份
            this.recentContactList[Key].sign = false;
            this.presentFriendGrounp = []; // 先还原
            this.presentFriendGrounp[parm.SessionId] = true; // 选中样式
            // Vue.set(this.recentContactList[Key], sign, false);
        },
        initFrient () { // 初始化一下最近联系人
            webimLogin(this.IMonMsgNotify, this.initIM)
        },
        initIM () { // 提取一下最近联系人
            this.IMinitInfoMapByMyFriends(this.IMinitInfoMapByMyGroups(this.IMinitRecentContactList))
            // console.log("IM链接成功后的动作")
        },
        // 将我的好友资料（昵称和头像）保存在infoMap
        IMinitInfoMapByMyFriends (cbOK) {
            var options = {
                'From_Account': loginInfo.identifier,
                'TimeStamp': 0,
                'StartIndex': 0,
                'GetCount': totalCount,
                'LastStandardSequence': 0,
                "TagList": [
                    "Tag_Profile_IM_Nick",
                    "Tag_Profile_IM_Image"
                ]
            };

            webim.getAllFriend(
                options,
                function (resp) {
                    if (resp.FriendNum > 0) {

                        var friends = resp.InfoItem;
                        if (!friends || friends.length === 0) {
                            if (cbOK) {
                                cbOK();
                            }
                            return;
                        }
                        var count = friends.length;

                        for (var i = 0; i < count; i++) {
                            var friend = friends[i];
                            var friend_account = friend.Info_Account;
                            var friend_name = ''; // 好友名字
                            var friend_image = ''; // 好友头像
                            for (var j in friend.SnsProfileItem) {
                                switch (friend.SnsProfileItem[j].Tag) {
                                    case 'Tag_Profile_IM_Nick':
                                        friend_name = friend.SnsProfileItem[j].Value;
                                        break;
                                    case 'Tag_Profile_IM_Image':
                                        friend_image = friend.SnsProfileItem[j].Value;
                                        break;
                                }
                            }
                            var key = webim.SESSION_TYPE.C2C + "_" + friend_account;
                            infoMap[key] = {
                                'name': friend_name,
                                'image': friend_image
                            };
                            // console.log("将我的好友资料（昵称和头像）保存在infoMap");
                            // console.log(infoMap);
                        }
                        if (cbOK) {
                            cbOK();
                        }
                    }
                },
                function (err) {
                    alert(err.ErrorInfo);
                }
            );
        },
        // 将我的群组资料（群名称和群头像）保存在infoMap
        IMinitInfoMapByMyGroups (cbOK) {
            var options = {
                'Member_Account': loginInfo.identifier,
                'Limit': totalCount,
                'Offset': 0,
                'GroupBaseInfoFilter': [
                    'Name',
                    'FaceUrl'
                ]
            };
            webim.getJoinedGroupListHigh(
                options,
                function (resp) {
                    if (resp.GroupIdList && resp.GroupIdList.length) {
                        for (var i = 0; i < resp.GroupIdList.length; i++) {
                            var group_name = resp.GroupIdList[i].Name;
                            var group_image = resp.GroupIdList[i].FaceUrl;
                            var key = webim.SESSION_TYPE.GROUP + "_" + resp.GroupIdList[i].GroupId;
                            infoMap[key] = {
                                'name': group_name,
                                'image': group_image
                            }
                            // console.log("将我的群组资料（群名称和群头像）保存在infoMap");
                            // console.log(infoMap);
                        }
                    }
                    if (cbOK) {
                        cbOK();
                    }
                },
                function (err) {
                    alert(err.ErrorInfo);
                }
            );
        },
        // 初始化聊天界面左侧最近会话列表
        IMinitRecentContactList (cbOK, cbErr) {
            var _self = this;
            var options = {
                'Count': reqRecentSessCount // 要拉取的最近会话条数
            };
            webim.getRecentContactList(
                options,
                function (resp) {
                    var tempSess; // 临时会话变量
                    var firstSessType; // 保存第一个会话类型
                    var firstSessId; // 保存第一个会话id

                    // 清空聊天对象列表
                    // var sessList = document.getElementsByClassName("sesslist")[0];
                    // sessList.innerHTML = "";
                    if (resp.SessionItem && resp.SessionItem.length > 0) { // 如果存在最近会话记录
                        var sessType = '';
                        var typeZh = '';
                        var sessionId = '';
                        var sessionNick = '';
                        var sessionImage = '';
                        var senderId = '';
                        var senderNick = '';
                        for (var i in resp.SessionItem) {
                            var item = resp.SessionItem[i];
                            var type = item.Type; // 接口返回的会话类型
                            console.log(typeZh); // 解决一个eslime的一个抗
                            if (type === webim.RECENT_CONTACT_TYPE.C2C) { // 私聊
                                typeZh = '私聊';
                                sessType = webim.SESSION_TYPE.C2C; // 设置会话类型
                                sessionId = item.To_Account; // 会话id，私聊时为好友ID或者系统账号（值为@TIM#SYSTEM，业务可以自己决定是否需要展示），注意：从To_Account获取
                                if (sessionId === '@TIM#SYSTEM') { // 先过滤系统消息，，
                                    webim.Log.warn('过滤好友系统消息,sessionId=' + sessionId);
                                    continue;
                                }
                                var key = sessType + "_" + sessionId;
                                var c2cInfo = infoMap[key];
                                if (c2cInfo && c2cInfo.name) { // 从infoMap获取c2c昵称
                                    sessionNick = c2cInfo.name; // 会话昵称，私聊时为好友昵称，接口暂不支持返回，需要业务自己获取（前提是用户设置过自己的昵称，通过拉取好友资料接口（支持批量拉取）得到）
                                } else { // 没有找到或者没有设置过
                                    sessionNick = sessionId; // 会话昵称，如果昵称为空，默认将其设成会话id
                                }
                                if (c2cInfo && c2cInfo.image) { // 从infoMap获取c2c头像
                                    sessionImage = c2cInfo.image; // 会话头像，私聊时为好友头像，接口暂不支持返回，需要业务自己获取（前提是用户设置过自己的昵称，通过拉取好友资料接口（支持批量拉取）得到）
                                } else { // 没有找到或者没有设置过
                                    sessionImage = friendHeadUrl; // 会话头像，如果为空，默认将其设置demo自带的头像
                                }
                                // 私聊时，这些字段用不到，直接设置为空
                                senderId = '';
                                senderNick = '';
                            } else if (type === webim.RECENT_CONTACT_TYPE.GROUP) { // 群聊
                                typeZh = '群聊';
                                sessType = webim.SESSION_TYPE.GROUP; // 设置会话类型
                                sessionId = item.ToAccount; // 会话id，群聊时为群ID，注意：从ToAccount获取
                                sessionNick = item.GroupNick; // 会话昵称，群聊时，为群名称，接口一定会返回

                                if (item.GroupImage) { // 优先考虑接口返回的群头像
                                    sessionImage = item.GroupImage; // 会话头像，群聊时，群头像，如果业务设置过群头像（设置群头像请参考wiki文档-设置群资料接口），接口会返回
                                } else { // 接口没有返回或者没有设置过群头像，再从infoMap获取群头像
                                    key = sessType + "_" + sessionId;
                                    var groupInfo = infoMap[key];
                                    if (groupInfo && groupInfo.image) { // sessionImage = groupInfo.image
                                    } else { // 不存在或者没有设置过，则使用默认头像
                                        sessionImage = groupHeadUrl; // 会话头像，如果没有设置过群头像，默认将其设置demo自带的头像
                                    }
                                }
                                senderId = item.MsgGroupFrom_Account; // 群消息的发送者id

                                if (!senderId) { // 发送者id为空
                                    webim.Log.warn('群消息发送者id为空,senderId=' + senderId + ",groupid=" + sessionId);
                                    continue;
                                }
                                if (senderId === '@TIM#SYSTEM') { // 先过滤群系统消息，因为接口暂时区分不了是进群还是退群等提示消息，
                                    webim.Log.warn('过滤群系统消息,senderId=' + senderId + ",groupid=" + sessionId);
                                    continue;
                                }

                                senderNick = item.MsgGroupFromCardName; // 优先考虑群成员名片
                                if (!senderNick) { // 如果没有设置群成员名片
                                    senderNick = item.MsgGroupFromNickName; // 再考虑接口是否返回了群成员昵称
                                    if (!senderNick && !sessionNick) { // 如果接口没有返回昵称或者没有设置群昵称，从infoMap获取昵称
                                        key = webim.SESSION_TYPE.C2C + "_" + senderId;
                                        c2cInfo = infoMap[key];
                                        if (c2cInfo && c2cInfo.name) {
                                            senderNick = c2cInfo.name; // 发送者群昵称
                                        } else {
                                            sessionNick = senderId; // 如果昵称为空，默认将其设成发送者id
                                        }
                                    }
                                }

                            } else {
                                typeZh = '未知类型';
                                sessionId = item.ToAccount; //
                            }
                            if (!sessionId) { // 会话id为空
                                webim.Log.warn('会话id为空,sessionId=' + sessionId);
                                continue;
                            }

                            if (sessionId === '@TLS#NOT_FOUND') { // 会话id不存在，可能是已经被删除了
                                webim.Log.warn('会话id不存在,sessionId=' + sessionId);
                                continue;
                            }

                            if (sessionNick.length > 12) { // 帐号或昵称过长，截取一部分，出于demo需要，业务可以自己决定
                                sessionNick = sessionNick.substr(0, 12) + "...";
                            }
                            tempSess = recentSessMap[sessType + "_" + sessionId];
                            // console.log("one保存最近会话列表");
                            // console.log(recentSessMap);
                            if (!tempSess) { // 先判断是否存在（用于去重），不存在增加一个

                                if (!firstSessId) {
                                    firstSessType = sessType; // 记录第一个会话类型
                                    firstSessId = sessionId; // 记录第一个会话id
                                }
                                recentSessMap[sessType + "_" + sessionId] = {
                                    SessionType: sessType, // 会话类型
                                    SessionId: sessionId, // 会话对象id，好友id或者群id
                                    SessionNick: sessionNick, // 会话昵称，好友昵称或群名称
                                    SessionImage: sessionImage, // 会话头像，好友头像或者群头像
                                    C2cAccount: senderId, // 发送者id，群聊时，才有用
                                    C2cNick: senderNick, // 发送者昵称，群聊时，才有用
                                    UnreadMsgCount: item.UnreadMsgCount, // 未读消息数,私聊时需要通过webim.syncMsgs(initUnreadMsgCount)获取,参考后面的demo，群聊时不需要。
                                    MsgSeq: item.MsgSeq, // 消息seq
                                    MsgRandom: item.MsgRandom, // 消息随机数
                                    MsgTimeStamp: webim.Tool.formatTimeStamp(item.MsgTimeStamp), // 消息时间戳
                                    MsgGroupReadedSeq: item.MsgGroupReadedSeq || 0,
                                    MsgShow: item.MsgShow // 消息内容,文本消息为原内容，表情消息为[表情],其他类型消息以此类推
                                };

                                // 在左侧最近会话列表框中增加一个会话div
                                // addSess(sessType, webim.Tool.formatText2Html(sessionId), webim.Tool.formatText2Html(sessionNick), sessionImage, item.UnreadMsgCount, 'sesslist');
                            }

                        }
                        // 清空聊天界面
                        // document.getElementsByClassName("msgflow")[0].innerHTML = "";

                        tempSess = recentSessMap[firstSessType + "_" + firstSessId]; // 选中第一个会话
                        // console.log("小华测报错");
                        // console.log(tempSess);
                        // console.log("保存最近会话列表");
                        // console.log(recentSessMap);
                        // sessionStorage.setItem("保存最近会话列表", JSON.stringify(recentSessMap));
                        _self.recentContactList = recentSessMap;
                        console.log(tempSess);
                        selType = tempSess.SessionType; // 初始化当前聊天类型

                        selToID = tempSess.SessionId; // 初始化当前聊天对象id

                        selSess = webim.MsgStore.sessByTypeId(selType, selToID); // 初始化当前会话对象
                        // console.log("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
                        // console.log(tempSess.SessionType);
                        // console.log(tempSess.SessionId);
                        // console.log(selSess);
                        // setSelSessStyleOn(selToID); // 设置当前聊天对象选中样式

                        webim.syncMsgs(_self.initUnreadMsgCount); // 初始化最近会话的消息未读数

        // 获取所有聊天会话
        // var sessMap = webim.MsgStore.sessMap();
        // console.log("获取所有聊天会话");
        // console.log(sessMap);
        // for (let i in sessMap) {
        //     var sess = sessMap[i];
        //     console.log(sess.type());
        //       console.log(sess.id());
        //       console.log(sess.name());
        //       console.log(sess.unread());
        //     if (selToID !== sess.id()) { // 更新其他聊天对象的未读消息数
        //       console.log(sess.type());
        //       console.log(sess.id());
        //       console.log(sess.name());
        //       console.log(sess.unread());
        //       // stopPolling = true;
        //     }
        // }
                        if (cbOK) {
                            cbOK();
                        } // 回调

                    }

                },
                cbErr
            );
        },
        initUnreadMsgCount () {
            // var sess;
            var sessMap = webim.MsgStore.sessMap();
            console.log("小华要测试");
            console.log(sessMap);
            // for (var i in sessMap) {
            //     sess = sessMap[i];
            //     if (selToID && selToID != sess.id()) {//更新其他聊天对象的未读消息数
            //         updateSessDiv(sess.type(), sess.id(), sess.name(), sess.unread());
            //     }
            // }
        }
    }
}
</script>
<style lang="scss" scoped>
.group-friends-main{
    width: 100%;
    height: 100%;
    background-color: #ccc;
    .group-friends-left{
        float: left;
        width: 254px;
        height: 100%;
        ul {
            height: 60px;
            // padding: 5px;
            font-size: 0;
            // background-color: #666666;
            // margin-bottom: 20px;
            position: relative;
            border-bottom: 1px solid #f0f0f0;
            i{ // 消息通知
                display: inline-block;
                position: absolute;
                right: 20px;
                top: 25px;
                width: 10px;
                height: 10px;
                background-color: red;
                border-radius: 50%;
            }
            &:hover{
                background-color: #e6f8f6;
            }
            li{
                display: inline-block;
                vertical-align: top;
                font-size: 16px;
            }
            li:nth-child(1) {
                width: 40px;
                height: 40px;
                overflow: hidden;
                margin: 10px;
                border-radius: 50%;
                border: 1px solid #fff;
                img{
                    width: 40px;
                    height: 40px;
                    border-radius: 50%;
                }
            }
            li:nth-child(2) {
                width: 100px;
                margin-top: 10px;
                height: 40px;
                p:nth-child(1){
                    width: 90px;
                    white-space:nowrap;
                    text-overflow : ellipsis;
                    font-size: 16px;
                    color: #333;
                }
                p:nth-child(2){
                    height: 14px;
                    width: 84px;
                    white-space:nowrap;
                    text-overflow : ellipsis;
                    line-height: 14px;
                    overflow: hidden;
                    font-size: 14px;
                    color: #666;
                }
            }
        }
    }
    .group-friends-right{
        float: left;
        border: 1px solid blue;
        width: 800px;
        height: 100%;
    }
}
.on{
    background-color: #e6f8f6;
}
</style>