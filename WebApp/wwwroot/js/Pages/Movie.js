function MovieViewController() {
    this.ViewName = "Movie";
    this.ApiEndPontName = "Movie";

    this.InitView = function () {
        console.log("Movie init view --> ok");
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
    }

    this.LoadTable = function () {
        var ca = new ControlActions();
        var service = this.ApiEndPontName + "/RetrieveAll";
        var urlService = ca.GetUrlApiService(service);

        var columns = [];
        columns[0] = { 'data': 'id' },  
        columns[1] = { 'data': 'title' },
        columns[2] = { 'data': 'description' },
        columns[3] = { 'data': 'genre' },
        columns[4] = { 'data': 'releaseDate' },
        columns[5] = { 'data': 'director' }

   

        $('#tblMovies').DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });
        $('#tblMovies tbody').on('click', 'tr', function () {
            var row = $(this).closest('tr');

            var movieDTO = $('#tblMovies').DataTable().row(row).data();

            $('#txtId').val(movieDTO.id);
            $('#txttitle').val(movieDTO.title);
            $('#txtdescription').val(movieDTO.description);
            $('#txtgenre').val(movieDTO.genre);
            $('#txtdirector').val(movieDTO.director);

            var olnyDate = movieDTO.releaseDate.split("T");
            $('#txtreleaseDate').val(olnyDate[0]);
        });

    };

    this.create = function () {
        var movieDTO = this.getCreateDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Create";

        ca.PostToAPI(urlService, movieDTO, function () {
            $('#tblMovies').DataTable().ajax.reload();
        });
    };

    this.update = function () {
        var movieDTO = this.getUpdateDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Update";

        ca.PutToAPI(urlService, movieDTO, function () {
            $('#tblMovies').DataTable().ajax.reload();
        });
    };

    this.delete = function () {
        var movieDTO = this.getUpdateDTO();

        var ca = new ControlActions();
        var urlService = this.ApiEndPontName + "/Delete?id=" + movieDTO.id;

        ca.DeleteToAPI(urlService, movieDTO, function () {
            $('#tblMovies').DataTable().ajax.reload();
        });
    };

    this.getCreateDTO = function () {
        return {
            title: $('#txttitle').val(),
            description: $('#txtdescription').val(),
            releaseDate: $('#txtreleaseDate').val(),
            genre: $('#txtgenre').val(),
            director: $('#txtdirector').val()
        };
    };

    this.getUpdateDTO = function () {
        return {
            id: parseInt($('#txtId').val()),
            title: $('#txttitle').val(),
            description: $('#txtdescription').val(),
            releaseDate: $('#txtreleaseDate').val(),
            genre: $('#txtgenre').val(),
            director: $('#txtdirector').val()
        };
    };



}

$(document).ready(function () {
    var vc = new MovieViewController();
    vc.InitView();
});
