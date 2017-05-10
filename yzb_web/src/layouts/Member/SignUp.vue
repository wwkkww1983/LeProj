<template>
    <div class="sigup-container">
        <div class="signUpInner">
            <header>
                <ul>
                    <li :class="{ 'stepOne': true,'active':pages.pageOne}">1.手机注册<i></i></li>
                    <li :class="{ 'stepTwo': true,'active':pages.pageTwo}">2.完善资料<i></i><i></i></li>
                    <li :class="{ 'stepThree': true,'active':pages.pageThree}">3.注册成功<i></i></li>
                </ul>
            </header>
            <section>
                <el-form :model="ruleForm2" ref="ruleForm2"  :rules="rules2">
                    <!-- 第一步 -->
                    <div v-show="pages.pageOne"  class="pageOne">
                        <el-form-item prop="phone" class="list">
                            <i class="phoneIcon"></i>
                            <el-input type="text" v-model="ruleForm2.phone" class="list-item" placeholder="请输入新手机号"></el-input>
                        </el-form-item>
                        <el-form-item prop="code" class="list">
                            <el-input v-model="ruleForm2.code" placeholder="验证码输入" class="phone-number"></el-input>
                            <div href="javascript:void(0)"  class="list-code" @click="ajaxCaptcha">获取验证码</div>
                        </el-form-item>
                        <el-form-item prop="password" class="list">
                            <i class="passwordIcon"></i>
                            <el-input type="password" v-model="ruleForm2.password" v-show="ruleForm2.newPasswordIcon" placeholder="请输入新密码"  class="list-item"></el-input>
                            <el-input type="text" v-model="ruleForm2.password" v-show="!ruleForm2.newPasswordIcon" placeholder="请输入新密码" class="list-item"></el-input>
                            <i @click="passwordIconCut" :class="{'passwordIconShow':!ruleForm2.newPasswordIcon,'passwordIconNone':ruleForm2.newPasswordIcon}"></i>
                        </el-form-item>
                        <el-form-item class="list">
                            <el-button class="waves-effect waves-light btn" @click="pageOneNext" type="primary">下一步</el-button>
                        </el-form-item>
                    </div>
                    <!-- 第二步 -->
                    <div class="pageTwo" v-show="pages.pageTwo">
                        <h3>请完善个人资料，填写真实姓名，</h3>
                        <h3>方便公司内部沟通。</h3>
                        <div class="userphone">
                            <input type="file" name="file" @change="addFile($event)"/>
                            <img :src="userInfo.accountPicture">
                        </div>
                        <ul>
                            <li>
                                <label>姓名</label>
                                <el-input v-model = "userInfo.sppNickSigs" placeholder="例如：李晨"></el-input>
                            </li>
                            <li class="waves-effect waves-light btn" @click="ajaxRegister">完成</li>
                        </ul>
                    </div>
                    <!-- 第三步 -->
                    <div class="pageThree" v-show="pages.pageThree">
                        <h3>恭喜您！注册完成！</h3>
                        <div class="succeed"></div>
                        <div class="homePage waves-effect waves-light btn" @click="pageThreeNext">进入首页</div>
                    </div>
                </el-form>
            </section>
        </div>
    </div>
</template>

