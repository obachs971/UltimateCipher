using Words;
public class brownCipher : cipherBase
{
    protected override string Name { get { return "Brown"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("BROWN", "Generated Word: {0}", answer);
        pages = browncipher(answer);
    }
}
