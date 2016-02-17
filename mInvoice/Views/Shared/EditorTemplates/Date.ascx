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

<%--<script type ="text/javascript" >
    function setValue(value) {
        var _today = new Date();

        var _day = this_form..getElementsByTagName('this_form').getElementById('day');
        var _month = this_form.getElementsByTagName ('this_form').getElementById('month');
        var _year = this_form.getElementsByTagName('this_form').getElementById('year');

        if (value == 'null') {
            //$(".day option[value='']").attr('selected', true)
            //$(".month option[value='']").attr('selected', true)
            //$(".year option[value='']").attr('selected', true)

            _day.value = '';
            _day.selected = true;

            _month.value = '';
            _month.selected = true;

            _year.value = '';
            _year.selected = true;

            $(".day option[value='']").attr('selected', true)
            $(".month option[value='']").attr('selected', true)
            $(".year option[value='']").attr('selected', true)
        }
        else {
            $(".day option[value='" + _today.getDay() + "']").attr('selected', true)
            $(".month option[value='" + _today.getMonth() + "']").attr('selected', true)
            $(".year option[value='" + _today.getYear() + "']").attr('selected', true)
        }
    }
</script>--%>
<form id="this_form">
        <table>
            <tr>
                <td><%= @Html.DropDownList("Day",
                    Enumerable.Range(1, 31).Select(i => new SelectListItem                          
                    {                              
                      Value = i.ToString(),                              
                      Text = i.ToString(),                             
                      Selected = (Model.HasValue ?  i == ((DateTime)Model).Day && Model != DateTime.MinValue && Model != DateTime.MaxValue : false)                         
                    }), 
                    "-- " + @Resources.Resource.day + " --", 
                    htmlAttributes: new { @class = "horizontal-style-cell-day form-control", @id="day" } ) %>
                </td>

                <td><%= @Html.DropDownList("Month", 
                    Enumerable.Range(1, 12).Select(i => new SelectListItem                          
                    {                              
                      Value = i.ToString(),                              
                      Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetMonthName(i),                             
                      Selected = (Model.HasValue ?  i == ((DateTime)Model).Month && Model != DateTime.MinValue && Model != DateTime.MaxValue : false) 
                    }), 
                    "-- " + @Resources.Resource.month + " --",  
                    htmlAttributes: new { @class = "horizontal-style-cell-month form-control", @id="month" }) %>
                </td>

                <td><%= @Html.DropDownList("Year", 
                    Enumerable.Range(DateTime.Now.Year-5, 15).Select(i => new SelectListItem                           
                    {                                                             
                      Value = i.ToString(),                               
                      Text = i.ToString(),                              
                     Selected = (Model.HasValue ?  i == ((DateTime)Model).Year && Model != DateTime.MinValue && Model != DateTime.MaxValue : false) 
                    }), 
                    "-- " + @Resources.Resource.year + " --",  
                    htmlAttributes: new { @class = "horizontal-style-cell-year form-control", @id="year"}) %>
                </td>
            </tr>
           <%-- <tr>
                <td colspan="3">
                   <button type="button"  onclick="javascript: setValue('null');" >
                       <%= Resources.Resource.delete %></button>      
                   <button type="button"  onclick="javascript: setValue('today');" >
                       <%= @Resources.Resource.set_today  %></button>
                </td>
            </tr>--%>
        </table>
    </form>