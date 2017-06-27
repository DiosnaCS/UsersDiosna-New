function filterSection() {
    var url = '/CMS/FilterSection/' + searchPattern;
    $.ajax({
        url: url,
        async: true,
        dataType: "json",
        type: "post",
        success: function (data, textStatus) {
            console.log(data + " Here are data from filterSection");
        }
}