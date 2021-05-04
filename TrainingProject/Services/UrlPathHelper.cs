using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingProject.Services
{
    public static class UrlPathHelper
    {
        public static string PathToUrl(string path)
        {
            string[] pathArray = path.Split('\\');

            string Url = string.Empty;

            if (pathArray.Length == 2 && pathArray[1] == null)
            {
                Url += pathArray[0][0];
                return Url;
            }

            for (int i = 0; i < pathArray.Length; i++)
            {
                if (pathArray[i].Contains(":"))
                {
                    Url += pathArray[i][0];
                }
                else
                {
                    Url += "/";
                    Url += pathArray[i];
                }
            }

            return Url;
        }

        public static string UrlToPath(string url)
        {
            string[] urlArray = url.Split('/');

            string path = string.Empty;
            if (url.Length == 1)
            {
                path += url;
                path += ":\\";
                return path;
            }

            for (int i = 0; i < urlArray.Length; i++)
            {
                if (i == 0)
                {
                    path += urlArray[i];
                    path += ":\\";
                }
                else
                {
                    path += urlArray[i];
                    if (i != urlArray.Length - 1)
                    {
                        path += '\\';
                    }
                }
            }

            return path;
        }
    }
}