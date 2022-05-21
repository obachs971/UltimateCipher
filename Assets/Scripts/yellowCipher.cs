using Words;
public class yellowCipher : cipherBase
{
    protected override string Name { get { return "Yellow"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("YELLOW", "Generated Word: {0}", answer);
        pages = yellowcipher(answer);
    }
}
