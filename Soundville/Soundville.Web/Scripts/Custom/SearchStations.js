$(function () {
    $(".submit-search").click(function () {
        $.ajax({
            url: "/Station/Search",
            dataType: "json",
            data: { searchString: $(".search-input").val() },
            success: function (data) {
                $("#station-container").empty();

                var items = data.Items;
                if (!items) {
                    var notFound = $("<span>").addClass("station-name")
                        .text("Station not found.");
                }
            }
        });
    });
});