var regions;

$(document).ready(function () {
    $.ajax({
        url: '/Districts/getDistrictEnum',
        type: "GET",
        success: function (response) {
            regions = JSON.parse(response);
        }
    });
    onSearch();
});
        
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