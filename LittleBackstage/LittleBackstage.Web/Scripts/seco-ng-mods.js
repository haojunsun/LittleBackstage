var sc = sc || {};
(function (window, angular) {
    'use strict';
    if (!sc.modUtils) {
        sc.modUtils = angular.module('scUtils', []);
    }
    //resize window according to portion
    sc.modUtils.directive('scResize', function () {
        return {
            restrict: 'AC',
            link: function (scope, element, attrs) {
                element.scWinSize();
                $(window).resize(function () {
                    element.scWinSize();
                });
            }
        };
    })
    //anime delay
    .directive('scOnDelay', ['$timeout', '$animate', function ($timeout, $animate) {
        return {
            link: function (scope, element, attrs) {
                $timeout(function () {
                    $animate.addClass(element, 'on');
                }, attrs.scOnDelay);
            }
        };
    }])
    //click to add
    .directive('scOnAdd', ['$animate', function ($animate) {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    if (attrs.scOnAdd) {
                        $(attrs.scOnAdd).each(function () {
                            $animate.addClass($(this), 'on');
                        });
                    }
                    else {
                        $animate.addClass(element, 'on');
                    }
                });
            }
        }
    }])
    //click to remove
    .directive('scOnRemove', ['$animate', function ($animate) {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    if (attrs.scOnRemove) {
                        $(attrs.scOnRemove).each(function () {
                            $animate.removeClass($(this), 'on');
                        });
                    }
                    else {
                        $animate.removeClass(element, 'on');
                    }
                });
            }
        }
    }])
    //click to toggle
    .directive('scOnToggle', ['$animate', function ($animate) {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    if (attrs.scOnToggle) {
                        if ($(attrs.scOnToggle).hasClass('on')) {
                            $(attrs.scOnToggle).each(function () {
                                $animate.removeClass($(this), 'on');
                            });
                        }
                        else {
                            $(attrs.scOnToggle).each(function () {
                                $animate.addClass($(this), 'on');
                            });
                        }
                    }
                    else {
                        if (element.hasClass('on')) {
                            $animate.removeClass(element, 'on');
                        }
                        else {
                            $animate.addClass(element, 'on');
                        }
                    }
                });
            }
        }
    }])
    //switch on class between lists
    .directive('scOnSwitch', function () {
        return {
            link: function (scope, element, attrs) {
                element.children().click(function () {
                    if (attrs.scOnSwitch) {
                        var index = $(this).index();
                        $(attrs.scOnSwitch).children().eq(index).addClass('on').siblings().removeClass('on');
                    }
                    else {
                        $(this).addClass('on').siblings().removeClass('on');
                    }
                });
            }
        }
    })
    //menu display detection
    .directive('scOnComp', ['$location', '$animate', function ($location, $animate) {
        return {
            link: function (scope, element, attrs) {
                function check(curScene) {
                    var scenes = attrs.scOnComp.split(' ');
                    var isHide = false;
                    if (scenes[0] === '!') {
                        isHide = true;
                    }
                    for (var i = 0; i < scenes.length; i++) {
                        if (curScene == scenes[i]) {
                            return isHide ? false : true;
                        }
                    }
                    if (isHide) {
                        return true;
                    }
                }
                function toggleState(newVal) {
                    if (check(newVal)) {
                        $animate.addClass(element, 'on');
                    }
                    else {
                        $animate.removeClass(element, 'on');
                    }
                }
                if (attrs.scCompShowByUrl) {
                    scope.$watch(function () {
                        return $location.path();
                    }, toggleState);
                }
                else if (attrs.scCompShowByVal) {
                    scope.$watch(attrs.scCompShowByVal, toggleState);
                }
                else {
                    scope.$watch('view', toggleState);
                }
            }
        };
    }])
    //next item selection
    .directive('scOnNext', function () {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    var $items = $(attrs.scOnNext).children('.on');
                    if (!$items.last().is(':last-child')) {
                        $items.last().next().addClass('on');
                        $items.first().removeClass('on');
                    }
                });
                
            }
        };
    })
    //prev item selection
    .directive('scOnPrev', function () {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    var $items = $(attrs.scOnPrev).children('.on');
                    if (!$items.first().is(':first-child')) {
                        $items.first().prev().addClass('on');
                        $items.last().removeClass('on');
                    }
                });
            }
        };
    })
    //add on class when current location.path equals href target
    .directive('scOnNavlink', ['$location', '$animate', function ($location, $animate) {
        return {
            link: function (scope, element, attrs) {
                scope.$watch(function () {
                    return $location.path();
                }, function () {
                    if (attrs.href) {
                        var cssName = 'on';
                        if (attrs.scOnNavlink) {
                            cssName = attrs.scOnNavlink;
                        }
                        var curPath = $location.path();
                        var linkPath = attrs.href.substr(1);
                        if (curPath === linkPath) {
                            $animate.addClass(element, cssName);
                        }
                        else {
                            $animate.removeClass(element, cssName);
                        }
                    }
                });
            }
        };
    }])

})(window, window.angular);


(function ($) {
    $.fn.scWinSize = function (options) {
        var settings = $.extend({
            //target width /height
            aspectRatio: 12 / 7
        }, options);

        var windowW = $(window).width();
        var windowH = $(window).height();
        var windowAspect = windowW / windowH;

        return this.each(function () {
            if (windowAspect < settings.aspectRatio) {
                $(this).css({
                    "height": "100%",
                    "width": parseInt(windowH * settings.aspectRatio) + "px",
                    "margin-left": parseInt((windowH * settings.aspectRatio - windowW) / -2) + "px"
                });
            }
            else {
                $(this).css({
                    "height": parseInt(windowW / settings.aspectRatio) + "px",
                    "width": "100%",
                    "margin-top": parseInt((windowW / settings.aspectRatio - windowH) / -2) + "px"
                });
            }
        });
    };
})(jQuery);