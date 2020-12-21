using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestor_de_Notas.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestor_de_Notas.Persistance.Config
{
    public class CicloConfig
    {
        public CicloConfig(EntityTypeBuilder<Ciclo> entityBuilder) {

            entityBuilder.Property(x => x.CicloId).IsRequired();
            entityBuilder.Property(x => x.CicloCantidadCursos).IsRequired();
            entityBuilder.Property(x => x.CicloPromedio).IsRequired();
            entityBuilder.Property(x => x.CicloPromedioBeca).IsRequired();
            entityBuilder.HasMany(x => x.Cursos).WithOne(x => x.Ciclo).HasForeignKey(x => x.CicloId);

        }
    }
}
