<template>
<div class="item-box">
    <div class="item-title">
        <span>供应商设置</span>
        <div class="buttons">
            <el-button type="text" 
            @click="addFun" class="text-info"
            v-show="isShow">编辑</el-button>
            <el-button 
            type="success" 
            size="small" 
            @click="finishFun"
            v-show="!isShow">完成</el-button>
        </div>
    </div>
    <div class="item-content">
        <ul>
            <li v-for="(item, index) in PartsNameList" >
                {{item.supplierName}}
                <i class="el-icon-circle-close i-close" v-if="isEidt" @click="delProduct(index)"></i>
            </li>
            <li v-if="isEidt" class="addLi" @click="newStockIn">
                <i class="el-icon-plus i-add"></i>
            </li>
        </ul>
    </div>
    <div class="dialog-w380">
        <el-dialog title="新增库位" v-model="popup.newPopup">
            <p class="m20">
                <el-form :model="ruleForm" :rules="rules" ref="ruleForm" class="demo-ruleForm">
                    <el-form-item prop="inputPartsName">
                        <el-input v-model="ruleForm.inputPartsName" placeholder="请输入供应商" v-bind:maxlength="10"></el-input>
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
    data () {
        var validateInput = (rule, value, callback) => {
            if (!value) {
                return callback();
            } else {
                let isAdd = this.PartsNameList.find((v) => v.supplierName === this.ruleForm.inputPartsName)
                if (isAdd !== undefined) {
                    callback(new Error('该产品类别已存在'));
                } else {
                    callback();
                }
            }
        }
        return {
            PartsNameList: null,
            isEidt: false,
            isShow: true,
            popup: { // 弹窗
                newPopup: false // 产品类型
            },
            defaultSetting: null,
            ruleForm: {
                inputPartsName: ''
            },
            rules: {
                inputPartsName: [
                    { required: true, message: "此处不能为空", trigger: 'blur' },
                    { validator: validateInput, trigger: 'change' }
                ]
            }
        }
    },
    created () {
        this.moreSetting()
    },
    methods: {
        moreSetting () {
            var data = {
                "data": {
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showProviderList";
            this.$http.post(url, data).then((response) => {
                console.log("库位设置");
                console.log(response.data.data.supplierList);
                this.PartsNameList = response.data.data.supplierList;
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
        newStockIn () { // 新建品牌类别
            this.popup.newPopup = true;
        },
        affirmNew () {  // 确认添加
            if (this.ruleForm.inputPartsName !== '') {
                let isAdd = this.PartsNameList.find((v) => v.supplierName === this.ruleForm.inputPartsName)
                if (isAdd === undefined) {
                    let addName = this.ruleForm.inputPartsName;
                    var data = {
                        "data": {
                            "supplierName": addName // 供应商名
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
                    var url = INTERFACE_URL_9083 + "/v1/headquarter/addProvider";
                    this.$http.post(url, data).then((response) => {
                        console.log("库位设置");
                        console.log(response.data.data.supplierList);
                        this.PartsNameList.push({
                            "supplierName": addName
                        })
                        this.cancelDialog()
                        this.moreSetting()
                    }, (response) => {
                        console.log(response);
                    })
                }
            }
        },
        delProduct (index) {
            // let isDel = true
            // if (isDel) {
            //     this.PartsNameList.splice(index, 1)
            // } else {
            //     this.$message('有商品使用了该数据，不能删除')
            // }
            var data = {
                "data": {
                    "handleType": "1", // 类型 1 删除
                    "supplierList": [
                    {
                        "supplierId": this.PartsNameList[index].supplierId // 供应商Id
                    }
                    ]
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
            sessionStorage.setItem("acs", JSON.stringify(data));
            var url = INTERFACE_URL_9083 + "/v1/headquarter/delProvider";
            this.$http.post(url, data).then((response) => {
                if (response.data.state === 200) {
                    this.PartsNameList.splice(index, 1);
                    alert("删除成功")
                } else {
                    alert(response.data.msg);
                }
                console.log("删除产品");
                console.log(response);
                // this.productTypeList[index].typeList.splice(i, 1);
            }, (response) => {
                console.log(response);
            })
        },
        cancelDialog () {
            this.popup.newPopup = false
            this.ruleForm.inputPartsName = ''
        }
    }
}
</script>

<style src="./css/header.scss" lang="scss" scoped></style>
<style lang="scss" scoped>
$d6: #D6D6D6; // li边框颜色

.item-content{
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
        padding: 0 0 20px 20px;
        >li{
            width: 240px;
            height: 35px;
            line-height: 35px;
            border: 1px solid $d6;
            border-radius: 4px;
            display: inline-block;
            margin: 20px 18px 0 0;
            position: relative;
            cursor: pointer;
            text-align: center;
            i.i-close{
                position: absolute;
                top: -7px;
                right: -7px;
            }
            i.i-add{
                color: #00c0ab;
            }
        }
        .addLi{
            border: 1px solid #00c0ab;
        }
    }
}
</style>
