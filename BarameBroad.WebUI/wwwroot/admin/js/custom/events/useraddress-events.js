
var userAddressEvents =
{
    delete: function (item) {
        var ref = $(item).data('delete-item');
        pageEvents.deleteAjaxItem("/Auth/UserAddress/Delete",
            { id: ref },
            function() {
                $('[data-ref="' + ref + '"]').remove();
            });
    },
    update: function() {
        if (!$('#update').valid()) {
            notificationEvents.showError('Lütfen Tüm Alanları Doldurun');
            return;
        }

        post("/Auth/UserAddress/Update",
            $('#update').serialize(),
            function (res) {
                
                if (res.Success) {
                    window.location.reload(true); 
                } else {
                    notificationEvents.showError(res.Message);
                }

                $('#basicmodal').modal('hide');
            });
    },
    add: function() {
        if (!$('#create').valid()) {
            notificationEvents.showError('Lütfen Tüm Alanları Doldurun');
            return;
        }

        post("/Auth/UserAddress/Create",
            $('#create').serialize(),
            function (res) {
                
                if (res.Success) {
                    window.location.reload(true); 
                } else {
                    notificationEvents.showError(res.Message);
                }

                $('#basicmodal').modal('hide');
            });
    },
    init: function () {
        pageEvents.loadPartials();
    }
}
$(document).on('click', 'a[data-delete-item]', function (e) {
    e.stopPropagation();
    e.preventDefault();
    userAddressEvents.delete($(this));
});

$(document).on('click', 'a[data-save-item]', function (e) {
    e.stopPropagation();
    e.preventDefault();
    userAddressEvents.add();
});
$(document).on('click', 'a[data-update-item]', function (e) {
    e.stopPropagation();
    e.preventDefault();
    userAddressEvents.update();
});


