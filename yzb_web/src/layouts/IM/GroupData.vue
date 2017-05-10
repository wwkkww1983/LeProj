<template>
  <div :class="private ? 'groupData groupPrivate' : 'groupData'">
      <div class="groupData-headPortrait">
        <div class="box"><img :src="mess.icon" alt=""></div>
        <div class="info">{{mess.name}}</div>
      </div>
      <div class="groupData-FriendsProfile">
        <div class="list">
          <div class="list-title">群组名称:</div>
          <div class="list-info">{{mess.name}}</div>
        </div>
        <div class="list">
          <div class="list-title">我在本群昵称:</div>
          <div class="list-info nickname">{{myGroupName.current}} <i @click="editMyNickname=true" class="nickname-icon"></i></div>
        </div>
        <div class="list">
          <div class="list-title">消息免打扰:</div>
          <div class="list-info">
              <el-switch v-model="dnd" on-text=""
  off-text="" on-color="#00e266" :width="55" @change="dodFn">
              </el-switch>
          </div>
        </div>
        <div class="list">
          <div class="list-title">置顶群组:</div>
          <div class="list-info">
            <el-switch v-model="pinned" on-text=""
    off-text="" on-color="#00e266" :width="55" @change="pinnedFn"></el-switch>
          </div>
        </div>
        <div class="list">
           <div class="list-title" @click="removeMsgFun" style="cursor:pointer;">清空聊天记录</div>
        </div>
        <div class="groupMembers">
           <div class="Membertitle" @click="lookMemberVisible = true" style="cursor:pointer">查看全部群成员{{groupMemberList.length}}人</div>


           <div class="Memberlist">
             <div class="item" v-for="(item, index) in groupMemberList" v-if="index < 3" @click="getCheckTheData(item.Member_Account)">
                    <div class="img-fr" :style="{backgroundImage: 'url('+ item.avatarUrl +')'}">
                       <span v-if="!item.avatarUrl">{{item.nickName || item.nickname}}</span>
                    </div>
                    <div class="titlename">{{item.nickName || item.nickname}}<i v-if="item.Role === 'Owner'" class="icon"></i></div>
             </div>
             <div class="item operation" v-if="private" @click="InvitationVisible = true">
                 <div class="delIcon">+</div>
                 <div class="titlename">邀请</div>
             </div>
             <div class="item operation" v-if="private" @click="clearMemberVisible = true">
                 <div class="delIcon">-</div>
                 <div class="titlename">删除</div>
             </div>
           </div>
        </div>
        <!--查看选中资料-->
        <el-dialog custom-class="friendsProfileMin" :top="private ? '15%' : '8%'" style="z-index:9999" :modal="false" v-if="dialogPersonalDataVisible" v-model="dialogPersonalDataVisible">
                <FriendsProfile :props="dialogPersonaHiden" :userId="actionGroupFriendsId"></FriendsProfile>
        </el-dialog>
        <!--全部群成员,讨论组-->
        <el-dialog custom-class="private-dialog" v-model="lookMemberVisible" v-if = "lookMemberVisible && private">
            <addMember
            :title="'全部成员'"
            :memberList= "groupMemberList"
            :allMemberHide = 'allMemberHide'
            v-on:getCheckTheDataFun = "getCheckTheData"
            >
            </addMember>
        </el-dialog>
        <!--全部群成员,公开组-->
        <addMember  v-if = "lookMemberVisible && !private"
        :title="'全部成员'"
        :memberList= "groupMemberList"
        :allMemberHide = 'allMemberHide'
        v-on:getCheckTheDataFun = "getCheckTheData"
        >
        </addMember>
        <!--邀请群成员-->
        <el-dialog custom-class="private-dialog" v-model="InvitationVisible" v-if = "InvitationVisible">
            <addMember
            :title="'邀请成员'"
            :memberList= "friendList"
            :InvitationBlo= "private"
            :allMemberHide = 'allMemberHide'
            v-on:addPrivate= 'addPrivate'
            >
            </addMember>
        </el-dialog>
        <!--删除群成员-->
        <el-dialog custom-class="private-dialog" v-model="clearMemberVisible" v-if = "clearMemberVisible">
            <addMember
            :title="'删除成员'"
            :memberList= "groupMemberList"
            :InvitationBlo= "private"
            :allMemberHide = 'allMemberHide'
            v-on:addPrivate= 'clearMemberTechnique'
            >
            </addMember>
        </el-dialog>
      </div>
      <!--<div class="groupData-editMyNickname" v-if="editMyNickname">
          <div class="header">
              <strong>编辑我的昵称</strong><span @click="editMyNickname = false">x</span>
          </div>
          <div class="compile">
             <input type="text" @focus="dleNmae=false" @blur="dleNmae=true" v-model="myGroupName.will" class="inputName">
             <i class="dleName" v-if="myGroupName.will" @click="myGroupName.will=''"></i>
          </div>
          <div slot="footer" class="dialog-footer">
            <el-button @click="editMyNickname = false" >取 消</el-button>
            <el-button type="primary" @click="changeMyGroupName">确 定</el-button>
          </div>
      </div>-->
  </div>
