using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; }
        public int ScanCounter { get; private set; }

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.off)
                return;
            PrintCounter++;
            Console.WriteLine($"{DateTime.Now.ToString("0:G")} Print: {document.GetFileName()}");
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = null;
            if (GetState() == IDevice.State.off)
                return;
            ScanCounter++;
            var extention = formatType.ToString().ToLower();
            var filename = $"ImageScan{ScanCounter}.{extention}";
            Console.WriteLine($"{DateTime.Now.ToString("0:G")} Scan: {filename}");

            document = new ImageDocument(filename);
        }

        public void ScanAndPrint()
        {
            IDocument document;

            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}
