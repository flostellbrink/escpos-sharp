using System.Drawing;
using EscPosSharp.Image;

namespace EscPosSharp.Test;

public class SampleGraphicsImage
{
    [Test]
    public void SampleGraphicsImageTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        Bitonal algorithm = new BitonalThreshold(127);
        using var githubStream = new FileStream("expected/github.png", FileMode.Open);
        var githubImage = (Bitmap)System.Drawing.Image.FromStream(githubStream);
        var escposImage = new EscPosImage(new CoffeeImageImpl(githubImage), algorithm);

        // this wrapper uses esc/pos sequence: "GS(L"
        GraphicsImageWrapper imageWrapper = new GraphicsImageWrapper();

        escpos.WriteLF(
            new Style().SetFontSize(Style.FontSize._2, Style.FontSize._2),
            "GraphicsImageWrapper"
        );

        escpos.WriteLF("default size");
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Double Height");
        imageWrapper.SetGraphicsImageBxBy(GraphicsImageWrapper.GraphicsImageBxBy.DoubleHeight);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Double Width");
        imageWrapper.SetGraphicsImageBxBy(GraphicsImageWrapper.GraphicsImageBxBy.DoubleWidth);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("Quadruple size");
        imageWrapper.SetGraphicsImageBxBy(GraphicsImageWrapper.GraphicsImageBxBy.Quadruple);
        escpos.Write(imageWrapper, escposImage);

        escpos.Feed(5);
        escpos.WriteLF("print on Left");
        imageWrapper.SetGraphicsImageBxBy(GraphicsImageWrapper.GraphicsImageBxBy.Normal_Default);
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

        var expected = File.ReadAllBytes("expected/graphics-image.txt");
        Assert.That(result.ToArray(), Is.EqualTo(expected));
    }
}
