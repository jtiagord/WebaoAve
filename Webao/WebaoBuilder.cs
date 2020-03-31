using System;
using System.Reflection;
using Webao.Attributes;

namespace Webao
{
    public class WebaoBuilder
    {
        public static AbstractAccessObject Build(Type webao, IRequest req) {
            AddParameterAttribute[] parameters = (AddParameterAttribute[]) webao.GetCustomAttributes(typeof(AddParameterAttribute), true);
            foreach(AddParameterAttribute param in parameters)
            {
                req.AddParameter(param.name, param.val);
            }

            BaseUrlAttribute baseURL = (BaseUrlAttribute) webao.GetCustomAttribute(typeof(BaseUrlAttribute), true);
            req.BaseUrl(baseURL.host);


            return (AbstractAccessObject) Activator.CreateInstance(webao, req);
        }
    }
}
