//Called from 'onSeeMore' function from apartmentView.js file
//w3hen the apartment modal is about to be opened
function loadMap(address) {
    //All the required parameters for map
    $.get('https://dev.virtualearth.net/REST/v1/Locations', {
        countryRegion: "IL",
        addressLine: address,
        maxResaults: 1,
        key: "AlNQea4USaYJiJV4kPGxdLToVtMi7j8YKvoc3CfzjJN0ZVDkyHT809I5wOvQeMdE"
    }, function (data) {
        var coordinates = data.resourceSets[0].resources[0].point.coordinates;

        var map = new Microsoft.Maps.Map(document.getElementById('map'), {
            center: new Microsoft.Maps.Location(coordinates[0], coordinates[1]),
            mapTypeId: Microsoft.Maps.MapTypeId.aerial,
            zoom: 16
        });

        //Pin the address
        var pushpin = new Microsoft.Maps.Pushpin(map.getCenter(), null);
        map.entities.push(pushpin);
    }).fail(function (err) {
        console.log("fail to find address");
    });
}
