<template>
<div class="FriendsProfile">
    <div class="FriendsProfile-head">
        <div class="box"><img v-if="user.avatarUrl" :src="user.avatarUrl" alt=""></div>
        <div class="info">{{user.nickname}}</div>
    </div>
    <div class="FriendsProfile-min">
          <div class="list">
              <div class="title">姓名</div>
              <div class="info">{{user.nickname}}</div>
          </div>
          <div class="list">
              <div class="title">性别</div>
              <div class="info">{{sexComputed}}</div>
          </div>
          <div class="list">
              <div class="title">联系电话</div>
              <div class="info">{{user.phone}}</div>
          </div>
          <div class="list">
              <div class="title">所在公司：</div>
              <div class="info">{{user.companyName}}</div>
          </div>
          <div class="list">
              <el-button class="btn-block" v-if="isFriendsBlo" @click="removeFriend">删除好友</el-button>
              <el-button class="btn-block" v-if="!isFriendsBlo" @click="addFriend">添加好友</el-button>
          </div>
          </div>
    </div>
</div>
</template>
<script>
  import {getUserById} from '../../Api/user.js'
  import {deleteFriend} from '../../IM/friend/'
  import { applyAddFriend } from '../../IM/friend'
  import {mapGetters} from 'vuex'
export default {
  data () {
    return {
        user: {},
        isFriendsBlo: true
    }
  },
  props: [
    'userId'
  ],
  computed: {
      ...mapGetters([
          'friendList'
      ]),
      sexComputed: function () {
          let sex = ''
          if (this.user.sex === '1') {
             sex = '男'
          } else if (this.user.sex === '2') {
             sex = '女'
          } else {
             sex = '未知'
          }
          return sex
      }
  },
  methods: {
      removeFriend: function () {
        const _self = this
          this.$confirm('删除好友, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
               deleteFriend([_self.userId], () => {
                   _self.isFriendsBlo = false
                   _self.friendList.map((arr, i) => {
                       if (arr.id === _self.userId) {
                           _self.friendList.splice(i, 1)
                       }
                   })
               })
          })

      },
      addFriend: function () {
         applyAddFriend(this.user, (res) => {

          this.friendList.push(this.user)
          this.isFriendsBlo = true
          this.$alert('添加成功')
        })
      },
      isFriend (str) { // 判断是否为好友
        const friendObj = this.friendList
        if (friendObj) {
            for (let i = 0; i < friendObj.length; i++) {
            if (friendObj[i].id === str) return true
            }
        }
        return false
     }
  },
  created: function () {
      if (this.isFriend(this.userId)) {
         this.isFriendsBlo = true
      } else {
          this.isFriendsBlo = false
      }
      getUserById({id: this.userId}).then(result => {
               if (result.data.state === 200) {
                   this.user = result.data.data
               }
       })
    }
  }
</script>
<style lang="scss">
    .FriendsProfile {background:#aaaaaa;  width: 296px;
       .el-dialog__header {
            width: 100%;
            border-bottom: none;
            position: absolute;
            top: 0px;
            left: 0px;
            z-index: 5;
        }
        .el-dialog__close {
           color: rgba(255, 255, 255, .7)
        }
        .el-dialog__body {
           padding: 0px;
        }
    }
    .FriendsProfile-head {
        padding-top:50px; height:177px; text-align:center;
            .box {width:82px; height:82px; margin-bottom:10px; border-radius:50%; background:#ffffff; display:inline-block;
                img {border-radius:50%; width:82px; height:82px; display:inline-block;}
            }
       .info {line-height:1; color:#ffffff; font-size:15px; text-align:center;}
    }
    .FriendsProfile-min {
        padding:0px 35px 45px 25px; background:#ffffff;
        .list {height:38px; border-bottom:1px solid #aaaaaa; display:flex;}
        .list:last-of-type {margin-top:30px; border:none;}
        .title,.info {float:left; text-align:left; line-height:40px;}
        .title {width:78px; font-weight:100; font-size:14px;}
        .info {flex:1; font-size:15px;}
        button {width:100%;}
    }


</style>