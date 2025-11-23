const modal = document.getElementById("modalAvaliacao");
const abrir = document.getElementById("abrirModal");
const fechar = document.getElementById("fecharModal");

window.addEventListener("keydown", function (e) {
    if (e.key === "Escape" && modal.style.display === "flex") {
        modal.style.display = "none";
    }
});
abrir.onclick = () => {
    modal.style.display = "flex";
    limparCamposAvaliacao();
}
fechar.onclick = () => modal.style.display = "none";

// Fecha clicando fora do conteúdo
window.onclick = (e) => {
    if (e.target === modal) modal.style.display = "none";
};

const estrelas = document.querySelectorAll('#estrelas-avaliacao span');

estrelas.forEach((estrela, i) => {
    estrela.addEventListener('click', function () {
        // Marca todas até a selecionada
        estrelas.forEach((s, j) => {
            if (j <= i) {
                s.classList.add('selecionada');
            } else {
                s.classList.remove('selecionada');
            }
        });
        // Salva o valor no input escondido
        inputNotaAvaliacao().value = estrela.getAttribute('data-value');
    });
});

//Hover interativo nas estrelas, so visual
estrelas.forEach((estrela, i) => {
    estrela.addEventListener('mouseover', function () {
        estrelas.forEach((s, j) => {
            s.classList.toggle('selecionada', j <= i);
        });
    });
    estrela.addEventListener('mouseout', function () {
        estrelas.forEach((s, j) => {
            s.classList.toggle('selecionada', j < notaInput.value);
        });
    });
});


document.querySelector("#modalAvaliacao form").addEventListener("submit", function (e) {
    e.preventDefault();
    const nome = inputNomeAvaliacao().value;
    const nota = inputNotaAvaliacao().value;
    const comentario = textAreaComentarioAvaliacao().value;

    const formData = new FormData();
    formData.append('nome', nome);
    formData.append('nota', nota);
    formData.append('comentario', comentario);

    fetch('/Avaliacao/EnviarAvaliacao', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data.sucesso) {
                Swal.fire({
                    title: 'Avaliação enviada!',                    
                    icon: 'success',
                    showConfirmButton: false, 
                    timer: 2000,
                    background: '#18181b',
                    color: '#facc15',                    
                    customClass: {
                        title: 'swal-title-custom'
                    }
                });
                fechar.click(); 
            } else {
                Swal.fire({
                    title: 'Erro avaliar!',
                    text: data.mensagem,
                    icon: 'error',
                    background: '#18181b',
                    color: '#facc15',
                    confirmButtonColor: '#facc15',
                    cancelButtonColor: '#b91c1c',
                    customClass: {
                        title: 'swal-title-custom'
                    }
                });
            }
        });
});

//Precisa limpar as estrelas tambem
function limparCamposAvaliacao() {
    inputNomeAvaliacao().value = '';
    inputNotaAvaliacao().value = '';
    textAreaComentarioAvaliacao().value = '';
}