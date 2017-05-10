<template>
    <input class="form-input" v-if="sliceNumber" ref="input" v-bind:value="defaultValue" :maxlength="maxLengthSix" v-on:input="updateValue($event.target.value)" v-on:blur="formatValue($event.target.value)">
</template>
<script>
    export default {
        props: ['value', 'sliceNumber', 'maxLengthSix'], // 元素值，小数，最大输入值
        // mounted () {
        //     console.log("重量的抗");
        //     console.log(this.value);
        //     if (!this.value) {
        //         this.$refs.input.value = 0.00;
        //     }
        // },
        data () {
            return {
                defaultValue: 0
            }
        },
        mounted: function () {
            this.initValue();
        },
        methods: {
            initValue () { // 初始化输入值
                if (this.value) {
                    this.defaultValue = this.value;
                } else {
                    this.defaultValue = parseFloat(0).toFixed(this.sliceNumber);
                }
            },
            updateValue: function (value) { // 取小数
                if (value.indexOf('.') !== -1) {
                    this.$refs.input.value = value.trim().slice(0, value.indexOf('.') + this.sliceNumber + 1);
                    this.$emit('input', parseFloat(this.$refs.input.value).toFixed(this.sliceNumber));
                } else if (!value) {
                    this.$emit('input', parseInt(0).toFixed(this.sliceNumber));
                } else if (!/^[.0-9]+$/i.test(value)) { // 过滤不是数字和小数点的值
                    this.$refs.input.value = value.trim().slice(0, value.length - 1);
                    this.$emit('input', this.$refs.input.value)
                } else if (value) {
                    this.$emit('input', parseInt(value).toFixed(this.sliceNumber));
                }
            },
            formatValue: function (value) { // 取小数
                if (value) {
                    this.$refs.input.value = parseFloat(value).toFixed(this.sliceNumber);
                } else {
                    this.$refs.input.value = parseFloat(0).toFixed(this.sliceNumber);
                }
            }
        }
    }
</script>