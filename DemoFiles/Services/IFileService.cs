namespace DemoFiles.Services
{
	public interface IFileService
	{
		Task<Stream> GetFileAsync(string filePath);
		Task<string> DeleteFileAsync(string filePath);
		Task<string> SaveFileAsync(string filePath, Stream file);
		Task<string> GetFolderSaveAsync(string filePath);
        
    }
}
