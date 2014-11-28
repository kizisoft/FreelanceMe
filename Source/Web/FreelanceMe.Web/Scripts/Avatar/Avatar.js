var HtmlCustomHelpers = HtmlCustomHelpers || {};

(function (Scope) {
    var Avatar = (function () {
        var self;

        function Avatar(controller, actionLoad, actionSave, actionDelete) {
            if (!controller || !actionLoad || !actionSave || !actionDelete) {
                throw new Error('Missing parameter(s)!');
            }

            self = this;
            this.controller = controller;
            this.actionLoad = actionLoad;
            this.actionSave = actionSave;
            this.actionDelete = actionDelete;
            this.isOnline = false;

            $('#avatar-file').hover(avatarFileHoverIn, avatarFileHoverOut);
            $('#btn-del').on('click', btnDelOnClick);
            $('#btn-save').on('click', btnSaveOnClick);
            $('#avatar-file').on('change', avatarFileOnChange);

            avatarLoad();
        }

        function avatarLoad() {
            var url = self.controller ? '/' + self.controller + '/' + self.actionLoad : self.actionLoad;
            $('#avatar-loading').removeClass('hidden');
            Scope.HttpRequester.get(url).then(function avatarLoadSuccess(data) {
                if (data !== "KO") {
                    self.isOnline = true;
                    var obj = JSON.parse(data);
                    showImage(obj.data);
                    $('#avatar').val(obj.avatar);
                }

                $('#avatar-loading').addClass('hidden');
            }, showError);
        }

        function showImage(data) {
            if (data) {
                $('#avatar-image').attr('src', data);
                $('#avatar-glyph').addClass('hidden');
                $('#avatar-image').removeClass('hidden');
            }

            if (self.isOnline) {
                $('#btn-save').addClass('hidden');
                $('#btn-add').addClass('hidden');
                $('#btn-del').removeClass('avatar-btn-left hidden');
                $('#btn-del').addClass('avatar-btn-middle');
                $('#avatar-online').removeClass('hidden');
            } else {
                $('#btn-del').removeClass('avatar-btn-middle hidden');
                $('#btn-del').addClass('avatar-btn-left');
                $('#btn-save').removeClass('hidden');
            }

            $('#avatar-loading').addClass('hidden');
        }

        function hideImage() {
            $('#avatar-image').addClass('hidden');
            $('#btn-del').addClass('hidden');
            $('#btn-save').addClass('hidden');
            $('#btn-add').removeClass('hidden');
            $('#avatar-online').addClass('hidden');
            $('#avatar-glyph').removeClass('hidden');
            $('#avatar-file').val('');
            $('#avatar-image').attr('src', '');
        }

        function showError(error) {
            alert(error);
            $('#avatar-loading').addClass('hidden');
        }

        function avatarFileHoverIn(e) {
            $('#btn-add').addClass('avatar-file-hover');
        }

        function avatarFileHoverOut(e) {
            $('#btn-add').removeClass('avatar-file-hover');
        }

        function btnDelOnClick() {
            if (self.isOnline) {
                var data = { fileName: $('#avatar').val(), token: $('[name = "__RequestVerificationToken"]').val() };
                var url = self.controller ? '/' + self.controller + '/' + self.actionDelete : self.actionDelete;
                Scope.HttpRequester.post(url, data).then(function avatarLoadSuccess(data) {
                    $('#avatar').val('');
                    self.isOnline = false;
                }, showError);
            }

            hideImage();
        }

        function btnSaveOnClick() {
            var img = $('#avatar-image').attr('src');
            var url = self.controller ? '/' + self.controller + '/' + self.actionSave : self.actionSave;
            $('#avatar-loading').removeClass('hidden');
            Scope.HttpRequester.post(url, { data: img }).then(function avatarSaveSuccess(data) {
                $('#avatar').val(data);
                self.isOnline = true;
                showImage();
            }, showError);
        }

        function avatarFileOnChange() {
            var input = $('#avatar-file')[0];
            var fReader = new FileReader();
            fReader.readAsDataURL(input.files[0]);
            fReader.onloadend = function (event) {
                showImage(event.target.result);
            }
        }

        return Avatar;
    }());

    Scope.Avatar = Avatar;
}(HtmlCustomHelpers));