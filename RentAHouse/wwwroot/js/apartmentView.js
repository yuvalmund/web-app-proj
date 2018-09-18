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

    loadMap("{0} {1} {2}".format([apartment.street,
                                        apartment.houseNumber,
                                        apartment.cityName]));

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
    $("#modelOwnerName").text("Name: " + apartment.firstName + " " + apartment.lastName);
    $("#modelOwnerRate").text("Rate: " + apartment.rate + "/5");
    $("#modalContact").data("OwnerEmail", apartment.Email);

    // Counts clicks as info for owner
    $.post('/ApartmentViews/addClick', { apartment: id }, function (data) {});
}

function contactOwner() {
    var email = $("#modalContact").data().OwnerEmail;
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

//Map
function loadMap(address) {

    $.get('https://dev.virtualearth.net/REST/v1/Locations', {
        countryRegion: "IL",
        addressLine: address,
        maxResaults: 1,
        key: "AlNQea4USaYJiJV4kPGxdLToVtMi7j8YKvoc3CfzjJN0ZVDkyHT809I5wOvQeMdE"
    }, function (data) {
        var coordinates = data.resourceSets[0].resources[0].point.coordinates;

        var map = new Microsoft.Maps.Map(document.getElementById('map'), {
            /* No need to set credentials if already passed in URL */
            center: new Microsoft.Maps.Location(coordinates[0], coordinates[1]),
            mapTypeId: Microsoft.Maps.MapTypeId.aerial,
            zoom: 16
        });

        var pushpin = new Microsoft.Maps.Pushpin(map.getCenter(), null);
        map.entities.push(pushpin);
     }).fail(function (err) {
         console.log("fail to find address");
         var map = new Microsoft.Maps.Map(document.getElementById('map'), {})
     });
}
