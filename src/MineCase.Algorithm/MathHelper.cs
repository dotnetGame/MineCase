namespace MineCase.Algorithm
{
    public class MathHelper
    {
        public static float DenormalizeClamp(float min, float max, float value)
        {
            if (value < 0)
            {
                return min;
            }
            else if (value > 1)
            {
                return max;
            }
            else
            {
                return min + (max - min) * value;
            }
        }
    }
}
