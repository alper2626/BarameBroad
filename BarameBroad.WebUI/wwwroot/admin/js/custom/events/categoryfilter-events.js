var options = {
    Type: 'info',
    Title: "Silme İşlemi",
    Description: "Filtre Açıklaması Silinecek Emin Misiniz ? ( Filtreye Bağlı Ürünler Etkilenebilir. )",
    ThenTrue: "",
    ThenFalse: ""
}
var categoryFilterEvents =
{
    saveOrUpdateFilterDescription: function (item) {
        var postData = {
            Id: $(item).data('ref'),
            CategoryFilterId: $(item).data('filter'),
            Description: $('[data-bind="' + $(item).data('ref') + '"]').val()
        }
        if ($(item).data('ref') != "") {
            post("/CategoryFilter/CategoryFilterDescription/Update",
                postData,
                function (res) {
                    if (res.Success) {
                        notificationEvents.showInfo(res.Message);
                    } else {
                        notificationEvents.showError(res.Message);
                    }
                });
        } else {
            post("/CategoryFilter/CategoryFilterDescription/Create",
                postData,
                function (res) {
                    if (res.Success) {
                        notificationEvents.showInfo(res.Message);
                        $(item).data('ref', res.Data);
                        $(item).parent('.input-group-prepend').parent('.input-group').children('[data-bind]').attr('data-bind', res.Data);
                        $(item).parent('.input-group-prepend').parent('.input-group').children('.input-group-append').children('[data-ref]').attr('data-ref', res.Data);
                    } else {
                        notificationEvents.showError(res.Message);
                    }
                });
        }
    },

    deleteFilterDescription: function (item) {
        var _item = $(item);
        if ($(_item).data('ref') != '' && $(_item).data('ref') != undefined) {
            pageEvents.deleteAjaxItem("/CategoryFilter/CategoryFilterDescription/Delete",
                { id: $(_item).data('ref') },
                function () {
                    $(item).parents('.form-group').eq(0).remove();
                });
        } else {
            $(item).parents('.form-group').eq(0).remove();
        }
        

    },
    init: function () {
        
    }
}
$(document).on('click', '.delete-description', function (e) {
    e.stopPropagation();
    e.preventDefault();
    categoryFilterEvents.deleteFilterDescription($(this));
});

$(document).on('click', '.save-description', function (e) {
    e.stopPropagation();
    e.preventDefault();
    categoryFilterEvents.saveOrUpdateFilterDescription($(this));
});

$(document).on('click', '[data-appender]', function (e) {
    e.stopPropagation();
    e.preventDefault();
    var n = $('#repeat').eq(0).clone();
    $(n).removeAttr('id');
    $(n).appendTo('.repeater-items');
    $(n).fadeIn('5000');
});

