using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Core
{
    public static class Decoder
    {

        public static string Encode(string encodeMe)
        {
            return encodeMe == "0" ? EncoderEnum.Ab01.ToString() : encodeMe == "1" ? EncoderEnum.Cd23.ToString() : encodeMe == "2" ? EncoderEnum.Ef45.ToString() : encodeMe == "3" ? EncoderEnum.Gh67.ToString() : encodeMe == "4" ? EncoderEnum.Ij89.ToString() : encodeMe == "5" ? EncoderEnum.Kl10.ToString() : encodeMe == "6" ? EncoderEnum.Mn32.ToString() : encodeMe == "7" ? EncoderEnum.Op54.ToString()
                : encodeMe == "8" ? EncoderEnum.Qr76.ToString():encodeMe == "9" ? EncoderEnum.St98.ToString(): encodeMe == "153" ? EncoderEnum.AbC12.ToString() : encodeMe == "154" ? EncoderEnum.BcZ59.ToString(): encodeMe == "155" ? EncoderEnum.HgD45.ToString(): encodeMe == "156" ? EncoderEnum.JyU14.ToString(): encodeMe == "157" ? EncoderEnum.ZaS18.ToString():EncoderEnum.Cd23.ToString();
        }
        public static string Decode(string decodeMe)
        {
            return decodeMe == EncoderEnum.Ab01.ToString() ? "0" : decodeMe == EncoderEnum.Cd23.ToString() ? "1" : decodeMe == EncoderEnum.Ef45.ToString() ? "2" : decodeMe == EncoderEnum.Gh67.ToString() ? "3" : decodeMe == EncoderEnum.Ij89.ToString()? "4" : decodeMe == EncoderEnum.Kl10.ToString()? "5" : decodeMe == EncoderEnum.Mn32.ToString()? "6" : decodeMe ==EncoderEnum.Op54.ToString()? "7": decodeMe ==EncoderEnum.Qr76.ToString()? "8" : decodeMe == EncoderEnum.St98.ToString()? "9" : decodeMe ==EncoderEnum.AbC12.ToString()? "153" : decodeMe == EncoderEnum.BcZ59.ToString()? "154" : decodeMe == EncoderEnum.HgD45.ToString()? "155" : decodeMe ==EncoderEnum.JyU14.ToString()? "156" : decodeMe == EncoderEnum.ZaS18.ToString()? "157" : EncoderEnum.Cd23.ToString();
        }
    }
    public enum EncoderEnum
    {
        Ab01=0,
        Cd23=1,
        Ef45=2,
        Gh67=3,
        Ij89=4,
        Kl10=5,
        Mn32=6,
        Op54=7,
        Qr76=8,
        St98=9,
        AbC12=153,
        BcZ59=154,
        HgD45=155,
        JyU14=156,
        ZaS18=157

    }
}
