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
                    console.log(data + " Here are data from filterSection");
                    console.log(data[0].Id);
                }
            }
        });
}