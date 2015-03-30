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
                    var audio = $("<audio>").addClass("audio-song").attr("preload", "none");
                    li.append(audio);
                    var source = $("<source>").attr("src", items[i].Url);
                    audio.append(source);
                    var artist = $("<span>").addClass("span-artist")
                        .attr("artist", items[i].Artist);
                    var title = $("<span>").addClass("span-title")
                                           .attr("title", items[i].Title);
                    li.append(artist);
                    li.append(title);
                    var hr = $("<hr/>");
                    $("#search-result").append(hr);
                }
            }
        });
    });

    $(document).on("click", ".song", function () {
        console.log($(this).data("id"));
    });

});