using System;

namespace UltimateCipher
{
    public class CipherInfo
    {
        public string Name { get; private set; }
        public string ColorblindLetter { get; private set; }
        public bool ColorblindWhite { get; private set; }
        public Func<ultimateCipher, Func<string, bool, PageInfo[]>> Cipher { get; private set; }
        public int TpPoints { get; private set; }
        public int Index { get; private set; }

        public CipherInfo(string name, Func<ultimateCipher, Func<string, bool, PageInfo[]>> cipher, int tpPoints, string cbLetter, bool cbWhite = false, int index = 0)
        {
            Name = name;
            Cipher = cipher;
            TpPoints = tpPoints;
            Index = index;
            ColorblindLetter = cbLetter;
            ColorblindWhite = cbWhite;
        }

        public SpecificCipherInfo GetSpecific(bool inverted)
        {
            return new SpecificCipherInfo(Name, Cipher, TpPoints, Index, ColorblindLetter, ColorblindWhite, inverted);
        }

        public CipherInfo WithIndex(int index)
        {
            return new CipherInfo(Name, Cipher, TpPoints, ColorblindLetter, ColorblindWhite, index);
        }
    }

    public class SpecificCipherInfo : CipherInfo
    {
        public bool Inverted { get; private set; }
        public string FullName { get { return (Inverted ? "Inverted " : "") + Name + " Cipher"; } }

        public SpecificCipherInfo(string name, Func<ultimateCipher, Func<string, bool, PageInfo[]>> cipher, int tpPoints, int index, string cbLetter, bool cbWhite, bool inverted) : base(name, cipher, tpPoints, cbLetter, cbWhite, index)
        {
            Inverted = inverted;
        }

        public PageInfo[] RunCipher(ultimateCipher uc, string word)
        {
            return Cipher(uc)(word, Inverted);
        }
    }
}
