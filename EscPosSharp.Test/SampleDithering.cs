using System.Drawing;
using EscPosSharp.Image;

namespace EscPosSharp.Test;

public class SampleDithering
{
    [Test]
    public void SampleDitheringTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        using var dogStream = new FileStream("expected/dog.png", FileMode.Open);
        var dogImage = (Bitmap)System.Drawing.Image.FromStream(dogStream);

        RasterBitImageWrapper imageWrapper = new RasterBitImageWrapper();

        Style title = new Style().SetFontSize(Style.FontSize._2, Style.FontSize._2);
        escpos.WriteLF(title, "Dithering BitonalThreshold");

        escpos.Feed(5);
        escpos.WriteLF("BitonalThreshold()");
        Bitonal algorithm = new BitonalThreshold();
        EscPosImage escposImage = new EscPosImage(new CoffeeImageImpl(dogImage), algorithm);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);

        escpos.WriteLF("BitonalThreshold(60) (clearing)");
        algorithm = new BitonalThreshold(100);
        escposImage = new EscPosImage(new CoffeeImageImpl(dogImage), algorithm);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);

        escpos.WriteLF("BitonalThreshold(150) (darkening)");
        algorithm = new BitonalThreshold(150);
        escposImage = new EscPosImage(new CoffeeImageImpl(dogImage), algorithm);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);

        escpos.Cut(EscPos.CutMode.PART);

        escpos.WriteLF(title, "Dithering");
        escpos.WriteLF(title, "BitonalOrderedDither");

        escpos.Feed(5);
        escpos.WriteLF("BitonalOrderedDither()");
        algorithm = new BitonalOrderedDither();
        escposImage = new EscPosImage(new CoffeeImageImpl(dogImage), algorithm);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);

        escpos.WriteLF("BitonalOrderedDither (clearing) values");
        algorithm = new BitonalOrderedDither(2, 2, 60, 100);
        escposImage = new EscPosImage(new CoffeeImageImpl(dogImage), algorithm);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);

        escpos.WriteLF("BitonalOrderedDither (darkening) values");
        algorithm = new BitonalOrderedDither(2, 2, 120, 170);
        escposImage = new EscPosImage(new CoffeeImageImpl(dogImage), algorithm);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);

        escpos.WriteLF("BitonalOrderedDither  3x3 matrix");
        escpos.WriteLF("quadruple sized to better see effects..");
        algorithm = new BitonalOrderedDither(3, 3, 100, 130);
        escposImage = new EscPosImage(new CoffeeImageImpl(dogImage), algorithm);
        imageWrapper.SetRasterBitImageMode(RasterBitImageWrapper.RasterBitImageMode.Quadruple);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);

        escpos.WriteLF("BitonalOrderedDither customized matrix");
        int[,] ditherMatrix = new int[3, 3]
        {
            { 100, 130, 100 },
            { 130, 0, 130 },
            { 100, 130, 100 },
        };
        var customAlgorithm = new BitonalOrderedDither(3, 3);
        customAlgorithm.SetDitherMatrix(ditherMatrix);
        escposImage = new EscPosImage(new CoffeeImageImpl(dogImage), customAlgorithm);
        escpos.Write(imageWrapper, escposImage);
        escpos.Feed(5);

        escpos.Feed(5);
        escpos.Cut(EscPos.CutMode.FULL);

        var expected = File.ReadAllBytes("expected/dithering.txt");
        Assert.That(result.ToArray(), Is.EqualTo(expected));
    }
}
