<template>
    <div>
        <div class="pageOne" style="width: 200px;margin: 60px;">
            <div>       
                <i class="phoneIcon"></i>
                <el-input type="text" v-model="phones" class="list-item" placeholder="请输入新手机号"></el-input>
            </div>
            <div style="margin-top: 20px">          
                <el-input v-model="codes" placeholder="验证码输入" class="phone-number"></el-input>
                <div style="margin-top: 20px" href="javascript:void(0)"  class="list-code" @click="ajaxCaptcha">获取验证码</div>
            </div>
            <div style="margin-top: 20px">   
                <el-button class="waves-effect waves-light btn" @click="confirm" type="primary">下一步</el-button>
            </div>
        </div>
    </div>
</template>
<script>
import {getVcCode, validateCode} from '../../Api/auth'
export default {
    data () {
        return {
            phones: null, // 手机号
            codes: null // 验证码
        }
    },
    methods: {
        // 获取验证码
        ajaxCaptcha () {
            let _self = this;
            if (!this.phones) {
                this.$alert('手机号码未输入', '提示信息', {
                    confirmButtonText: '确定'
                });
                return;
            }
            let data = {
                phone: this.phones,
                type: 'GET_LOGIN_CODE'
            }
            console.log(data);
            getVcCode(data).then((res) => {
                console.log("获取验证码")
                console.log(res);
                if (res.data.state === 200) { // 通过得到验证码
                    console.log(res);
                    _self.autoCode = res.data.msg + '';
                } else if (res.data.state === 40000) { // 手机号已经被注册
                    _self.autoCode = '1';
                }
            }, (err) => {
                _self.autoCode = '0'
                console.log(err);
            })
        },
        confirm () { // 确定
            let _self = this;
            let data = {
                "phone": this.phones, // 手机号码
                "code": this.codes, // 手机验证码
                "uid": JSON.parse(sessionStorage.getItem("uid")).uid,
                "type": JSON.parse(sessionStorage.getItem("uid")).type // 1:qq登录 2:微信登录
            }
            validateCode(data).then((response) => {
                console.log("绑定手机号");
                console.log(data);
                console.log(response);
                // 本地存储
                sessionStorage.setItem("id", response.data.data.id)
                sessionStorage.setItem("sig", response.data.data.sig)
                sessionStorage.setItem('tokenId', response.data.data.tokenId)
                sessionStorage.setItem('companyId', response.data.data.companyId)
                // 登录IM
                loginInfo.identifier = response.data.data.id
                loginInfo.identifierNick = response.data.data.nickName
                loginInfo.userSig = response.data.data.sig
                loginInfo.headurl = response.data.data.avatarUrl
                _self.$router.push('/')
            }, (response) => {
                console.log(response);
            })
        }
    }
}
</script>
<style lang="scss" scoped>
/*第一步*/
.pageOne {
  margin-top: 86px;
  height: 260px;
  font-size: 14px;
  .list {
    padding: 0;
    width: 350px;
    height: 50px;
    position: relative;
    margin-bottom: 20px;
    .list-item {
      height: 50px;
      input {
        position: absolute;
        left: 0px;
        width: 350px;
        padding-left: 50px;
        height: 50px;
        line-height: 50px;
        border: 1px solid #d6d6d6;
        font-size: 16px;
        outline: none;
        box-shadow: none;
      }
    }
    p {
      font-size: 0;
      height: 0;
      color: red;
    }
    i {
      position: absolute;
      top: 15px;
      display: block;
      width: 22px;
      height: 22px;
      z-index: 8;
      cursor: pointer;
    }
    .phoneIcon {
      left: 17px;
      background: url("../../assets/img/signup/phoneIcon.png") no-repeat center center;
    }
    .passwordIcon {
      left: 17px;
      background: url("../../assets/img/signup/passwordIcon.png") no-repeat center center;
    }
    .passwordIconShow {
      right: 8px;
      background: url("../../assets/img/signup/passwordShow.png") no-repeat center center;
    }
    .passwordIconNone {
      cursor: pointer;
      left: 316px;
      background: url("../../assets/img/signup/passwordNone.png") no-repeat center center;
    }
  }
  .list:nth-child(2) {
    position: relative;
    .el-input {
      width: 212px;
      height: 50px;
      float: left;
    }
    input {
      position: absolute;
      left: 0;
      height: 50px;
      margin-top: 0;
      padding-left: 46px;
    }
    .list-code {
      float: left;
      font-size: 16px;
      line-height: 50px;
      width: 128px;
      height: 50px;
      border: 1px solid #C0CCDA;
      text-align: center;
      margin-left: 10px;
      border-radius: 3px;
      color: red;
      cursor: pointer;
    }
  }
  .list:nth-child(4) {
    text-align: center;
    line-height: 8px;
    overflow: hidden;
    border: none;
    margin-top: 30px;
    button {
      width: 100%;
      background: #9ad3cd;
      border: none;
    }
  }
  .list:nth-child(5) {
    text-align: center;
    margin-top: 20px;
    background-color: red;
    color: #fff;
    cursor: pointer;
  }
}
</style>