namespace API.DTOs.Category
{
    public record DisplayCategoryDTO
    (
        int Id,
        string Name,
        string Description
    );
    public record AddCategoryDTO
    (
        string Name,
        string Description
    );
    public record UpdateCategoryDTO
    (
        int Id,
        string Name,
        string Description
    );
}
