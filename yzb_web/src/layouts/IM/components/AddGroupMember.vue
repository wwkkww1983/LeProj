<template>
   <div class="addGroupMember">
      <div class="el-dialog__header" @click="lookMemberVisibleFun">
        <span class="el-dialog__title"></span>
        <div class="el-dialog__headerbtn"><i class="el-dialog__close el-icon el-icon-close"></i></div>
      </div>
      <div class="allMember-tiarBlock">
        <div class="tiarBlock-title">全部成员 <i class="icon"></i></div>
        <div class="listApply">
          <div class="search">
            <input type="text" v-model="searchData" class="search-inpt" placeholder="搜索手机号码/群友昵称">
            <i class="search-icon"></i>
          </div>
          <div class="tiarBlock-list">
            <node v-if="!searchData" v-for="user in memberList" :role="user.Role" @click.native='getCheckTheData(user.Member_Account)'
              :avatarUrl="user.avatarUrl" :nodeName="user.nickName">
            </node>
            <node v-if="searchData" v-for="user in searchMemberList" :role="user.Role" @click.native='getCheckTheData(user.Member_Account)'
              :avatarUrl="user.avatarUrl" :nodeName="user.nickName">
            </node>
          </div>
        </div>
      </div>
   </div>
</template>
<script>
   export default {
     data () {
       return {

       }
     }
   }
</script>
<style lang="scss">
    .lookMember {position:fixed; top:0px; left:0px; z-index:999; right:0px; bottom:0px; background:rgba(#000000,.4); }
  .allMember-tiarBlock {width:100%; height:100%; background:#ffffff;
      .tiarBlock-title {widthL:100%; height:51px; line-height:51px; text-align:center; border-bottom:1px solid #bbbbbb;}
      .search {height:67px; padding-top:20px;}
      .listApply {width:100%; padding:20px 36px 38px 36px;
                .search {margin-bottom:10px; position:relative; padding:6px 10px; height:46px; background:#cccccc; border-bottom:1px solid #bbbbbb;}
                .search-inpt {padding-left:26px; float:left; width:100%; height:32px; border-radius:6px; border:none; background:#ffffff; outline:none;}
                .search-icon {position:absolute; left:15px; top:14px; width:16px; height:16px; background:url(../../../assets/img/commonality/seek.png) no-repeat;}
            }
      .tiarBlock-list {width:100%; height:560px; overflow-y:auto;
          .item:hover {background:#cccccc;
              .useNmae {color:#ffffff;}
          }
      }
  }
</style>