</template>
<script>
    import {mapGetters} from 'vuex'
    import {addGroupMember, deleteGroupMember} from '../../IM/group/index.js'
    import {getUserById} from '../../Api/user.js' // 获取好友资料
    import node from '../../components/Q-node.vue'
    import FriendsProfile from './FriendsProfile.vue' // 获取对象资料
    import AddMember from './components/AllMember.vue' // 全成员
    export default {
        data () {
            return {
               dnd: false,
               pinned: false,
               editMyNickname: false,
               myGroupName: {
                  current: 'xxx',
                   will: 'xxx'
               },
               dleNmae: true,
               groupName: '',
               groupImg: require('../../assets/img/chat/inputImg.png'),
               lookMemberVisible: false,
               actionGroupFriendsId: '', // 群友资料
               dialogPersonalDataVisible: false,
               InvitationVisible: false, // 邀请成员
               clearMemberVisible: false // 删除群成员
            }
        },
        components: {
            node,
            FriendsProfile,
            AddMember
        },
        props: [
            'mess'
        ],
        computed: {
            ...mapGetters([
                'dndList',
                'stick',
                'recentContacts',
                'friendList',
                'groupMemberList'
            ]),
            private: function () {
                if (this.mess.typeEn === 'Private') {
                    return true
                } else {
                    return false
                }
            }
        },
        methods: {
          dodFn: function (msg) { // 消息免打扰
              const _self = this
              if (msg) {
                 this.dndList.push(_self.mess.id)
                 console.log(this.dndList)
              } else {
                 this.dndList.map((id, i) => {
                     if (id === _self.mess.id) {
                         _self.dndList.splice(i, 1)
                         console.log(this.dndList)
                     }
                 })
              }
          },
          removeMsgFun: function () { // 清空历史记录
             this.$emit('removeMsg')
          },
          pinnedFn: function (msg) { // 置顶群组
              const _self = this;
              if (msg) {
                 this.stick.push(_self.mess.id)

                 for (let j = 0; j < this.recentContacts.length; j++) {
                   if (this.mess.id === this.recentContacts[j].id) {
                        this.recentContacts.unshift(this.recentContacts.splice(j, 1)[0])
                    }
                 }
              } else {
                 for (let i = 0; i < this.stick.length; i++) {
                    if (this.stick[i] === _self.mess.id) {
                         _self.stick.splice(i, 1)
                     }
                 }
              }
          },
          changeMyGroupName: function () {
             if (this.myGroupName.will.length <= 0) {
                 this.$alert('长度不能为空', '')
                 return
             }
          },
          getCheckTheData: function (data) { // 查看资料
            this.dialogPersonalDataVisible = true
            this.actionGroupFriendsId = data
          },
          allMemberHide: function() { // 隐藏addMember组件
             this.lookMemberVisible = false
             this.InvitationVisible = false
             this.clearMemberVisible = false
          },
          dialogPersonaHiden () {
             this.dialogPersonalDataVisible = false
          },
          addPrivate: function (data) { // 邀请好友操作
            const self = this
            let oMemberList = []
            for (let i = 0; i < data.length; i++) {
               oMemberList.push({
                 'Member_Account': data[i]
               })
            }
            var options = {
                'GroupId': this.mess.id,
                'MemberList': oMemberList
            }
            addGroupMember(options, (msg) => {
                for (let j = 0; j < data.length; j++) {
                    let blo = true
                    for (let i = 0; i < self.groupMemberList.length; i++) {
                        if (data[j] === self.groupMemberList[i].Member_Account) {
                            blo = false
                            break
                        }
                    }
                    if (blo) { // 未在群成员列表内
                        getUserById({id: data[j]}).then((res) => {
                            let data = res.data.data
                            data.Member_Account = data.id
                            self.groupMemberList.push(data)
                            self.allMemberHide()
                        }, (err) => {
                            alert(err)
                        })
                    }
                }
            })
          },
          clearMemberTechnique: function (data) { // 删除好友操作
                const self = this
                var options = {
                    'GroupId': this.mess.id,
                    'MemberToDel_Account': data
                }
                deleteGroupMember(options, (msg) => {
                    for (let j = 0; j < data.length; j++) {
                        for (let i = 0; i < self.groupMemberList.length; i++) {
                            if (self.groupMemberList[i].Member_Account === data[j]) {
                                // console.log(self.name)
                                self.groupMemberList.splice(i, 1)
                                self.allMemberHide()
                            }
                        }
                    }
                })
          }
        },
        created () {
          // console.log(this.mess)
        }
    }
