using EscPosSharp;
using EscPosSharp.Barcode;

namespace EscPosSharp.Test;

public class SampleBitImage
{
    [Test]
    public void SampleBitImageTest()
    {
        using var result = new MemoryStream();
        using var escpos = new EscPos(result);

        Style title = new Style()
            .SetFontSize(Style.FontSize._2, Style.FontSize._2)
            .SetJustification(Justification.Center);

        escpos.WriteLF(title, "Barcode");
        escpos.Feed(2);
        BarCode barcode = new BarCode();

        escpos.WriteLF("barcode default options CODE93 system");
        escpos.Feed(2);
        escpos.Write(barcode, "hello barcode");
        escpos.Feed(3);

        escpos.WriteLF("barcode write HRI above");
        barcode.SetHRIPosition(BarCode.BarCodeHRIPosition.AboveBarCode);
        escpos.Feed(2);
        escpos.Write(barcode, "hello barcode");
        escpos.Feed(3);

        escpos.WriteLF("barcode write HRI below");
        barcode.SetHRIPosition(BarCode.BarCodeHRIPosition.BelowBarCode);
        escpos.Feed(2);
        escpos.Write(barcode, "hello barcode");
        escpos.Feed(3);

        escpos.WriteLF("barcode right justification ");
        barcode.SetHRIPosition(BarCode.BarCodeHRIPosition.NotPrinted_Default);
        barcode.SetJustification(Justification.Right);
        escpos.Feed(2);
        escpos.Write(barcode, "hello barcode");
        escpos.Feed(3);

        escpos.WriteLF("barcode height 200 ");
        barcode.SetJustification(Justification.Left_Default);
        barcode.SetBarCodeSize(2, 200);
        escpos.Feed(2);
        escpos.Write(barcode, "hello barcode");
        escpos.Feed(3);

        escpos.WriteLF("barcode UPCA system ");
        barcode.SetSystem(BarCode.BarCodeSystem.UPCA);
        barcode.SetHRIPosition(BarCode.BarCodeHRIPosition.BelowBarCode);
        barcode.SetBarCodeSize(2, 100);
        escpos.Feed(2);
        escpos.Write(barcode, "12345678901");
        escpos.Feed(3);

        escpos.Feed(5);
        escpos.Cut(EscPos.CutMode.PART);

        escpos.WriteLF(title, "QR Code");
        escpos.Feed(2);
        QRCode qrcode = new QRCode();

        escpos.WriteLF("QRCode default options");
        escpos.Feed(2);
        escpos.Write(qrcode, "hello qrcode");
        escpos.Feed(3);

        escpos.WriteLF("QRCode size 6 and center justified");
        escpos.Feed(2);
        qrcode.SetSize(7);
        qrcode.SetJustification(Justification.Center);
        escpos.Write(qrcode, "hello qrcode");
        escpos.Feed(3);

        escpos.Feed(5);
        escpos.Cut(EscPos.CutMode.PART);

        escpos.WriteLF(title, "PDF 417");
        escpos.Feed(2);
        PDF417 pdf417 = new PDF417();

        escpos.WriteLF("pdf417 default options");
        escpos.Feed(2);
        escpos.Write(pdf417, "hello PDF 417");
        escpos.Feed(3);

        escpos.WriteLF("pdf417 height 5");
        escpos.Feed(2);
        pdf417.SetHeight(5);
        escpos.Write(pdf417, "hello PDF 417");
        escpos.Feed(3);

        escpos.WriteLF("pdf417 error level 4");
        escpos.Feed(2);
        pdf417 = new PDF417().SetErrorLevel(PDF417.PDF417ErrorLevel._4);
        escpos.Write(pdf417, "hello PDF 417");
        escpos.Feed(3);

        escpos.Feed(5);
        escpos.Cut(EscPos.CutMode.FULL);

        escpos.Close();

        var expected = File.ReadAllBytes("expected/bitimage.txt");
        Assert.That(result.ToArray(), Is.EqualTo(expected));
    }
}
