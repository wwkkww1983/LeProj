//function alertError() {
//    if (@errorMsg != "") {
//        window.alert("@errorMsg");
//    }
//}
//alertError();

function alertError() {
    var errorList = document.getElementsByClassName("error-message");
    var errorMsg = "";
    for(var i=0;i<errorList.length;i++)
    {
        errorMsg += errorList[i].innerHTML.replace(/(^\s*)|(\s*$)/g, "");
    }
    if (errorMsg != "") {
        window.alert(errorMsg);
    }
}
alertError();