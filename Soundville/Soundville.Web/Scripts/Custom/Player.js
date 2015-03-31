var styleChange = { pause: {}, play: {}, broadcasting: {}, stopBroadcasting: {} };

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

styleChange.broadcasting.change = function () {
    $('#brcasting > div').css({
        'box-shadow': 'inset 0 4px 2px #414243, 0 0 10px #5466da',
        'border': '5px solid #5466da'
    });
    $('#broadcasting').css('background-image', 'url(../../Content/Images/playHo.png)');
};

styleChange.broadcasting.recovery = function () {
    $('#brcasting > div').removeAttr('style');
    $('#broadcasting').removeAttr('style');
};

styleChange.stopBroadcasting.change = function () {
    $('#sbrcasting > div').css({
        'box-shadow': 'inset 0 4px 2px #414243, 0 0 10px #5466da',
        'border': '5px solid #5466da'
    });
    $('#stop-broadcasting').css('background-image', 'url(../../Content/Images/stopHo.png)');
};

styleChange.stopBroadcasting.recovery = function () {
    $('#sbrcasting > div').removeAttr('style');
    $('#stop-broadcasting').removeAttr('style');
};

$(document).ready(function () {
    var dur, durM, val, mus, prog;

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

        $(".playing-song").text(artist + " - " + title);
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

    $('.playlist-song').click(function () {
        $('#error').text('');
        styleChange.play.change();
        styleChange.pause.recovery();
        if (mus) {
            mus[0].pause();
            mus[0].currentTime = 0;
        }
        mus = $(this).next("audio");
        //mus[0].play();

        var wavesurfer = Object.create(WaveSurfer);

        wavesurfer.init({
            container: document.querySelector('#wave'),
            waveColor: 'violet',
            progressColor: 'purple'
        });

        wavesurfer.on('ready', function () {
            wavesurfer.play();
        });

        wavesurfer.load($(this).next().children().attr("src"));
    });

    //Кнопка воспроизведения
    $('#pl').click(function () {
        if (mus) {
            mus[0].play();
            styleChange.play.change();
            styleChange.pause.recovery();
        }
        else {
            $('#error').text('Сначала начните воспроизведение!');
        }
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

    $('#brcasting').click(function() {
        styleChange.broadcasting.change();
        styleChange.stopBroadcasting.recovery();
    });

    $('#sbrcasting').click(function () {
        styleChange.broadcasting.recovery();
        styleChange.stopBroadcasting.change();
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