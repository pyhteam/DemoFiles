using DemoFiles.Data;

namespace DemoFiles.Services
{
	public class FileService : IFileService
	{
		private readonly HMZContext _context;
		private readonly IConfiguration _configuration;
        string rootPath;
		public FileService( HMZContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
			rootPath  = _configuration.GetValue<string>("Storage:RootPath");
		}
		public Task<string> DeleteFileAsync(string filePath)
		{
			string fullPath = Path.Combine(rootPath, filePath);
			if(File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}
			return Task.FromResult(filePath);
		}

		public Task<Stream> GetFileAsync(string filePath)
		{
			string fullPath = Path.Combine(rootPath, filePath);
			if(!File.Exists(fullPath))
			{
				throw new FileNotFoundException("File not found", filePath);
			}
			return  Task.FromResult<Stream>(new FileStream(fullPath, FileMode.Open));
		}

		public Task<string> GetFolderSaveAsync(string filePath)
		{
			string fullPath = Path.Combine(rootPath, filePath);
			if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
			}
			return Task.FromResult(fullPath);
		}

		public async Task<string> SaveFileAsync(string filePath, Stream file)
		{
			string fullPath = Path.Combine(rootPath, filePath);
			if(!Directory.Exists(Path.GetDirectoryName(fullPath)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
			}
			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return filePath;
		}
	}
}
