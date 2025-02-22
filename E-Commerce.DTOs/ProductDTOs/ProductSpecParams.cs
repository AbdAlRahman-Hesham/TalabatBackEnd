namespace E_Commerce.DTOs.ProductDTOs;

public class ProductSpecParams
{
    public int? BrandId { get; set; }
    public int? CategorityId { get; set; }
    public string? Sort { get; set; }
    public string? SortOrder { get; set; }
    public int? PageSize
    {
        get => pageSize;
        set => pageSize = value.HasValue ? (value.Value <= MaxPageSize ? value.Value : MaxPageSize) : MaxPageSize;
    }
    public int? PageIndex { get; set; } = 1;
    public string? SearchName { get; set; }


    private const int MaxPageSize = 10;
    private int pageSize = MaxPageSize;
}
