<template>
    <div id="wl_faces_box" class="wl_faces_box">
        <ul id="emotionUL">
            <li v-for="item in emotionsList">
                <img  :id="item.id" v-on:click.stop="selectEmotionImg(item.id)" :src="item.src" :style="item.style" alt="">
            </li>
        </ul>
    </div>
</template>

<script>
  export default {
    data() {
      return {
        imgList: [] // 表情data
      }
    },
    props: [
      'msgtosend',
      'emotionsList'
    ],
    methods: {
      selectEmotionImg(id) {
        this.$emit('increment', id)
      }
    },
    created() {

      this.emotionFlag = true
    }
  }
</script>

<style lang="scss">
  .wl_faces_box {
    position: absolute;
    width: 500px;
    height: 280px;
    bottom: 28px;
    left: -3px;
    padding: 10px;
    border: 1px solid #b0b1b5;
    box-shadow: 0 0 3px #b0b1b5;
    background: #ffffff;
  }

  #emotionUL {
    width: 100%;
    height: 100%;
    overflow: auto;
  }

  #emotionUL li {
    float: left;
  }
</style>