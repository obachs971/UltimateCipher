using Words;
public class indigoCipher : cipherBase
{
    protected override string Name { get { return "Indigo"; } }
    protected override string ColorblindLetter { get { return "I"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("INDIGO", "Generated Word: {0}", answer);
        pages = indigocipher(answer);
    }
}
