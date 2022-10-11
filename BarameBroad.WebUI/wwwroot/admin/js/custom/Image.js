var myDropzone;
Dropzone.autoDiscover = false;

$(document).ready(function () {

    $('#upload-imagezone').each(function () {

        let dropzoneControl = $(this)[0].dropzone;
        if (dropzoneControl) {
            dropzoneControl.destroy();
        }
    });

    $('#upload-imagezone').dropzone({
        autoProcessQueue: false,
        uploadMultiple: true,
        parallelUploads: 5,
        url: $('#upload-imagezone').attr('action'), // Set the url for your upload script location
        paramName: "image", // The name that will be used to transfer the file
        maxFiles: 5,
        maxFilesize: 10, // MB
        previewsContainer: '#dropzonePreview',
        addRemoveLinks: true,
        acceptedFiles: "image/*",
        init: function (e) {

            myDropzone = this;
            this.on("success",
                function (file, responseText) {
                    if (responseText.Success) {
                        if (responseText.Data != null && responseText.Data != undefined) {
                            window.location.href = $('#upload-imagezone').data('redirect') + "?id=" + responseText.Data;
                        }
                        toastr.info(responseText.Message);
                        return;
                    }
                    $('#loading').hide();
                    toastr.error(responseText.Message);
                });

        }
    });

});

$("button[type=submit]").click(function (e) {

    if (!$('#upload-imagezone')[0].valid()) {
        notificationEvents.showInfo("Lütfen Gerekli Alanları Doldurun");
        return;
    }
    $('#loading').show();

    if (myDropzone.files.length > 0) {
        e.preventDefault();
        e.stopPropagation();
        myDropzone.processQueue();
    } else {

        $('#upload-imagezone')[0].submit();
    }
});

function deleteImage(item) {
    var redirect = $(item).data('href');
    var message = $(item).data('message');
    var _this = item;
    swal.fire({
        "title": "Silme İşlemi",
        "text": message,
        "type": "success",
        "showCloseButton": true,
        "showCancelButton": true,
        "focusConfirm": false,
        "confirmButtonText":
            '<i class="fa fa-thumbs-up"></i> Sil !',
        "confirmButtonAriaLabel": 'Sil',
        "cancelButtonText":
            '<i class="fa fa-thumbs-down"></i> İptal',
        "cancelButtonAriaLabel": 'İptal Et'

    }).then((result) => {
        if (result.value) {
            $.ajax({
                dataType: 'Json',
                type: 'get',
                url: redirect,
                beforeSend: function () {
                    $('#loading').show();
                },
                success: function (data) {
                    $('#loading').hide();

                    if (data.Success) {

                        $(_this).parent('span').parent('div').remove();
                        toastr.info(data.Message);
                        return;
                    }
                    toastr.error(data.Message);
                }
            });
        }
    });
}
