using CourseGuide.Objects.Generics;

namespace CourseGuide.Repositories.Interfaces.Generics.Methods
{
    // Interface específica para repositórios que lidam com entidades identificadas por Guid.
    public interface IGetByIdGuidRepository<T> : IGenericRepository<T> where T : class, IEntityGuid
    {
        Task<T> GetById(Guid id);  // Método para obter um registro do tipo T por seu Guid.
    }
}
