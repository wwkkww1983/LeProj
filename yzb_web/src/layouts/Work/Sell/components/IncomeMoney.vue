<template>
    <div class="income-money dialog-w540-h740-hn">
        <el-dialog title="" v-model="popups">
            <h3 class="header">销售结算<span @click="closePopup">X</span></h3>
            <div class="income-money-body">
                <div class="body-left">
                    <ul class="ipt-computed">
                        <li><i>总额</i><input readonly="true" type="text" v-model="allMonny"></li>
                        <li @click="openPopup('现金', 'cash')">
                            <i>Q 现金</i>
                            <input readonly="true" v-model="onlyMonny.cash" ref='xhCash' type="text">
                        </li>
                        <li @click="openPopup('刷卡', 'swipingCard')">
                            <i>W 刷卡</i>
                            <input readonly="true" v-model="onlyMonny.swipingCard" ref='xhSwipingCard' type="text">
                        </li>
                        <li @click="openPopup('微信', 'weChat')">
                            <i>E 微信</i>
                            <input readonly="true" v-model="onlyMonny.weChat" ref='xhWeChat' type="text">
                        </li>
                        <li @click="openPopup('支付宝', 'alipay')">
                            <i>R 支付宝</i>
                            <input readonly="true" v-model="onlyMonny.alipay" ref='xhAlipay' type="text">
                        </li>
                        <li @click="openPopup('支付宝', 'other')">
                            <i>T 其它</i>
                            <input readonly="true" v-model="onlyMonny.other" ref='xhOther' type="text">
                        </li>
                        <li><i>合计</i><input readonly="true" type="text" v-model="totalMonny"></li>
                        <li><i>找零</i><input readonly="true" type="text" v-model="changeMonny"></li>
                    </ul>
                    <!-- 输入金额弹窗 -->
                    <div class="popup-bg" v-if="popupIpt"></div>
                    <div class="popup-body" v-if="popupIpt">
                        <div class="popup-body-ipt">
                            <i>{{newmethod}}</i>
                            <input ref='xhIpt' type="text" v-model="iptNum" @keyup.enter="confirmIpt" placeholder="请输入金额">
                        </div>
                        <div class="operation-btn">
                            <a href="javascript:void(0)" @click="closePopupIpt">取消</a>
                            <a href="javascript:void(0)" @click="confirmIpt">确定</a>
                        </div>
                    </div>
                    <div class="keyboard-btn">
                        <ul class="keyboard-left">
                            <li @click="addValue(1)">1</li>
                            <li @click="addValue(2)">2</li>
                            <li @click="addValue(3)">3</li>
                            <li class="special-number" @click="addValue(1000)">1000</li>
                            <li @click="addValue(4)">4</li>
                            <li @click="addValue(5)">5</li>
                            <li @click="addValue(6)">6</li>
                            <li class="special-number" @click="addValue(2000)">2000</li>
                            <li @click="addValue(7)">7</li>
                            <li @click="addValue(8)">8</li>
                            <li @click="addValue(9)">9</li>
                            <li class="special-number" @click="addValue(5000)">5000</li>
                            <li @click="addValue(0)">0</li>
                            <li @click="addValue('00')">00</li>
                            <li @click="addValue('.')">.</li>
                            <li class="special-number" @click="addValue(10000)">10000</li>
                        </ul>
                        <ul class="keyboard-right">
                            <li @click="closeKeyboard"><img src="./../../../../assets/img/work/returnDel.png"></li>
                            <li @click="confirmBtn">确定</li>
                        </ul>
                    </div>
                    <ul class="body-bottom">
                        <li @click="restoreValue">清除</li>
                        <li>暂不结算</li>
                        <li>结束打单</li>
                        <li>打印单据</li>
                    </ul>
                </div>
                <!--<div class="body-right">
                    <ul>
                        <li @click="openPopup('现金', 'cash')">现金（Q）</li>
                        <li @click="openPopup('刷卡', 'swipingCard')">刷卡（W）</li>
                        <li @click="openPopup('微信', 'weChat')">微信（E）</li>
                        <li @click="openPopup('支付宝', 'alipay')">支付宝（R）</li>
                        <li @click="openPopup('支付宝', 'other')">其它（T）</li>
                    </ul>
                </div>-->
            </div>
        </el-dialog>
    </div>
