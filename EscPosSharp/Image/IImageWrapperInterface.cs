namespace EscPosSharp.Image;

public interface IImageWrapperInterface
{
    byte[] GetBytes(EscPosImage image);
    void SetJustification(Justification justification);
}
