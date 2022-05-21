using Words;
public class greenCipher : cipherBase
{
    protected override string Name { get { return "Green"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("GREEN", "Generated Word: {0}", answer);
        pages = greencipher(answer);
    }
}
