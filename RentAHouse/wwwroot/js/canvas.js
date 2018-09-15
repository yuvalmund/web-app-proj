var canvas = $("#canvas");
var c = canvas.get(0).getContext('2d');
c.canvas.width = window.innerWidth;
c.canvas.height = window.innerHeight;
var cX = canvas.width / 2;
var cY = canvas.height / 2;
var blue = "#447FFF";
var white = "#FFF";

// house body
c.beginPath();
c.rect(cX - 100, cY - 50, 200, 200);
c.fillStyle = blue;
c.fill();
c.closePath();

// window
c.beginPath();
c.rect(cX - 70, cY - 20, 40, 40);
c.fillStyle = white;
c.fill();
c.closePath();

// roof
c.beginPath();
c.moveTo(cX - 100, cY - 50);
c.lineTo(cX, cY - 150);
c.lineTo(cX + 100, cY - 50);
c.lineTo(cX - 100, cY - 50);
c.fillStyle = blue;
c.fill();
c.closePath();

// door
c.beginPath();
c.rect(cX + 20, cY + 71, 50, 80);
c.fillStyle = white;
c.fill();
c.closePath();

// roof decor
for (var j = 0; j < 5; j++) {
    for (var i = 0; i < 10; i++) {
        c.beginPath();
        c.moveTo((cX - 70) + (i * 20), (cY - 50) - (j * 20));
        c.lineTo((cX - 80) + (i * 20), (cY - 60) - (j * 20));
        c.moveTo((cX - 110) + (i * 20), (cY - 50) - (j * 20));
        c.lineTo((cX - 90) + (i * 20), (cY - 70) - (j * 20));
        c.strokeStyle = white;
        c.stroke();
        c.closePath();
    }
}

// body decor
for (var j = 0; j < 7; j++) {
    for (var i = 0; i < 8; i++) {
        c.beginPath();
        c.moveTo((cX - 100) + (i * 30), (cY + 135) - (j * 30));
        c.lineTo((cX - 130) + (i * 30), (cY + 135) - (j * 30));
        c.lineTo((cX - 130) + (i * 30), (cY + 150) - (j * 30));
        if (j != 6) {
            c.moveTo((cX - 115) + (i * 30), (cY + 135) - (j * 30));
            c.lineTo((cX - 115) + (i * 30), (cY + 120) - (j * 30));
            c.lineTo((cX - 85) + (i * 30), (cY + 120) - (j * 30));
        }
        //c.rect((cX - 100) + (i * 30), (cY + 135) - (j * 30), 30, 15);
        //if ( j != 6 ) {
        //c.rect((cX - 85) + (i * 30), (cY + 120) - (j * 30), 30, 15);
        //}
        c.strokeStyle = white;
        c.stroke();
        c.closePath();
    }
}

// chimney
c.beginPath();
c.rect(cX + 60, cY - 120, 20, 50);
c.rect(cX + 50, cY - 130, 40, 10);
c.fillStyle = blue;
c.fill();
c.closePath();

// window lines
c.beginPath();
c.moveTo(cX - 70, cY);
c.lineTo(cX - 30, cY);
c.moveTo(cX - 50, cY - 20);
c.lineTo(cX - 50, cY + 20);
c.strokeStyle = blue;
c.lineWidth = 2;
c.stroke();
c.closePath();

canvas[0] = c;

