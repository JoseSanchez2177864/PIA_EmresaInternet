$(document).ready(function () {
    // Lista inicial de usuarios
    const usuarios = [
        { id: 1, nombre: "Juan Pérez", correo: "juan@gmail.com", rol: "Usuario" },
        { id: 2, nombre: "María López", correo: "maria@gmail.com", rol: "Administrador" }
    ];

    // Contador para IDs
    let nextId = usuarios.length + 1;

    // Función para renderizar la tabla
    function renderTable() {
        const tableBody = $("#usersTable tbody");
        tableBody.empty(); // Vaciar la tabla antes de volver a llenarla
        usuarios.forEach(user => {
            tableBody.append(`
                <tr>
                    <td>${user.id}</td>
                    <td>${user.nombre}</td>
                    <td>${user.correo}</td>
                    <td>${user.rol}</td>
                    <td>
                        <button class="btn btn-warning btn-sm edit-user" data-id="${user.id}">Editar</button>
                        <button class="btn btn-danger btn-sm delete-user" data-id="${user.id}">Eliminar</button>
                    </td>
                </tr>
            `);
        });

        // Inicializar o actualizar Tablesorter
        $("#usersTable").trigger("update");
    }

    // Renderizar tabla al cargar
    renderTable();

    // Crear usuario
    $("#addUserBtn").click(function () {
        $("#userModalLabel").text("Crear Usuario");
        $("#userForm")[0].reset();
        $("#userId").val("");
        $("#userModal").modal("show");
    });

    // Guardar usuario
    $("#userForm").submit(function (e) {
        e.preventDefault();
        const id = $("#userId").val();
        const nombre = $("#userName").val();
        const correo = $("#userEmail").val();
        const rol = $("#userRole").val();

        if (id) {
            // Editar usuario existente
            const user = usuarios.find(u => u.id == id);
            user.nombre = nombre;
            user.correo = correo;
            user.rol = rol;
        } else {
            // Crear nuevo usuario
            const newUser = { id: nextId++, nombre, correo, rol };
            usuarios.push(newUser);
        }

        $("#userModal").modal("hide");
        renderTable();
    });

    // Editar usuario
    $(document).on("click", ".edit-user", function () {
        const id = $(this).data("id");
        const user = usuarios.find(u => u.id == id);

        $("#userModalLabel").text("Editar Usuario");
        $("#userName").val(user.nombre);
        $("#userEmail").val(user.correo);
        $("#userRole").val(user.rol);
        $("#userId").val(user.id);
        $("#userModal").modal("show");
    });

    // Eliminar usuario
    $(document).on("click", ".delete-user", function () {
        const id = $(this).data("id");
        if (confirm("¿Seguro que deseas eliminar este usuario?")) {
            const index = usuarios.findIndex(user => user.id == id);
            if (index > -1) {
                usuarios.splice(index, 1); // Eliminar del arreglo de usuarios
                renderTable(); // Volver a renderizar la tabla
            }
        }
    });

    // Configuración de Tablesorter
    $("#usersTable").tablesorter({
        theme: "bootstrap", // Tema visual
        widgets: ["zebra"], // Alternar colores de filas
        headers: {
            4: { sorter: false } // Deshabilitar ordenación en columna de acciones
        }
    });
});
