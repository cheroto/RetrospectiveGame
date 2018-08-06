$(document).ready(function () {
    console.log("ready");

    $("#startBattle").click(function () {
        console.log("startBattle was clicked.")
        UpdatePage("/Home/About");
    });
});

function UpdatePage(ajaxPostUrl) {
    $.ajax({
        type: "POST",
        url: ajaxPostUrl,
        success: function () {
            location.
        }
    });
}
