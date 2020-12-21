using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestor_de_Notas.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestor_de_Notas.Persistance.Config
{
    public class CursoConfig
    {
        public CursoConfig(EntityTypeBuilder<Curso> entityBuilder)
        {

            entityBuilder.Property(x => x.CursoId).IsRequired();
            entityBuilder.Property(x => x.CursoCodigo).IsRequired();
            entityBuilder.Property(x => x.CursoNombre).IsRequired();
            entityBuilder.Property(x => x.CursoCreditos).IsRequired();
            entityBuilder.Property(x => x.CursoCantidadCampos).IsRequired();
            entityBuilder.Property(x => x.CursoPromedio).IsRequired();
            entityBuilder.Property(x => x.CursoVez).IsRequired();
            entityBuilder.HasOne(x => x.Ciclo).WithMany(x => x.Cursos).HasForeignKey(x => x.CursoId);
            entityBuilder.HasMany(x => x.Campos).WithOne(x => x.Curso).HasForeignKey(x => x.CursoId);
        }
    }
}
