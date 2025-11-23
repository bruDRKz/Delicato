function mostrarLoaderSwal() {
    Swal.fire({
        title: 'Carregando...',
        html: '<div class="loader"></div>',
        showConfirmButton: false,
        allowOutsideClick: false,
        background: '#222',
        customClass: {
            popup: 'swal2-loader-popup'
        }
    });
}

function esconderLoaderSwal() {
    Swal.close();
}