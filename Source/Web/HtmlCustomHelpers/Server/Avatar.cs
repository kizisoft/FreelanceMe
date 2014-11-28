namespace HtmlCustomHelpers.Server
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Helpers;

    public class Avatar
    {
        public string SaveToFile(string imageData, string imagePath, string imageName)
        {
            var indexOfBase64 = imageData.IndexOf(",");
            var base64 = imageData.Substring(indexOfBase64 + 1);
            var fileExtantionIndex = imageData.IndexOf("/") + 1;
            var fileExtantionLength = imageData.IndexOf(";") - fileExtantionIndex;
            var fileExtantion = imageData.Substring(fileExtantionIndex, fileExtantionLength);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                using (FileStream fs = new FileStream(imagePath + "\\" + imageName + "." + fileExtantion, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }

            return imageName + "." + fileExtantion;
        }

        public string LoadFromFile(string imagePath, string imageName)
        {
            var imageFullName = imagePath + "\\" + imageName;
            if (!File.Exists(imageFullName))
            {
                return "KO";
            }

            string data;
            using (FileStream fs = new FileStream(imageFullName, FileMode.Open))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    data = Convert.ToBase64String(ms.ToArray());
                }
            }

            return Json.Encode(new
            {
                data = "data:image/" + Path.GetExtension(imageFullName).Substring(1) + ";base64," + data,
                avatar = imageName
            });
        }

        public string DeleteFile(HttpRequestBase request, string imageFullName, string token)
        {
            AntiForgery.Validate(request.Cookies["__RequestVerificationToken"].Value, token);

            if (!File.Exists(imageFullName))
            {
                return "KO";
            }

            File.Delete(imageFullName);

            return "OK";
        }
    }
}