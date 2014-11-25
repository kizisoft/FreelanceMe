(function () {
    $('#avatar-file').hover(
        function (e) {
            $('#btn-add').addClass('avatar-file-hover');
        },
        function (e) {
            $('#btn-add').removeClass('avatar-file-hover')
        });

    $('#btn-del').on('click', function () {
        if (!$('#avatar-online').hasClass('hidden')) {
            var data = JSON.stringify({ name: '@this.User.Identity.Name', token: $('[name = "__RequestVerificationToken"]').val() });
            $.ajax({
                url: 'AvatarDeleteAjax',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                data: data,
                success: function (response) {
                    alert('success');
                },
                error: function (er) {
                    alert('error');
                }
            });
        }

        $('#avatar-image').addClass('hidden');
        $('#btn-del').addClass('hidden');
        $('#btn-save').addClass('hidden');
        $('#avatar-online').addClass('hidden');
        $('#avatar-glyph').removeClass('hidden');
        $('#avatar-file').val('');
        $('#avatar-image').attr('src', '');
    });

    $('#btn-save').on('click', function () {
        if ($('#btn-save').hasClass('btn-save-disabled')) {
            return;
        }

        $('#avatar-loading').removeClass('hidden');

        var img = $('#avatar-image').attr('src');
        var data = JSON.stringify({ data: img });
        $.ajax({
            url: 'AvatarPostAjax',
            type: "POST",
            dataType: 'json',
            contentType: 'application/json',
            data: data,
            success: function (response) {
                $('#avatar-loading').addClass('hidden');
                $('#btn-save').addClass('btn-save-disabled');
                $('#btn-save').removeClass('btn-save');
                $('#avatar-online').removeClass('hidden');
            },
            error: function (er) {
                $('#avatar-loading').removeClass('hidden');
                alert('error');
            }
        });
    });

    $('#avatar-file').on('change', function () {
        var input = $('#avatar-file')[0];
        var fReader = new FileReader();
        fReader.readAsDataURL(input.files[0]);
        fReader.onloadend = function (event) {
            $('#avatar-image').attr('src', event.target.result);
            $('#avatar-glyph').addClass('hidden');
            $('#avatar-image').removeClass('hidden');
            $('#btn-del').removeClass('hidden');
            var $btnSave = $('#btn-save');
            $btnSave.removeClass('hidden');

            if ($btnSave.hasClass('btn-save-disabled')) {
                $btnSave.addClass('btn-save');
                $btnSave.removeClass('btn-save-disabled');
            }
        }
    });
}());