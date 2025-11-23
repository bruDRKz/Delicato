const modalLogin = document.getElementById("modalLogin");
const abrirModalLogin = document.getElementById("abrirModalLogin");
const fecharModalLogin = document.getElementById("fecharModalLogin");
const loginForm = document.querySelector(".login-form");

// Função para verificar se o usuário já está logado na sessão
function getUsuarioID() {
    return sessionStorage.getItem("usuarioId");
}

// Ao clicar para abrir o modal, verifica se já está logado na sessão
abrirModalLogin.onclick = () => {
    if (getUsuarioID()) {
        // Usuário já está logado na aba, dispara busca de reservas direto
        buscarReservas(getUsuarioID());
        return;
    }
    // Limpa campos
    document.getElementById("inputUsuarioLogin").value = "";
    document.getElementById("inputSenhaLogin").value = "";
    modalLogin.style.display = "flex";
};

fecharModalLogin.onclick = () => modalLogin.style.display = "none";

// Fecha clicando fora
window.onclick = (e) => {
    if (e.target === modalLogin) modalLogin.style.display = "none";
};
// Fecha no ESC
window.addEventListener("keydown", function (e) {
    if (e.key === "Escape" && modalLogin.style.display === "flex") {
        modalLogin.style.display = "none";
    }
});

// Submit do login adaptado para sessionStorage
loginForm.addEventListener("submit", function (e) {
    e.preventDefault();
    const telefone = document.getElementById("inputUsuarioLogin").value;
    const senha = document.getElementById("inputSenhaLogin").value;

    const formData = new FormData();
    formData.append("telefone", telefone);
    formData.append("senha", senha);

    fetch("/Usuario/AutenticarUsuario", {
        method: "POST",
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data.sucesso) {
                sessionStorage.setItem("usuarioId", data.id);
                if (data.admin) {
                    sessionStorage.setItem("isAdmin", "true");
                } else {
                    sessionStorage.removeItem("isAdmin");
                }
                modalLogin.style.display = "none";
                if (data.admin) {
                    // Redireciona ao admin
                    window.location.href = "/Home/Administrativo";
                } else {
                    buscarReservas(data.id);
                }
            } else {
                Swal.fire({
                    title: 'Erro ao autenticar!',
                    text: 'Usuário e/ou senha incorretos.',
                    icon: 'error',
                    background: '#18181b',
                    color: '#facc15',
                    confirmButtonColor: '#facc15',
                    customClass: { title: 'swal-title-custom' }
                });
            }
        })
        .catch(() => {
            Swal.fire({
                title: 'Erro!',
                text: 'Não foi possível conectar ao servidor.',
                icon: 'error',
                background: '#18181b',
                color: '#facc15',
                confirmButtonColor: '#facc15',
                customClass: { title: 'swal-title-custom' }
            });
        });
});

