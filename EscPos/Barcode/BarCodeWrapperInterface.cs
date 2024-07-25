namespace EscPosSharp.Barcode
{
    public interface BarCodeWrapperInterface<T>
    {
        byte[] GetBytes(string data);
        T SetJustification(Justification justification);
    }
}
