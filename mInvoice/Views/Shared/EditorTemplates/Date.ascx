<%@ Control Language= "C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime" %>
<%@ Import Namespace="System.Threading" %> 
<%@ Import Namespace=" System.Web.Mvc" %>
<%@ Import Namespace=" System.Web.Mvc.Html" %>


<%Html.DropDownListFor(dateTime => dateTime.Day, Enumerable.Range(1, 31).Select(i => new SelectListItem                          
{                              
  Value = i.ToString(),                              
  Text = i.ToString(),                             
  Selected = (i == Model.Day && Model != DateTime.MinValue && Model != DateTime.MaxValue)                         
}), "-- Day --", htmlAttributes: new { @class = "form-control-not-100" })  %> 

<%= Html.DropDownListFor(dateTime => dateTime.Month, Enumerable.Range(1, 12).Select(i => new SelectListItem                          
{                              
  Value = i.ToString(),                              
  Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetMonthName(i),                             
  Selected = (i == Model.Month && Model != DateTime.MinValue && Model != DateTime.MaxValue)                         
}), "-- Month --", htmlAttributes: new { @class = "form-control-not-100" })%>  

<%= Html.DropDownListFor(dateTime => dateTime.Year, Enumerable.Range(DateTime.Now.Year-110, 110).Select(i => new SelectListItem                           
{                                                             
  Value = i.ToString(),                               
  Text = i.ToString(),                              
  Selected = (i == Model.Year && Model != DateTime.MinValue && Model != DateTime.MaxValue)                          
}), "-- Year --", htmlAttributes: new { @class = "form-control-not-100" })%>

<%= Html.HiddenFor(dateTime => dateTime.Hour)%>
<%= Html.HiddenFor(dateTime => dateTime.Minute)%>
<%= Html.HiddenFor(dateTime => dateTime.Second)%>