﻿$(document).ready(function () {
    $.post('/Apartments/GetAllCites', function (data, status) {
        var cityArray = JSON.parse(data);

        $("#selectCity").empty();

        var sel = $("#selectCity");
        cityArray.forEach(function (city) {
            sel.append($("<option>").attr('value', city.ID).text(city.cityName));
        });
    });
});
function send() {
    $.post('/Apartments/Create',
        {
            city: $("#selectCity").val(),
            street: $("#street").val(),
            houseNumber: $("#houseNumber").val(),
            roomsNumber: $("#roomsNumber").val(),
            price: $("#price").val(),
            cityTax: $("#cityTax").val(),
            BuildingTax: $("#BuildingTax").val(),
            furnitureInculded: $("#furnitureInculded").val(),
            isRenovatetd: $("#isRenovatetd").val(),
            arePetsAllowed: $("#arePetsAllowed").val(),
            isThereElivator: $("#isThereElivator").val(),
            EnterDate: $("#EnterDate").val(),
            floor: $("#floor").val()
        },
        function (data, status) {

        });
}

var form = $("#createForm");
form.submit(function (e) {
    if (form[0].checkValidity()) {
        e.preventDefault();
        this.classList.add("hidden");
        $("#finishMessage").removeClass("hidden");
    }
});
