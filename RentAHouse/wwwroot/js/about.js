// adding the graph and attributes
var svg = d3.select("#graphPie"),
    width = +svg.attr("width"),
    height = +svg.attr("height"),
    radius = Math.min(width, height) / 2,
    g = svg.append("g").attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

// creating a color array
var color = d3.scaleOrdinal(["#98abc5", "#8a89a6", "#7b6888", "#6b486b", "#a05d56", "#d0743c", "#ff8c00"]);

// sorting the values
var pie = d3.pie()
    .sort(null)
    .value(function (d) { return d.count; });

// creating the radius
var path = d3.arc()
    .outerRadius(radius - 10)
    .innerRadius(0);

var label = d3.arc()
    .outerRadius(radius - 40)
    .innerRadius(radius - 40);

// getting the data
// the csv is created in HomeConroller.cs
d3.csv("../../../../data/graphPie.csv", function (d) {
    d.count = +d.count;
    return d;
}, function (error, data) {
    if (error) throw error;

    var arc = g.selectAll(".arc")
        .data(pie(data))
        .enter().append("g")
        .attr("class", "arc");

    // filling the graph
    arc.append("path")
        .attr("d", path)
        .attr("fill", function (d) { return color(d.data.cityName); });

    arc.append("text")
        .attr("transform", function (d) { return "translate(" + label.centroid(d) + ")"; })
        .attr("dy", "0.35em")
        .text(function (d) { return d.data.cityName; });
});
