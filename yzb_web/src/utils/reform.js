export const reformQQ_kv = (arr) => { // reform kev-Value
  let o = {}
  for (let i in arr) {
    o[arr[i].Tag] = arr[i].Value
  }
  return o
}
export const getNameById = (id, arr) => { // getNameById
  for (let item of arr) {
    if (id === item.id) {
      return item.nickname
    }
  }
}
export const reformMessage = (arr) => {
  let o = []
  for (let i in arr) {
    if (i > 0) {
      if (arr[i].sess._impl.curMaxMsgSeq !== arr[i - 1].sess._impl.curMaxMsgSeq) {
        o.push(arr[i].sess._impl)
      } else {
      }
    } else {
      o.push(arr[i].sess._impl)
    }
  }
  return o
}
