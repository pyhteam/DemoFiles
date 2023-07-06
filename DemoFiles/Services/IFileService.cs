namespace DemoFiles.Services
{
	public interface IFileService
	{
		Task<MemoryStream> GetFileAsync(string filePath);
		Task<FileInfo> GetFileInfoAsync(MemoryStream memoryStream);
		Task DeleteFileAsync(IFormFile file);
		Task<string> DeleteFileAsync(string filePath);
		Task<string> SaveFileAsync(string filePath, IFormFile file);
	}
}
