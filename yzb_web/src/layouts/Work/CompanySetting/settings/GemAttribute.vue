<template>
<div class="item-box">
   <div class="item-title">
        <span>宝石属性</span>
        <div class="buttons">
            <el-button type="text" class="text-info"
            v-show="isShow"  @click="editFun">编辑</el-button>
            <el-button type="success" size="small" 
            v-show="!isShow" @click="finishFun">完成</el-button>
        </div>
    </div>
    <div class="item-content">
        <dl v-for="(item, index) in gemName" :class="{hasDel:isDel}">
            <i class="el-icon-delete" v-show="isDel"></i>
            <dt>
                {{item.classesName}}
            </dt>
            <dd>
                <ul>
                    <li v-for="(item, i) in item.childrenList">
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
        <el-dialog title="新增宝石属性" v-model="popup.newPopup">
            <p class="m20">
                <el-form :model="ruleForm" :rules="rules" ref="ruleForm" class="demo-ruleForm">
                    <el-form-item prop="inputProductName">
                        <el-input v-model="ruleForm.inputProductName" placeholder="请输入 宝石属性" v-bind:maxlength="7"></el-input>
                    </el-form-item>
                </el-form>                        
            </p>
            <span slot="footer">
                <a href="javascript: null" class="cancel-btn-w120" @click="popup.addPopup=false">取 消</a>
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
                let isAdd = this.gemName[this.newPopupIndex].childrenList.find((v) => v.classesName === this.ruleForm.inputProductName)
                if (isAdd !== undefined) {
                    callback(new Error('该宝石属性已存在'));
                } else {
                    callback();
                }
            }
        };
        return {
            gemName: null, // 宝石名称
            isEidt: false, // 是否编辑
            isShow: true, // 是否显示编辑
            isDel: false, // 是否删除
            isDelShow: true, // 是否显示删除
            popup: { // 弹窗
                newPopup: false, // 产品类型
                addPopup: false
            },
            newPopupIndex: null,
            ruleForm: {
                inputProductName: ''
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
                    "type": "4",
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
            // var url = INTERFACE_URL_9083 + "/v1/goods/getProductTypeList";
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showProductClassesList";
            this.$http.post(url, data).then((response) => {
                console.log("产品名称");
                console.log(response.data.data.list);
                this.gemName = response.data.data.list;
            }, (response) => {
                console.log(response);
            })
        },
        addFun () { // 添加
            this.popup.addPopup = true;
            this.isEidt = false;
            this.isShow = !this.isEidt;
            this.isDel = false;
            this.isDelShow = !this.isDel;
        },
        editFun () { // 编辑
            this.isEidt = true;
            this.isShow = !this.isEidt;
            this.isDel = false;
            this.isDelShow = !this.isDel;
        },
        finishFun () { // 编辑完成
            this.isEidt = false;
            this.isShow = !this.isEidt;
        },
        delFun () { // 删除
            this.isDel = true;
            this.isDelShow = !this.isDel;
            this.isEidt = false;
            this.isShow = !this.isEidt;
        },
        finishDelFun () { // 删除完成
            this.isDel = false;
            this.isDelShow = !this.isDel;
        },
        newStockIn (index) { // 新建产品类别
            this.popup.newPopup = true;
            this.newPopupIndex = index;
        },
        affirmNew () {  // 小类添加
            if (this.ruleForm.inputPartsName !== null && this.ruleForm.inputPartsName !== "") {
                let isAdd = this.gemName[this.newPopupIndex].childrenList.find((v) => v.classesName === this.ruleForm.inputProductName)
                if (isAdd === undefined) {
                    let addName = this.ruleForm.inputProductName;
                    var data = {
                        "data": {
                            "handleType": "4", // 1 新增大小一起 2 修改（只有类型1 2 3 最高级可以修改）3 新增单独大类 4 新增单独小
                            "list": {
                                "childrenList": [
                                    {
                                    "classesName": addName // 子级名称
                                    }
                                ],
                                "classesName": this.gemName[this.newPopupIndex].classesName, // 最高级名
                                "classesId": this.gemName[this.newPopupIndex].classesId // 最高级ID
                            },
                            "type": "4" // 1成色名称 2宝石名称 3首饰类别  4 宝石属性
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
                    sessionStorage.setItem("aaa", JSON.stringify(data));
                    var url = INTERFACE_URL_9083 + "/v1/headquarter/operateProductClasses";
                    this.$http.post(url, data).then((response) => {
                        if (response.data.state === 200) {
                            this.gemName[this.newPopupIndex].childrenList.push({
                                "classesName": addName
                            })
                            alert("添加成功")
                            console.log(addName)
                            this.cancelDialog()
                            this.productList()
                        } else {
                            alert(response.data.msg);
                        }
                        console.log("添加类别");
                        console.log(response);
                        // this.productTypeList[index].typeList.splice(i, 1);
                    }, (response) => {
                        console.log(response);
                    })
                }
            }
        },
        delProduct (index, i) { // 小类删除
            var data = {
                "data": {
                    "list": {
                        "childrenList": [
                            {
                            "classesId": this.gemName[index].childrenList[i].classesId // 子级id
                            }
                        ],
                        "classesId": this.gemName[index].classesId,
                        "classesName": this.gemName[index].classesName
                    },
                    "topClasses": "2", // 删除：1最高级2子级
                    "type": "4" // 1成色名类2宝石名称3首饰类别 4 宝石属性
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/delProductClasses";
            this.$http.post(url, data).then((response) => {
                if (response.data.state === 200) {
                    this.gemName[index].childrenList.splice(i, 1);
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
        cancelDialog () {  // 取消弹框
            this.popup.newPopup = false
            this.ruleForm.inputProductName = ''
        }
    }
}
</script>

<style src="./css/header.scss" lang="scss" scoped></style>
<style src="./css/addCol6.scss" lang="scss" scoped></style>
