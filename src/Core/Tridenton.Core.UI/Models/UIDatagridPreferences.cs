namespace Tridenton.Core.UI.Models;

public record UIDatagridPreferences
{
    public int PageSize { get; set; }
    public List<int> HiddenHeadersIndices { get; init; }
    public List<int> HiddenCellsIndices { get; init; }

    public UIDatagridPreferences()
    {
        PageSize = PaginationConstants.DefaultPageSize;
        HiddenHeadersIndices = [];
        HiddenCellsIndices = [];
    }
}