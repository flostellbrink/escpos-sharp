namespace EscPosSharp.Image;

public interface ImageWrapperInterface
{
    byte[] GetBytes(EscPosImage image);
    void SetJustification(Justification justification);
}
