using System.Data;

namespace Tridenton.Core.Utilities.Collections;

public class DropdownDataTable : DataTable
{
	private readonly DataTable _tempCopy;

	/// <summary>
	/// Column of key value
	/// </summary>
	public string KeyColumnName { get; }

	/// <summary>
	/// Column of user-friendly value to be displayed in textbox
	/// </summary>
	public string ValueColumnName { get; }

	/// <summary>
	/// Names of columns that are hidden in the UI
	/// </summary>
	public string[] HiddenColumns { get; set; }

	/// <summary>
	/// Indices of columns that are hidden in the UI
	/// </summary>
	public List<int> HiddenColumnsIndices { get; private set; }

	/// <summary>
	/// Helpful additional properties dictionary
	/// </summary>
	public PropertiesCollection Properties { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="DropdownDataTable"/> with empty rows and columns
	/// </summary>
	public DropdownDataTable() : this(string.Empty, string.Empty, [], []) { }

	/// <summary>
	/// Initializes a new instance of <see cref="DropdownDataTable"/> without hidden columns
	/// </summary>
	/// <param name="keyColumnName">Column of key value</param>
	/// <param name="valueColumnName">Column of user-friendly value to be displayed in textbox</param>
	/// <param name="columns">Array of columns</param>
	public DropdownDataTable(string keyColumnName, string valueColumnName, string[] columns) : this(keyColumnName, valueColumnName, columns, []) { }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="keyColumnName"></param>
	/// <param name="valueColumnName"></param>
	/// <param name="dataTable"></param>
	public DropdownDataTable(string keyColumnName, string valueColumnName, string[] columns, string[] hiddenColumns, DataTable dataTable) : this(keyColumnName, valueColumnName, columns, hiddenColumns)
	{
		foreach (DataRow row in dataTable.Rows)
		{
			ImportRow(row);
		}
	}

	/// <summary>
	/// Initializes a new instance of <see cref="DropdownDataTable"/> with hidden columns
	/// </summary>
	/// <param name="keyColumnName">Column of key value</param>
	/// <param name="valueColumnName">Column of user-friendly value to be displayed in textbox</param>
	/// <param name="columns">Array of columns</param>
	/// <param name="hiddenColumns">Array of hidden columns</param>
	public DropdownDataTable(string keyColumnName, string valueColumnName, string[] columns, string[] hiddenColumns) : base()
	{
		_tempCopy = new();

		KeyColumnName = keyColumnName;
		ValueColumnName = valueColumnName;
		HiddenColumns = hiddenColumns;

		HiddenColumnsIndices = [];

		Properties = [];

		var index = 0;

		foreach (var column in columns.Union(hiddenColumns))
		{
			_ = Columns.Add(column);
			_ = _tempCopy.Columns.Add(column);

			if (index > columns.Length - 1)
			{
				HiddenColumnsIndices.Add(index);
			}

			index++;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="row"></param>
	/// <returns></returns>
	public object? GetKeyValue(DataRow row)
	{
		var column = Columns[KeyColumnName];

		if (column is null)
		{
			return null;
		}

		return row[column.Ordinal];
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="row"></param>
	/// <returns></returns>
	public object? GetValue(DataRow row)
	{
		var column = Columns[ValueColumnName];

		if (column is null)
		{
			return null;
		}

		return row[column.Ordinal];
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="columnName"></param>
	/// <param name="value"></param>
	public void Filter(string columnName, object? value)
	{
		var columnIndex = -1;

		for (var i = 0; i < Columns.Count; i++)
		{
			var column = Columns[i];

			if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase))
			{
				columnIndex = i;
				break;
			}
		}

		if (columnIndex == -1)
		{
			return;
		}

		if (_tempCopy.Rows.Count == 0)
		{
			foreach (DataRow row in Rows)
			{
				_tempCopy.ImportRow(row);
			}
		}

		Rows.Clear();

		foreach (DataRow row in _tempCopy.Rows)
		{
			var respectiveColumnValue = row.ItemArray[columnIndex]?.ToString();

			if (respectiveColumnValue != value?.ToString())
			{
				continue;
			}

			ImportRow(row);
		}
	}

	public new DropdownDataTable Copy()
	{
		var columns = new List<string>();

		foreach (DataColumn column in Columns)
		{
			columns.Add(column.ColumnName);
		}

		var copy = new DropdownDataTable(KeyColumnName, ValueColumnName, [.. columns])
		{
			HiddenColumnsIndices = HiddenColumnsIndices,
		};

		foreach (DataRow row in Rows)
		{
			copy.ImportRow(row);
		}

		return copy;
	}
}