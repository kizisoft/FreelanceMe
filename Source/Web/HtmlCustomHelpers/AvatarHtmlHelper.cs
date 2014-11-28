namespace HtmlCustomHelpers
{
    using System.Web.Mvc;

    using HtmlCustomHelpers.Client;

    public static class AvatarHtmlHelper
    {
        public static AvatarBox Avatar(this HtmlHelper html, string controller, string actionLoad, string actionSave, string actionDelete)
        {
            return new AvatarBox(controller, actionLoad, actionSave, actionDelete);
        }
    }
}