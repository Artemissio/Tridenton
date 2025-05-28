using System.Data;

namespace Tridenton.Core.Utilities;

public class ExtendedDataTable : DataTable
{
    private readonly DataTable _tempCopy;
    
    /// <summary>
    /// Indices of columns that are hidden in the UI
    /// </summary>
    private readonly List<int> _hiddenColumnsIndices;

    public ExtendedDataTable() : this([]) { }

    public ExtendedDataTable(string[] columns) : this(columns, []) { }

    public ExtendedDataTable(string[] columns, string[] hiddenColumns) : base()
    {
        _tempCopy = new();
        _hiddenColumnsIndices = [];
        
        var index = 0;

        foreach (var column in columns.Union(hiddenColumns))
        {
            _ = Columns.Add(column);
            _ = _tempCopy.Columns.Add(column);

            if (index > columns.Length - 1)
            {
                _hiddenColumnsIndices.Add(index);
            }

            index++;
        }
    }
}