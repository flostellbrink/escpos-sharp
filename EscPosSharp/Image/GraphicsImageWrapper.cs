namespace EscPosSharp.Image;

/// <summary>
/// Supply ESC/POS Graphics print Image commands.
/// using <code>GS(L</code>
/// </summary>
public class GraphicsImageWrapper : EscPosConst, IImageWrapperInterface
{
    /// <summary>
    /// Values for Raster Bit Image mode.
    /// </summary>
    /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
    public class GraphicsImageBxBy
    {
        public static GraphicsImageBxBy Normal_Default = new(1, 1);
        public static GraphicsImageBxBy DoubleWidth = new(2, 1);
        public static GraphicsImageBxBy DoubleHeight = new(1, 2);
        public static GraphicsImageBxBy Quadruple = new(2, 2);

        public int bx;
        public int by;

        private GraphicsImageBxBy(int bx, int by)
        {
            this.bx = bx;
            this.by = by;
        }
    }

    /// <summary>
    /// Values for Raster Bit Image mode.
    /// </summary>
    /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
    protected Justification justification;

    /// <summary>
    /// Values for Raster Bit Image mode.
    /// </summary>
    /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
    protected GraphicsImageBxBy graphicsImageBxBy;

    /// <summary>
    /// Values for Raster Bit Image mode.
    /// </summary>
    /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
    public GraphicsImageWrapper()
    {
        justification = Justification.Left_Default;
        graphicsImageBxBy = GraphicsImageBxBy.Normal_Default;
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
    /// set values of Bx and By referring to the image size.
    /// </summary>
    /// <param name="graphicsImageBxBy">values used on function 112</param>
    /// <returns>this object</returns>
    /// <remarks>@see#getBytes(EscPosImage)</remarks>
    public virtual GraphicsImageWrapper SetGraphicsImageBxBy(GraphicsImageBxBy graphicsImageBxBy)
    {
        this.graphicsImageBxBy = graphicsImageBxBy;
        return this;
    }

    /// <summary>
    /// Bit Image commands Assembly into ESC/POS bytes.
    ///
    /// Select justification
    /// ASCII ESC a n
    ///
    /// function 112 Store the graphics data in the print buffer
    /// GS(L pL pH m fn a bx by c xL xH yL yH d1...dk
    ///
    /// function 050 Prints the buffered graphics data
    /// GS ( L pL pH m fn
    /// </summary>
    /// <param name="image">to be printed</param>
    /// <returns>bytes of ESC/POS</returns>
    /// <remarks>
    /// @seeEscPosImage#getRasterBytes()
    /// @seeEscPosImage#getRasterSizeInBytes()
    /// </remarks>
    public virtual byte[] GetBytes(EscPosImage image)
    {
        var bytes = new MemoryStream();

        //
        bytes.WriteByte((byte)ESC);
        bytes.WriteByte((byte)'a');
        bytes.WriteByte((byte)justification.value);

        //
        var paramSize = image.GetRasterSizeInBytes() + 10;
        var pL = paramSize & 0xFF;
        var pH = (paramSize & 0xFF00) >> 8;
        bytes.WriteByte((byte)GS);
        bytes.WriteByte((byte)'(');
        bytes.WriteByte((byte)'L');
        bytes.WriteByte((byte)pL); // pl
        bytes.WriteByte((byte)pH); // ph
        bytes.WriteByte((byte)48); // m
        bytes.WriteByte((byte)112); //fn
        bytes.WriteByte((byte)48); // a
        bytes.WriteByte((byte)graphicsImageBxBy.bx); // bx
        bytes.WriteByte((byte)graphicsImageBxBy.by); // by
        bytes.WriteByte((byte)49); // c

        //  bits in horizontal direction for the bit image
        var horizontalBits = image.GetWidthOfImageInBits();
        var xL = horizontalBits & 0xFF;
        var xH = (horizontalBits & 0xFF00) >> 8;

        //
        //  bits in vertical direction for the bit image
        int verticalBits = image.GetHeightOfImageInBits();

        // getting first and second bytes separatted
        int yL = verticalBits & 0xFF;
        int yH = (verticalBits & 0xFF00) >> 8;
        bytes.WriteByte((byte)xL);
        bytes.WriteByte((byte)xH);
        bytes.WriteByte((byte)yL);
        bytes.WriteByte((byte)yH);

        // write bytes
        var rasterBytes = image.GetRasterBytes();
        bytes.Write(rasterBytes, 0, rasterBytes.Length);

        // function 050
        bytes.WriteByte((byte)GS);
        bytes.WriteByte((byte)'(');
        bytes.WriteByte((byte)'L');
        bytes.WriteByte((byte)2); // pl
        bytes.WriteByte((byte)0); // ph
        bytes.WriteByte((byte)48); //m
        bytes.WriteByte((byte)50); //fn

        //
        return bytes.ToArray();
    }
}
