const modalReserva = document.getElementById("modalReserva");
const abrirModalReserva = document.getElementById("abrirModalReserva");
const fecharModalReserva = document.getElementById("fecharModalReserva");

window.addEventListener("keydown", function (e) {
    if (e.key === "Escape" && modalReserva.style.display === "flex") {
        modalReserva.style.display = "none";
    }
});
abrirModalReserva.onclick = () => {
    modalReserva.style.display = "flex"
    limparCamposReserva();
};
fecharModalReserva.onclick = () => modalReserva.style.display = "none";
window.onclick = (e) => {
    if (e.target === modalReserva) modalReserva.style.display = "none";
};

document.querySelector("#modalReserva form").addEventListener("submit", function (e) {
    e.preventDefault();
    
    // Captura dos dados do formulário
    const nome = inputNome().value;
    const contato = inputContato().value;
    const numPessoas = selectNumPessoas().value;
    const dataReserva = inputDataReserva().value;
    const observacoes = inputObservacoes().value;


    // Monta o formData para buscar o usuário
    const formData = new FormData();
    formData.append('telefone', contato);

    // Verifica se usuário existe pelo telefone
    fetch('/Usuario/VerificarExistenciaUsuario', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            const existeUsuario = data.Existe || data.existe;
            const idUsuario = data.ID || data.Id || data.id; // o JSON sempre esta retornando minusculo, mas por precaução...
            

            if (!existeUsuario) {
                // Usuário não existe, pedir senha para cadastro
                Swal.fire({
                    title: 'Crie uma senha para consultar suas reservas',
                    input: 'password',
                    inputLabel: 'Senha',
                    inputPlaceholder: 'Digite uma senha',
                    confirmButtonText: 'Confirmar',
                    showCancelButton: true,
                    inputAttributes: {
                        minlength: 6,
                        required: true,
                        autocapitalize: 'off',
                        autocorrect: 'off'
                    },
                    background: '#18181b',
                    color: '#facc15',
                    confirmButtonColor: '#facc15',
                    cancelButtonColor: '#b91c1c',
                    customClass: {
                        title: 'swal-title-custom',
                        inputLabel: 'swal-label-custom'
                    }
                }).then((result) => {
                    if (result.isConfirmed && result.value) {
                        const formDataSenha = new FormData();
                        formDataSenha.append('nome', nome);
                        formDataSenha.append('senha', result.value.trim());
                        formDataSenha.append('telefone', contato);

                        fetch('/Usuario/AdicionarUsuario', {
                            method: 'POST',
                            body: formDataSenha
                        })
                        
                            .then(resp => resp.json())
                            .then(finalData => {
                                const finalIdUsuario = finalData.ID || finalData.Id || finalData.id;
                                const finalSucesso = finalData.sucesso || finalData.Sucesso;
                                if (finalSucesso && finalIdUsuario) {
                                    criarReserva(finalIdUsuario);
                                } else {
                                    Swal.fire({
                                        title: 'Erro!',
                                        text: 'Não foi possível cadastrar o usuário.',
                                        icon: 'error',
                                        background: '#18181b',
                                        color: '#facc15',
                                        confirmButtonColor: '#facc15'
                                    });
                                }
                            })
                            .catch(() => {
                                Swal.fire({
                                    title: 'Erro!',
                                    text: 'Erro ao cadastrar usuário.',
                                    icon: 'error',
                                    background: '#18181b',
                                    color: '#facc15',
                                    confirmButtonColor: '#facc15'
                                });
                            });
                    }
                });
            } else {
                // Usuário já existe, segue fluxo sem pedir senha
                criarReserva(idUsuario);
            }
        })
        .catch(() => {
            Swal.fire({
                title: 'Erro!',
                text: 'Não foi possível consultar usuário.',
                icon: 'error',
                background: '#18181b',
                color: '#facc15',
                confirmButtonColor: '#facc15'
            });
        });

    // Função para criar a reserva
    function criarReserva(idUsuario) {
        const formDataReserva = new FormData();
        formDataReserva.append('IdUsuario', idUsuario);
        formDataReserva.append('NomeNaReserva', nome);
        formDataReserva.append('DataReserva', dataReserva.split("T")[0]);
        formDataReserva.append('HoraReserva', dataReserva.split("T")[1]);
        formDataReserva.append('NumeroPessoas', parseInt(numPessoas));
        formDataReserva.append('ContatoReserva', contato);
        formDataReserva.append('Observacoes', observacoes);

        fetch('/Reservas/CriarReserva', {
            method: 'POST',
            body: formDataReserva
        })
            .then(res => res.json())
            .then(reservaData => {
                if (reservaData.sucesso) {
                    Swal.fire({
                        title: 'Reserva confirmada!',
                        text: reservaData.mensagem,
                        icon: 'success',
                        background: '#18181b',
                        color: '#facc15',
                        showConfirmButton: false,
                        timer: 2000,
                        customClass: {
                            title: 'swal-title-custom'
                        }
                    });
                    fecharModalReserva.click();
                } else {
                    Swal.fire({
                        title: 'Erro ao reservar!',
                        text: reservaData.mensagem,
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
    }
});

function limparCamposReserva() {
    inputNome().value = "";
    inputContato().value = "";
    selectNumPessoas().selectedIndex = 0;
    inputDataReserva().value = getDataDeHoje19h();
    inputObservacoes().value = "";
};


function getDataDeHoje19h() {
    const agora = new Date();
    const ano = agora.getFullYear();
    const mes = String(agora.getMonth() + 1).padStart(2, '0');
    const dia = String(agora.getDate()).padStart(2, '0');
    return `${ano}-${mes}-${dia}T19:00`;
}

