namespace EscPosSharp.Image;

/// <summary>
/// Supply ESC/POS Raster bit Image commands.
/// using <code>GS 'v' '0'</code>
/// </summary>
public class RasterBitImageWrapper : EscPosConst, IImageWrapperInterface
{
    /// <summary>
    /// Values for Raster Bit Image mode.
    /// </summary>
    /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
    public class RasterBitImageMode
    {
        public static RasterBitImageMode Normal_Default = new(0);
        public static RasterBitImageMode DoubleWidth = new(1);
        public static RasterBitImageMode DoubleHeight = new(2);
        public static RasterBitImageMode Quadruple = new(3);

        public int value;

        private RasterBitImageMode(int value)
        {
            this.value = value;
        }
    }

    /// <summary>
    /// Values for Raster Bit Image mode.
    /// </summary>
    /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
    protected Justification justification;

    /// <summary>
    /// Values for Raster Bit Image mode.
    /// </summary>
    /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
    protected RasterBitImageMode rasterBitImageMode;

    /// <summary>
    /// Values for Raster Bit Image mode.
    /// </summary>
    /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
    public RasterBitImageWrapper()
    {
        justification = Justification.Left_Default;
        rasterBitImageMode = RasterBitImageMode.Normal_Default;
    }

    /// <summary>
    /// Set horizontal justification of bar-code
    /// </summary>
    /// <param name="justification">left, center or right</param>
    /// <returns>this object</returns>
    public virtual void SetJustification(Justification justification)
    {
        this.justification = justification;
    }

    /// <summary>
    /// Set the mode of Raster Bit Image.
    /// </summary>
    /// <param name="rasterBitImageMode">mode to be used with GS v 0</param>
    /// <returns>this object</returns>
    /// <remarks>@see#getBytes(EscPosImage)</remarks>
    public virtual RasterBitImageWrapper SetRasterBitImageMode(
        RasterBitImageMode rasterBitImageMode
    )
    {
        this.rasterBitImageMode = rasterBitImageMode;
        return this;
    }

    /// <summary>
    /// Bit Image commands Assembly into ESC/POS bytes.
    ///
    /// Select justification
    /// ASCII ESC a n
    ///
    /// Print raster bit image
    /// ASCII GS v 0 m xL xH yL yH d1...dk
    /// </summary>
    /// <param name="image">to be printed</param>
    /// <returns>bytes of ESC/POS</returns>
    /// <remarks>@seeEscPosImage</remarks>
    public virtual byte[] GetBytes(EscPosImage image)
    {
        using var bytes = new MemoryStream();

        //
        bytes.WriteByte((byte)ESC);
        bytes.WriteByte((byte)'a');
        bytes.WriteByte((byte)justification.value);

        //
        bytes.WriteByte((byte)GS);
        bytes.WriteByte((byte)'v');
        bytes.WriteByte((byte)'0');
        bytes.WriteByte((byte)rasterBitImageMode.value);

        //
        //  bytes in horizontal direction for the bit image
        var horizontalBytes = image.GetHorizontalBytesOfRaster();
        var xL = horizontalBytes & 0xFF;
        var xH = (horizontalBytes & 0xFF00) >> 8;

        //
        //  bits in vertical direction for the bit image
        var verticalBits = image.GetHeightOfImageInBits();

        // getting first and second bytes separatted
        var yL = verticalBits & 0xFF;
        var yH = (verticalBits & 0xFF00) >> 8;
        bytes.WriteByte((byte)xL);
        bytes.WriteByte((byte)xH);
        bytes.WriteByte((byte)yL);
        bytes.WriteByte((byte)yH);

        // write raster bytes
        var rasterBytes = image.GetRasterBytes();
        bytes.Write(rasterBytes, 0, rasterBytes.Length);

        //
        return bytes.ToArray();
    }
}
