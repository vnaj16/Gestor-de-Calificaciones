using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestor_de_Notas.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestor_de_Notas.Persistance.Config
{
    public class CampoConfig
    {
        public CampoConfig(EntityTypeBuilder<Campo> entityBuilder)
        {
            entityBuilder.HasKey(x => new {x.CampoTipo, x.CampoNumero, x.CursoId });
            entityBuilder.Property(x => x.CampoTipo).IsRequired();
            entityBuilder.Property(x => x.CampoNumero).IsRequired();
            entityBuilder.Property(x => x.CampoDescripcion).IsRequired();
            entityBuilder.Property(x => x.CampoPeso).IsRequired();
            entityBuilder.Property(x => x.CampoNota).IsRequired();
            entityBuilder.Property(x => x.CampoRellenado).IsRequired();
            entityBuilder.HasOne(x => x.Curso).WithMany(x => x.Campos);
            //HasData -> Probar obj (ejm: Campo)
        }
    }
}
