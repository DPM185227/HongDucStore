using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace HongDucStore.Library
{
    public static class Library
    {
        public static string UploadFile(string folderName, HttpPostedFileBase postedFile)
        {
            string _saveDBPath = "";
            try
            {
                if (postedFile.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(postedFile.FileName);
                    string _path = Path.Combine(HttpContext.Current.Server.MapPath("~/Storage/"+folderName+""), _FileName);
                    _saveDBPath = "Storage\\"+folderName+"\\"+_FileName;
                    postedFile.SaveAs(_path);
                }
            }
            catch(Exception e) {
                e.ToString();
            }
            return _saveDBPath;
        }

        public static void DeleteFile(string path)
        {
            string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\" + path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public static string ComputeMd5Hash(string message)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.ASCII.GetBytes(message);
                byte[] hash = md5.ComputeHash(input);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}