<template>
<div class="item-box">
    <div class="item-title">
        <span>证书</span>
        <div class="buttons">
            <el-button type="text" class="text-info"
            v-show="isShow"  @click="editFun">编辑</el-button>
            <el-button type="success" size="small" 
            v-show="!isShow" @click="finishFun">完成</el-button>
        </div>
    </div>
    <div class="item-content">
        <ul>
            <li v-for="(item, index) in CertificateList">
                {{item.certificateName}}
                <i class="el-icon-circle-close i-close" v-if="isEidt" @click.self.stop="delProduct(index)"></i>                
                <i class="el-icon-edit text-info" v-if="isEidt" @click="lookCertificate(index)"></i>
            </li>
            <li v-if="isEidt" class="addLi" @click="addCertificate">
                <i class="el-icon-plus i-add"></i>
            </li>
        </ul>
    </div>
    <div class="dialog-w380-h520">
        <el-dialog title="证书" v-model="popup.newPopup">
            <p class="m20">
                <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
                    <el-form-item label="证 书 名" prop="certificateName">
                        <el-input v-model="ruleForm.certificateName" :disabled="prohibitUse"></el-input>
                    </el-form-item>
                    <el-form-item label="检测机构" prop="organizationName">
                        <el-input v-model="ruleForm.organizationName" :disabled="prohibitUse"></el-input>
                    </el-form-item>
                    <el-form-item label="网　　 址" prop="website">
                        <el-input v-model="ruleForm.website" :disabled="prohibitUse"></el-input>
                    </el-form-item>
                    <el-form-item label="电　　 话" prop="phone">
                        <el-input v-model="ruleForm.phone" :disabled="prohibitUse"></el-input>
                    </el-form-item>
                    <el-form-item label="检测标准 1" prop="stand1">
                        <el-input v-model="ruleForm.stand1" :disabled="prohibitUse"></el-input>
                    </el-form-item>
                    <el-form-item label="检测标准 2" prop="stand2">
                        <el-input v-model="ruleForm.stand2" :disabled="prohibitUse"></el-input>
                    </el-form-item>
                    <el-form-item label="检测标准 3" prop="stand3">
                        <el-input v-model="ruleForm.stand3" :disabled="prohibitUse"></el-input>
                    </el-form-item>
                </el-form>                       
            </p>
            <span slot="footer">
                <a href="javascript: null" class="confirm-btn-lg" v-if="prohibitUse" @click="editCertificate">编辑</a>
                <a href="javascript: null" class="cancel-btn-w120" v-if="!prohibitUse" @click="cancelDialog">取 消</a>
                <a href="javascript: null" class="confirm-btn-w120 mr0" v-if="!prohibitUse" @click="affirmEditCertificate" >确 定</a>
            </span>
        </el-dialog>
    </div>
</div>
</template>

