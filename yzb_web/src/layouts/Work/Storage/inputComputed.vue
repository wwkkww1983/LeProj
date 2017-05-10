<template>
  <div>
    <!-- 只限制输入 -->
    <input ref="input" v-bind:value="defaultValue" v-on:input="soldPriceComputed($event.target.value)" v-on:blur="focusComputed">
  </div>
</template>
<script>
export default {
  props: ['littleClassKey', 'costPrice', 'ratio', 'soldPrice'], // 小类名，成本，倍率，售价
  mounted: function () {
      this.initValue();
  },
  data () {
      return {
        defaultValue: 0
      }
  },
  watch: {
      ratio: function() {
          if (this.littleClassKey === "soldPrice") { // 倍率改变
              console.log("倍率改变啦");
              console.log("监听9");
              console.log(this.costPrice);
              console.log(this.ratio);
              if (!Number(this.costPrice) * Number(this.ratio) || Number(this.costPrice) * Number(this.ratio) === Infinity) { // 如果计算不合法
                  return;
              }
              this.defaultValue = Number(this.costPrice) * Number(this.ratio);
              this.$emit('input', Number(this.defaultValue));
          }
          return;
      },
      soldPrice: function () { // 售价改变
          if (this.littleClassKey === "ratio") {
              console.log("售价改变啦");
              console.log(this.soldPrice);
              console.log(this.costPrice);
              if (!Number(this.soldPrice) / Number(this.costPrice) || Number(this.soldPrice) / Number(this.costPrice) === Infinity) { // 如果计算不合法
                  console.log("9999999999999999999999999999999999999999999999999999")
                  return;
              }
              console.log("ppppppppppppppppppp");
              console.log(Number(this.soldPrice) / Number(this.costPrice));
              this.defaultValue = Number(this.soldPrice) / Number(this.costPrice);
              console.log("监听2")
              console.log(this.defaultValue);
              this.$emit('input', Number(this.defaultValue));
          }
          return;
      }
  },
  methods: {
      initValue () { // 初始化输入值
          switch (this.littleClassKey) {
              case 'ratio': // 倍率
                  if (this.ratio) {
                      console.log("有倍率");
                      this.defaultValue = Number(this.ratio);
                  } else {
                      console.log("没有倍率");
                      if (!Number(this.soldPrice) / Number(this.costPrice) || Number(this.costPrice) * Number(this.ratio) === Infinity) { // 如果计算不合法
                          return;
                      }
                      this.defaultValue = Number(this.soldPrice) / Number(this.costPrice);
                  }
                  break;
              case 'soldPrice': // 售价
                  if (this.soldPrice) {
                      console.log("有售价");
                      this.defaultValue = this.soldPrice;
                  } else {
                      console.log("没有售价");
                      console.log(this.costPrice);
                      console.log(this.ratio);
                      this.defaultValue = Number(this.costPrice) * Number(this.ratio);
                  }
                  break;
          }
          this.$emit('input', Number(this.defaultValue));
      },
      soldPriceComputed: function (value) { // 只限制输入字数
          console.log(value);
          if (value) {
            this.$refs.input.value = value;
            this.$emit('input', value)
          } else {
            this.$refs.input.value = null;
            this.$emit('input', null)
          }
      },
      focusComputed: function (value) { // 离开检测
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
    height: 20px;
    text-align: right;
    border: none;
    outline: none;
    background-color: transparent;
  }
</style>