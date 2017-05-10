<template>
  <div class="add-sysAdmin">
    <el-form @submit.native.prevent = "findStaff">
      <template v-if="step === 1">
        <el-form-item label="手机号码添加" class="phone-number" >
          <el-input v-model="phoneNumber"></el-input>
        </el-form-item>
        <div class="search-result" v-if = "res">
          <Qnode :nodeName="res.nickName" :img="res.avatarUrl" :nodeMessage="res.companyName"></Qnode>
        </div>
        <el-form-item class="button-panel">
          <el-button @click.native = "res = null">取消</el-button>
          <el-button v-if="res" @click.native = "setAdmin">设置</el-button>
          <el-button v-else @click.native = "findStaff" >下一步</el-button>
        </el-form-item>
      </template>
      <template v-if="step === 2 ">
      </template>
    </el-form>
  </div>
</template>
<script>
  import {findStaffByphone, setSysAdmin} from '../../../Api/authChange'
  import Qnode from '../../../components/Q-node'
  import {mapActions} from 'vuex'
  export default {
    data () {
      return {
        phoneNumber: null,
        step: 1,
        res: null
      }
    },
    components: {
      Qnode
    },
    methods: {
      ...mapActions([
        'getComInfo'
      ]),
      findStaff () {
        let data = {
          phone: this.phoneNumber
        }
        findStaffByphone(data).then((res) => {
          this.res = res.data.data;
          console.log(this.res);
        }, (err) => {
          console.log(err);
        })
      },
      setAdmin () {
        let data = {
          userId: this.res.userId
        }
        setSysAdmin(data).then((res) => {
          console.log(res);
          if (res.data.state === 200) {
            this.getComInfo()
            this.$alert('设置成功')
          }
        }, (err) => {
          console.log(err);
        })
      }
    }
  }
</script>
<style lang="scss">
.add-sysAdmin{width:450px;
  .phone-number{}
  .button-panel{margin-top: 50px;text-align: right}
  .Q-node {border:solid 1px #ccc;}
}
</style>
