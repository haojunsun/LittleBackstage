var sc = sc || {};
(function (window, angular) {
    'use strict';
    if (!sc.modUtils) {
        sc.modUtils = angular.module('scUtils', []);
    }
    angular.forEach(['panleft:scDragSwipeLeft', 'panright:scDragSwipeRight', 'panup:scDragSwipeUp', 'pandown:scDragSwipeDown'], function (val) {
        var hammerEv = val.split(':')[0];
        var angularEv = val.split(':')[1];
        sc.modUtils.directive(angularEv, ['$parse', function ($parse) {
            var dragging = false;
            return {
                link: function (scope, element, attrs) {
                    var fn = $parse(attrs[angularEv]);
                    Hammer(element[0]).on(hammerEv, function (event) {
                        //event.gesture.stopPropagation();
                        if (dragging) {
                            return;
                        }
                        dragging = true;
                        fn(scope, {$event: event});
                    });
                    Hammer(element[0]).on('panend', function () {
                        dragging = false;
                    });
                }
            };
        }]);
    });

})(window, window.angular);
