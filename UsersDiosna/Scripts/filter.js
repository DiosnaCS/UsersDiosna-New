function filterSection() {
    var searchPattern = $("#searchSections").val();
    console.log(searchPattern);
    var url = '/CMS/FilterSection/' + searchPattern;
    $.ajax(
        {
            url: url,
            async: true,
            dataType: "json",
            type: "post",
            success: function (data, textStatus) {
                if (data != null) {
                    console.log(data[0]);
                    console.log(data[1]);
                    dataIntoTable(data);
                }       
            }
        });
}
function dataIntoTable(data) {
    var thNumber = 0;
    var THs = document.getElementsByTagName("th");
    for (i = 0; i < data.length; i++) {
        for (var key in data[i]) {
            if (data[i].hasOwnProperty(key)) {
                for (var thKey in THs) {
                    var th = THs[thKey];
                    if (key == th.innerText) {                        
                        console.log(th.innerText);
                        console.log(data[i][key]);
                        document.getElementsByTagName("td")[thNumber].innerText = data[i][key];
                        thNumber++;
                    }
                }
            }
        }
    }
}
function filterArticle() {
    var searchPattern = $("#searchArticle").val();
    console.log(searchPattern);
    var url = '/CMS/FilterArticle/' + searchPattern;
    $.ajax(
        {
            url: url,
            async: true,
            dataType: "json",
            type: "post",
            success: function (data, textStatus) {
                if (data != null) {
                    console.log(data[0] + " Here are data from filterArticle");
                    console.log(data[0].Id);
                }
            }
        });
}