<script>
export default {
    data () {
        return {
            CertificateList: null,
            isEidt: false, // 是否编辑
            isShow: true, // 是否显示编辑
            isDel: false, // 是否删除
            isDelShow: true, // 是否显示删除
            popup: { // 弹窗
                newPopup: false // 产品类型
            },
            certificateIdIndex: null,
            prohibitUse: true,
            ruleForm: {
                certificateName: '',
                organizationName: '',
                website: '',
                phone: '',
                stand1: '',
                stand2: '',
                stand3: ''
            },
            rules: {
                certificateName: [
                    { required: true, message: '请输入证书名', trigger: 'blur' }
                ],
                organizationName: [
                    { required: true, message: '请输入检测机构', trigger: 'blur' }
                ],
                website: [
                    { required: true, message: '请输入网址', trigger: 'blur' }
                ],
                phone: [
                    { required: true, message: '请输入电话', trigger: 'blur' }
                ],
                stand1: [
                    { required: true, message: '请输入检测标准1', trigger: 'blur' }
                ],
                stand2: [
                    { required: true, message: '请输入检测标准2', trigger: 'blur' }
                ],
                stand3: []
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showhCertificateList";
            this.$http.post(url, data).then((response) => {
                console.log("证书");
                console.log(response.data.data.list);
                this.CertificateList = response.data.data.list;
            }, (response) => {
                console.log(response);
            })
        },
        addFun () { // 添加按钮
            this.popup.addPopup = true;
            this.isEidt = false;
            this.isShow = !this.isEidt;
            this.isDel = false;
            this.isDelShow = !this.isDel;
        },
        editFun () { // 编辑按钮
            this.isEidt = true;
            this.isShow = !this.isEidt;
            this.isDel = false;
            this.isDelShow = !this.isDel;
        },
        finishFun () { // 编辑完成
            this.isEidt = false;
            this.isShow = !this.isEidt;
        },
        // delFun () { // 删除按钮
        //     this.isDel = true;
        //     this.isDelShow = !this.isDel;
        //     this.isEidt = false;
        //     this.isShow = !this.isEidt;
        // },
        // finishDelFun () { // 删除完成
        //     this.isDel = false;
        //     this.isDelShow = !this.isDel;
        // },
        newStockIn () { // 新建产品类别
            this.inputCertificateName = '';
            this.popup.newPopup = true;
        },
        lookCertificate (index) { // 查看证书
            this.popup.newPopup = true;
            this.handleType = 2
            this.ruleForm.certificateName = this.CertificateList[index].certificateName
            this.ruleForm.organizationName = this.CertificateList[index].organizationName
            this.ruleForm.website = this.CertificateList[index].website
            this.ruleForm.phone = this.CertificateList[index].phone
            this.ruleForm.stand1 = this.CertificateList[index].stand1
            this.ruleForm.stand2 = this.CertificateList[index].stand2
            this.ruleForm.stand3 = this.CertificateList[index].stand3
            this.certificateIdIndex = index
        },
        editCertificate () { // 编辑证书
            this.prohibitUse = false;
        },
        affirmEditCertificate () {
            switch (this.handleType) {
                case 1:
                    this.affirmNew()
                    break
                case 2:
                    this.ajaxPutCertificate()
                    break
            }
        },
        ajaxPutCertificate () { // 修改
            var data = {
                "data": {
                    "handleType": 2, // 1 新增 2 修改
                    "list": [
                        {
                        "certificateId": this.CertificateList[this.certificateIdIndex].id, // 证书ID    （修改时有）
                        "organizationName": this.ruleForm.organizationName, // 机构名称
                        "certificateName": this.ruleForm.certificateName, // 证书名称
                        "website": this.ruleForm.website, // 网址
                        "phone": this.ruleForm.phone, // 电话
                        "stand1": this.ruleForm.stand1, // 检测标准1
                        "stand2": this.ruleForm.stand2, // 检测标准2
                        "stand3": this.ruleForm.stand3 // 检测标准3
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/addHCertificateList";
            sessionStorage.setItem("isa", JSON.stringify(data));
            this.$http.post(url, data).then((response) => {
                if (response.data.state === 200) {
                    let obj = this.CertificateList[this.certificateIdIndex]
                    obj.certificateId = obj.certificateId
                    obj.organizationName = this.ruleForm.organizationName
                    obj.certificateName = this.ruleForm.certificateName
                    obj.website = this.ruleForm.website
                    obj.phone = this.ruleForm.phone
                    obj.stand1 = this.ruleForm.stand1
                    obj.stand2 = this.ruleForm.stand2
                    obj.stand3 = this.ruleForm.stand3
                    alert("添加成功")
                } else {
                    alert(response.data.msg);
                }
                this.reset()
                this.prohibitUse = true;
                console.log(response);
                // this.productTypeList[index].typeList.splice(i, 1);
            }, (response) => {
                console.log(response);
            })
        },
        addCertificate () { // 添加证书
            this.handleType = 1
            this.prohibitUse = false;
            this.popup.newPopup = true;
        },
        reset () { // 重置
                this.popup.newPopup = false;
                this.ruleForm.certificateName = ''
                this.ruleForm.organizationName = ''
                this.ruleForm.website = ''
                this.ruleForm.phone = ''
                this.ruleForm.stand1 = ''
                this.ruleForm.stand2 = ''
                this.ruleForm.stand3 = ''
        },
        affirmNew () { // 确认添加
            console.log(55555555555)
            var data = {
                "data": {
                    "handleType": 1, // 1 新增 2 修改
                    "list": [
                        {
                        "organizationName": this.ruleForm.organizationName, // 机构名称
                        "certificateName": this.ruleForm.certificateName, // 证书名称
                        "website": this.ruleForm.website, // 网址
                        "phone": this.ruleForm.phone, // 电话
                        "stand1": this.ruleForm.stand1, // 检测标准1
                        "stand2": this.ruleForm.stand2, // 检测标准2
                        "stand3": this.ruleForm.stand3 // 检测标准3
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
            sessionStorage.setItem("test", JSON.stringify(data));
            var url = INTERFACE_URL_9083 + "/v1/headquarter/addHCertificateList";
            this.$http.post(url, data).then((response) => {
                if (response.data.state === 200) {
                    this.CertificateList.push({
                        "organizationName": this.ruleForm.organizationName, // 机构名称
                        "certificateName": this.ruleForm.certificateName, // 证书名称
                        "website": this.ruleForm.website, // 网址
                        "phone": this.ruleForm.phone, // 电话
                        "stand1": this.ruleForm.stand1, // 检测标准1
                        "stand2": this.ruleForm.stand2, // 检测标准2
                        "stand3": this.ruleForm.stand3 // 检测标准3
                    })
                    this.prohibitUse = true;
                    this.reset()
                    alert("添加成功")
                } else {
                    alert(response.data.msg);
                }
                console.log(response);
                // this.productTypeList[index].typeList.splice(i, 1);
            }, (response) => {
                console.log(response);
            })
        },
        cancelDialog () {
            this.popup.newPopup = false
            this.inputCertificateName = ''
        },
        delProduct (index) {
            this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消'
            }).then(() => {
                this.ajaxDeleteProduct(index)
            }).catch(() => {
                this.$message({
                    type: 'info',
                    message: '已取消删除'
                })
            })
        },
      ajaxDeleteProduct (index) {
        var data = {
            "data": {
                "handleType": 1,
                "id": this.CertificateList[index].id
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
        var url = INTERFACE_URL_9083 + "/v1/headquarter/delHCertificate";
        console.log(555555555555)
        this.$http.post(url, data).then((response) => {
            if (response.data.state === 200) {
                this.CertificateList.splice(this.certificateIdIndex, 1)
            } else {
                alert(response.data.msg);
            }
            console.log(response);
            // this.productTypeList[index].typeList.splice(i, 1);
        }, (response) => {
            console.log(response);
        })
        this.prohibitUse = true;
      }
    }
}
</script>

<style src="./css/header.scss" lang="scss" scoped></style>
<style lang="scss" scoped>
$f0: #f0f0f0; // 目录下边框颜色
$a2: #222324; // 目录文字颜色
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
        padding-left: 20px;
        >li{
            width: 240px;
            height: 36px;
            line-height: 36px;
            border: 1px solid $d6;
            margin-right: 16px;
            position: relative;
            font-size: 14px;
            display: inline-block;
            span{
                position: absolute;
                right: 10px;
                .el-radio__label{
                    font-size: 10px;
                }
            }
        }
        li:last-child{
            margin-right: 0;
        }
    }
    ul{
        padding-bottom: 20px;
        >li{
            height: 35px;
            line-height: 35px;
            border: 1px solid $d6;
            border-radius: 4px;
            text-align: center;
            display: inline-block;
            margin-top: 20px;
            position: relative;
            cursor: pointer;
            i.i-close{
                position: absolute;
                top: -7px;
                right: -7px;
            }
            i.i-add{
                color: #00c0ab;
            }
            i.el-icon-edit.text-info{
                position: absolute;
                top: 0;
                right: 10px;
                text-size: 14px;
                height: 35px;
                line-height: 35px;
            }
            i.el-icon-edit.text-info::before{
                color: #00c0ab;
            }
        }
        .addLi{
            border: 1px solid #00c0ab;
        }
    }
}
</style>
