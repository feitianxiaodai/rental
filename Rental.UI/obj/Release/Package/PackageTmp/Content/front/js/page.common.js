
/*全局事件*/
/*
1.判断全局语言 window.GlobalLangIsHK 
2.根据语言设置简繁体高亮
3.根据搜索引擎设置电话
4.EDM邮件部分 SubmitEDM
5.生成页面链接二维码
6.初始化日期控件
7.获得酒店实时价格  GetGoldenFeeds
8.获得国内/国外热门城市 GetCityList
9.腾讯地图 TxMapInit
10.谷歌地图 GoogleMapInit
11.全局ajax域名 AjaxDomain
*/

//设置全局简繁体
function SetGlobalLang() {
    window.GlobalLangIsHK = false;//是否为繁体；
    var pageUrl = window.location.href;
    var langIndex_hk = pageUrl.toLowerCase().indexOf("zh-hk");
    if (langIndex_hk > 0) {
        var cnUrl = pageUrl.substring(0, langIndex_hk) + 'zh-CN' + pageUrl.substring(langIndex_hk + 5);
        window.GlobalLangIsHK = true;
        $('.hd-mb-lang a:eq(0),.jiant a').on("click", function () {
            //if (typeof (_gaq) != "undefined")
            //    _gaq.push(['_trackEvent', 'hilton', 'language', 'simplified']);
            window.location.href = cnUrl;
        });
        $('.hd-mb-lang a:eq(1),.fant a').addClass('on').siblings().removeClass("on");
        //设置页脚希尔顿酒店官网Url
        $('div.global a:eq(1)').attr('href', '/zh-HK/index.html');
    }
    else {
        var langIndex_cn = pageUrl.toLowerCase().indexOf("zh-cn");
        if (langIndex_cn > 0) {
            var hkUrl = pageUrl.substring(0, langIndex_cn) + 'zh-HK' + pageUrl.substring(langIndex_cn + 5);
            window.GlobalLangIsHK = false;
            $('.hd-mb-lang a:eq(1),.fant a').on("click", function () {
                //if (typeof (_gaq) != "undefined")
                //    _gaq.push(['_trackEvent', 'hilton', 'language', 'traditional']);
                window.location.href = hkUrl;
            });
            $('.hd-mb-lang a:eq(0),.jiant a').addClass('on').siblings().removeClass("on");
        }
    }
}

//根据搜索引擎设置电话
function SetTelephone() {
    var $ele = $(".hd-mb-hotline-tel");
    var _telTitle = "免费订房热线：";
    if (window.GlobalLangIsHK)
        _telTitle = _telTitle.traditionalized();
    var _telHtml = "400-820-0500";
    var _telNum = "tel:400-820-0500";
    if (GetQueryString("source") != null) {
        $.cookie("search-cookie", GetQueryString("source"));
    }
    if ($.cookie("search-cookie") != null) {
        var _source = $.cookie("search-cookie");
        if (_source == "search") {
            _telHtml = "400-820-3011";
            _telNum = "tel:" + _telHtml;
        } else if (_source == "baidu" ) {
            _telHtml = "400-820-2012";
            _telNum = "tel:" + _telHtml;
        }
    }
    $ele.attr("href", _telNum);
    $ele.eq(0).html(_telTitle + _telHtml);
    //首页电话
    $("#searchcall").html(_telHtml);
    if (_telHtml.indexOf('|') < 0) {
        if ($(".hotline a").length == 2)
            $(".hotline a:last").remove();
        $(".hotline a").attr("href", _telNum);
        $(".hotline a").html(_telHtml);
    }
}


//生成页面链接二维码
function CreateQrcode() {
    if ($("#urlQrCode")[0]) {
        var qrcode = new QRCode("urlQrCode", {
            width: 136,
            height: 136,
            colorDark: "#000000",
            colorLight: "#ffffff",
            correctLevel: QRCode.CorrectLevel.M
        });
        var url = window.location.href;
        qrcode.makeCode(url);
    }
}


