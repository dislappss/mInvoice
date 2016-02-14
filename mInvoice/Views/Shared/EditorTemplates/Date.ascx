<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Nullable<DateTime>>" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace=" System.Web.Mvc" %>
<%@ Import Namespace=" System.Web.Mvc.Html" %>

<style>
    .horizontal-style {
        display: table;
        width: 100%
    }
    .horizontal-style-cell-day {
       padding: 5px 6px;
       text-align: center;
    }
    .horizontal-style-cell-month {
    padding: 5px 6px;
       text-align: center;
    }
    .horizontal-style-cell-year {
        padding: 5px 6px;
       text-align: center;
    }
        
</style>

<table >
    <tr>
        <td><%= @Html.DropDownList("Day",
            Enumerable.Range(1, 31).Select(i => new SelectListItem                          
            {                              
              Value = i.ToString(),                              
              Text = i.ToString(),                             
              Selected = (Model.HasValue ?  i == ((DateTime)Model).Day && Model != DateTime.MinValue && Model != DateTime.MaxValue : false)                         
            }), 
            "-- " + @Resources.Resource.day + " --", 
            htmlAttributes: new { @class = "horizontal-style-cell-day form-control" } ) %>
        </td>

        <td><%= @Html.DropDownList("Month", 
            Enumerable.Range(1, 12).Select(i => new SelectListItem                          
            {                              
              Value = i.ToString(),                              
              Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetMonthName(i),                             
              Selected = (Model.HasValue ?  i == ((DateTime)Model).Month && Model != DateTime.MinValue && Model != DateTime.MaxValue : false) 
            }), 
            "-- " + @Resources.Resource.month + " --",  
            htmlAttributes: new { @class = "horizontal-style-cell-month form-control" }) %>
        </td>

        <td><%= @Html.DropDownList("Year", 
            Enumerable.Range(DateTime.Now.Year-5, 15).Select(i => new SelectListItem                           
            {                                                             
              Value = i.ToString(),                               
              Text = i.ToString(),                              
             Selected = (Model.HasValue ?  i == ((DateTime)Model).Year && Model != DateTime.MinValue && Model != DateTime.MaxValue : false) 
            }), 
            "-- " + @Resources.Resource.year + " --",  
            htmlAttributes: new { @class = "horizontal-style-cell-year form-control"}) %>
        </td>
    </tr>
    <tr>
        <td colspan="3">
          <%= @Html.ActionLink(@Resources.Resource.positions, "Index", "Invoice_details", new { @class = "btn btn-primary btn-xs" })%>
        </td>
    </tr>
</table>