var gridEvents =
{
    deleteItem: function (item) {
        var row = $(item).parents('tr');
        var refresh = $(item).data('refresh');
        var from = $(item).data('from');
        var to = $(item).data('to');
        var url = "/" + $(item).data('area') + "/" + $(item).data('async-remove-grid') + "/Delete";

        pageEvents.deleteAjaxItem(url,
            { id: $(item).data('ref') },
            function () {
                if (refresh) {
                    gridEvents.refreshGrids(from);
                }
                else if (from) {
                    $(row).hide('slow');
                }
                if (to)
                    gridEvents.refreshGrids(to);
            });
    },
    deletePostItem: function (item) {
        var row = $(item).parents('tr');
        var refresh = $(item).data('refresh');
        var from = $(item).data('from');
        var to = $(item).data('to');
        var url = "/" + $(item).data('area') + "/" + $(item).data('async-remove-post-grid') + "/Delete";
        var serializedData = $(item).parent('div').children('form').serialize();
        pageEvents.deleteAjaxItem(url,
            serializedData,
            function () {
                if (refresh) {
                    gridEvents.refreshGrids(from);
                }
                else if (from) {
                    $(row).hide('slow');
                }
                if (to)
                    gridEvents.refreshGrids(to);
            });
    },
    addItem: function (item) {
        var row = $(item).parents('tr');
        var refresh = $(item).data('refresh');
        var from = $(item).data('from');
        var to = $(item).data('to');
        var url = "/" + $(item).data('area') + "/" + $(item).data('async-add-grid') + "/Create";
        var serializedData = $(item).parent('div').children('form').serialize();
        
        post(url,
            serializedData,
            function (res) {
                if (res.Success) {
                    notificationEvents.showInfo(res.Message);
                    if (refresh) {
                        gridEvents.refreshGrids(from);
                    } else if (from) {
                        $(row).hide('slow');
                    }
                    if (to)
                        gridEvents.refreshGrids(to);

                    return;
                }
                notificationEvents.showError(res.Message);
            });

    },
    refreshGrids: function (gridIds) {
        $.each(gridIds.split(','),
            function (index, item) {
                $("#" + item).dxDataGrid("instance").refresh();
            });
    },
    init: function () {
        
    },

}
$(function () {
    gridEvents.init();
});
$(document).on('click', '[data-async-remove-grid]',
    function (e) {
        var _this = $(this);
        gridEvents.deleteItem(_this);
        e.stopPropagation();
        e.preventDefault();
    });

$(document).on('click', '[data-async-remove-post-grid]',
    function (e) {
        var _this = $(this);
        gridEvents.deletePostItem(_this);
        e.stopPropagation();
        e.preventDefault();
    });

$(document).on('click', '[data-async-add-grid]',
    function (e) {
        var _this = $(this);
        gridEvents.addItem(_this);
        e.stopPropagation();
        e.preventDefault();
    });


$(document).on('show.bs.dropdown',
    '.grid-menu-btn',
    function (e) {
        var _this = $(this);
        $(_this).parent('td').css('overflow', 'initial');
        $('.dx-scrollable-container').css('overflow-y', 'initial');
        $('.dx-datagrid-rowsview').css('overflow-y', 'initial');
    });

$(document).on('hide.bs.dropdown',
    '.grid-menu-btn',
    function (e) {
        var _this = $(this);
        $(_this).parent('td').css('overflow', 'hidden');
        $('.dx-scrollable-container').css('overflow-y', 'hidden');
        $('.dx-datagrid-rowsview').css('overflow-y', 'hidden');

    });


