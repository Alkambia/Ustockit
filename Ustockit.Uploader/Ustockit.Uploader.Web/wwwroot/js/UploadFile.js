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
            onSuccess: function (files, data, xhr, pd) {
                $.toast({
                    heading: data.status,
                    text: data.message,
                    showHideTransition: 'slide',
                    icon: data.status.toLowerCase()
                })
            },
            onError: function (files, status, errMsg, pd) {
                $("#eventsmessage").html($("#eventsmessage").html() + "<br/>Error for: " + JSON.stringify(files));
            },
        });
    }
}

$(document).ready(function () {
    UploadFile.Init();
});