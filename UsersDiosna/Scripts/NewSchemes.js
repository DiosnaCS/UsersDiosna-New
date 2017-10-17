$(document).ready(function reload() {
    setInterval("autoreload()", 10000);
});
function autoreload() {
    getValues();
    setInterval("autoreload()", 10000);
}
class Value {
    constructor(tableName, columnName) {
        this.tableName = tableName;
        this.columnName = columnName;
    }
};
function getValues() {    
    var nameValues = [];
    $('.value').each(function (index) {
        var name = $(this).attr('id').split(".");
        var tableName = name[0];
        var columnName = name[1];
        nameValues[index] = new Value(tableName, columnName);
    });
    var jsonData;
    
    console.log("values:")
    console.log(nameValues);
    var requestJSON = JSON.stringify(nameValues);
    console.log(requestJSON);
    $.ajax({
        url: 'SchemeEditor/getData',
        async: true,
        dataType: 'json',
        type: 'post',
        data: requestJSON,
        success: function (data, textStatus) {
            jsonData = data; 
            console.log("Done:")
            console.log(jsonData);
            setValues(jsonData);
        }
    });   
}

function setValues(values) {
    console.log("set value");
    console.log(values);
    console.log(values.length);
    for (var i = 0; i < values.length; i++) {
        console.log("in for");
        var id = values[i].tableName + "." + values[i].columnName;
        console.log("setting values:");
        console.log(id);
        console.log(values[i].value);
        var value = Math.round(values[i].value * 100) / 100;
        document.getElementById(id).textContent = value;
        console.log(document.getElementById(id));
    }
}