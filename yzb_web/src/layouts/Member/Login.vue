<template>
    <div class="login">
        <div class="header">
            <img src="../../assets/img/logo.png" alt="">
        </div>
        <div class="main">
            <div v-if="isRegistry" class="block_registry">
                <ForgetPanel v-if="isForget" @goto-login="gotoLogin"></ForgetPanel>
                <RegistryPanel v-else @goto-login="gotoLogin"></RegistryPanel>
            </div>
            <div v-else class="block">
                <div class="form">
                    <div class="form_head"><span class="login_with_pw" v-bind:class="{ active: isCurrent == 'pw' }" @click="handlePwClick">密码登录</span><span class="login_with_qr" v-bind:class="{ active: isCurrent == 'qr' }" @click="handleQrClick">验证码登录</span></div>
                    <div>
                        <LoginPanel :is-current="isCurrent" @goto-registry="gotoRegistry" @goto-forget="gotoForget"></LoginPanel>
                    </div>
                </div>
                <Qrcode></Qrcode>
            </div>
        </div>
        <div class="footer">
            <div class="link_wrap">
                <router-link to="/about">关于金掌门</router-link>|
                <router-link to="/private">隐私协议</router-link>|
                <router-link to="/service">服务条款</router-link>
            </div>
        </div>
    </div>
</template>

