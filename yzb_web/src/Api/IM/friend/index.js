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
            console.log("获取好友列表");
            console.log(res);
        },
        (err) => {
            console.error(err);
        }
    )
}
