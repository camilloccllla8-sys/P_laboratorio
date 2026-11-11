
using System;

class Program
{
    // REQ 1: Validar precio y cantidad
    static bool ValidarDatos(double precio, int cantidad)
    {
        if (precio <= 0 || cantidad <= 0)
        {
            Console.WriteLine("ERROR: Precio o cantidad no validos.");
            return false;
        }

        return true;
    }

    // REQ 2: Imprimir matriz bidimensional
    static void ImprimirMatrizDatos(double[,] matriz, string[] codigos)
    {
        Console.WriteLine("\n--- MATRIZ BIDIMENSIONAL DE DATOS ---");
        Console.WriteLine("Fila | Codigo   | Precio ($) | Cantidad");
        Console.WriteLine("----------------------------------------");

        for (int i = 0; i < codigos.Length; i++)
        {
            Console.WriteLine(
                $"[{i}] | {codigos[i]} | " +
                $"$ {matriz[i, 0]:N2} | {matriz[i, 1]}");
        }
    }

    // REQ 3: Ordenamiento burbuja descendente
    static void OrdenarBurbuja(
        string[] codigos,
        string[] modelos,
        double[,] datos,
        double[] montosNetos)
    {
        int n = codigos.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (montosNetos[j] < montosNetos[j + 1])
                {
                    double auxMonto = montosNetos[j];
                    montosNetos[j] = montosNetos[j + 1];
                    montosNetos[j + 1] = auxMonto;

                    string auxCodigo = codigos[j];
                    codigos[j] = codigos[j + 1];
                    codigos[j + 1] = auxCodigo;

                    string auxModelo = modelos[j];
                    modelos[j] = modelos[j + 1];
                    modelos[j + 1] = auxModelo;

                    for (int k = 0; k < 2; k++)
                    {
                        double auxDato = datos[j, k];
                        datos[j, k] = datos[j + 1, k];
                        datos[j + 1, k] = auxDato;
                    }
                }
            }
        }
    }

    // REQ 4: Busqueda lineal
    static void BuscarCodigo(
        string[] codigos,
        string[] modelos,
        double[] montos,
        string codigoBuscado)
    {
        if (string.IsNullOrWhiteSpace(codigoBuscado))
        {
            Console.WriteLine("ERROR: El codigo esta vacio.");
            return;
        }

        for (int i = 0; i < codigos.Length; i++)
        {
            if (codigos[i].Equals(
                codigoBuscado.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    $"ENCONTRADO! Modelo: {modelos[i]} | " +
                    $"Monto: $ {montos[i]:N2}");
                return;
            }
        }

        Console.WriteLine("ERROR: Codigo no registrado.");
    }

    static void Main(string[] args)
    {
        // Arreglos de vehiculos
        string[] codigos =
        {
            "AUTO-201", "AUTO-202", "AUTO-203",
            "AUTO-204", "AUTO-205", "AUTO-206"
        };

        string[] modelos =
        {
            "SUV Interceptor",
            "Sedan Executive",
            "PickUp Titan",
            "City Hatchback",
            "Electric EV",
            "Van Carga"
        };

        // Matriz: precio y cantidad
        double[,] datos =
        {
            { 28000.0, 2 },
            { 18500.0, 4 },
            { 35000.0, 1 },
            { 14000.0, 5 },
            { 42000.0, 1 },
            { 22500.0, 3 }
        };

        double[] montosNetos = new double[6];

        double granTotal = 0;
        int ventasPremium = 0;

        Console.WriteLine("========================================");
        Console.WriteLine("    CONTROL DE VENTAS AUTOMOTRICES");
        Console.WriteLine("========================================");

        // Mostrar matriz original
        ImprimirMatrizDatos(datos, codigos);

        // Calcular montos y metricas
        for (int i = 0; i < codigos.Length; i++)
        {
            double precio = datos[i, 0];
            int cantidad = (int)datos[i, 1];

            if (ValidarDatos(precio, cantidad))
            {
                montosNetos[i] = precio * cantidad;

                granTotal += montosNetos[i];

                if (montosNetos[i] > 50000)
                {
                    ventasPremium++;
                }
            }
        }

        // Ordenar de mayor a menor
        OrdenarBurbuja(codigos, modelos, datos, montosNetos);

        Console.WriteLine("\n--- REPORTE ORDENADO POR MONTO NETO ---");

        for (int i = 0; i < codigos.Length; i++)
        {
            Console.WriteLine(
                $"{i + 1}. Codigo: {codigos[i]} | " +
                $"Modelo: {modelos[i]} | " +
                $"Monto: $ {montosNetos[i]:N2}");
        }

        // Mostrar resumen
        Console.WriteLine("\n----------------------------------------");
        Console.WriteLine("METRICAS RESUMEN:");

        Console.WriteLine($"Gran Total Acumulado: $ {granTotal:N2}");
        Console.WriteLine($"Ventas Premium (> $50,000): {ventasPremium} transacciones");

        // Buscar vehiculo
        Console.WriteLine("\n----------------------------------------");
        Console.WriteLine("PRUEBA DE BUSQUEDA SECUENCIAL:");

        Console.Write("Ingrese codigo a buscar: ");
        string codigoBuscado = Console.ReadLine() ?? "";

        BuscarCodigo(codigos, modelos, montosNetos, codigoBuscado);

        Console.WriteLine("\nPresione una tecla para salir...");
        Console.ReadKey();
    }
}

