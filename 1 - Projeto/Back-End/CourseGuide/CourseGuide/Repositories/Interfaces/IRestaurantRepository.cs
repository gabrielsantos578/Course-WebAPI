using CourseGuide.Objects.Generics;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces.Generics;

namespace CourseGuide.Repositories.Interfaces
{
    // Interface para o repositório de restaurantes, herda de IIntRepository para suporte a operações com ID int.
    public interface IRestaurantRepository<T> : IIntRepository<T> where T : class, IEntityInt
    {
        Task<IEnumerable<RestaurantModel>> GetByOwner(int idOwner);  // Método para buscar restaurantes pelo dono.
    }
}
