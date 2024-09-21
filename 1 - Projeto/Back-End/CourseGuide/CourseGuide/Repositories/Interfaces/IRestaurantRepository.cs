using CourseGuide.Objects.Generics;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces.Generics.Methods;

namespace CourseGuide.Repositories.Interfaces
{
    // Interface para o repositório de restaurantes, herda de IGetByIdIntRepository para suporte a operações com ID int.
    public interface IRestaurantRepository<T> : IGetByIdIntRepository<T> where T : class, IEntityInt
    {
        Task<IEnumerable<RestaurantModel>> GetByOwner(int idOwner);  // Método para buscar restaurantes pelo dono.
    }
}
