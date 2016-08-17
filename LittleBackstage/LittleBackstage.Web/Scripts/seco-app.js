var sc = sc || {};
sc.app = angular.module('scApp', [])

    .controller('RootController', ['$scope', '$location', function ($scope, $location) {

    }])
    .controller('HomeController', ['$scope', '$location', '$http', function ($scope, $location, $http) {
        $scope.pageIndex = 1;//页码
        $scope.pageSize = 115;//条数每页
        $scope.videoList = [];
        $scope.datacount = 0;//总条数
        $scope.totalpage = 0;//总页数
        $scope.searchtype = true;

        $scope.mz = '';
        $scope.searchkey = '';
        $scope.yzfs = '';

        //进入详情页
        openDetail = function (whid, ele) {
            console.log(whid);
            window.location.href = '/home/detail?instruments=' + whid;
        }
        //全文or乐器名
        $scope.searchEvent = function () {
            $scope.searchtype = $('#searchtype1')[0].checked;//全局为true  乐器名为false
        }

        //$.pagination('pages', 1, 6, 13, "", { keyword: 'hello world' });
        //加载数据
        $scope.getVedioList = function () {
            $scope.videoList = [];
            var state = $scope.searchtype ? 0 : 1;
            //alert($scope.searchtype + "," + state);
            $http.post(sc.baseUrl + 'ForExcel/SeniorSearch', { "state": state, "key": $scope.searchkey, "yzfs": $scope.yzfs, "mz": $scope.mz, "pageSize": $scope.pageSize, "pageIndex": $scope.pageIndex }).success(function (data) {
                console.log(data);
                $scope.datacount = data.totalCount;
                $scope.totalpage = ($scope.datacount / $scope.pageSize) >> 0;
                $scope.videoList = data.list;
                //重新加载页码
                //$.pagination('pages', $scope.pageIndex, $scope.pageSize, $scope.datacount, "", { keyword: 'hello world' });

                setTimeout(function () {
                    $('#demo').jplist({
                        itemsBox: '.list'
                   , itemPath: '.list-item'
                   , panelPath: '.jplist-panel'

                        //save plugin state
                   , storage: 'localstorage' //'', 'cookies', 'localstorage'
                   , storageName: 'jplist-div-layout'
                    });
                }, 100);

            }).error(function (data) {
                console.log("查询失败");
            });
        }

        $scope.getVedioList();

        //民族选择
        changeSelect = function (eve) {
            $scope.mz = $(eve).val() == '全部' ? '' : $(eve).val();
            $scope.pageIndex = 1;
            $scope.getVedioList();
        }
        //演奏方式
        $scope.changeFs = function (val) {
            $scope.yzfs = val == '全部' ? '' : val;
            $scope.pageIndex = 1;
            $scope.getVedioList();
        }
        //搜素关键字
        $scope.changeSearchkey = function () {
            $scope.searchkey = $("#searchkey").val();
            console.log($scope.searchkey);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        }

        //翻页
        changePage = function (ele) {
            var nextpage = $(ele).text();
            if (nextpage == '第一页') {
                $scope.pageIndex = 1;
            } else if (nextpage == '下一页') {
                $scope.pageIndex = parseInt($scope.pageIndex) + 1;
            } else if (nextpage == '最后一页') {
                $scope.pageIndex = $scope.totalpage;
            } else if (nextpage == '上一页') {
                $scope.pageIndex = $scope.pageIndex - 1;
            } else {
                $scope.pageIndex = nextpage;
            }
            $scope.getVedioList();
        }

    }])
    .controller('VideoListController', ['$scope', '$http', '$location', function ($scope, $http, $location) {

    }])
    .controller('DetailController', ['$scope', '$http', function ($scope, $http) {
        var whid = window.location.search.indexOf('=') > -1 ? window.location.search.split('=')[1] : "";
        $scope.videoinfo = {};
        $scope.yzjflist = [];
        $scope.yzjffirst = [];
        $scope.yzjfsecond = [];
        $scope.imglist = [];

        $scope.sfyqlist = [];
        $scope.sfyqmp3 = [];
        $scope.sfyqmp4 = [];

        //获取单条数据
        $scope.getVideoInfo = function () {
            if (!whid)
                return;
            $http.post(sc.baseUrl + 'ForExcel/Find', { "id": whid }).success(function (data) {
                console.log(data);
                $scope.videoinfo = data;
                $scope.yzjflist = data.YanZhouJiFa_MingChen.split(',');//演奏技法名
                $scope.yzjffirst = data.YanZhouJiFa_First.split(',');
                $scope.yzjfsecond = data.YanZhouJiFa_Second.split(',');

                $scope.sfyqlist = data.ShiFanYuQu_QuMuMing.split(',');//示范乐曲名
                $scope.sfyqmp3 = data.ShiFanYuQu_YinPin.split(',');
                $scope.sfyqmp4 = data.ShiFanYuQu_ShiPin.split(',');

                $scope.imglist = data.Img.split(',');
            }).error(function (data) {
                console.log("查询失败");
            });
        }

        $scope.getVideoInfo();

        openModal = function (eve) {
            event.preventDefault();
            $('.cd-popup3').addClass('is-visible3');

            var vid = $(eve).attr('_videoid');

            var curWwwPath = window.document.location.href;
            var pathName = window.document.location.pathname;
            var pos = curWwwPath.indexOf(pathName);
            var localhostPaht = curWwwPath.substring(0, pos);
            console.log(localhostPaht + '/Uploads/videos/' + $scope.videoinfo.RenGongBianMa + '/' + vid + '.wav');
            var media = document.getElementById("audioplayer");
            media.src = localhostPaht + '/Uploads/videos/' + $scope.videoinfo.RenGongBianMa + '/' + vid + '.mp3';
            media.load();
            // media.play();

        }

        openVideoModal = function (eve) {
            event.preventDefault();
            $('.cd-popup4').addClass('is-visible3');

            var vid = $(eve).attr('_videoid');

            var curWwwPath = window.document.location.href;
            var pathName = window.document.location.pathname;
            var pos = curWwwPath.indexOf(pathName);
            var localhostPaht = curWwwPath.substring(0, pos);
            console.log(localhostPaht + '/Uploads/videos/' + $scope.videoinfo.RenGongBianMa + '/' + vid + '.mp4');
            //$('#videosource').attr("src", localhostPaht + '/Uploads/videos/' + vid + '.mp4');
            var myVideo = document.getElementById('videoplayer');
            myVideo.src = localhostPaht + '/Uploads/videos/' + $scope.videoinfo.RenGongBianMa + '/' + vid + '.mp4';
            myVideo.load();
            //myVideo.play();

        }

    }])

;;