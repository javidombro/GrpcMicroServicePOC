using Grpc.Core;
using Grpc.Net.Client;
using GrpcMicroServicePOC.Protos;
using System;
using System.Threading.Tasks;

namespace GrpcMicroSerivePOC.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Equipo.EquipoClient(channel);

            using var equiposStream = client.GetAllTeams(new EmptyRequest());
            while (await equiposStream.ResponseStream.MoveNext())
            {
                var equipo = equiposStream.ResponseStream.Current;
                Console.WriteLine($"Equipo: {equipo.Nombre}");
                Console.WriteLine($"Titulos: {equipo.Titulos}");
                Console.WriteLine($"Descensos: {equipo.Descensos}");
                Console.WriteLine($"Grande: {equipo.EsGrande}");
                Console.WriteLine($"Ubicacion: {equipo.Ubicacion}");
                Console.WriteLine("----------------------------");
            }

            while (true)
            {
                Console.WriteLine("Ingrese un Id");
                int id = int.Parse(Console.ReadLine());
                var equipo = await client.EquipoInfoAsync(new EquipoRequest { Id = id });
                Console.WriteLine($"Equipo: {equipo.Nombre}");
                Console.WriteLine($"Titulos: {equipo.Titulos}");
                Console.WriteLine($"Descensos: {equipo.Descensos}");
                Console.WriteLine($"Grande: {equipo.EsGrande}");
                Console.WriteLine($"Ubicacion: {equipo.Ubicacion}");
            }

        }
    }
}
