function notification() {
    var time = 60000;
    var url = '/Notification/getNotifications';
    console.log("In notification");
    $.ajax({
        url: url,
        async: true,
        dataType: "json",
        type: "post",
        success: function (data, textStatus) {
            if (data[0] != null) { //test the presence of notification
                if (data[0]["Id"] != null) {
                    var html;
                    time = 104000;
                    $('#bell ').css("color", "red");
                    for (var i = 0; i < data.length; i++) {
                        id = data[i]["Id"];
                        var projectName = data[i]["ProjectName"];
                        var tags = data[i]["Tags"];
                        var rest = tags + '\n' + data[i]["Detail"]; 
                        html += '<li><a href="/Notification/turnOff/' + id + '">' + '<b>' + projectName + '</b>' + detail + '</a></li>';
                        notifyMe(rest, data[i]["ProjectName"], id);
                    }
                    $('#notification').html(html);
                } else {
                    $('#bell').css("color", "white");
                }
                console.log(time);
                setInterval("notification()", time);
            }
        }
    });
}