namespace DemoFiles.Services
{
	public class FileService : IFileService
	{
		public Task DeleteFileAsync(IFormFile file)
		{
			throw new NotImplementedException();
		}

		public Task<string> DeleteFileAsync(string filePath)
		{
			throw new NotImplementedException();
		}

		public Task<MemoryStream> GetFileAsync(string filePath)
		{
			throw new NotImplementedException();
		}

		public Task<FileInfo> GetFileInfoAsync(MemoryStream memoryStream)
		{
			throw new NotImplementedException();
		}

		public Task<string> SaveFileAsync(string filePath, IFormFile file)
		{
			throw new NotImplementedException();
		}
	}
}
