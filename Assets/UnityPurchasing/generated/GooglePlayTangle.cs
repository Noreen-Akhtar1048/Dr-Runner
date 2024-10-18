// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("PRHf8ZBRZthHGAqVemv2xNTaa43dAsA8V0bqQVf+kgDLsxyB8jcyCYiFjdEQvO8N0y4Za/gBtX2HOYWakxAeESGTEBsTkxAQEZu+69vj3jLPlwEqI+RnsXsgXqquxTyEQU8ec38e5P3rRNBIG08EEs33Ptp9AA6b0xlVjKFpSXMYQQk/6ZTny1zkKZrdddDqpftQIO/wEq9ZjVFYYzzht8AgPfpKezdYsURRQx7wGOC3WqNf1m7bCcr1P29s/yFz6vrWlSE3diqhShQPuWyM9KZz6Gm4STvQtQyDi58cq2JbPafA5W0QEHK6eTaC2QyooJ8w3VRmiQqBKLNhssmR0ZDumc8hkxAzIRwXGDuXWZfmHBAQEBQREjfstneTKrqqJBMSEBEQ");
        private static int[] order = new int[] { 7,13,6,13,4,13,9,8,13,9,12,11,12,13,14 };
        private static int key = 17;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
