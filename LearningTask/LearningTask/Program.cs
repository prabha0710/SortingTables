public class TableSorter
{
	public class Table
	{
		public string Name;
		public string[] Parents;

		public Table(string name, string[] parents)
		{
			Name = name;
			Parents = parents;
		}
	}

	public static void Main()
	{
		Table[] tables = new Table[]
		{
			new Table("A", new string[] { "B" }),
			new Table("B", new string[] { }),
			new Table("C", new string[] { "E" }),
			new Table("D", new string[] { "C", "A" }),
			new Table("E", new string[] { })
		};

		try
		{
			List<string> sorted = SortTables(tables);
			Console.WriteLine("Sorted Tables (Parent First):");
			foreach(string name in sorted)
			{
				Console.WriteLine(name);
			}
		}
		catch(Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
	}

	public static List<string> SortTables(Table[] tables)
	{
		List<string> sorted = new List<string>();
		List<string> visited = new List<string>();
		List<string> path = new List<string>(); 

		foreach(Table table in tables)
		{
			if(!visited.Contains(table.Name))
			{
				Visit(table.Name, tables, sorted, visited, path);
			}
		}

		return sorted;
	}

	private static void Visit(string name, Table[] tables, List<string> sorted,
							  List<string> visited, List<string> path)
	{
		if(path.Contains(name))
			throw new Exception("Cyclic dependency detected at " + name);

		if(visited.Contains(name))
			return;

		path.Add(name);

		Table current = FindTable(name, tables);
		if(current != null)
		{
			foreach(string parent in current.Parents)
			{
				Visit(parent, tables, sorted, visited, path);
			}
		}

		path.Remove(name);
		visited.Add(name);
		sorted.Add(name);
	}

	private static Table FindTable(string name, Table[] tables)
	{
		foreach(Table table in tables)
		{
			if(table.Name == name)
				return table;
		}
		return null;
	}
}
