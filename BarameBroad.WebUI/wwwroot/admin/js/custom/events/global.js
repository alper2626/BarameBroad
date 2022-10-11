function get(route, params, callback) {
    pageEvents.showLoader();
    $.ajax({
        url: getBaseUrl() + route,
        type: 'GET',
        data: params,
        success: function (data) {
            
            if (callback)
                callback(data);
        },
        fail: function () {
            notificationEvents.showError("İşlem Sırasında Hata Oluştu");
        },
        error: function() {
            notificationEvents.showError("İşlem Sırasında Hata Oluştu");
        },
        complete: function () {
            pageEvents.hideLoader();
        }
    });
}

function post(route, params, callback) {
    pageEvents.showLoader();
    $.ajax({
        url: getBaseUrl() + route,
        type: 'POST',
        data: params,
        success: function (data) {
            if (callback)
                callback(data);
        },
        fail: function () {
            notificationEvents.showError("İşlem Sırasında Hata Oluştu");
        },
        error: function () {
            notificationEvents.showError("İşlem Sırasında Hata Oluştu");
        },
        complete: function () {
            pageEvents.hideLoader();
        }
    });
}

function getBaseUrl() {
    var getUrl = window.location;
    return getUrl.protocol + "//" + getUrl.host;
}

$(function () {
    notificationEvents.backNotification();
});