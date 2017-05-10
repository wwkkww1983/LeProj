<template>
<div class="item-box">
    <div class="item-title">
        <span>库位设置</span>
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
            <el-radio-group v-model="defaultSetting" @change="selectDeSet">
                <li v-for="(item, index) in PartsNameList" >
                    {{item.repositoryName}}
                    <i class="el-icon-circle-close i-close" v-if="isEidt" @click="delProduct(index)"></i>
                    <span v-if="item.isDefault==='Y'">
                        <el-radio class="radio" :label="item.repositoryId">默认库位</el-radio>
                    </span>
                    <span v-else>
                        <el-radio class="radio" :label="item.repositoryId">设为默认</el-radio>
                    </span>
                </li>
            </el-radio-group>
            <li v-if="isEidt" class="addLi" @click="newStockIn">
                <i class="el-icon-plus i-add"></i>
            </li>
        </ul>
    </div>
    <div class="dialog-w380">
        <el-dialog title="新增库位" v-model="popup.newPopup">
            <p class="m20">
                <el-input v-model="inputPartsName" placeholder="请输入库位" v-bind:maxlength="7"></el-input>                        
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
        return {
            PartsNameList: null,
            isEidt: false,
            isShow: true,
            popup: { // 弹窗
                newPopup: false // 产品类型
            },
            inputPartsName: '',
            defaultSetting: ''
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/showRepositoryList";
            this.$http.post(url, data).then((response) => {
                console.log("库位设置");
                console.log(response.data.data.repositoryList);
                this.PartsNameList = response.data.data.repositoryList;
                let temp = this.PartsNameList.find(v => v.isDefault === 'Y')
                this.defaultSetting = temp.repositoryId
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
        affirmNew () {
            if (this.inputPartsName === '') {
                this.$message('此处不能为空')
            } else {
                let isAdd = this.PartsNameList.find((v) => v.classesName === this.inputPartsName)
                if (isAdd !== undefined) {
                    this.$message('该产品类别已存在')
                } else {
                    this.PartsNameList.push({
                        classesName: this.inputPartsName
                    })
                    this.cancelDialog()
                }
            }
        },
        delProduct (index) {
            let isDel = true
            if (isDel) {
                this.PartsNameList.splice(index, 1)
            } else {
                this.$message('有商品使用了该数据，不能删除')
            }
        },
        cancelDialog () {
            this.popup.newPopup = false
            this.inputPartsName = ''
        },
        selectDeSet (radio) {
            let data = {
                "data": {
                    "repositoryIdList": [
                        {"repositoryId": radio}
                    ], // 库位ID 列表
                    "handleType": "2" // 操作类型:1 删除 2 设置默认
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
            var url = INTERFACE_URL_9083 + "/v1/headquarter/delRepository";
            this.$http.post(url, data).then((response) => {
                if (response.data.state === 200) {
                    let temp = this.PartsNameList.find(v => v.isDefault === 'Y')
                    temp.isDefault = 'N'
                    let tempChange = this.PartsNameList.find(v => v.repositoryId === radio)
                    tempChange.isDefault = 'Y'
                } else {
                    alert(response.data.msg);
                }
                // this.popup.editNamePopup = false
                // this.inputEidtName = ''
                // console.log("添加类别");
                // console.log(response);
            }, (response) => {
                console.log(response);
            })
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
        div{
            margin-top: 20px;
            padding: 0 20px;
            li{
                width: 240px;
                height: 36px;
                line-height: 36px;
                border: 1px solid $d6;
                margin-right: 13px;
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
            padding-left: 20px;
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
        }
        .addLi{
            border: 1px solid #00c0ab;
        }
    }
}
</style>
