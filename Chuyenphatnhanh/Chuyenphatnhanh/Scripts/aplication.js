function DistrictChange(selected, wardId) {
    $.ajax({
        url: '/WardMst/DistrictChange?DistrictID=' + selected.value,
        type: 'get',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) { SuccessDistrictChangeHandler(wardId, data); }
    });
    return false;
}
function SuccessDistrictChangeHandler(wardId, data) {
    $('#' + wardId).find('option').remove();
    $.each(data, function (index, item) { // item is now an object containing properties ID and Text
        $('#' + wardId).append($("<option></option>")
            .attr("value", item.value)
            .text(item.displayValue));
    });
    $('#' + wardId).trigger('chosen:updated');
}

$(document).ready(function () {
    $('.table').dynatable();
})

var config = {
    '.chosen-select': { width: '100%' },
    '.chosen-select-lang': { width: '20%' },
    '.chosen-select-deselect': { allow_single_deselect: true },
    '.chosen-select-no-single': { disable_search_threshold: 10 },
    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
    '.chosen-select-rtl': { rtl: true },
    '.chosen-select-width': { width: '95%' }
}
for (var selector in config) {
    $(selector).chosen(config[selector]);
}
function showleft() {
    if (jQuery("body").hasClass("show-left")) {
        jQuery("body").removeClass("show-left");
    } else {
        jQuery("body").addClass("show-left");
    }
}
jQuery(".menu-btnmain").click(function () {
    showleft();
});
jQuery(".btn-close-mn").click(function () {
    showleft();
});
if ($(window).width() > 992) {
    if (!$("body").hasClass("show-left")) {
        $("body").addClass("show-left");
    }
}
function changeLanguage(lang) {
    var url = '/Home/ChangeLanguage?lang=' + lang.val;
    window.location.replace(url);

}

$(document).ready(function () {
    $(".addnewBill").click(function () {
        var _count = $(".billCount").val();
        _count++;
    });
});
$(document).ready(function () {
    $("#Cust_From_Phone").blur(function () {
        var _phone = $("#Cust_From_Phone").val();
        $.ajax({
            url: '/CustMst/GetCustomer?phone=' + _phone,
            type: 'get',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) { pushdata(data); }
        });
    })
}) 
function pushdata(data) {
    $("#Cust_From_Name").val(data.CUST_NAME);
    $("#CUST_FROM_ID").val(data.CUST_ID);
    $("#ADDRESS_FROM").val(data.DEFAULT_ADDRESS);
    $("#DISTRICT_ID_FROM").val(data.DEFAULT_DISTRICT_ID).change();
    $("#DISTRICT_ID_FROM").trigger('chosen:updated');
    $("#DISTRICT_ID_FROM").delay(2000);
    $("#WARD_ID_FROM").val(data.DEFAULT_WARD_ID);    
}

$(document).ready(function () {
    $("#Cust_To_Phone").blur(function () {
        var _phone = $("#Cust_To_Phone").val();
        $.ajax({
            url: '/CustMst/GetCustomer?phone=' + _phone,
            type: 'get',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) { pushdataTo(data); }
        });
    })
})
function pushdataTo(data) {
    $("#Cust_To_Name").val(data.CUST_NAME);
    $("#CUST_TO_ID").val(data.CUST_ID);
    $("#ADDRESS_TO").val(data.DEFAULT_ADDRESS);
    $("#DISTRICT_ID_TO").val(data.DEFAULT_DISTRICT_ID).change();
    $("#DISTRICT_ID_TO").trigger('chosen:updated');
    $("#DISTRICT_ID_TO").delay(2000);
    $("#WARD_ID_TO").val(data.DEFAULT_WARD_ID);
}