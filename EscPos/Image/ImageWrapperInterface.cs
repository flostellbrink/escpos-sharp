namespace EscPosSharp.Image
{
    public interface ImageWrapperInterface<T>
    {
        byte[] GetBytes(EscPosImage image);
        T SetJustification(Justification justification);
    }
}
