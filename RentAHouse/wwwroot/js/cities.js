var regions;

$(document).ready(function () {
    $.ajax({
        url: '/Cities/getDistrictEnum',
        type: "GET",
        success: function (response) {
            regions = JSON.parse(response);
        }
    });
    onSearch();
});
        //$.ajax({
        //    url: "/Cities/GetCitiesBtCriterias",
        //    data: {
        //        "name": $("#selectCity").val(),
        //        "minNumOfResidents": $("#numOfResidents").val(),
        //        "region": $("#region").val()
        //    },
        //    type: "GET",
        //}).done(function (data) {
        //    var cities = JSON.parse(data);
        //    cities.forEach(function (city) {
        //        var addcities = "<tr><td>"
        //            + city.cityName + "</td> <td>"
        //            + city.GraduatesPercents + "</td> <td>"
        //            + city.mayor + "</td> <td>"
        //            + city.avarageSalary + "</td> <td>"
        //            + city.numOfResidents + "</td> <td>"
        //            + city.region + "</td> <td>"
        //            + ap.cityTax + "</td> </tr>"
        //        $("#tableBody").append(addCities);
        //    })
        //});
    //});

    function onSearch() {
        $.ajax({
            url: '/Cities/GetCitiesBtCriterias',
            data: {
                "region": $("#region").val(),
                "name": $("#selectCity").val(),
                "minNumOfResidents": $("#numOfResidents").val() 
            },
            type: "GET",
            success: function (response) {
                var cities = JSON.parse(response);

                $("#tableBody").empty();

                cities.forEach(function (city) {
                    var addcities = "<tr><td>"
                        + city.cityName + "</td> <td>"
                        + city.GraduatesPercents + "</td> <td>"
                        + city.mayor + "</td> <td>"
                        + city.avarageSalary + "</td> <td>"
                        + city.numOfResidents + "</td> <td>"
                        + regions[city.region] + "</td> <td>";
                    $("#tableBody").append(addcities);
                })
            }
        });
}