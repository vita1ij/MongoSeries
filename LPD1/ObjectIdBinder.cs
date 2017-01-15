using MongoDB.Bson;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace LPD1
{
    public class ObjectIdBinder : System.Web.Mvc.IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            return new ObjectId(result.AttemptedValue);
        }
    }
}