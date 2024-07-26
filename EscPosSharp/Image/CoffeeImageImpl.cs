using System.Drawing;

namespace EscPosSharp.Image;

/// <summary>
/// implements CoffeeImage using System.Drawing.Bitmap
/// </summary>
/// <remarks>
/// @seeCoffeeImage
/// @seeBufferedImage
/// </remarks>
public class CoffeeImageImpl : ICoffeeImage
{
    protected Bitmap image;

    public CoffeeImageImpl(Bitmap image)
    {
        this.image = image;
    }

    public virtual int GetWidth()
    {
        return image.Width;
    }

    public virtual int GetHeight()
    {
        return image.Height;
    }

    public virtual ICoffeeImage GetSubimage(int x, int y, int w, int h)
    {
        var newImage = new Bitmap(w, h);
        using var graphics = Graphics.FromImage(newImage);
        graphics.DrawImage(
            image,
            new Rectangle(0, 0, w, h),
            new Rectangle(x, y, w, h),
            GraphicsUnit.Pixel
        );
        return new CoffeeImageImpl(newImage);
    }

    public virtual int GetARGB(int x, int y)
    {
        return image.GetPixel(x, y).ToArgb();
    }
}
