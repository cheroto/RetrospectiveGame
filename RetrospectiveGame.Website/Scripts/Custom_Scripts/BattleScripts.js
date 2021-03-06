﻿$(document).ready(function () {
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
            UpdateProgressBarColor();
            console.log(data);
        });
        return false;


        function UpdateProgressBarColor() {
            $(".progress-bar").each(function () {
                if ($(this).attr("aria-valuenow") < 50) {
                    $(this).removeClass("progress-bar-success");
                    $(this).addClass("progress-bar-warning");
                }
                if ($(this).attr("aria-valuenow") < 30) {
                    $(this).removeClass("progress-bar-success");
                    $(this).removeClass("progress-bar-warning");
                    $(this).addClass("progress-bar-danger");
                }
            })
        }
    };

    async function writeSlowly() {
        var testString = "This is a test.";
        for (i = 0; i < testString.length; i++) {
                $("#testDiv").append(testString.charAt(i));
                await sleep(50);
        }
    }

    function sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }


    $("form[data-rg-ajax='true']").submit(ajaxFormSubmit);
    $("#testButton").click(writeSlowly);
});

