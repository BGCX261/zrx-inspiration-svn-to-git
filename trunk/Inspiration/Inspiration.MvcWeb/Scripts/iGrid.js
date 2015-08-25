
(function ($, undefined) {
    $.widget("ui.iGrid", {
        options: {
            name: "",
            width: "500px",
            height: "250px",
            hoverable: true,
            selectable: true
        },
        _init: function () {
        },
        _create: function () {
            var self = this, o = this.options;
            var $trs = $("tbody tr", self.element);

            if (o.hoverable) {
                $trs.hover(function () {
                    if (!$(this).hasClass("tr-selected")) {
                        $(this).addClass("tr-hover");
                    }
                }, function () {
                    $(this).removeClass("tr-hover");
                });
            }

            if (o.selectable) {
                if ($(".igrid-chkall", self.element).length > 0) {
                    var $chkall = $(".igrid-chkall", self.element);
                    $chkall.parent().on("click", function () {
                        if ($chkall.attr("checked") == "checked") {
                            $chkall.removeAttr("checked");
                        }
                        else {
                            $chkall.attr("checked", "checked");
                        }
                        $chkall.trigger("change");
                    });
                    $chkall.on("change", function () {
                        if ($(this).attr("checked") == "checked") {
                            $(".igrid-chk", self.element).attr("checked", "checked");
                        }
                        else {
                            $(".igrid-chk", self.element).removeAttr("checked");
                        }
                    });
                }

                $trs.on("click", function () {
                    var $tr = $(this);
                    var selected = $tr.hasClass("tr-selected")
                     || ($(".igrid-chk", $tr).attr("checked") == "checked");

                    if (selected) {
                        $tr.removeClass("tr-selected");
                        if ($(".igrid-chk", this).length > 0) {
                            $(".igrid-chk", this).removeAttr("checked");
                        }
                    }
                    else {
                        $("tbody tr", self.element).removeClass("tr-selected");
                        $tr.addClass("tr-selected");
                        if ($(".igrid-chk", this).length > 0) {
                            $(".igrid-chk", this).attr("checked", "checked");
                        }
                    }

                });
            }
        },
        destroy: function () {
        }
    });
    $.extend($.ui.iGrid, {
        version: "1.8.20"
    });

    //    $.fn.iDialog = function (options) {
    //        alert("3");
    //        return $("<div></div>").iDialog(options);
    //    };
} (jQuery));
