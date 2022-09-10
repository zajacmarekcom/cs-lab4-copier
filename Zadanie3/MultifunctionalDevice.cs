namespace Zadanie3
{
    public class MultifunctionalDevice : BaseDevice, IPrinter, IScanner
    {
        private readonly Printer Printer = new Printer();
        private readonly Scanner Scanner = new Scanner();
        private readonly Fax Fax = new Fax();

        public int PrintCounter => Printer.PrintCounter;
        public int ScanCounter => Scanner.ScanCounter;
        public int FaxCounter => Fax.FaxCounter;

        public void PowerOff()
        {
            Printer.PowerOff();
            Scanner.PowerOff();
            Fax.PowerOff();
            base.PowerOff();
        }

        public void PowerOn()
        {
            Printer.PowerOn();
            Scanner.PowerOn();
            Fax.PowerOn();
            base.PowerOn();
        }

        public void Print(in IDocument document)
        {
            Printer.Print(document);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            Scanner.Scan(out document, formatType);
        }

        public void Send(in IDocument document)
        {
            Fax.Send(document);
        }

        public void ScanAndPrint()
        {
            IDocument document;

            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }

        public void ScanAndFax()
        {
            IDocument document;

            Scan(out document, IDocument.FormatType.JPG);
            Send(document);
        }
    }
}
