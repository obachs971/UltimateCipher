using Words;
public class brownCipher : cipherBase
{
    protected override string Name { get { return "Brown"; } }
    protected override string ColorblindLetter { get { return "N"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("BROWN", "Generated Word: {0}", answer);
        pages = browncipher(answer);
    }
}
