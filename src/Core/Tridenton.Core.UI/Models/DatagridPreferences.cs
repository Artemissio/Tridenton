namespace Tridenton.Core.UI.Models;

public record DatagridPreferences
{
    public int PageSize { get; set; }
    public List<int> HiddenHeadersIndices { get; init; }
    public List<int> HiddenCellsIndices { get; init; }

    public DatagridPreferences()
    {
        PageSize = 100;
        HiddenHeadersIndices = [];
        HiddenCellsIndices = [];
    }
}