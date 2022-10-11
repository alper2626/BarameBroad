var categoryFilterProductEvents =
{
    loadProductImages: function () {
        var pName = $('#selectedName').val();
        imageEvents.allImagePaths($('#selectedId').val(), "Product", function (paths) {

            $('#p-images').children('a').remove();
            $.each(paths,
                function (index, item) {

                    $('#p-images').append('<a data-fancybox="gallery" href="' +
                        item +
                        '" class="symbol symbol-30 symbol-circle"><img src="' +
                        item +
                        '" alt="' +
                        pName +
                        '"></a>');
                });
        });
    },
    saveOrUpdateProductFilter: function (item) {

        var ser = $(item).parent('td').parent('tr');
        var postDataStr = $(ser).find('input').serialize();

        pageEvents.customQueryStringToJSON(postDataStr, function (data) {
            data["Id"] = "";
            data["ProductFilterDescriptionsData"] = "";
            $(ser).find('option:selected').each(function (index) {
                if (index != 0) {
                    data["ProductFilterDescriptionsData"] += "-";
                }
                data["ProductFilterDescriptionsData"] += this.value;


            });

            if ($(ser).data('ref') != "" && $(ser).data('ref') != undefined) {
                data["Id"] = $(ser).data('ref');
                post("/CategoryFilter/ProductCategoryFilter/Update",
                    data,
                    function (res) {
                        if (res.Success) {
                            notificationEvents.showInfo(res.Message);
                        } else {
                            notificationEvents.showError(res.Message);
                        }
                    });
            } else {
                post("/CategoryFilter/ProductCategoryFilter/Create",
                    data,
                    function (res) {
                        if (res.Success) {
                            notificationEvents.showInfo(res.Message);
                            $(ser).data('ref', res.Data);
                        } else {
                            notificationEvents.showError(res.Message);
                        }
                    });
            }

        });

    },

    deleteProductFilter: function (item) {

        var ref = $(item).parent('td').parent('tr');
        if ($(ref).data('ref') != '' && $(ref).data('ref') != undefined) {
            pageEvents.deleteAjaxItem("/CategoryFilter/ProductCategoryFilter/Delete",
                { id: $(ref).data('ref') },
                function () {
                    $(ref).remove();
                });
        } else {
            $(ref).remove();
        }


    },
    init: function () {
        categoryFilterProductEvents.loadProductImages();
        demo4();
    }
}
$(document).on('click', 'a[data-repeater-delete]', function (e) {
    e.stopPropagation();
    e.preventDefault();
    pageEvents.setTooltips();
    categoryFilterProductEvents.deleteProductFilter($(this));
});

$(document).on('click', 'a[data-repeater-save]', function (e) {

    e.stopPropagation();
    e.preventDefault();
    pageEvents.setTooltips();
    categoryFilterProductEvents.saveOrUpdateProductFilter($(this));
});


$(document).on('click', 'a[data-repeater-append]', function (e) {
    e.stopPropagation();
    e.preventDefault();
    var n = $('#repeat').eq(0).clone();
    $(n).removeAttr('id');
    $(n).appendTo('.repeater-items');
    $(n).fadeIn('slow');
    pageEvents.setTooltips();
});


var demo4 = function () {

    new KTCard('kt_card_4');


}
