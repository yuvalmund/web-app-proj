var apartments;

//Build result list for search
function tableBuilder(apartment) {

    var tb = "<td>{0}</td><td>{1}</td>" +
        "<td>{2}</td><td>{3}</td><td>{4}</td>" +
        "<td>{5}</td>" +
        "<td><input id='seeMoreButton" + apartment.ID +
        "' type='button' class='btn'" +
        " value='See more' data-toggle='modal' data-target='#modal'" +
        " onclick = 'onSeeMore(" + apartment.ID + ")' /></td > ";

    return tb.format([apartment.street,
    apartment.houseNumber,
    apartment.cityName,
    apartment.roomsNumber,
    apartment.size,
    apartment.price]);
}

//Update cities list by chosen region
function onRegionSelected(region) {
    $.get('/Cities/GetCitiesBtCriterias', { region: region }, function (data) {
        var cityArray = JSON.parse(data);

        $("#selectCity").empty();
        var sel = $("#selectCity");
        cityArray.forEach(function (city) {

            sel.append($("<option>").attr('value', city.ID).text(city.cityName));
        });
    });
}

//Search apartments by parameters and update the result list
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
                row = document.createElement("TR");
                row.innerHTML = tableBuilder(apartment);
                $("#tableBody").append(row);
            });
            $("#apartmentsTable").css('display', apartments.length ? '' : 'none');
        }
    });
}

//Fill apartment information modal with data
//Add click to clicks-counter table in db
function onSeeMore(id) {

    // Counts clicks as info for owner
    $.post('/ApartmentViews/addClick', { apartment: id }, function (data) { });

    var apartment = apartments.find(a => a.ID == id);

    //This function is in map.js file
    //Looking for the accurate address and display on map
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

    
   
}


// Makes life easier for formating strings:
// with format function you can send parameters to locations in the string
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
