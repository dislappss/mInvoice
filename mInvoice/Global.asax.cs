using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mInvoice
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            //string culture = null;
            //if (context.Request.UserLanguages != null && Request.UserLanguages.Length > 0)
            //{
            // culture = Request.UserLanguages[0];
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            //}
        }




    }


    public class PropertyModelBinder : DefaultModelBinder
    {
        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            if (propertyDescriptor.ComponentType == typeof(PropertyModel))
            {
                if (propertyDescriptor.Name == "Tax_rates")
                {
                    var obj = bindingContext.ValueProvider.GetValue("Price");
                    return Convert.ToInt32(obj.AttemptedValue.ToString().Replace(",", ""));
                }
            }
            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }
    }


    public class DecimalModelBinder :  IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            decimal _tmp_val_dec = -1;
            NumberStyles style;

            style = NumberStyles.AllowDecimalPoint;

            try
            {
                if (CultureInfo.CurrentCulture.Name == "de-DE" ||
                    CultureInfo.CurrentCulture.Name == "en-US")
                {
                    //actualValue = Convert.ToDecimal(valueResult.AttemptedValue.Replace(".", ","), CultureInfo.CurrentCulture);
                    if (Decimal.TryParse(valueResult.AttemptedValue, style, CultureInfo.CurrentCulture, out _tmp_val_dec))
                    {
                        actualValue = _tmp_val_dec;
                    }
                    else
                    {
                        throw new FormatException(); 
                    }                    
                }
                else
                {
                    actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.CurrentCulture);
                }

                //actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.CurrentCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
