namespace EscPosSharp
{
    /// <summary>
    /// Values for print justification.
    /// </summary>
    public class Justification
    {
        public static Justification Left_Default = new Justification(48);
        public static Justification Center = new Justification(49);
        public static Justification Right = new Justification(50);

        public int value;

        public Justification(int value)
        {
            this.value = value;
        }
    }
}