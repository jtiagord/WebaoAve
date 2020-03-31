using System;
using System.Diagnostics;
using System.Reflection;
using Webao.Attributes;
namespace Webao
{
    public abstract class AbstractAccessObject
    {
        private readonly IRequest req;
        
        protected AbstractAccessObject(IRequest req)
        {
            this.req = req;
        }

        public object Request(params object[] args) {
            StackTrace stackTrace = new StackTrace();
            MethodInfo callSite = (MethodInfo) stackTrace.GetFrame(1).GetMethod();
            /*
             * The callsite is the caller method.
            */
           
            //Get the path
            GetAttribute get = (GetAttribute) callSite.GetCustomAttribute(typeof(GetAttribute));
            
            String path = get.path;
            
            //Replace the variables in the string with their values
            int i = 0;
            foreach (ParameterInfo param in callSite.GetParameters())
            {
                path = path.Replace("{" + param.Name + "}", args[i++].ToString());
            }
            
            //Get The Map Attribute
            MappingAttribute map = (MappingAttribute) callSite.GetCustomAttribute(typeof(MappingAttribute));

            //Get the object path
            String[] paths = map.path.Split('.');

            //http request
            object objToReturn = req.Get(path, map.dest);

            //Return the desired object based on path
            for (int j = 1; j < paths.Length; j++)
            {
                PropertyInfo pi = objToReturn.GetType().GetProperty(paths[j]);

                objToReturn = pi.GetValue(objToReturn);
            }
            
            return objToReturn; 
            
        }
    }
}
