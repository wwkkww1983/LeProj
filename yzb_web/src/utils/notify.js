import createNotify from './createNotify'

let id = sessionStorage.getItem('id')
let yunba = new Yunba({
  appkey: '57ff501af859c75d42b0c4f7'
})
export default function (cb) {
  yunba.init((ok) => {
    if (ok) {
      yunba.connect((cbOk, err) => {
        if (cbOk) {
          console.log('云巴连接成功');
          yunba.connect_by_customid(id, (ok, msg, sessionid) => {
            if (ok) {
              yunba.set_alias({alias: id}, (data) => {
                if (!data.success) {
                  console.log(data.msg);
                }
              })
              yunba.set_message_cb((data) => {
                if (cb instanceof Function) cb(data);
                let msg = JSON.parse(data.msg)
                createNotify(msg.msgTitle, {
                  body: msg.msgContent
                })
              })
            }
          })
        } else {
          console.log(object);
        }
      })
    }
  })
}
