<template>
    <input class="form-input" v-if="integer" ref="input" v-bind:value="value" v-on:input="updateInteger($event.target.value)" :maxlength="maxLengthSix">
</template>
<script>
    export default {
        props: ['value', 'integer', 'maxLengthSix'], // 元素值，纯数字，最大输入值
        methods: {
            updateInteger: function (value) { // 取整数
                if (value) {
                    this.$refs.input.value = parseInt(value);
                    this.$emit('input', parseInt(this.$refs.input.value));
                } else {
                    this.$refs.input.value = null;
                    this.$emit('input', null);
                }
            }
        }
    }
</script>