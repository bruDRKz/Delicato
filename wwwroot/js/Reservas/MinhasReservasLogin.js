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
                modalLogin.style.display = "none";
                buscarReservas(data.id);
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

// Função exemplo para buscar reservas por usuário
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
                        <span><strong>Data:</strong> ${r.dataReserva}</span>
                        <span><strong>Hora:</strong> ${r.horaReserva}</span>
                        <span><strong>Pessoas:</strong> ${r.numeroPessoas}</span>
                        <span><strong>Contato:</strong> ${r.contato}</span>
                        <span><strong>Status:</strong> ${statusTexto}</span>
                    </div>
                    <div class="reserva-acoes">
                        <button class="btn-cancelar" data-id="${r.idReserva}">Cancelar</button>
                        <button class="btn-concluir" data-id="${r.idReserva}">Concluir</button>
                    </div>
                `;

                    listaConteudo.appendChild(item);
                });

                // Listeners para ações (implemente as funções abaixo conforme sua controller)
                listaConteudo.querySelectorAll(".btn-cancelar").forEach(btn => {
                    btn.addEventListener("click", () => {
                        const idReserva = btn.getAttribute("data-id");
                        // cancelarReserva(idReserva);
                    });
                });

                listaConteudo.querySelectorAll(".btn-concluir").forEach(btn => {
                    btn.addEventListener("click", () => {
                        const idReserva = btn.getAttribute("data-id");
                        // concluirReserva(idReserva);
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

