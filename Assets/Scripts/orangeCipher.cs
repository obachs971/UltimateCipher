using Words;
public class orangeCipher : cipherBase
{
    protected override string Name { get { return "Orange"; } }
    protected override string ColorblindLetter { get { return "O"; } }

    static int moduleIdCounter = 1;

    protected override void Initialize()
    {
        moduleId = moduleIdCounter++;
        answer = new Data().PickWord(6);
        Log("ORANGE", "Generated Word: {0}", answer);
        pages = orangecipher(answer);
    }
}
