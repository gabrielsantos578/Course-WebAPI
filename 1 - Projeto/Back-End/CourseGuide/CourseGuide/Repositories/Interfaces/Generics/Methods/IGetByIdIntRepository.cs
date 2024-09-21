using CourseGuide.Objects.Generics;

namespace CourseGuide.Repositories.Interfaces.Generics.Methods
{
    // Interface específica para repositórios que lidam com entidades identificadas por int.
    public interface IGetByIdIntRepository<T> : IGenericRepository<T> where T : class, IEntityInt
    {
        Task<T> GetById(int id);  // Método para obter um registro do tipo T por seu int Id.
    }
}
