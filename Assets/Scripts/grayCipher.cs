using Words;
public class grayCipher : cipherBase
{
    protected override string Name { get { return "Gray"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("GRAY", "Generated Word: {0}", answer);
        pages = graycipher(answer);
    }
}
