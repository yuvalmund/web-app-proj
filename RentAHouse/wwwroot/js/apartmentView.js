var apartments;

function tableBuilder(apartment) {

    var tb = "<td>{0}</td><td>{1}</td>" +
        "<td>{2}</td><td>{3}</td><td>{4}</td>" +
        "<td>{5}</td>" +
        "<td><input id='seeMoreButton" + apartment.ID + "' type='button' class='btn btn - success btn - lg' value='See more' data-toggle='modal' data-target='#modal' onclick='onSeeMore(" + apartment.ID + ")'/></td>";

    return tb.format([apartment.street,
    apartment.houseNumber,
    apartment.cityName,
    apartment.roomsNumber,
    apartment.size,
    apartment.price]);
}


function onRegionSelected(region) {
    $.get('/Apartments/GetCities', { region: region }, function (data) {
        var cityArray = JSON.parse(data);

        $("#selectCity").empty();
        var sel = $("#selectCity");
        cityArray.forEach(function (city) {

            sel.append($("<option>").attr('value', city.ID).text(city.cityName));
        });
    });
}

function onSearch() {
    $.ajax({
        url: '/Apartments/GetApartments',
        data: {
            "cityId": $("#selectCity").val(),
            "roomNumber": $("#roomNum").val() || -1,
            "minPrice": $("#minPrice").val() || -1,
            "maxPrice": $("#maxPrice").val() || -1
        },
        type: "GET",
        success: function (response) {
            apartments = JSON.parse(response);

            // Sort by price
            apartments.sort(function (a, b) {
                return a.price - b.price;
            });

            $("#tableBody").empty();

            apartments.forEach(function (apartment) {
                //TODO - add all wanted parameters
                row = document.createElement("TR");
                row.innerHTML = tableBuilder(apartment);
                $("#tableBody").append(row);
            });
            $("#apartmentsTable").css('display', apartments.length ? '' : 'none');
        },
        error: function (xhr) {
            //TODO - needed?
        }
    });
}

function onSeeMore(id) {
    var apartment = apartments.find(a => a.ID == id);

    $('#modelStreet').text(apartment.street);
    $("#modelNumber").text(apartment.houseNumber);
    $("#modelPrice").text(apartment.price);
    $("#modelSize").text(apartment.size);
    $("#modelRooms").text(apartment.roomsNumber);
    $("#modelFloor").text(apartment.floor);
    $("#modelCityTax").text(apartment.cityTax);
    $("#modelBuildingTax").text(apartment.BuildingTax);
    $("#modelCity").text(apartment.cityName);
    $("#modelRenovated").text(apartment.isRenovatetd ? "✓" : "✗");
    $("#modelPets").text(apartment.arePetsAllowed ? "✓" : "✗");
    $("#modelElevator").text(apartment.isThereElivator ? "✓" : "✗");
    $("#modelFurniture").text(apartment.furnitureInculded ? "✓" : "✗");
    $("#modelDate").text(apartment.EnterDate.slice(0, 10));
    $("#modelOwnerName").text(apartment.firstName + " " + apartment.lastName);

    $.post('/ApartmentViews/addClick', { apartment: id }, function (data) {
    });



}

// Makes life easier for tableBuilder function
String.prototype.format = function (args) {
    var str = this;
    return str.replace(String.prototype.format.regex, function (item) {
        var intVal = parseInt(item.substring(1, item.length - 1));
        var replace;
        if (intVal >= 0) {
            replace = args[intVal];
        } else if (intVal === -1) {
            replace = "{";
        } else if (intVal === -2) {
            replace = "}";
        } else {
            replace = "";
        }
        return replace;
    });
};
String.prototype.format.regex = new RegExp("{-?[0-9]+}", "g");