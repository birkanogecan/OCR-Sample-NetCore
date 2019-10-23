using System;
using System.Drawing;
using System.IO;
using Tesseract;
namespace OCR_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = string.Empty;
            try
            {
                using (var engine = new TesseractEngine(@"C:\Users\birkan\Source\Repos\OCR-Sample\OCR-Sample\tessdata", "tur", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(@"C:\Users\birkan\Source\Repos\OCR-Sample\OCR-Sample\test.png"))
                    {
                        using (var page = engine.Process(img))
                        {

                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();

                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {


                                                string buffer = iter.GetText(PageIteratorLevel.Word);
                                                if (!(buffer == "" || buffer == null || buffer == string.Empty || buffer == " "))
                                                {
                                                    text += " " + iter.GetText(PageIteratorLevel.Word);
                                                }


                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    if (!(text == " " || text == String.Empty))
                                                    {
                                                        text += " ";
                                                    }

                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                if (!(text == "" || text == String.Empty))
                                                {
                                                    text += " ";
                                                }

                                            }
                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                text = "";
            }
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
