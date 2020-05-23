function loadItems(pageNum, controllerName) {
    $.ajax({
        type: 'GET',
        url: '/' + controllerName + '/GetItems?startRow=' + pageNum,
        success: function (data, textstatus) {
            $("#items").html(data);
        }
    });
}