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

        var columns = [
            { 'data': 'id' },
            { 'data': 'userCode' },
            { 'data': 'name' },
            { 'data': 'email' },      // Asegúrate que la propiedad se llama así
            { 'data': 'birthDate' },
            { 'data': 'status' }
        ];

        $("#tblUsers").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });
    }
}

$(document).ready(function () {
    var vc = new UserViewController();
    vc.InitView();
});
