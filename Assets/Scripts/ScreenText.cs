namespace UltimateCipher
{
    public struct ScreenText
    {
        public string Text { get; set; }
        public int FontSize { get; private set; }
        public bool SouvenirAvoid { get; private set; }

        public ScreenText(string text, int fontSize, bool souvenirAvoid = false)
        {
            Text = text;
            FontSize = fontSize;
            SouvenirAvoid = souvenirAvoid;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}