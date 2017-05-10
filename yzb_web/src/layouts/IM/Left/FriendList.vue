<template>
<!-- <div class="friend-list FriendsList">
    <el-table :data="friendListData" style="100%">
      <el-table-column
        inline-template
        label = '头像'
      >
        <div><img :src="row.SnsProfileItem[0].value" alt="" class = "avatar"></div>
      </el-table-column>
      <el-table-column
        inline-template
        label="昵称"
      >
        <p @click = "chat(row)">{{row.SnsProfileItem[1].value}}</p>
      </el-table-column>
      <el-table-column
        prop = 'sex'
        label = '性别'
      ></el-table-column>
      <el-table-column
        prop = 'phone'
        label= '电话'
      ></el-table-column>
      <el-table-column
        prop = 'companyName'
        label= '所属公司'
      ></el-table-column>
      <el-table-column
        inline-template
        label= '操作'>
         <el-dropdown  @command="(command) => friendOperationFun(command, row)">
            <el-button type="primary">
              {{row.friendOperation = '发送消息'}}<i class="el-icon-caret-bottom el-icon--right"></i>
            </el-button>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item command="发起会话">发起会话</el-dropdown-item>
              <el-dropdown-item command="删除好友">删除好友</el-dropdown-item>
            </el-dropdown-menu>
         </el-dropdown>
      </el-table-column>
    </el-table>
</div> -->
    <div class="friend-list">
        <div>这一页</div>
        <div>{{friendListData}}</div>
        <div>
          <ul v-for="item in friendListData">
              <!-- <template v-for="Name in item.SnsProfileItem"> -->
              <li><img :src="item.SnsProfileItem[0].Value" alt=""></li>
              <li @click="friendOperationFun(2, item)">{{item.SnsProfileItem[1].Value}}</li>
             <!--  </template> -->
          </ul>
        </div>
    </div>
</template>
<script>
import {mapGetters, mapActions} from 'vuex'
import {deleteFriend} from './../../../IM/friend/index.js'
// import {getMyFriends} from './../../Api/IM/friend/index'
// import {getMyFriends} from './../../Api/IM/friend/index'
export default {
    data () {
      return {
          type: 'C2C',
          friendListData: []
      }
    },
    methods: {
        ...mapActions([
            'getRecentContacts'
        ]),
        IMfriendList () {
            let options = {
                'From_Account': loginInfo.identifier,
                'TimeStamp': 0,
                'StartIndex': 0,
                'GetCount': 100,
                'LastStandardSequence': 0,
                "TagList": [
                    "Tag_Profile_IM_Image",
                    "Tag_Profile_IM_Nick"
                ]
            }
            webim.getAllFriend(
                options,
                (res) => {
                    console.log("获取好友列表");
                    console.log(res);
                    this.friendListData = res.InfoItem;
                },
                (err) => {
                    console.error(err);
                }
            )
            console.log("执行获取好友列表");
            // getMyFriends();
        },
        chat: function (row) {
            const _self = this
            selToID = row.id
            console.log(_self.type, selToID)
            let a = webim.MsgStore.sessByTypeId(_self.type, selToID)
            console.log(a);
        },
        friendOperationFun: function(command, row) {
            // row.friendOperation = command
            if (command === 2) {
              sessionStorage.setItem("好友列表", JSON.stringify(this.friendListData))
                // let exist = false // 判断当前对象是否已存在最近聊天里
                // selType = webim.SESSION_TYPE.C2C;
                // selToID = row.id
                // selSess = new webim.Session(selType, row.id, row.nickname, row.avatarUrl, Math.round(new Date().getTime() / 1000))

                // for (var arr of this.recentContacts) {
                //     if (arr.id === selSess._impl.id) {
                //         exist = true
                //     }
                // }
                // if (!exist) {
                //     this.recentContacts.push(selSess._impl)
                // }

                // selSess = webim.MsgStore.sessByTypeId(selType, selToID)
                // webim.setAutoRead(selSess, true, true)
                this.$router.push('/IM');
            } else if (command === "删除好友") {
                this.removeFriend(row);
            }
        },
        removeFriend: function (row) {
            const _self = this
            this.$confirm('删除好友, 是否继续?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                deleteFriend([row.id], () => {
                    _self.friendList.map((arr, i) => {
                        if (arr.id === row.id) {
                            this.friendList.splice(i, 1)
                            this.$alert('删除好友成功', '', {
                                confirmButtonText: '确定'});
                            }
                    })
                })
            })
        }
    },
    computed: {
        ...mapGetters({
            friendList: "friendList",
            recentContacts: 'recentContacts'
        })
    },
    created () {
        console.log("haha")
        this.IMfriendList();
      // console.log(this.recentContacts);
    }
}
</script>
<style lang="scss" scoped>
  .friend-list{
    ul{
      font-size: 0;
      padding: 20px;
      li{
          vertical-align: top;
          display: inline-block;
          height: 40px;
          font-size: 14px;
      }
      li:nth-child(1) {
          width: 40px;
          height: 40px;
          border: 1px solid #ccc;
          img{
            width: 40px;
            height: 40px;
            border-radius: 4px;
          }
      }
      li:nth-child(2) {
          width: 260px;
          height: 40px;
          border: 1px solid #ccc;
      }
    }
  }
  .FriendsList{
    .el-button--primary {
        background:#ffffff; color:#888888; padding:7px 8px 5px 14px;
        border:1px solid #bbbbbb;
    }
  }
.friend-list {
  .avatar {display:block;height:42px;width:42px; margin: 5px 0;border-radius:50%;}
}
</style>