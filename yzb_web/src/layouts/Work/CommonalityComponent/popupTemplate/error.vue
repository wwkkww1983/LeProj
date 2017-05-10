<template>
    <div class="error-popup">
        <div class="dialog-w320-h220-hn">
            <el-dialog title="" v-model="popupError">
                <ul class="body-wrap">
                    <li>{{popupError}}</li>
                </ul>
                <span slot="footer">
                    <a href="javascript: null" class="confirm-btn-w120 mr0" @click="workPopupError(false)">确 定</a>
                </span>
            </el-dialog>
        </div>
    </div>
</template>
<script>
import {mapGetters, mapActions} from 'vuex'
export default {
    computed: {
        ...mapGetters([
            "popupError"
        ])
    },
    methods: {
        ...mapActions([
            "workPopupError"
        ])
    }
}
</script>
<style lang="scss" scoped>
    .error-popup{
        position: relative;
        .body-wrap{
            padding: 0 30px;
            margin-top: 50px;
            li{
                margin-top: 70px;
                font-size: 14px;
                line-height: 30px;
                color: #333;
                text-align: center;
            }
        }
    }
</style>