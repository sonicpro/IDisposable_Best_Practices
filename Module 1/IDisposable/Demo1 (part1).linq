<Query Kind="Program" />

void Main()
{
	using (var state = new DatabaseState())
	{
		state.GetDate().Dump();
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
			_connection = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Database=master;Integrated Security=SSPI;App=LINQPad;");
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