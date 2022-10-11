var useCountTypeItems = $("select[name='DiscountUseCountType'] > option");
var discountEvents =
{
    changeUseType: function () {
        var discountType = $("select[name='DiscountType']").val();
        $('.basket-ent').show();
        $('.coupon-item').hide();
        switch (discountType) {
            case 'Product':
                var options = useCountTypeItems;
                options = options.eq(1).remove();
                $("select[name='DiscountUseCountType']").children('option').remove();
                $("select[name='DiscountUseCountType']").append(options);
                $('.basket-ent').hide();
                break;
            case 'Coupon':
                $("select[name='DiscountUseCountType']").children('option').remove();
                $("select[name='DiscountUseCountType']").append(useCountTypeItems);
                $('.coupon-item').show();
                break;
            case 'User':
                $("select[name='DiscountUseCountType']").children('option').remove();
                $("select[name='DiscountUseCountType']").append(useCountTypeItems);
                break;
        }

    },
    init: function () {
        discountEvents.changeUseType();
    }
}
$(function () {
    discountEvents.init();
});

$('.notify-select').select2({
});
$('.notify-select').on('select2:select',
    function (e) {
        $('#update-form').append('<input type="hidden" name="NotificationTypes" value="' + e.params.data.id + '"/>');
    });

$('.notify-select').on('select2:unselecting', function (e) {
    $('input[value="' + e.params.args.data.id + '"]').remove();
});

$("select[name='DiscountRelationType'],select[name='DiscountType']").change(function () {
    discountEvents.changeUseType();
});
$(document).on('click','a[data-append-discount]',
    function(e) {
        e.stopPropagation();
        e.preventDefault();
        var data = $(this).data('append-discount');
        post('/Discount/DiscountEntity/Create',
            { SelectedDiscountId: $('#Id').val(), EntityId: data },
            function (res) {
                if (res.Success) {
                    gridEvents.refreshGrids('ProductTable,DiscountedProductTable');
                    notificationEvents.showInfo(res.Message);
                } else {
                    notificationEvents.showError(res.Message);
                }
            });

    });
$(document).on('click','a[data-remove-discount]',
    function (e) {
        e.stopPropagation();
        e.preventDefault();
        var data = $(this).data('remove-discount');
        post('/Discount/DiscountEntity/Delete',
            { SelectedDiscountId: $('#Id').val(), EntityId: data },
            function(res) {
                if (res.Success) {
                    gridEvents.refreshGrids('ProductTable,DiscountedProductTable');
                    notificationEvents.showInfo(res.Message);
                } else {
                    notificationEvents.showError(res.Message);
                }
            });
        
    });
$('#color-input').change(function () {
    $(this).attr('value', $(this).val());
});