using Words;
public class magentaCipher : cipherBase
{
    protected override string Name { get { return "Magenta"; } }
    protected override string ColorblindLetter { get { return "M"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("MAGENTA", "Generated Word: {0}", answer);
        pages = magentacipher(answer);
    }
}
