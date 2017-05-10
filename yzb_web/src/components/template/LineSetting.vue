<template>
    <div class="line-setting">
        <div class="setting-title">
            <span class="setting-span1">基本元件：</span><span class="setting-span2">直线</span>
        </div>
        <div class="setting-body">
            <el-form label-width="80px" label-position="left">
                <el-form-item label="长度">
                    <LengthInput :length="data.width" @change="(value)=>{this.data.width = value}"></LengthInput>
                </el-form-item>
                <el-form-item label="线高">
                    <LengthInput :length="data.height" @change="(value)=>{this.data.height = value}"></LengthInput>
                </el-form-item>
                <el-form-item label="横轴">
                    <LengthInput :length="data.left" @change="(value)=>{this.data.left = value}"></LengthInput>
                </el-form-item>
                <el-form-item label="竖轴">
                    <LengthInput :length="data.top" @change="(value)=>{this.data.top = value}"></LengthInput>
                </el-form-item>
                <div class="el-form-item">
                    <label class="el-label">线条颜色</label><input type="text" class="color" v-model="data.color"><el-color-picker v-model="data.color"></el-color-picker>
                </div>
            </el-form>
        </div>
    </div>
</template>
<script>
import Vue from 'vue'
import {
    Form,
    FormItem,
    ColorPicker
} from 'element-ui'
import LengthInput from './LengthInput'

Vue.use(Form)
Vue.use(FormItem)
Vue.use(ColorPicker)

export default {
    components: {
        LengthInput
    },
    data() {
        return {
            ready: false,
            data: {
                left: 0,
                top: 0,
                width: 0,
                height: 0,
                color: '#000'
            }
        }
    },
    watch: {
        data: {
            handler(data) {
                if (this.ready) {
                    this.$emit('changeComponentSetting', data)
                }
            },
            deep: true
        }
    },
    mounted() {
        this.$on('set_data', data => {
            this.ready = false
            let dataClone = JSON.parse(JSON.stringify(data))
            for (let key in dataClone) {
                if (this.data[key] !== undefined) {
                    this.data[key] = dataClone[key]
                }
            }
            Vue.nextTick(() => {
                this.ready = true
            })
        })
    }
}
</script>

<style lang="scss">
@import "~assets/css/template/mixin.scss";
@import "~assets/css/template/fonts.scss";
@import "~assets/css/template/colors.scss";
.line-setting {
    .setting-title {
        padding: 0 20px;
        line-height: 44px;
        .setting-span1 {
            margin-right: 10px;
        }
        .setting-span2 {
            color: #0abfab;
        }
    }
    .setting-body {
        padding-top: 16px;
        padding-bottom: 10px;
        width: 190px;
        margin: 0 auto;
        border-top: 1px solid #d6d6d6;
        .el-form-item {
            height: 26px;
            margin-bottom: 14px;
            &:last-child {
                margin-bottom: 0;
            }
            .el-form-item__content {
                line-height: 26px;
                .el-input {
                    .el-input__inner {
                        width: 66px;
                        height: 26px;
                    }
                    .el-input-group__append {
                        width: 44px;
                    }
                }
            }
            label {
                @include text-align-justify;
                font-size: 14px;
                padding: 6px 24px 6px 0;
            }
            .color {
                width: 88px;
                height: 26px;
                box-sizing: border-box;
                padding: 3px 10px;
                @include BD1;
                border-right: 0;
                border-radius: 4px;
                border-top-right-radius: 0;
                border-bottom-right-radius: 0;
                vertical-align: middle;
            }
            .el-color-picker {
                vertical-align: middle;
                .el-color-picker__trigger {
                    display: block;
                    padding: 0;
                    height: 26px;
                    border-left: 0;
                    border-radius: 4px;
                    border-top-left-radius: 0;
                    border-bottom-left-radius: 0;
                    overflow: hidden;
                    .el-color-picker__color {
                        width: 21px;
                        height: 24px;
                        border: none;
                    }
                    .el-color-picker__icon {
                        display: none;
                    }
                }
            }
        }
    }
}
</style>