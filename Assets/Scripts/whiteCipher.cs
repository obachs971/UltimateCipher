using Words;
public class whiteCipher : cipherBase
{
    protected override string Name { get { return "White"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("WHITE", "Generated Word: {0}", answer);
        pages = whitecipher(answer);
    }
}
