using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Gestor_de_Notas.Dto;
using Gestor_de_Notas.Persistance;
using Gestor_de_Notas.Model;

namespace Gestor_de_Notas.Service.Implementation
{
    public class CampoServiceI : CampoService
    {
        private readonly ApplicationDbContext AppDbC;
        private readonly IMapper Imapper;
        public CampoServiceI(ApplicationDbContext _AppDbC, IMapper _Imapper)
        {
            AppDbC = _AppDbC;
            Imapper = _Imapper;
        }
        public CampoDto Create(CampoCreateDto model)
        {
            var curso = AppDbC.Curso.Single(x => x.CursoId == model.CursoId);
            var entry = new Campo
            {
                CampoTipo = model.CampoTipo,
                CampoPeso = model.CampoPeso,
                CampoNumero = model.CampoNumero,
                CampoDescripcion = model.CampoDescripcion,
                CursoId = model.CursoId,
                Curso = curso,
                CampoRellenado = false

            };
            AppDbC.Campo.Add(entry);
            AppDbC.SaveChanges();
            return Imapper.Map<CampoDto>(entry);

        }

        public void Delete(string Tipo,int Numero, int Id)
        {
            AppDbC.Remove(new Campo { CursoId = Id,CampoTipo = Tipo, CampoNumero = Numero});
            //manera 2:
            //var delete = AppDbC.Campo.Single(x => x.CursoId == Id && x.CampoTipo == Tipo && x.CampoNumero == Numero);
            //AppDbC.Campo.Remove(delete);
           
            AppDbC.SaveChanges();
        }

        public void Update(CampoUpdateDto model, string Tipo, int Numero, int Id)
        {
            var entry = AppDbC.Campo.Single(x => x.CursoId == Id && x.CampoTipo == Tipo && x.CampoNumero == Numero);
            entry.CampoTipo = model.CampoTipo;
            entry.CampoNumero = model.CampoNumero;
            entry.CampoDescripcion = model.CampoDescripcion;
            entry.CampoPeso = model.CampoPeso;
            entry.CampoNota = model.CampoNota;
            entry.CampoRellenado = model.CampoRellenado;
            AppDbC.SaveChanges();
        }
    }
}
