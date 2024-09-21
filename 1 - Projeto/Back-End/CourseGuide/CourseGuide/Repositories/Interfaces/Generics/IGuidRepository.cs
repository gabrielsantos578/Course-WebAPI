using CourseGuide.Objects.Generics;

namespace CourseGuide.Repositories.Interfaces.Generics
{
    // Interface específica para repositórios que lidam com entidades identificadas por Guid.
    public interface IGuidRepository<T> : IGenericRepository<T> where T : class, IEntityGuid
    {
        Task<T> GetById(Guid id);  // Método para obter um registro do tipo T por seu Guid.
    }
}
