//复制到剪切板
function CopyTextToClipboard(text)
{
    // 创建一个临时的input元素
    var $tempTextarea = $("<textarea>");
    $("body").append($tempTextarea);

    // 设置input的值并选择它
    $tempTextarea.val(text).select();

    document.execCommand("copy");

    // 移除临时input元素
    $tempTextarea.remove();
}