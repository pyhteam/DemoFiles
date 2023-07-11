﻿using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Replacing;

namespace DemoFiles
{
	public class HMZHelper
	{
		public static void ExportWordToPdf(string docxFilePath, string pdfFilePath, string imgPath, float width, float height,string key)
		{
			// Net core 
			// sử dụng Aspose.Words này để xuất ra file 
			// Tìm kiém từ này " HMZKey " và chèn hình ảnh vào chỗ đó.
			// Khởi tạo Document từ tệp Word đầu vào
			Document doc = new Document(docxFilePath);
			// Tìm và lấy văn bản trong tài liệu
			FindReplaceOptions options = new FindReplaceOptions();
			NodeCollection nodes = doc.GetChildNodes(NodeType.Run, true);
			foreach (Run run in nodes)
			{
				if (run.Text.Contains(key))
				{
					// Chèn hình ảnh vào tài liệu
					Shape imgShape = new Shape(doc, ShapeType.Image);
					imgShape.ImageData.SetImage(imgPath);
					imgShape.Width = width;
					imgShape.Height = height;

					// Thay thế văn bản bằng hình ảnh
					run.ParentParagraph.InsertBefore(imgShape, run);
					run.Text = run.Text.Replace(key, string.Empty);
				}
			}

			// Lưu tài liệu dưới dạng PDF
			doc.Save(pdfFilePath, SaveFormat.Pdf);
		}
	}
}
