//when click movie detail button, this function is called before modal show ...
function fillModalHtml(movie) {
    const modalBody = $('#modalMovieDetails .modal-body');
    var html = '';
    html += `<div class="card">
                         <div class="card-body">
                             <h5 class="cart-title">${movie.Title} (${movie.ReleaseYear})</h5>
                              <p class="card-text">
                                 ${movie.ImdbRate}
                                 <i class="fa fa-star text-warning"></i>
                               </p>
                               <p class="card-text" id="categories"></p>
                               <p class="card-text">${movie.StoryLine}</p>
                               <div class="row">
                                   <div class="col-md-5"><img class="img-fluid" src="/Uploads/Img/${movie.Image}"></div>
                                   <div class="col-md-7">
                                       <div class="embed-responsive embed-responsive-16by9">
                                           ${movie.Trailer === "empty" ?"<div class='alert alert-warning'>Henüz fragman yüklenmemiş</div>":movie.Trailer}
                                       </div>
                                   </div>
                               </div>
                        </div>
                     </div>`;
    modalBody.html(html);
    var categoryHtml = '';
    movie.Categories.forEach(function (category) {
        categoryHtml += `<span class="badge badge-primary mr-1">${category}</span>`;
    });
    $('#categories').html(categoryHtml);
}

//cart manager
var CartManager = function () {

    this.addMovie = function (movie) {
        let movieList = JSON.parse(localStorage.getItem('cart')) || [];
        var confirmation = true;
        if (movieList.length > 0) {
            movieList.forEach(function (item) {
                if (item.Id === movie.Id) {
                    confirmation = false;
                }
            });
        }
        if (confirmation) {
            movieList.push(movie);
            localStorage.clear();
            localStorage.setItem('cart', JSON.stringify(movieList));
        }
        return confirmation;
    };

    this.getMovies = function () {
        const movieList = JSON.parse(localStorage.getItem('cart')) || [];
        return movieList;
    };

    this.showShoppingCart = function () {
        var modalBody = $('#modalShoppingCart .modal-body');
        modalBody.html('');
        const movieList = JSON.parse(localStorage.getItem('cart')) || [];
        var html = '';
        var table = '';
        if (movieList.length === 0) {
            html += '<div class="alert alert-warning">Alışveriş sepeti boş</div>';
        } else {
            movieList.forEach(function (item) {
                table += `<tr>
                    <td>
                        <img width="50" src="/Uploads/Img/${item.Image}">
                    </td>
                    <td>${item.Title}</td>
                    <td>
                        <button class="btn btn-sm btn-danger btnRemoveFromCart" movieId="${item.Id}">
                            <i class="fa fa-times"></i>
                        </button>
                    </td>
                </tr>`;
            });

            html = `<table class="table table-hover table-bordered">${table}</table>`;
        }

        modalBody.html(html);
    };

    this.removeFromCart = function (movieId) {
        const movieList = JSON.parse(localStorage.getItem('cart')) || [];
        var deletedIndex = -1;
        if (movieList.length !== 0) {
            movieList.forEach(function (movie) {
                if (movie.Id == movieId) {
                    deletedIndex = movieList.indexOf(movie);
                }
            });
            if (deletedIndex >= 0) {
                localStorage.clear();
                movieList.splice(deletedIndex, 1);
                localStorage.setItem('cart', JSON.stringify(movieList));
            }

        }
    };

    this.confirmOrder = function () {
        var movies = this.getMovies();
        var self = this;
        if (movies.length > 0) {
            var movieIds = movies.map(x => x.Id);

            $.ajax({
                url: '/Order/ConfirmOrder',
                method: 'post',
                dataType: 'json',
                data: { movieIds: movieIds },
                success: response => {
                    swal({
                        type: 'success',
                        title: 'Sipariş Sonucu',
                        text: response.Message,
                        showConfirmButton: true,
                        confirmButtonText: 'Tamam',
                        allowOutsideClick: false
                    }).then(function () {
                        localStorage.clear();
                        self.showShoppingCart();
                    });
                },
                error: error => {
                    swal({
                        type: 'error',
                        title: 'Hata...',
                        text: error,
                        showConfirmButton: true,
                        confirmButtonText: 'Tamam',
                        allowOutsideClick: false
                    });
                }
            });

        } else {
            swal({
                type: 'warning',
                title: 'Hata...',
                text: 'Sipariş listeniz boş lütfen önce ürün seçiniz...!',
                showConfirmButton: true,
                confirmButtonText: 'Tamam',
                allowOutsideClick: false
            });
        }
    };
};

$(function () {
    $('.btnMovieDetails').click(function () {
        var movieId = $(this).attr('movieId');
        $.ajax({
            url: '/Movie/GetMovieDetails',
            method: 'post',
            dataType: 'json',
            data: { movieId: movieId },
            success: response => {
                fillModalHtml(response.Movie);
                $('#modalMovieDetails').modal('show');
            },
            error: err => {
                console.log(err);
            }
        });
    });

    //after modal hide event...
    $('#modalMovieDetails').on('hidden.bs.modal', function () {
        $(this).find('.modal-body').html('');
    });

    //Add Movie To Shopping Cart
    $('.btnAddShoppingCart').click(function () {
        var movieId = $(this).attr('movieId');
        $.ajax({
            url: '/Movie/GetMovieInfoToAddShoppingCart',
            method: 'post',
            dataType: 'json',
            data: { movieId: movieId },
            success: movie => {
                swal({
                    title: "Sepet eklensin mi?",
                    text: `${movie.Title}`,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Evet, ekle!',
                    cancelButtonText: 'Hayır, ekleme!'
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        var cartManager = new CartManager();
                        if (cartManager.addMovie(movie)) {
                            swal({
                                type: 'success',
                                title: 'İşlem Başarılı...',
                                text: `${movie.Title} sepete eklendi.`,
                                showConfirmButton: true,
                                confirmButtonText: 'Tamam',
                                allowOutsideClick: false
                            });
                        } else {
                            swal({
                                type: 'error',
                                title: 'işlem gerçekleşmedi...',
                                text: `${movie.Title} sepette zaten ekli.`,
                                showConfirmButton: true,
                                confirmButtonText: 'Tamam',
                                allowOutsideClick: false
                            });
                        }
                    }
                });
            },
            error: err => {
                console.log(err);
            }
        });
    });


    //show shopping cart
    $('#btnOpenShoppingCart').click(function () {
        var cartManager = new CartManager();
        cartManager.showShoppingCart();
        $('#modalShoppingCart').modal('show');
    });

    //remove movie from cart
    $('#modalShoppingCart').on('click', '.btnRemoveFromCart', function () {
        var movieId = $(this).attr('movieId');
        var cartManager = new CartManager();
        cartManager.removeFromCart(movieId);
        cartManager.showShoppingCart();
    });

    //submit the order
    $('#confirmOrder').click(function () {
        var cartManager = new CartManager();
        cartManager.confirmOrder();
    });
});

