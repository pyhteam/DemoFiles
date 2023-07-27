
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Kernel.Colors;
using System.Text;

namespace DemoFiles
{
    public class HMZHelperPDF
    {
		public static void ReplaceTextWithImage(string inputPath, string outputPath, string searchText, string imagePath, bool isFirst = false)
		{
			PdfDocument pdfDoc = new PdfDocument(new PdfReader(inputPath), new PdfWriter(outputPath));
			PdfCanvasProcessor processor;
			try
			{

				for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
				{
					PdfPage page = pdfDoc.GetPage(i);
					CustomLocationTextExtractionStrategy strategy = new CustomLocationTextExtractionStrategy();
					processor = new PdfCanvasProcessor(strategy);
					processor.ProcessPageContent(page);
					List<TextInfo> textInfos = strategy.GetTextInfos();
					for (int j = 0; j < textInfos.Count - searchText.Length + 1; j++)
					{
						bool found = true;
						for (int k = 0; k < searchText.Length; k++)
						{
							if (textInfos[j + k].Text != searchText[k].ToString())
							{
								found = false;
								break;
							}
						}
						if (found)
						{
							Rectangle startRect = textInfos[j].Rect;
							Rectangle endRect = textInfos[j + searchText.Length - 1].Rect;
							float x = startRect.GetX();
							float y = startRect.GetY();
							float width = endRect.GetX() + endRect.GetWidth() - startRect.GetX();
							float height = startRect.GetHeight();

							ImageData imageData = ImageDataFactory.Create(imagePath);
							Image img = new Image(imageData);

							float originalWidth = img.GetImageWidth();
							float originalHeight = img.GetImageHeight();

							// Tính tỷ lệ co giãn theo chiều dọc và chiều ngang
							float targetSize = 110; // Square pixels
							float ratioX = (float)targetSize / originalWidth;
							float ratioY = (float)targetSize / originalHeight;

							// Tính tỷ lệ co giãn tối thiểu
							float ratio = Math.Min(ratioX, ratioY);

							// Tính kích thước mới dựa vào tỷ lệ co giãn
							int newWidth = (int)(originalWidth * ratio);
							int newHeight = (int)(originalHeight * ratio);

							PdfCanvas canvas = new PdfCanvas(page);
							// margin-bottom  50px : y-50
							canvas.AddImageWithTransformationMatrix(imageData, newWidth, 0, 0, newHeight, x, y - 60);
							if (isFirst)
							{
								break;
							}
						}
					}
				}
				pdfDoc.Close();
			}
			catch (Exception ex)
			{
				/// Khi mà bị lỗi thì tắt proccess hoặc kết thúc luôn cái PDF đi
				// Release the write access to the output file before throwing the exception
				if (pdfDoc != null)
				{
					pdfDoc.Close();
				}
				// Restore the original input file to its original state
				File.Copy(inputPath, outputPath, true);
				throw;
			}
		}
		public void ReplaceTextWithText(string inputPath, string outputPath, string searchText, string replaceText)
        {

            PdfWriter writer = new PdfWriter(outputPath);
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(inputPath), writer);
            PdfFont font = PdfFontFactory.CreateFont("C:/Windows/Fonts/times.ttf", PdfEncodings.IDENTITY_H);
            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                PdfPage page = pdfDoc.GetPage(i);
                CustomLocationTextExtractionStrategy strategy = new CustomLocationTextExtractionStrategy();
                PdfCanvasProcessor processor = new PdfCanvasProcessor(strategy);
                processor.ProcessPageContent(page);
                List<TextInfo> textInfos = strategy.GetTextInfos();
                for (int j = 0; j < textInfos.Count - searchText.Length + 1; j++)
                {
                    bool found = true;
                    for (int k = 0; k < searchText.Length; k++)
                    {
                        if (textInfos[j + k].Text != searchText[k].ToString())
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        Rectangle startRect = textInfos[j].Rect;
                        Rectangle endRect = textInfos[j + searchText.Length - 1].Rect;
                        float x = startRect.GetX();
                        float y = startRect.GetY();
                        float width = endRect.GetX() + endRect.GetWidth() - startRect.GetX();
                        //float height = textInfos[j].Rect.GetHeight();
                        int fontSize = 11;
                        float height = fontSize+2;

                        PdfCanvas canvas = new PdfCanvas(page);
                        canvas.SaveState();
                        canvas.Rectangle(startRect.GetX(), startRect.GetY()-3, width, height);
                        canvas.Clip();
                        canvas.EndPath();
                        canvas.SetColor(ColorConstants.WHITE, true);
                        canvas.Rectangle(startRect.GetX(), startRect.GetY()-3, width, height);
                        canvas.Fill();
                        canvas.RestoreState();

                        canvas.BeginText().SetFontAndSize(font, fontSize).MoveText(x, y).ShowText(replaceText).EndText();
                    }
                }
            }
            pdfDoc.Close();
        }

    }

        public class CustomLocationTextExtractionStrategy : LocationTextExtractionStrategy
    {
        private List<TextInfo> textInfos = new List<TextInfo>();

        public override void EventOccurred(IEventData data, EventType type)
        {
            base.EventOccurred(data, type);
            if (type == EventType.RENDER_TEXT)
            {
                TextRenderInfo renderInfo = (TextRenderInfo)data;
                string text = renderInfo.GetText();
                for (int i = 0; i < text.Length; i++)
                {
                    TextInfo textInfo = new TextInfo();
                    textInfo.Text = text[i].ToString();
                    textInfo.Rect = renderInfo.GetCharacterRenderInfos()[i].GetBaseline().GetBoundingRectangle();
                    textInfos.Add(textInfo);
                }
            }
        }

        public List<TextInfo> GetTextInfos()
        {
            return textInfos;
        }
    }

    public class TextInfo
    {
        public string Text { get; set; }
        public Rectangle Rect { get; set; }
    }

}
