<%@ Control Language= "C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime>"%>
<%@ Import Namespace="System.Threading" %> 
<%@ Import Namespace=" System.Web.Mvc" %>
<%@ Import Namespace=" System.Web.Mvc.Html" %>

<style>
    .horizontal-style {
        display: table;
        width: 100%
    }
    /*.horizontal-style li {
        display: table-cell;
    }*/
    .horizontal-style-cell-day {
       display: table-cell ;
       width:70px;
       text-align: center;
    }
    .horizontal-style-cell-month {
       display: table-cell ;
       width:120px;
       text-align: center;
    }
    .horizontal-style-cell-year {
       display: table-cell ;
       width:90px;
       text-align: center;
    }
    

    .
</style>

<div class="horizontal-style">
    <%= @Html.DropDownListFor(dateTime => dateTime.Day, Enumerable.Range(1, 31).Select(i => new SelectListItem                          
    {                              
      Value = i.ToString(),                              
      Text = i.ToString(),                             
      Selected = (i == Model.Day && Model != DateTime.MinValue && Model != DateTime.MaxValue)                         
    }), "-- " + @Resources.Resource.day + " --", htmlAttributes: new { @class = "horizontal-style-cell-day form-control" } ) %>

    <%= @Html.DropDownListFor(dateTime => dateTime.Month, Enumerable.Range(1, 12).Select(i => new SelectListItem                          
    {                              
      Value = i.ToString(),                              
      Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetMonthName(i),                             
      Selected = (i == Model.Month && Model != DateTime.MinValue && Model != DateTime.MaxValue)
    }), "-- " + @Resources.Resource.month + " --",  htmlAttributes: new { @class = "horizontal-style-cell-month form-control" }) %>

    <%= @Html.DropDownListFor(dateTime => dateTime.Year, Enumerable.Range(DateTime.Now.Year-100, 200).Select(i => new SelectListItem                           
    {                                                             
      Value = i.ToString(),                               
      Text = i.ToString(),                              
      Selected = (i == Model.Year && Model != DateTime.MinValue && Model != DateTime.MaxValue)
    }), "-- " + @Resources.Resource.year + " --",  htmlAttributes: new { @class = "horizontal-style-cell-year form-control"}) %>
</div>