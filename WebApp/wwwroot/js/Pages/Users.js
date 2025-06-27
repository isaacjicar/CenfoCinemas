function UserViewController() {
    this.ViewName = "User";
    this.ApiEndPontName = "User";

    this.InitView = function () {
        console.log("User init view --> ok");
        this.LoadTable();
    }

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

        // Invocamos a DataTable para llenar la tabla de usuarios más robusta
        $('#tblUsers').DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });
    }
}

$(document).ready(function () {
    var vc = new UserViewController();
    vc.InitView();
});
