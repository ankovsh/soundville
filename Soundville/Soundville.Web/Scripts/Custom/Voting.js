$(function () {
    function vote(songId, value) {
        $.ajax({
            url: "/StationSong/Vote",
            method: "POST",
            data: {
                stationSongId: songId,
                value: value
            },
            success: function (result) {
                if (result.hasErrors) {
                    console.log(result.errorMessage);
                } else {
                    console.log("VOTED!!!");
                }
            }
        });
    }

    $(".up-vote").click(function() {
        var songId = $(this).data("songId");
        vote(songId, 1);
    });

    $(".down-vote").click(function () {
        var songId = $(this).data("songId");
        vote(songId, -1);
    });
});