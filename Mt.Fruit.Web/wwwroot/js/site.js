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
            getArticles(page);//根据点击页数渲染页面
        }
    });
};

function getArticles(page) {
    $.get("/list/" + categoryId + "?page=" + page, function (res) {
        switch (categoryId) {
            case 43:
                render1(res.data);
                break;
            case 44:
                break;
            default:
                render(res.data);
                break;
        }

        setPaginator(res);
    });
}

function render(data) {
    $(".table tbody").empty();
    var html = [];
    $.each(data, function (i) {
        html.push("<tr>");
        var row = $("#trExample").html();
        row = row.replace("{0}", this.title);
        row = row.replace("{1}", this.author);
        row = row.replace("{2}", this.replys + "/" + this.clicks);
        row = row.replace("{3}", this.lastReplyUser == null ? "" : this.lastReplyUser);
        html.push(row);

        html.push("</tr>");
    });

    $(".table tbody").html(html.join());
}

function render1(data) {
    $("#list").empty();
    var html = [];
    $.each(data, function (i) {
        html.push("<div class='row mb-3'>");
        var row = $("#example").html();

        if (this.cover) {
            row = row.replace("{4}", this.cover);
        }
        else {
            row = row.replace("{4}", "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIHZpZXdCb3g9IjAgMCA2NCA2NCIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+PCEtLQpTb3VyY2UgVVJMOiBob2xkZXIuanMvNjR4NjQKQ3JlYXRlZCB3aXRoIEhvbGRlci5qcyAyLjYuMC4KTGVhcm4gbW9yZSBhdCBodHRwOi8vaG9sZGVyanMuY29tCihjKSAyMDEyLTIwMTUgSXZhbiBNYWxvcGluc2t5IC0gaHR0cDovL2ltc2t5LmNvCi0tPjxkZWZzPjxzdHlsZSB0eXBlPSJ0ZXh0L2NzcyI+PCFbQ0RBVEFbI2hvbGRlcl8xNmRiYTFmMzk0MSB0ZXh0IHsgZmlsbDojQUFBQUFBO2ZvbnQtd2VpZ2h0OmJvbGQ7Zm9udC1mYW1pbHk6QXJpYWwsIEhlbHZldGljYSwgT3BlbiBTYW5zLCBzYW5zLXNlcmlmLCBtb25vc3BhY2U7Zm9udC1zaXplOjEwcHQgfSBdXT48L3N0eWxlPjwvZGVmcz48ZyBpZD0iaG9sZGVyXzE2ZGJhMWYzOTQxIj48cmVjdCB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIGZpbGw9IiNFRUVFRUUiLz48Zz48dGV4dCB4PSIxMi41IiB5PSIzNi41Ij42NHg2NDwvdGV4dD48L2c+PC9nPjwvc3ZnPg==");
        }
        row = row.replace("{0}", this.title);
        row = row.replace("{1}", this.replys);
        row = row.replace("{2}", this.likes);
        row = row.replace("{3}", this.lead);
        html.push(row);

        html.push("</div>");
    });

    $("#list").html(html.join());
}