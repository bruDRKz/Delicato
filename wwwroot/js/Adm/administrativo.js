// Utilitário — Formata data para yyyy-mm-dd
function formatarDataPadrao(date) {
    // Para inputs tipo date (yyyy-mm-dd)
    const y = date.getFullYear();
    const m = String(date.getMonth() + 1).padStart(2, '0');
    const d = String(date.getDate()).padStart(2, '0');
    return `${y}-${m}-${d}`;
}

// Define datas padrão e faz primeira busca ao carregar
window.addEventListener('DOMContentLoaded', function () {
    const hoje = new Date();
    const daqui7 = new Date();
    daqui7.setDate(hoje.getDate() + 7);

    document.getElementById('dataInicio').value = formatarDataPadrao(hoje);
    document.getElementById('dataFim').value = formatarDataPadrao(daqui7);

    // Primeira carga automática para o intervalo padrão
    buscarReservasPorData(
        document.getElementById('dataInicio').value,
        document.getElementById('dataFim').value
    );
});

// Evento do botão para filtrar intervalo
document.querySelector('.btn-aplicar-filtros').addEventListener('click', function () {
    const dataInicio = document.getElementById('dataInicio').value;
    const dataFim = document.getElementById('dataFim').value;

    if (dataInicio && dataFim) {
        buscarReservasPorData(dataInicio, dataFim);
    }
});


//-----------------------Bloqueio de datas-------------------------
document.getElementById('btnBloquearData').addEventListener('click', function () {
    Swal.fire({
        title: 'Bloquear Data',
        html: `
            <input type="date" id="dataBloqueio" class="swal2-input" placeholder="Data a bloquear" style="margin-bottom:8px;">
            <input type="text" id="motivoBloqueio" class="swal2-input" placeholder="Motivo do bloqueio">
        `,
        showCancelButton: true,
        confirmButtonText: 'Bloquear',
        cancelButtonText: 'Cancelar',
        background: '#18181b',
        color: '#facc15',
        confirmButtonColor: '#facc15',
        cancelButtonColor: '#b91c1c',
        customClass: {
            title: 'swal-title-custom',
            popup: 'swal-popup-custom',
            confirmButton: 'swal-btn-confirm-custom',
            cancelButton: 'swal-btn-cancel-custom'
        },
        preConfirm: () => {
            const data = document.getElementById('dataBloqueio').value;
            const motivo = document.getElementById('motivoBloqueio').value;
            if (!data || !motivo) {
                Swal.showValidationMessage('Informe a data e o motivo.');
                return false;
            }
            return { data, motivo };
        }
    }).then(result => {
        if (result.isConfirmed && result.value) {
            bloquearData(result.value.data, result.value.motivo);
        }
    });
});

function bloquearData(dataBloqueio, motivoBloqueio) {
    const formData = new FormData();
    formData.append('data', dataBloqueio);
    formData.append('motivo', motivoBloqueio);

    fetch('/Reservas/BloquearData', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            Swal.fire({
                title: 'Sucesso',
                text: 'Data bloqueada com sucesso!',
                icon: 'success',
                background: '#18181b',
                color: '#facc15',
                showConfirmButton: false,
                timer: 2000,
                customClass: {
                    title: 'swal-title-custom',
                    popup: 'swal-popup-custom',                    
                }
            });
        })
        .catch(() => {
            Swal.fire({
                title: 'Erro',
                text: 'Não foi possível bloquear a data.',
                icon: 'error',
                background: '#18181b',
                color: '#facc15',
                confirmButtonColor: '#facc15',
                cancelButtonColor: '#b91c1c',
                customClass: {
                    title: 'swal-title-custom',
                    popup: 'swal-popup-custom',
                    confirmButton: 'swal-btn-confirm-custom'
                }
            });
        });
}



// ----------- Reservas -----------

//Cancelar Reserva

