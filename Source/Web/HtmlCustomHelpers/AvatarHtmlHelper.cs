namespace HtmlCustomHelpers
{
    using System.Web.Mvc;

    public static class AvatarHtmlHelper
    {
        public static AvatarBox Avatar(this HtmlHelper html, string text)
        {
            return new AvatarBox(text);
        }
    }
}