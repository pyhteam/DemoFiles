

using Spire.Pdf;
using Spire.Pdf.Exporting;
using Spire.Pdf.Graphics;

namespace DemoFiles
{
	public class HMZHelperPDF
	{

		public static void ReplaceImage(string filePath, string imagePath, string pdfOutputPath)
		{
			
		}
		public static void ReplaceImagesInPdf(string inputPdfPath, string imageFilePath, string outputPdfPath)
		{
			PdfDocument document = new PdfDocument();
			document.LoadFromFile(inputPdfPath);

			foreach (PdfPageBase page in document.Pages)
			{
				// Get the existing images on the page
				PdfImageInfo[] images = page.ImagesInfo;

				foreach (PdfImageInfo imageInfo in images)
				{
					// Replace the image with the new image
					PdfImage newImage = PdfImage.FromFile(imageFilePath);
					// set width and height
					int index = imageInfo.Index;
					page.ReplaceImage(index, newImage);
				}
			}

			document.SaveToFile(outputPdfPath);
			document.Close();
		}


	}

}
