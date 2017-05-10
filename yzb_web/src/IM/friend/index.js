import { reformQQ_kv } from '../../utils/reform'
// 申请加好友
export const applyAddFriend = function (obj, cbOk) {
    const addFriendItem = [{
      'To_Account': obj.id, // 好友账号(id)
      "AddSource": "AddSource_Type_Unknow",
      "AddWording": "我是:" // 加好友附言，可为空
    }]
    const options = {
      'From_Account': loginInfo.identifier,
      'AddFriendItem': addFriendItem
    }
    webim.applyAddFriend(
      options,
      function (res) {
        cbOk(res)
      },
      function (err) {
        console.log('OUT: ' + err.ErrorInfo);
      }
    )
  }
// 获取好友列表
export const getMyFriends = function (cbOk) {
    let options = {
      'From_Account': loginInfo.identifier,
      'TimeStamp': 0,
      'StartIndex': 0,
      'GetCount': 100,
      'LastStandardSequence': 0,
      "TagList": [
        "Tag_Profile_IM_Image",
        "Tag_Profile_IM_Nick"
      ]
    }
    webim.getAllFriend(
      options,
      (res) => {
        let arr = []
        if (res.ActionStatus === "OK") {
          // console.log(res.InfoItem)
          if (res.InfoItem) {
            arr = res.InfoItem.map((item) => {
              return reformQQ_kv(item.SnsProfileItem)
            })
            for (let item in res.InfoItem) {
              arr[item].id = res.InfoItem[item].Info_Account
            }
          }
        }
        cbOk(arr)
      },
      (err) => {
        console.error(err);
      }
    )
  }

// 删除好友
export const deleteFriend = function (ids, check) {
  var to_account = ids;
  if (to_account.length <= 0) {
    return;
  }
  var options = {
    'From_Account': loginInfo.identifier,
    'To_Account': to_account,
    'DeleteType': 'Delete_Type_Both'
  }
  webim.deleteFriend(
    options,
    function (resp) {
      check()
    },
    function (err) { alert(err.ErrorInfo) }
  )
}

