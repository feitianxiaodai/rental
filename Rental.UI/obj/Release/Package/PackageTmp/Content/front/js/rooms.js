
$(document).ready(function () {

    var eleWidth = $("ul.ul-wrap li:first").outerWidth() + 1;//内边距和边框+margin
    var eleCount = $("ul.ul-wrap li").length;
    $("ul.ul-wrap").width(eleWidth * eleCount);

    function ChangeNav() {
        var index = gallerySwiper.activeIndex;
        $homeView.Load_pic($(".comtemplate-detail-panel-img").eq(index), $homeView.winWidth > 750 ? "data-big-src" : "data-small-src");
        $(".mb-gallery-nav-details .ul-wrap li").eq(index).addClass("on").siblings().removeClass("on");
        if (index == 0)
            $(".mb-gallery-lf").hide();
        else
            $(".mb-gallery-lf").show();
        if (index + 1 == $(".mb-gallery-nav-details .ul-wrap li").length)
            $(".mb-gallery-rg").hide();
        else
            $(".mb-gallery-rg").show();
    }

    if ($(".comtemplate-kv")[0]) {
        var eleCount = $("ul.ul-wrap li").length;
        if (eleCount <= 1)
        {
            $(".mb-gallery-lf").hide();
            $(".mb-gallery-rg").hide();
            $(".photos-wrapper-arrow.left").hide();
            $(".photos-wrapper-arrow.right").hide();
        }
        ////酒店房间预览Gallery 轮播
        var gallerySwiper = new Swiper('.comtemplate-kv', {
            loop: false,
            onSlideChangeStart: function () {
                ChangeNav();
            },
            nextButton: '.mb-gallery-rg',
            prevButton: '.mb-gallery-lf',
            preloadImages: false,
            lazyLoading: true
        });
        $('.mb-gallery-lf').on('click', function (e) {
            e.preventDefault()
            gallerySwiper.swipePrev()
        })
        $('.mb-gallery-rg').on('click', function (e) {
            e.preventDefault()
            gallerySwiper.swipeNext()
        })
        $(".mb-gallery-lf").hide();
        $(".mb-gallery-nav-details .ul-wrap li").on("click", function () {
            if ($(this).hasClass("on"))
                return false;
            var index = $(this).index();
            gallerySwiper.swipeTo(index);
            $(this).addClass("on").siblings().removeClass("on");
        });
    }
    var navLeft = 0;
    $(".photos-wrapper-arrow.left").on("click", function () {
        var eleWidth = $("ul.ul-wrap li:first").outerWidth() + 1;//内边距和边框+margin
        //var eleCount = $("ul.ul-wrap li").length;
        //var $ele = $(".ul-wrap");
        //var pOffset = $ele.parent().offset().left;
        //var oOffset = $ele.offset().left;
        var moveDis = eleWidth * 3;

        //if ((oOffset + moveDis) > pOffset) {
        //    moveDis = pOffset - oOffset;
        //}
        //if (moveDis <= 0)
        //    return;
        //$ele.animate({ right: '-=' + moveDis }, 200);
        if (navLeft > 0) {
            navLeft = navLeft - moveDis;
            $("ul.ul-wrap").animate({ "margin-left": "-" + navLeft + "px" });
        }
    });

    $(".photos-wrapper-arrow.right").on("click", function () {
        var eleWidth = $("ul.ul-wrap li:first").outerWidth() + 1;//内边距和边框+margin
        //var eleCount = $("ul.ul-wrap li").length;
        var $ele = $(".ul-wrap");
        //var pOffset = $ele.parent().offset().left;
        //var oOffset = $ele.offset().left;
        var moveDis = eleWidth * 3;

        var oWidth = $(".ul-wrap").width();
        var pWidth = $ele.parent().width();
        //if (oWidth <= pWidth)
        //    return;
        //var diffOffset = Math.abs(pOffset - oOffset);
        //var diffWidth = Math.abs(pWidth - oWidth);
        //if ((diffWidth - diffOffset) < moveDis) {
        //    moveDis = diffWidth - diffOffset;
        //};
        //if (moveDis <= 0)
        //    return;
        //$ele.animate({ right: '+=' + moveDis }, 200);
        if (oWidth - navLeft > pWidth) {
            navLeft = navLeft + moveDis;
            $("ul.ul-wrap").animate({ "margin-left": "-" + navLeft + "px" });
        }
    });

})