//腾讯地图
//单标记地图
function TxMapInit(loct, hotelgroup, hotelname, hoteladr, hoteltel, eleid, translate) {
    try {
        if (loct.length > 0 && !isNaN(parseFloat(loct.split(',')[0], 10)) && !isNaN(parseFloat(loct.split(',')[1], 10))) {
            var logxy = loct.split(',');
            var latlng = new qq.maps.LatLng(logxy[0], logxy[1]);
            var map = new qq.maps.Map(document.getElementById(eleid), {
                center: latlng,
                zoom: 18
            });
            if (translate) {
                //谷歌坐标转tx坐标
                qq.maps.convertor.translate(latlng, 5, function (res) {
                    latlng = res[0];
                });
            }
            var marker = new qq.maps.Marker({
                map: map,
                position: latlng
            });
            var anchor = new qq.maps.Point(29, 28),
                 size = new qq.maps.Size(59, 55),
                 origin = new qq.maps.Point(0, 0),
                 markerIcon = new qq.maps.MarkerImage(
              mapIcon(hotelgroup),
             size,
             origin,
             anchor
           );
            marker.setIcon(markerIcon);
            var info = new qq.maps.InfoWindow({
                map: map,
                content: "<div style='color:#4C4C4C'>" + hotelname + "<br />" + hoteladr + "<br />" + hoteltel + "</div>",
                position: latlng,
            });
            qq.maps.event.addListener(marker, 'click', function () {
                info.open();
            });
        } else {
            $("#" + eleid).html("腾讯地图加载失败！");
        }
    } catch (e) {
        $("#" + eleid).html("腾讯地图加载失败！");
    }
}
//多标记地图
function TxMapInit_MultiMaker(loct, eleid, params)
{
    function Listener(marker, infoWin, hotellatlng, hotelname, hoteladr, hoteltel) {
        qq.maps.event.addListener(marker, 'click', function () {
            infoWin.open();
            infoWin.setContent("<div style='color:#4C4C4C'>" + hotelname + "<br />" + hoteladr + "<br />" + hoteltel + "</div>");
            infoWin.setPosition(hotellatlng);
        });
    }
    try {
        if (loct.length > 0 && !isNaN(parseFloat(loct.split(',')[0], 10)) && !isNaN(parseFloat(loct.split(',')[1], 10))) {
            var logxy = loct.split(',');
            var latlng = new qq.maps.LatLng(logxy[0], logxy[1]);
            var map = new qq.maps.Map(document.getElementById(eleid), {
                center: latlng,
                zoom: 10
            });
            var infoWin = new qq.maps.InfoWindow({
                map: map
                //content: "<div style='color:#4C4C4C'>" + hotelname + "<br />" + hoteladr + "<br />" + hoteltel + "</div>",
                //position: hotellatlng,
            });
            for (var i = 0; i < params.length ; i++) {
                var hotello = params[i].hotello, hotelgroup = params[i].hotelgroup, hotelname = params[i].hotelname,
                    hoteladr = params[i].hoteladr, hoteltel = params[i].hoteltel, translate = params[i].translate;
                if (hotello.length > 0 && !isNaN(parseFloat(hotello.split(',')[0], 10)) && !isNaN(parseFloat(hotello.split(',')[1], 10))) {
                    var hotellogxy = hotello.split(',');
                    var hotellatlng = new qq.maps.LatLng(hotellogxy[0], hotellogxy[1]);
                    if (translate) {
                        //谷歌坐标转tx坐标
                        qq.maps.convertor.translate(hotellatlng, 5, function (res) {
                            hotellatlng = res[0];
                        });
                    }
                    var marker = new qq.maps.Marker({
                        map: map,
                        position: hotellatlng
                    });
                    var anchor = new qq.maps.Point(29, 28),
                         size = new qq.maps.Size(59, 55),
                         origin = new qq.maps.Point(0, 0),
                         markerIcon = new qq.maps.MarkerImage(
                      mapIcon(hotelgroup),
                     size,
                     origin,
                     anchor
                   );
                    marker.setIcon(markerIcon);
                    //qq.maps.event.addListener(marker, 'click', function () {
                    //    infoWin.open();
                    //    infoWin.setContent("<div style='color:#4C4C4C'>" + i + "|||||" + hotelname + "<br />" + hoteladr + "<br />" + hoteltel + "</div>");
                    //    infoWin.setPosition(hotellatlng);
                    //});
                    Listener(marker, infoWin, hotellatlng, hotelname, hoteladr, hoteltel);
                }
            }
        } else {
            $("#" + eleid).html("腾讯地图加载失败！");
        }
    } catch (e) {
        $("#" + eleid).html("腾讯地图加载失败！");
    }
}
function mapIcon(v) {
    switch (parseInt(v, 10)) {
        case 1: return "/images/map_icon/icon_W.png";
        case 2: return "/images/map_icon/icon_conrad.png";
        case 3: return "/images/map_icon/icon_hilton.png";
        case 4: return "/images/map_icon/icon_doubletree.png";
        case 5: return "/images/map_icon/icon_hilton.png";
        case 6: return "/images/map_icon/icons_E.png";
        case 7: return "/images/map_icon/icons_x.png";
        case 8: return "/images/map_icon/icons_hampton.png";
        default: return "/images/map_icon/icons_hampton.png";
    }
}
//加载谷歌地图
function GoogleMapInit(loct, hotelgroup, hotelname, hoteladr, hoteltel, eleid) {
    try {
        //大图加载
        //BigInitialize(loct, hotelname, hoteladr, hoteltel);
        if (loct.length > 0 && !isNaN(parseFloat(loct.split(',')[0], 10)) && !isNaN(parseFloat(loct.split(',')[1], 10))) {
            var logxy = loct.split(',');
            var latlng = new google.maps.LatLng(logxy[0], logxy[1]);
            var myOptions = {
                zoom: 16,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById(eleid), myOptions);
            var image = new google.maps.MarkerImage(
            mapIcon(hotelgroup),
            new google.maps.Size(59, 55),
            new google.maps.Point(0, 0),
            new google.maps.Point(29.0, 28.0)
        );
            var shadow = new google.maps.MarkerImage(
            mapIcon(hotelgroup),
            new google.maps.Size(59, 55),
            new google.maps.Point(0, 0),
            new google.maps.Point(29.0, 28.0)
        );
            var marker = new google.maps.Marker({
                position: latlng,
                map: map,
                title: hotelname,
                icon: image,
                shadow: shadow
            });
            // InfoWindow 内容提示
            var infowindow = new google.maps.InfoWindow({
                content: "<div style='color:#4C4C4C'>" + hotelname + "<br />" + hoteladr + "<br />" + hoteltel + "</div>",
                position: latlng,
                size: new google.maps.Size(50, 50)
            });
            //给marker添加点击事件  
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map); //如果提示窗口关闭了，点击标记图标可再次显示提示主窗口  
            });
        } else {
            $("#" + eleid).html("谷歌地图加载失败！");
        }
    } catch (e) {
        $("#" + eleid).html("谷歌地图加载失败！");
    }
}
//多标记地图
function GoogleMapInit_MultiMaker(loct, eleid, params) {
    function Listener(ma, mk, hotellatlng, hotelname, hoteladr, hoteltel) {
        // InfoWindow 内容提示
        var infowindow = new google.maps.InfoWindow({
            content: "<div style='color:#4C4C4C'>" + hotelname + "<br />" + hoteladr + "<br />" + hoteltel + "</div>",
            position: hotellatlng,
            size: new google.maps.Size(50, 50)
        });
        //给marker添加点击事件  
        google.maps.event.addListener(mk, 'click', function () {
            infowindow.open(ma, mk); //如果提示窗口关闭了，点击标记图标可再次显示提示主窗口  
        });
    }
    try {
        if (loct.length > 0 && !isNaN(parseFloat(loct.split(',')[0], 10)) && !isNaN(parseFloat(loct.split(',')[1], 10))) {
            var logxy = loct.split(',');
            var latlng = new google.maps.LatLng(logxy[0], logxy[1]);
            var myOptions = {
                zoom: 11,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById(eleid), myOptions);
            for (var i = 0; i < params.length ; i++) {
                var hotello = params[i].hotello, hotelgroup = params[i].hotelgroup, hotelname = params[i].hotelname,
                    hoteladr = params[i].hoteladr, hoteltel = params[i].hoteltel;
                if (hotello.length > 0 && !isNaN(parseFloat(hotello.split(',')[0], 10)) && !isNaN(parseFloat(hotello.split(',')[1], 10))) {
                    var hotellogxy = hotello.split(',');
                    var hotellatlng = new qq.maps.LatLng(hotellogxy[0], hotellogxy[1]);
                    var image = new google.maps.MarkerImage(
                    mapIcon(hotelgroup),
                    new google.maps.Size(59, 55),
                    new google.maps.Point(0, 0),
                    new google.maps.Point(29.0, 28.0)
                );
                    var shadow = new google.maps.MarkerImage(
                    mapIcon(hotelgroup),
                    new google.maps.Size(59, 55),
                    new google.maps.Point(0, 0),
                    new google.maps.Point(29.0, 28.0)
                );
                    var marker = new google.maps.Marker({
                        position: hotellatlng,
                        map: map,
                        title: hotelname,
                        icon: image,
                        shadow: shadow
                    });
                    //// InfoWindow 内容提示
                    //var infowindow = new google.maps.InfoWindow({
                    //    content: "<div style='color:#4C4C4C'>" + hotelname + "<br />" + hoteladr + "<br />" + hoteltel + "</div>",
                    //    position: hotellatlng,
                    //    size: new google.maps.Size(50, 50)
                    //});
                    ////给marker添加点击事件  
                    //google.maps.event.addListener(marker, 'click', function () {
                    //    infowindow.open(map, marker); //如果提示窗口关闭了，点击标记图标可再次显示提示主窗口  
                    //});
                    Listener(map, marker, hotellatlng,hotelname, hoteladr, hoteltel);
                }
            }
        } else {
            $("#" + eleid).html("谷歌地图加载失败！");
        }
    } catch (e) {
        $("#" + eleid).html("谷歌地图加载失败！");
    }
}
//跳转预定链接
function OpenBookingUrl() {
    //绑定预定按钮
    $('#bookingUrl').bind('click', function () {
        if (window.GlobalLangIsHK) {
            if ($('#StartTime').val() == '入住日期'.traditionalized() || $('#EndTime').val() == '离店日期'.traditionalized() || $('#StartTime').val() == "" || $('#EndTime').val() == "") {
                alert('请选择入住日期或离店日期'.traditionalized());
                return;
            }
        }
        else {
            if ($('#StartTime').val() == '入住日期' || $('#EndTime').val() == '离店日期' || $('#StartTime').val() == "" || $('#EndTime').val() == "") {
                alert('请选择入住日期或离店日期');
                return;
            }
        }

        var bookingUrl = $('#bookingUrl').attr('data-url'); //预定地址
        var codetype = $("#codetype").val(); //优惠类型

        var inCode = $('#inCode').val(); //优惠代码

        var strsIndate = new Array(); //定义一数组
        strsIndate = $('#StartTime').val().split("-"); //字符分割  
        var strsOutdate = new Array();
        strsOutdate = $('#EndTime').val().split("-");
        var redeemPts = $("#redeempts_check").is(":checked"); //是否使用荣誉积分

        var htmlurl = bookingUrl + "&inputModule=HOTEL_SEARCH" + "&arrivalDay=" + strsIndate[2] + "&arrivalMonth=" + strsIndate[1] + "&arrivalYear=" + strsIndate[0] + "&departureDay=" + strsOutdate[2] + "&departureMonth=" + strsOutdate[1] + "&departureYear=" + strsOutdate[0] + "&redeemPts=" + redeemPts;

        if (inCode == "请输入相应代码" || inCode == "請輸入相應代碼") { inCode = ""; }
        switch (parseInt(codetype, 10)) {
            case 0:
                htmlurl = htmlurl + "&promo_code=" + inCode;
                break;
            case 1:
                htmlurl = htmlurl + "&corporateCode=" + inCode;
                break;
            case 2:
                htmlurl = htmlurl + "&groupCode=" + inCode;
                break;
            case 3:
                htmlurl = htmlurl + "&tid=" + inCode;
                break;
        }
        window.open(htmlurl);

    });
}


