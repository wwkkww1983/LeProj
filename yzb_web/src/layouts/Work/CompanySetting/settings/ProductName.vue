<template>
<div class="item-box">
    <div class="item-title">
        <span>产品名称</span>
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
        <dl v-for="(item, index) in productTypeList">
            <dt>{{getConfigValue(item.classesType)}}</dt>
            <dd>
                <ul>
                    <li v-for="(item, i) in item.typeList">
                        {{item.classesName}}
                        <i class="el-icon-circle-close i-close" v-if="isEidt" @click="delProduct(index, i)"></i>
                    </li>
                    <li v-if="isEidt" class="addLi" @click="newStockIn(index)">
                        <i class="el-icon-plus i-add"></i>
                    </li>
                </ul>
            </dd>
        </dl>
    </div>
    <div class="dialog-w380">
        <el-dialog title="新增产品类型" v-model="popup.newPopup">            
            <p class="m20">
                <el-form :model="ruleForm" :rules="rules" ref="ruleForm" class="demo-ruleForm">
                    <el-form-item prop="inputProductName">
                        <el-input v-model="ruleForm.inputProductName" placeholder="请输入 产品类别" v-bind:maxlength="7"></el-input>
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
                let isAdd = this.productTypeList[this.newPopupIndex].typeList.find((value) => value.classesName === this.ruleForm.inputProductName)
                if (isAdd !== undefined) {
                    callback(new Error('该产品类别已存在'));
                } else {
                    callback();
                }
            }
        }
        return {
            productTypeList: null,
            isEidt: false,
            isShow: true,
            popup: { // 弹窗
                newPopup: false // 产品类型
            },
            newPopupIndex: null,
            ruleForm: {
                inputProductName: null
            },
            rules: {
                inputProductName: [
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
                console.log("产品名称");
                console.log(response.data.data.list);
                this.productTypeList = response.data.data.list;
            }, (response) => {
                console.log(response);
            })
        },
        getConfigValue (parm) { // 配置产品类别
            let configName = null;
            switch (parm) {
                case '1':
                    configName = "素金类（计重）";
                    break;
                case '2':
                    configName = "珠宝类（计件）";
                    break;
                case '3':
                    configName = "饰品类（银饰 / 饰品）"
                    break;
            }
            return configName;
        },
        addFun () { // 添加
            this.isEidt = true;
            this.isShow = !this.isEidt;
        },
        finishFun () { // 完成
            this.isEidt = false;
            this.isShow = !this.isEidt;
        },
        newStockIn (index) { // 新建产品类别
            this.popup.newPopup = true;
            this.newPopupIndex = index;
        },
        affirmNew () {
            if (this.ruleForm.inputProductName !== null && this.ruleForm.inputProductName !== "") {
                let isAdd = this.productTypeList[this.newPopupIndex].typeList.find((v) => v.classesName === this.ruleForm.inputProductName)
                if (isAdd === undefined) {
                    let addName = this.ruleForm.inputProductName;
                    var data = {
                        "data": {
                            "type": "1", // 显示类型：1 产品类型
                            "list": [
                            {
                                "classesType": this.productTypeList[this.newPopupIndex].classesType, // 1 素金类2 珠宝类3 饰品类
                                "classesId": "", // 类型ID
                                "classesName": addName// 名称
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
                    // sessionStorage.setItem("aaa", JSON.stringify(data));
                    var url = INTERFACE_URL_9083 + "/v1/goods/insertProductType";
                    this.$http.post(url, data).then((response) => {
                        if (response.data.state === 200) {
                            this.productTypeList[this.newPopupIndex].typeList.push({
                                "classesName": addName
                            });
                            alert("添加成功")
                            console.log(addName)
                        } else {
                            alert(response.data.msg);
                        }
                        console.log("添加类别");
                        console.log(response);
                        // this.productTypeList[index].typeList.splice(i, 1);
                        this.cancelDialog()
                        this.productList()
                    }, (response) => {
                        console.log(response);
                    })
                }
            }
        },
        delProduct (index, i) {
            var data = {
                "data": {
                    "type": "1", // 显示类型：1 产品类型
                    "list": [
                        {
                        "classesId": this.productTypeList[index].typeList[i].classesId // 类型ID
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
            var url = INTERFACE_URL_9083 + "/v1/goods/delProductTypeByList";
            this.$http.post(url, data).then((response) => {
                if (response.data.state === 200) {
                    this.productTypeList[index].typeList.splice(i, 1);
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
            this.ruleForm.inputProductName = ''
        },
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
            if (valid) {
                alert('submit!');
            } else {
                console.log('error submit!!');
                return false;
            }
            });
      },
      resetForm(formName) {
        this.$refs[formName].resetFields();
      }
    }
}
</script>

<style src="./css/header.scss" lang="scss" scoped></style>
<style src="./css/col6.scss" lang="scss" scoped></style>
