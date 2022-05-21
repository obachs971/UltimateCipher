using Words;
public class crimsonCipher : cipherBase
{
    protected override string Name { get { return "Crimson"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("CRIMSON", "Generated Word: {0}", answer);
        pages = crimsoncipher(answer);
    }
}