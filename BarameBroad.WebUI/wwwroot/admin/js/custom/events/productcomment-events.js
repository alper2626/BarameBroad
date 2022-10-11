
var productCommentEvents =
{
    loadCommentCount: function () {
        
        get("/ProductComment/ProductComment/NotApprovedCommentCount",
            '',
            function (res) {
                if (res != "0") {
                    $('.comment-badge').parent('span').show();
                    $('.comment-badge').html(res);
                    return;
                }
                $('.comment-badge').parent('span').remove();
            });
    },
    init: function () {
        
        productCommentEvents.loadCommentCount();
    }
}
$(function() {
    productCommentEvents.init();
});

