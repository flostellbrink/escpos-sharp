namespace EscPosSharp.Test;

public class SampleCodeTable
{
    [Test]
    public void SampleCodeTableTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        Style title = new Style()
            .SetFontSize(Style.FontSize._2, Style.FontSize._2)
            .SetJustification(Justification.Center);

        escpos.WriteLF(title, "Code Table");
        escpos.Feed(2);

        escpos.WriteLF("Using code table of the France");
        escpos.SetCharacterCodeTable(EscPos.CharacterCodeTable.CP863_Canadian_French);
        escpos.Feed(2);
        escpos.WriteLF("Liberté et Fraternité.");
        escpos.Feed(3);

        escpos.WriteLF("Using Portuguese code table");
        escpos.SetCharacterCodeTable(EscPos.CharacterCodeTable.CP860_Portuguese);
        escpos.WriteLF("Programação java.");

        escpos.Feed(5);
        escpos.Cut(EscPos.CutMode.FULL);

        escpos.Close();

        var expected = File.ReadAllBytes("expected/charcode.txt");
        Assert.That(result.ToArray(), Is.EqualTo(expected));
    }
}
