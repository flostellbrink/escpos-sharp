namespace EscPosSharp.Test;

public class SampleTextStyle
{
    [Test]
    public void SampleTextStyleTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        Style title = new Style()
            .SetFontSize(Style.FontSize._3, Style.FontSize._3)
            .SetJustification(Justification.Center);

        Style subtitle = new Style(escpos.GetStyle())
            .SetBold(true)
            .SetUnderline(Style.Underline.OneDotThick);
        Style bold = new Style(escpos.GetStyle()).SetBold(true);

        escpos
            .WriteLF(title, "My Market")
            .Feed(3)
            .Write("Client: ")
            .WriteLF(subtitle, "John Doe")
            .Feed(3)
            .WriteLF("Cup of coffee                      $1.00")
            .WriteLF("Botle of water                     $0.50")
            .WriteLF("----------------------------------------")
            .Feed(2)
            .WriteLF(bold, "TOTAL                              $1.50")
            .WriteLF("----------------------------------------")
            .Feed(8)
            .Cut(EscPos.CutMode.FULL);

        var expected = File.ReadAllBytes("expected/textstyle.txt");
        Assert.That(result.ToArray(), Is.EqualTo(expected));
    }
}
