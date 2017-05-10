<template>
    <input class="form-input" v-if="pureNumber" ref="input" v-bind:value="value" v-on:input="updatePureNumber($event.target.value)" :maxlength="maxLengthSix">
</template>
<script>
    export default {
        props: ['value', 'pureNumber', 'maxLengthSix'], // 元素值，纯数字，最大输入值
        methods: {
            updatePureNumber: function (value) { // 纯数
                if (!/^[0-9]+$/i.test(value)) {
                    this.$refs.input.value = value.slice(0, value.length - 1);
                    this.$emit('input', null);
                } else {
                    this.$refs.input.value = value;
                    this.$emit('input', value);
                }
            }
        }
    }
</script>