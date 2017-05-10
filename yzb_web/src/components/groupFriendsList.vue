<template>
    <div class="groupFriendsList"  @click="dialogPersonalDataVisible = true">
          <div class="urlImg" :style="{background:'url('+user.avatarUrl+') no-repeat;'}"></div>
          <img :src="user.avatarUrl" alt="" width="25px" height="25px">
          <div class="useNmae">{{user.nickName}}</div>
          <el-dialog custom-class="friendsProfile" v-model="dialogPersonalDataVisible"><!--查看群友资料-->
                  <!--<FriendsProfile :props="dialogPersonaHiden" :mess="mess"></FriendsProfile>-->
          </el-dialog>
    </div>
</template>
<script>
   export default {
     data () {
       return {
          dialogPersonalDataVisible: false
       }
     },
     props: ['user'],
     created () {
     }
   }
</script>

<style lang="scss" scoped>
.groupFriendsList {
    padding: 5px 17px 0px;
    width: 100%;
    height: 56px;
    border-bottom: 1px solid #bbbbbb;
    cursor: pointer;
    .urlImg {
        margin-right: 25px;
        float: left;
        width: 40px;
        height: 40px;
        border: 1px solid #bebebe;
        background-size: cover;
        border-radius: 50%;
    }
    .useNmae {
        float: left;
        width: 230px;
        height: 44px;
        padding-top: 13px;
        font-size: 14px;
        color: #101010;
    }
}
</style>