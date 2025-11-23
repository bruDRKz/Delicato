document.addEventListener("DOMContentLoaded", () => {
    const carrosselWrappers = document.querySelectorAll(".carrossel-wrapper");

    carrosselWrappers.forEach(wrapper => {
        // DUPLICA TODAS AS IMAGENS
        wrapper.innerHTML += wrapper.innerHTML;

        let scrollPos = 0;
        const velocidade = parseFloat(wrapper.parentElement.dataset.velocidade) || 0.5;

        const totalWidth = Array.from(wrapper.children)
            .slice(0, wrapper.children.length / 2)
            .reduce((acc, img) => acc + img.offsetWidth + 5, 0); // soma largura das imagens + gap

        function animar() {
            scrollPos -= velocidade;

            if (Math.abs(scrollPos) >= totalWidth) {
                scrollPos = 0; // reinicia suavemente
            }

            wrapper.style.transform = `translateX(${scrollPos}px)`;

            requestAnimationFrame(animar);
        }

        animar();
    });
});