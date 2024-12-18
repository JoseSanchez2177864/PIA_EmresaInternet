$(document).ready(function () {
    const paquetes = [
        {
            id: 1,
            nombre: "Paquete Básico",
            precio: 300,
            descripcion: "Ideal para navegación ligera y redes sociales.",
            imagen: "https://via.placeholder.com/100" // Imagen de ejemplo
        },
        {
            id: 2,
            nombre: "Paquete Avanzado",
            precio: 500,
            descripcion: "Perfecto para streaming y trabajo remoto.",
            imagen: "https://via.placeholder.com/100"
        }
    ];

    let nextId = paquetes.length + 1;

    function renderTable() {
        const tableBody = $("#packagesTable tbody");
        tableBody.empty();
        paquetes.forEach(package => {
            tableBody.append(`
                <tr>
                    <td>${package.id}</td>
                    <td>${package.nombre}</td>
                    <td>$${package.precio.toFixed(2)}</td>
                    <td>${package.descripcion}</td>
                    <td><img src="${package.imagen}" alt="Imagen de ${package.nombre}" class="img-thumbnail" width="100"></td>
                    <td>
                        <button class="btn btn-warning btn-sm edit-package" data-id="${package.id}">Editar</button>
                        <button class="btn btn-danger btn-sm delete-package" data-id="${package.id}">Eliminar</button>
                    </td>
                </tr>
            `);
        });

        $("#packagesTable").trigger("update");
    }

    renderTable();

    $("#addPackageBtn").click(function () {
        $("#packageModalLabel").text("Crear Paquete");
        $("#packageForm")[0].reset();
        $("#packageId").val("");
        $("#packageImage").siblings(".custom-file-label").text("Seleccionar archivo");
        $("#packageModal").modal("show");
    });

    $("#packageForm").submit(function (e) {
        e.preventDefault();
        const id = $("#packageId").val();
        const nombre = $("#packageName").val();
        const precio = parseFloat($("#packagePrice").val());
        const descripcion = $("#packageDescription").val();
        const imagen = $("#packageImage")[0].files[0]
            ? URL.createObjectURL($("#packageImage")[0].files[0])
            : "https://via.placeholder.com/100"; // Imagen por defecto

        if (id) {
            const paquete = paquetes.find(p => p.id == id);
            paquete.nombre = nombre;
            paquete.precio = precio;
            paquete.descripcion = descripcion;
            paquete.imagen = imagen;
        } else {
            const newPackage = { id: nextId++, nombre, precio, descripcion, imagen };
            paquetes.push(newPackage);
        }

        $("#packageModal").modal("hide");
        renderTable();
    });

    $(document).on("click", ".edit-package", function () {
        const id = $(this).data("id");
        const paquete = paquetes.find(p => p.id == id);

        $("#packageModalLabel").text("Editar Paquete");
        $("#packageName").val(paquete.nombre);
        $("#packagePrice").val(paquete.precio);
        $("#packageDescription").val(paquete.descripcion);
        $("#packageId").val(paquete.id);
        $("#packageImage").siblings(".custom-file-label").text("Seleccionar archivo");
        $("#packageModal").modal("show");
    });

    $(document).on("click", ".delete-package", function () {
        const id = $(this).data("id");
        if (confirm("¿Seguro que deseas eliminar este paquete?")) {
            const index = paquetes.findIndex(p => p.id == id);
            if (index > -1) {
                paquetes.splice(index, 1);
                renderTable();
            }
        }
    });

    $("#packagesTable").tablesorter({
        theme: "bootstrap",
        widgets: ["zebra"],
        headers: {
            5: { sorter: false } // Deshabilitar ordenación en columna de acciones
        }
    });

    // Actualizar el texto del botón cuando se selecciona un archivo
    $(document).on("change", "#packageImage", function () {
        const fileName = $(this).val().split("\\").pop(); // Obtener el nombre del archivo
        $(this).siblings(".custom-file-label").text(fileName || "Seleccionar archivo");
    });
});
