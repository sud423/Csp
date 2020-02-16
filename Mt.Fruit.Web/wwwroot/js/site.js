/**
 * 网站脚本
 */
var scroll_top = {
	"#qiyejieshao" : 670,
	"#gongkaike" : 1420,
	"#kechengyudingzhi" : 2585,
	"#yuanxiao" : 3340,
	"#zhuanjiayuxiaoyou" : 4175,
	"#xianshangxuexi" : 5310
};

var categoryId = 0;

$(function () {

    $(".nav a").click(function () {
        setTimeout(setScrollTop, 10);
    });

    setScrollTop();
});

function setScrollTop() {

	var key = window.location.hash;
	if (!key)
		return;

	var h = scroll_top[key];

	$("html,body").scrollTop(h);
}

//分页功能
var setPaginator = function (data) {
    $('.pagination').bootstrapPaginator({
        bootstrapMajorVersion: 3, //对应bootstrap版本
        size: 'small', //分页大小
        currentPage: data.currentPage, //当前页
        numberOfPages: 3, //显示的页数
        totalPages: data.totalPage, // 总页数
        /**
         * 分页点击事件
         * @param {any} event [jquery对象]
         * @param {any} originalEvent [dom原生对象]
         * @param {any} type [按钮类型]
         * @param {any} page [点击按钮对应的页码]
         */
        onPageClicked: function (event, originalEvent, type, page) {
            render(page);//根据点击页数渲染页面
        }
    });
};

function render(page) {
    $.get("/list/" + categoryId + "?page=" + page, function (data) {
        $(".table tbody").empty();
        var html = [];
        $.each(data.data, function (i) {
            html.push("<tr>");
            var row = $("#trExample").html();
            row = row.replace("{0}", this.title);
            row = row.replace("{1}", this.user.nickName);
            row = row.replace("{2}", this.replys + "/" + this.clicks);
            row = row.replace("{3}", this.lastReplyUser == null ? "" : this.lastReplyUser);
            html.push(row);

            html.push("</tr>");
        });

        $(".table tbody").html(html.join());
        setPaginator(data);
    });
}
