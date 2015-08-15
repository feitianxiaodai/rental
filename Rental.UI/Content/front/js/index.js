
$(document).ready(function () {
    InitKv(0);
    $(".mb-gallery-nav .sorts:eq(0) a").on("click", function () {
        InitKv(0);
        $(this).addClass("on").parent().siblings(".sorts").each(function () { $(this).children("a").removeClass("on") });
    });
    $(".mb-gallery-nav .sorts:eq(1) a").on("click", function () {
        InitKv(2);
        $(this).addClass("on").parent().siblings(".sorts").each(function () { $(this).children("a").removeClass("on") });
    });
    $(".mb-gallery-nav .sorts:eq(2) a").on("click", function () {
        InitKv(3);
        $(this).addClass("on").parent().siblings(".sorts").each(function () { $(this).children("a").removeClass("on") });
    });
    $(".mb-gallery-nav .sorts:eq(3) a").on("click", function () {
        InitKv(4);
        $(this).addClass("on").parent().siblings(".sorts").each(function () { $(this).children("a").removeClass("on") });
    });
    $(".mb-gallery-nav .sorts:eq(4) a").on("click", function () {
        InitKv(5);
        $(this).addClass("on").parent().siblings(".sorts").each(function () { $(this).children("a").removeClass("on") });
    });
    $(".mb-gallery-nav .sorts:eq(5)").remove();//视频暂时隐藏
    $(".mb-gallery-nav .sorts:eq(5) a").on("click", function () {
        InitKv(6);
        $(this).addClass("on").parent().siblings(".sorts").each(function () { $(this).children("a").removeClass("on") });
    });

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

        if (indexSwiper.navLeft > 0) {
            indexSwiper.navLeft = indexSwiper.navLeft - moveDis;
            $("ul.ul-wrap").animate({ "margin-left": "-" + indexSwiper.navLeft + "px" });
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

        if (oWidth - indexSwiper.navLeft > pWidth) {
            indexSwiper.navLeft = indexSwiper.navLeft + moveDis;
            $("ul.ul-wrap").animate({ "margin-left": "-" + indexSwiper.navLeft + "px" });
        }
    });

})

var indexSwiper = {
    navLeft:0,
    gallerySwiper:null,
    Init: function () {
        indexSwiper.navLeft = 0;
        if (indexSwiper.gallerySwiper != null) { indexSwiper.gallerySwiper.destroy(true); $(".swiper-wrapper").attr("style", ""); };
        indexSwiper.gallerySwiper = new Swiper('.mb-gallery-container', {
            onSlideChangeStart: function () {
                indexSwiper.ChangeNav();
            },
            nextButton: '.mb-gallery-rg',
            prevButton: '.mb-gallery-lf',
            preloadImages: false,
            lazyLoading: true
        });
        $('.mb-gallery-lf').on('click', function (e) {
            e.preventDefault()
            indexSwiper.gallerySwiper.swipePrev()
        })
        $('.mb-gallery-rg').on('click', function (e) {
            e.preventDefault()
            indexSwiper.gallerySwiper.swipeNext()
        })

        $(".mb-gallery-nav-details li").off('click').on("click", function () {
            if ($(this).hasClass("on"))
                return false;
            var index = $(this).index();
            indexSwiper.gallerySwiper.swipeTo(index);
            $(this).addClass("on").siblings().removeClass("on");
        });
        $(".mb-gallery-lf").hide();

    },
    ChangeNav: function () {
        var index = indexSwiper.gallerySwiper.activeIndex;
        $homeView.Load_pic($(".mb-gallery-img").eq(index), $homeView.winWidth > 750 ? "data-big-src" : "data-small-src");
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
}


function InitKv(type) {
    var datalist = [];
    var title = "";
    GetHotelAlbumList();
    
    switch (type) {
        case 0: datalist = HotelAlbumList; break;
        case 2: datalist = RoomList; title = "房型"; break;
        case 3: datalist = RestList; title = "餐厅"; break;
        case 4: datalist = IntroList; title = "设施"; break;
    }
    $("ul.mb-gallery-scroll").empty();
    $("ul.ul-wrap").empty();


    //生成滚动图片
    $.each(datalist, function (i, item) {
        //大图
        var li = '   	<li class="mb-gallery-item swiper-slide">\
                    	<img class="mb-gallery-img imgauto" \
                            data-small-src="' + item.MiddleSrc + '" \
                            data-big-src="' + item.ImageSrc + '" \
                            data-ratio="0.618" alt="' + item.ImageAlt + '" ></img>\
                    	<h3 class="mb-gallery-intro">' + HotelName + '<span>' + item.ImageAlt + '</span></h3>\
                    </li>';
        if (window.GlobalLangIsHK)
            li = li.traditionalized();
        $("ul.mb-gallery-scroll").append(li);
        //小图
        var li = '  <li ' + (i == 0 ? 'class="on"' : '') + ' >\
                    	<img style="width:100%;height:100%" src="' + item.ThumbSrc + '" alt="' + item.ImageAlt + '" />\
                    </li>';
        if (window.GlobalLangIsHK)
            li = li.traditionalized();
        $("ul.ul-wrap").append(li);
    });
    if (datalist.length == 1)
        $(".mb-gallery-rg").hide();
    else
        $(".mb-gallery-rg").show();

    var eleWidth = $("ul.ul-wrap li:first").outerWidth() + 1;//内边距和边框+margin
    var eleCount = $("ul.ul-wrap li").length;
    $("ul.ul-wrap").width(eleWidth * eleCount);

    indexSwiper.Init();
    //indexSwiper.gallerySwiper.swipeTo(0);
    //$(".swiper-wrapper").width($(".swiper-wrapper .swiper-slide:first").width() * eleCount);
    var $ele = $(".ul-wrap");
    var pOffset = $ele.parent().offset().left;
    var oOffset = $ele.offset().left;
    if (oOffset < pOffset) {
        var moveDis = pOffset - oOffset;
        $ele.animate({ right: '-=' + moveDis }, 200);
    }
    //初始化宽度高度
    $homeView.init();
}
