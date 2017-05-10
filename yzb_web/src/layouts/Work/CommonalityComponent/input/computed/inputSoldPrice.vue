<template>
    <div>
        <input ref="input" v-bind:value="defaultValue" v-on:input="soldPriceComputed($event.target.value)" v-on:blur="focusComputed($event.target.value)">
    </div>
</template>
<script>
export default {
  props: ['costPrice', 'ratio', 'soldPrice'], // 成本，倍率，售价
  mounted () {
      this.initValue();
  },
  data () {
      return {
          sliceNumber: 2,
          defaultValue: 0
      }
  },
  watch: {
      'ratio': function () { // 监听倍率
          let endNumber = (Number(this.costPrice) || 0) * (Number(this.ratio) || 0); // 售价 = 成本 * 倍率
          this.defaultValue = endNumber.toFixed(2);
          this.$refs.input.value = endNumber;
          this.$emit('input', endNumber);
      }
  },
  methods: {
      initValue () { // 初始化输入值
          if (this.costPrice) {
              let endNumber = (Number(this.costPrice) || 0) * (Number(this.ratio) || 0);
              if (endNumber === 0) {
                  this.defaultValue = "0.00";
              } else {
                  this.defaultValue = endNumber;
              }
              this.$emit('input', this.defaultValue);
          } else {
              this.defaultValue = parseFloat(0).toFixed(2);
          }
      },
      soldPriceComputed: function (value) { // 只限制输入字数
        // console.log("成本输入检测");
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
      focusComputed: function (value) { // 离开检测
        console.log(value);
        console.log("离开检测99");
        console.log(typeof value)
          if (value) {
              this.$refs.input.value = parseFloat(value).toFixed(2);
          } else {
              this.$refs.input.value = parseFloat(0).toFixed(2);
          }
          console.log(typeof this.$refs.input.value)
          this.$emit('input', Number(this.$refs.input.value));
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
    height: 34px;
    text-align: right;
    border: none;
    outline: none;
    background-color: transparent;
  }
</style>