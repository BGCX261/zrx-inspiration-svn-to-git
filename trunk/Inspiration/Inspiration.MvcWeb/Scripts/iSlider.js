
(function ($, undefined) {
    $.widget("ui.iSlider", $.ui.mouse, {
        options: {
            mouseStart: function (e) { },
            mouseDrag: function (e) { },
            mouseStop: function (e) { },
            mouseCapture: function (e) { return true; }
        },
        _init: function () {
            return this._mouseInit();
        },
        _create: function () {
            var self = this, o = this.options;
        },
        destroy: function () {
            this._mouseDestroy();
        },
        _mouseCapture: function (event) {
            if (event.srcElement.id == "sliderBar") {
                this._mouseSliding = true;
            }

            $("#msg").html("capture");
            return true;
        },
        _mouseStart: function (event) {
            if (this._mouseSliding) {
                this._offset = event.pageX - $("#sliderBar").offset().left;
                $("#msg").html("_mouseStart");
            }
            return true;
        },
        _mouseDrag: function (event) {
            if (this._mouseSliding) {
                var targetLeft = event.pageX - $(this.element).offset().left - this._offset;
                if (targetLeft < 0) {
                    targetLeft = 0;
                }
                if (targetLeft > ($(this.element).width() - $("#sliderBar").width())) {
                    targetLeft = ($(this.element).width() - $("#sliderBar").width());
                }

                $("#sliderBar").css("left", targetLeft);

                $("#msg").html(event.pageX + "," + event.pageY);

            }
            //            var position = { x: event.pageX, y: event.pageY }

            //            this._slide(event, this._handleIndex, normValue);

            return true;
        },
        _mouseStop: function (event) {
            this._mouseSliding = false;
            this._offset = 0;
            return true;
        }
    });
    $.extend($.ui.iSlider, {
        version: "1.8.20"
    });
} (jQuery));
