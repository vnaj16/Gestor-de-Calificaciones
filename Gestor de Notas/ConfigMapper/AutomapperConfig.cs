using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gestor_de_Notas.Dto;
using Gestor_de_Notas.Model;

namespace Gestor_de_Notas.ConfigMapper
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Ciclo, CicloDto>();
            CreateMap<Curso, CursoDto>();
            CreateMap<Campo, CampoDto>();
            CreateMap<List<Ciclo>, List<CicloDto>>();
            CreateMap<List<Curso>, List<CursoDto>>();
            CreateMap<List<Campo>, List<CampoDto>>();
        }
    }
}
