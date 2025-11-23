// Elementos do modal
const btnAbrirModalAdicionar = document.getElementById('btnAbrirModalAdicionarItem');
const modalAdicionar = document.getElementById('modalAdicionarItem');
const btnFecharAdicionar = document.getElementById('fecharModalAdicionarItem');
const formAdicionar = document.getElementById('formAdicionarItem');

// Abrir modal
btnAbrirModalAdicionar.addEventListener('click', () => {
    modalAdicionar.style.display = 'flex';
    limparCamposAdicionarItem();
});

// Fechar modal ao clicar no X
btnFecharAdicionar.addEventListener('click', () => {
    modalAdicionar.style.display = 'none';
});

// Fechar modal ao clicar fora
window.addEventListener('click', (e) => {
    if (e.target === modalAdicionar) {
        modalAdicionar.style.display = 'none';
    }
});

//Submit do formulário de adicionar item
formAdicionar.addEventListener('submit', function (event) {
    event.preventDefault();

    // Coleta apenas os campos necessários para o backend
    const nome = document.getElementById('inputNomeItem').value.trim();
    const preco = document.getElementById('inputPrecoItem').value.replace(/\./g, ',');
    const categoria = document.getElementById('inputCategoriaItem').value; 
    const tipo = document.getElementById('selectTipoItem').value;
    const descricao = document.getElementById('inputDescricaoItem').value.trim();
           
    const formData = new FormData();
    formData.append('nome', nome);
    formData.append('preco', preco);
    formData.append('categoria', categoria);
    formData.append('tipo', tipo);
    formData.append('descricao', descricao);
    

    fetch('/Cardapio/AdicionarItemCardapio', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(result => {
            if (result.sucesso) {
                modalAdicionar.style.display = 'none';
                Swal.fire({
                    title: 'Sucesso',
                    text: 'Item adicionado ao cardápio!',
                    icon: 'success',
                    background: '#18181b',
                    color: '#facc15',
                    showConfirmButton: false,
                    timer: 1600
                });                
            } else {
                Swal.fire({
                    title: 'Erro',
                    text: 'Falha ao adicionar item.',
                    icon: 'error',
                    background: '#18181b',
                    color: '#facc15',
                    confirmButtonColor: '#facc15'
                });
            }
            montarCardapio();
        })
        .catch(() => {
            Swal.fire({
                title: 'Erro',
                text: 'Não foi possível adicionar o item.',
                icon: 'error',
                background: '#18181b',
                color: '#facc15',
                confirmButtonColor: '#facc15'
            });
        });
            
});



//Editar e remover itens (delegação de eventos)

// Elementos do modal de edição
const modalEditar = document.getElementById('modalEditarItem');
const btnFecharEditar = document.getElementById('fecharModalEditarItem');
const formEditar = document.getElementById('formEditarItem');

// Abre o modal e preenche os dados do item
function abrirModalEditarItem(id, tipo) {
    limparCamposEditarItem()
    // Busca o item no cache
    let item;
    if (tipo === 'Comida') {
        item = (cardapioCache.comidasDTO || []).find(i => i.idComida == id);
    } else {
        item = (cardapioCache.bebidasDTO || []).find(i => i.idBebida == id);
    }

    if (!item) return;

    // Preenche os inputs
    document.getElementById('editNomeItem').value = item.nomeComida || item.nomeBebida || '';
    document.getElementById('editDescricaoItem').value = item.descricaoComida || item.descricaoBebida || '';
    document.getElementById('editPrecoItem').value = item.preco || '';
    document.getElementById('editCategoriaItem').value = item.idCategoria || '';
    document.getElementById('editDisponivelItem').checked = item.disponivel || false;
    document.getElementById('editTipoItem').value = tipo;

    // Armazena id e tipo para usar no submit
    formEditar.dataset.id = id;
    formEditar.dataset.tipo = tipo;

    modalEditar.style.display = 'flex';
}

// Fechar modal ao clicar no X ou fora
btnFecharEditar.addEventListener('click', () => modalEditar.style.display = 'none');
window.addEventListener('click', (e) => {
    if (e.target === modalEditar) modalEditar.style.display = 'none';
});

// Evento de clique nos botões de editar
gridCardapio.addEventListener('click', function (e) {
    const btn = e.target.closest('.btn-editar-item');
    if (!btn) return;
    const id = btn.getAttribute('data-id');
    const tipo = btn.getAttribute('data-tipo');
    abrirModalEditarItem(id, tipo);
});

formEditar.addEventListener('submit', function (event) {
    event.preventDefault();
    const id = formEditar.dataset.id;
    const tipo = formEditar.dataset.tipo;
    const nome = document.getElementById('editNomeItem').value.trim();
    const preco = document.getElementById('editPrecoItem').value.replace(/\./g, ',');
    const categoria = document.getElementById('editCategoriaItem').value;    
    const descricao = document.getElementById('editDescricaoItem').value.trim();
    const disponivel = document.getElementById('editDisponivelItem').checked;

    const formData = new FormData();
    formData.append('id', id);
    formData.append('nome', nome);
    formData.append('descricao', descricao);
    formData.append('preco', preco);
    formData.append('categoria', categoria);
    formData.append('tipo', tipo);    
    formData.append('disponivel', disponivel);

    fetch('/Cardapio/EditarItemCardapio', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(result => {
            if (result.sucesso) {
                modalEditar.style.display = 'none';
                Swal.fire({
                    title: 'Sucesso',
                    text: 'Item editado com sucesso!',
                    icon: 'success',
                    background: '#18181b',
                    color: '#facc15',
                    timer: 1600,
                    showConfirmButton: false
                });
                
            } else {
                Swal.fire({
                    title: 'Erro',
                    text: result.Mensagem,
                    icon: 'error',
                    background: '#18181b',
                    color: '#facc15',
                    confirmButtonColor: '#facc15'
                });
            }

            montarCardapio();
        })
        .catch(() => {
            Swal.fire({
                title: 'Erro',
                text: 'Não foi possível editar o item.',
                icon: 'error',
                background: '#18181b',
                color: '#facc15',
                confirmButtonColor: '#facc15'
            });
        });    
});

function limparCamposAdicionarItem() {
    document.getElementById('inputNomeItem').value = '';
    document.getElementById('inputPrecoItem').value = '';
    document.getElementById('inputCategoriaItem').value = '';
    document.getElementById('selectTipoItem').value = '';
    document.getElementById('inputDescricaoItem').value = '';
    document.getElementById('inputDisponivelItem').checked = true; 
}
function limparCamposEditarItem() {
    document.getElementById('editNomeItem').value = '';
    document.getElementById('editPrecoItem').value = '';
    document.getElementById('editCategoriaItem').value = '';
    document.getElementById('editTipoItem').value = '';
    document.getElementById('editDescricaoItem').value = '';
    document.getElementById('editDisponivelItem').checked = true; 
}

//Excluir itens
function removerItemCardapio(id, tipo) {
    Swal.fire({
        title: 'Confirmar exclusão',
        text: 'Tem certeza que deseja remover este item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#facc15',
        cancelButtonColor: '#b91c1c',
        confirmButtonText: 'Sim, remover',
        cancelButtonText: 'Cancelar',
        background: '#18181b',
        color: '#facc15'
    }).then((result) => {
        if (result.isConfirmed) {            
            fetch(`/Cardapio/RemoverItemCardapio?id=${id}&tipo=${encodeURIComponent(tipo)}`, {
                method: 'DELETE'
            })
                .then(response => response.json())
                .then(data => {
                    if (data.sucesso) {
                        Swal.fire({
                            title: 'Removido!',
                            text: data.Mensagem || 'Item removido com sucesso.',
                            icon: 'success',
                            background: '#18181b',
                            color: '#facc15',
                            timer: 1700,
                            showConfirmButton: false
                        });
                        montarCardapio(); 
                    } else {
                        Swal.fire({
                            title: 'Erro',
                            text: data.Mensagem || 'Não foi possível remover o item.',
                            icon: 'error',
                            background: '#18181b',
                            color: '#facc15',
                            confirmButtonColor: '#facc15'
                        });
                    }
                })
                .catch(() => {
                    Swal.fire({
                        title: 'Erro',
                        text: 'Falha ao remover item.',
                        icon: 'error',
                        background: '#18181b',
                        color: '#facc15',
                        confirmButtonColor: '#facc15'
                    });
                });
        }
    });
}
