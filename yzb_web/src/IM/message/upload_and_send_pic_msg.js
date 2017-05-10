// 检查文件类型和大小
export const checkFile = (obj, fileSize) => {
    var picExts = 'jpg|jpeg|png|bmp|gif|webp';
    var photoExt = obj.value.substr(obj.value.lastIndexOf(".") + 1).toLowerCase(); // 获得文件后缀名
    var pos = picExts.indexOf(photoExt);
    if (pos < 0) {
        alert("您选中的文件不是图片，请重新选择");
        return false;
    }
    fileSize = Math.round(fileSize / 1024 * 100) / 100; // 单位为KB
    if (fileSize > 30 * 1024) {
        alert("您选择的图片大小超过限制(最大为30M)，请重新选择");
        return false;
    }
    return true;
}
