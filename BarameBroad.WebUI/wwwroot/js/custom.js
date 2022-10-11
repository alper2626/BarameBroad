/* Global Events */
function get(route, params, callback) {
    
    $.ajax({
        url: getBaseUrl() + route,
        type: 'GET',
        data: params,
        success: function (data) {

            if (callback)
                callback(data);
        },
        fail: function () {
            //notificationEvents.showError("Ýþlem Sýrasýnda Hata Oluþtu");
        },
        error: function () {
            //notificationEvents.showError("Ýþlem Sýrasýnda Hata Oluþtu");
        },
        complete: function () {
            //pageEvents.hideLoader();
        }
    });
}

function post(route, params, callback) {
    //pageEvents.showLoader();
    $.ajax({
        url: getBaseUrl() + route,
        type: 'POST',
        data: params,
        success: function (data) {
            if (callback)
                callback(data);
        },
        fail: function () {
           // notificationEvents.showError("Ýþlem Sýrasýnda Hata Oluþtu");
        },
        error: function () {
            //notificationEvents.showError("Ýþlem Sýrasýnda Hata Oluþtu");
        },
        complete: function () {
            //pageEvents.hideLoader();
        }
    });
}

function getBaseUrl() {
    var getUrl = window.location;
    return getUrl.protocol + "//" + getUrl.host;
}



/* Map Events */

let map;

function initMap() {
    if ($('#googlemaps').length > 0) {
        map = new google.maps.Map(document.getElementById("googlemaps"), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8,
        });
    }

}

window.initMap = initMap;


// init
$(function () {
    //notificationEvents.backNotification();
    $('[data-partial]').each(function (index, item) {
        var url = $(item).data('partial');
        if (url && url.length > 0) {
            get(url,
                null,
                function (response) {
                    $(item).replaceWith(response);
                });
            
        }
    });
});
