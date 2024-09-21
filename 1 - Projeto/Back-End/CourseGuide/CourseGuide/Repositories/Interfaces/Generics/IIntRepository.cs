using CourseGuide.Objects.Generics;

namespace CourseGuide.Repositories.Interfaces.Generics
{
    // Interface específica para repositórios que lidam com entidades identificadas por int.
    public interface IIntRepository<T> : IGenericRepository<T> where T : class, IEntityInt
    {
        Task<T> GetById(int id);  // Método para obter um registro do tipo T por seu int Id.
    }
}