</template>
<script>
// document.onkeydown = function (e) {
//     alert("键盘呗按下")
//     console.log(e);
// }
export default {
    props: [
        'incomeMoneyPopup', // 弹窗
        'receiptsIntroList' // 单据简介-销售
    ],
    data () {
        return {
            onlyMonny: { // 只读的金钱
                "cash": 0, // 现金
                "swipingCard": 0, // 刷卡
                "weChat": 0, // 微信
                "alipay": 0, // 支付宝
                "other": 0 // 其它
            },
            newKey: "", // 当前的key
            newBindingInput: "", // 当前绑定的值
            popups: false, // 弹窗
            popupIpt: false, // 输入金额弹窗
            iptNum: null, // 输入要收款的金钱
            newmethod: "", // 付款方式
            xiao: ""
        }
    },
    computed: {
        allMonny: function () { // 总额
            return Number(this.receiptsIntroList.salePrice) || 0 + Number(this.receiptsIntroList.recyclePrice) || 0 + Number(this.receiptsIntroList.exchangePrice) || 0;
        },
        totalMonny: function () { // 合计
            return Number(this.onlyMonny.cash) || 0 + Number(this.onlyMonny.swipingCard) || 0 + Number(this.onlyMonny.weChat) || 0 + Number(this.onlyMonny.alipay) || 0;
        },
        changeMonny: function () { // 找零
            return this.totalMonny - this.allMonny;
        }
    },
    watch: {
        'incomeMoneyPopup': function () { // 监听弹窗
            this.popups = this.incomeMoneyPopup;
            if (this.incomeMoneyPopup === false) {
                document.onkeydown = null;
            }
        },
        'popups': function () { // 监听弹窗关闭
            if (this.popups === false) {
                this.closePopup();
            }
        }
    },
    created () {
        var _self = this;
        document.onkeydown = function (e) { // 绑定键盘事件
            console.log(e.keyCode);
            switch (e.keyCode) {
                case 81:
                    _self.openPopup('现金', 'cash');
                    return;
                case 87:
                    _self.openPopup('刷卡', 'swipingCard');
                    return;
                case 69:
                    _self.openPopup('微信', 'weChat');
                    return;
                case 82:
                    _self.openPopup('支付宝', 'alipay');
                    return;
            }
        }
    },
    beforeDestroy () { // 组件销毁
        document.onkeydown = null;
    },
    methods: {
        /* 打开输入框事件 */
        openPopup (Name, iptKey) { // 打开收款金额弹窗
            var _self = this;
            this.newKey = iptKey;
            this.popupIpt = true;
            this.newmethod = Name;
            setTimeout(function () {
                _self.$refs.xhIpt.focus();
            }, 300)
        },
        confirmIpt () { // 确定收款金额
            this.onlyMonny[this.newKey] = (this.onlyMonny[this.newKey] || 0) + Number(this.iptNum)
            this.iptNum = null;
            this.popupIpt = false;
        },
        closePopupIpt () { // 关闭收款金额弹窗
            this.popupIpt = false;
        },
        /* 数字键盘 */
        confirmBtn () { // 确定按钮
            alert("确定按钮");
            console.log(this.incomeMoneyPopup);
        },
        closeKeyboard () { // 删除一个数字
            let iptNum = this.iptNum.toString();
            console.log(typeof iptNum);
            this.iptNum = Number(iptNum.slice(0, iptNum.length - 1));
        },
        addValue (parm) { // 键盘的对应事件
            this.$refs.xhIpt.value = this.$refs.xhIpt.value + parm;
            this.$refs.xhIpt.focus();
        },
        closePopup () { // 关闭弹窗
            this.$emit('closeIncomeMoney', false)
        },
        /* 底部的四大功能 */
        restoreValue () { // 还原现金输入
            this.onlyMonny.cash = 0;
            this.onlyMonny.swipingCard = 0;
            this.onlyMonny.weChat = 0;
            this.onlyMonny.alipay = 0;
        }
    }
}
</script>
<style lang="scss" scoped>
.income-money{
    position: relative;
    .header{
        position: relative;
        height: 70px;
        line-height: 70px;
        background-color: #f6f7f8;
        text-align: center;
        span{
            position: absolute;
            right: 10px;
            padding: 0 20px;
            // background-color: red;
            color: #999;
        }
    }
    .income-money-body{
        // border: 1px solid red;
        // height: 600px;
        font-size: 0;
        background-color: #fff;
        padding: 0 60px;
        .body-left{
            display: inline-block;
            vertical-align: top;
            height: 540px;
            margin-top: 20px;
            .ipt-computed{
                li{
                    height: 36px;
                    line-height: 36px;
                    // width: 200px;
                    // margin-right: 20px;
                    margin-bottom: 20px;
                    display: inline-block;
                    i{
                        display: inline-block;
                        font-style: normal;
                        vertical-align: top;
                        font-size: 14px;
                        height: 36px;
                        line-height: 36px;
                        width: 80px;
                        text-align: center;
                        background-color: #f6f7f8;
                        color: #42a8ff;
                        border-top-left-radius: 5px;
                        border-bottom-left-radius: 5px;
                        border: 1px solid #d6d6d6;
                        border-right: none;
                        // opacity: 0.1;
                    }
                    input{
                        padding-right: 12px;
                        vertical-align: top;
                        // padding: 14px;
                        width: 120px;
                        height: 36px;
                        text-align: right;
                        border: 1px solid #d6d6d6;
                        border-top-right-radius: 4px;
                        border-bottom-right-radius: 4px;
                        border-left: 0px solid #fff;
                    }
                }
                li:nth-child(1), li:nth-child(6){
                    display: block;
                    margin-right: 0;
                    width: 100%;
                    input{
                        width: 340px;
                    }
                }
                li:nth-child(2), li:nth-child(4), li:nth-child(6), li:nth-child(7){
                    margin-right: 20px;
                }
                li:nth-child(7), li:nth-child(8){
                    margin-bottom: 0px;
                }
                li:nth-child(7){
                    input{
                        width: 170px;
                        color: red;
                        font-weight: bolder;
                    }
                }
                li:nth-child(8){
                    input{
                        width: 70px;
                        color: red;
                        font-weight: bolder;
                    }
                }
                li:nth-child(1), li:nth-child(7), li:nth-child(8){
                    i{
                        color: #fff;
                        background-color: #ffba42;
                    }
                }
            }
            .keyboard-btn{
                position: absolute;
                z-index: 4000;
                margin-top: 40px;
                border: 2px solid #d6d6d6;
                height: 240px;
                // width: 420px;
                background-color: #555656;
                border-radius: 4px;
                .keyboard-left{
                    // padding: 10px;
                    width: 321px;
                    // border: 1px solid blue;
                    display: inline-block;
                    vertical-align: top;
                    li{
                        display: inline-block;
                        width: 64px;
                        height: 42px;
                        line-height: 42px;
                        font-size: 14px;
                        text-align: center;
                        border-radius: 5px;
                        margin-left: 16px;
                        margin-top: 14px;
                        background-color: yellow;
                        color: #666;
                        background-color: #fff;
                        border: 1px solid #d6d6d6;
                    }
                    .special-number{
                        border: 1px solid #ffba42;
                        // margin-right: 0;
                    }
                }
                .keyboard-right{
                    // border: 1px solid red;
                    display: inline-block;
                    width: 95px;
                    padding-left: 16px;
                    // display: none;
                    li{
                        font-size: 14px;
                        text-align: center;
                        font-size: 18px;
                        border-radius: 5px;
                        margin-top: 14px;
                    }
                    li:nth-child(1){
                        width: 65px;
                        height: 42px;
                        line-height: 42px;
                        background-color: #ffba42;
                        img{
                            margin: 7px 4px 0 0;
                        }
                    }
                    li:nth-child(2){
                        width: 65px;
                        height: 154px;
                        line-height: 154px;
                        color: #fff;
                        font-weight: 600;
                        // margin-top: 10px;
                        background-color: #ffba42;
                    }
                }
            }
            .body-bottom{
                position: absolute;
                bottom: 0;
                left: 0;
                width: 100%;
                height: 70px;
                font-size: 0;
                padding-left: 65px;
                background-color: #f6f7f8;
                li{
                    display: inline-block;
                    width: 90px;
                    height: 40px;
                    line-height: 40px;
                    text-align: center;
                    color: #999;
                    margin-right: 20px;
                    margin-top: 17px;
                    font-size: 14px;
                    border-radius: 5px;
                    background-color: #fff;
                    border: 1px solid #d6d6d6;
                    &:last-child{
                        margin-right: 0;
                    }
                }
            }
        }
        .body-right{
            display: inline-block;
            vertical-align: top;
            margin-left: 40px;
            margin-top: 20px;
            width: 160px;
            // height: 500px;
            // background-color: yellow;
            ul{
                li{
                    height: 36px;
                    line-height: 36px;
                    font-size: 14px;
                    text-align: center;
                    border-radius: 5px;
                    margin-bottom: 20px;
                    color: #fefefe;
                    background-color: #474d5d;
                }
            }
        }
    }
    .popup-bg{ // 弹窗背景层
        position: absolute;
        top: 0;
        left: 0;
        opacity: 0.3;
        width: 100%;
        height: 100%;
        background-color: red;
    }
    .popup-body{
        position: absolute;
        top: 146px;
        left: 80px;
        // right: 0;
        // margin: auto;
        width: 340px;
        height: 190px;
        z-index: 3000;
        // font-size: 20px;
        border-radius: 4px;
        background-color: #fff;
        .popup-body-ipt{
            // border: 1px solid red;
            width: 300px;
            height: 50px;
            line-height: 50px; 
            margin-left: 20px;
            margin-top: 45px;
            font-size: 0;
            border-radius: 5px;
            overflow: hidden;
            border: 1px solid #d6d6d6;
            i{
                font-style: normal;
                display: inline-block;
                vertical-align: top;
                width: 50px;
                height: 50px;
                line-height: 50px;
                font-size: 16px;
                color: #fff;
                background-color: #ffba42;
                text-align: center;
            }
            input{
                display: inline-block;
                height: 50px;
                line-height: 50px;
                width: 248px;
                outline: none;
                padding-left: 10px;
            }
        }
        .operation-btn{
            height: 50px;
            margin-top: 45px;
            background-color: #f6f7f8;
            text-align: center;
            font-size: 0;
            a{
                display: inline-block;
                width: 120px;
                height: 36px;
                border-radius: 4px;
                line-height: 36px;
                text-align: center;
                font-size: 14px;
            }
            a:nth-child(1){
                border: 1px solid #d6d6d6;
                margin-right: 40px;
            }
            a:nth-child(2){
                background-color: #4fdccb;
                color: #fff;
            }
            // border: 1px solid red;
        }
    }
}
</style>