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

    var paramters = {customer_id: _customers_id};

    $.ajax({
        url: '/Invoice_header/getCustomer',
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(paramters),
        success: function (ret_value) {
            $("#countriesid").val(ret_value[0]);
            $("#zip").val(ret_value[1]);
            $("#city").val(ret_value[2]);
            $("#street").val(ret_value[3]);
            $("#payment_terms_id").val(ret_value[4]);
            $("#delivery_terms_id").val(ret_value[5]);
            $("#tax_rate_id").val(ret_value[6]);
            $("#currency_id").val(ret_value[7]);
        },
        error: function (xhr, err) {
            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
    });
}

function getArticle() {

    var _id = $('#customers_id').val();

    var paramters = { id: _id };

    $.ajax({
        url: '/Invoice_details/getArticle',
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(paramters),
        success: function (ret_value) {
            $("#quantity_units_id").val(ret_value[0]);
            $("#description").val(ret_value[1]);
            $("#tax_rate_id").val(ret_value[2]);
            $("#price_netto").val(ret_value[3]);
        },
        error: function (xhr, err) {
            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
    });
}