function cancelarReservaAdmin(idReserva) {
    
    const hoje = new Date();
    const daqui7 = new Date();
    daqui7.setDate(hoje.getDate() + 7);

    
    const formData = new FormData();
    formData.append('idReserva', idReserva);

    fetch('/Reservas/CancelarReserva', {
        method: 'DELETE',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            Swal.fire({
                title: 'Sucesso',
                text: 'Reserva cancelada com sucesso!',
                icon: 'success',
                background: '#18181b',
                color: '#facc15',
                showConfirmButton: false,
                timer: 2000,
                customClass: {
                    title: 'swal-title-custom',
                    popup: 'swal-popup-custom'
                }
            });
            buscarReservasPorData(hoje, daqui7);
        })
        .catch(() => {
            Swal.fire({
                title: 'Erro',
                text: 'Não foi possível cancelar a reserva.',
                icon: 'error',
                background: '#18181b',
                color: '#facc15',
                confirmButtonColor: '#facc15',
                cancelButtonColor: '#b91c1c',
                customClass: {
                    title: 'swal-title-custom',
                    popup: 'swal-popup-custom',
                    confirmButton: 'swal-btn-confirm-custom'
                }
            });
        });
}



//Concluir Reserva

function concluirReservaAdmin(idReserva) {

    const hoje = new Date();
    const daqui7 = new Date();
    daqui7.setDate(hoje.getDate() + 7);


    const formData = new FormData();
    formData.append('idReserva', idReserva);

    fetch('/Reservas/ConcluirReserva', {
        method: 'PUT',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            Swal.fire({
                title: 'Sucesso',
                text: 'Reserva concluída com sucesso!',
                icon: 'success',
                background: '#18181b',
                color: '#facc15',
                showConfirmButton: false,
                timer: 2000,
                customClass: {
                    title: 'swal-title-custom',
                    popup: 'swal-popup-custom'
                }
            });
            buscarReservasPorData(hoje, daqui7);
        })
        .catch(() => {
            Swal.fire({
                title: 'Erro',
                text: 'Não foi possível concluir a reserva.',
                icon: 'error',
                background: '#18181b',
                color: '#facc15',
                confirmButtonColor: '#facc15',
                cancelButtonColor: '#b91c1c',
                customClass: {
                    title: 'swal-title-custom',
                    popup: 'swal-popup-custom',
                    confirmButton: 'swal-btn-confirm-custom'
                }
            });
        });
}


//Busca reservas por data e renderiza na grid

function buscarReservasPorData(dataInicio, dataFim) {
    const gridReservas = document.querySelector('.grid-reservas');
    gridReservas.innerHTML = ''; // Limpa grid

    const formData = new FormData();
    formData.append('dataInicio', dataInicio);
    formData.append('dataFim', dataFim);

    fetch('/Reservas/ObterReservasPorPeriodo', {
        method: 'POST',
        body: formData
    })
        .then(r => r.json())
        .then(reservas => {
            if (!reservas || reservas.length === 0) {
                // Exibe mensagem de nenhum resultado
                gridReservas.innerHTML = `
                <div class="reserva-card reserva-card-vazio">
                    <div class="reserva-card-info">
                        <span class="reserva-titulo">Nenhuma reserva encontrada.</span>
                    </div>
                </div>`;
            } else {
                reservas.slice(0, 10).forEach(r => {
                    const statusTexto = r.reservaConcluida ? "Concluída" : "Pendente";
                    const idReserva = r.idReserva;

                    const card = document.createElement('div');
                    card.classList.add('reserva-card');
                    card.innerHTML = `
                            <div class="reserva-card">
                                <div class="reserva-card-dados">
                                    <span class="reserva-dado"><strong>Nome:</strong> ${r.nomeNaReserva}</span>
                                    <span class="reserva-dado"><strong>Data:</strong> ${formatarData(r.dataReserva)}</span>
                                    <span class="reserva-dado"><strong>Horário:</strong> ${r.horaReserva}</span>
                                    <span class="reserva-dado"><strong>Pessoas:</strong> ${r.numeroPessoas}</span>
                                </div>
                                <div class="reserva-card-acoes" style="flex-direction: row; gap: 12px; align-items: center;">
                                    ${!r.reservaConcluida ? `
                                        <button class="btn-editar-item" data-id="${idReserva}" title="Concluir" onclick="concluirReservaAdmin(${idReserva})"><i class="fa fa-check"></i></button>
                                        <button class="btn-excluir-item" data-id="${idReserva}" title="Excluir" onclick="cancelarReservaAdmin(${idReserva})" ><i class="fa fa-times"></i></button>
                                    ` : `<span class="reserva-dado" style="color: #facc15; font-weight: 600;">Concluída</span>`}
                                </div>
                            </div>
                        `;
                    gridReservas.appendChild(card);
                });

            }
        })
        .catch(() => {
            gridReservas.innerHTML = `
            <div class="reserva-card reserva-card-vazio">
                <div class="reserva-card-info">
                    <span class="reserva-titulo">Erro ao carregar reservas.</span>
                </div>
            </div>`;
        });
}
// ----------- Cardápio -----------

