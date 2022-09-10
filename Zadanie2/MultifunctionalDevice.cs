using System;
namespace Zadanie2
{
    public class MultifunctionalDevice : BaseDevice, IPrinter, IScanner, IFax
    {
        public int PrintCounter { get; private set; }
        public int ScanCounter { get; private set; }
        public int FaxCounter { get; private set; }

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

        public void Send(in IDocument document)
        {
            if (GetState() == IDevice.State.off)
                return;
            FaxCounter++;
            Console.WriteLine($"{DateTime.Now.ToString("0:G")} Sent by fax: {document.GetFileName()}");
        }

        public void ScanAndFax()
        {
            IDocument document;

            Scan(out document, IDocument.FormatType.JPG);
            Send(document);
        }
    }
}
