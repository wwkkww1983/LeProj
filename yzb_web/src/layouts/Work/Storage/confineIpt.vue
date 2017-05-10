<template>
  <div>
  <p>{{maxLengthSix}}</p>
    <label v-if="label">{{ sliceNumber }}</label>
    <input v-if="integer" ref="input" v-bind:value="value" v-on:input="updateInteger($event.target.value)" v-bind:maxlength="maxLengthSix">
    <input v-else ref="input" v-bind:value="defaultValue" v-on:input="updateValue($event.target.value)" v-on:focus="selectAll" v-on:blur="formatValue($event.target.value)" v-bind:maxlength="maxLengthSix">
  </div>
</template>
<script>
export default {
  props: ['value', 'sliceNumber', 'integer', 'maxLengthSix'], // 元素值，小数，整数，最大输入值
  mounted: function () {
    this.initValue();
  },
  data () {
    return {
      defaultValue: 0
    }
  },
  methods: {
    initValue () { // 初始化输入值
      if (this.integer) {
        this.defaultValue = parseInt(this.defaultValue);
      } else {
        if (this.value) {
          this.defaultValue = this.value.toFixed(this.sliceNumber);
        } else {
          this.defaultValue = this.defaultValue.toFixed(this.sliceNumber);
        }
      }
    },
    updateInteger: function (value) { // 取整数
      if (value) {
        this.$refs.input.value = parseInt(value);
        this.$emit('input', parseInt(this.$refs.input.value));
      } else {
        this.$refs.input.value = parseInt(0);
        this.$emit('input', parseInt(0));
      }
    },
    updateValue: function (value) { // 取小数
      console.log(value)
      // var NumberOrDot = /^[.0-9]+$/i;
      if (value.indexOf('.') !== -1) {
        this.$refs.input.value = value.trim().slice(0, value.indexOf('.') + parseInt(this.sliceNumber) + 1);
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
    },
    selectAll: function (event) { // Workaround for Safari bug
      setTimeout(function () {
        event.target.select()
      }, 0)
    }
  }
}
</script>
<style lang="less">
</style>
