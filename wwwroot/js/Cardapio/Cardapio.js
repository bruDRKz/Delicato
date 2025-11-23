const URL_CARDAPIO = '/Cardapio/ListarCardapioCompleto';

const mapaCategoriasComidas = {
    8: 'Espetos Tradicionais',
    9: 'Espetos Especiais',
    10: 'Porções Individuais',
    11: 'Lanches'
};

const mapaCategoriasBebidas = {
    12: 'Não Alcoólicos',
    13: 'Cervejas',
    14: 'Doses',
    15: 'Drinks',
    16: 'Caipirinhas'
};

// Agrupa por categoria (com base no IdCategoria)
function agruparPorCategoria(lista, mapaCategorias, campoIdCategoria) {
    return lista.reduce((acc, item) => {
        const id = item[campoIdCategoria];
        const nomeCategoria = mapaCategorias[id] || 'Outros';
        if (!acc[nomeCategoria]) acc[nomeCategoria] = [];
        acc[nomeCategoria].push(item);
        return acc;
    }, {});
}

function formatarPreco(valor) {
    return valor.toFixed(2).replace('.', ',');
}

// Cria bloco da categoria com fade-in
function criarBlocoCategoria(nomeCategoria, itens, classeExtra = '') {
    const wrapper = document.createElement('div');
    wrapper.className = `menu-coluna-wrapper ${classeExtra}`;

    const h3 = document.createElement('h3');
    h3.textContent = nomeCategoria;

    const coluna = document.createElement('div');
    coluna.className = 'menu-coluna';

    const ul = document.createElement('ul');

    itens.forEach(i => {
        const li = document.createElement('li');
        const nome = i.nomeComida || i.nomeBebida || '';
        li.textContent = nome + ' ';

        const span = document.createElement('span');
        span.textContent = formatarPreco(i.preco);

        li.appendChild(span);
        ul.appendChild(li);
    });

    coluna.appendChild(ul);
    wrapper.appendChild(h3);
    wrapper.appendChild(coluna);
    return wrapper;
}

// Renderiza cards de comidas escalonados com fade-in
function renderizarComidas(comidasDTO) {
    const container = document.getElementById('menusComidas');
    container.innerHTML = '';

    const agrupado = agruparPorCategoria(comidasDTO, mapaCategoriasComidas, 'idCategoria');
    Object.entries(agrupado).forEach(([categoria, itens], idx) => {
        const classe = categoria.toLowerCase().replace(/\s+/g, '-');
        const bloco = criarBlocoCategoria(categoria, itens, classe);
        container.appendChild(bloco);

        setTimeout(() => bloco.classList.add('loaded'), 110 * idx);
    });
}

// Renderiza cards de bebidas alternando colunas, com fade-in
function renderizarBebidas(bebidasDTO) {
    const colunaEsq = document.getElementById('colunaBebidasEsquerda');
    const colunaDir = document.getElementById('colunaBebidasDireita');
    colunaEsq.innerHTML = '';
    colunaDir.innerHTML = '';

    const agrupado = agruparPorCategoria(bebidasDTO, mapaCategoriasBebidas, 'idCategoria');
    const entradas = Object.entries(agrupado);

    entradas.forEach(([categoria, itens], idx) => {
        const classe = categoria.toLowerCase().replace(/\s+/g, '-');
        const bloco = criarBlocoCategoria(categoria, itens, classe);
        if (idx % 2 === 0) {
            colunaEsq.appendChild(bloco);
        } else {
            colunaDir.appendChild(bloco);
        }
        setTimeout(() => bloco.classList.add('loaded'), 110 * idx);
    });
}

// Busca cardápio via controller
async function buscarCardapioCompleto() {
    const resp = await fetch(URL_CARDAPIO);
    if (!resp.ok) throw new Error('Erro ao buscar cardápio');
    return await resp.json();
}

// Entrada principal (loader + renderização)
async function montarCardapio() {
    try {
        mostrarLoaderSwal();
        const dto = await buscarCardapioCompleto();
        esconderLoaderSwal();

        renderizarComidas(dto.comidasDTO || []);
        renderizarBebidas(dto.bebidasDTO || []);
    } catch (e) {
        esconderLoaderSwal();
        console.error(e);
    }
}


document.addEventListener('DOMContentLoaded', montarCardapio);
