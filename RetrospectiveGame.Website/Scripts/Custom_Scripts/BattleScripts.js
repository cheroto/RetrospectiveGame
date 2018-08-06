$(document).ready(function () {
    console.log("ready");
    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            method: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-rg-target"));
            $target.replaceWith(data);
            $("#startButton").val("Next Round");
            console.log(data);
        });
        return false;
    };

    $("form[data-rg-ajax='true']").submit(ajaxFormSubmit);
});

