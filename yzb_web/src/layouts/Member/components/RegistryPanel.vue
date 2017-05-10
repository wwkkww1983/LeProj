<template>
	<div class="registry-panel">
		<p>
      用户注册<span>已有账号，去<a style="color:#4fdcca;cursor:pointer;" @click="gotoLogin">登录</a></span>
    </p>
		<el-form class="registry-form">
			<el-form-item>
      	<el-input v-model="registryInfo.phoneNumber" placeholder="请输入手机号"></el-input>
      </el-form-item>
      <el-form-item>
      	<el-input v-model="registryInfo.SMSCode" placeholder="验证码">
      		<el-button v-if="sended" slot="append" style="width:115px">已发送({{subsub<10? '0'+subsub : subsub}}s)</el-button>
      		<el-button v-else slot="append" style="color:#4fdcca;width:115px" @click.native="handleRegIconClick">发送验证码</el-button>
      	</el-input>
      </el-form-item>
      <el-form-item style="position: relative;">
      	<el-input v-model="registryInfo.password" type="password" placeholder="创建密码"></el-input>
        <span style="position: absolute; bottom: 0px; right: -40px;">必填</span>
      </el-form-item>
      <el-form-item style="position: relative; display: inline;">
      	<el-input v-model="registryInfo.name" placeholder="创建姓名"></el-input>
        <span style="position: absolute; bottom: 0px; right: -40px;">必填</span>
      </el-form-item>
      <div style="padding-top:20px;"><span class="radio" v-bind:class="{'active': isRemember}" @click="handleRemember">&nbsp;阅读并同意《<router-link to="/service" style="color:#42a8ff;">用户服务协议</router-link>》和《<router-link to="/private" style="color:#42a8ff;">隐私协议</router-link>》</span></div>
      <el-form-item>
        <el-button native-type = "button" type="primary" size="large" class="btn-registry" @click="ajaxRegister">注册</el-button>
      </el-form-item>
		</el-form>
	</div>
</template>

<script>
import {userRegistry, getVcCode, confirmCode} from '../../../Api/auth_v1'

export default {
  data () {
    return {
      registryInfo: {
        phoneNumber: '',
        SMSCode: '',
        password: '',
        name: ''
      },
      sended: false,
      subsub: 60,
      isRemember: false
    }
  },
  methods: {
    handleRemember (ev) {
      this.isRemember = !this.isRemember;
    },
    gotoLogin (ev) {
      this.$emit('goto-login', true);
    },
    handleRegIconClick () {
      let data = {
        phone: this.registryInfo.phoneNumber,
        type: '01'
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
    ajaxRegister () {
      let data = {
        'phone': this.registryInfo.phoneNumber,
        'password': md5(this.registryInfo.password),
        'SMSCode': this.registryInfo.SMSCode,
        'username': this.registryInfo.name
      }
      let data_before = {
        'phone': this.registryInfo.phoneNumber,
        'SMSCode': this.registryInfo.SMSCode,
        'type': '01'
      };
      let self = this;
      confirmCode(data_before).then((res) => {
        if (!(/^1[34578]\d{9}$/.test(data.phone))) {
          alert("请正确输入有效手机号.");
        } else {
          if (this.registryInfo.SMSCode < 1000 || this.registryInfo.SMSCode.length >= 10000) {
            alert("请正确输入验证码");
            console.log(this.forgetInfo.SMSCode);
            return;
          }
          if (this.registryInfo.password === '' || this.registryInfo.password.length < 6 || this.registryInfo.password.length > 32) {
            alert("密码长度为6-32个字符");
            return;
          }
          if (this.registryInfo.name === '' || this.registryInfo.name.length < 3 || this.registryInfo.name.length > 30) {
            alert("用户名长度为3-30个字符");
            return;
          }
          if (res.data.state === 200) {
            userRegistry(data).then((res) => {
              if (res.data.state === 200) {
                sessionStorage.setItem('userId', res.data.userId);
                sessionStorage.setItem('companyId', res.data.companyId)
                self.$router.push('/work/');
              } else {
                alert('尚未注册成功，请重试');
              }
            });
          } else {
            alert("验证短信码失败");
          }
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
    position: relative;
    &::before {
      content: '';
      position: absolute;
      top: 50%;
      left: -20px;
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
    position: relative;
    &::before {
      content: '';
      position: absolute;
      top: 50%;
      left: -20px;
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
	.registry-panel{
		p{
			position: relative;
			text-align: center;
			color: #46c4b4;
			font-size: 20px;
			height: 70px;
			line-height: 70px;
			margin-top: 20px;
			&::after{
				content: '';
				position: absolute;
				width: 600px;
				height: 1px;
				bottom: 0px;
				left: 70px;
				background-color: #46c4b4;
			}
      span {
        position: absolute;
        top: 50%;
        right: 70px;
        margin-top: -35px;
        font-size:14px;
        color: #333;
      }
		}
	}
	.registry-form {
		width: 370px;
		margin: 0 auto;
		padding-top: 40px;
		input {
			padding-left: 20px;
			height: 50px;
		}
    div {
      text-align: center;
    }
    .btn-registry {
      padding:0px;
      width:100%;
      height:50px;
      line-height:50px;
      background:#4fdcca;
      text-align:center;color:#fff;
      font-size:18px;
      margin-top:30px;
      border: none;
    }
    div.el-form-item__content {
      line-height: 14px;
    }
  }
</style>