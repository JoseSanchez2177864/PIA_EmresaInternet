$(document).ready(function () {
    const opiniones = [
        { id: 1, texto: "Excelente servicio, muy satisfecho.", anonimo: false, usuario: "Juan Pérez", fecha: "2024-11-01" },
        { id: 2, texto: "Me gusta la rapidez de conexión.", anonimo: true, usuario: "Anónimo", fecha: "2024-11-02" }
    ];

    function renderOpinions() {
        const container = $("#opinionsList");
        container.empty();

        if (opiniones.length === 0) {
            container.append('<p class="text-muted">No hay opiniones aún. ¡Sé el primero en opinar!</p>');
        } else {
            opiniones.forEach(opinion => {
                container.append(`
                    <div class="card mb-3">
                        <div class="card-body">
                            <h5 class="card-title">${opinion.anonimo ? "Anónimo" : opinion.usuario}</h5>
                            <p class="card-text">${opinion.texto}</p>
                            <p class="text-muted small">Publicado el ${opinion.fecha}</p>
                        </div>
                    </div>
                `);
            });
        }
    }

    renderOpinions();

    // Publicar nueva opinión
    $("#opinionForm").submit(function (e) {
        e.preventDefault();

        const texto = $("#opinionText").val();
        const anonimo = $("#isAnonymous").is(":checked");
        const usuario = anonimo ? "Anónimo" : "Usuario Actual"; // Sustituir por el nombre del usuario autenticado
        const fecha = new Date().toISOString().split("T")[0]; // Fecha en formato YYYY-MM-DD

        const newOpinion = { id: opiniones.length + 1, texto, anonimo, usuario, fecha };
        opiniones.unshift(newOpinion); // Agregar al inicio para mostrarlo como más reciente

        renderOpinions();

        // Resetear el formulario
        $("#opinionForm")[0].reset();
    });
});
