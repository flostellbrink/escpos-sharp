namespace EscPosSharp.Test;

public class PortedTests
{
    [Test]
    public void SetPrinterCharacterTableTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        escpos.SetPrinterCharacterTable(10);
        escpos.Close();

        using var expected = new MemoryStream();
        expected.WriteByte((byte)EscPos.ESC);
        expected.WriteByte((byte)'t');
        expected.WriteByte((byte)10);

        Assert.That(expected.ToArray(), Is.EqualTo(result.ToArray()));
    }
}
