<template>
<div class="group-list">
    <el-table :data="groupList" style="width:100%">
      <el-table-column
        inline-template
        label = '头像'
      >
        <div class="headImg">
          <img style="width:30px; height:30px; position:relative; top:4px" :src="row.FaceUrl" alt="" class = "avatar">
        </div>
      </el-table-column>
      <el-table-column
        prop = 'Name'
        label="群名称"
      ></el-table-column>
      <el-table-column
        prop = 'Type'
        label = '群类型'
      ></el-table-column>
      <el-table-column
        prop = 'MemberNum'
        label = '人数'
      ></el-table-column>
      <el-table-column
        inline-template
        label = '操作'
      >
          <el-dropdown>
            <el-button type="primary">
              {{row.group}}<i class="el-icon-caret-bottom el-icon--right"></i>
            </el-button>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item @click.native = 'chat(row)'>发起会话</el-dropdown-item>
              <el-dropdown-item @click.native = 'dissolve(row)'>解散群组</el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
      </el-table-column>
    </el-table>
</div>
</template>
<script>
import {mapGetters, mapActions} from 'vuex'
import {dissolveGroup} from './../../../IM/group/group_manager'
export default {
  computed: {
    ...mapGetters({
      groupList: "groupList",
      recentContacts: "recentContacts"
    })
  },
  methods: {
    ...mapActions([
      'getGroupList'
    ]),
    dissolve (row) {
      dissolveGroup(row.GroupId, (res) => {
        this.getGroupList()
      })
    },
    chat (row) {
      let exist = false
      selType = webim.SESSION_TYPE.GROUP;
      selToID = row.GroupId
      selSess = new webim.Session(selType, row.GroupId, row.Name, row.FaceUrl, Math.round(new Date().getTime() / 1000))
      selSess._impl.typeEn = row.TypeEn
      for (var arr of this.recentContacts) {
          if (arr.id === selSess._impl.id) {
            exist = true
          }
      }
      if (!exist) {
        this.recentContacts.push(selSess._impl)
      }
      // 标记已读
      // selSess = webim.MsgStore.sessByTypeId(selType, selToID)
      // webim.setAutoRead(selSess, true, true)
      this.$router.push('/')
    }
  }
}
</script>
<style lang="scss">
.group-list {
   .el-button--primary {
      background:#ffffff; color:#888888; padding:7px 8px 5px 14px;
      border:1px solid #bbbbbb;
  }
  .headImg {
     img {width:33px; height:33px; height:100%; border-radius:50%;}
  }
}
</style>