<template>
    <el-dialog class="dialog-w540-h320-hn" title="" v-model="isShow">
        <div class="receipts-class" v-if="cutData.one">
            <p class="popTitle">
                选择产品大类
            </p>
            <div class="dialog-info">
                <ul id="big" :class="['bigType', {'animateFour': fourBig}]" :style="bigStyle">
                    <li v-for="(item, index) in productTypeList" @click="move(index)" v-if="item.show">{{getConfigValue(parseInt(item.classesType))}}</li>
                </ul>
                <ul id="small" :class="['smallType']" v-if="transform">
                    <li v-for="item in productTypeListSmall" @click="getChildDta(item.classesId)">{{item.classesName}}</li>
                </ul>
            </div>
        </div>
        <!-- 店铺列表 -->
        <div class="list" v-if="cutData.two">
            <p class="popTitle">
                选择店铺
            </p>
            <ul>
                <li v-for="item in shopListByCo" @click="getShopId(item.shopId)">
                    {{item.shopName}}
                </li>
            </ul>
        </div>
        <div class="list" v-if="cutData.three">
            <p class="popTitle">
                <a href="javascript:void(0)" class="prePage" @click="toPageFun('two', 'three')">上一步</a>
                供应商
                <a href="javascript:void(0)" class="skip" @click="toPageFun('four', 'three')">下一步</a>
            </p>
            <ul>
                <li v-for="item in supplierListData" @click="getSupplierId(item.supplierId)">
                    {{item.supplierName}}
                </li>
            </ul>
        </div>
        <!-- 库位列表 -->
        <div class="list" v-if="cutData.four">
            <p class="popTitle">
                选择库位
            </p>
            <ul>
                <li v-for="item in repositoryList" @click="getRepositoryId(item.repositoryId)">
                    {{item.repositoryName}}
                </li>
            </ul>
        </div>
    </el-dialog>
