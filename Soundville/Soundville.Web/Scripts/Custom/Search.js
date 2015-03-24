$(function () {
    $(".submit-search").click(function () {
        $.ajax({
            url: "/StationSong/Search",
            dataType: "json",
            data: { searchString: $(".search-input").val() },
            success: function (items) {
                $("#search-result").empty();
                for (var i = 0; i < items.length; i++) {
                    var li = $("<li>").addClass("song").attr("data-id", items[i].Id).text(items[i].Value);
                    $("#search-result").append(li);
                }
            }
        });
    });

    $(document).on("click", ".song", function () {
        console.log($(this).data("id"));
    });
});