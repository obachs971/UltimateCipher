using Words;

public class blackCipher : cipherBase
{
    protected override string Name { get { return "Black"; } }
    protected override string ColorblindLetter { get { return "K"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("BLACK", "Generated Word: {0}", answer);
        pages = blackcipher(answer);
    }
}
