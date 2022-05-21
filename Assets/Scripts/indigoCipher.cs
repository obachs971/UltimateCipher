using Words;
public class indigoCipher : cipherBase
{
    protected override string Name { get { return "Indigo"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("INDIGO", "Generated Word: {0}", answer);
        pages = indigocipher(answer);
    }
}
