<template>
<div class="item-box">
    <div class="item-title">
        <span>配件名称</span>
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
            <li v-for="(item, index) in PartsNameList">
                {{item.classesName}}
                <i class="el-icon-circle-close i-close" v-if="isEidt" @click="delProduct(index)"></i>
            </li>
            <li v-if="isEidt" class="addLi" @click="newStockIn">
                <i class="el-icon-plus i-add"></i>
            </li>
        </ul>
    </div>
    <div class="dialog-w380">
        <el-dialog title="新增配件名称" v-model="popup.newPopup">
            <p class="m20">
                <el-form :model="ruleForm" :rules="rules" ref="ruleForm" class="demo-ruleForm">
                    <el-form-item prop="inputPartsName">
                        <el-input v-model="ruleForm.inputPartsName" placeholder="请输入 配件名称" v-bind:maxlength="7"></el-input>
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
                let isAdd = this.PartsNameList.find((v) => v.classesName === this.ruleForm.inputPartsName)
                if (isAdd !== undefined) {
                    callback(new Error('该配件名称已存在'));
                } else {
                    callback();
                }
            }
        };
        return {
            PartsNameList: null,
            isEidt: false,
            isShow: true,
            popup: { // 弹窗
                newPopup: false // 产品类型
            },
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
        this.productList()
    },
    methods: {
        productList () {
            var data = {
                "data": {
                    "type": "4", // 显示类型：1金含量2品牌3金属颜色4配件名称
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showProductPropertyList";
            this.$http.post(url, data).then((response) => {
                console.log("配件名称");
                console.log(response.data.data.list);
                this.PartsNameList = response.data.data.list;
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
        },
        affirmNew () { // 确认新增
            if (this.ruleForm.inputPartsName !== null && this.ruleForm.inputPartsName !== "") {
                let isAdd = this.PartsNameList.find((v) => v.classesName === this.ruleForm.inputPartsName)
                if (isAdd === undefined) {
                    let addName = this.ruleForm.inputPartsName;
                    var data = {
                        "data": {
                            "type": "4", // 显示类型：1 产品类型
                            "list": [
                                {
                                "classesName": addName // 名称
                                }]
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
                    var url = INTERFACE_URL_9083 + "/v1/headquarter/addProductProperty";
                    this.$http.post(url, data).then((response) => {
                        if (response.data.state === 200) {
                            this.PartsNameList.push({
                                "classesName": addName
                            });
                            alert("添加成功")
                            console.log(addName)
                            this.cancelDialog()
                            this.productList()
                        } else {
                            alert(response.data.msg);
                        }
                        console.log("添加类别");
                        console.log(response);
                        // this.PartsNameList[index].typeList.splice(i, 1);
                    }, (response) => {
                        console.log(response);
                    })
                }
            }
        },
        delProduct (index) {  // 删除
            var data = {
                "data": {
                    "type": "4", // 显示类型：1 产品类型
                    "list": [
                        {
                        "classesId": this.PartsNameList[index].classesId // 类型ID
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
            console.log(data);
            var url = INTERFACE_URL_9083 + "/v1/headquarter/delProductProperty";
            this.$http.post(url, data).then((response) => {
                if (response.data.state === 200) {
                    this.PartsNameList.splice(index, 1);
                    alert("删除成功")
                } else {
                    alert(response.data.msg);
                }
                console.log("删除配件");
                console.log(response);
            }, (response) => {
                console.log(response);
            })
        },
        cancelDialog () {
            this.popup.newPopup = false
            this.inputPartsName = ''
        }
    }
}
</script>

<style src="./css/header.scss" lang="scss" scoped></style>
<style src="./css/brand.scss" lang="scss" scoped></style>