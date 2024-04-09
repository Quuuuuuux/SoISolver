namespace SoISolver.Helpers;

public static class Constants
{
    public static class BaseInfo
    {
        public const int MaxRank = 5;
        public const int MaxDimension = 3;
    
        public static readonly byte[] BaseSet;
    
        static BaseInfo()
        {
            BaseSet = ConstructBaseSet();
        }

        private static byte[] ConstructBaseSet()
        {
            var set = new byte[MaxRank];
            for (var i = 0; i < MaxRank; i++)
            {
                set[i] = (byte)(int)Math.Pow(2, i);
            }

            return set;
        }
    }
}