<script>
    import LoginPanel from './components/LoginPanel'
    import ThirdParty from './components/ThirdParty'
    import RegistryPanel from './components/RegistryPanel'
    import ForgetPanel from './components/ForgetPanel'
    import Qrcode from './components/Qrcode'
    import {qrcodeLogin, userLogin} from '../../Api/auth_v1'

    export default {
        data() {
            return {
                isCurrent: 'pw',
                isRegistry: false,
                isForget: false,
                wsUri: "ws://192.168.100.110:8082/yunzhubao/websocket",
                qr: '',
                qrCodeUrl: '',
                sessionId: '',
                loginKey: '',
                webSocketFirstMsg: '',
                reConnectFlag: 5
            };
        },
        methods: {
            handlePwClick () {
                this.$data.isCurrent = 'pw';
            },
            handleQrClick () {
                this.$data.isCurrent = 'qr';
            },
            gotoRegistry (val) {
                this.$data.isRegistry = true;
                this.$data.isForget = false;
            },
            gotoForget (val) {
                this.$data.isRegistry = true;
                this.$data.isForget = true;
            },
            gotoLogin (val) {
                this.$data.isRegistry = false;
                this.$data.isForget = false;
            },
            updateQrCodeLogin () {
                qrcodeLogin({}).then((res) => {
                    this.loginKey = res.data.data.split("key=", 13)[1];
                    this.qrCodeUrl = res.data.data + '&sessionId=' + this.sessionId;
                    this.qr = new QRious({
                      element: document.getElementById('qrious'),
                      size: 250,
                      value: this.qrCodeUrl
                    });
                });
            },
            createWebSocket () {
                let webSocket = new WebSocket(this.wsUri);
                let self = this;
                webSocket.onopen = function (evt) {
                    let t = new Date();
                    let timeStr = (t.getHours() < 10 ? '0' + t.getHours() : t.getHours()) + ':' + (t.getMinutes() < 10 ? '0' + t.getMinutes() : t.getMinutes());
                    console.log(timeStr + ' websocket Opened.');
                };
                webSocket.onerror = function (evt) {
                    let t = new Date();
                    let timeStr = (t.getHours() < 10 ? '0' + t.getHours() : t.getHours()) + ':' + (t.getMinutes() < 10 ? '0' + t.getMinutes() : t.getMinutes());
                    console.log(timeStr + ' happend Error. Try be reconnect..');
                    if (this.reConnectFlag >= 0) {
                        setTimeout(self.createWebSocket, 1000);
                        --this.reConnectFlag;
                    }
                };
                webSocket.onmessage = function (evt) {
                    if (evt.data.length === 36) {
                        self.sessionId = evt.data;
                        self.updateQrCodeLogin();
                        setInterval(self.updateQrCodeLogin, 60000);
                    } else {
                        let data = {
                            userId: evt.data,
                            key: self.loginKey,
                            operateType: '3'
                        };
                        userLogin(data).then((res) => {
                            if (res.data.state === 200) {
                                console.log(res.data);
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
                };
                webSocket.onclose = function (evt) {
                    let t = new Date();
                    let timeStr = (t.getHours() < 10 ? '0' + t.getHours() : t.getHours()) + ':' + (t.getMinutes() < 10 ? '0' + t.getMinutes() : t.getMinutes());
                    console.log(timeStr + ' Server Closed the connect. Will be reconnect..');
                    if (this.reConnectFlag >= 0) {
                        setTimeout(self.createWebSocket, 1000);
                        --this.reConnectFlag;
                    }
                };
            }
        },
        components: {
            LoginPanel,
            Qrcode,
            ThirdParty,
            RegistryPanel,
            ForgetPanel
        },
        mounted () {
            this.createWebSocket();
        },
        updated () {
            this.updateQrCodeLogin();
        }
    };
</script>

<style lang="scss">
    .header{
        width: 100%;
        height: 90px;
        position: relative;
        img{
            position: absolute;
            top: 50%;
            left: 50%;
            margin-top: -20px;
            margin-left: -350px;
        }
        span{
            margin-left: 16px;
            color: #999999;
            font-size: 16px;
        }
    }
    .main{
        width: 100%;
        height: 740px;
        background: url(../../assets/img/bg.png) 0 0 repeat-x;
        position: relative;
        .block_registry {
            position: absolute;
            width: 740px;
            height: 580px;
            top: 130px;
            left: 50%;
            margin-left: -370px;
            box-shadow: 0 5px 10px 0 rgba(0, 0, 0, 0.12), 0 5px 10px 0 rgba(0, 0, 0, 0.12);
            border-radius: 10px;
            background-color: #fff;
            opacity: 0.9;
        }
        .block{
            position: absolute;
            width: 740px;
            height: 420px;
            top: 130px;
            left: 50%;
            margin-left: -370px;
            box-shadow: 0 5px 10px 0 rgba(0, 0, 0, 0.12), 0 5px 10px 0 rgba(0, 0, 0, 0.12);
            border-radius: 10px;
            background-color: #fff;
            opacity: 0.97;
            div.form{
                width: 440px;
                height:420px;
                position: absolute;
                top: 0;
                left: 0;
                padding: 30px 45px 0px 45px;
                &::after {
                    content: '';
                    background-color: #d6d6d6;
                    position: absolute;
                    width: 1px;
                    height: 320px;
                    top: 50px;
                    right: 0px;
                }
                .form_head{
                    color: #999;
                    width: 84%;
                    height: 50px;
                    font-size: 18px;
                    margin: 0 auto;
                    border-bottom: 1px solid #d6d6d6;
                    padding: 0px 20px;
                    margin-bottom: 40px;
                    position: relative;
                    span {
                        cursor: pointer;
                    }
                    .login_with_pw{
                        float: left;
                        line-height: 40px;
                    }
                    .login_with_pw.active{
                        color: #4fdcca;
                        &::before{
                            content: '';
                            position: absolute;
                            left: 0px;
                            bottom: 0px;
                            width: 110px;
                            height: 4px;
                            background-color: #4fdcca;
                        }
                    }
                    .login_with_qr {
                        float: right;
                        line-height: 40px;
                    }
                    .login_with_qr.active{
                        color: #4fdcca;
                        &::before{
                            content: '';
                            position: absolute;
                            right: 0px;
                            bottom: 0px;
                            width: 130px;
                            height: 4px;
                            background-color: #4fdcca;
                        }
                    }
                }
            }
            div.qrCode{
                padding: 45px;
                width: 300px;
                height:420px;
                position: absolute;
                top: 0px;
                right: 0px;
                p{
                    text-align: center;
                    img{
                        width: 210px;
                        height: 210px;
                        /* border: 1px solid #ccc;
                        border-radius: 8px; */
                    }
                }
                p:nth-child(1){
                    padding: 22px 0px;
                    font-size: 18px;
                    text-align: left;
                    color: #666;
                    height: 60px;
                }
                p:nth-child(2){
                    padding: 10px 0px;
                }
                p:nth-child(3){
                    padding: 8px 0px;
                    font-size: 14px;
                    color: #666;
                    height: 50px;
                }
            }
        }
    }
    .footer{
        width: 100%;
        height: 130px;
        background-color: #333333;
        color: #fff;
        padding-top: 40px;
        .link_wrap{
            text-align: center;
        }
        a{
            margin: 0px 40px;
            font-size: 16px;
            color: #fff;
        }
    }
</style>