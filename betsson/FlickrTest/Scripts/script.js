/*
    // This can enable "loading" for every AJAX.
    $body = $("body");
    $(document).on({
        ajaxStart: function () { $body.addClass("loading"); },
        ajaxStop: function () { $body.removeClass("loading"); }
    });
*/
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g, function (m, n) {
        return args[n] ? args[n] : m;
    });
};

var tags = null;
$("#tags").keyup(function () {
    clearTimeout(tags);
    tags = setTimeout(function () {
        if ($("#tags").val().length < 3) {
            return false;
        }

        //var URL = window.location + "/Home/GetImages?tags=" + $("#tags").val();
        var url = window.location + "/Home/GetImages";
        var params = "tags=" + $("#tags").val();

        $("#link").html(url).attr("href", url);

        // This is "loading" overlay
        var body = $("body");
        body.addClass("loading");
        var cacheUsed = $("#cacheUsed");
        //$.getJSON(URL, function (data) {
        cacheUsed.html("Loading...").show(); // This is "loading", also, show it!
        $.post(url, params, function (data) {
            var container = $("#container");
            container.html("");
            var cacheUsed = $("#cacheUsed");
            cacheUsed.html(data.Cached ? "Loaded from cache" : "Loaded from flickr");
            if (null == data.Images || 0 == data.Images.length) {
                cacheUsed.html(data.Cached ? "Loaded from cache, no results" : "Loaded from flickr, no results");
            } else {
                cacheUsed.html((data.Cached ? "Loaded from cache, {0} results" : "Loaded from flickr, {0} results").format(data.Images.length));
                $.each(data.Images, function () {
                    var item = $("<div class='item'>");
                    var thumb = $("<div class='group'>");
                    var image = $("<img class='image'>")
                        .attr("src", this.ImageUrl).attr("alt", this.Title);
                    var title = $("<span class='title'>")
                        .html(this.Title);
                    thumb.append(image);
                    thumb.append(title);    // Title after image
                    item.append(thumb);
                    // FadeIn this way wanted?
                    container.append(item.fadeIn(3000));
                });
            }
            body.removeClass("loading");
        }, 'json');
    }, 250);
})