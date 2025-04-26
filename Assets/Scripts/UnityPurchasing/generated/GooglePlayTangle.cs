// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("5N0CA5TMNgRJTSsdaHFG5qWVnrmnJColFackLyenJCQl3U+dEEhlZOBSI7QTg7rVID5ru8ceLQOdAy1XYj2sc1g1rMBED/g2YLD74IaiC37rU179NNXVsPVJVQyxrf634AnWbgf6rTpWvtIxPhgauccaxU32BfyUzv9onKztuecmeV7/Xi4KQUVqadgVpyQHFSgjLA+jbaPSKCQkJCAlJtVxMv5rdU6hQ7DOI2UHEmXuzookMZhJ35Iy6zHPBBpW12Po7iNSvtaUAyi3b6GDtZtdje3siYKo3BVrGjc+a/c6uamDOhR0RzwPHsZEPInYqVuIbyWHuThDXathVYtfIHbQ5fkesUV5BTqTXzRSfdlgbpXHmCzN0j8NPePM4BGVPicmJCUk");
        private static int[] order = new int[] { 8,1,2,13,12,11,10,8,11,10,13,11,13,13,14 };
        private static int key = 37;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
