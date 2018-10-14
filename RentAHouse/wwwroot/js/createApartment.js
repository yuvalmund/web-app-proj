$(document).ready(function () {
    $.post('/Cities/GetCitiesBtCriterias',
        {
            region: 0
        },
        function (data, status) {
        var cityArray = JSON.parse(data);

        $("#selectCity").empty();

        var sel = $("#selectCity");
        cityArray.forEach(function (city) {
            sel.append($("<option>").attr('value', city.ID).text(city.cityName));
        });

            
     $("#suggestPriceButton").click(onSuggestPrice);
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
        $("#shareModal").('toggle');
    }
});

function onSuggestPrice() {
        $.ajax({
            url: '/ML/predict',
            data: {
                "inCityID": $("#selectCity").val(),
                "inRoomsNumber": $("#roomsNumber").val(),
                "inSizeInMeters": $("#size").val(),
                "inIsThereElivator": $("#isThereElivator").is(":checked"),
                "inFurnitureInculded": $("#furnitureInculded").is(":checked"),
                "inIsRenovated": $("#isRenoveted").is(":checked")
            },
            type: "GET",
            success: function (response) {
                $("#price").val(response);
            }
        });
}