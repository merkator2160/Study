using System;
using System.Web.Mvc;
using Castle.ActiveRecord;

namespace Mvc3App.Binder
{
    public class ActiveRecordModelBinder<T> : DefaultModelBinder
        where T : ActiveRecordBase<T>
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue("id");
            int id;
            if (value != null && Int32.TryParse(value.AttemptedValue, out id))
            {
                return ActiveRecordBase<T>.Find(id);                
            }
            return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}