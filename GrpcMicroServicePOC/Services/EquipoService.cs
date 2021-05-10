using Grpc.Core;
using GrpcMicroServicePOC.Protos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcMicroServicePOC.Services
{
    public class EquipoService : Equipo.EquipoBase
    {

        public override Task<EquipoReply> EquipoInfo(EquipoRequest request, ServerCallContext context)
        {
            var equipos = MisEquipos();

            if (request.Id < equipos.Count)
            {
                return Task.FromResult(equipos[request.Id]);
            }

            throw new System.Exception("Bad Request");
        }

        public override async Task GetAllTeams(EmptyRequest request, IServerStreamWriter<EquipoReply> responseStream, ServerCallContext context)
        {
            var equipos = MisEquipos();
            foreach (var equipo in equipos)
            {
                await responseStream.WriteAsync(equipo);
            }
        }

        private List<EquipoReply> MisEquipos()
        {
            var boca = new EquipoReply
            {
                Descensos = 0,
                EsGrande = true,
                Nombre = "Boca Juniors",
                Titulos = 70,
                Ubicacion = EquipoReply.Types.Ubicacion.Capital
            };
            var racing = new EquipoReply
            {
                Descensos = 1,
                EsGrande = false,
                Nombre = "Racing",
                Titulos = 37,
                Ubicacion = EquipoReply.Types.Ubicacion.BuenosAires
            };
            var river = new EquipoReply
            {
                Descensos = 1,
                EsGrande = true,
                Nombre = "River Plate",
                Titulos = 66,
                Ubicacion = EquipoReply.Types.Ubicacion.Capital
            };
            var independiente = new EquipoReply
            {
                Descensos = 1,
                EsGrande = true,
                Nombre = "Independiente",
                Titulos = 45,
                Ubicacion = EquipoReply.Types.Ubicacion.BuenosAires
            };
            var rosario = new EquipoReply
            {
                Descensos = 2,
                EsGrande = false,
                Nombre = "Club Rosario Puerto Belgrano",
                Titulos = 8,
                Ubicacion = EquipoReply.Types.Ubicacion.Interior
            };
            return new List<EquipoReply>
            {
                boca, river, racing, independiente,rosario
            };
        }
    }
}
