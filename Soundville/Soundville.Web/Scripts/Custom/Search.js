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
                    var li = $("<li>").addClass("song").attr("data-id", items[i].Id).text(items[i].Artist + " - " + items[i].Title);
                    $("#search-result").append(li);
                }
            }
        });
    });

    $(document).on("click", ".song", function () {
        console.log($(this).data("id"));
    });
});