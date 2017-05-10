<template>
  <div class="add-staff">
    <el-form @submit.native.prevent = "search">
      <template v-if="step === 1">
        <el-form-item label="手机号码添加" class="phone-number" >
          <el-input v-model="phoneNumber"></el-input>
        </el-form-item>
        <div class="search-result" v-if = "res">
          <Qnode :nodeName="res.nickname" :img="res.avatarUrl" ></Qnode>
        </div>
        <el-form-item class="button-panel">
          <el-button @click.native = "res = null">取消</el-button>
          <el-button v-if="res" @click.native = "addStaff">邀请</el-button>
          <el-button v-else @click.native = "search" >下一步</el-button>
        </el-form-item>
      </template>
      <template v-if="step === 2 ">
      </template>
    </el-form>
  </div>
</template>
<script>
import Qnode from '../../../components/Q-node'
import {inviteStaff} from '../../../Api/staff'
export default {
  data () {
    return {
      phoneNumber: '',
      res: null,
      step: 1
    }
  },
  methods: {
    search: function () {
      let _self = this
      let tokenId = sessionStorage.getItem('tokenId')
      let url = INTERFACE_URL + `:9082/v1/user/userMsg/${this.phoneNumber}/${tokenId}`
      this.$http.get(url).then((res) => {
        _self.res = res.data.data
        console.log(_self.res)
      })
    },
    addStaff: function () {
      let _self = this
      let data = {
        comName: this.$store.getters.userInfo.companyName,
        depId: _self.dep ? _self.dep.id : '0',
        shopId: _self.dep ? _self.dep.id : '0',
        depName: _self.dep ? _self.dep.fullName : "0",
        type: _self.dep ? _self.dep.type : '3',
        userId: this.res.id,
        userName: this.$store.getters.userInfo.nickname,
        groupId: _self.dep ? _self.dep.groupId : '0'
      }
      inviteStaff(data).then((res) => {
        _self.$alert('邀请已发送，等待对方接受！')
      })
    }
  },
  props: [
    'dep'
  ],
  components: {
    Qnode
  }
}
</script>
<style lang="scss">
.add-staff{width:450px;
  .phone-number{}
  .button-panel{margin-top: 50px;text-align: right}
  .Q-node {border:solid 1px #ccc;}
}
</style>