</template>
<script>
import {mapGetters, mapActions} from "vuex"
import {operateCreateRKReceipt} from './../../Api/commonality/operate'
export default {
    data () {
        return {
            "productTypeList": null,
            "productTypeListSmall": null,
            "transform": true,
            "fourBig": false,
            "bigStyle": {},
            "viewBig": 2,
            "chooseData": {
                "childIndex": 1,
                "chooseBig": "",
                "chooseSmall": ""
            },
            "cutData": {
                "one": true,
                "two": false,
                "three": false,
                "four": false
            },
            "newDatas": { // 新增数据(其它页面也用到的)
                "shopId": "", // 分销商ID
                "supplierId": "", // 供应商ID
                "productTypeId": "", // 产品类别ID
                "repositoryId": "" // 库位ID
            },
            "isShow": false
        }
    },
    props: [
        'newPopup'
    ],
    watch: {
        'newPopup': function () {
            console.log(this.newPopup)
            if (this.newPopup === true) {
                this.isShow = this.newPopup;
            }
        },
        'isShow': function () {
            if (this.isShow === false) {
                this.$emit("closePopup", false)
            }
        }
    },
    created () {
        this.getProduct();
        this.workRepositoryList();
        this.workSupplierList();
        this.isShow = this.newPopup;
    },
    computed: {
        ...mapGetters([
            "shopListByCo", // 店铺列表
            "repositoryList", // 库位列表
            "supplierListData" // 供应商
        ])
    },
    methods: {
        ...mapActions([
            "workRepositoryList", // 库位列表
            "getShopListByCo", // 店铺列表
            "workSupplierList" // 供应商
        ]),
        toPageFun (to, from) { // 去到的， 目前的
            this.cutData[to] = true;
            this.cutData[from] = false;
        },
        getShopId (parm) { // 绑定店铺Id
            this.newDatas.shopId = parm;
            this.cutData.two = false;
            this.cutData.three = true;
            // alert(parm);
        },
        getSupplierId (parm) { // 绑定供应商
            this.cutData.three = false;
            this.cutData.four = true;
            this.newDatas.supplierId = parm;
        },
        getRepositoryId (parm) { // 绑定库位Id
            var options = {
                // "shopId": this.newDatas.shopId, // 分销商ID
                "supplierId": this.newDatas.supplierId, // 供应商ID
                "productTypeId": this.newDatas.productTypeId, // 产品类别ID
                "repositoryId": parm // 库位ID
            }
            sessionStorage.setItem("确定新建入库", JSON.stringify(options));
            operateCreateRKReceipt(options).then((response) => { // 新建单据
                console.log("新建单据");
                console.log(response);
                if (response.data.state === 200) {
                    sessionStorage.setItem("orderNumber", response.data.data.orderNum);
                    this.$router.push("/work/storage/NewStockIn");
                } else {
                    alert(response.data.msg);
                }
                // console.log("确定新建入库");
                // console.log(response);
            }, (response) => {
                console.log(response);
            })
            this.cutData.three = false;
        },
        cancelPop () {
            this.popup.openPopup = false;
        },
        getProduct () { // 大类个数
            var data = {
                "data": {
                    "type": "1",
                    "userId": sessionStorage.getItem("id")
                },
                "unit": {
                    "companyId": sessionStorage.getItem("companyId"),
                    "channel": 3,
                    "os": "string",
                    "ip": "string",
                    "userId": sessionStorage.getItem("id"),
                    "tokenId": sessionStorage.getItem("tokenId")
                }
            }
            var url = INTERFACE_URL_9083 + "/v1/goods/getProductTypeList";
            this.$http.post(url, data).then((response) => {
                // console.log("产品名称");
                // console.log(response.data.data.list);
                this.productTypeList = response.data.data.list;
                let Num = this.productTypeList.length;
                // console.log(Num)
                for (var i = 0; i < Num; i++) {
                    this.productTypeList[i].show = true;
                }
                if (Num > 3) {
                this.bigStyle = {
                    left: '40px'
                };
            }
            }, (response) => {
                console.log(response);
            })
        },
        getConfigValue (parm) { // 配置产品类别
            let configName = null;
            switch (parm) {
                case 1:
                    configName = "素金类";
                    break;
                case 2:
                    configName = "珠宝类";
                    break;
                case 3:
                    configName = "饰品类"
                    break;
            }
            return configName;
        },
        move (index) {
            let Num = this.productTypeList.length; // 获取大类长度
            for (var i = 0; i < Num; i++) {
                this.productTypeList[i].show = false;
            }
            this.productTypeList[index].show = true;
            this.productTypeListSmall = this.productTypeList[index].typeList;
            var big = document.getElementById("big");
            var small = document.getElementById("small");
            this.move2(big, {"left": "40", "top": "20", "opacity": 100}, "normal");
            this.move2(small, {"left": "40", "top": "76", "opacity": 100}, "slow")
            if (Num > 3) {
                this.fourBig = true;
            }
            this.chooseData.chooseBig = this.productTypeList[index].classesType;
        },
        // nextView () {
        //     this.$emit("changeView", this.viewBig);
        // },
        getChildDta (parm) {
            this.cutData.one = false;
            this.cutData.two = true;
            this.newDatas.productTypeId = parm;
        },
        move2 (obj, moveJson, stopTime) { // 对象 终点 运动终点  运动方式
            var prd_speed = { // 预定义 predefine
                veryslow: 5000,
                slow: 1000,
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
<style lang="scss" scoped>
.receipts-class{ // 产品类别
    width: 100%;
    height: 100%;
    .dialog-info{ // 弹窗内容  
        position: relative;
        height: 215px;
        overflow-y: auto;
        ul.bigType{           
            display: inline-block;
            position: absolute;
            top: 90px;
            left: 105px;
            opacity: 1;
            li{
                display: inline-block;
                width: 100px;
                height: 36px;
                line-height: 36px;               
                background: #fff;
                border: 1px solid #d6d6d6;
                color: #999;
                cursor: pointer;
                text-align: center;
                border-radius: 3px;
                margin:0 20px 20px 0;                
            }
            li:last-child{
                margin-right:0;
            }
            li:nth-child(4n){
                margin-right: 0;
            }
            li:hover{
                background: #FFBA41;
                border: 1px solid #FFBA41;
                color: #fff;
            }
        }
        ul.smallType{
            position: absolute;
            top: 150px;
            text-align: left;
            left: 40px;
            opacity: 0;
            li{
                display: inline-block;
                width: 100px;
                height: 36px;
                line-height: 36px;               
                background: rgba(79,220,202,0.1);
                border: 1px solid #4fdcca;
                color: #4fdcca;
                cursor: pointer;
                text-align: center;
                border-radius: 3px;
                margin: 0 20px 10px 0;
            }
        }
    }
}
.popTitle{ // 弹窗标题
    height: 70px;
    line-height: 70px;
    text-align: center;
    background: #f6f7f8;
    border-radius: 4px 4px 0 0;
    .prePage{ // 上一页
        position: absolute;
        left: 10px;
    }
    .skip{ // 跳过
        position: absolute;
        right: 10px;
    }
}
.list{
    width: 540px;
    height: 320px;
    ul{
        padding: 20px;
        li{
            display: inline-block;
            width: 100px;
            height: 36px;
            line-height: 36px;               
            background: #fff;
            border: 1px solid #d6d6d6;
            color: #999;
            cursor: pointer;
            text-align: center;
            border-radius: 3px;
            margin:0 20px 20px 0; 
        }
        li:hover{
            background: #FFBA41;
            border: 1px solid #FFBA41;
            color: #fff;
        }
    }
}
// { // 分销商
//     width: 540px;
//     height: 320px;
// }
// .repository-list{ // 店铺列表
//     width: 540px;
//     height: 320px;
// }
</style>
<!-- <style src="./../css/tep.scss" lang="scss" scoped></style> -->