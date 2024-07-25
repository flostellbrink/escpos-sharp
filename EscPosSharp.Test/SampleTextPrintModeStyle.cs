namespace EscPosSharp.Test;

public class SampleTextPrintModeStyle
{
    [Test]
    public void SampleTextPrintModeStyleTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        PrintModeStyle normal = new PrintModeStyle();

        PrintModeStyle title = new PrintModeStyle()
            .SetFontSize(true, true)
            .SetJustification(Justification.Center);

        PrintModeStyle subtitle = new PrintModeStyle().SetBold(true).SetUnderline(true);

        PrintModeStyle bold = new PrintModeStyle().SetBold(true);

        escpos
            .WriteLF(title, "My Market")
            .Feed(3)
            .Write(normal, "Client: ")
            .WriteLF(subtitle, "Jane Doe")
            .Feed(3)
            .WriteLF(normal, "Cup of coffee                      $1.00")
            .WriteLF(normal, "Botle of water                     $0.50")
            .WriteLF(normal, "----------------------------------------")
            .Feed(2)
            .WriteLF(bold, "TOTAL                              $1.50")
            .WriteLF(normal, "----------------------------------------")
            .Feed(8)
            .Cut(EscPos.CutMode.FULL);

        var expected = File.ReadAllBytes("expected/textprintmodestyle.txt");
        Assert.That(result.ToArray(), Is.EqualTo(expected));
    }
}
