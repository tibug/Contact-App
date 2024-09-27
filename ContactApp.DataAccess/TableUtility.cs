namespace ContactApp.DataAccess;

public static class TableUtility
{
    public static DataTable CreateDataTable(List<int>? list)
    {
        var table = new DataTable();
        table.Columns.Add("Value", typeof(int));
        if (list != null)
        {
            foreach (var item in list)
            {
                table.Rows.Add(item);
            }
        }
        return table;
    }
}
