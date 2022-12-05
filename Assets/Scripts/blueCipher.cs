using Words;
public class blueCipher : cipherBase
{
    protected override string Name { get { return "Blue"; } }
    protected override string ColorblindLetter { get { return "B"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("BLUE", "Generated Word: {0}", answer);
        pages = bluecipher(answer);
    }
}
