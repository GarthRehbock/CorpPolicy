
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

$(function () {
    function initToolbarBootstrapBindings() {
        var fonts = ['Serif', 'Sans', 'Arial', 'Arial Black', 'Courier',
              'Courier New', 'Comic Sans MS', 'Helvetica', 'Impact', 'Lucida Grande', 'Lucida Sans', 'Tahoma', 'Times',
              'Times New Roman', 'Verdana'],
              fontTarget = $('[title=Font]').siblings('.dropdown-menu');
        $.each(fonts, function (idx, fontName) {
            fontTarget.append($('<li><a data-edit="fontName ' + fontName + '" style="font-family:\'' + fontName + '\'">' + fontName + '</a></li>'));
        });
        $('a[title]').tooltip({ container: 'body' });
        $('.dropdown-menu input').click(function () { return false; })
		    .change(function () { $(this).parent('.dropdown-menu').siblings('.dropdown-toggle').dropdown('toggle'); })
        .keydown('esc', function () { this.value = ''; $(this).change(); });

        $('[data-role=magic-overlay]').each(function () {
            var overlay = $(this), target = $(overlay.data('target'));
            overlay.css('opacity', 0).css('position', 'absolute').offset(target.offset()).width(target.outerWidth()).height(target.outerHeight());
        });
        if ("onwebkitspeechchange" in document.createElement("input")) {
            var editorOffset = $('#editor').offset();
            $('#voiceBtn').css('position', 'absolute').offset({ top: editorOffset.top, left: editorOffset.left + $('#editor').innerWidth() - 35 });
        } else {
            $('#voiceBtn').hide();
        }
    };
    function showErrorAlert(reason, detail) {
        var msg = '';
        if (reason === 'unsupported-file-type') { msg = "Unsupported format " + detail; }
        else {
            console.log("error uploading file", reason, detail);
        }
        $('<div class="alert"> <button type="button" class="close" data-dismiss="alert">&times;</button>' +
		 '<strong>File upload error</strong> ' + msg + ' </div>').prependTo('#alerts');
    };
    initToolbarBootstrapBindings();
    $('#editor').wysiwyg({ fileUploadError: showErrorAlert });
    window.prettyPrint && prettyPrint();
});

