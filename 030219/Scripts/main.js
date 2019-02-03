$(document).ready(function () {

    $("select[name='BrendId']").change(function () {
        var req_data = {
            brendid: $(this).val()
        };


        $.ajax({
            url: "/home/getmodelsjr",
            type: "get",
            dataType: "json",
            data: req_data,
            success: function (response) {
                $("select[name='ModelId']").empty();

                $.each(response, function (key, value) {
                    $("select[name='ModelId']").append(`<option value="${value.Id}">${value.Name}</option>`);
                });

                //for (var i = 0; i < response.length; i++) {
                //    $("select[name='ModelId']").append(`<option value="${response[i].Id}">${response[i].Name}</option>`)
                //}
            },
            error: function (err) {
                alert(err.responseJSON.message);
            }
        });
    });
   

    if ($(".student-actions").length) {

        // get student list
        $.ajax({
            url: "/students/list",
            type: "get",
            dataType: "json",
            success: function (response) {
                $(".studentList tbody").empty();
                $.each(response, function (key, student) {
                    var tr = `<tr data-id="${student.Id}">
                                <td>${student.Name}</td>
                                <td>${student.Surname}</td>
                                <td>${student.Group}</td>
                                <td>
                                    <a class="edit" href="#"><i class="fas fa-edit"></i></a>
                                    <a class="delete" href="#"><i class="fas fa-trash"></i></a>
                                </td>
                            </tr>`;

                    $(".studentList tbody").append(tr);
                });

                $(".loader").addClass("d-none");
                $(".studentList").removeClass("d-none");
            },
            error: function (err) {
                alert("Yeniden sinayin");     
            }
        });

        // modal opening
        $('#student-modal').on('show.bs.modal', function (e) {
            getGroups();
        });

        // Modal closed
        $('#student-modal').on('hidden.bs.modal', function (e) {
            if ($(this).data("type") === "update") {
                $("input[name='Name']").val("");
                $("input[name='Surname']").val("");

                $("#student-modal .modal-title").text("Yeni Tələbə");

                $("#student-modal #modal-action").text("Əlavə et");

                $("#student-form").data("type", "create");
                $("#student-form").removeData("id");
                $(this).data("type", "create");
            }
        });

        $("#modal-action").click(function () {
            $("#student-form").submit();
        });

        // Create Student
        $("#student-form").submit(function (ev) {
            ev.preventDefault();

            if ($(this).data("type") === "create") {
                $.ajax({
                    url: "/students/create",
                    type: "post",
                    dataType: "json",
                    data: $(this).serialize(),
                    success: function (response) {
                        var tr = `<tr data-id="${response.Id}">
                                <td>${response.Name}</td>
                                <td>${response.Surname}</td>
                                <td>${response.Group}</td>
                                <td>
                                    <a class="edit" href="#"><i class="fas fa-edit"></i></a>
                                    <a class="delete" href="#"><i class="fas fa-trash"></i></a>
                                </td>
                            </tr>`;

                        $(".studentList tbody").append(tr);

                        $('#student-modal').modal('hide');

                        toastr.success("Tələbə yaradıldı");
                    },
                    error: function (err) {
                        $.each(err.responseJSON, function (key, value) {
                            toastr.warning(value);
                        });
                    }
                });
            } else {
                $id = $(this).data("id");
               
                $.ajax({
                    url: "/students/edit/"+$id,
                    type: "post",
                    dataType: "json",
                    data: $(this).serialize(),
                    success: function (response) {

                        var tr = $("tr[data-id='" + response.Id + "']");
                        tr.find("td").eq(0).text(response.Name);
                        tr.find("td").eq(1).text(response.Surname);
                        tr.find("td").eq(2).text(response.Group);

                        $('#student-modal').modal('hide');

                        toastr.success("Tələbə yeniləndi");
                    },
                    error: function (err) {
                        $.each(err.responseJSON, function (key, value) {
                            toastr.warning(value);
                        });
                    }
                });
            }
            
        });

        // Delete student
        $(document).on("click", ".delete", function (ev) {
            ev.preventDefault();

            var $id = $(this).parents("tr").data("id");

            swal({
                title: "Tələbə silmək",
                text: "Silməyə əminsiniz mi?",
                icon: "warning",
                buttons: {
                    cancel: {
                        text: "Xeyr",
                        value: false,
                        visible: true,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Bəli",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                }

            }).then((value) => {
                if (value) {
                    $.ajax({
                        url: "/students/delete/" + $id,
                        type: "get",
                        dataType: "json",
                        success: function (response) {
                            $("tr[data-id='" + $id + "']").remove();
                            toastr.success("Tələbə silindi");
                        },
                        error: function (err) {
                            toastr.warning(err.responseJSON.message);
                        }
                    });
                }
            });
        });

        // Open Update Modal
        $(document).on("click", ".edit", function (ev) {
            ev.preventDefault();
            var $id = $(this).parents("tr").data("id");

            $.getJSON("/students/details/" + $id, function (response) {
                $("input[name='Name']").val(response.Name);
                $("input[name='Surname']").val(response.Surname);

                $("#student-modal .modal-title").text(response.Name + " " + response.Surname);

                $("#student-modal #modal-action").text("Yenilə");

                $("#student-form").data("type", "update");
                $("#student-form").data("id", $id);

                $('#student-modal').modal('show');
                $('#student-modal').data("type", "update");
                setTimeout(function () {
                    $("select[name='GroupId']").val(response.Group.Id);
                }, 500);


            });
        });
    }

    // get groups
    function getGroups() {
        $.ajax({
            url: "/students/groups",
            type: "get",
            dataType: "json",
            success: function (response) {
                $("select[name='GroupId']").empty();
                $.each(response, function (key, group) {
                    var opt = `<option value="${group.Id}">${group.Name}</option>`;

                    $("select[name='GroupId']").append(opt);
                });
            },
            error: function (err) {
                alert("Yeniden sinayin");
            }
        });
    }
});