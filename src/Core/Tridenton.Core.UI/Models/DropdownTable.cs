using System.Data;

namespace Tridenton.Core.UI;

public class DropdownTable : DataTable
{
    public List<DropdownTable> SubMenus { get; set; }

    public DropdownTable()
    {
        SubMenus = [];
    }
}