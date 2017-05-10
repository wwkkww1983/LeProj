<template>
  <div class="login-panel">
      <el-form v-if="isCurrent == 'pw'" label-position = "right" :model = "userInfo" :rules="rules" class="demo-form-stacked">
        <el-form-item prop="tel">
          <el-input v-model="userInfo.phoneNumber" class="phone-number" placeholder="请输入手机号"></el-input>
        </el-form-item>
        <el-form-item prop="password" style="margin-top: 20px;">
          <el-input v-model="userInfo.password" type="password" class="password-number" placeholder="请输入密码">
            <!--<el-button slot="append" icon="search"></el-button>-->
          </el-input>
        </el-form-item>
        <el-form-item style="margin-top: 20px;">
          <span class="radio" v-bind:class="{'active': isRemember}" @click="handleRemember">&nbsp;记住登录状态</span>
          <span style="float:right;">
            <span style="cursor:pointer;-webkit-user-select:none; -moz-user-select:none; -ms-user-select:none; user-select:none;" @click="gotoForget">忘记密码?</span>
            <span style="color:#4fdcca;margin-left:16px;cursor:pointer;-webkit-user-select:none; -moz-user-select:none; -ms-user-select:none; user-select:none;" @click="gotoRegistry">免费注册</span>
          </span>
        </el-form-item>
        <el-form-item style="margin-top: 20px;">
          <el-button native-type="button" type="primary" size="large" class="btn-block" @click="ajaxLogin">登录</el-button>
        </el-form-item>
      </el-form>
      <el-form v-else>
        <el-form-item prop="tel">
          <el-input v-model="userInfo.phoneNumber" class="phone-number" placeholder="请输入手机号"></el-input>
        </el-form-item>
        <el-form-item style="margin-top: 20px;">
          <el-input v-model="userInfo.SMSCode" type="text" class="password-number" placeholder="请输入验证码">
            <el-button v-if="sended" slot="append" style="width:115px;">已发送({{subsub<10? '0'+subsub : subsub}}s)</el-button>
            <el-button v-else slot="append" style="color:#4fdcca;width:115px;" @click.native="handleIconClick">发送验证码</el-button>
          </el-input>
        </el-form-item>
        <el-form-item style="margin-top: 20px;">
          <el-button native-type="button" type="primary" size="large" class="btn-block" @click="ajaxLogin">登录</el-button>
        </el-form-item>
        <span style="float:right; margin-top: 10px;">
          <span style="color:#4fdcca;cursor:pointer;" @click="gotoRegistry">免费注册</span>
        </span>
      </el-form>
  </div>
</template>

