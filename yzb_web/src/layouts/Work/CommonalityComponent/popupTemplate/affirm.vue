<template>
    <el-dialog class="dialog-w540-h320-hn" title="选择产品类型" v-model="saveSuccess">
        <ul class="body-wrap">
            <li>保存成功</li>
        </ul>
    </el-dialog>
</template>
<script>
export default {
    props: [
        'saveSuccessPopup'
    ],
    data () {
        return {
            "saveSuccess": false
        }
    },
    created () {
        this.saveSuccess = this.saveSuccessPopup;
    },
    watch: {
        'saveSuccessPopup': function () {
            if (this.saveSuccessPopup) {
                this.saveSuccess = this.saveSuccessPopup
            } else {
                this.saveSuccess = false
            }
        }
    }
}
</script>
<style lang="scss" scoped>
.body-wrap{
    padding: 0 30px;
    margin-top: 60px;
    li{
        font-size: 14px;
        line-height: 30px;
        color: #333;
        text-align: center;
    }
}
</style>