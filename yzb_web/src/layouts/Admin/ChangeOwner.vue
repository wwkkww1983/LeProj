<template>
  <div class="changeOwner">
    <el-steps class="changeOwner-stepLine" :space="200" :active='active' finish-status="success" :align-center="true">
      <el-step description="确认身份"></el-step>
      <el-step description="确认新创建者"></el-step>
      <el-step description="转让成功"></el-step>
    </el-steps>
    <el-row>
      <el-col :span="8" :offset="8">
        <p style="color:red;margin:0 -50px;text-align:center;margin-bottom:40px;">*转让创建者后，您将不再拥有创建者的权限，也不为该公司员工</p>  
        <div class="panel" v-if="active == 0">
          <el-form :rules = 'rules' :model="form">
            <el-form-item prop='phone'>
              <el-input v-model= 'form.phone' placeholder="请输入本人手机号"></el-input>
            </el-form-item>
            <el-form-item>
              <el-row :gutter="20">
                <el-col :span="14">
                  <el-input placeholder="请输入验证码" v-model="form.code"></el-input>
                </el-col>
                <el-col :span="10">
                  <el-button @click="getCode" class="btn-block" :disabled="codeDisable">{{codeState}}</el-button>
                </el-col>
              </el-row>
            </el-form-item>
            <el-form-item>
              <el-button @click="nextStep">下一步</el-button>
            </el-form-item>
          </el-form>
        </div>
        <div class="panel" v-if="active == 1">
          <el-form>
            <el-form-item>
              <el-row :gutter="20">
                <el-col :span="18">
                  <el-input v-model='search.searchVal' placeholder="请输入转让对象的手机号"></el-input>
                </el-col>
                <el-col :span="6">
                  <el-button @click="searchStaff">搜索</el-button>
                </el-col>
              </el-row>
            </el-form-item>
            <el-form-item v-if="search.searchRes">
              <el-row v-for="res in search.searchRes" class="resRow">
                <el-col :span="14" class="resRowLeft">
                  <Qnode :nodeName="res.nickname" :img="res.avatarUrl" :nodeMessage="res.companyName"></Qnode>
                </el-col>
                <el-col :span="10" class="resRowRight">
                  <el-button @click="changeOwner(res)">设置为创建者</el-button>
                </el-col>
              </el-row>
              
            </el-form-item>
          </el-form>
        </div>
        <div class="panel" v-if="active == 2">
          <el-form>
            <el-form-item>
              <el-button @click="nextStep">确认</el-button>
            </el-form-item>
          </el-form>
        </div>
      </el-col>
    </el-row>
  </div>
</template>
<script>
  import {getVcCode, validateCodeForPhone} from '../../Api/auth'
  import {getInfoByPN} from '../../Api/user'
  import {transCreater} from '../../Api/company'
  import Qnode from '../../components/Q-node'
  export default {
    data() {
      let _self = this
      // 验证手机号
      let validatePhone = (rule, value, callback) => {
        if (value === '') {
          _self.codeDisable = true
          callback(new Error('请输入手机号码'));
        } else if (value !== '13097202014') {
          _self.codeDisable = true
          callback(new Error('手机号码错误'));
        } else {
          _self.codeDisable = false
          callback()
        }
      }
      return {
        active: 0,
        codeState: '获取验证码',
        codeDisable: true,
        search: {
          searchVal: '',
          searchRes: null
        },
        form: {
          phone: '',
          code: ''
        },
        rules: {
          phone: [{
            validator: validatePhone,
            trigger: 'change'
          }]
        }
      }
    },
    components: {
      Qnode
    },
    methods: {
      getCode() {
        let _self = this
        let data = {
          phone: _self.form.phone,
          type: 'GET_LOGIN_CODE'
        }
        _self.codeDisable = true;
        getVcCode(data).then((res) => {
          let times = 5
          let t
          _self.codeState = '剩余 ' + times + ' 秒'
          t = setInterval(function() {
            if (times <= 1) {
              clearInterval(t)
              _self.codeDisable = false;
              _self.codeState = '获取验证码'
            } else {
              times--
              _self.codeState = '剩余 ' + times + ' 秒'
            }
          }, 1000)
        })
      },
      nextStep() {
        let _self = this
        let data = this.form
        validateCodeForPhone(data).then((res) => {
          if (res.data.state === 200) {
            this.active++
          } else if (res.data.state === 40002) {
            _self.$message.error('验证码错误');
          }
        })
      },
      searchStaff() {
        let _self = this
        if (_self.search.searchVal === '') return;
        let data = {
          PN: _self.search.searchVal
        }
        console.log(data)
        getInfoByPN(data).then((res) => {
          _self.search.searchRes = res.data.data
          console.log(_self.search.searchRes)
        })
      },
      changeOwner (obj) {
        let data = {
          companyName: this.$store.getters.comInfo.company.name,
          newUserId: obj.id
        }
        transCreater(data).then((res) => {
          console.log(res)
        })
      }
    }
  }
</script>
<style lang="scss">
  .changeOwner {
    .changeOwner-stepLine {
      text-align: center;
      margin-top: 50px;
      padding-left: 200px;
      margin-bottom: 50px;
    }
    .resRow {border: solid 1px #ccc;
      .Q-node{border-bottom:0;}
      .Q-node:hover{box-shadow: none;}
      .resRowRight{padding-top: 12px;text-align: center;}
    }
  }
</style>