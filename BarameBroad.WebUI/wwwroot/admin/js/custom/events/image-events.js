var imageEvents =
{
    loadAllPageImage: function () {
        $('img[data-img]').each(function (index, item) {
            
            var module = $(item).data('module');
            var img = $(item).data('img');
            imageEvents.firstImage(img, module);
        });
    },
    loadAllPageDivBg: function (callback) {

        $('[data-img]').each(function (index, item) {
            
            var module = $(item).data('module');
            var img = $(item).data('img');
            imageEvents.divFirstImage(img, module);
            if (callback)
                callback();
        });
    },
    allImagePaths: function (id, module,callback) {
        var route = "/dosya/tumresimler?id=" + id + "&module=" + module;
        get(route, "", function (data) {
            if (callback)
                callback(data);
        });
    },
    firstImage: function (id, module) {
        var route = "/dosya/ilkresim?id=" + id + "&module=" + module;
        get(route, "", function (data) {
            var obj = $('[data-img="' + id + '"]');
            $(obj).attr('src', data);
            $(obj).removeAttr('data-img');
            $(obj).removeAttr('data-module');
        });
    },
    divFirstImage: function (id, module) {
        var route = "/dosya/ilkresim?id=" + id + "&module=" + module;
        get(route, "", function (data) {
            var obj = $('[data-img="' + id + '"]');
            
            $(obj).css('background-image', "url('../../../" + data + "')");
            $(obj).removeAttr('data-img');
            $(obj).removeAttr('data-module');
        });
    },
    init: function () {
        imageEvents.loadAllPageImage();
    }
}

$(function () {
    imageEvents.init();
});