<script>
  import {userReg, getVcCode} from '../../Api/auth'
  export default {
    data () {
      const validatephoss = (rule, value, callback) => { // 手机号码验证
        var obj = /^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/.test(value);
        if (!obj) {
          callback(new Error("格式错误，请正确输入手机号码"));
        } else {
          callback();
        }
      }
      const validateautoCode = (rule, value, callback) => { // 验证码验证
        if (this.autoCode === '0') {
          callback(new Error('获取验证码失败，请重新拉取'))
        } else if (this.autoCode === '1') {
          callback(new Error('手机号已经被注册'))
        } else if (value === this.autoCode) {
          callback();
        } else {
          callback(new Error('验证码错误'))
        }
      }
      const validatepassword = (rule, value, callback) => { // 密码验证
        var obj = /^[A-Za-z]+\w+$/.test(value)
        if (!obj) {
          callback(new Error("密码第一个字符需要是字母"));
        } else {
          callback();
        }
      }
      return {
        ruleForm2: {
          "phone": "", // 用户名
          "password": "", // 密码
          "code": "", // 验证码
          "newPasswordIcon": true, // 当前密码是否可见
          "age": ""
        },
        userInfo: {
          "sppNickSigs": "", // 用户名昵称
          "accountPicture": "", // 用户头像
          "localImgUrl": require("../../assets/img/signup/userphone.png"), // 本地图片预览
          "uid": "", // 第三方应用id
          "type": "" // 第三方应用类型，1为qq,2微信
        },
        autoCode: "", // 验证码
        pages: { // 步骤切换
          pageOne: true,
          pageTwo: false,
          pageThree: false
        },
        rules2: {
          phone: [{
            required: true,
            message: '账号不能为空',
            trigger: 'blur'
          }, {
            validator: validatephoss
          }],
          code: [{
            required: true,
            message: '验证码不能为空',
            trigger: 'blur'
          }, {
            validator: validateautoCode
          }],
          password: [{
            required: true,
            message: '密码不能为空',
            trigger: 'blur'
          }, {
            validator: validatepassword
          }]
        }
      }
    },
    methods: {
      // 获取验证码
      ajaxCaptcha () {
        let userAccount = this.ruleForm2.phone;
        let _self = this
        if (!userAccount) {
          this.$alert('手机号码未输入', '提示信息', {
            confirmButtonText: '确定'
          });
          return;
        }
        let data = {
          phone: userAccount,
          type: 'GET_REGISTER_CODE'
        }
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
      // 密码框状态改变
      passwordIconCut() {
        this.ruleForm2.newPasswordIcon = !this.ruleForm2.newPasswordIcon;
      },
      // 注册步骤一
      pageOneNext() {
        this.$refs.ruleForm2.validate((valid) => {
          if (valid) {
            this.pages.pageOne = false;
            this.pages.pageTwo = true;
          }
        });
      },
      addFile(evnt) { // 192.168.5.110:9081,上传图片
        var obj = event.currentTarget; // 事件属性返回其监听器触发事件的节点
        var reader = new FileReader(); // 读取本地文件对象
        reader.readAsDataURL(obj.files[0]); // 读取参数里的文件，当读取成功会触发onload及loadend
        var _self = this;
        reader.onload = function() { // 当文件读取成功
          _self.userInfo.localImgUrl = this.result; // this.result获得文件数据
        };
        var formdata = new FormData(); // 创建表单数据对象(本地上传服务器文件对象)
        this.$http.options.emulateJSON = true; // 请求编码为application / json,方便后台读取文件
        formdata.append("file", obj.files[0]); // 将文件对象append进去
        this.$http.post(INTERFACE_URL + "/v1/user/uploadNoTokenId", formdata).then((response) => {
          console.log("上传图片");
          console.log(formdata);
          if (response.data.state === 200) { // 上传图片成功，后台返回图片链接地址，将地址赋值
            this.userInfo.accountPicture = response.data.data;
          } else {
            alert('图片上传失败 ' + response.data.state)
          }
        }, (response) => {
          console.log(response);
        });
      },
      // 注册步骤二
      ajaxRegister() {
        let RegisterData = {
          "avatarUrl": this.userInfo.accountPicture, // 头像
          "code": this.ruleForm2.code, // 验证码
          "nickName": this.userInfo.sppNickSigs, // 昵称
          "password": md5(this.ruleForm2.password), // 密码
          "phone": this.ruleForm2.phone, // 手机号
          "type": this.userInfo.type, // -----1:qq注册 2:微信注册 6666：其他注册
          "uid": this.userInfo.uid // --如何type不是1或者2的时候,填写null或者不填
        }
        userReg(RegisterData).then((res) => {
          console.log("注册第二步");
          console.log(res);
          if (res.data.state === 200) {
            sessionStorage.setItem('id', res.data.data.id);// 用户id
            sessionStorage.setItem('tokenId', res.data.data.tokenId)
            loginInfo.identifier = sessionStorage.getItem('id')
            loginInfo.userSig = sessionStorage.getItem('sig')
            this.pages.pageTwo = false
            this.pages.pageThree = true
          } else {
            alert('注册失败')
          }
          console.log(res);
        }, (err) => {
          alert('服务器请求失败')
          console.log(err);
        })
      },
      // 注册步骤三
      pageThreeNext() {
        this.$router.push("/");
      }
    },
    mounted() {
      const userInfoStr = sessionStorage.getItem('userInfo')
      const appInfoStr = sessionStorage.getItem('appInfo')
      console.log(userInfoStr)
      if (userInfoStr) {
        var userInfo = JSON.parse(userInfoStr)
        var appInfo = JSON.parse(appInfoStr)

        console.log(userInfo)
          // 第三方获取数据
        this.userInfo.sppNickSigs = userInfo.nickname
        this.userInfo.accountPicture = userInfo.headimgurl
        this.userInfo.localImgUrl = userInfo.headimgurl
        this.userInfo.uid = appInfo.uid
        this.userInfo.type = appInfo.type
      }
    }
  }
</script>
<style lang="scss">
  @import '../../assets/css/_mixin.scss';

  $green: #0abfab;
  $gray: #edf1f6;
  .sigup-container {
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    margin: auto;
    height: 600px;
    width: 800px;
    box-shadow: 0 0 10px #616161;
    .signUpInner {
      position: absolute;
      left: 0;
      right: 0;
      margin: auto;
      height: 386px;
      width: 600px;
      margin-top: 90px;
      header {
        ul {
          margin-bottom: 0;
          .active {
            background-color: $green;
            color: #fff;
          }
          li {
            font-size: 16px;
            position: relative;
            float: left;
            width: 200px;
            height: 40px;
            line-height: 40px;
            text-align: center;
            background-color: $gray;
            a {
              position: absolute;
              display: block;
              height: 20px;
              left: 0;
              right: 0;
              top: 0;
              bottom: 0;
              margin: auto;
              text-align: center;
              color: #000;
            }
          }
          .stepOne {
            i {
              @include triangle(right, #fff, transparent);
            }
          }
          .stepTwo {
            i:nth-child(1) {
              @include triangle(left, transparent, #fff);
            }
            i:nth-child(2) {
              @include triangle(right, #fff, transparent);
            }
          }
          .stepThree {
            i {
              @include triangle(left, transparent, #fff);
            }
          }
        }
      }
      section {
        position: absolute;
        left: 0;
        right: 0;
        top: 40px;
        margin: auto;
        width: 350px;
        height: 346px;
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
              color: $green;
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
            background-color: $green;
            color: #fff;
            cursor: pointer;
          }
        }
        //第二步
        .pageTwo {
          margin-top: 40px;
          height: 260px;
          position: relative;
          h3 {
            height: 20px;
            font-size: 18px;
            text-align: center;
            margin-top: 5px;
            margin-bottom: 0;
          }
          .userphone {
            left: 0;
            right: 0;
            margin: auto;
            width: 90px;
            height: 90px;
            border-radius: 50%;
            overflow: hidden;
            margin-top: 30px;
            position: relative;
            .el-input {
              opacity: 0
            }
            input {
              width: 90px;
              height: 90px;
              font-size: 0;
              outline: none;
              cursor: pointer;
              border-bottom: 1px solid #d4d4d4;
            }
            img {
              width: 90px;
              height: 90px;
              position: absolute;
              border-radius: 50%;
              top: 0;
              z-index: -1;
            }
          }
          ul {
            position: absolute;
            left: 0;
            right: 0;
            margin: auto;
            width: 100%;
            height: 40px;
            margin: 0;
            margin-top: 40px;
            li:nth-child(1) {
              position: relative;
              width: 100%;
              height: 40px;
              line-height: 40px;
              border-bottom: 1px solid $gray;
              label {
                line-height: 40px;
                color: #000;
                display: inline;
                position: absolute;
                left: 0px;
                top: 0px;
                z-index: 3;
              }
              input {
                position: absolute;
                left: 0;
                width: 100%;
                height: 40px;
                background: none;
                border-bottom: 1px solid #d4d4d4;
                line-height: 40px;
                border: none;
                font-size: 16px;
                text-align: center;
              }
            }
            li:nth-child(2) {
              width: 350px;
              height: 50px;
              line-height: 50px;
              padding: 0;
              text-align: center;
              color: #fff;
              background-color: $green;
              border: none;
              cursor: pointer;
              margin-top: 30px;
            }
          }
        }
        .pageThree {
          margin-top: 60px;
          position: relative;
          h3 {
            height: 20px;
            font-size: 18px;
            text-align: center;
            margin-top: 5px;
            margin-bottom: 0;
          }
          .succeed {
            left: 0;
            right: 0;
            margin: auto;
            width: 90px;
            height: 90px;
            border-radius: 50%;
            overflow: none;
            margin-top: 30px;
            background: url("../../assets/img/signup/succeed.png") no-repeat center center;
          }
          .homePage {
            position: absolute;
            left: 0;
            right: 0;
            margin: auto;
            width: 218px;
            height: 40px;
            text-align: center;
            color: #fff;
            line-height: 40px;
            background-color: $green;
            border: none;
            margin-top: 96px;
            border-radius: 50%;
          }
        }
      }
    }
  }
</style>