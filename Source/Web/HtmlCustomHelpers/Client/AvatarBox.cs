namespace HtmlCustomHelpers.Client
{
    using System.Web;
    using System.Web.Mvc;

    public class AvatarBox : IHtmlString
    {
        private readonly string controller;
        private readonly string actionSave;
        private readonly string actionDelete;
        private readonly string actionLoad;

        public AvatarBox(string controller, string actionLoad, string actionSave, string actionDelete)
        {
            this.controller = controller;
            this.actionLoad = actionLoad;
            this.actionSave = actionSave;
            this.actionDelete = actionDelete;
        }

        public string ToHtmlString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            return this.RenderAvatar();
        }

        private string RenderAvatar()
        {
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("avatar-container text-center");

            var glyph = new TagBuilder("span");
            glyph.AddCssClass("avatar-image avatar-glyph glyphicon glyphicon-user");
            glyph.Attributes.Add("id", "avatar-glyph");
            wrapper.InnerHtml += glyph;

            var img = new TagBuilder("img");
            img.AddCssClass("avatar-image hidden");
            img.Attributes.Add("id", "avatar-image");
            img.Attributes.Add("alt", "Avatar");
            wrapper.InnerHtml += img;

            var loading = new TagBuilder("div");
            loading.AddCssClass("avatar-loading hidden");
            loading.Attributes.Add("id", "avatar-loading");
            wrapper.InnerHtml += loading;

            var online = new TagBuilder("span");
            online.AddCssClass("avatar-online hidden");
            online.Attributes.Add("id", "avatar-online");
            online.SetInnerText("Online");
            wrapper.InnerHtml += online;

            var menu = new TagBuilder("div");
            menu.AddCssClass("avatar-menu");

            var btnDel = new TagBuilder("span");
            btnDel.AddCssClass("btn-btn btn-del avatar-btn avatar-btn-left glyphicon glyphicon-trash hidden");
            btnDel.Attributes.Add("id", "btn-del");
            btnDel.Attributes.Add("title", "Delete Profile Picture");
            menu.InnerHtml += btnDel;

            var btnAdd = new TagBuilder("span");
            btnAdd.AddCssClass("btn-btn btn-add avatar-btn avatar-btn-middle glyphicon glyphicon-plus");
            btnAdd.Attributes.Add("id", "btn-add");
            btnAdd.Attributes.Add("title", "Add Profile Picture");

            var inputFile = new TagBuilder("input");
            inputFile.AddCssClass("avatar-file");
            inputFile.Attributes.Add("id", "avatar-file");
            inputFile.Attributes.Add("type", "file");
            btnAdd.InnerHtml += inputFile;
            menu.InnerHtml += btnAdd;

            var btnSave = new TagBuilder("span");
            btnSave.AddCssClass("btn-btn btn-save avatar-btn avatar-btn-right glyphicon glyphicon-floppy-save hidden");
            btnSave.Attributes.Add("id", "btn-save");
            btnSave.Attributes.Add("title", "Save Profile Picture");
            menu.InnerHtml += btnSave;

            var avatarFileName = new TagBuilder("input");
            avatarFileName.AddCssClass("hidden");
            avatarFileName.Attributes.Add("id", "avatar");
            avatarFileName.Attributes.Add("type", "hidden");
            avatarFileName.Attributes.Add("name", "avatar");
            wrapper.InnerHtml += avatarFileName;

            var script = new TagBuilder("script");
            script.InnerHtml = "new HtmlCustomHelpers.Avatar('" + this.controller + "', '" + this.actionLoad + "', '" + this.actionSave + "', '" + this.actionDelete + "');";

            wrapper.InnerHtml += menu;
            wrapper.InnerHtml += script;

            return wrapper.ToString();
        }
    }
}