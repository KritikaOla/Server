namespace LibraryManagementAPI.Models
{
	public class LibraryManagementDatabaseSettings
	{
		public string ConnectionString { get; set; } = null!;
		public string DatabaseName { get; set; } = null!;
		public string BooksCollectionName { get; set; } = null!;
		public string UsersCollectionName { get; set; } = null!;
	}
}