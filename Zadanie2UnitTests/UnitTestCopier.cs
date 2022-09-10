using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Zadanie2.UnitTests
{

    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multifunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, cmultifunctionalDevice.GetState());
        }


        // weryfikacja, czy po wywo³aniu metody `Print` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                cmultifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Print` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                cmultifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                cmultifunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send_DeviceOff()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                cmultifunctionalDevice.Send(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send_DeviceOn()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                cmultifunctionalDevice.Send(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                cmultifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywo³anie metody `Scan` z parametrem okreœlaj¹cym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                cmultifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                cmultifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                cmultifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie pojawiaj¹ siê s³owa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                cmultifunctionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndFax_DeviceOn()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                cmultifunctionalDevice.ScanAndFax();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // ani s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                cmultifunctionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndFax_DeviceOff()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                cmultifunctionalDevice.ScanAndFax();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            cmultifunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            cmultifunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            cmultifunctionalDevice.Print(in doc3);

            cmultifunctionalDevice.PowerOff();
            cmultifunctionalDevice.Print(in doc3);
            cmultifunctionalDevice.Scan(out doc1);
            cmultifunctionalDevice.PowerOn();

            cmultifunctionalDevice.ScanAndPrint();
            cmultifunctionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(5, cmultifunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            IDocument doc1;
            cmultifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            cmultifunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            cmultifunctionalDevice.Print(in doc3);

            cmultifunctionalDevice.PowerOff();
            cmultifunctionalDevice.Print(in doc3);
            cmultifunctionalDevice.Scan(out doc1);
            cmultifunctionalDevice.PowerOn();

            cmultifunctionalDevice.ScanAndPrint();
            cmultifunctionalDevice.ScanAndPrint();

            // 4 skany, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(4, cmultifunctionalDevice.ScanCounter);
        }

        public void MultifunctionalDevice_FaxCounter()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            cmultifunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            cmultifunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            cmultifunctionalDevice.Print(in doc3);

            cmultifunctionalDevice.PowerOff();
            cmultifunctionalDevice.Send(in doc3);
            cmultifunctionalDevice.Scan(out doc1);
            cmultifunctionalDevice.PowerOn();

            cmultifunctionalDevice.ScanAndFax();
            cmultifunctionalDevice.ScanAndFax();

            // 5 wydruków, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(5, cmultifunctionalDevice.FaxCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var cmultifunctionalDevice = new MultifunctionalDevice();
            cmultifunctionalDevice.PowerOn();
            cmultifunctionalDevice.PowerOn();
            cmultifunctionalDevice.PowerOn();

            IDocument doc1;
            cmultifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            cmultifunctionalDevice.Scan(out doc2);

            cmultifunctionalDevice.PowerOff();
            cmultifunctionalDevice.PowerOff();
            cmultifunctionalDevice.PowerOff();
            cmultifunctionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            cmultifunctionalDevice.Print(in doc3);

            cmultifunctionalDevice.PowerOff();
            cmultifunctionalDevice.Print(in doc3);
            cmultifunctionalDevice.Scan(out doc1);
            cmultifunctionalDevice.PowerOn();

            cmultifunctionalDevice.ScanAndPrint();
            cmultifunctionalDevice.ScanAndPrint();

            // 3 w³¹czenia
            Assert.AreEqual(3, cmultifunctionalDevice.Counter);
        }

    }
}