if ($("#cunsumptionTable").length != 0) {
    var TDs = document.getElementsByTagName("td");
    for (var i = 0; i < TDs.length; i++) {        
        if (TDs[i].innerText == "0") {
            TDs[i].innerText = "";
        }
    }
}