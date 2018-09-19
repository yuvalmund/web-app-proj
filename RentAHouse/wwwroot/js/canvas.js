var c = document.getElementById("myCanvas");
var ctx = c.getContext("2d");
ctx.fillStyle = "#FF0000";
ctx.fillRect(12.5, 30, 175, 70);

// Draw triangle
ctx.fillStyle = "#A2322E";
ctx.beginPath();
ctx.moveTo(12.5, 30);
ctx.lineTo(185, 30);
ctx.lineTo(99, 0);
ctx.closePath();
ctx.fill();

//windows
ctx.fillStyle = "#663300";
ctx.fillRect(25, 40, 35, 50);
ctx.fillStyle = "#0000FF";
ctx.fillRect(27, 42, 13, 23);
ctx.fillRect(43, 42, 13, 23);
ctx.fillRect(43, 67, 13, 21);
ctx.fillRect(27, 67, 13, 21);

//door
ctx.fillStyle = "#754719";
ctx.fillRect(80, 53, 30, 100);

//door knob
ctx.beginPath();
ctx.fillStyle = "#F2F2F2";
ctx.arc(105, 75, 3, 0, 2 * Math.PI);
ctx.fill();
ctx.closePath();

//Text on the Right
//ctx.font = "20px Veranda";
//ctx.fillText("RentAHouse", 130, 60);