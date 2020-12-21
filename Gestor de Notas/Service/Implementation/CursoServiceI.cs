using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using Gestor_de_Notas.Dto;
using Gestor_de_Notas.Model;
using Gestor_de_Notas.Persistance;

namespace Gestor_de_Notas.Service.Implementation
{
    public class CursoServiceI : CursoService
    {
        private readonly ApplicationDbContext AppDbC;
        private readonly IMapper Imapper;
        private static int Id;
        public CursoServiceI(ApplicationDbContext _AppDbC, IMapper _Imapper)
        {
            AppDbC = _AppDbC;
            Imapper = _Imapper;
            Id = 0;
        }
        public CursoDto Create(CursoCreateDto model)
        {
            var ciclo = AppDbC.Ciclo.Single(x => x.CicloId == model.CicloId);
            var entry = new Curso
            {
                CursoId = Id++,
                CursoCodigo = model.CursoCodigo,
                CursoNombre = model.CursoNombre,
                CursoCreditos = model.CursoCreditos,
                CicloId = model.CicloId,
                Ciclo = ciclo
            };
            AppDbC.Curso.Add(entry);
            AppDbC.SaveChanges();
            return Imapper.Map<CursoDto>(entry);
        }

        public void Delete(int Id)
        {
            AppDbC.Remove(new Curso { CursoId = Id });
            AppDbC.SaveChanges();
        }

        public void Update(CursoUpdateDto model, int Id)
        {
            var ciclo = AppDbC.Ciclo.Single(x => x.CicloId == model.CicloId);
            var entry = AppDbC.Curso.Single(x => x.CursoId == Id);

            entry.CursoNombre = model.CursoNombre;
            entry.CursoCreditos = model.CursoCreditos;
            entry.CursoCantidadCampos = model.CursoCantidadCampos;
            entry.CursoPromedio = model.CursoPromedio;
            entry.CursoVez = model.CursoVez;
            entry.CicloId = model.CicloId;
            entry.Ciclo = ciclo;

            AppDbC.SaveChanges();
        }

        public List<CursoDto> GetCursosCiclo(int Id) 
        {
            return Imapper.Map<List<CursoDto>>(
                AppDbC.Curso
                .Include(x=>x.Campos)
                .Include(x=>x.Ciclo)
                .OrderByDescending(x => x.CicloId == Id)
                ) ;
        }

    }
}
