<template>
<div class="addFriends" :class="{searching: resData.data}">
    <input type="text" @keyup.enter="searchPhone" v-model="searchData.findUserByPhoneOrName" class="search-input" placeholder="搜索好友">
    <div class="result" v-if = "resData.msg && resultVisible" v-clickoutside="hide">
      <template v-if = "resData.data[0]">
        <Qnode v-for = "item in resData.data" :img="item.avatarUrl" :nodeName="item.nickname" @click.native = " nodeClick (item) "></Qnode>
      </template>
      <div v-else class="search-msg">无搜索结果</div>
    </div>
</div>
</template>
<script>
import Qnode from "../../components/Q-node"
import clickoutside from 'element-ui/lib/utils/clickoutside'
import {mapGetters, mapActions} from 'vuex'
import { applyAddFriend } from '../../IM/friend/index.js'
import {getInfoByPN} from '../../Api/user'
export default {
  data () {
    return {
      searchData: {
        findUserByPhoneOrName: ''
      },
      resData: {
        data: null
      },
      resultVisible: false
    }
  },
  directives: {clickoutside},
  components: {
    Qnode
  },
  computed: {
    ...mapGetters([
        'friendList',
        'recentContacts'
    ])
  },
  methods: {
    ...mapActions([
      "getFriendList"
    ]),
    searchPhone () {
      let data = {
        PN: this.searchData.findUserByPhoneOrName
      }
      getInfoByPN(data).then((res) => {
        console.log(res.data);
        this.resData = res.data
        this.resultVisible = true
      }, (err) => {
        console.log(err);
      })
    },
    hide () {
      this.resultVisible = false
    },
    isFriend (str) { // 判断是否为好友
      const friendObj = this.friendList
      if (friendObj) {
        for (let i = 0; i < friendObj.length; i++) {
           if (friendObj[i].id === str) return true
        }
      }
      return false
    },
    nodeClick (obj) {
      let isFriend = this.isFriend(obj.id)
      if (isFriend) {
        this.$alert('进入对话框').then(() => {
            selType = webim.SESSION_TYPE.C2C;
            selToID = obj.id
            selSess = new webim.Session(selType, obj.id, obj.nickname, obj.avatarUrl, Math.round(new Date().getTime() / 1000))
            this.$router.push('/')

            for (var arr of this.recentContacts) {
                if (arr.id === selSess._impl.id) return
            }
            this.recentContacts.push(selSess._impl)
            // 消息已读标记
            // selSess = webim.MsgStore.sessByTypeId(selType, selToID)
            // webim.setAutoRead(selSess, true, true)
        })

      } else {
        applyAddFriend(obj, (res) => {
          this.friendList.push(obj)
          this.$alert('添加成功')
        })
      }

    }
  },
  created: function() {
  }
}
</script>
<style lang="scss">
.addFriends{
  .search-input{display:block;line-height:42px;background-color: #eee;border:0;width: 100%;padding-left:1em;height:42px;}
  .result{padding:15px 0;border-bottom:solid 1px #ccc;margin-bottom:10px;
    .search-msg{text-align:center;}
  }
}
</style>