</script>
<style lang="scss">
  .groupData {position:relative; display:flex; flex-direction:column;}
  .groupData-headPortrait {padding:30px 0px 20px 0px; height:233px; text-align:center;
         .box {width:88px; height:88px; margin-bottom:10px; display:inline-block;
            img {border-radius:50%; width:88px; height:88px; display:inline-block; cursor:pointer;}
         }
         .info {line-height:1; color:#876755; font-size:19px; text-align:center;}
    }
  .groupData-FriendsProfile { flex:1; padding:0px 28px 45px 40px; background:#ffffff; color:#101010; font-size:17px;
        .list {height:55px; border-bottom:1px solid #aaaaaa; display:flex;}
        .list:last-of-type { border:none; color:#e51c23}
        .list {
           .list-title,.list-info {float:left; text-align:left; line-height:55px;}
           .list-title {width:108px; font-weight:100; font-size:17px;}
           .list-info {flex:1; font-size:18px; text-align:right; padding-right:6px;}
        }

        /*.el-switch_core {border-radius:17px;}*/
        .nickname-icon {position:relative; top:4px; width:27px; height:21px; background:url(../../assets/img/chat/channame.png) no-repeat;
            display:inline-block; cursor:pointer;
        }
        .groupMembers {width:100%;
            .Membertitle {height:50px; padding-top:10px;}
            .Memberlist {display:flex;
               .item {flex:1; cursor:pointer;
                    .img-fr {margin:0px auto; margin-bottom:15px; width:55px; height:55px; border-radius:50%; background-size:cover;background-position:center;
                       span {font-size:24px;color:#fff;line-height:40px;display:block;text-align: center;}
                    }
                    .titlename {text-align:center; line-height:1; }
                    .icon {width:16px; height:17px; display:inline-block; background:url(../../assets/img/chat/dataImg.png) no-repeat;}
               }
            }

            .operation {flex:1; cursor:pointer;
              .delIcon {margin:0px auto; width:55px; height:55px; border-radius:50%; text-align:center; line-height:55px;
                 font-weight:1200px; border:1px solid #c5c5c5; margin-bottom:15px; font-size:32px; color:#607d8b;
              }
              .titlename {text-align:center; width:100%;}
            }
        }
        .friendsProfileMin {width: 296px;}

    }
  .groupData-editMyNickname { position:absolute; width:396px; height:222px; padding:0px 22px; background:#ffffff;
        left:-50px; top:280px; border:1px solid #d1d1d1;
        .header { margin-bottom:17px;height:59px; text-align:center; line-height:59px; border-bottom:1px solid #bbbbbb;
                    font-size:16px;
                span {cursor:pointer; float:right; padding-right:5px; font-weight:bold;}
        }
        .compile {width:100%; height:42px; border:1px solid #bbbbbb; padding:0px 8px;
            .inputName {float:left; width:305px; height:42px; line-height:42px; font-size:14px; color:#101010; border:none; background:none; outline:medium}
            .dleName {float:left; margin-top:5px; cursor:pointer; width:27px; height:28px; display:inline-block; background:url(../../assets/img/chat/del.png) no-repeat;}
        }
  }

  // 讨论组样式
  .groupPrivate {width:448px; position:absolute; top:42px; bottom:0px; right:0px;
     /*.friendsProfileMin {height:527px}*/
     .el-dialog__header {border:none; position:absolute; top:0px; right:0px; z-index:3}
      background:#ffffff; border:1px solid rgba(#bbbbbb,.4); z-index:8;
     border-right:none;border-bottom:none;
     .private-dialog {width:436px;}
     .friendsProfileMin {
         .el-dialog__body {padding:0px}
     }
  }

</style>