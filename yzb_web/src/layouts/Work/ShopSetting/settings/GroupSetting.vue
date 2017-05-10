<template>
<div class="item-box">
   <div class="item-title">
        <span>柜组设置</span>
        <div class="buttons">
            <el-button type="text" 
            v-show="isShow"
            @click="addFun">编辑</el-button>
            <el-button 
            type="success" 
            size="small" 
            v-show="!isShow"
            @click="finishFun">完成</el-button>
        </div>
    </div>
    <div class="item-content">
        <ul class="proBrand">
            <li v-for="(item, index) in groupList">
                {{item.counterName}}
                <i class="el-icon-circle-close i-close" v-if="item.defaultFlag=='Y' ? false : isEidt"  @click="delProduct(index)"></i>
                <span v-if="item.defaultFlag=='Y'">默认柜组</span>
            </li>
            <li v-if="isEidt" class="addLi" @click="newStockIn">
                <i class="el-icon-plus i-add"></i>
            </li>
        </ul>
    </div>
    
    <div class="dialog-w380">
        <el-dialog title="新增柜组" v-model="popup.newPopup">
            <p class="m20">
                <el-form :model="ruleForm" :rules="rules" ref="ruleForm" class="demo-ruleForm">
                    <el-form-item prop="inputProductName">
                        <el-input v-model="ruleForm.inputProductName" placeholder="请输入柜组名称" v-bind:maxlength="7"></el-input>                        
                    </el-form-item>
                </el-form>                                             
            </p>
            <span slot="footer">
                <a href="javascript: null" class="cancel-btn-w120" @click="cancelDialog">取 消</a>
                <a href="javascript: null" class="confirm-btn-w120 mr0" @click="affirmNew" >确 定</a>
            </span>
        </el-dialog>
    </div>
</div>
</template>

<script>
export default {
    props: [
        'shopStorage'
    ],
    data () {
        return {
            groupList: [],
            isEidt: false,
            isShow: true,
            popup: { // 弹窗
                newPopup: false // 产品类型
            },
            newPopupIndex: null,
            ruleForm: {
                inputProductName: ''
            },
            rules: {
                inputProductName: [
                    { required: true, message: '此处不能为空', trigger: 'blur' }
                ]
            }
        }
    },
    created () {
        this.getCounter()
    },
    methods: {
        getCounter () {
            var data = {
                "data": {
                    "shopId": this.shopStorage // 店铺ID
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
            console.log(this.shopStorage)
            console.log(1111111111)
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showCounterList";
            this.$http.post(url, data).then((response) => {
                console.log("所有柜台");
                console.log(response.data.data.counterList);
                // this.thisShopId = response.data.data.counterList;
                this.groupList = response.data.data.counterList;
            }, (response) => {
                console.log(response);
            })
        },
        addFun () { // 添加
            this.isEidt = true;
            this.isShow = !this.isEidt;
        },
        finishFun () { // 完成
            this.isEidt = false;
            this.isShow = !this.isEidt;
        },
        newStockIn () { // 新建产品类别
            this.popup.newPopup = true;
            // this.newPopupIndex = index;
        },
        affirmNew () { // 确认添加
            var data = {
                "data": {
                    "shopId": this.shopStorage, // 店铺idid
                    "counterName": this.ruleForm.inputProductName
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/addCounter";
            this.$http.post(url, data).then((response) => {
                console.log("所有柜台");
                console.log(response.data);
                // this.thisShopId = response.data.data.counterList;
                // this.groupList = response.data.data;
                this.groupList.push({
                    counterName: this.ruleForm.inputProductName
                })
                this.cancelDialog()
            }, (response) => {
                console.log(response);
            })
        },
        cancelDialog () {
            this.popup.newPopup = false
            this.ruleForm.inputProductName = ''
        },
        delProduct (index) {
            var data = {
                "data": {
                    "counterIdList": [{
                        "counterId": this.groupList[index].counterId
                    }], // 柜组ID 列表
                    "handleType": "1"
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/deleCounter"
            this.$http.post(url, data).then((response) => {
                console.log("删除柜台");
                this.groupList.splice(index, 1)
            }, (response) => {
            console.log(response);
            })
            this.cancelDialog()
            }
        }
}
</script>

<style src="./header.scss" lang="scss" scoped></style>
<style lang="scss" scoped>
$d6: #D6D6D6; // li边框颜色

.item-content{
    padding: 10px 0 0;
    select{
        width: 240px;
        height: 36px;
        line-height: 36px;
        border: 1px solid $d6;
        padding: 0 10px 0 20px;
        margin-right: 20px;
    }
    select:last-child{
        margin: 0;
    }
    ul{
        padding: 10px 0 0 20px;
        >li{
            width: 240px;
            height: 36px;
            line-height: 36px;
            border: 1px solid $d6;
            margin: 0 20px 20px 0;
            position: relative;
            font-size: 14px;
            display: inline-block;
            cursor: pointer;
            padding-left: 15px;
            span{
                position: absolute;
                right: 10px;
                .el-radio__label{
                    font-size: 10px;
                }
            }
            i.i-close{
                position: absolute;
                top: -7px;
                right: -7px;
            }
            i.i-add{
                color: #00c0ab;
            }
        }
        li:last-child{
            margin-right: 0;
        }
        .addLi{
            text-align: center;
            border: 1px solid #00c0ab;
        }
    }    
}
</style>
