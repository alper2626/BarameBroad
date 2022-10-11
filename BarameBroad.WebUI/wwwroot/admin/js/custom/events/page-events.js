var pageEvents =
{
    ckEditors: [],
    showLoader: function () {
        $('#loading').css('display', 'block');
    },
    hideLoader: function () {
        $('#loading').css('display', 'none');
    },
    setTooltips: function () {
        $('[data-original-title]').tooltip();
        $('.bs-tooltip-bottom').remove();
    },
    updateNumberInputs: function () {
        if ($('input[data-number="true"]').length > 0) {
            $.each($('input[data-number="true"]'),
                function (index, item) {

                    var val = $(item).val().replace(',', '.');
                    $(item).val(val);
                    $(item).attr('type', 'number');
                });
        }
    },
    setEditors: function () {
        if ($('.editor').length > 0) {
            pageEvents.ckEditors = [];
            for (var i = 0; i < $('.editor').length; i++) {
                ClassicEditor.create($('.editor')[i],
                    {

                        toolbar: ['heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList'],
                        heading: {
                            options: [
                                { model: 'paragraph', title: 'Paragraf', class: 'ck-heading_paragraph' },
                                { model: 'heading1', view: 'h1', title: 'Başlık 1', class: 'ck-heading_heading1' },
                                { model: 'heading2', view: 'h2', title: 'Başlık 2', class: 'ck-heading_heading2' },
                                { model: 'heading3', view: 'h3', title: 'Başlık 3', class: 'ck-heading_heading3' }
                            ]
                        }
                    })
                    .then(editor => {
                        pageEvents.ckEditors.push(editor);
                    })
                    .catch(error => {
                        debugger;
                    });
            }



        }
    },
    loadPartials: function () {
        $('[data-partial]').each(function (index, item) {
            var url = $(item).data('partial');
            if (url && url.length > 0) {
                get(url,
                    null,
                    function (response) {
                        $(item).replaceWith(response);
                    });
                pageEvents.loadMasksAfterLoadPartial();
            }
        });
    },
    refreshValidations: function () {
        $("form").removeData("validator");
        $("form").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form");
    },
    loadMasksAfterLoadPartial: function () {
        $("#Iban").mask("TR99 99999 9 99999999999999", {
            placeholder: '____ ____ ____ ____ ____ __'
        });
        $("#phone").mask("(999) 999-99-99", {
            placeholder: '(___) ___-__-__'
        });
    },
    fillModal: function (url) {

        get(url, null,
            function (data) {

                $('#basicmodalcontent').children().remove();
                $('#basicmodalcontent').append(data);
                $('#basicmodal').modal('show');
            });
    },
    postModalForm: function () {
        var _form = $('#update-form');
        if (!$(_form).valid()) {
            notificationEvents.showError("Lütfen Gerekli Alanları Doldurun.");
            return;
        }
        post($(_form).attr('action'),
            $(_form).serialize(),
            function (dataResponse) {
                if (dataResponse.Success) {
                    notificationEvents.showInfo(dataResponse.Message);

                    pageEvents.closeModal();
                } else {
                    notificationEvents.showError(dataResponse.Message);
                }
            });
    },
    closeModal: function () {
        $('#basicmodal').modal('hide');
    },
    deleteItem: function (redirect) {
        var obj = {
            Type: 'info',
            Title: '' + Messages.DefaultRemove + '',
            Description: '' + Messages.DefaultRemoveDesc + '',
            ThenTrue: function () {
                window.location.href = redirect;
            }
        };
        messageEvents.show(obj);
    },
    deleteAjaxItem: function (url, data, callback) {
        var obj = {
            Type: 'info',
            Title: '' + Messages.DefaultRemove + '',
            Description: '' + Messages.DefaultRemoveDesc + '',
            ThenTrue: function () {
                post(url,
                    data,
                    function (dataResponse) {
                        if (!dataResponse.Success) {
                            notificationEvents.showError(dataResponse.Message);
                            return;
                        }
                        notificationEvents.showInfo(dataResponse.Message);
                        if (callback)
                            callback();
                    });
            }
        };

        messageEvents.show(obj);
    },
    queryStringToJSON: function (callback) {
        var queryString = window.location.href;
        if (queryString.indexOf('?') == -1 && callback) {
            callback({});
            return;
        }

        queryString = queryString.split('?')[1];
        var pairs = queryString.split('&');
        var pairsLength = pairs.length;
        var result = {};
        pairs.forEach(function (pair, index) {

            pair = pair.split('=');
            result[pair[0]] = decodeURIComponent(pair[1] || '');
            if (pairsLength == index + 1 && callback)
                callback(result);
        });
    },
    customQueryStringToJSON: function (str, callback) {
        var pairs = str.split('&');
        var pairsLength = pairs.length;
        var result = {};
        pairs.forEach(function (pair, index) {
            pair = pair.split('=');
            result[pair[0]] = decodeURIComponent(pair[1] || '');
            if (pairsLength == index + 1 && callback)
                callback(result);
        });
    },
    JSONToQueryString: function (json, callback) {

        var r = '?' +
            Object.keys(json).map(function (key) {
                return encodeURIComponent(key) + '=' +
                    encodeURI(json[key]);
            }).join('&');
        callback(r);
    },
    init: function (callback) {
        pageEvents.loadPartials();
        pageEvents.setEditors();
        pageEvents.setTooltips();
        pageEvents.updateNumberInputs();
        pageEvents.loadMasksAfterLoadPartial();
        if (callback)
            callback();
    }
}

$(document).on('click', '[data-fill-url]',
    function (e) {
        var _this = $(this);
        pageEvents.fillModal($(_this).data('fill-url'));
        e.stopPropagation();
        e.preventDefault();
    });

$(document).on('click', '.update-modal-btn',
    function (e) {
        e.stopPropagation();
        e.preventDefault();
        pageEvents.postModalForm();

    });

//Hoş değil !!!!
$('input[data-switch]').on('change.bootstrapSwitch', function (e) {
    e.stopPropagation();
    e.preventDefault();
    if (e.target.checked) {
        $(this).val(true);
    } else {
        $(this).val(false);
    }
});

$(document).on('click', '.visible-changer', function (e) {
    e.stopPropagation();
    e.preventDefault();
    pageEvents.setTooltips();
    $('.hidden-changer').css('display', 'block');
    $('.visible-changer').remove();
});


$(function () {
    pageEvents.init();
});

