
using System.Drawing;
using Spire.Pdf;
using Spire.Pdf.General.Find;
using Spire.Pdf.Graphics;

namespace DemoFiles
{
    public static class PdfExtensions
    {

        public static void ReplaceTextWithText(string inputPath, string outputPath, string searchText, string replaceText)
        {
            //Load the sample document file
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(inputPath);

            //Searched the text from the first page of the sample document
            PdfPageBase page = doc.Pages[0];
            PdfTextFindCollection collection = page.FindText(searchText, TextFindParameter.IgnoreCase);
            foreach (PdfTextFind find in collection.Finds)
            {
                //Replace the text with the new string
                find.ApplyRecoverString(replaceText, Color.White);
            }

            //Save the document to file
            doc.SaveToFile(outputPath);
        }
		public static void ReplaceTextWithImage(string inputPath, string outputPath, string searchText, string imagePath)
		{
			//Load the sample document file
			PdfDocument doc = new PdfDocument();
			doc.LoadFromFile(inputPath);

			//Load the image from the specified path
			Image replaceImage = Image.FromFile(imagePath);

			//Convert the System.Drawing.Image to a Spire.Pdf.Graphics.PdfImage
			PdfImage pdfImage = PdfImage.FromImage(replaceImage);

			//Searched the text from the first page of the sample document
			PdfPageBase page = doc.Pages[0];
			PdfTextFindCollection collection = page.FindText(searchText, TextFindParameter.IgnoreCase);
			foreach (PdfTextFind find in collection.Finds)
			{
				//Replace the text with the new image
				float x = find.Position.X;
				float y = find.Position.Y + find.Size.Height + 5; // move the image down by one line

				//Draw the image on the page
				page.Canvas.DrawImage(pdfImage, x, y, 80, 80);
			}

			//Save the document to file
			doc.SaveToFile(outputPath);
		}
	}

}
