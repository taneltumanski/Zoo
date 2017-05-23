using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.Helpers
{
	// https://stackoverflow.com/a/25809351
	public static class PathHelper
    {
        public static string FullyQualifiedApplicationPath(HttpRequestBase httpRequestBase)
        {
            string appPath = string.Empty;

            if (httpRequestBase != null)
            {
                //Formatting the fully qualified website url/name
                appPath = string.Format("{0}://{1}{2}{3}",
                            httpRequestBase.Url.Scheme,
                            httpRequestBase.Url.Host,
                            httpRequestBase.Url.Port == 80 ? string.Empty : ":" + httpRequestBase.Url.Port,
                            httpRequestBase.ApplicationPath);
            }

            if (!appPath.EndsWith("/"))
            {
                appPath += "/";
            }

            return appPath;
        }
    }
}