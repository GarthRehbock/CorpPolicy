
$(function () {
    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function(data) {
            var $target = $($form.attr("data-corp-target"));
            $target.replaceWith(data);
        });

        return false;
    };

    var createAutoComplete = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-corp-autocomplete")
        };

        $input.autocomplete(options);
    };

    var getPage = function () {
        var $a = $(this);
        var options = {
            url: $a.attr("href"),
            type: "get",
            data: $("form").serialize()
        };

        $.ajax(options).done(function(data) {
            var $target = $($a.parents("div.pagedList").attr("data-corp-target"));
            $target.replaceWith(data);
        });

        return false;
    };
    
    var getSortPage = function () {
        var $a = $(this);
        var options = {
            url: $a.attr("href"),
            type: "get",
            data: $("form").serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($a.attr("data-corp-target"));
            $target.replaceWith(data);
        });

        return false;
    };

    $("form[data-corp-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-corp-autocomplete]").each(createAutoComplete);
    $(".body-content").on("click", ".pagedList a[href]", getPage);
    $(".body-content").on("click", "a[data-corp-sortlink='true']", getSortPage);
});

