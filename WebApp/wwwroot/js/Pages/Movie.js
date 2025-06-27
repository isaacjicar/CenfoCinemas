function MovieViewController() {
    this.ViewName = "Movie";
    this.ApiEndPontName = "Movie";

    this.InitView = function () {
        console.log("Movie init view --> ok");
        this.LoadTable();
    }

    this.LoadTable = function () {
        var ca = new ControlActions();
        var service = this.ApiEndPontName + "/RetrieveAll";
        var urlService = ca.GetUrlApiService(service);


        var columns = [];
        columns[0] = { 'data': 'title' }
        columns[1] = { 'data': 'description' }
        columns[2] = { 'data': 'releaseDate' }
        columns[3] = { 'data': 'genre' }
        columns[4] = { 'data': 'director' }

        // Invocamos a DataTable para llenar la tabla de peliculas más robusta
        $('#tblMovies').DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });

    }
}

$(document).ready(function () {
    var vc = new MovieViewController();
    vc.InitView();
});
