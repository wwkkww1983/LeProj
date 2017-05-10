<template>
    <div class="step">
        <p class="popTitle">
            <a @click="showLastView" class="prePage">上一步</a>
            选择首饰名称
        </p>
        <div class="dialog-info">
            <ul id="big" :class="['bigType', {'animateFour': fourBig}]" :style="bigStyle">
                <li v-for="(item, index) in productTypeList" @click="move(index)" v-if="item.show">{{item.classesName}}</li>
            </ul>
            <ul id="small" :class="['smallType']" v-if="transform" :style="smallStyle">
                <li v-for="item in productTypeListSmall" @click="getChildDta(item.classesId, item.classesName)">{{item.classesName}}</li>
            </ul>
        </div>
    </div>
</template>
<script>
import {seekProductClassList} from './../../../../../Api/commonality/seek'
export default {
    data () {
        return {
            productTypeList: null,
            productTypeListSmall: null,
            show: true,
            transform: true,
            fourBig: false,
            bigStyle: {},
            smallStyle: {},
            view: 4,
            lastView: 3,
            chooseData: {
                childIndex: 4,
                chooseBig: "",
                chooseBigName: "",
                chooseSmall: "",
                chooseSmallName: ""
            }
        }
    },
    created () {
        this.getProduct();
    },
    methods: {
        cancelPop () {
            this.popup.openPopup = false;
        },
        getProduct () { // 大类个数
            let options = "3"
            seekProductClassList(options).then((response) => {
                this.productTypeList = response.data.data.list;
                let Num = this.productTypeList.length;
                for (var i = 0; i < Num; i++) {
                    this.productTypeList[i].show = true;
                }
                if (Num > 3) {
                    this.bigStyle = {
                        left: '40px'
                    };
                }
                console.log(response);
            }, (response) => {
                console.log(response);
            })
        },
        move (index) {
            let Num = this.productTypeList.length; // 获取大类长度
            for (var i = 0; i < Num; i++) {
                this.productTypeList[i].show = false;
            }
            this.productTypeList[index].show = true;
            this.productTypeListSmall = this.productTypeList[index].childrenList;
            var big = document.getElementById("big");
            var small = document.getElementById("small");
            this.move2(big, {"left": "40", "top": "20", "opacity": 100}, "normal");
            this.move2(small, {"left": "40", "top": "76", "opacity": 100}, "normal")
            if (Num > 3) {
                this.fourBig = true;
            }
            this.chooseData.chooseBig = this.productTypeList[index].classesType;
            this.chooseData.chooseBigName = this.productTypeList[index].classesName;
        },
        nextView () { // 下一步
            this.$emit("changeView", this.view)
        },
        showLastView () {
            this.$emit("changeView", this.lastView)
        },
        // getChildDta (msg) {
        //     this.chooseData.chooseSmall = msg;
        //     this.$emit("childData", this.chooseData);
        //     // console.log(this.chooseData);
        //     this.nextView();
        //     this.$emit("changeView");
        //     // this.$router.push("/work/sell/sellReceiptsList/sellDetail")
        // },
        getChildDta (id, name) { // 获取大小类的数据
            this.chooseData.chooseSmall = id;
            this.chooseData.chooseSmallName = name;
            this.$emit("childData", this.chooseData);
            this.nextView();
            this.$emit("changeView");
        },
        move2 (obj, moveJson, stopTime) { // 对象 终点 运动终点  运动方式
            var prd_speed = { // 预定义 predefine
                veryslow: 5000,
                slow: 2000,
                normal: 1000,
                fast: 700,
                veryfast: 300
            };
            // 如果输入预定速度的字符串，就进行转换
            if (stopTime) {
                if (typeof stopTime === 'string') {
                    stopTime = prd_speed[stopTime];
                };
            } else {
                stopTime = prd_speed.normal;
            }
            var start = {}; // json
            var dis = {}; // json
            for (var key in moveJson) {
                start[key] = this.getStyle(obj, key);
                dis[key] = moveJson[key] - start[key]; // 距离 distance
            }
            // 定时器---------------------------------------------
            var count = parseInt(stopTime / 30); // 次数
            var n = 0; // 步进
            clearInterval(obj.timer); // 开定时器之前，先清除定时器
            obj.timer = setInterval(function() {
                n++;
                for (var key in moveJson) {
                    var a = 1 - n / count; // a的值会越来越小
                    var step_dis = start[key] + dis[key] * (1 - a * a * a);
                    if (key === 'opacity') { // 透明
                        obj.style.filter = 'alpha(opacity:' + step_dis + ')';
                        obj.style.opacity = step_dis / 100;
                    } else { // 其他
                        obj.style[key] = step_dis + 'px';
                    }
                };
                // 清除定时器
                if (n === count) {
                    clearInterval(obj.timer);
                };
            }, 30)
        },
        getStyle (obj, styleName) {
            var value = obj.currentStyle ? obj.currentStyle[styleName] : getComputedStyle(obj, false)[styleName];
            if (styleName === 'opacity') {
                value = Math.round(parseFloat(value) * 100);
            } else {
                value = parseInt(value);
            }
            return value;
        }
    }
}
</script>
<style src="./../css/tep.scss" lang="scss" scoped></style>