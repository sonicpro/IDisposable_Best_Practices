<Query Kind="Program" />

void Main()
{
	for (int i = 0; i < 1000; i++)
	{
		//using (var state = new DatabaseState())
		var state = new DatabaseState();
		{
			state.GetDate().Dump();
		}
	}
}

// Define other methods and classes here
public class DatabaseState: IDisposable
{
	private SqlConnection _connection;
	
	public string GetDate()
	{
		if (_connection == null)
		{
			_connection = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Database=master;Integrated Security=SSPI;App=LINQPad; Max pool size=132;Connection timeout=2;");
			_connection.Open();
		}
		using (var command = _connection.CreateCommand())
		{
			command.CommandText = "SELECT getdate()";
			return command.ExecuteScalar().ToString();
		}
	}
	
	public void Dispose()
	{
		string.Format("Disposing SqlConnection: {0}", _connection.GetHashCode()).Dump();
		_connection.Close();
		_connection.Dispose();
	}
}