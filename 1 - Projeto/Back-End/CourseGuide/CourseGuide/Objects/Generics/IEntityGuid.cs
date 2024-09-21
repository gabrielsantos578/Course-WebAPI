namespace CourseGuide.Objects.Generics
{
    // Interface que define uma entidade com um identificador do tipo Guid.
    public interface IEntityGuid
    {
        Guid Id { get; set; } // Propriedade Id, que deve ser implementada em classes que representam entidades.
    }
}
