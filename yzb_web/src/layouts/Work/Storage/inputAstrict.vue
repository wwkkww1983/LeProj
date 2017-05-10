<template>
  <div>
    <template v-if="onlyMaxlength">
        <!-- 纯数字 -->
        <input v-if="pureNumber" ref="input" v-bind:value="value" v-on:input="updatePureNumber($event.target.value)" :maxlength="maxLengthSix">
        <!-- 整数 -->
        <input v-if="integer" ref="input" v-bind:value="value" v-on:input="updateInteger($event.target.value)" v-bind:maxlength="maxLengthSix">
        <!-- 字符串 -->
        <input v-if="isString" ref="input" v-bind:value="value" v-on:input="updateString($event.target.value)">
        <!-- 小数 -->
        <input v-if="sliceNumber" ref="input" v-bind:value="defaultValue" v-on:input="updateValue($event.target.value)" v-on:focus="selectAll" v-on:blur="formatValue($event.target.value)" v-bind:maxlength="maxLengthSix">
    </template>
    <!-- 只限制输入 -->
    <input v-else ref="input" v-bind:value="value" v-on:input="updateOnlyMaxlength($event.target.value)">
  </div>
</template>
<script>
export default {
  props: ['value', 'sliceNumber', 'integer', 'pureNumber', 'maxLengthSix'], // 元素值，小数，整数，纯数字，最大输入值
  mounted: function () {
    this.initValue();
  },
  data () {
    return {
      defaultValue: 0
    }
  },
  computed: {
    isString () {
      if (this.sliceNumber || this.pureNumber) { // 避免到小数点时成字符串
        return false
      } else {
        return typeof (this.value) === "string"
      }
    },
    onlyMaxlength () { // 只有限制长度的情况下(证书号...)
      if (this.sliceNumber || this.integer || this.pureNumber || this.isString) {
        return true
      }
      return false;
    },
    sliceNumbers () {
      if (this.sliceNumber) {
        return true
      } else {
        return false
      }
    }
  },
  methods: {
    initValue () { // 初始化输入值
      if (this.onlyMaxlength) {
        if (this.integer) { // 整数
          if (this.value) {
              this.defaultValue = parseInt(Number(this.value));
          } else {
              this.defaultValue = null;
          }
        } else if (this.isString) { // 字符串
            this.defaultValue = this.value;
        } else { // 小数
          if (this.value && this.sliceNumber) {
            this.defaultValue = Number(this.value).toFixed(this.sliceNumber);
          } else {
            this.defaultValue = this.defaultValue.toFixed(this.sliceNumber);
          }
        }
      } else { // 只限制输入值得情况
          if (this.value) {
              this.defaultValue = this.value;
          } else {
              this.defaultValue = null;
          }
      }
    },
    updatePureNumber: function (value) { // 纯数
      console.log("来了")
      if (!/^[0-9]+$/i.test(value)) {
          this.$refs.input.value = value.slice(0, value.length - 1);
          this.$emit('input', null);
      } else {
          this.$refs.input.value = value;
          this.$emit('input', value);
      }
    },
    updateString: function (value) { // 字符串
      if (value) {
          this.$emit('input', value);
      }
    },
    updateInteger: function (value) { // 取整数
      if (value) {
          this.$refs.input.value = parseInt(value);
          this.$emit('input', parseInt(this.$refs.input.value));
      } else {
          this.$refs.input.value = null;
          this.$emit('input', null);
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
    },
    updateOnlyMaxlength: function (value) { // 只限制输入字数
      console.log(value);
      if (value) {
        this.$refs.input.value = value;
        this.$emit('input', value)
      } else {
        this.$refs.input.value = null;
        this.$emit('input', null)
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
<style lang="scss" scoped>
  div{
    width: 118px;
  }
  input {
    padding: 10px;
    width: 100%;
    height: 20px;
    text-align: right;
    border: none;
    outline: none;
    background-color: transparent;
  }
</style>