<script>
import {userLogin, getVcCode} from '../../../Api/auth_v1'
// import {webimLogin} from './../../../Api/IM/login/login'
// import {login} from '../../../IM/index'
export default {
  data () {
    return {
      userInfo: {
        phoneNumber: '',
        password: '',
        SMSCode: '',
        operateType: '1'
      },
      rules: {
        password: [
            {required: false, message: '请输入密码', trigger: 'blur'}
        ],
        smscode: [
            {required: false, message: '请输入验证码', trigger: 'blur'}
        ]
      },
      radio: '0',
      sended: false,
      subsub: 60,
      isRemember: false
    }
  },
  props: ['isCurrent'],
  methods: {
    handleRemember (ev) {
      this.isRemember = !this.isRemember;
    },
    handleIconClick (ev) {
      let data = {
        phone: this.userInfo.phoneNumber,
        type: '02'
      };
      if (!(/^1[34578]\d{9}$/.test(data.phone))) {
        alert("请正确输入有效手机号.");
      } else {
        let self = this;
        getVcCode(data).then((res) => {
          console.log(res.data);
          if (res.data.state === 200) {
            self.sended = true;
            self.intersubsub = setInterval(() => {
              --self.subsub;
              if (self.subsub <= 0) {
                clearInterval(self.intersubsub)
                self.sended = false;
                self.subsub = 60;
              }
            }, 1000);
          } else {
            alert(res.data.msg);
          }
        });
      }
    },
    gotoRegistry (ev) {
      this.$emit('goto-registry', true);
    },
    gotoForget (ev) {
      this.$emit('goto-forget', true);
    },
    ajaxLogin (ev) {
      const self = this
      let data = {
        phone: this.userInfo.phoneNumber
      }
      if (this.isCurrent === 'pw') {
        data.password = md5(this.userInfo.password);
        data.operateType = '1'
      } else {
        data.SMSCode = this.userInfo.SMSCode;
        data.operateType = '2'
      }
      if (!(/^1[34578]\d{9}$/.test(this.userInfo.phoneNumber))) {
        alert('请正确输入电话号码');
        return;
      }
      if (this.isCurrent === 'pw') {
        if (this.userInfo.password === '' || this.userInfo.password.length > 32) {
          alert('请正确输入密码');
          return;
        }
      } else {
        if (this.userInfo.SMSCode === '' || this.userInfo.SMSCode.length > 4) {
          alert('请正确输入验证码');
          return;
        }
      }
      console.log(data);
      userLogin(data).then((res) => {
        if (res.data.state === 200) {
          console.log('login success:');
          console.log(res.data);
          if (this.isRemember) {
            this.userInfo.phone = this.userInfo.phone;
            this.userInfo.password = this.userInfo.password;
          } else {
            this.userInfo.phone = '';
            this.userInfo.password = '';
          }
          sessionStorage.setItem('id', res.data.data.id);
          sessionStorage.setItem('companyId', res.data.data.companyId);
          sessionStorage.setItem('tokenId', res.data.data.tokenId);
          sessionStorage.setItem('sig', res.data.data.sig);
          sessionStorage.setItem('role', res.data.data.role);
          self.$router.push('/work/');
        } else {
          alert(res.data.msg);
        }
      });
    }
  }
}
</script>
<style lang="scss">
  .radio.active {
    cursor: pointer;
    -webkit-user-select:none; -moz-user-select:none; -ms-user-select:none; user-select:none;
    margin-left: 20px;
    &::before {
      content: '';
      position: absolute;
      top: 50%;
      left: 0px;
      margin-top: -9px;
      width: 16px;
      height: 16px;
      background: #4fdcca;
      border: 1px solid #4fdcca;
      -moz-border-radius: 8px;
      -webkit-border-radius: 8px;
      border-radius: 8px;
      cursor: pointer;
    }
  }
  .radio {
    cursor: pointer;
    margin-left: 20px;
    -webkit-user-select:none; -moz-user-select:none; -ms-user-select:none; user-select:none;
    &::before {
      content: '';
      position: absolute;
      top: 50%;
      left: 0px;
      margin-top: -9px;
      width: 16px;
      height: 16px;
      background: none;
      border: 1px solid #999;
      -moz-border-radius: 8px;
      -webkit-border-radius: 8px;
      border-radius: 8px;
      cursor: pointer;
    }
  }
  .login-panel{
    .phone-number{
      &:before {position:absolute; left:14px; top:15px; z-index:999; width:30px; height:20px; background:url(../../../assets/img/login/phone.png) no-repeat; display:inline-block; content:" ";}
      input{padding:0px 42px; width:100%; height:50px; border:1px solid #d3d3d3; border-radius:3px;font-size:14px; color:#999999; line-height:50px;float: left;}
    }
    .password-number{
      &:before {position:absolute; left:14px; top:15px; z-index:999; width:30px; height:20px; background:url(../../../assets/img/login/passwords-2.png) no-repeat; display:inline-block; content:" ";}
      input{padding:0px 42px; width:100%; height:50px; border:1px solid #d3d3d3; border-radius:3px;font-size:14px; color:#999999; line-height:50px;float: left;}
    }
    .btn-block{padding:0px; width:100%; height:50px; line-height:50px; background:#4fdcca; text-align:center;color:#fff; font-size:18px;margin-top:30px;border: none;}
    .el-form-item {margin-bottom: 5px;}
  }
</style>