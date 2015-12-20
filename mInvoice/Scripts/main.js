$(function () {
    $("body nav li").each(function () {
        if ($(this).find("ul").length > 0) {

            //show subnav on hover
            $(this).mouseenter(function () {
                $(this).find("ul").stop(true, true).slideDown();
            });

            //hide submenus on exit
            $(this).mouseleave(function () {
                $(this).find("ul").stop(true, true).slideUp();
            });

            $(this).find("ul").mousemove(function () {
                $(this).stop(true, true).show();
            });
        }
    });
});

Sys.Mvc.ValidatorRegistry.validators.splittedDateRequiredValidator = function (rule) {        
    var dayFieldId = rule.ValidationParameters.dayFieldId;    
    var monthFieldId = rule.ValidationParameters.monthFieldId;    
    var yearFieldId = rule.ValidationParameters.yearFieldId;    
    return function (value, context) {                
        var dayIdx = $get(dayFieldId).selectedIndex;        
        var monthIdx = $get(monthFieldId).selectedIndex;        
        var yearIdx = $get(yearFieldId).selectedIndex;        
        if (dayIdx === 0 || monthIdx === 0 || yearIdx === 0) return false;        
        else return isValidDate(parseInt($get(yearFieldId).value), monthIdx, dayIdx);     
    };
};

function isValidDate(y, m, d) {
    var _mon = m - 1

    var date = new Date(y, _mon, d);
    var convertedDate = "" + date.getFullYear() + (date.getMonth() + 1) + date.getDate();
    var givenDate = "" + y + m + d;
    return (givenDate == convertedDate);
}