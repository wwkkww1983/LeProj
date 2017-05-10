<template>
    <div class="sendImg">
        <div class="checked">
            <div class="title">选择图片</div>
            <input type="file" @change="fileOnChange($event)" class="files"/>
            <el-button type="primary">图片上传<i class="el-icon-upload el-icon--right"></i></el-button>
        </div>
        <div class="Preview">
             <div class="title">图片预览</div>
             <div class="imgBox">
                 <img :src="img.src" :alt="img.alt">
             </div>
        </div>
        <!--<div class="form-group">
             <div class="title">进度</div>
             <div class="col-sm-10">
                <progress  :value="progressVal" max="100"></progress>
             </div>
         </div>-->
         <div slot="footer" class="dialog-footer">
                <el-button id="upd_abort" >取 消</el-button>
                <el-button type="primary" @click="uploadPic">上传图片</el-button>
         </div>
    </div>
</template>
<script>
  import {checkFile} from '../../IM/message/upload_and_send_pic_msg.js'
  import {mapGetters, mapActions} from 'vuex'
  export default {
    data() {
      return {
        file: "", // 图片文件
        img: {
          src: '',
          alt: ''
        }
      }
    },
    props: [
      "dialogTableVisibleFun"
    ],
    computed: {
      ...mapGetters({
        tarMessage: 'tarMessage',
        userInfo: 'userInfo'
      })
    },
    methods: {
      ...mapActions([
        'setTarMessage'
      ]),
      fileOnChange(uploadFile) { // 选择图片触发事件
        const _self = this
        if (!window.File || !window.FileList || !window.FileReader) {
          alert("您的浏览器不支持File Api");
          return;
        }
        this.file = uploadFile.target.files[0];
        var fileSize = this.file.size;

        // 先检查图片类型和大小
        if (!checkFile(uploadFile.target, fileSize)) {
          return;
        }

        // 预览图片
        var reader = new FileReader();

        reader.onload = (function(file, _self) {
          return function(e) {

            _self.img.src = this.result
            _self.img.alt = _self.file.name
          }
        })(this.file, _self)
        reader.readAsDataURL(this.file, _self)
      },
      uploadPic() { // 上传图片
        const _self = this
        var businessType // 业务类型，1-发群图片，2-向好友发图片
        if (selType === webim.SESSION_TYPE.C2C) { // 向好友发图片
          businessType = webim.UPLOAD_PIC_BUSSINESS_TYPE.C2C_MSG;
        } else if (selType === webim.SESSION_TYPE.GROUP) { // 发群图片
          businessType = webim.UPLOAD_PIC_BUSSINESS_TYPE.GROUP_MSG;
        }
        // 封装上传图片请求
        var opt = {
            'file': this.file, // 图片对象
            'abortButton': document.getElementById('upd_abort'), // 停止上传图片按钮
            'From_Account': loginInfo.identifier, // 发送者帐号
            'To_Account': selToID, // 接收者
            'businessType': businessType // 业务类型
          }
          // 上传图片
        webim.uploadPic(opt,
          function(resp) {
            // 上传成功发送图片
            _self.sendPic(resp)
            _self.dialogTableVisibleFun()
          },
          function(err) {
            alert(err.ErrorInfo);
          }
        );
      },
      sendPic(images) {
        const _self = this
        if (!selToID) {
          this.$alert('您还没有好友，暂不能聊天', '发送消息', {
            confirmButtonText: '确定'
          })
          return;
        }

        if (!selSess) {
          selSess = new webim.Session(selType, selToID, selToID, friendHeadUrl, Math.round(new Date().getTime() / 1000));
        }
        var msg = new webim.Msg(selSess, true, -1, -1, -1, loginInfo.identifier, 0, loginInfo.identifierNick);
        var images_obj = new webim.Msg.Elem.Images(images.File_UUID);
        for (var i in images.URL_INFO) {
          var img = images.URL_INFO[i]
          var newImg;
          var type;
          switch (img.PIC_TYPE) {
            case 1: // 原图
              type = 1 // 原图
              break
            case 2: // 小图（缩略图）
              type = 3 // 小图
              break
            case 4: // 大图
              type = 2 // 大图
              break
          }
          newImg = new webim.Msg.Elem.Images.Image(type, img.PIC_Size, img.PIC_Width, img.PIC_Height, img.DownUrl);
          images_obj.addImage(newImg)
        }
        msg.addImage(images_obj)
        let msgModel = document.getElementById('msg-model')
          // 调用发送图片消息接口
        webim.sendMsg(msg, function(resp) {

          if (selType === webim.SESSION_TYPE.C2C) { // 私聊时，在聊天窗口手动添加一条发的消息，群聊时，长轮询接口会返回自己发的消息
            const timeStr = webim.Tool.formatTimeStamp(msg.getTime())
            msg.timeStr = timeStr // 时间
            msg.avator = _self.userInfo.avatarUrl // 头像
            _self.tarMessage.push(msg)
              // 在聊天窗体中新增一条消息
            _self.setTarMessage(_self.tarMessage)
            _self.file = null
            _self.img = {
              src: '',
              alt: ''
            }
          }
          setTimeout(function() {

            msgModel.scrollTop = msgModel.scrollHeight;
          }, 300)
        }, function(err) {
          alert(err.ErrorInfo);
        });
      }
    }
  }
</script>
<style lang="scss">
  .title {
    float: left;
    width: 120px;
    text-align: left;
    line-height: 1;
    font-size: 16px;
    font-weight: 700;
  }

  .checked {
    position: relative;
    height: 36px;
    margin-bottom: 30px;
    .files {
      position: absolute;
      width: 97px;
      height: 36px;
      opacity: 0;
      cursor: pointer
    }
  }

  .Preview {
    padding-bottom: 30px;
    overflow: hidden;
    .imgBox {
      float: left;
      img {
        max-width: 200px;
        min-width: 100px;
      }
    }
  }

  .form-group {
    height: 30px;
    padding-bottom: 30px;
  }

  .el-dialog__body {
    position: relative;
    height: 100%;
  }

  .dialog-footer {
    position: absolute;
    width: 100%;
    padding-right: 50px;
    bottom: 50px;
    text-align: right
  }
</style>