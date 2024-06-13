window.infiniteScroll = {
    init: function (dotnetHelper) {
        window.onscroll = function () {
            if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
                dotnetHelper.invokeMethodAsync('LoadMoreItems');
            }
        };
    }
};
