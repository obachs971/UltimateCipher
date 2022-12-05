using Words;
public class violetCipher : cipherBase
{
    protected override string Name { get { return "Violet"; } }
    protected override string ColorblindLetter { get { return "V"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("VIOLET", "Generated Word: {0}", answer);
        pages = violetcipher(answer);
    }
}
