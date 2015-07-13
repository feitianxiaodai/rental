$(document).ready(function () {
    var hotelId = $(".hd-mb-title.ht-title h1.t").attr("data-id");
    $.ajax({
        type: "GET",
        url: window.AjaxDomain + '/Handler/Common.ashx?typecode=getpromolist&random=' + parseInt(Math.random() * 10000) + '&callback=?',
        dataType: "json",
        async: false,
        data: { hotelId: hotelId, ishk: (window.GlobalLangIsHK ? 1 : 0) },
        success: function (res) {
            $(".hotel-details-column.rg").empty();
            if (res.length > 0) {
                var i = 0;
                $.each(res, function (index, item) {
                    if (i > 5) return;
                    var li = "<a class='campaign' href='" + item.DiscLink + ".html'>" +
                    "<img class='campaign-img imgauto' src='" + item.ImageSrc + "' "+
                    "data-big-src='" + item.ImageSrc + "' " +
                    "data-small-src='" + item.MiddleSrc + "' " +
                    "data-ratio='0.618' alt=''>" +
                    "<span class='campaign-layer-bg'></span>" +
                    "<span class='campaign-layer-txt'><i>" + item.DiscTitle + "</i>" + (item.DiscDesc.length > 20 ? (item.DiscDesc.substr(0, 20) + "…") : item.DiscDesc) + "</span>" +
                "</a>";
                    $(".hotel-details-column.rg").append(li);
                    i++;
                });
                //初始化宽度高度
                $homeView.init();
            }
        },
        error: function () {
        }
    });
})