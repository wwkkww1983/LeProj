<template>
  <div class="propTest">
    <div class="prop-panel" v-for="item in props" :class="{'edit': item.edit, 'add': item.add, 'del': item.del}">
      <div class="prop-title">
        <span class="className">{{item.className}}</span>
        <div class="control-panel">
          <span v-if = 'item.add===false' @click="item.add = true">新增</span>
          <span v-else @click = "item.add = false">新增完成</span>
          <span v-if = 'item.del===false' @click="item.del = true">删除</span>
          <span v-else @click = "item.del = false">删除完成</span>
          <span v-if = 'item.edit===false' @click="item.edit = true">编辑</span>
          <span v-else @click = "item.edit = false">编辑完成</span>
        </div>
      </div>
      <div class="prop-container">
        <div class="prop-box del" v-for='(prop, index) in item.props'>
          <div class="prop-name">
            <input v-if="item.edit" class="propName-edit" v-model = 'prop.name'>
            <span v-else>{{prop.name}}</span>
          </div>
          <div class="prop-box-tags">
            <div class="prop-tag" v-for = '(tag, index) in prop.tags'>{{tag}}<i v-if="item.edit" class="tag-del" @click = "delTag(prop, index)">X</i></div>
            <div class="prop-tag" @click = 'addTag(prop)' v-if="item.edit">+</div>
          </div>
          <div class="prop-delete" @click = 'delProp(item, index)'><i>x</i></div>
        </div>
        <div class="prop-box" v-if = "item.add">
          <div class="prop-name"><input class="propName-edit" :placeholder="'输入' + item.className" v-model = 'item.newProp.name'></div>
          <div class="prop-box-tags">
            <el-button class="add-prop-btn" @click="addProp(item)">+</el-button>
          </div>
        </div>
      </div>
    </div>     
  </div>
</template>
<script>
  let tags = ['黄金(克)', '铂金(克)', '钯金(克)', 'K金(克)', '周大生']
  class Prop {
    constructor (name) {
      this.name = name || (Math.floor(Math.random() * 3) > 1 ? '黄金' : '铂金')
      this.tags = [...tags]
    }
    delTag (index) {
      this.tags.splice(index, 1)
    }
    addTag (tag) {
      this.tags.push(tag)
    }
  }
  class C {
    constructor (className) {
      this.className = className || '未定义的类别'
      this.props = []
      this.newProp = new Prop('未命名的' + this.className)
      this.edit = false
      this.del = false
      this.add = false
      for (let x = 0; x < Math.ceil(Math.random() * 5); x++) {
        this.props.push(new Prop())
      }
    }
    addProp () {
      this.props.push(this.newProp)
      this.newProp = new Prop('未命名的' + this.className)
    }
    delProp (index) {
      this.props.splice(index, 1)
    }
  }
  export default {
    data () {
      return {
        props: [],
        tag: {
          name: 'aaa'
        }
      }
    },
    methods: {
      addProp (item) {
        item.addProp(item.newProp)
      },
      delProp (item, index) {
        this.$confirm('是否删除该属性').then(() => {
          item.delProp(index)
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          })
        })
      },
      addTag (prop) {
        this.$prompt('请输入标签名').then(({value}) => {
          prop.addTag(value)
        }).catch(() => {
          console.log('取消输入');
        })
      },
      delTag (prop, index) {
        prop.delTag(index)
      }
    },
    created () {
      this.props.push(new C('成色名称'))
      this.props.push(new C('产品类别'))
      console.log('a', this);
    }
  }
</script>
<style lang='scss'>
  .propTest{padding: 10px;
    .prop-panel{border-radius:5px;box-shadow:0 0 8px #888;background-color: #ececec;margin:auto;margin-bottom: 20px;max-width:800px;
      .prop-title{position:relative;text-align: center;padding:0 15px;line-height: 32px;height:32px;overflow: hidden;
        .className {font-size: 16px;font-weight:bold;}
      }
      .control-panel{position: absolute;right:0;top:0;padding-right:10px;
        span{display: inline-block;margin:0 5px;}
      }
      .prop-container{background-color: #fff;border-radius:5px;border-top:solid 1px #ccc;}
      .prop-box{border-bottom:solid 1px #ccc;position: relative;}
      .propName-edit{width: 100%; display:block;border:0;line-height: 32px;outline:0;}
      .prop-delete{display:none;font-size:32px;position: absolute;width: 50px;top:15px;left:15px;height:50px;line-height:50px;text-align: center;}
      .prop-name{margin:0 15px;border-bottom:solid 1px #ccc;line-height: 32px;}
      .prop-box-tags{padding:0 15px;}
      .tag-del{position: absolute;right:-7px;top:-7px;border-radius:50%;text-align: center;color:#fff;background-color: #999;line-height:14px;font-size:12px;width:14px;height:14px;}
      .add-prop-btn{margin:15px;}
      .prop-tag{position: relative;margin:10px; display: inline-block;border:solid 1px #ccc;padding: 0 15px;border-radius: 5px;line-height: 2;}
    }
    .prop-panel.edit{
    }
    .prop-panel.del{
      .prop-box{padding-left: 65px;}
      .prop-delete{display:block;}
    }
  }
</style>