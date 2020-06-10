var UploadFile = {
    Init: function () {
        $("#fileuploader").uploadFile({
            url: "/Home/Upload",
            statusBarWidth: 'auto',
            dragdropWidth: 'auto',
            autoSubmit: true,
            showProgress: true,
            multiple: true,
            dragDrop: true,
        });
    }
}

$(document).ready(function () {
    UploadFile.Init();
});