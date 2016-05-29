var controller = "memory";

$(document).ready(function () {

    //$("#Tags").change(function () {
    //    $("#log").ajaxError(function (event, jqxhr, settings, exception) {
    //        alert(exception);
    //    });

    //    var tagIdSelected = $("select option:selected").first().val();
    //    $.get(controller + "/MemoriesByTag",
    //        { id: tagIdSelected }, function (data) {
    //            $("#target").html(data);
    //        });
    //});

    //$("#txtTag").autocomplete({
    //    source: function (request, response) {
    //        $.ajax({
    //            url: controller + "/TagsByName",
    //            data: "{ 'prefix': '" + request.term + "'}",
    //            dataType: "json",
    //            type: "POST",
    //            contentType: "application/json; charset=utf-8",
    //            success: function (data) {
    //                response($.map(data.d, function(item) {
    //                    return {
    //                        label: item
    //                    }
    //                }));
    //            },
    //            error: function (xmlHttpRequest, textStatus, errorThrown) {
    //                alert(textStatus);
    //            }
    //        });
    //    }
    //});

    //$(function () {
    //    $("#txtTag").autocomplete({ source: controller + "/TagsByName" });
    //});

    //    $("#txtTag").autocomplete({
    //        source: controller + "/TagsByName" },
    //        multiselect: true
    //});

    $(function () {
        var availableTags = [
            "ActionScript",
            "AppleScript",
            "Asp",
            "BASIC",
            "C",
            "C++",
            "Clojure",
            "COBOL",
            "ColdFusion",
            "Erlang",
            "Fortran",
            "Groovy",
            "Haskell",
            "Java",
            "JavaScript",
            "Lisp",
            "Perl",
            "PHP",
            "Python",
            "Ruby",
            "Scala",
            "Scheme"
        ];
        $("#txtTag").autocomplete({
            source: controller + "/TagsByName",
            multiselect: true,
            success: function (data) {
                //response($.map(data.d, function(item) {
                //    return {
                //        label: item
                //    }
                //}));

                alert($("#txtTag").val());
            }
        });
    });

    $("#txtTag").change(function () {
        $("#log").ajaxError(function (event, jqxhr, settings, exception) {
            alert(exception);
        });

        alert($("#txtTag").val());

        //var tagIdSelected = $("#txtTag");
        ////var tagIdSelected = $("select option:selected").first().val();
        //$.get(controller + "/MemoriesByTag",
        //    { id: tagIdSelected }, function (data) {
        //        $("#target").html(data);
        //    });
    });
});


