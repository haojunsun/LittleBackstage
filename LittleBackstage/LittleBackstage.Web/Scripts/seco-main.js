sc.main = (function () {
    var bgMusic;
    var lockSound = false;
    var isSoundNeedResume = false;
    var wxCallbacks = {
        // 分享操作开始之前
        ready: function () {
            // 你可以在这里对分享的数据进行重组
            //alert("准备分享");
        },
        // 分享被用户自动取消
        cancel: function (resp) {
            // 你可以在你的页面上给用户一个小Tip，为什么要取消呢？
            //alert("分享被取消");
        },
        // 分享失败了
        fail: function (resp) {
            // 分享失败了，是不是可以告诉用户：不要紧，可能是网络问题，一会儿再试试？
            //alert("分享失败");
        },
        // 分享成功
        confirm: function (resp) {
            // 分享成功了，我们是不是可以做一些分享统计呢？
            //window.location.href='http://192.168.1.128:8080/wwyj/test.html';
            //alert("分享成功");
        },
        // 整个分享过程结束
        all: function (resp) {
            // 如果你做的是一个鼓励用户进行分享的产品，在这里是不是可以给用户一些反馈了？
            //alert("分享结束");
        }
    };
    function hideUrlBar() {
        // hide URL field on the iPhone/iPod touch
        var p = String(navigator.platform);
        container = document.getElementById("container");
        if (p === 'iPad' || p === 'iPhone' || p === 'iPod touch') {
            var v = (navigator.appVersion).match(/OS (\d+)_(\d+)_?(\d+)?/);
            if (parseInt(v[1], 10) >= 7) {
                // iOS >=7
                if (container) {
                    container.style.top = (0) + "px";
                    container.style.left = (0) + "px";
                    container.style.width = (window.innerWidth) + "px";
                    container.style.height = (window.innerHeight) + "px";
                    if (pano) {
                        pano.setViewerSize(window.innerWidth, window.innerHeight);
                    }
                }
                window.scrollTo(0, 0);
            } else {
                if (container) {
                    var cheight;
                    switch (window.innerHeight) {
                        case 208: cheight = 268; break; // landscape
                        case 260: cheight = 320; break; // landscape, fullscreen
                        case 336: cheight = 396; break; // portrait, in call status bar
                        case 356: cheight = 416; break; // portrait 
                        case 424: cheight = 484; break; // portrait iPhone5, in call status bar
                        case 444: cheight = 504; break; // portrait iPhone5 
                        default: cheight = window.innerHeight;
                    }
                    if ((cheight) && ((container.offsetHeight != cheight) || (window.innerHeight != cheight))) {
                        container.style.height = cheight + "px";
                    }
                }
                document.getElementsByTagName("body")[0].style.marginTop = "1px";
                window.scrollTo(0, 1);
            }
        }
    }

    return {
        preInit: function () {
            if (window.addEventListener) {
                window.addEventListener("load", hideUrlBar);
                window.addEventListener("resize", hideUrlBar);
                window.addEventListener("orientationchange", hideUrlBar);
            }
            setTimeout(function () { hideUrlBar(); }, 10); //hide url bar in iphone

            WeixinApi.ready(function (Api) {
                //Api.hideOptionMenu();
                Api.shareToFriend(sc.weixinData, wxCallbacks);
                Api.shareToTimeline(sc.weixinData, wxCallbacks);
                Api.shareToWeibo(sc.weixinData, wxCallbacks);
            });

            bgMusic = document.getElementById("song");

            window.onorientationchange = function () {
                if (window.orientation == 90 || window.orientation == -90) {
                    $('.horizontal').fadeIn();
                }
                else {
                    $('.horizontal').fadeOut();
                }
            };

            $('body').on('touchmove', function (e) {
                e.preventDefault();
            });
        },
        createPano: function (id, config) {
            var pano = new pano2vrPlayer(id);
            var skin = new pano2vrSkin(pano);
            pano.readConfigUrl(config);
            var gyro = new pano2vrGyro(pano, id);
            return pano;
        },
        addOnTo: function (target) {
            $(target).addClass('on');
        },
        openModal: function (i, offMusic) {
            $('.mymodal').eq(i).addClass('on');
            if (offMusic && !$('.music-btn').is('.on')) {
                this.toggleMusic(0);
                isSoundNeedResume = true;
                lockSound = true;
            }
        },
        openShare: function (i, offMusic) {
            $('.share-info').eq(i).addClass('on');
        },
        toggleMusic: function (action) {
            if (lockSound) {
                return;
            }
            if (action === 1) {
                bgMusic.play();
                $('.music-btn').removeClass('on');
            }
            else if (action === 0) {
                bgMusic.pause();
                $('.music-btn').addClass('on');
            }
            else if (bgMusic.paused) {
                bgMusic.play();
                $('.music-btn').removeClass('on');
            }
            else {
                bgMusic.pause();
                $('.music-btn').addClass('on');
            }
        },
        resumeSound: function () {
            lockSound = false;
            if (isSoundNeedResume) {
                this.toggleMusic(1);
                isSoundNeedResume = false;
            }
            var myVideo = document.getElementsByTagName('video')[0];
            myVideo.pause();

        },
        loadVideoPlayer: function (vid, cid) {
            var player = new YKU.Player(cid, {
                styleid: '0',
                client_id: 'c96b2678f7fa4ade',
                vid: vid
            });
            return player;
        }
    };
})();