// common
$(document).ready(function () {
    $("a[href='http://hhonors.hilton.com.cn/portfolio.html']").attr("href", "javascript:void(0);");
    //window.AjaxDomain = "http://www.hilton.com.cn";
    SetGlobalLang();
    SetTelephone();
    CreateQrcode();
    //initDatePick();
    $(".hd-mb-xin").hide();

    OpenBookingUrl();
    //初始化宽度高度
    $homeView.init();

    //top bar logos
    $(".top-bar-logos li").hover(function () {
        $(this).addClass("on").siblings().removeClass("on");
        $(this).find(".logobar_brand_tooltip").css("visibility", "visible");
    }, function () {
        $(this).removeClass("on").siblings().removeClass("on");
        $(this).find(".logobar_brand_tooltip").css("visibility", "hidden");
    });
    //footer bar logos
    $(".logos li").hover(function () {
        $(this).find(".brand_tooltip").css("display", "block");
    }, function () {
        $(this).find(".brand_tooltip").css("display", "none");
    });
    //手机二维码
    $(".mb-code,.top-bar-code").on("click", function () {
        $(this).siblings(".mb-code-layer").slideDown();
    });
    $(".mb-code-layer .cs,.top-bar-code .cs").on("click", function () {
        $(this).parent(".mb-code-layer").slideUp();

        $('#inCode').val(''); //关闭清空
        $("#redeempts_check").removeAttr('checked');

    });
    //微信
    $(".hd-mb-xin,.hotelwechat").on("click", function () {
        $(".hd-mb-xin").siblings(".mb-code-layer").slideDown();
    });
    //面包屑
    var _size = $(".hd-mb-track .active").size();
    for (var i = 0; i < _size; i++) {
        $(".hd-mb-track .active:eq(" + i + ")").css("z-index", _size - i);
    };

    //
    $(".hotel-campaign-profile .toggle").on("click", function () {
        $(this).toggleClass("on");
        $(".hotel-campaign-profile-terms").toggle();
    });
});
//
var userAgent = navigator.userAgent.toLowerCase();
// Figure out what browser is being used 
jQuery.browser = {
    version: (userAgent.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [])[1],
    safari: /webkit/.test(userAgent),
    opera: /opera/.test(userAgent),
    msie: /msie/.test(userAgent) && !/opera/.test(userAgent),
    mozilla: /mozilla/.test(userAgent) && !/(compatible|webkit)/.test(userAgent)
};
var $homeView = {
    $window: $(window),
    winHeight: null,
    winWidth: null,
    $header: null,
    $viewPort: null,
    $viewPortWidth: null,
    $viewIntroHeight: null,
    $viewPriceHeight: null,
    $viewImgHeight: null,
    $imgauto: null,
    //设置酒店弹出层
    $cityDialogWidth: null,
    $cityDialogHeight: null,
    $cityDialogSwipersize: null,
    $cityDialogSwiperHeight: null,
    $cityDialogSwiperWidth: null,
    $cityDialogSwiperImg: null,
    //设置酒店首页gallery
    $hotelNavWidth: null,
    $hotelviewPortWidth: null,
    //hotel template
    $temGalThumbSize: null,
    //hotel index 2015-02-11 begin
    $indexGalSlideWidth: null,
    //hotel index 2015-02-11 end
    //hotel index 2015-02-12 begin
    $bodyWidth: null,
    //hotel index 2015-02-12 end
    //hotel-campaign-detail
    $hotelCampaignTermsHeight: null,
    init: function () {
        $homeView.$imgauto = $(".imgauto");
        //酒店弹出层信息
        $homeView.$cityDialogSwiperImg = $(".city-hotel-detail-img");
        //hotel template
        $homeView.$temGalThumbSize = $(".mb-gallery-nav-details .photos-wrapper-container .ul-wrap li").size();

        $homeView.$window.resize($homeView.onWindowResize);
        $homeView.onWindowResize();
    },


    ////窗口变化
    onWindowResize: function () {
        $homeView.$header = $(".header").height();
        $homeView.winHeight = $homeView.$window.height();
        $homeView.winWidth = $homeView.$window.width();
        $homeView.$viewPortWidth = $(".computer .img-wrap").width();
        $homeView.$viewIntroHeight = $(".computer .city-intro").height();
        if ($(".computer .city-intro"))
            $homeView.$viewPriceHeight = 42;//$(".computer .city-intro").next("div").height();
        else
            $homeView.$viewPriceHeight = 0;
        $homeView.$viewPort = $homeView.winHeight - $homeView.$header - 10;/* 10px 为viewport 的间距 */
        $homeView.$viewImgHeight = $homeView.$viewPort / 2 - $homeView.$viewIntroHeight - $homeView.$viewPriceHeight - 10- 23;/* 23px 为了viewport 下面的空白 */

        //设置酒店首页gallery
        $homeView.$hotelNavWidth = $(".mainbody-sub").width();
        if ($homeView.winWidth > 750)
        {
            $homeView.$hotelviewPortWidth = $homeView.winWidth - $homeView.$hotelNavWidth;
            if ($(".videoDiv iframe")) {
                $(".videoDiv iframe").height($homeView.$viewPort - $(".mb-gallery-nav").height() + 10).width($homeView.$hotelviewPortWidth);
                $(".mb-gallery-container").height($homeView.$viewPort + 10).width($homeView.$hotelviewPortWidth);
            }
        }
        else
        {
            $homeView.$hotelviewPortWidth = $homeView.winWidth;
            if ($(".videoDiv iframe")) {
                $(".videoDiv iframe").width("100%");
                $(".videoDiv iframe").height($(".videoDiv iframe").width());
                $(".mb-gallery-container").height("auto").width("100%");
            }
        }

        //hotel-campaign-detail
        $homeView.$hotelCampaignTermsHeight = $homeView.$viewPort - 120 - $(".hotel-campaign-profile .inner>p").eq(0).height();// - $(".hotel-campaign-profile .inner>p").eq(1).height();

        ////设置酒店城市列表6栅格
        $homeView.setViewHeight();
        //设置酒店弹出层
        $homeView.setCityDialog();
        //图片预加载
        /* $homeView.preImageEvents(); */
        //酒店弹出层图片预加载
        /* $homeView.preDialogImageEvents(); */

        //hotel index 2015-02-11 begin
        $homeView.$indexGalSlideWidth = $homeView.$hotelviewPortWidth - ($(".mb-gallery-nav .sorts").outerWidth(true) * $(".mb-gallery-nav>div.sorts").length) - 1;
        $homeView.setIndexKvSlideWidth();
        //hotel index 2015-02-11 end
        //hotel index 2015-02-12 begin
        $homeView.$bodyWidth = $("body").width();
        $(".index-kv-pagination").css({ "margin-left": "-" + ($(".index-kv-pagination").outerWidth() / 2) + "px" });
        //hotel index 2015-02-12 end

        //酒店简介页面 文字与服务互换位置
        $homeView.moveHotelDetails();

        //设置不同屏幕大小下的图片切换
        $homeView.setPicSrc();
        $homeView.setRatios();

        //设置宽高
        //$homeView.setWidthHeight();
    },

    //hotel index 2015-02-11 begin
    setIndexKvSlideWidth: function () {
        if ($homeView.winWidth > 750) {
            //$(".mb-gallery-nav-details").width($homeView.$indexGalSlideWidth * 0.68);
            $(".mb-gallery-nav-details").width($homeView.$indexGalSlideWidth);
        } else {
            $(".mb-gallery-nav-details").css("width", "100%");
        }
    },
    //hotel index 2015-02-11 end
    //酒店简介页面 文字与服务互换位置
    moveHotelDetails: function () {
        var $data = $(".hotel-details-column.lf .items");
        if ($homeView.winWidth > 750) {
            $($data).insertAfter($(".hotel-details-column.lf .intro"));
        } else {
            $($data).insertBefore($(".hotel-details-column.lf .intro"));//$($data).insertAfter($(".hotel-details-column.lf .t"));
        };
    },
    //设置不同屏幕大小下的图片切换
    setPicSrc: function () {
        if ($homeView.winWidth > 750) {
            $homeView.Load_pic($(".comtemplate-detail-panel-img"), "data-big-src");//$(".comtemplate-detail-panel-img").each(function () { $(this).attr("src", $(this).attr("data-big-src")); });//电脑
            //hotel index 2015-02-11 begin
            $homeView.Load_pic($(".mb-gallery-img"), "data-big-src"); //$(".mb-gallery-img").each(function () { $(this).attr("src", $(this).attr("data-big-src")); });
            //hotel index 2015-02-11 end
            //hotel index 2015-02-12 begin brandhome
            $homeView.Load_pic($(".index-kv-panel-img"), "data-big-src");//$(".index-kv-panel-img").each(function () { $(this).attr("src", $(this).attr("data-big-src")); });
            //hotel index 2015-02-12 end
            $homeView.Load_pic($(".hotel-campaign-detail-img"), "data-big-src"); //$(".hotel-campaign-detail-img").each(function () { $(this).attr("src", $(this).attr("data-big-src")); });
            $homeView.Load_pic($(".comtemplate-map-img"), "data-big-src");
        } else {
            $homeView.Load_pic($(".comtemplate-detail-panel-img"), "data-small-src"); //$(".comtemplate-detail-panel-img").each(function () { $(this).attr("src", $(this).attr("data-small-src")); });//手机
            //hotel index 2015-02-11 begin
            $homeView.Load_pic($(".mb-gallery-img"), "data-small-src"); //$(".mb-gallery-img").each(function () { $(this).attr("src", $(this).attr("data-small-src")); });
            //hotel index 2015-02-11 end
            //hotel index 2015-02-12 begin brandhome
            $homeView.Load_pic($(".index-kv-panel-img"), "data-small-src"); //$(".index-kv-panel-img").each(function () { $(this).attr("src", $(this).attr("data-small-src")); });
            //hotel index 2015-02-12 end
            $homeView.Load_pic($(".hotel-campaign-detail-img"), "data-small-src"); //$(".hotel-campaign-detail-img").each(function () { $(this).attr("src", $(this).attr("data-small-src")); });
            $homeView.Load_pic($(".comtemplate-map-img"), "data-small-src");
        }
    },

    Load_pic: function ($imgs, dataSrc, callback) {
        //$imgs.each(function () { $(this).attr("src", ''/*$(this).attr("data-big-src")*/); });
        this.loop_f = function (i, o_file, len, f, obj) {
            if (i < len - 1) {
                i = i + 1;
                f(i, o_file, len, obj);
            }
        };
        this.creat_pic = function (i, o_file, len, obj) {
            var f = arguments.callee,
           image = o_file[i];
            // if (i == 0) debugger;
            //image.onload = function () {
            //    if (image.complete == true) {
            //        if (len == 1 && (typeof (callback) == "function")) {
            //            callback.call(image);
            //            return;
            //        }
            //        obj.loop_f(i, o_file, len, f, obj);
            //    }
            //};  
            if ($.browser.msie) {
                if ($.browser.version == 6.0 || $.browser.version == 9.0) {                  //IE9和IE6一样  微软真是怪异
                    image.onreadystatechange = function () {
                        if (image.readyState == "complete") {
                            if (len == 1 && (typeof (callback) == "function")) {
                                callback.call(image);
                                return;
                            }
                            obj.loop_f(i, o_file, len, f, obj);
                        }
                    };
                } else {
                    ie7imagetime = window.setInterval(function () {
                        var rs = image.readyState;
                        if (rs == "complete") {
                            if (len == 1 && (typeof (callback) == "function")) {
                                callback.call(image);
                                return;
                            }
                            window.clearInterval(ie7imagetime);
                            obj.loop_f(i, o_file, len, f, obj);
                        } else {
                            return;
                        }
                    }, 200);
                }
            } else {
                image.onload = function () {
                    if (image.complete == true) {
                        if (len == 1 && (typeof (callback) == "function")) {
                            callback.call(image);
                            return;
                        }
                        obj.loop_f(i, o_file, len, f, obj);
                    }
                };
            }
            image.src = $(image).attr(dataSrc);
        };
        var len = $imgs.length,
             i = 0;
        i < len ? this.creat_pic(i, $imgs, len, this) : '';
    },

    //设置城市列表6栅格
    setViewHeight: function () {
        $(".computer").height($homeView.$viewPort / 2 - 10);
        if ($(".computer .img-wrap")) {
            $homeView.$viewImgHeight = $homeView.$viewImgHeight + 23 - 5
            $(".computer .img-wrap").height($homeView.$viewImgHeight);
        }
        //设置满屏显示
        $(".no-touch #viewport").height($homeView.$viewPort + 10);
        //hotel access
        if ($(".access-map").length > 0) {
            if ($(".swiper-wrapper").length > 0) {
                $(".hotel-details-column,.swiper-container").height($homeView.$viewPort + 10);
            }
            $(".access-map").height($homeView.$viewPort + 10);
            $(".access-guide").height($(".access-guide-inner").height() + 10);
            $("#viewport").height(($(".access-guide-inner").height() > $homeView.$viewPort ? $(".access-guide-inner").height() : $homeView.$viewPort) + 10);
        }
    },
    //小于750取消宽高设置，手机自适应。
    setWidthHeight: function () {
        if ($homeView.winWidth > 750) {
            //hotel detail
            //$(".hotel-details-column.rg .campaign").height(($homeView.$viewPort + 10) / 3).width($homeView.$hotelviewPortWidth / 4);(Math.ceil($(".hotel-details-column.rg .campaign").length / 2))
            $(".hotel-details-column.rg .campaign").height(($homeView.$viewPort + 10) / 3).width(($(".hotel-details-column.rg").width() - 1) / 2);
            //hotel campaign
            $(".campaign-panel").height(($homeView.$viewPort + 10) / 2).width(($homeView.$hotelviewPortWidth - 1) / 2);
            //hotel room 
            $(".comtemplate-panel").height(($homeView.$viewPort + 10) / 2).width(($homeView.$hotelviewPortWidth - 1) / 3);
            $(".comtemplate-panel-dinings").width(($homeView.$hotelviewPortWidth - 1) / 2);

            //暂时隐藏$(".comtemplate-panel>a").height(($homeView.$viewPort + 10) / 2 - 50).width(($homeView.$hotelviewPortWidth - 1) / 3);
            $(".comtemplate-panel>a").height(($homeView.$viewPort + 10) / 2).width(($homeView.$hotelviewPortWidth - 1) / 3);
            $(".comtemplate-panel-dinings>a").width(($homeView.$hotelviewPortWidth - 1) / 2);
            //template
            $(".comtemplate-kv").height($homeView.$viewPort + 10).width($homeView.$hotelviewPortWidth * 0.68);
            $(".comtemplate-kv-thumb").width($homeView.$hotelviewPortWidth * 0.68);
            $(".comtemplate-profile").height($homeView.$viewPort + 10).width($homeView.$hotelviewPortWidth * 0.32 - 1);
            $(".comtemplate-profile .inner").height($homeView.$viewPort + 10-80);
            $(".comtemplate-kv-wrapper").height($homeView.$viewPort + 10).width($homeView.$hotelviewPortWidth * 0.68 * $homeView.$temGalThumbSize);
            $(".comtemplate-kv-items").height($homeView.$viewPort + 10).width($homeView.$hotelviewPortWidth * 0.68);
            //hotel-campaign-detail
            $(".hotel-campaign-detail").height($homeView.$viewPort + 10 /*- 70*/).width($homeView.$hotelviewPortWidth * 0.68);
            $(".hotel-campaign-profile-terms").height($homeView.$hotelCampaignTermsHeight);
            //hotel index 2015-02-11 begin hotelindex
            $(".mb-gallery-container").height($homeView.$viewPort + 10).width($homeView.$hotelviewPortWidth);
            //$(".mb-gallery-container").height($homeView.$viewPort + 10).width($homeView.$hotelviewPortWidth);
            //hotel index 2015-02-11 end
            //hotel index 2015-02-12 begin brandhome
            $(".index-kv").height($homeView.$viewPort + 10).width($homeView.$bodyWidth);
            $(".index-kv-panel").height($homeView.$viewPort + 10).width($homeView.$bodyWidth);
            //hotel index 2015-02-12 end
        } else {
            //hotel campaign
            $(".campaign-panel").height("auto").width("100%");
            $(".campaign-panel-img").css({ "width": "100%", "height": "auto", "margin": 0 });
            //hotel room
            $(".comtemplate-panel").height("auto").width("100%");
            $(".comtemplate-panel>a").height("auto").width("100%")
            $(".comtemplate-panel-img").css({ "width": "100%", "height": "auto", "margin": 0 });
            //template
            $(".comtemplate-kv").height("auto").width("100%");
            $(".comtemplate-profile").height("auto").width("100%");
            $(".comtemplate-kv-wrapper").height("auto").width(($homeView.$temGalThumbSize * 100) + "%")
            $(".comtemplate-kv-items").height("auto").width((100 / $homeView.$temGalThumbSize) + "%");
            $(".comtemplate-detail-panel-img").css({ "width": "100%", "height": "auto", "margin": 0 });
            //hotel-campaign-detail
            $(".hotel-campaign-detail").height("auto").width("100%");
            $(".hotel-campaign-detail-img").css({ "width": "100%", "height": "auto", "margin": 0 });
            $(".hotel-campaign-profile-terms").height("auto");
            //hotel index 2015-02-11 begin hotelindex
            $(".mb-gallery-container").width("100%").height($(".mb-gallery-img:eq(0)").height());
            //$(".mb-gallery-img").css({ "width": "100%", "height": "auto", "margin": 0 });
            //hotel index 2015-02-11 end
            //hotel index 2015-02-12 begin brandhome
            $(".index-kv").width("100%").height($(".index-kv-panel-img:eq(0)").height());
            $(".index-kv-panel").width("100%").height($(".index-kv-panel-img:eq(0)").height());
            //hotel index 2015-02-12 end
        };
    },

    //设置酒店弹出层
    setCityDialog: function () {
        //酒店弹出层信息
        $(".city-dialog-profile .description").height($(".city-dialog-kv").height() - 159);//设置描述文字div高度
        $homeView.$cityDialogWidth = $(".city-dialog").width();
        $homeView.$cityDialogHeight = $(".city-dialog").height();
        $homeView.$cityDialogSwiperWidth = ($homeView.$cityDialogWidth *0.65 - 30);
        $homeView.$cityDialogSwiperHeight = $homeView.$cityDialogSwiperWidth * 0.618;
        $homeView.$cityDialogSwipersize = $(".city-dialog-kv .swiper-pagination-switch").size();

        //$(".city-hotel-detail-img").height($homeView.$cityDialogSwiperHeight);
        $("#swiper-container-div").height($homeView.$cityDialogSwiperHeight).width($homeView.$cityDialogSwiperWidth);
        $("#swiper-wrapper-div .swiper-slide").height($homeView.$cityDialogSwiperHeight).width($homeView.$cityDialogSwiperWidth);
        $("#swiper-wrapper-div").height($homeView.$cityDialogSwiperHeight).width($homeView.$cityDialogWidth * 0.65 - 30 * $homeView.$cityDialogSwipersize);

        $(".city-dialog").css({ "margin-top": "-" + $homeView.$cityDialogHeight / 2 + "px", "margin-left": "-" + $homeView.$cityDialogWidth / 2 + "px" });

        if ($(".city-dialog").length > 0 && $(".city-dialog").offset().top < 0)
            $(".city-dialog").css({ "margin-top": "-" + $homeView.winHeight / 2 + "px" });
        $homeView.setRatio($(".city-hotel-detail-img:first"));
    },

    preImage: function (url, callback) {
        if (typeof (url) != "undefined") {
            //因为img开始并不存在本页面中,因此先实例化一个img对象
            var img = new Image();
            //给这个对象附上传递进来的url信息
            img.src = url;
            //判断这个图片是否已经缓存
            if (img.complete) {
                //用call()方式把图片信息返回
                callback.call(img);
                return;
            }
            //如果上面的图片没有经过缓存信息已存在,那么就在刷新页面的时候先加载图片信息.
            img.onload = function () {
                callback.call(img);
            };
        }
    },
    setRatios: function (e) {
        /*  $homeView.$imgauto.each(function () {
             $homeView.setRatio($(this));
         }); */
        if (e)
            $homeView.setRatio(e);
        else
            $homeView.setRatio($($homeView.$imgauto[0]));
    },

    //计算屏幕宽高比自适应。
    setRatio: function (e) {
        var t = $(e), n = t.height(), r = t.width(), i, s, o;
        var imageSrc = t.attr("src");//+ "?" + (new Date());
        $homeView.preImage(imageSrc, function () {
            n = this.height;
            r = this.width;
            $homeView.reSetRatio(s, o, n, r, t);
        });
        //$homeView.reSetRatio(s, o, n, r, t);
    },

    reSetRatio: function (s, o, n, r, t) {
        s = $homeView.$viewImgHeight;
        o = $homeView.$viewPortWidth;
        //hotel detail 
        if (t.hasClass("campaign-img")) {
            s = ($homeView.$viewPort + 10) / 3;
            o = $homeView.$hotelviewPortWidth / 4;
        };
        //hotel campaign 
        if (t.hasClass("campaign-panel-img")) {
            s = ($homeView.$viewPort + 10) / 2;
            o = $homeView.$hotelviewPortWidth / 2;
        };
        //hotel room 
        if (t.hasClass("comtemplate-panel-img")) {
            //暂时隐藏s = ($homeView.$viewPort + 10) / 2 - 50;
            s = ($homeView.$viewPort + 10) / 2;
            o = $homeView.$hotelviewPortWidth / 3;
        };
        if (t.hasClass("comtemplate-panel-img-dinings")) {
            //暂时隐藏s = ($homeView.$viewPort + 10) / 2 - 50;
            s = ($homeView.$viewPort + 10) / 2;
            o = $homeView.$hotelviewPortWidth / 2;
        };
        //hotel template 
        if (t.hasClass("comtemplate-detail-panel-img")) {
            s = $homeView.$viewPort + 10;
            o = $homeView.$hotelviewPortWidth * 0.68;
        };
        //hotel index 2015-02-11 begin 
        if (t.hasClass("mb-gallery-img")) {
            s = $homeView.$viewPort + 10;
            o = $homeView.$hotelviewPortWidth;
        };
        //hotel index 2015-02-11 end 
        //hotel index 2015-02-12 begin brandhome
        if (t.hasClass("index-kv-panel-img")) {
            s = $homeView.$viewPort + 10;
            o = $homeView.$bodyWidth;
        };
        //hotel index 2015-02-12 end 
        //hotel campaign 
        if (t.hasClass("hotel-campaign-detail-img")) {
            s = $homeView.$viewPort + 10;//- 70;
            o = $homeView.$hotelviewPortWidth * 0.68;
        };
        if (t.hasClass("city-hotel-detail-img")) {
            s = $homeView.$cityDialogSwiperWidth * 0.618;
            o = ($homeView.$cityDialogWidth *0.65 - 30);
        }
        if (t.hasClass("comtemplate-map-img")) {
            s = $homeView.$viewPort + 10;
            if ($homeView.winWidth > 750)
                o = $homeView.$hotelviewPortWidth / 2;
            else
                o = $homeView.$hotelviewPortWidth;
        }

        var u = s / o * 100, a = n / r * 100, f = {
            "margin-top": 0,
            "margin-left": 0
        };

        if (u > a) {
            if (t.hasClass("imgauto")) {
                $homeView.$imgauto.height(s).width("auto");
            }
            else {
                var clsName = t.attr("class");
                if (clsName) {
                    $("." + clsName).height(s).width("auto");
                }
            }
            //$(".swiper-wrapper").height(s).find(".swiper-slide").height(s);
            i = t.width() - o;
            f = {
                "margin-top": 0,
                "margin-left": "-" + i / 2 + "px"
            }
        } else {
            if (t.hasClass("imgauto")) {
                $homeView.$imgauto.width(o).height("auto");
            }
            else {
                var clsName = t.attr("class");
                if (clsName) {
                    $("." + clsName).width(o).height("auto");
                }
            }
            //$(".swiper-wrapper").width(o).find(".swiper-slide").width(o);
            i = t.height() - s;
            f = {
                "margin-left": 0,
                "margin-top": "-" + i / 2 + "px"
            }
        }
        if (t.hasClass("imgauto")) {
            $homeView.$imgauto.css(f);
        }
        else {
            var clsName = t.attr("class");
            if (clsName) {
                $("." + clsName).css(f);
            }
        }
        $homeView.setWidthHeight();
    }
}
