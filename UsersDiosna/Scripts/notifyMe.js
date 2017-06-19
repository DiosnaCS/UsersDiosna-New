function notifyMe(notif, notifTitle, id) {
    if (Notification.permission !== "granted")
        Notification.requestPermission();
    else {
        var notification = new Notification(notifTitle, {
            icon: 'http://icons.iconarchive.com/icons/fasticon/coffee-break/128/bread-icon.png',
            body: notif,
        });

        notification.onclick = function () {
            window.open("/Notification/turnOff/" + id);
        };
    }
}