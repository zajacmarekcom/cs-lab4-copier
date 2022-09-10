using System;

namespace Zadanie2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var xerox = new MultifunctionalDevice();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2, IDocument.FormatType.JPG);

            xerox.Send(in doc2);

            xerox.ScanAndPrint();
            xerox.ScanAndFax();
            Console.WriteLine(xerox.Counter);
            Console.WriteLine(xerox.PrintCounter);
            Console.WriteLine(xerox.ScanCounter);
        }
    }
}
