using iTextSharp.text;
using iTextSharp.text.pdf;
using StickerGenerator.Models;

namespace StickerGenerator.Services
{
    public class PDFGenerator
    {
        float marginLeft = 0;
        float marginTop = 0;
        float marginRight = 0;
        float marginBottom = 0;

        int stickerRowsInPage = 1;
        int stickerColsInPage = 1;

        float stickerHeight = 0;

        string documentName = "";
        string outputPath = "";

        PageSettings pageSettings = new PageSettings();

        private readonly StickerService _stickerService;

        public PDFGenerator(StickerService stickerService)
        {
            _stickerService = stickerService;
        }

        public string GenerateDocument(PageSettings settings)
        {
            pageSettings = settings;
            SetMargins();
            SetRowsCols();
            SetDocumentName();
            Document document = new Document(PageSize.A4, marginLeft, marginRight, marginTop, marginBottom);
            PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
            document.Open();


            PdfPTable table = new PdfPTable(stickerColsInPage);
            table.TotalWidth = document.PageSize.Width - marginLeft - marginRight;
            table.LockedWidth = true;

            float tableHeight = document.PageSize.Height - marginTop - marginBottom;
            stickerHeight = tableHeight / stickerRowsInPage;

            foreach (var item in _stickerService.Stickers)
            {
                PdfPTable innerTable = CreateInnerTable(item.Sticker);
                for (int i = 0; i < item.Amount; i++)
                {
                    PdfPCell cell = new PdfPCell(innerTable);
                    cell.FixedHeight = stickerHeight;
                    table.AddCell(cell);
                }
            }
            document.Add(table);
            document.Close();
            return documentName;
        }

        private void SetMargins()
        {
            marginLeft = pageSettings.MarginLeft;
            marginRight = pageSettings.MarginRight;
            marginTop = pageSettings.MarginTop;
            marginBottom = pageSettings.MarginBottom;
        }

        private void SetRowsCols()
        {
            stickerRowsInPage = pageSettings.Rows;
            stickerColsInPage = pageSettings.Cols;
        }


        private void SetDocumentName()
        {
            documentName = "stickers_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            outputPath = "/data/" + documentName;
        }
        private PdfPTable CreateInnerTable(Sticker sticker)
        {
            PdfPTable innerTable = new PdfPTable(2);

            PdfPCell countPriceCell = new PdfPCell(new Phrase(sticker.Amount + "x " + sticker.Price.ToString()));
            countPriceCell.HorizontalAlignment = Element.ALIGN_CENTER;
            countPriceCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            countPriceCell.Colspan = 2;

            PdfPCell codeCell = new PdfPCell(new Phrase(sticker.Name));
            codeCell.HorizontalAlignment = Element.ALIGN_LEFT;
            codeCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell sumCell = new PdfPCell(new Phrase(sticker.FinalPrice.ToString()));
            sumCell.HorizontalAlignment = Element.ALIGN_CENTER;
            sumCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            sumCell.Phrase.Font.SetStyle(Font.BOLD);

            countPriceCell.FixedHeight = codeCell.FixedHeight = stickerHeight / 2;

            innerTable.AddCell(countPriceCell);
            innerTable.AddCell(codeCell);
            innerTable.AddCell(sumCell);
            return innerTable;
        }
    }
}
