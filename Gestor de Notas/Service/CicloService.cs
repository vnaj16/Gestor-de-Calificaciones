using Gestor_de_Notas.Dto;

namespace Gestor_de_Notas.Service
{
    public interface CicloService
    {
        CicloDto Create(CicloCreateDto model);
        void Update(CicloUpdateDto model, int Id);
        void Delete(int Id);

        ///delarar implementaciones extra

    }
}
