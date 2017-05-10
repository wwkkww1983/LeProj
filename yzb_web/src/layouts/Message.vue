<template>
    <div class="IM-panel">
        <div class="im-aside">
            <AddFriend></AddFriend>
<!--             <div class="currentContact">
                <Qnode nodeName="消息助手" nodeMessage="欢迎使用金掌门管理系统" @click.native = "targetId = 'sys'"></Qnode>
                <Qnode v-for = "item in recentContacts" :unread="item.unread" :tailMsg="item.msgs[item.msgs.length - 1]" :nodeName="item.name" :avatarUrl="item.avatarUrl || item.icon" @click.native = 'setTarget(item)'></Qnode>
            </div> -->
            <div>
                <Qnode nodeName="公告" nodeMessage="放假啦！！！！！！！" @click.native = "targetId = 'notice'"></Qnode>
            </div>
            <div>
                <groupFriends></groupFriends>
            </div>
        </div>
        <div class="msg-container">
<!--             <div v-if = "targetId === 'sys'" class="sys-frame">
              <h3>系统消息</h3>
              <ReqMess v-for="item in sysMess" :msg = "item"></ReqMess>
            </div> -->
            <template v-if="imCutFriendGroup">
                <chat-frame></chat-frame>
            </template>
            <template v-else>    
                <div v-if = "targetId === 'notice'" class="notice-frame">
                    <h2>公告</h2>
                    <div class="route-list">
                        <ul id="all-notice">
                            <li @click="toggle(index ,tab.view)" v-for="(tab, index) in tabs" :class="{active:active==index}">
                                {{tab.type}}       
                            </li>
                        </ul>       
                    </div>
                    <div class="notice-main">
                        <component :is="currentView"></component>        
                    </div>
                </div>
            </template>
        </div>
    </div>
</template>
<script>
import {mapGetters} from 'vuex'
import groupFriends from './Message/GroupFriends' // 最近联系人
import chatFrame from './Message/ChatFrame' // 聊天框
import AddFriend from './IM/AddFriend'
import Qnode from "../components/Q-node"
// import ReqMess from './IM/ReqMess'
import noRead from './Notice/noRead'
import hasRead from './Notice/hasRead'

export default {
    data() {
        return {
            "message": '',
            "targetId": 'selToID',
            "currentView": 'noRead',
            "active": 0,
            tabs: [{
                type: '未读公告',
                view: 'noRead'
            }, {
                type: '已读公告',
                view: 'hasRead'
            }]
        }
    },
    computed: {
        ...mapGetters([
            "friendList",
            "recentContacts",
            "sysMess",
            "imCutFriendGroup" // 切换好友和群的当前数据
        ])
    },
    components: {
        groupFriends,
        chatFrame,
        AddFriend,
        Qnode,
        noRead,
        hasRead
    },
    methods: {
        // setTarget: function(obj) {
        //     obj.unread = false
        //     selToID = obj.id
        //     if (obj.type === 'C2C') {
        //         friendHeadUrl = obj.avatarUrl
        //         selType = webim.SESSION_TYPE.C2C
        //     } else if (obj.type === 'GROUP') {
        //         selType = webim.SESSION_TYPE.GROUP
        //     }
        //     // 消息已读标记
        //     // selSess = webim.MsgStore.sessByTypeId(selType, selToID)
        //     // webim.setAutoRead(selSess, true, true)

        //     this.targetId = obj.id
        // },
        created() {
            const self = this
            console.log(self.friendList)
        },
        toggle(i, v) {
            this.active = i
            this.currentView = v
        }
    }
}
</script>
<style lang="scss" scoped>
.IM-panel {
    .im-aside {
        width: 254px;
        border-right: solid 1px #ccc;
        position: absolute;
        left: 0;
        top: 0;
        bottom: 0;
        overflow-x: hidden;
        overflow-y: auto;
    }
    .msg-container {
        position: absolute;
        left: 254px;
        right: 0;
        top: 0;
        bottom: 0;
    }
    .sys-frame {
        padding: 15px;
        height: 100%;
        overflow: auto;
    }
    .notice-frame {
        h2 {
            height: 48px;
            line-height: 48px;
            text-align: center;
            font-weight: normal;
            font-size: 16px;
            color: #333;
        }
        .route-list {
            ul {
                li {
                    width: 50%;
                    float: left;
                    border: 1px solid #ccc;
                    border-right: none;
                    text-align: center;
                    height: 38px;
                    line-height: 38px;
                }
                li:first-child {
                    border-left: none;
                }
                .active {
                    color: rgb(10, 191, 171);
                    border-bottom: 2px solid #0abfab;
                }
                overflow: hidden;
            }
        }
        .notice-main {
            li {
                height: 60px;
                border-bottom: 1px solid #ccc;
            }
        }
    }
}
</style>