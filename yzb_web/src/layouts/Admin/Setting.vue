<template>
  <div class="setting">
    <el-row>
      <el-col :span="12" :offset="6">
        <el-form label-width="120px" class="setting-form">
          <el-form-item label="旧密码：">
            <el-input></el-input>
          </el-form-item>
          <el-form-item label="新密码：">
            <el-input></el-input>
          </el-form-item>
          <el-form-item label="重复新密码：">
            <el-input></el-input>
          </el-form-item>
          <el-form-item>
            <el-button>修改密码</el-button>
          </el-form-item>
        </el-form>        
      </el-col>
    </el-row>
    </section>
  </div>
</template>
<script>
  export default {
    data() {
      return {
        oldpassword: "", // 旧密码
        newpassword: "", // 新密码
        newPasswordIcon: true, // 眼睛的开关
        ErrorPrompt: {
          errorOldPassword: false,
          errorNewPassword: false
        }
      }
    },
    methods: {
      onOldPassword: function() {
        this.ErrorPrompt.errorOldPassword = "请输入以字母开头6-16密码";
      },
      onNewPassword: function() {
        this.ErrorPrompt.errorNewPassword = "请输入以字母开头6-16密码";
      },
      passwordIconCut() { // 眼睛的开关
        this.newPasswordIcon = !this.newPasswordIcon;
      },
      amendPassword() {
        let data
        data = {
          "newpassword": md5(this.newpassword),
          "oldpassword": md5(this.oldpassword),
          "tokenId": sessionStorage.getItem("tokenId")
        }
          // console.log(md5(this.newpassword));
        console.log(md5(this.oldpassword).length);
        this.$http.put('http://119.29.138.104:9082/v1/user/password', data).then((response) => { // 创建公司
          alert("修改密码");
          console.log(response);
          if (response.data.state === 0) {
            alert("修改密码成功");
            this.oldpassword = "";
            this.newpassword = "";
            this.ErrorPrompt.errorOldPassword = "";
            this.ErrorPrompt.errorNewPassword = "";
          }
        }, (response) => {
          alert('修改密码失败' + response);
        });
      }
    }
  }
</script>
<style lang="scss">
.setting{
  .setting-form{margin-top:150px;}
}
</style>