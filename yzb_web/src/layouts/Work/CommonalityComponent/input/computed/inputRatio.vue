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
      'soldPrice': function () { // 监听售价
          let endNumber = (Number(this.soldPrice) || 0) / (Number(this.costPrice) || 0);
          // console.log("售价改变了");
          // console.log(Number(this.soldPrice));
          // console.log(Number(this.costPrice));
          // console.log(endNumber);
          this.$refs.input.value = endNumber;
          this.$emit('input', Number(endNumber))
      }
  },
  methods: {
      initValue () { // 初始化输入值
        console.log("初始化");
        console.log(this.costPrice);
        console.log(this.ratio);
        console.log(this.soldPrice);
          let endNumber = ((Number(this.soldPrice) || 0) / (Number(this.costPrice) || 0)) || "0.00"; // 倍率 = 售价 / 成本
          this.defaultValue = endNumber;
          this.$refs.input.value = endNumber;
          this.$emit('input', endNumber);
          // this.$refs.input.value = Number(this.ratio);
          // this.$emit('input', Number(this.ratio));
          // if (this.ratio) {
          //     this.defaultValue = Number(this.ratio).toFixed(2);
          // } else {
          //     this.defaultValue = parseFloat(0).toFixed(this.sliceNumber);
          // }
      },
      soldPriceComputed: function (parm) { // 只限制输入字数
          if (parm.indexOf('.') !== -1) {
              this.$refs.input.value = parm.trim().slice(0, parm.indexOf('.') + this.sliceNumber + 1);
              this.$emit('input', parseFloat(this.$refs.input.value).toFixed(this.sliceNumber));
          } else if (!parm) {
              this.$emit('input', parseInt(0).toFixed(this.sliceNumber));
          } else if (!/^[.0-9]+$/i.test(parm)) { // 过滤不是数字和小数点的值
              this.$refs.input.value = parm.trim().slice(0, parm.length - 1);
              this.$emit('input', this.$refs.input.value)
          } else if (parm) {
              this.$emit('input', parseInt(parm).toFixed(this.sliceNumber));
          }
      },
      focusComputed: function (value) { // 离开检测
          if (value) {
              this.$refs.input.value = parseFloat(value).toFixed(this.sliceNumber);
              this.$emit('input', this.$refs.input.value);
          } else {
              this.$refs.input.value = parseFloat(0).toFixed(this.sliceNumber);
              this.$emit('input', this.$refs.input.value);
          }
          console.log("离开检测")
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