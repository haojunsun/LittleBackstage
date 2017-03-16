var sc = sc || {};
sc.app = angular.module('scApp', [])

    .controller('RootController', ['$scope', '$location', function ($scope, $location) {

    }])
    .controller('HomeController', ['$scope', '$http', function ($scope, $http) {
        $scope.listtype = '';//10大类
        $scope.searchkey = '';//搜索词
        $scope.datadetail = '';
        $scope.imglist = '';

        //点击10大类
        $scope.changetype = function (type) {
            location.href = encodeURI("/home/VideoList?type=" + type);
        }
        //搜索
        $scope.search = function () {
            $scope.searchkey = $('#searchkey').val();
            location.href = "/home/VideoList?searchkey=" + $scope.searchkey;
        }
    }])
    .controller('VideoController', ['$scope', '$http', function ($scope, $http) {
        var type, searchkey,
            url = decodeURI(window.location.search);

        if (url.indexOf('searchkey') > -1) {
            searchkey = url.split('=')[1];
        }
        else if (url.indexOf('type') > -1) {
            type = url.split('=')[1];
        }

        //获取数据
        $scope.getMainList = function () {
            if (!searchkey)//搜索字
                searchkey = '';
            if (!type)//项目类型
                type = '';
            $scope.videoList = [];
            //$.get(sc.baseUrl + 'ForExcel/SeniorSearch?pageSize=500&pageIndex=1', function (data) {
            //    console.log(data);
            //})

            $http.get(sc.baseUrl + 'ForExcel/SeniorSearch?pageSize=500&pageIndex=1&key=' + searchkey + '&type=' + type).success(function (data) {
                var str = data.list.substring(1);
                var str2 = str.substring(0, str.length - 1);
                if (str2.length > 0) {
                    $('.nodata').css('display', 'none');
                    var arr = [];
                    arr = str2.indexOf('},{') > -1 ? str2.split('},{') : [];
                    if (arr.length > 2) {
                        arr[0] = arr[0] + '}';
                        arr[arr.length - 1] = '{' + arr[arr.length - 1];
                        for (var i = 1; i < arr.length - 1; i++) {
                            arr[i] = '{' + arr[i] + '}';
                        }
                    }
                    else if (arr.length == 2) {
                        arr[0] = arr[0] + '}';
                        arr[1] = '{' + arr[1];
                    } else {//1
                        arr[0] = str2;
                    }

                    for (var i = 0; i < arr.length; i++) {
                        $scope.videoList.push(JSON.parse(arr[i]));
                    }

                    console.log($scope.videoList);
                } else {
                    $('.nodata').css('display', 'block');
                }
                //$scope.videoList = data.list;
            }).error(function (data) {
                console.log("查询失败");
            });
        }

        $scope.getMainList();

        //搜索
        $scope.search = function () {
            searchkey = $('#searchkey').val();
            $scope.getMainList();
        }

        $scope.firstimg = "";

        //获取详情
        $scope.getDataDetails = function (id) {
            $scope.datadetail = '';

            $http.get(sc.baseUrl + 'ForExcel/Find?id=' + id).success(function (data) {
                var str = data.table.substring(1);
                var str2 = str.substring(0, str.length - 1);
                $scope.datadetail = JSON.parse(str2);
                console.log($scope.datadetail);

                var htmlvideo = '  <video id="detailvideo" src="' + "/Uploads/sources/" + $scope.datadetail.FirstLevel + "/" + $scope.datadetail.XiangMuMingCheng + "/" + $scope.datadetail.TypeVideo + ".mp4" + '" controls="controls" width="100%" type="video/mp4"></video>';
                $('#video-content').html(htmlvideo);
                $scope.imglist = $scope.datadetail.Type.substring(1).split(',');
                $scope.firstimg = $scope.imglist[0];
                //$('#detailvideo').attr('src', "/Uploads/sources/" + $scope.datadetail.FirstLevel + "/" + $scope.datadetail.XiangMuMingCheng + "/" + $scope.datadetail.ZhuYaoChuanChengRen + ".mp4");
            }).error(function (data) {
                console.log("查询失败");
            });
        }

        $scope.modalShow = function (whid) {
            $scope.getDataDetails(whid);
            $(".modal-list").addClass("on");
            $('#to_back').fadeOut();
            $('#to_top').fadeOut();
        }
    }])
    .controller('DetailController', ['$scope', '$http', function ($scope, $http) {
        var whid = window.location.search.indexOf('=') > -1 ? window.location.search.split('=')[1] : "";
    }])
;;