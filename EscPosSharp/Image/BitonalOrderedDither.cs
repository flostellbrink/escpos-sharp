namespace EscPosSharp.Image
{
    /// <summary>
    /// Implements ordered dithering based on a <code>ditherMatrix</code>
    /// with size <code>matrixWidth</code> and <code>matrixHeight</code>.
    /// Each value of this <code>ditherMatrix</code> must be between 0 and 255.
    /// You can assemble the values of this matrix
    /// or the class can mount this array to the automatic values.
    /// </summary>
    public class BitonalOrderedDither : Bitonal
    {
        protected readonly string ErrorMatrixSize = "matrixWidth and matrixHeight must be > 0";
        protected readonly string ErrorthreshoudVal =
            "values of threshould must be between 0 and 255";
        protected int[,] ditherMatrix;
        protected readonly int matrixWidth;
        protected readonly int matrixHeight;

        /// <summary>
        /// Creates a new BitonalOrderedDither with <code>ditherMatrix[matrixWidth][matrixHeight]</code> filled by zeros.
        /// </summary>
        /// <param name="matrixWidth">width of <code>ditherMatrix</code> must be &gt; 0</param>
        /// <param name="matrixHeight">height of <code>ditherMatrix</code> must be &gt; 0</param>
        public BitonalOrderedDither(int matrixWidth, int matrixHeight)
        {
            if (matrixWidth < 1)
            {
                throw new ArgumentException(ErrorMatrixSize);
            }

            if (matrixHeight < 1)
            {
                throw new ArgumentException(ErrorMatrixSize);
            }

            this.matrixWidth = matrixWidth;
            this.matrixHeight = matrixHeight;
            ditherMatrix = new int[this.matrixWidth, this.matrixHeight];
        }

        /// <summary>
        /// Creates a new BitonalOrderedDither with <code>ditherMatrix[matrixWidth][matrixHeight]</code>
        /// automatically filled with values between <code>threshouldMin</code> and <code>threshouldMax</code>.
        /// </summary>
        /// <param name="matrixWidth">width of <code>ditherMatrix</code> must be &gt; 0</param>
        /// <param name="matrixHeight">height of <code>ditherMatrix</code> must be &gt; 0</param>
        /// <param name="thresholdMin">min threshold must be between 0 and 255. 0 is lighter and 255 is darker.</param>
        /// <param name="thresholdMax">max threshold must be between 0 and 255. 0 is lighter and 255 is darker.</param>
        public BitonalOrderedDither(
            int matrixWidth,
            int matrixHeight,
            int thresholdMin,
            int thresholdMax
        )
            : this(matrixWidth, matrixHeight)
        {
            if (thresholdMin < 0 || thresholdMin > 255)
            {
                throw new ArgumentException(ErrorthreshoudVal);
            }

            if (thresholdMax < 0 || thresholdMax > 255)
            {
                throw new ArgumentException(ErrorthreshoudVal);
            }

            if (thresholdMax < thresholdMin)
            {
                throw new ArgumentException("thresholdMax must be >= thresholdMin");
            }

            var matrixSize = (float)(matrixWidth * matrixHeight);
            var thresholdUtil = (float)(thresholdMax - thresholdMin);
            var valueToBeAddedOnEachPosition = (float)thresholdUtil / (matrixSize - 1F);
            var positionValue = (float)thresholdMin;
            var randomCoordinates = new Random(1);
            var shuffledX = Shuffle(matrixWidth, randomCoordinates);
            var shuffledY = Shuffle(matrixHeight, randomCoordinates);
            for (var x = 0; x < matrixWidth; x++)
            {
                for (var y = 0; y < matrixHeight; y++)
                {
                    ditherMatrix[shuffledX[x], shuffledY[y]] = (int)Math.Round(positionValue);
                    positionValue += valueToBeAddedOnEachPosition;
                }
            }
        }

        /// <summary>
        /// Creates a new BitonalOrderedDither with <code>ditherMatrix[matrixWidth][matrixHeight]</code>
        /// automatically filled with values between <code>threshouldMin</code> and <code>threshouldMax</code>.
        /// </summary>
        /// <param name="matrixWidth">width of <code>ditherMatrix</code> must be &gt; 0</param>
        /// <param name="matrixHeight">height of <code>ditherMatrix</code> must be &gt; 0</param>
        /// <param name="thresholdMin">min threshold must be between 0 and 255. 0 is lighter and 255 is darker.</param>
        /// <param name="thresholdMax">max threshold must be between 0 and 255. 0 is lighter and 255 is darker.</param>
        private int[] Shuffle(int size, Random random)
        {
            var set = new HashSet<int>();
            var intArray = new int[size];
            var i = 0;
            while (set.Count < size)
            {
                int val = random.Next(size);
                if (set.Contains(val))
                {
                    continue;
                }

                set.Add(val);
                intArray[i++] = val;
            }

            return intArray;
        }

        /// <summary>
        /// Creates a new BitonalOrderedDither with default values.
        /// </summary>
        public BitonalOrderedDither()
            : this(2, 2, 64, 127) { }

        /// <summary>
        /// Set ditherMatrix value.
        /// You can assemble special matrix, like watermark pattern.It's up to you.
        /// </summary>
        /// <param name="ditherMatrix">matrix filled by the user. Should be <code>new int[matrixWidth][matrixHeight]</code></param>
        public virtual void SetDitherMatrix(int[,] ditherMatrix)
        {
            this.ditherMatrix = ditherMatrix;
        }

        /// <summary>
        /// translate RGBA colors to 0 or 1 (print or not).
        /// the return is based on ditherMatrix values.
        /// </summary>
        /// <param name="alpha">range from 0 to 255</param>
        /// <param name="red">range from 0 to 255</param>
        /// <param name="green">range from 0 to 255</param>
        /// <param name="blue">range from 0 to 255</param>
        /// <param name="x">the X coordinate of the image</param>
        /// <param name="y">the Y coordinate of the image</param>
        /// <returns> 0 or 1</returns>
        /// <remarks>@seeBitonal#zeroOrOne(int, int, int, int, int, int)</remarks>
        public override int ZeroOrOne(int alpha, int red, int green, int blue, int x, int y)
        {
            var luminance = 0xFF;
            if (alpha > 127)
            {
                luminance = (red + green + blue) / 3;
            }

            var threshold = ditherMatrix[x % matrixWidth, y % matrixHeight];
            return (luminance < threshold) ? 1 : 0;
        }
    }
}
