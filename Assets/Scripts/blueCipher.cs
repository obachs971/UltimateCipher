using Words;
public class blueCipher : cipherBase
{
    protected override string Name { get { return "Blue"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("BLUE", "Generated Word: {0}", answer);
        pages = bluecipher(answer);
    }
}
