var styleChange = { pause: {}, play: {}, repickTrack: {}, retickPlayList: {} };

styleChange.play.change = function () {
    $('#pl > div').css({
        'box-shadow': 'inset 0 4px 2px #414243, 0 0 10px #5466da',
        'border': '5px solid #5466da'
    });
    $('#play').css('background-image', 'url(../../Content/Images/playHo.png)');
};

styleChange.play.recovery = function () {
    $('#pl > div').removeAttr('style');
    $('#play').removeAttr('style');
};

styleChange.pause.change = function () {
    $('#pa > div').css({
        'box-shadow': 'inset 0 4px 2px #414243, 0 0 10px #5466da',
        'border': '5px solid #5466da'
    });
    $('#pause').css('background-image', 'url(../../Content/Images/pauseHo.png)');
};

styleChange.pause.recovery = function () {
    $('#pa > div').removeAttr('style');
    $('#pause').removeAttr('style');
};

styleChange.repickTrack.change = function () {
    $('#repick > div').css({
        'box-shadow': 'inset 0 4px 2px #414243, 0 0 10px #5466da',
        'border': '5px solid #5466da'
    });
    $('#repickTrack').css('background-image', 'url(../../Content/Images/repTrackHo.png)');
};

styleChange.repickTrack.recovery = function () {
    $('#repick > div').removeAttr('style');
    $('#repickTrack').removeAttr('style');
};

styleChange.retickPlayList.change = function () {
    $('#repickPl > div').css({
        'box-shadow': 'inset 0 4px 2px #414243, 0 0 10px #5466da',
        'border': '5px solid #5466da'
    });
    $('#repickPlay').css('background-image', 'url(../../Content/Images/repPlHo.png)');
};

styleChange.retickPlayList.recovery = function () {
    $('#repickPl > div').removeAttr('style');
    $('#repickPlay').removeAttr('style');
};

$(document).ready(function () {
    var dur, durM, val, mus, elem, prog;
    var repick = 0;
    var repickPl = 0;

    $(document).on("click", ".song-link", function () {
        $('#error').text('');
        styleChange.play.change();
        styleChange.pause.recovery();
        if (mus) {
            mus[0].pause();
            mus[0].currentTime = 0;
        }
        mus = $(this).next("audio");
        mus[0].play();

        var artist = $(this).next().next().attr("artist");
        var title = $(this).next().next().next().attr("title");

        var playingSong = $(".playing-song").text(artist + " - " + title);
    });

    $(document).on("timeupdate", ".audio-song", function () {
        d = this.duration;
        c = this.currentTime;
        curM = Math.floor(c / 60);
        curS = Math.round(c - curM * 60);
        $('#current').text(curM + ':' + curS);
        $('#progress').slider({
            max: d,
            min: 0,
            value: c
        });
    });

    $(document).on("playing", ".audio-song", function () {
        dur = this.duration;
        durM = Math.floor(dur / 60) + ':' + Math.round((dur - Math.floor(dur / 60)) / 10);
        $('#duration').text(durM);
    });

    //информация о треке
    $('#progress').slider({
        value: 0,
        orientation: "horizontal",
        range: "min",
        animate: true,
        step: 1
    });

    $('audio').on("timeupdate", function () {
        d = this.duration;
        c = this.currentTime;
        curM = Math.floor(c / 60);
        curS = Math.round(c - curM * 60);
        $('#current').text(curM + ':' + curS);
        $('#progress').slider({
            max: d,
            min: 0,
            value: c
        });
    });

    $('audio').on("playing", function () {
        dur = this.duration;
        durM = Math.floor(dur / 60) + ':' + Math.round((dur - Math.floor(dur / 60)) / 10);
        $('#duration').text(durM);
    });

    $('audio').on("ended", function () {
        mus = $(this).parent('li').next('li').first();
        mus = mus.children('audio');
        if (mus[0]) {
            mus[0].play();
        }
        else {
            if (repickPl == 1) {
                mus = $('audio:first');
                mus[0].play();
            }
            else {
                $('#error').text('Конец списка воспроизведения!');
                styleChange.play.recovery();
            }
        }
    });

    //Кнопка воспроизведения
    $('#pl').click(function () {
        if (mus) {
            mus[0].play();
        }
        else {
            mus = $('audio:first');
            mus[0].play();
        }
        styleChange.play.change();
        styleChange.pause.recovery();
        $('#error').text('');
    });

    // Кнопка паузы
    $('#pa').click(function () {

        if (mus) {
            mus[0].pause();
            styleChange.play.recovery();
            styleChange.pause.change();
        }
        else {
            $('#error').text('Сначала начните воспроизведение!');
        }

    });

    //Кнопка повтора трека
    $('#repick').click(function () {
        if (repick == 0) {
            styleChange.repickTrack.change();
            repick = 1;
            mus[0].loop = true;
        }
        else {
            styleChange.repickTrack.recovery();
            repick = 0;
            mus[0].loop = false;
        }
    });

    //Кнопка следующий трек
    $('#nexTreck').click(function () {
        mus[0].pause();
        mus[0].currentTime = 0;
        mus = mus.parent('li').next('li').first();
        mus = mus.children('audio');
        if (mus[0]) {
            mus[0].play();
        }
        else {
            if (repickPl == 1) {
                mus = $('audio:first');
                mus[0].play();
            }
            else {
                $('#error').text('Конец списка воспроизведения! Выберите трек!');
                styleChange.play.recovery();
                mus = null;
            }
        }
    });

    //Кнопка предыдущий трек
    $('#preTreck').click(function () {
        mus[0].pause();
        mus[0].currentTime = 0;
        mus = mus.parent('li').prev('li').last();
        mus = mus.children('audio');
        if (mus[0]) {
            mus[0].play();
        }
        else {
            if (repickPl == 1) {
                mus = $('audio:last');
                mus[0].play();
            }
            else {
                $('#error').text('Начало списка воспроизведения! Выберите трек!');
                styleChange.play.recovery();
                mus = null;
            }
        }
    });

    //Кнопка повтора плейлиста
    $('#repickPl').click(function () {
        if (repickPl == 0) {
            styleChange.retickPlayList.change();
            repickPl = 1;
        }
        else {
            styleChange.retickPlayList.recovery();
            repickPl = 0;
        }
    });

    //Настройка ползунка громкости
    $('#rangeVal').slider({
        value: 60,
        orientation: "horizontal",
        range: "min",
        animate: true,
        step: 1
    });

    // регулировка громкости и отображение уровня громкости
    val = $('#rangeVal').slider("value");
    $('#val').text(val);

    $('#rangeVal').slider({
        slide: function (event, ui) {
            val = ui.value;
            $('#val').text(val);

            if (mus) {
                mus[0].volume = val / 100;
            }
            else {
                $('#error').text('Регулировка громкости доступна только после начала воспроизведения!');
            }
        }
    });

    // Перемотка
    $('#progress').slider({
        start: function (event, ui) {
            mus[0].pause();
        },
        stop: function (event, ui) {
            prog = ui.value;
            mus[0].currentTime = prog;
            mus[0].play();
        }
    });
});