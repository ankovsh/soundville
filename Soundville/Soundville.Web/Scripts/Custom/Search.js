$(function () {
    $(".submit-search").click(function () {
        $.ajax({
            url: "/StationSong/SearchAsync",
            dataType: "json",
            data: { searchString: $(".search-input").val() },
            success: function (data) {
                $("#search-result").empty();

                var items = data.Items;
                for (var i = 0; i < items.length; i++) {
                    var li = $("<li>").addClass("song");
                    $("#search-result").append(li);
                    var a = $("<a>").addClass("song-link")
                                    .attr("data-id", items[i].Id)
                                    .text(items[i].Artist + " - " + items[i].Title);
                    li.append(a);
                    var audio = $("<audio>").attr("src", items[i].Url);
                    li.append(audio);
                    var hr = $("<hr/>");
                    $("#search-result").append(hr);
                }
            }
        });
    });

    $(document).on("click", ".song", function () {
        console.log($(this).data("id"));
    });

    $(document).on("click", ".song-link", function () {
        var wavesurfer = Object.create(WaveSurfer);

        wavesurfer.init({
            container: document.querySelector('#wave'),
            waveColor: 'violet',
            progressColor: 'purple'
        });

        wavesurfer.on('ready', function() {
            wavesurfer.play();
        });

        var songUrl = $(this).next().attr("src");

        wavesurfer.load(songUrl);
    });
});