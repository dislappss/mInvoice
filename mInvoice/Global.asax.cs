using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using mInvoice.Models;

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
            ModelBinders.Binders.Add(typeof(Nullable<DateTime>), new DateTimeModelBinder()
            {
                //Date = DateTime.Now, // Date parts are not splitted in the View
                //// (e.g. the whole date is held by a TextBox  with id “xxx_Date”)
                //Time = DateTime.Now, // Time parts are not  splitted in the View
                //// (e.g. the whole time  is held by a TextBox with id “xxx_Time”)
                //Day = DateTime.Now.Day,
                //Month = DateTime.Now.Month,
                //Year = DateTime.Now.Year,
                //Hour = DateTime.Now.Hour,
                //Minute = DateTime.Now.Minute,
                //Second = DateTime.Now.Second
            });

            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder()
            {
                Date = DateTime.Now, // Date parts are not splitted in the View
                // (e.g. the whole date is held by a TextBox  with id “xxx_Date”)
                Time = DateTime.Now, // Time parts are not  splitted in the View
                // (e.g. the whole time  is held by a TextBox with id “xxx_Time”)
                Day = DateTime.Now.Day,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                Hour = DateTime.Now.Hour,
                Minute = DateTime.Now.Minute,
                Second = DateTime.Now.Second
            });

            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(DateRequiredAttribute), typeof(SplittedDateRequiredValidator));
        }
        
        private void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            //string culture = null;
            //if (context.Request.UserLanguages != null && Request.UserLanguages.Length > 0)
            //{
            // culture = Request.UserLanguages[0];
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
            //Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            //}


        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var handler = Context.Handler as MvcHandler;
            var routeData = handler != null ? handler.RequestContext.RouteData : null;
            var routeCulture = routeData != null && routeData.Values["culture"] != null ? routeData.Values["culture"].ToString() : null;
            var languageCookie = HttpContext.Current.Request.Cookies["lang"];
            var userLanguages = HttpContext.Current.Request.UserLanguages;

            // Set the Culture based on a route, a cookie or the browser settings,
            // or default value if something went wrong
            var cultureInfo = new CultureInfo(
                routeCulture ?? (languageCookie != null
                   ? languageCookie.Value
                   : userLanguages != null
                       ? userLanguages[0]
                       : "en")
            );

            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }
    }

    public class DecimalModelBinder : IModelBinder
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
                if (CultureInfo.CurrentCulture.Name == "de" ||
                    CultureInfo.CurrentCulture.Name == "en")
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
            catch (Exception e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }

    public class DateTimeModelBinder : IModelBinder
    {
        public DateTime? Date = new DateTime? ();
        public DateTime? Time = new DateTime? ();
        public int? Day = null;
        public int? Month = null;
        public int? Year = null;
        public int? Hour =  null;
        public int? Minute = null;
        public int? Second = null;


        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            var _day = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".Day");
            var _month = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".Month");
            var _year = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".Year");

            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;

            try
            {
                //if (CultureInfo.CurrentCulture.Name == "de" ||
                //    CultureInfo.CurrentCulture.Name == "en")
                //{
                    if (_year != null && 
                        !string.IsNullOrEmpty(_year.AttemptedValue)
                        && !string.IsNullOrEmpty(_month.AttemptedValue)
                        && !string.IsNullOrEmpty(_day.AttemptedValue))
                    {
                        actualValue = new DateTime(
                            Convert.ToInt32(_year.AttemptedValue),
                            Convert.ToInt32(_month.AttemptedValue),
                            Convert.ToInt32(_day.AttemptedValue), 0, 0, 0);
                    }
                    else
                    {
                        actualValue = new Nullable<DateTime>();// DateTime.Now; 
                    }
                //}

            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }
            catch (Exception e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
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

       
    }
    
    
}
