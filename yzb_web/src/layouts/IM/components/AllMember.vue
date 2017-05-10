<template>
  <div class="lookMember allMember">
      <div class="el-dialog__header allMember_el-dialog__header" @click="hiden">
        <span class="el-dialog__title"></span>
        <div class="el-dialog__headerbtn"><i class="el-dialog__close el-icon el-icon-close"></i></div>
      </div>
      <div class="allMember-tiarBlock">
        <div class="tiarBlock-title">{{title}} <i class="icon"></i></div>
        <div class="listApply">
          <div class="search">
            <input type="text" v-model="searchData" class="search-inpt" placeholder="搜索手机号码/好友昵称">
            <i class="search-icon"></i>
          </div>
          <div class="tiarBlock-list">
            <node v-if="!searchData"
              v-for= "user in memberList"
              :role= "user.Role"
              :id = "user.id || user.Member_Account"
              @click.native= 'getCheckTheData(user.Member_Account)'
              v-on:addAction= 'addAction'
              :avatarUrl= "user.avatarUrl"
              :selectBlo= "InvitationBlo"
              :nodeName="user.nickName || user.nickname">
            </node>
            <node v-if="searchData"
              v-for= "user in searchMemberList"
              :role= "user.Role"
              :id = "user.id"
              @click.native= 'getCheckTheData(user.Member_Account)'
              v-on:addAction= 'addAction'
              :avatarUrl= "user.avatarUrl"
              :selectBlo= "InvitationBlo"
              :nodeName="user.nickName || user.nickname">
            </node>
          </div>

        </div>
        <div class="allMember-bottm" v-if="InvitationBlo">
              <div class="allMember-bottm-info">已选择：（{{actionFriend.length}}人）</div>
              <div class="btn" @click="addPrivate">确定</div>
        </div>
      </div>
  </div>

</template>

<script>
   import node from '../../../components/Q-node.vue'
   export default {
     data () {
       return {
          searchMemberList: [], // 搜索data
          actionFriend: [],
          searchData: '' // 搜索信息
       }
     },
     props: [
       'title',
       'memberList', // 成员
       'allMemberHide', // 群组件隐藏
       'InvitationBlo' // 邀请群友组件判断
      ],
     components: {
        node
     },
     watch: {
           searchData: function (val, oldVal) {
               const _self = this
               let nameSearch = new RegExp(val, "g")
               this.searchMemberList = []
               this.actionFriend = []
               this.memberList.map((item, index) => {
                   let name = item.nickName || item.nickname
                   if (nameSearch.test(name)) {
                       _self.searchMemberList.push(item)
                   }
               })
           }
     },
     methods: {
          getCheckTheData: function (id) {
             this.$emit('getCheckTheDataFun', id)
          },
          addAction: function (blo, id) { // 选中成员
            if (blo) {
              this.actionFriend.push(id)
            } else {
              this.actionFriend.map((item, index) => {
                  if (item === id) {
                     this.actionFriend.splice(index, 1)
                  }
              })
            }
            console.log(id)
            console.log(this.actionFriend)
          },
          addPrivate: function () { // 邀请好友操作
            if (!this.actionFriend.length) {
              alert('请选择你邀请的人员')
            } else {
              this.$emit('addPrivate', this.actionFriend)
            }
          },
          hiden: function () {
            this.allMemberHide()
          }
     },
     created () {
     }
   }
</script>

<style lang="scss">
  .lookMember {position:fixed; top:0px; left:0px; z-index:999; right:0px; bottom:0px;}
  .allMember-tiarBlock {width:100%; height:110%; background:#ffffff;

      .tiarBlock-title {widthL:100%; height:51px; line-height:51px; text-align:center; border-bottom:1px solid #bbbbbb;}
      .search {height:67px; padding-top:20px;}
      .listApply {width:100%; padding:20px 36px 38px 36px; background:#ffffff;
                .search {margin-bottom:10px; position:relative; padding:6px 10px; height:46px; background:#cccccc; border-bottom:1px solid #bbbbbb;}
                .search-inpt {padding-left:26px; float:left; width:100%; height:32px; border-radius:6px; border:none; background:#ffffff; outline:none;}
                .search-icon {position:absolute; left:15px; top:14px; width:16px; height:16px; background:url(../../../assets/img/commonality/seek.png) no-repeat;}
            }
      .tiarBlock-list {width:100%; height:362px; overflow-y:auto;
          .item:hover {background:#cccccc;
              .useNmae {color:#ffffff;}
          }
      }
  }
  .allMember-bottm {padding:10px 36px 15px 36px; overflow:hidden; background:#ffffff;
     .btn {cursor:pointer; position:relative; top:0px; float:right; width:67px; height:30px; border-radius:6px; background:#e84e40;
        color:#ffffff; font-size:12px; line-height:30px; text-align:center;
     }
  }
  .allMember-bottm-info {padding-top:8px;line-height:1; float:left; font-size:16px; color:#607d8b;}
  .allMember_el-dialog__header {border:none; position:absolute; top:0px; right:0px
   }
</style>