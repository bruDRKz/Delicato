// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


document.addEventListener('DOMContentLoaded', function () {
    const btnToggle = document.getElementById('btnMenuToggle');
    const navMenu = document.getElementById('navMenu');
    if (btnToggle && navMenu) {
        btnToggle.addEventListener('click', function () {
            navMenu.classList.toggle('open');
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const btnHomeADM = document.getElementById("btnHomeAdm");
    //if (btnHome) {
    //    if (sessionStorage.getItem("isAdmin") === "true") {
    //        btnHome.setAttribute("href", "/Home/Administrativo");
    //    } else {
    //        btnHome.setAttribute("href", "/Home/Index");
    //    }
    //}

    
    if (sessionStorage.getItem("isAdmin") === "true") {
        btnHomeADM.style.display = "flex";
      } else {
        btnHomeADM.style.display = "none";
      }
});


