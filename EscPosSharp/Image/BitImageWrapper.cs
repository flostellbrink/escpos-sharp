namespace EscPosSharp.Image;

/// <summary>
/// Supply ESC/POS Bit Image commands.
/// using <code>ESC '*'</code>
/// </summary>
public class BitImageWrapper : EscPosConst, IImageWrapperInterface
{
    /// <summary>
    /// Values for Bit Image Mode.
    /// </summary>
    /// <remarks>@see#setMode(BitImageMode)</remarks>
    public class BitImageMode
    {
        public static BitImageMode _8DotSingleDensity = new(0, 8);

        public static BitImageMode _8DotDoubleDensity = new(1, 8);

        public static BitImageMode _24DotSingleDensity = new(32, 24);

        public static BitImageMode _24DotDoubleDensity_Default = new(33, 24);

        public int value;
        public int bitsForVerticalData;

        private BitImageMode(int value, int bitsPerSlice)
        {
            this.value = value;
            this.bitsForVerticalData = bitsPerSlice;
        }
    }

    /// <summary>
    /// Values for Bit Image Mode.
    /// </summary>
    /// <remarks>@see#setMode(BitImageMode)</remarks>
    protected Justification justification;

    /// <summary>
    /// Values for Bit Image Mode.
    /// </summary>
    /// <remarks>@see#setMode(BitImageMode)</remarks>
    protected BitImageMode mode;

    /// <summary>
    /// Values for Bit Image Mode.
    /// </summary>
    /// <remarks>@see#setMode(BitImageMode)</remarks>
    public BitImageWrapper()
    {
        justification = Justification.Left_Default;
        mode = BitImageMode._24DotDoubleDensity_Default;
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
    /// Select bit-image mode.
    /// </summary>
    /// <param name="mode">mode to be used on command ESC *</param>
    /// <returns>this object</returns>
    /// <remarks>@see#getBytes(EscPosImage)</remarks>
    public virtual BitImageWrapper SetMode(BitImageMode mode)
    {
        this.mode = mode;
        return this;
    }

    /// <summary>
    /// Bit Image commands Assembly into ESC/POS bytes.
    ///
    /// Select justification
    /// ASCII ESC a n
    ///
    /// Set lineSpace in bytes
    /// ASCII ESC '3' n
    ///
    /// write all rows of the raster image
    /// ASCII ESC ✻ m nL nH d1 ... dk
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
        bytes.WriteByte((byte)ESC);
        bytes.WriteByte((byte)'3');
        bytes.WriteByte((byte)16);

        // getting first and second bytes separatted
        var nL = image.GetWidthOfImageInBits() & 0xFF;
        var nH = (image.GetWidthOfImageInBits() & 0xFF00) >> 8;
        var RasterColumns = image.GetRasterRows(mode.bitsForVerticalData);
        foreach (var rol in RasterColumns)
        {
            //write one rol to print
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'*');
            bytes.WriteByte((byte)mode.value);
            bytes.WriteByte((byte)nL);
            bytes.WriteByte((byte)nH);
            bytes.Write(rol, 0, rol.Length);
            bytes.WriteByte((byte)LF);
        }

        //
        return bytes.ToArray();
    }
}
