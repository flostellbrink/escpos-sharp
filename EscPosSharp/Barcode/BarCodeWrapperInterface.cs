namespace EscPosSharp.Barcode;

public interface BarCodeWrapperInterface
{
    byte[] GetBytes(string data);
    void SetJustification(Justification justification);
}
