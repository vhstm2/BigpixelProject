// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("hb+hnMXst0PmrxvOMXJyLhQo5SMFzt0pv4aKQgaVBbfQ0YG6vZfo9ftJyun7xs3C4U2DTTzGysrKzsvIu8sxV7OGUb/SGs61EQoLpdXiuSeOpw+11ke+Xd5cZSrCfIRtDJHD06pd98tdua3TvyH7GpZj71jF2g49/dYgomcLP/Qf5Up8AOO4sq5IjIKo8vhoxLDs8BfI4itJjiDo7uE1Bf+jUHAqAK9DjmVWp4yjg9o/+b+mRntrPzaGKZYHTNMmUV/OcX/x49GEM7FJALrBZkUZiL+XvBbfzMwwvAVVv+jQ2RSrlLuv3uxGhKrzaB7f/QDP+wq3/f2zIJeaPZci89vjvwVJysTL+0nKwclJysrLUcrVkqyIWYENbLkiALM6JMnIysvK");
        private static int[] order = new int[] { 5,7,5,13,5,5,10,12,8,13,10,13,13,13,14 };
        private static int key = 203;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
