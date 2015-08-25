
(function ($, undefined) {
    $.widget("ui.iDialog", {
        options: {
            name: "",
            title: "iDialog",
            width: "500px",
            height: "250px",
            minWidth: "500px",
            minHeight: "300px",
            useIframe: false,
            url: ""
        },
        _init: function () {
        },
        _create: function () {
            var self = this, o = this.options;
            self.resizing = false;
            //init
            //            self.element.attr("id", o.name);
            //            self.element.addClass("iDialog")
            //            .addClass("css3")
            //            .addClass("iDialog-corner")
            //            .addClass("iDialog-shadow");
            //title

            //            $("<div class='iDialog-header iDialog-header-corner'><span class='iDialog-header-title'>" + o.title +
            //            "</span><span class='iDialog-header-close'>x</span></div>").appendTo(self.element[0]);
            //button
            //            $("<div class='iDialog-header-close'>x</div>").appendTo($(".iDialog-header", self.element[0]));
            $(".title-close", self.element).on("click", function () {
                self.close();
            });
            //            self.element.appendTo($("body")[0]);

            //content

            //position
            self.element.position({
                of: $(window),
                my: "center center",
                at: "center center"
            });
            //content

            if (o.useIframe) {
                $(".content", self.element).css("overflow", "hidden");
                //                $("<iframe src='" + o.url + "' width='100%' height='100%' frameborder='0' scrolling='auto'></iframe>").
                //                appendTo($(".content-middle", self.element));
            }
            //draggable
            self.element.draggable({ handle: ".iDialog .title" });
            self.element.resizable({
                helper: "iDialog-resizable-helper",
                stop: function () {
                    self.resizeContent();
                }
            });

            self.element.hide();
        },
        destroy: function () {
        },
        open: function () {
            var self = this, o = this.options;
            var title = $(".title", self.element);
            self.element.show();
            if (o.url != '') {
                var iframe = $(".content iframe", self.element);
                if (iframe.length > 0) {
                    iframe.attr("src", o.url);
                    iframe.height(self.element.height() - title.height());
                }
                else {
                    $.ajax({
                        url: o.url,
                        type: 'GET',
                        success: function (res) {
                            $(".content", self.element).html(res);
                        },
                        error: function (err) {
                            debugger;
                            alert(err);
                        }
                    });
                }
                //                $.ajax({
                //                    url: o.url,
                //                    type: 'GET',
                //                    success: function (res) {
                //                        $(".iDialog-content", self.element).html(res.responseText);
                //                    }
                //                });

                //                $.ajax({
                //                    url: o.url,
                //                    success: function (data) {
                //                        alert(data);
                //                    },
                //                    error: function (hr, status) {
                //                        alert(status);
                //                    }
                //                });

                //                $.ajax({
                //                    url: o.url,
                //                    type: "GET",
                //                    dataType:"html"
                //                }).done(function (data) {
                //                    $(".iDialog-content", self.element).html(data);
                //                }).fail(function (hr, status) {
                //                    alert(status);
                //                });
            }

        },
        hide: function () {
            var self = this, o = this.options;
            self.element.hide();

        },
        close: function () {
            var self = this, o = this.options;
            self.element.hide();
        }
                ,
        resizeContent: function () {
            var self = this, o = this.options;
            //            if (!self.resizing) {
            //                self.resizing = true;
            var title = $(".title", self.element);
            var iframe = $(".content iframe", self.element);
            iframe.height(self.element.height() - title.height());
            self.resizing = false;
            //            }

            //            var content = $(".content", self.element);
            //            var content_left = $(".content-left", self.element);
            //            var content_middle = $(".content-middle", self.element);
            //            var content_right = $(".content-right", self.element);
            //            var contentHeight = self.element.height() - $(".title", self.element).height() - $(".foot", self.element).height();
            //            content.height(contentHeight);
            //            content_left.height(contentHeight);
            //            content_middle.height(contentHeight);
            //            content_right.height(contentHeight);
        }
    });
    $.extend($.ui.iDialog, {
        version: "1.8.20"
    });

    //    $.fn.iDialog = function (options) {
    //        alert("3");
    //        return $("<div></div>").iDialog(options);
    //    };
} (jQuery));
