namespace Zadanie3
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        private readonly IPrinter Printer = new Printer();
        private readonly IScanner Scanner = new Scanner();

        public int PrintCounter => Printer.Counter;
        public int ScanCounter => Scanner.Counter;

        public void PowerOff()
        {
            Printer.PowerOff();
            Scanner.PowerOff();
            base.PowerOff();
        }

        public void PowerOn()
        {
            Printer.PowerOn();
            Scanner.PowerOn();
            base.PowerOn();
        }

        public void Print(in IDocument document)
        {
            Printer.Print(document);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            Scanner.Scan(out document, formatType);
        }

        public void ScanAndPrint()
        {
            IDocument document;

            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}
