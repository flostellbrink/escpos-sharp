using System.Drawing;
using EscPosSharp.Image;

namespace EscPosSharp.Test;

public class SampleBarcode
{
    [Test]
    public void SampleBarcodeTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        var algorithm = new BitonalThreshold(127);
        using var githubStream = new FileStream("expected/github.png", FileMode.Open);
        var githubImage = (Bitmap)System.Drawing.Image.FromStream(githubStream);
        var escposImage = new EscPosImage(new CoffeeImageImpl(githubImage), algorithm);
        var imageWrapper = new BitImageWrapper();

        escpos.WriteLF(
            new Style().SetFontSize(Style.FontSize._2, Style.FontSize._2),
            "BitImageWrapper"
        );

        escpos.WriteLF("default size");
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Double Height");
        imageWrapper.SetMode(BitImageWrapper.BitImageMode._8DotDoubleDensity);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Double Width");
        imageWrapper.SetMode(BitImageWrapper.BitImageMode._24DotSingleDensity);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Quadruple size");
        imageWrapper.SetMode(BitImageWrapper.BitImageMode._8DotSingleDensity);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("print on Left");
        imageWrapper.SetMode(BitImageWrapper.BitImageMode._24DotDoubleDensity_Default);
        imageWrapper.SetJustification(Justification.Left_Default);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);
        escpos.WriteLF("print on Right");
        imageWrapper.SetJustification(Justification.Right);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);
        escpos.WriteLF("print on Center");
        imageWrapper.SetJustification(Justification.Center);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.Cut(EscPos.CutMode.FULL);

        escpos.Close();

        var expected = File.ReadAllBytes("expected/bitimage.txt");
        Assert.That(result.ToArray(), Is.EqualTo(expected));
    }
}
