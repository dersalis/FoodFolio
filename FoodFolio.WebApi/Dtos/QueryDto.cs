using FoodFolio.WebApi.Enums;

namespace FoodFolio.WebApi.Dtos;

public class QueryDto
{
    public string SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}

