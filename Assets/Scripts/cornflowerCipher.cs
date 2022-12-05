using Words;
public class cornflowerCipher : cipherBase
{
    protected override string Name { get { return "Cornflower"; } }
    protected override string ColorblindLetter { get { return "L"; } }

    private static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("CORNFLOWER", "Answer: {0}", answer);
        pages = cornflowercipher(answer);
    }
}
