using CourseGuide.Objects.Generics;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces.Generics.Methods;

namespace CourseGuide.Repositories.Interfaces
{
    // Interface para o repositório de mesas, herda de IGetByIdIntRepository para suporte a operações com ID int.
    public interface ITableRepository<T> : IGetByIdIntRepository<T> where T : class, IEntityInt
    {
        Task<IEnumerable<TableModel>> GetByRestaurant(int idRestaurant);  // Método para buscar mesas pelo restaurante.
    }
}