//Busco as reservas e populo o modal
function buscarReservas(idUsuario) {
    const modalListaReservas = document.getElementById("modalListaReservas");
    const listaConteudo = document.getElementById("listaReservasConteudo");

    listaConteudo.innerHTML = "";

    const formData = new FormData();
    formData.append("idUsuario", idUsuario);

    fetch("/Reservas/ObterReservasPorUsuario", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(reservas => {
            if (!reservas || reservas.length === 0) {
                listaConteudo.innerHTML = `
                <div class="reserva-item">
                    <div class="reserva-info">
                        <span class="reserva-titulo">Nenhuma reserva encontrada.</span>
                    </div>
                </div>`;
            } else {
                reservas.forEach(r => {
                    // aqui considerando camelCase vindo da API:
                    // idReserva, nomeNaReserva, dataReserva, horaReserva, numeroPessoas, contato, reservaConcluida
                    const item = document.createElement("div");
                    item.classList.add("reserva-item");

                    const statusTexto = r.reservaConcluida ? "Concluída" : "Pendente";

                    item.innerHTML = `
                                     <div class="reserva-info">
                                         <span class="reserva-titulo"><strong>Nome:</strong> ${r.nomeNaReserva}</span>
                                         <span><strong>Data:</strong> ${formatarData(r.dataReserva)}</span>
                                         <span><strong>Hora:</strong> ${r.horaReserva}</span>
                                         <span><strong>Pessoas:</strong> ${r.numeroPessoas}</span>
                                         <span><strong>Contato:</strong> ${r.contato}</span>
                                         <span id="spanStatusReserva"><strong>Status:</strong> ${statusTexto}</span>
                                     </div>
                                     <div class="reserva-acoes">
                                         ${!r.reservaConcluida ? `
                                             <button id="cancelarReserva" class="btn-cancelar" data-id="${r.idReserva}" >Cancelar</button>
                                             <button id="concluirReserva" class="btn-concluir" data-id="${r.idReserva}">Concluir</button>
                                         ` : ""}
                                     </div>
`;                  

                    listaConteudo.appendChild(item);                    
                });

                // Listeners para ações (implemente as funções abaixo conforme sua controller)
                listaConteudo.querySelectorAll(".btn-cancelar").forEach(btn => {
                    btn.addEventListener("click", () => {
                        const idReserva = btn.getAttribute("data-id");
                        cancelarReserva(idReserva, idUsuario);
                    });
                });

                listaConteudo.querySelectorAll(".btn-concluir").forEach(btn => {
                    btn.addEventListener("click", () => {
                        const idReserva = btn.getAttribute("data-id");
                        concluirReserva(idReserva, idUsuario);
                    });
                });

            }

            modalListaReservas.style.display = "flex";
        })
        .catch(() => {
            listaConteudo.innerHTML = `
            <div class="reserva-item">
                <div class="reserva-info">
                    <span class="reserva-titulo">Erro ao carregar reservas.</span>
                </div>
            </div>`;
            modalListaReservas.style.display = "flex";
        });
}

function formatarData(dataIso) {
    const [ano, mes, dia] = dataIso.split("-");
    return `${dia}/${mes}/${ano}`;
}



//Eventos da reserva: Concluir e Cancelar
function concluirReserva(idReserva, idUsuario) {
    Swal.fire({
        title: 'Deseja concluir esta reserva?',
        text: 'Após concluir, a reserva será marcada como finalizada.',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Concluir',
        cancelButtonText: 'Cancelar',
        background: '#18181b',
        color: '#facc15',
        confirmButtonColor: '#efb73d',   
        cancelButtonColor: '#404046',    
        customClass: {
            title: 'swal-title-custom',
            confirmButton: 'swal-confirm-custom',
            cancelButton: 'swal-cancel-custom'
        }
    }).then((result) => {
        if (result.isConfirmed) {
            const formData = new FormData();
            formData.append("idReserva", idReserva);
            fetch('/Reservas/ConcluirReserva', {
                method: 'PUT',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    if (data.sucesso) {
                        Swal.fire({
                            title: data.mensagem,
                            icon: 'success',
                            background: '#18181b',
                            color: '#facc15',
                            confirmButtonColor: '#facc15',
                            customClass: { title: 'swal-title-custom' }
                        });

                        buscarReservas(idUsuario);
                    }
                })
                .catch(() => {
                    Swal.fire({
                        title: 'Erro!',
                        text: 'Erro ao concluir reserva.',
                        icon: 'error',
                        background: '#18181b',
                        color: '#facc15',
                        confirmButtonColor: '#facc15'
                    });
                });
        }
    });
}

function cancelarReserva(idReserva, idUsuario) {
    Swal.fire({
        title: 'Deseja cancelar esta reserva?',
        text: 'Esta ação irá excluir sua reserva permanentemente.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Cancelar Reserva',
        cancelButtonText: 'Voltar',
        background: '#18181b',
        color: '#facc15',
        confirmButtonColor: '#404046',   
        cancelButtonColor: '#efb73d',    
        customClass: {
            title: 'swal-title-custom',
            confirmButton: 'swal-cancel-custom',
            cancelButton: 'swal-confirm-custom'
        }
    }).then((result) => {
        if (result.isConfirmed) {
            const formData = new FormData();
            formData.append("idReserva", idReserva);
            fetch('/Reservas/CancelarReserva', {    
                method: 'DELETE',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    if (data.sucesso) {
                        Swal.fire({
                            title: data.mensagem,
                            icon: 'success',
                            background: '#18181b',
                            color: '#facc15',
                            confirmButtonColor: '#facc15',
                            customClass: { title: 'swal-title-custom' }
                        });

                        buscarReservas(idUsuario);
                    }
                })
                .catch(() => {
                    Swal.fire({
                        title: 'Erro!',
                        text: 'Erro ao cancelar reserva.',
                        icon: 'error',
                        background: '#18181b',
                        color: '#facc15',
                        confirmButtonColor: '#facc15'
                    });
                });
        }
    });
}
