using System;

namespace Zadanie3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2, IDocument.FormatType.JPG);

            xerox.ScanAndPrint();
            Console.WriteLine(xerox.Counter);
            Console.WriteLine(xerox.PrintCounter);
            Console.WriteLine(xerox.ScanCounter);

            var multi = new MultifunctionalDevice();
            multi.PowerOn();
            IDocument doc3 = new PDFDocument("aaa.pdf");
            multi.Print(in doc3);

            IDocument doc4;
            multi.Scan(out doc4, IDocument.FormatType.JPG);

            multi.Send(in doc4);

            multi.ScanAndPrint();
            multi.ScanAndFax();
            Console.WriteLine(multi.Counter);
            Console.WriteLine(multi.PrintCounter);
            Console.WriteLine(multi.ScanCounter);
        }
    }
}
