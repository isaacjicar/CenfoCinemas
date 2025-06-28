function UserViewController() {
    this.ViewName = "User";
    this.ApiEndPontName = "User";

    this.InitView = function () {
        console.log("User init view --> ok");
        this.LoadTable();

        $('#btnCreate').click(() => {
            this.create();
        });

        $('#btnUpdate').click(() => {
            this.update();
        });

        $('#btnDelete').click(() => {
            this.delete();
        });
    };

    this.LoadTable = function () {
        var ca = new ControlActions();
        var service = this.ApiEndPontName + "/RetrieveAll";
        var urlService = ca.GetUrlApiService(service);

        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'userCode' }
        columns[2] = { 'data': 'name' }
        columns[3] = { 'data': 'email' }
        columns[4] = { 'data': 'birthDate' }
        columns[5] = { 'data': 'status' }

        $('#tblUsers').DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });

        $('#tblUsers tbody').on('click', 'tr', function () {
            var row = $(this).closest('tr');

            var userDTO = $('#tblUsers').DataTable().row(row).data();

            $('#txtId').val(userDTO.id);
            $('#txtUserCode').val(userDTO.userCode);
            $('#txtName').val(userDTO.name);
            $('#txtEmail').val(userDTO.email);
            $('#txtStatus').val(userDTO.status);

            var onlyDate = userDTO.birthDate.split("T");
            $('#txtBDate').val(onlyDate[0]);
        });
    };

    this.create = function () {
        var userDTO = this.getDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Create";

        ca.PostToAPI(urlService, userDTO, function () {
            $('#tblUsers').DataTable().ajax.reload();
        });
    };

    this.update = function () {
        var userDTO = this.getDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Update";

        ca.PutToAPI(urlService, userDTO, function () {
            $('#tblUsers').DataTable().ajax.reload();
        });
    };

    this.delete = function () {
        var userDTO = this.getDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Delete?id=" + userDTO.id;

        ca.DeleteToAPI(urlService, userDTO, function () {
            $('#tblUsers').DataTable().ajax.reload();
        });
    };


    this.getDTO = function () {
        return {
            id: $('#txtId').val(),
            created: "2025-01-01",
            updated: "2025-01-01",
            userCode: $('#txtUserCode').val(),
            name: $('#txtName').val(),
            email: $('#txtEmail').val(),
            status: $('#txtStatus').val(),
            birthDate: $('#txtBDate').val(),
            password: $('#txtPassword').val()
        };
    };
}

$(document).ready(function () {
    var vc = new UserViewController();
    vc.InitView();
});
