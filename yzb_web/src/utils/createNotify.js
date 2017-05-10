// createNotify
export default function (title, option, click) {
  let msgTitle = title || ''
  let msgOption = option || {}
  let notification
  if (!("Notification" in window)) {
    alert("This browser does not support desktop notification");
  } else if (Notification.permission === "granted") {
    // If it's okay let's create a notification
    notification = new Notification(msgTitle, msgOption)
    notification.onclick = function () {
      if (click instanceof Function) click()
    }
    setTimeout(function () {
      notification.close()
    }, 10000)

  }
}
