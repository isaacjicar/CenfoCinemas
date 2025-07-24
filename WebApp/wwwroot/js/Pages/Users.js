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

        $('#btnBuscarId').click(() => {
            this.searchById();
        });

        $('#btnRetrieveByEmail').click(() => {
            this.searchByEmail();
        });

        $('#btnRetrieveByUserCode').click(() => {
            this.searchByUserCode();
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
            fillUserForm(userDTO);
        });
    };

    this.create = function () {
        var userDTO = this.getCreateDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Create";

        ca.PostToAPI(urlService, userDTO, function () {
            $('#tblUsers').DataTable().ajax.reload();
        });
    };

    this.update = function () {
        var userDTO = this.getUpdateDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Update";

        ca.PutToAPI(urlService, userDTO, function () {
            $('#tblUsers').DataTable().ajax.reload();
        });
    };

    this.delete = function () {
        var userDTO = this.getUpdateDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Delete?id=" + userDTO.id;

        ca.DeleteToAPI(urlService, userDTO, function () {
            $('#tblUsers').DataTable().ajax.reload();
        });
    };

    this.getCreateDTO = function () {
        return {
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

    this.getUpdateDTO = function () {
        return {
            id: parseInt($('#txtId').val()),
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

    this.searchById = function () {
        const id = $('#txtbuscarId').val();
        if (!id) {
            alert("Ingrese un ID para buscar");
            return;
        }

        const ca = new ControlActions();
        const urlService = this.ApiEndPontName + "/RetrieveById?id=" + id;

        ca.GetToApi(urlService, function (userDTO) {
            fillUserForm(userDTO);
        });
    };

    this.searchByEmail = function () {
        const email = $('#txtRetrieveByEmail').val().trim();
        if (!email) {
            alert("Ingrese un email para buscar");
            return;
        }

        const ca = new ControlActions();
        const urlService = this.ApiEndPontName + "/RetrieveByEmail?email=" + encodeURIComponent(email);
        console.log("➡️ Email URL: ", urlService); // ✅ Verifica esto

        ca.GetToApi(urlService, function (userDTO) {
            console.log("✅ Resultado recibido: ", userDTO);

            // Detectar si viene como { data: user } o directamente
            const data = userDTO?.data || userDTO;

            if (!data || !data.id) {
                alert("Usuario no encontrado.");
                return;
            }

            fillUserForm(data);
        });


    };


    this.searchByUserCode = function () {
        const code = $('#txtRetrieveByUserCode').val().trim();
        if (!code) {
            alert("Ingrese un código de usuario para buscar");
            return;
        }

        const ca = new ControlActions();
        const urlService = this.ApiEndPontName + "/RetrieveByUserCode?userCode=" + encodeURIComponent(code);

        ca.GetToApi(urlService, function (userDTO) {
            console.log("✅ Resultado recibido: ", userDTO);

            // Detectar si viene como { data: user } o directamente
            const data = userDTO?.data || userDTO;

            if (!data || !data.id) {
                alert("Usuario no encontrado.");
                return;
            }

            fillUserForm(data);
        });


    };
}

// ✅ Función fuera del controlador
function fillUserForm(userDTO) {
    $('#txtId').val(userDTO.id);
    $('#txtUserCode').val(userDTO.userCode);
    $('#txtName').val(userDTO.name);
    $('#txtEmail').val(userDTO.email);
    $('#txtStatus').val(userDTO.status);

    if (userDTO.birthDate) {
        const onlyDate = userDTO.birthDate.split("T");
        $('#txtBDate').val(onlyDate[0]);
    } else {
        $('#txtBDate').val("");
    }

    if (userDTO.password) {
        $('#txtPassword').val(userDTO.password);
    } else {
        $('#txtPassword').val("");
    }
}

$(document).ready(function () {
    var vc = new UserViewController();
    vc.InitView();
});
