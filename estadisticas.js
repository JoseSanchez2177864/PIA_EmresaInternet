$(document).ready(function () {
    // Datos simulados de paquetes
    const paquetes = [
        { id: "1", nombre: "Paquete Básico" },
        { id: "2", nombre: "Paquete Avanzado" },
        { id: "3", nombre: "Paquete Premium" }
    ];

    // Datos simulados de ventas para todos los meses
    const ventas = [
        { mes: "Enero", ventas: 300, paqueteId: "1" },
        { mes: "Febrero", ventas: 400, paqueteId: "1" },
        { mes: "Marzo", ventas: 500, paqueteId: "1" },
        { mes: "Abril", ventas: 350, paqueteId: "1" },
        { mes: "Mayo", ventas: 450, paqueteId: "1" },
        { mes: "Junio", ventas: 400, paqueteId: "1" },
        { mes: "Julio", ventas: 500, paqueteId: "1" },
        { mes: "Agosto", ventas: 600, paqueteId: "1" },
        { mes: "Septiembre", ventas: 700, paqueteId: "1" },
        { mes: "Octubre", ventas: 800, paqueteId: "1" },
        { mes: "Noviembre", ventas: 750, paqueteId: "1" },
        { mes: "Diciembre", ventas: 850, paqueteId: "1" },
        { mes: "Enero", ventas: 200, paqueteId: "2" },
        { mes: "Febrero", ventas: 300, paqueteId: "2" },
        { mes: "Marzo", ventas: 400, paqueteId: "2" },
        { mes: "Abril", ventas: 250, paqueteId: "2" },
        { mes: "Mayo", ventas: 300, paqueteId: "2" },
        { mes: "Junio", ventas: 350, paqueteId: "2" },
        { mes: "Julio", ventas: 400, paqueteId: "2" },
        { mes: "Agosto", ventas: 500, paqueteId: "2" },
        { mes: "Septiembre", ventas: 600, paqueteId: "2" },
        { mes: "Octubre", ventas: 700, paqueteId: "2" },
        { mes: "Noviembre", ventas: 650, paqueteId: "2" },
        { mes: "Diciembre", ventas: 750, paqueteId: "2" },
        { mes: "Enero", ventas: 150, paqueteId: "3" },
        { mes: "Febrero", ventas: 250, paqueteId: "3" },
        { mes: "Marzo", ventas: 350, paqueteId: "3" },
        { mes: "Abril", ventas: 200, paqueteId: "3" },
        { mes: "Mayo", ventas: 300, paqueteId: "3" },
        { mes: "Junio", ventas: 250, paqueteId: "3" },
        { mes: "Julio", ventas: 300, paqueteId: "3" },
        { mes: "Agosto", ventas: 400, paqueteId: "3" },
        { mes: "Septiembre", ventas: 500, paqueteId: "3" },
        { mes: "Octubre", ventas: 550, paqueteId: "3" },
        { mes: "Noviembre", ventas: 600, paqueteId: "3" },
        { mes: "Diciembre", ventas: 700, paqueteId: "3" }
    ];

    // Inicialización de Chart.js
    const salesChartConfig = {
        type: 'bar',
        data: {
            labels: [],
            datasets: [{
                label: 'Ventas ($)',
                data: [],
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    };

    let salesChart;

    // Cargar los paquetes en el dropdown
    function loadPackages() {
        const packageFilter = $("#packageFilter");
        packageFilter.empty();
        packageFilter.append('<option value="todos">Todos los Paquetes</option>');
        paquetes.forEach(paquete => {
            packageFilter.append(`<option value="${paquete.id}">${paquete.nombre}</option>`);
        });
    }

    // Filtrar datos de ventas por paquete
    function filterSalesData(packageId) {
        if (packageId === "todos") {
            return ventas.reduce((acc, curr) => {
                const index = acc.findIndex(item => item.mes === curr.mes);
                if (index >= 0) {
                    acc[index].ventas += curr.ventas;
                } else {
                    acc.push({ mes: curr.mes, ventas: curr.ventas });
                }
                return acc;
            }, []);
        } else {
            return ventas.filter(venta => venta.paqueteId === packageId);
        }
    }

    // Cargar datos de ventas en el gráfico
    function loadSalesData(packageId = "todos") {
        const filteredSales = filterSalesData(packageId);
        salesChartConfig.data.labels = filteredSales.map(item => item.mes);
        salesChartConfig.data.datasets[0].data = filteredSales.map(item => item.ventas);

        // Si el gráfico ya existe, actualizarlo
        if (salesChart) {
            salesChart.update();
        } else {
            const salesCtx = document.getElementById('salesChart').getContext('2d');
            salesChart = new Chart(salesCtx, salesChartConfig);
        }
    }

    // Evento para filtrar por paquete
    $("#packageFilter").change(function () {
        const packageId = $(this).val();
        loadSalesData(packageId);
    });

    // Inicialización
    loadPackages(); // Cargar paquetes en el dropdown
    loadSalesData(); // Cargar datos de ventas iniciales
});
