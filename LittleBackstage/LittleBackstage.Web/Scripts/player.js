/**
 * V3.0 HTML5鍏煎 
 * JavaScript Tools for Youku Player
 * Charset: UTF-8
 * <script type="text/javascript" src="http://project.youku.com/_global/v1.2.0907/youku/player.js"></script>
**/

var player = player || {};
(function() {
    var isHTML5 = /(iPhone|iPad|iPod|android)/i.test(navigator.userAgent);
    player.Setting = {
        vid: '',
        div: 'youku_player',
        width: 400,
        height: 314,
        autoPlay: 'false',
        imgLogo: '',
        loadType: 'normal',
        channel: '101',
        partnerId: 'XMjM2NA==',
        player: 'http://static.youku.com/v/swf/qplayer.swf'
    };
    player.load = function(opt, skinVars) {
        opt = opt || {};
        this.Setting = this._extend(this.Setting, opt);
        var rnd = String(Math.random()).substr(2);
        var flashPlayerDiv = "youkuPlayerLoad_" + rnd;
        var flashPlayerId = "youkuPlayer_" + rnd;
        document.getElementById(this.Setting.div).innerHTML = '<div id="' + flashPlayerDiv + '"></div>';
        if (isHTML5) {
            var str = "<video id=\"" + flashPlayerId + "\" width=\"" + this.Setting.width +"\" src=\"http://v.youku.com/player/getM3U8/vid/" + this.Setting.vid + "/type/mp4/v.m3u8\" controls=\"controls\" autoplay=\"" + this.Setting.autoPlay + "\" poster=\"" + this.Setting.imgLogo + "\"></video>";
            document.getElementById(flashPlayerDiv).innerHTML = str;
            return flashPlayerId;
        }
        var flashParams = {
            "quality": "high",
            "allowScriptAccess": "always",
            "allowFullScreen": "true",
            "bgcolor": "#000000",
            "wmode": "transparent"
        };
        var flashAttributes = {
            "id": flashPlayerId,
            "name": flashPlayerId
        };
        var flashVars = {};
        switch (this.Setting.loadType) {
        case "normal":
            flashVars = {
                //"winType": "index",
                "winType": "interior",
                "isShowRelatedVideo": "false",
                "isAutoPlay": this.Setting.autoPlay,
                "VideoIDS": this.Setting.vid
            };
            this.Setting.player = 'http://static.youku.com/v/swf/qplayer.swf';
            break;
        case 'full':
            flashVars = {
                "winType": "interior",
                "isShowRelatedVideo": "true",
                "isAutoPlay": this.Setting.autoPlay,
                "VideoIDS": this.Setting.vid
            };
            this.Setting.player = 'http://static.youku.com/v/swf/qplayer.swf';
            break;
        case 'api':
            flashVars = (typeof(skinVars) == "object" ? skinVars: {});
            flashVars.playMovie = "true";
            flashVars.isShowRelatedVideo = "false";
            flashVars.isAutoPlay = this.Setting.autoPlay;
            flashVars.isLoop = "false";
            this.Setting.player = 'http://player.youku.com/player.php/sid/' + this.Setting.vid + '/partnerid/' + this.Setting.partnerId + '/v.swf';
            break;
        case 'live':
            flashVars = {
                "reconnects": 3,
                "channel": this.Setting.channel,
                "autoplay": 1,
                "showfrontad": 0,
                "dragback": 0
            };
            this.Setting.player = 'http://static.youku.com/v/swf/livePlayer.swf';
            break;
        default:
            flashVars = {
                "winType": "index",
                "isShowRelatedVideo": "false",
                "isAutoPlay": this.Setting.autoPlay,
                "VideoIDS": this.Setting.vid
            };
            this.Setting.player = 'http://static.youku.com/v/swf/qplayer.swf';
            break
        }
        if (this.Setting.imgLogo != "") {
            flashVars.imglogo = this.Setting.imgLogo
        }
        swfobject.embedSWF(this.Setting.player, flashPlayerDiv, this.Setting.width, this.Setting.height, "9.0.0", null, flashVars, flashParams, flashAttributes);
        return flashPlayerId;
    }
    player._extend = function(a, b) {
        var c = {};
        for (var m in a) {
            c[m] = a[m]
        }
        for (var m in b) {
            c[m] = b[m]
        }
        return c
    }
})();