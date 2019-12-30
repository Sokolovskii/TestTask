function Render() {
    var request = $.ajax({
        type: 'GET',
        url: 'api/person',
    });

    request.done(function (data) {
        $("#Table").html("<tr>"
            + "<th>Имя</th>"
            + "<th>Должность</th>"
            + "<th>Номер сотрудника</th>"
            + "<th>Отдел</th>"
            + "<th>Руководитель сотрудника</th>"
            + "<th></th>"
            + "</tr>");
        $.each(data, function (index, person) {
            var managerNumber = 0;
            var manager = "";

            if (person.manager != null) {
                manager = person.manager.name;
                managerNumber = person.manager.tableNumber;
            }
            
            $("#Table").append("<tr>"
                + "<td><a href='#Sub' style='color:black; text-decoration:none' class='Subordination' data-id='"
                + person.id + "'>"
                + person.name + "</a></td>"
                + "<td>" + person.position + "</td>"
                + "<td>" + person.tableNumber + "</td>"
                + "<td>" + person.order + "</td>"
                + "<td>" + manager + "</td>"
                + "<td>" + "<button class= 'Delete' data-id=" + person.id + ">"
                + "<img src = '/css/deletebutton.png' width = '32' height = '32' />"
                + "</button > " + "</td > "
                + "<td>" + "<button class= 'Update' data-id= '" + person.id
                + "' data-name= '" + person.name
                + "' data-position= '" + person.position
                + "' data-tableNumber= '" + person.tableNumber
                + "' data-order= '" + person.order
                + "' data-managerNumber= '" + managerNumber+ "' ><img src = '/css/updatebutton.png' width = '32' height = '32' />"
                + "</button>" + "</td > "
                + "</tr>");
            
        });
        $(".Delete").click(function () {
            var id = $(this).attr("data-id");
            Delete(id);
        });
       
        $(".Update").click(function () {
            var id = $(this).attr("data-id");
            $("#idModalUpdate").val(id);
            var name = $(this).attr("data-name");
            $("#nameModalUpdate").val(name);
            var position = $(this).attr("data-position");
            $("#positionModalUpdate").val(position);
            var tableNumber = $(this).attr("data-tableNumber");
            $("#tableNumberModalUpdate").val(tableNumber);
            var order = $(this).attr("data-order");
            $("#orderModalUpdate").val(order);
            var managerNumber = $(this).attr("data-managerNumber");
            $("#managerNumberModalUpdate").val(managerNumber);
            $("#UpdateModal").modal();
            $("#UpdateSubmit").click(function () {
                name = document.getElementById("nameModalUpdate").value;
                position = document.getElementById("positionModalUpdate").value;
                tableNumber = document.getElementById("tableNumberModalUpdate").value;
                order = document.getElementById("orderModalUpdate").value;
                managerNumber = document.getElementById("managerNumberModalUpdate").value;
                Update(id, name, position, tableNumber, order, managerNumber);
            })
            
        });

        $(".Subordination").click(function () {
            var id = $(this).attr("data-id");
            ShowSubordination(id);
            $("#SubordinationTable").html("");
        });
    });
}

$("#Add").click(function () {
    $("#AddModal").modal();
    $("#AddSubmit").click(function () {
        name = document.getElementById("nameModalAdd").value;
        position = document.getElementById("positionModalAdd").value;
        tableNumber = document.getElementById("tableNumberModalAdd").value;
        order = document.getElementById("orderModalAdd").value;
        managerNumber = document.getElementById("managerNumberModalAdd").value;
        Add(name, position, tableNumber, order, managerNumber);
        $("#nameModalAdd").val("");
        $("#positionModalAdd").val("");
        $("#tableNumberModalAdd").val("");
        $("#orderModalAdd").val("");
        $("#managerNumberModalAdd").val("");
    });
});

function ShowSubordination(id) {
    var request = $.ajax({
        type: 'GET',
        url: 'api/person/Managers',
        data: {id:id}
    });

    request.done(function (data) {
        var count = data.length - 1;
        if (count == 0) {
            $("#SubordinationTable").append("<tr><td style='border-color:white; text-align:center; font-weight:400'>"
                + "У этого человека отсутствует руководитель"
                + "</td></tr>");
        }
        else {
            $.each(data, function (index, manager) {
                if (index == count) {
                    $("#SubordinationTable").append("<tr><td style='border-color:white; text-align:center; font-weight:700'>"
                        + manager.name + " ("
                        + manager.position + ")</td></tr>");
                }
                else {
                    $("#SubordinationTable").append("<tr><td style='border-color:white; text-align:center'>"
                        + manager.name + " ("
                        + manager.position + ")</td></tr>"
                        + "<tr><td style='border-color:white; text-align:center'>"
                        + "<img src = '/css/downarrow.png' width = '32' height = '32' /></td></tr>");
                }

            });
        }

        $("#SubordinationModal").modal('show');

    });
}

function Add(name, position, tableNumber, order, managerNumber) {
    var ajaxPost = $.ajax({
        type: 'POST',
        url: 'api/person/Add',
        data: { name: name, position: position, tableNumber: tableNumber, order: order, managerTableNumber: managerNumber }
    });
    ajaxPost.done(function (data) {
        if (data) {
            Render();
            $("#AddModal").modal('hide');
        }
        else {
            alert("Произошла ошибка");
        }

    });
}

function Delete(id) {
    var ajaxPost = $.ajax({
        type: 'POST',
        url: 'api/person',
        data: { id: id },
    });
    ajaxPost.done(function (data) {
        if (data) {
            Render()
        }
        else {
            alert("Произошла ошибка")
        }
    });
}

function Update(id, name, position, tableNumber, order, managerNumber) {

    var ajaxPost = $.ajax({
        type: 'POST',
        url: 'api/person/Update',
        data:{ id: id, name: name, position: position, tablenumber: tableNumber, order: order, managerTableNumber: managerNumber },
    });
    ajaxPost.done(function (data) {
        if (data) {
            Render();
            $("#UpdateModal").modal('hide');
        }
        else {
            alert("Произошла ошибка");
        }
    })
}

$("#HideTable").click(function () {
    $("#Table").hide();
    $("#CreateTable").show();
    $("#HideTable").hide();
    $("#CreateExcelTable").hide();
    $("#Add").hide();
});

$("#CreateTable").click(function () {
    Render();
    $("#CreateTable").hide();
    $("#HideTable").show();
    $("#CreateExcelTable").show();
    $("#Table").show();
    $("#Add").show();
});

$("#CreateExcelTable").click(function () {
    window.location("api/person/excel");
});
