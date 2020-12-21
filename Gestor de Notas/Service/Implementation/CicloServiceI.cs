using System.Linq;
using AutoMapper;
using Gestor_de_Notas.Dto;
using Gestor_de_Notas.Model;
using Gestor_de_Notas.Persistance;

namespace Gestor_de_Notas.Service.Implementation
{
    public class CicloServiceI : CicloService
    {
        private readonly ApplicationDbContext AppDbC;
        private readonly IMapper Imapper;
        private static int Id;
        public CicloServiceI(ApplicationDbContext _AppDbC, IMapper _Imapper)
        {
            AppDbC = _AppDbC;
            Imapper = _Imapper;
            Id = 0;
        }
        public CicloDto Create(CicloCreateDto model)
        {
            var entry = new Ciclo
            {
                CicloId = Id++,
                CicloCantidadCursos = model.CicloCantidadCursos,
                CicloPromedio = model.CicloPromedio,
                CicloPromedioBeca = model.CicloPromedioBeca
            };

            AppDbC.Ciclo.Add(entry);
            AppDbC.SaveChanges();
            return Imapper.Map<CicloDto>(entry);
        }

        public void Delete(int Id)
        {
            AppDbC.Remove(new Ciclo{ CicloId = Id});
            AppDbC.SaveChanges();
        }

        public void Update(CicloUpdateDto model, int Id)
        {
            var entry = AppDbC.Ciclo.Single(x => x.CicloId == Id);

            entry.CicloPromedio = model.CicloPromedio;
            entry.CicloPromedioBeca = model.CicloPromedioBeca;
            entry.CicloCantidadCursos = model.CicloCantidadCursos;
            AppDbC.SaveChanges();
        }

        ///funcionalidades extra
        public void UpdatePonderado()
        {

        }

    }
}
