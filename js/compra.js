$(document).ready(function () {
    // Simulación de paquetes disponibles (se pasa desde otra página)
    const paquetes = [
        { id: 1, nombre: "Paquete Básico", precio: 300, descripcion: "Ideal para navegación ligera y redes sociales." },
        { id: 2, nombre: "Paquete Avanzado", precio: 500, descripcion: "Perfecto para streaming y trabajo remoto." },
        { id: 3, nombre: "Paquete Premium", precio: 800, descripcion: "Alta velocidad para gamers y empresas." }
    ];

    // Obtener el ID del paquete seleccionado desde el URL
    const urlParams = new URLSearchParams(window.location.search);
    const packageId = parseInt(urlParams.get("id"));

    // Validar si hay un paquete seleccionado
    const paqueteSeleccionado = paquetes.find(p => p.id === packageId);
    if (!paqueteSeleccionado) {
        // Redirigir a la página de paquetes si no hay selección
        alert("Por favor, selecciona un paquete primero.");
        window.location.href = "paquetes.html";
        return;
    }

    // Mostrar los datos del paquete seleccionado
    $("#packageName").text(paqueteSeleccionado.nombre);
    $("#packageDescription").text(paqueteSeleccionado.descripcion);
    $("#packagePrice").text(paqueteSeleccionado.precio.toFixed(2));

    // Manejar la compra
    $("#purchaseForm").submit(function (e) {
        e.preventDefault();

        const buyerName = $("#buyerName").val();
        const buyerEmail = $("#buyerEmail").val();

        alert(`Gracias ${buyerName}! Tu compra del paquete ${paqueteSeleccionado.nombre} ha sido confirmada. Se enviará la información a ${buyerEmail}.`);

        // Redirigir al inicio después de confirmar la compra
        window.location.href = "index.html";
    });
});
