$(document).ready(function () {
    const opiniones = [
        { id: 1, opinion: "Excelente servicio.", anonimo: true, usuario: "Juan Pérez" },
        { id: 2, opinion: "Muy rápido y confiable.", anonimo: false, usuario: "María López" },
        { id: 3, opinion: "El servicio es bueno, pero hay desconexiones.", anonimo: true, usuario: "Carlos Ramírez" }
    ];

    function renderTable() {
        const tableBody = $("#opinionsTable tbody");
        tableBody.empty();
        opiniones.forEach(opinion => {
            tableBody.append(`
                <tr>
                    <td>${opinion.id}</td>
                    <td>${opinion.opinion}</td>
                    <td>${opinion.anonimo ? "Sí" : "No"}</td>
                    <td>${opinion.usuario}</td>
                    <td>
                        <button class="btn btn-danger btn-sm delete-opinion" data-id="${opinion.id}">Eliminar</button>
                    </td>
                </tr>
            `);
        });

        $("#opinionsTable").trigger("update");
    }

    renderTable();

    // Eliminar opinión
    $(document).on("click", ".delete-opinion", function () {
        const id = $(this).data("id");
        if (confirm("¿Seguro que deseas eliminar esta opinión?")) {
            const index = opiniones.findIndex(o => o.id == id);
            if (index > -1) {
                opiniones.splice(index, 1);
                renderTable();
            }
        }
    });

    $("#opinionsTable").tablesorter({
        theme: "bootstrap",
        widgets: ["zebra"],
        headers: {
            4: { sorter: false } // Deshabilitar ordenación en columna de acciones
        }
    });
});
