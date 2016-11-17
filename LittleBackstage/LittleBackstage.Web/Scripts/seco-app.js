sc.app = angular.module('scApp', ['scUtils', 'ngRoute', 'ngAnimate', 'ngTouch'])
    //angular route configuration
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: '_home.html',
            controller: 'HomeCtrl'
        }).when('/pano1', {
            templateUrl: '_pano1.html',
            controller: 'Pano1Ctrl'
        }).when('/pano2', {
            templateUrl: '_pano2.html',
            controller: 'Pano2Ctrl'
        }).when('/pano3', {
            templateUrl: '_pano3.html',
            controller: 'Pano3Ctrl'
        }).otherwise({
            redirectTo: '/'
        });
    }])
    //root controller
    .controller('RootCtrl', ['$scope', '$location', function ($scope, $location) {
        $scope.$on('routeChangeSuccess', function (event, newRoute, oldRoute) {
            if (ga) {
                ga('send', 'pageview', $location.path());
            }
        });
    }])
    //home controller
    .controller('HomeCtrl', ['$scope', '$timeout', function ($scope, $timeout) {
        $timeout(function () {
            $scope.curStage = 1;
        }, 100);

        $scope.pressing = function () { };
    }])
    //the first pano scene
    .controller('Pano1Ctrl', ['$scope', '$rootScope', function ($scope, $rootScope) {
        $scope.panoConfig = 'pano1_config.xml';
    }])
    //the second pano scene
    .controller('Pano2Ctrl', ['$scope', function ($scope) {
        $scope.panoConfig = 'pano2_config.xml';
    }])
    .controller('Pano3Ctrl', ['$scope', function ($scope) {
        $scope.panoConfig = 'livingroom.xml';
    }])
    //pano setup
    .directive('pjPano', ['$timeout', function ($timeout) {
        var pano, timer;
        return {
            link: function (scope, element, attrs) {
                //delay to prevent pano initialize failure
                $timeout(function () {
                    pano = sc.main.createPano(element.attr('id'), scope.panoConfig);
                }, 1000);
                timer = setInterval(function () {
                    if (typeof (pano) != 'undefined' && pano.isLoaded) {
                        clearInterval(timer);
                        scope.$apply(function () {
                            scope.isLoaded = true;
                        });
                    }
                }, 500);
            }
        };
    }])
    .directive('pjLoad', ['$timeout', function ($timeout) {
        var size = sc.assets.images.length;
        return {
            link: function (scope, element, attrs) {
                var loaded = 0;
                function handleFileComplete(e) {
                    scope.$apply(function () {
                        element.html('100 %');
                        scope.curStage = 2;
                    });
                }
                function handleFileLoad(e) {
                    loaded++;
                    var progress = ((loaded / size) * 100).toFixed(0).toString() + ' %';
                    element.html(progress);
                }

                var preload = new createjs.LoadQueue();
                preload.addEventListener("fileload", handleFileLoad);
                preload.addEventListener("complete", handleFileComplete);

                $timeout(function () {
                    for (var i = 0; i < sc.assets.images.length; i++) {
                        preload.loadFile(sc.assets.images[i]);
                    }
                }, 2400);
            }
        };
    }])
    .directive('pjFinger', ['$location', function ($location) {
        return {
            link: function (scope, element, attrs) {
                var hammer = new Hammer(element[0]);
                hammer.get('press').set({
                    time: 300
                });
                hammer.on('press', function () {
                    scope.$apply(function () {
                        scope.curStage = 3;
                    });
                    //scope.$apply(function () {
                    //    $location.path('/pano1');
                    //});
                    setTimeout(function () {
                        scope.$apply(function () {
                            $location.path('/pano1');
                        });
                    }, 4500); //从触发指纹识别开始计算跳转pano1的时间
                });
            }
        };
    }])
    .directive('scOnSwitchBySwipe', ['$animate', function ($animate) {
        var dragging = false;
        return {
            link: function (scope, element, attrs) {
                var hammer = new Hammer(element[0]);
                hammer.on('panleft', function () {
                    if (dragging) {
                        return;
                    }
                    dragging = true;
                    var $items = element.find(attrs.scOnSwitchBySwipe).children('.on');
                    if (!$items.is(':last-child')) {
                        element.removeClass('prev').addClass('next');
                        $items.each(function () {
                            $animate.removeClass($(this), 'on');
                            $animate.addClass($(this).next(), 'on');
                        });
                    }
                });
                hammer.on('panright', function () {
                    if (dragging) {
                        return;
                    }
                    dragging = true;
                    var $items = element.find(attrs.scOnSwitchBySwipe).children('.on');
                    if (!$items.is(':first-child')) {
                        element.removeClass('next').addClass('prev');
                        $items.each(function () {
                            $animate.removeClass($(this), 'on');
                            $animate.addClass($(this).prev(), 'on');
                        });
                    }
                });
                hammer.on('panend', function () {
                    dragging = false;
                });
            }
        };
    }])
    .directive('pjTips', ['$timeout', function ($timeout) {
        var timer;
        function next(ele, scope) {
            clearTimeout(timer);
            var curItem = ele.find('span:visible');
            if (curItem.next().length > 0) {
                curItem.fadeOut(function () {
                    curItem.next().fadeIn();
                });
                timer = setTimeout(function () {
                    next(ele);
                }, 3000);
            }
            else {
                ele.fadeOut(function () {
                    scope.$root.isTipsShown = true;
                });
            }
        }
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    next(element, scope);
                });
                scope.$on('panoLoadComplete', function () {
                    timer = setTimeout(function () {
                        next(element, scope);
                    }, 3000);
                });
            }
        };
    }])
    .directive('pjPanoLoading', function () {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    if (scope.isLoaded) {
                        element.fadeOut(1000);
                        if (!scope.isTipsShown) {
                            scope.$emit('panoLoadComplete');
                        }
                    }
                });
            }
        };
    })

;