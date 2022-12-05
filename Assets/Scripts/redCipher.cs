using Words;
public class redCipher : cipherBase
{
    protected override string Name { get { return "Red"; } }
    protected override string ColorblindLetter { get { return "R"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("RED", "Generated Word: {0}", answer);
        pages = redcipher(answer);
    }
}