const selectTipo = document.getElementById('tipoFiltro'); // comida, bebida ou todos
const gridCardapio = document.querySelector('.grid-cardapio');
const URL_CARDAPIO = '/Cardapio/ListarCardapioCompleto';

let cardapioCache = null;

async function buscarCardapioCompleto() {
    const resp = await fetch(URL_CARDAPIO);
    if (!resp.ok) throw new Error('Erro ao buscar cardápio');
    return await resp.json();
}

// Renderiza uma lista de itens diretamente, sem blocos por categoria
function renderizarItensCardapio(lista) {
    gridCardapio.innerHTML = '';
    if (!lista.length) {
        gridCardapio.innerHTML = '<div class="cardapio-vazio">Nenhum item encontrado.</div>';
        return;
    }

    lista.forEach(item => {
        const nome = item.nomeComida || item.nomeBebida || '';
        const preco = typeof item.preco === 'number'
            ? item.preco.toFixed(2).replace('.', ',')
            : (item.preco || '');

        const card = document.createElement('div');
        card.className = 'cardapio-item-vertical';
        card.innerHTML = `
            <span class="cardapio-nome">${nome}</span>
            <span class="cardapio-preco">R$ ${preco}</span>
            <div class="cardapio-acoes">
                <button class="btn-editar-item" title="Editar" data-id="${item.idComida || item.idBebida}" data-tipo="${item.nomeComida ? 'Comida' : 'Bebida'}"><i class="fa fa-pen"></i></button>
                <button class="btn-excluir-item"
                    data-id="${item.idComida || item.idBebida}" 
                    data-tipo="${item.nomeComida ? 'Comida' : 'Bebida'}"
                    onclick="removerItemCardapio(this.getAttribute('data-id'), this.getAttribute('data-tipo'))">
                            <i class="fa fa-times"></i>
                </button>

            </div>
        `;
        
        gridCardapio.appendChild(card);
    });
}


// Filtra e exibe os itens de acordo com selects
function filtrarEExibirCardapio() {
    const tipoSelecionado = selectTipo.value;

    let itens = [];
    if (cardapioCache) {
        // Seleciona comidas, bebidas ou ambos conforme filtro
        if (!tipoSelecionado || tipoSelecionado === 'comida') {
            let comidas = cardapioCache.comidasDTO || [];           
            itens = itens.concat(comidas);
        }
        if (!tipoSelecionado || tipoSelecionado === 'bebida') {
            let bebidas = cardapioCache.bebidasDTO || [];            
            itens = itens.concat(bebidas);
        }
    }
    renderizarItensCardapio(itens);
}

// Eventos dos filtros
selectTipo.addEventListener('change', filtrarEExibirCardapio);

async function montarCardapio() {
    try {
        mostrarLoaderSwal();
        cardapioCache = await buscarCardapioCompleto();
        esconderLoaderSwal();
        filtrarEExibirCardapio();
    } catch (e) {
        esconderLoaderSwal();
        console.error(e);
    }
}

document.addEventListener('DOMContentLoaded', montarCardapio);


// ----------- Utilitário -----------


// Formata data para dd/MM/yyyy
function formatarData(dataIso) {
    if (!dataIso) return '';
    const partes = dataIso.split("-");
    if (partes.length >= 3) {
        return `${partes[2]}/${partes[1]}/${partes[0]}`;
    }
    return dataIso;
}