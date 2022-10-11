$(document).ready(function() {
    $("#Iban").mask("TR99 99999 9 99999999999999", {
        placeholder: '____ ____ ____ ____ ____ __'
    });
    $("#phone").mask("(999) 999-99-99", {
        placeholder: '(___) ___-__-__'
    });

});