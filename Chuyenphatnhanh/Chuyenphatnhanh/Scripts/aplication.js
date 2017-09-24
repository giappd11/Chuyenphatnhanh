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
}

$(document).ready(function () {

    $('.table').dynatable();
})