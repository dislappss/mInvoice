function getPrice(is_edit) {
    var _article_id = $('#article_id').val();
    var _price_netto = $('#price_netto').val();

    //if (is_edit ||
    //    _price_netto === "" && typeof _price_netto === "string") {

        $.ajax({
            url: '/Invoice_details/getPrice',
            type: "GET",
            dataType: "JSON",
            data: { article_id: _article_id },
            success: function (ret_value) {
                $("#price_netto").val(ret_value);
            },
            //error: function (xhr, err) {
            //    alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            //    alert("responseText: " + xhr.responseText);
            //}
        });
    //}
}

function getTaxRate(is_edit) {
    var _article_id = $('#article_id').val();
    var _tax_rate_id = $('#tax_rate_id').val();

    //if (is_edit ||
    //    _tax_rate_id === "" && typeof _tax_rate_id === "string") {

        $.ajax({
            url: '/Invoice_details/getTax_rate_id',
            type: "GET",
            dataType: "JSON",
            data: { article_id: _article_id },
            success: function (ret_value) {
                $("#tax_rate_id").val(ret_value);
            },
            //error: function (xhr, err) {
            //    alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            //    alert("responseText: " + xhr.responseText);
            //}
        });
    //}
}

function getQuantity_unit(is_edit) {
    var _article_id = $('#article_id').val();
    var _quantity_units_id = $('#quantity_units_id').val();

    //if (is_edit ||
    //    _quantity_units_id === "" && typeof _quantity_units_id === "string") {

    $.ajax({
        url: '/Invoice_details/getQuantity_unit',
        type: "GET",
        dataType: "JSON",
        data: { article_id: _article_id },
        success: function (ret_value) {
            $("#quantity_units_id").val(ret_value);
        },
        //error: function (xhr, err) {
        //    alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
        //    alert("responseText: " + xhr.responseText);
        //}
    });
    //}
}

function getArticleDescription(is_edit) {
    var _article_id = $('#article_id').val();
    var _description = $('#description').val();

    //if (is_edit ||
    //    _description === "" && typeof _description === "string") {

        $.ajax({
            url: '/Invoice_details/getArticleDescription',
            type: "GET",
            dataType: "JSON",
            data: { article_id: _article_id },
            success: function (ret_value) {
                $("#description").val(ret_value);
            },
            //error: function (xhr, err) {
            //    alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            //    alert("responseText: " + xhr.responseText);
            //}
        });
    //}
}

function getCustomer(is_edit) {   

    var _customers_id = $('#customers_id').val();

    //alert(_customers_id);


    //var _quantity_units_id = $('#quantity_units_id').val();

    //if (is_edit ||
    //    _quantity_units_id === "" && typeof _quantity_units_id === "string") {

    $.ajax({
        url: '/Invoice_header/getCustomer',
        type: "GET",
        dataType: "JSON",
        data: { customers_id: _customers_id },
        success: function (ret_value) {

            $("#countriesid").val(ret_value.countriesid);
            $("#zip").val(ret_value.zip);
            $("#city").val(ret_value.city);
            $("#street").val(ret_value.street);
            $("#payment_terms_id").val(ret_value.payment_terms_id);
            $("#delivery_terms_id").val(ret_value.delivery_terms_id);
            $("#tax_rate_id").val(ret_value.tax_rate_id);
            $("#currency_id").val(ret_value.currency_id);
        },
        error: function (xhr, err) {
            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
    });
    //}
}