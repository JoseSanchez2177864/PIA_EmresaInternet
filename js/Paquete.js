$(document).ready(function () {
    const paquetes = [
        { id: 1, nombre: "Paquete Básico", precio: 300, descripcion: "Ideal para navegación ligera y redes sociales.", imagen: "https://www.astrolabio.com.mx/wp-content/uploads/2015/05/12.1.png" },
        { id: 2, nombre: "Paquete Avanzado", precio: 500, descripcion: "Perfecto para streaming y trabajo remoto.", imagen: "https://www.astrolabio.com.mx/wp-content/uploads/2015/05/12.1.png" },
        { id: 3, nombre: "Paquete Premium", precio: 800, descripcion: "Alta velocidad para gamers y empresas.", imagen: "https://cdn.hobbyconsolas.com/sites/navi.axelspringer.es/public/media/image/2023/08/mejores-routers-gaming-3098252.jpg?tf=3840x" }
    ];

    function renderPackages() {
        const container = $("#packagesList");
        container.empty();

        paquetes.forEach(paquete => {
            container.append(`
                <div class="col-md-4">
                    <div class="card mb-4 shadow-sm">
                        <img src="${paquete.imagen}" class="card-img-top" alt="Imagen del paquete">
                        <div class="card-body">
                            <h5 class="card-title">${paquete.nombre}</h5>
                            <p class="card-text">${paquete.descripcion}</p>
                            <p class="text-primary font-weight-bold">$${paquete.precio.toFixed(2)}</p>
                            <button class="btn btn-success btn-block buy-package" data-id="${paquete.id}">Comprar</button>
                        </div>
                    </div>
                </div>
            `);
        });
    }

    renderPackages();

    // Redirigir a la página de compra con el ID del paquete
    $(document).on("click", ".buy-package", function () {
        const packageId = $(this).data("id");
        window.location.href = `Compra.html?id=${packageId}`;
    });
});
