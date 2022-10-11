var myDropzone;
Dropzone.autoDiscover = false;

$(function () {

    KTCardDraggable.init();
    $('#upload-imagezone').each(function () {

        let dropzoneControl = $(this)[0].dropzone;
        if (dropzoneControl) {
            dropzoneControl.destroy();
        }
    });

    $('#upload-imagezone').dropzone({
        addRemoveLinks: true,
        dictRemoveFile: 'Dosyayı Sil',
        dictCancelUpload:'',
        autoProcessQueue: false,
        uploadMultiple: true,
        parallelUploads: 5,
        url: $('#upload-imagezone').attr('action'), // Set the url for your upload script location
        paramName: "file", // The name that will be used to transfer the file
        maxFiles: 5,
        maxFilesize: 10, // MB
        previewsContainer: '#dropzonePreview',
        acceptedFiles: "image/*",
        accept: function (file, done) {
            $('.dz-error-message').remove();
            $('.dz-success-mark').remove();
            $('.dz-error-mark').remove();
            $('.dz-filename').addClass('pt-5');
            
            done();
        },
        init: function (e) {

            myDropzone = this;
            sortableDropzone(myDropzone);
            this.on("success",
                function (file, responseText) {
                    if (responseText.Success) {
                        debugger;
                        if ($('#upload-imagezone').attr('data-message-query') == "true") {
                            window.location.href = $('#upload-imagezone').data('redirect') + "?id=" + responseText.Data;
                            toastr.info(responseText.Message);
                            return;
                        } else {
                            window.location.href = $('#upload-imagezone').data('redirect');
                            toastr.info(responseText.Message);
                            return;
                        }

                    }
                    
                    notificationEvents.showError(responseText.Message);
                });

        }
    });

});

function sortableDropzone() {
    var dropzoneDrag = document.querySelectorAll('#dropzonePreview');
    var dzDrop = new Sortable.default(dropzoneDrag, {
        draggable: '.dz-preview',
        handle: '.dz-details',
        mirror: {
            appendTo: 'body',
            constrainDimensions: true
        }
    });
    dzDrop.on('sortable:stop', function () {
        var newQueue = [];
        var queue = myDropzone.files;
        $('#upload-imagezone .dz-preview .dz-filename [data-dz-name]').each(function (count, el) {
            var name = el.textContent;
            queue.forEach(function (file) {

                if (file.name === name && !newQueue.some((val) => val == file)) {
                    newQueue.push(file);
                }

            });
        });

        myDropzone.files = newQueue;
    });

}


var KTCardDraggable = {
    init: function () {
        var containers = document.querySelectorAll('.draggable-zone');

        if (containers.length === 0) {
            return false;
        }

        var swappable = new Sortable.default(containers, {
            draggable: '.draggable',
            handle: '.draggable .draggable-handle',
            mirror: {
                appendTo: 'body',
                constrainDimensions: true
            }
        });

        swappable.on('sortable:stop', function () {
            $('.reorder-wrapper').show();
        });
    }
}




$('.reorder').click(function () {

    var len = $('[data-fp]').length;
    var items = [];
    $.each($('[data-fp]'), function (index, item) {
        items.push($(item).data('fp'));
        if (len == index + 1) {
            post('/Admin/File/MoveItems', { paths: items }, function (res) {
                if (res.Success) {
                    $.each($('[data-fp]'), function (subIndex, subItem) {
                        $(subItem).data('fp', res.Data[subIndex]);
                    });
                    notificationEvents.showInfo(res.Message);
                    $('.reorder-wrapper').hide();
                    return;
                }
                notificationEvents.errorInfo(res.Message);
            });
        }
    });


});


$("button[type=submit]").unbind().click(function (e) {
    for (var instance in pageEvents.ckEditors) {
        if (Object.prototype.hasOwnProperty.call(pageEvents.ckEditors, instance)) {
            $(pageEvents.ckEditors[instance].sourceElement).val(pageEvents.ckEditors[instance].getData());
        }
    }
    
    if (!$('#upload-imagezone').valid()) {
        e.preventDefault();
        e.stopPropagation();
        notificationEvents.showError("Lütfen Zorunlu Alanları Doldurun");
        return;
    }
    

    if (myDropzone.files.length > 0) {
        e.preventDefault();
        e.stopPropagation();

        myDropzone.processQueue();

    } else {

        $('#upload-imagezone')[0].submit();
    }
});

function deleteFile(item) {
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
                    pageEvents.showLoader();
                },
                success: function (data) {
                    pageEvents.hideLoader();

                    if (data.Success) {

                        $("[data-remove='" + $(_this).data('remove-index') + "']").remove();
                        notificationEvents.showInfo(data.Message);
                        return;
                    }
                    notificationEvents.showError(data.Message);
                }
            });
        }
    });
}


