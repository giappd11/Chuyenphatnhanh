var MultiLanguage = MultiLanguage || {};

function DistrictChange(selected, wardId) {
    $('#ajax-wall').css("display", "block");
    $.ajax({
        url: '/'+MultiLanguage.Cookies.getCookie('Language')+'/WardMst/DistrictChange?DistrictID=' + selected.value,
        type: 'get',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) { SuccessDistrictChangeHandler(wardId, data); }
    });
    $('#ajax-wall').css("display", "none");
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
    $('.table').dynatable({ features: { responsive: true } });
})

var config = {
    '.chosen-select': { width: '100%' },
    '.chosen-select-lang': { width: '120px' },
    '.chosen-select-deselect': { allow_single_deselect: true },
    '.chosen-select-no-single': { disable_search_threshold: 10 },
    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
    '.chosen-select-rtl': { rtl: true },
    '.chosen-select-width': { width: '95%' }
}
for (var selector in config) {
    $(selector).chosen(config[selector]);
}

MultiLanguage.createNameSpace = function (namespace) {

    var namespaceArr = namespace.split('.');
    var parent = MultiLanguage;

    if (namespaceArr[0] === 'MultiLanguage') {
        namespaceArr = namespaceArr.slice(1);
    }

    for (var i = 0; i < namespaceArr.length; i++) {
        var partname = namespaceArr[i];
        if (typeof parent[partname] === 'undefined') {
            parent[partname] = {};
        }
        parent = parent[partname];
    }
    return parent;
};

MultiLanguage.createNameSpace("MultiLanguage.Cookies");

MultiLanguage.Cookies.getCookie = function (ck_name) {
    var ck_value = document.cookie;
    var ck_start = ck_value.indexOf(" " + ck_name + "=");
    if (ck_start == -1) {
        ck_start = ck_value.indexOf(ck_name + "=");
    }
    if (ck_start == -1) {
        ck_value = null;
    } else {
        ck_start = ck_value.indexOf("=", ck_start) + 1;
        var ck_end = ck_value.indexOf(";", ck_start);
        if (ck_end == -1) {
            ck_end = ck_value.length;
        }
        ck_value = unescape(ck_value.substring(ck_start, ck_end));
    }
    return ck_value;
};

MultiLanguage.Cookies.setCookie = function (ck_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var ck_value = escape(value) + ((exdays == null) ? "" : "; path=/;expires=" + exdate.toUTCString());
    document.cookie = ck_name + "=" + ck_value;
};

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
    MultiLanguage.Cookies.setCookie("Language", lang.value.toLowerCase(), 30);
    location.reload(true);
}

$(document).ready(function () {
    $(".addnewBill").click(function () {
        var _count = $(".billCount").val();
        _count++;
    });
});

$(document).ready(function () {
    $(".order-create #Cust_From_Phone").blur(function () {
        
        var _phone = $("#Cust_From_Phone").val();
        if (_phone != "") {
            $('#ajax-wall').css("display", "block");
            $.ajax({
                url: '/' + MultiLanguage.Cookies.getCookie('Language') + '/CustMst/GetCustomer?phone=' + _phone,
                type: 'get',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    pushdata(data);
                }
            });
            $('#ajax-wall').css("display", "none");
        }
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
    $(".order-create  #Cust_To_Phone").blur(function () {
        
        var _phone = $("#Cust_To_Phone").val();
        if (_phone != "") {
            $('#ajax-wall').css("display", "none");
            $.ajax({
                url: '/' + MultiLanguage.Cookies.getCookie('Language') + '/CustMst/GetCustomer?phone=' + _phone,
                type: 'get',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    pushdataTo(data);
                }

            });
            $('#ajax-wall').css("display", "none");
        }
    });


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

$(document).ready(function () {
    $(".addnewBill").click(function () {
        $('#ajax-wall').css("display", "block");
        var _billCount = parseInt($(".billCount").val()) + 1;
        $.ajax({
            url: '/' + MultiLanguage.Cookies.getCookie('Language') +'/Bill/NewBill?position=' + _billCount,
            type: 'get',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var count = parseInt($(".billCount").val());
                $(".removenewBill").removeClass("removenewBill");
                while (count >= 0) {
                    count--;
                    if ($(".bill" + count).length > 0) {
                        console.log(".bill" + count);
                        $(".bill" + count).after(data);
                        break;
                    }
                }
                $(".billCount").val(_billCount);
                $('#ajax-wall').css("display", "none");
            }
        });
        return false;
    })
})
$(document).ajaxStop(function () {
    $(".removenewBill").click(function () {
        var _billCount = parseInt($(".billCount").val()) - 1;
        $(".billCount").val(_billCount);
        _billCount--;
        console.log(".bill" + _billCount);
        $(".bill" + _billCount + " .iconremove").addClass("removenewBill");
        $(this).parent().parent().remove();
    });
});
$('#DISTRICT_ID_FROM').change(function () {
    caculTotalAmoun();
    console.log(2);
})
$('#DISTRICT_ID_TO').change(function () {
    caculTotalAmoun();
    console.log(3);
})
$('#WARD_ID_FROM').change(function () {
    caculTotalAmoun();
    console.log(4);
})
$('#WARD_ID_TO').change(function () {
    caculTotalAmoun();
    console.log(5);
})
$(document).ajaxStop(function () {
    $('.weight').focusout(function () {
        caculTotalAmoun();
        console.log(1);
    })
    
})
function caculTotalAmoun() {
    try {
        $('#ajax-wall').css("display", "block");
        var districtfrom = $('#DISTRICT_ID_FROM').val();
        var districtto = $('#DISTRICT_ID_TO').val();
        var wardfrom = $('#WARD_ID_FROM').val();
        var wardto = $('#WARD_ID_TO').val();
        var weights = '';
        var weight;
        var count = 0;
        $('.weight').each(function () {
            weight = parseInt($(this).val());
            if (count != 0) {
                weights += ',';
            }
            count++;
            weights += weight;
        });
        weights.substring(0, weights.length - 2);
        var url = '/' + MultiLanguage.Cookies.getCookie('Language') +'/Bill/CaculAmount?districtfrom=' + districtfrom + '&wardfrom=' + wardfrom + '&districtto=' + districtto + '&wardto=' + wardto + '&weight=' + weights;

            $.ajax({
                url: url,
                type: 'get',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('.totalAmount').html(data);
                    $('#AMOUNT').val(data);
                    $('#ajax-wall').css("display", "none");
                }
            }); 
        $('#ajax-wall').css("display", "none");
        return false;
    } catch (err) {
        console.log(err);
    }
}

$('a:not(".chosen-single")').click(function () {
    $('#ajax-wall').css("display", "block");
});

$(document).ready(function () {
    $('#ajax-wall').css("display", "none");
})

$(document).ready(function () {
    $(".toggle").click(function () {
        
        if ($(this).hasClass('fa-plus')) {
            $(this).removeClass('fa-plus');
            $(this).addClass('fa-minus');
        } else if ($(this).hasClass('fa-minus')) {
            $(this).removeClass('fa-minus');
            $(this).addClass('fa-plus');
        }
        $(this).next().addClass("active");
        $(this).next().slideToggle();
    })
})