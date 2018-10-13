$(document).ready(function()
{
    $.get('/Apartments/GetApartmentByCity',
        function (data) {
            var appartmentsByCities = JSON.parse(data);
            var htmlToAppend = "";

            for (var group of appartmentsByCities) {
                htmlToAppend += `<div><h3>${group[0].cityName}</h3>` +
                    `<table class="table"><thead>` +
                    "<tr>" +
                    "<th>Street</th>  " +
                    "<th>House number</th > " +
                    "<th>Rooms number</th > " +
                    "<th>Size</th > " +
                    "<th>Price</th > " +
                    "<th>City Tax</th > " +
                    "<th>Building Tax</th > " +
                    "<th>Furniture included</th > " +
                    "<th>Is rennovated?</th > " +
                    "<th>Are pets allowed?</th > " +
                    "<th>Is there elivator?</th > " +
                    "<th>Enter date</th > " +
                    "<th>Floor</th > " +
                    "<th></th></tr></thead><tbody>";
                for (var ap of group) {
                    htmlToAppend += "<tr>" +
                        `<td> ${ap.street}</td>` +
                        `<td> ${ap.houseNumber}</td >` +
                        `<td> ${ap.roomsNumber}</td >` +
                        `<td> ${ap.size}</td >` +
                        `<td> ${ap.price}</td >` +
                        `<td> ${ap.cityTax}</td >` +
                        `<td> ${ap.BuildingTax}</td >` +
                        `<td> ${ap.furnitureInculded}</td >` +
                        `<td> ${ap.isRenovatetd}</td >` +
                        `<td> ${ap.arePetsAllowed}</td >` +
                        `<td> ${ap.isThereElivator}</td >` +
                        `<td> ${ap.EnterDate}</td >` +
                        `<td> ${ap.floor}</td >` +
                        `<td>` +
                        `<a href="/Apartments/Details/${ap.ID}">Details</a> |` +
                        `<a href="/Apartments/Edit/${ap.ID}">Edit</a> |` +
                        `<a href="/Apartments/Delete/${ap.ID}">Delete</a>` +
                        `</td>`
                        "</tr>";
                }

                htmlToAppend += "</tbody></table></div>";
            }

            $("#listBycities").append(htmlToAppend)
    });
})