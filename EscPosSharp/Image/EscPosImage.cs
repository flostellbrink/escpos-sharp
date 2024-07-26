namespace EscPosSharp.Image
{
    /// <summary>
    /// Supply raster patterns images
    /// </summary>
    public class EscPosImage
    {
        protected readonly CoffeeImage image;
        protected readonly Bitonal bitonalAlgorithm;
        protected byte[]? baCachedEscPosRaster = null;
        protected List<byte[]> CashedEscPosRasterRows_8 = new();
        protected List<byte[]> CachedEscPosRasterRows_24 = new();

        /// <summary>
        /// creates an EscPosImage
        /// </summary>
        /// <param name="image">normal RGB image</param>
        /// <param name="bitonalAlgorithm">Algorithm that transform RGB to bitonal</param>
        /// <remarks>@see#getBitonalVal(int, int)</remarks>
        public EscPosImage(CoffeeImage image, Bitonal bitonalAlgorithm)
        {
            this.image = image;
            this.bitonalAlgorithm = bitonalAlgorithm;
        }

        public virtual int GetHorizontalBytesOfRaster()
        {
            return ((image.GetWidth() % 8) > 0)
                ? (image.GetWidth() / 8) + 1
                : (image.GetWidth() / 8);
        }

        public virtual int GetWidthOfImageInBits()
        {
            return image.GetWidth();
        }

        public virtual int GetHeightOfImageInBits()
        {
            return image.GetHeight();
        }

        public virtual int GetRasterSizeInBytes()
        {
            if (baCachedEscPosRaster?.Length > 0)
                return (int)baCachedEscPosRaster.Length;
            baCachedEscPosRaster = Image2EscPosRaster();
            return (int)baCachedEscPosRaster.Length;
        }

        /// <summary>
        /// get raster bytes of image in Rows pattern.
        /// Utilize cached bytes if available
        /// </summary>
        /// <param name="bitsPerColumn_8_or_24">possible values are 8 or 24</param>
        /// <returns>a list of rows in raster pattern</returns>
        public virtual IList<byte[]> GetRasterRows(int bitsPerColumn_8_or_24)
        {
            if (bitsPerColumn_8_or_24 == 8)
            {
                if (CashedEscPosRasterRows_8.Count > 0)
                {
                    return CashedEscPosRasterRows_8;
                }

                CashedEscPosRasterRows_8 = Image2Rows(bitsPerColumn_8_or_24);
                return CashedEscPosRasterRows_8;
            }
            else
            {
                if (CachedEscPosRasterRows_24.Count > 0)
                {
                    return CachedEscPosRasterRows_24;
                }

                CachedEscPosRasterRows_24 = Image2Rows(bitsPerColumn_8_or_24);
                return CachedEscPosRasterRows_24;
            }
        }

        /// <summary>
        /// transform RGB image to raster Rows format
        /// </summary>
        /// <param name="bitsPerColumn_8_or_24">possible values are 8 or 24</param>
        /// <returns>a list of rows in raster pattern</returns>
        protected virtual List<byte[]> Image2Rows(int bitsPerColumn_8_or_24)
        {
            var lRasterRows = new List<byte[]>();
            var lRGBImageRows = new List<CoffeeImage>();
            for (int y = 0; y < image.GetHeight(); y += bitsPerColumn_8_or_24)
            {
                int height = bitsPerColumn_8_or_24;
                if ((y + height) > image.GetHeight())
                {
                    height = image.GetHeight() - y;
                }

                var row = image.GetSubimage(0, y, image.GetWidth(), height);
                lRGBImageRows.Add(row);
            }

            int heightOffset = 0;
            foreach (CoffeeImage RGBRow in lRGBImageRows)
            {
                var baColumBytes = new MemoryStream();
                for (int x = 0; x < RGBRow.GetWidth(); x++)
                {
                    int col = 0;
                    int max_y = Math.Min(bitsPerColumn_8_or_24, RGBRow.GetHeight());
                    int bit = 0;
                    int bitsWritten = 0;
                    for (int y = 0; y < max_y; y++)
                    {
                        int val = GetBitonalVal(x, y + heightOffset);
                        col = col | (val << (7 - bit));
                        bit++;
                        if (bit == 8)
                        {
                            baColumBytes.WriteByte((byte)col);
                            bitsWritten += 8;
                            col = 0;
                            bit = 0;
                        }
                    }

                    if (bit > 0)
                    {
                        baColumBytes.WriteByte((byte)col);
                        bitsWritten += 8;
                        while (bitsWritten < bitsPerColumn_8_or_24)
                        {
                            baColumBytes.WriteByte((byte)0);
                            bitsWritten += 8;
                        }
                    }
                }

                lRasterRows.Add(baColumBytes.ToArray());
                heightOffset += bitsPerColumn_8_or_24;
            }

            return lRasterRows;
        }

        /// <summary>
        /// get raster bytes of image.
        /// Utilize cached bytes if available.
        /// </summary>
        /// <returns>bytes of raster image.</returns>
        public virtual byte[] GetRasterBytes()
        {
            if (baCachedEscPosRaster?.Length > 0)
                return baCachedEscPosRaster;
            baCachedEscPosRaster = Image2EscPosRaster();
            return baCachedEscPosRaster;
        }

        /// <summary>
        /// Call the custom algorithm to determine print or not print on each coordinates.
        /// </summary>
        /// <param name="x">the X coordinate of the image</param>
        /// <param name="y">the Y coordinate of the image</param>
        /// <returns> 0 or 1</returns>
        /// <remarks>
        /// @see#EscPosImage(CoffeeImage, Bitonal) (BufferedImage, Bitonal)
        /// @seeBitonal#getBitonalVal(CoffeeImage, int, int)
        /// </remarks>
        protected virtual int GetBitonalVal(int x, int y)
        {
            return bitonalAlgorithm.GetBitonalVal(image, x, y);
        }

        /// <summary>
        /// transform RGB image in raster format.
        /// </summary>
        /// <returns>raster byte array</returns>
        protected virtual byte[] Image2EscPosRaster()
        {
            var byteArray = new MemoryStream();
            int Byte;
            int bit;
            for (int y = 0; y < image.GetHeight(); y++)
            {
                Byte = 0;
                bit = 0;
                for (int x = 0; x < image.GetWidth(); x++)
                {
                    int val = GetBitonalVal(x, y);
                    Byte = Byte | (val << (7 - bit));
                    bit++;
                    if (bit == 8)
                    {
                        byteArray.WriteByte((byte)Byte);
                        Byte = 0;
                        bit = 0;
                    }
                }

                if (bit > 0)
                {
                    byteArray.WriteByte((byte)Byte);
                }
            }

            return byteArray.ToArray();
        }
    }
}
