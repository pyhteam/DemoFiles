using System.Diagnostics;
using DemoFiles.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoFiles.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilesController : ControllerBase
	{
		private readonly IFileService _fileService;
        HMZHelperPDF HMZHelperPDF = new HMZHelperPDF();
		public FilesController(IFileService _fileService)
		{
			this._fileService = _fileService;
		}

		[HttpPost("signature/{key}")]
		public  IActionResult Post(string key)
		{
			//string filePath = @"E:\Don_Ly_Hon.pdf";
			string filePath = @"E:\test.pdf";
			string pdfOutput = @"E:\HMZOutput\"+Guid.NewGuid().ToString()+".pdf";
			string imageFilePath = @"E:\signature.png";
			try
			{
				// SignatureKey
				//HMZHelper.ExportToPdf(filePath, imageFilePath, pdfOutput);
				PdfExtensions.ReplaceTextWithImage(filePath, filePath, key, imageFilePath);
				//HMZHelperPDF.ReplaceTextWithImage(filePath, filePath, key, imageFilePath);

				// open the file to see the result
				Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
				return Ok(filePath);

			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpPost("signature/text/{key}")]
		public IActionResult PostText(string key, [FromBody] string text)
		{
			string filePath = @"E:\Don_Ly_Hon_Key.pdf";
			string pdfOutput = @"E:\HMZOutput\" + Guid.NewGuid().ToString() + ".pdf";

			try
			{
				// SignatureKey
				HMZHelperPDF.ReplaceTextWithText(filePath, pdfOutput, key, text);
				//PdfExtensions.ReplaceTextWithText(filePath, pdfOutput, key, text);
				// open the file to see the result
				Process.Start(new ProcessStartInfo(pdfOutput) { UseShellExecute = true });
				return Ok(pdfOutput);

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
