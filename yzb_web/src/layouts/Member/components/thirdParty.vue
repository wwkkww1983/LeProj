<template>
  <div class="form-3">
    <div class="info-1"><span></span>其他登录方式<span></span></div>
    <div class="item-2">
      <span id="qq_login_btn" class="qqIcon" @click="QQLogin"></span>
      <a href="https://open.weixin.qq.com/connect/qrconnect?appid=wx3c7025375b63932c&redirect_uri=http%3a%2f%2flocalhost%3a80&response_type=code&scope=snsapi_login&state=STATE#wechat_redirect"
        class="wxIcon"></a>
    </div>
  </div>
</template>
<script>
import {loginByOther} from '../../../Api/thirdParty.js'
  // import {qqLogin, getQqInfo} from '../../../Api/thirdParty.js'
  export default {
    methods: {
        judge (parm) {
            loginByOther(parm).then((response) => { // 检测是否注册
                console.log("微信绑定");
                console.log(response);
                if (response.data.state === 99999) { // 未绑定手机
                    sessionStorage.setItem("uid", JSON.stringify(parm));
                    this.$router.push("/member/bindingPhone");
                } else {
                  console.log("存本地");
                    console.log(response);
                    // 存本地
                    sessionStorage.setItem("id", response.data.data.id);
                    sessionStorage.setItem("sig", response.data.data.sig);
                    sessionStorage.setItem('tokenId', response.data.data.tokenId);
                    sessionStorage.setItem('companyId', response.data.data.companyId);
                    // QQ登录
                    loginInfo.identifier = response.data.data.id;
                    loginInfo.identifierNick = response.data.data.nickName;
                    loginInfo.userSig = response.data.data.sig;
                    loginInfo.headurl = response.data.data.avatarUrl;
                    this.$router.push('/');
                    console.log("绑定了")
                }
                console.log("检测是否注册");
                console.log(response.data.data);
            }, (response) => {
                console.log("检测是否注册请求失败");
                console.log(response);
            })
        },
        QQLogin() {
            console.log("QQdenglu")
            let _self = this;
            QC.Login.showPopup({ // 直接打开QQ登录弹窗
                appId: "101350019"
            })
            // 调用QC.Login方法，指定btnId参数将按钮绑定在容器节点中
            QC.Login({}, function(reqData, opts) {
                if (QC.Login.check()) { // 检测是否登录成功!
                    console.log("登录成功")
                    // 获取当前登录用户的Access Token以及OpenID
                    QC.Login.getMe(function(openId, accessToken) {
                        let data = {
                            "uid": openId, // 第三方的uid
                            "type": 1 // 1:qq登录 2:微信登录
                        }
                        _self.judge(data);
                    })
                }
            }, function(error) {
              alert(error);
            })
        }
    },
    mounted() {
        const _self = this;
        // 如果已经进行过微信扫码则在url中会生成一个code参数
        if (window.location.search.includes('code')) {
            // 拿到用户的code
            var userCode = window.location.search.slice(6, window.location.search.length - 12);
            let URL = INTERFACE_URL + '/v1/weixinApp/getAccessToken/'
            // let URL = "http://192.168.100.105:8080/yunzhubao/v1/weixinApp/getAccessToken";
            let data = {
                "code": userCode
            }
            console.log(userCode);
            // 在微信开发平台,通过code获取access_token
            _self.$http.post(URL, data).then((response) => { // 获取授权
                if (response.data.openid) {
                    let data = {
                        "uid": response.data.openid, // 第三方的uid
                        "type": 2 // 1:qq登录 2:微信登录
                    }
                    this.judge(data);
                }
            }, (response) => {
                console.log(response)
            })
        }
    }
    // mounted() {
    //   alert("mounted");
    //   const _self = this
    //   let appInfo = { // 第三方获得的info
    //     type: null, // 1表示qq，2表示微信
    //     uid: null
    //   }
    //   if (window.location.search.includes('code')) { // 如果已经进行过微信扫码则在url中会生成一个code参数

    //     var userCode = window.location.search.slice(6, window.location.search.length - 12)

    //     var getAccessToken = INTERFACE_URL + '/v1/weixinApp/getAccessToken/' + userCode // 获取授权
    //     _self.$http.get(getAccessToken).then((response) => {

    //       const weChatLoginCheckUrl = INTERFACE_URL + '/v1/auth/loginByOther' // 发送到企业服务器验证地址
    //       var openId = response.data.openid // 获取用户唯一标识符
    //       const refreshToken = response.data.refresh_token // 用户用来刷accees_token

    //       var weChatUserInfo = {
    //           "uid": openId,
    //           "type": 2
    //         } // 发送到企业服务器用户验证信息

    //       _self.$http.post(weChatLoginCheckUrl, weChatUserInfo).then((response) => { // 查询是否已注册

    //         if (response.data.state === 99999) { // 未绑定手机号

    //           appInfo = weChatUserInfo
    //           sessionStorage.setItem('appInfo', JSON.stringify(appInfo))

    //           _self.$http.get(INTERFACE_URL + `/v1/weixinApp/userinfo/${refreshToken}/${openId}`)
    //             .then(response => {
    //               // 获取微信资料
    //               const jsonData = response.data.data.userInfo
    //               sessionStorage.setItem('userInfo', jsonData)
    //               _self.$router.push('/member/signup')
    //             })

    //         } else if (response.data.state === 200) { // 已绑定手机号
    //           // sessionStorage.setItem("id", response.data.data.id)
    //           // sessionStorage.setItem("sig", response.data.data.sig)
    //           // sessionStorage.setItem('tokenId', response.data.data.tokenId)
    //           // sessionStorage.setItem('companyId', response.data.data.companyId)
    //           // loginInfo.identifier = response.data.data.id
    //           // loginInfo.identifierNick = response.data.data.nickName
    //           // loginInfo.userSig = response.data.data.sig
    //           // loginInfo.headurl = response.data.data.avatarUrl
    //           this.$store.dispatch('getUserInfo')
    //           _self.$router.push('/')
    //         } else {

    //           alert('未知错误!')

    //         }
    //       }, (error) => {
    //         alert(error)
    //       });

    //     }, (error) => {

    //       alert(error)

    //     })
    //   }
    // }
  }
</script>

<style lang="scss" scoped>
  #qq_login_btn {
    cursor: pointer
  }

  .form-3 {
    .info-1 {
      margin-bottom: 20px;
      width: 100%;
      text-align: center;
      font-size: 14px;
      color: #999999;
      line-height: 1;
      span {
        margin-top: 6px;
        float: left;
        width: 102px;
        height: 1px;
        background: #d7d7d7;
        display: inline-block;
      }
      span:nth-of-type(2) {
        float: right;
      }
    }
    .item-2 {
      padding-top: 20px;
      width: 100%;
      text-align: center;
      .qqIcon,
      .wxIcon {
        margin-right: 35px;
        width: 30px;
        height: 30px;
        display: inline-block;
        background: url(../../../assets/img/login/logoQq.png) no-repeat;
        overflow: hidden;
      }
      .qqIcon {
        a {
          display: block;
          height: 100%;
        }
        img {
          display: none
        }
      }
      .wxIcon {
        background: url(../../../assets/img/login/wxIcon.png) no-repeat;
      }
    }
  }
</style>