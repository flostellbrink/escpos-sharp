using System.Drawing;
using EscPosSharp.Image;

namespace EscPosSharp.Test;

public class SampleRasterImage
{
    [Test]
    public void SampleRasterImageTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        Bitonal algorithm = new BitonalThreshold(127);
        var githubImage = (Bitmap)System.Drawing.Image.FromFile("expected/github.png");
        var escposImage = new EscPosImage(new CoffeeImageImpl(githubImage), algorithm);

        // this wrapper uses esc/pos sequence: "GS 'v' '0'"
        RasterBitImageWrapper imageWrapper = new RasterBitImageWrapper();

        escpos.WriteLF(
            new Style().SetFontSize(Style.FontSize._2, Style.FontSize._2),
            "RasterBitImageWrapper"
        );

        escpos.WriteLF("default size");
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Double Height");
        imageWrapper.SetRasterBitImageMode(RasterBitImageWrapper.RasterBitImageMode.DoubleHeight);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Double Width");
        imageWrapper.SetRasterBitImageMode(RasterBitImageWrapper.RasterBitImageMode.DoubleWidth);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Quadruple size");
        imageWrapper.SetRasterBitImageMode(RasterBitImageWrapper.RasterBitImageMode.Quadruple);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("print on Left");
        imageWrapper.SetRasterBitImageMode(RasterBitImageWrapper.RasterBitImageMode.Normal_Default);
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

        var actual = result.ToArray();
        var expected = File.ReadAllBytes("expected/raster-image.txt");

        Directory.CreateDirectory("actual");
        File.WriteAllBytes("actual/raster-image.txt", actual);

        Assert.That(actual, Is.EqualTo(expected));
    }
}
