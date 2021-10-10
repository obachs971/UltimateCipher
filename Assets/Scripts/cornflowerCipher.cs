using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;
using Words;

using Rnd = UnityEngine.Random;
using System.Runtime.Remoting.Messaging;

public class cornflowerCipher : MonoBehaviour
{
    public TextMesh[] screenTexts;
    public KMBombInfo Bomb;
    public KMBombModule module;
    public AudioClip[] sounds;
    public KMAudio Audio;
    public TextMesh submitText;

    public KMSelectable leftArrow;
    public KMSelectable rightArrow;
    public KMSelectable submit;
    public KMSelectable[] keyboard;

    private string[][] pages;
    private List<List<string>> wordList;
    private string answer;
    private int page;
    private bool submitScreen;
    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        moduleId = moduleIdCounter++;

        leftArrow.OnInteract += delegate () { left(leftArrow); return false; };
        rightArrow.OnInteract += delegate () { right(rightArrow); return false; };
        submit.OnInteract += delegate () { submitWord(submit); return false; };

        foreach (KMSelectable keybutton in keyboard)
        {
            KMSelectable pressedButton = keybutton;
            pressedButton.OnInteract += delegate () { letterPress(pressedButton); return false; };
        }
    }

    void Start()
    {
        wordList = new Data().allWords;
        // Answer is always 6 letters long
        answer = pickWord(6);
        Debug.LogFormat("[Cornflower Cipher #{0}] Answer: {1}", moduleId, answer);

        pages = Enumerable.Range(0, 2).Select(i => Enumerable.Repeat("", 3).ToArray()).ToArray();
        var encrypted = cornflowercipher(answer);
        pages[0][0] = encrypted.Substring(0, encrypted.Length / 2);
        pages[0][1] = encrypted.Substring(encrypted.Length / 2);
        page = 0;
        getScreens();
    }

    string cornflowercipher(string word)
    {
        var bitsInt = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumber().First());
        var bits = Enumerable.Range(0, 5).Select(bit => (bitsInt & (1 << bit)) != 0).ToArray();

        // Chain Rotation Cipher
        var chainRotationN = Rnd.Range(1, 10);
        var chainRotationEncr = "";
        while (word.Length > 0)
        {
            var obt = word[word.Length - 1];
            word = word.Remove(word.Length - 1);
            if (word.Length > 0)
                obt = (char) ('A' + (obt - 'A' + 52 - (word[word.Length - 1] - 'A' + 1)) % 26);
            chainRotationEncr = obt + chainRotationEncr;
            var amt = chainRotationN % chainRotationEncr.Length;
            chainRotationEncr = chainRotationEncr.Substring(chainRotationEncr.Length - amt) + chainRotationEncr.Substring(0, chainRotationEncr.Length - amt);
        }

        Debug.LogFormat("[Cornflower Cipher #{0}] Before Chain Rotation Cipher: {1}", moduleId, chainRotationEncr);
        Debug.LogFormat("[Cornflower Cipher #{0}] Chain Rotation Cipher amount: {1}", moduleId, chainRotationN);

        // Stunted Blind Polybius Cipher
        Debug.LogFormat("[Cornflower Cipher #{0}] Braille: {1}", moduleId, toBraille(chainRotationEncr));
        var brailleDots = "1,12,14,145,15,124,1245,125,24,245,13,123,134,1345,135,1234,12345,1235,234,2345,136,1236,2456,1346,13456,1356"
            .Split(',').Select(d => Enumerable.Range(0, 6).Select(i => d.Contains((char) ('1' + i))).ToArray()).ToArray();

        // “nibble” = 4 bits. Some nibbles are a 2×2 square within a Braille letter, some are the bottom 2 dots of one Braille letter and the top 2 dots of the next
        var brailleNibbles = new int[(chainRotationEncr.Length * 3 + 1) / 2];
        for (int i = 0; i < chainRotationEncr.Length; i++)
            for (var dot = 0; dot < 6; dot++)
                if (brailleDots[chainRotationEncr[i] - 'A'][dot])
                    brailleNibbles[(dot % 3 + 3 * i) / 2] |= 1 << ((dot % 3 + 3 * i) % 2) * 2 + dot / 3;

        Debug.LogFormat("[Cornflower Cipher #{0}] Braille nibbles: {1}", moduleId, brailleNibbles.Select(i => "⠀⠁⠈⠉⠂⠃⠊⠋⠐⠑⠘⠙⠒⠓⠚⠛"[i]).Join(""));

        var kw3 = pickWord(8);
        Debug.LogFormat("[Cornflower Cipher #{0}] KW3: {1}", moduleId, kw3);
        var colSeq = sequencing(kw3.Substring(0, 4));
        var rowSeq = sequencing(kw3.Substring(4));
        Debug.LogFormat("[Cornflower Cipher #{0}] Blind Polybius columns: {1}; rows: {2}", moduleId, colSeq.Select(i => i + 1).Join(""), rowSeq.Select(i => i + 1).Join(""));

        var polybius = (bits[0] ? (kw3 + "ABCDEFGHIJKLMNOP") : "ABCDEFGHIJKLMNOP".Except(kw3).Concat(kw3)).Distinct().Where(ch => ch <= 'P').Join("");
        Debug.LogFormat("[Cornflower Cipher #{0}] Stunted Polybius square: {1}", moduleId, polybius);

        var encrypted = brailleNibbles.Select(nibble => polybius[colSeq[nibble % 4] + 4 * rowSeq[nibble / 4]]).Join("");
        Debug.LogFormat("[Cornflower Cipher #{0}] Encrypted word: {1}", moduleId, encrypted);

        // Backward Straddling Checkerboard Cipher
        var kw2 = pickWord(4, 8);
        var d3 = Bomb.GetBatteryCount() % 6;
        var d4 = Bomb.GetOnIndicators().Count() % 6;
        if (d4 == d3)
            d4 = (d4 + 1) % 6;

        var rowDigits2 = Enumerable.Range(0, 6).Where(d => d != d3 && d != d4).ToArray();
        var straddlingCheckerboard2 = MakeStraddlingCheckerboard(bits[1], bits[2], kw2, rowDigits2);

        var encryptedDigits = new List<int>();
        foreach (var ch in kw3)
        {
            var ix = straddlingCheckerboard2.IndexOf(ch);
            if (ix >= 6)
                encryptedDigits.Add(rowDigits2[ix / 6 - 1]);
            encryptedDigits.Add(ix % 6);
        }

        // Forward Straddling Checkerboard Cipher
        var kw1 = pickWord(4, 8);
        var d1 = Bomb.GetIndicators().Count() % 6;
        var d2 = Bomb.GetPortCount() % 6;
        if (d2 == d1)
            d2 = (d2 + 1) % 6;

        var rowDigits1 = Enumerable.Range(0, 6).Where(d => d != d1 && d != d2).ToArray();
        var straddlingCheckerboard1 = MakeStraddlingCheckerboard(bits[3], bits[4], kw1, rowDigits1);

        var straddlingCheckerBoardEncrypted = "";
        for (var i = 0; i < encryptedDigits.Count; i++)
        {
            if (encryptedDigits[i] == d1 || encryptedDigits[i] == d2)
                straddlingCheckerBoardEncrypted += straddlingCheckerboard1[encryptedDigits[i]];
            else
            {
                if (i == encryptedDigits.Count - 1)
                    encryptedDigits.Add(rowDigits2.Where(d => d != d1 && d != d2).First());
                straddlingCheckerBoardEncrypted += straddlingCheckerboard1[(Array.IndexOf(rowDigits1, encryptedDigits[i]) + 1) * 6 + encryptedDigits[i + 1]];
                i++;
            }
        }

        Debug.LogFormat("[Cornflower Cipher #{0}] Backward Straddling Checkerboard Cipher: KW2: {1}, D3: {2}, D4: {3}", moduleId, kw2, d3, d4);
        for (var i = 0; i < 5; i++)
            Debug.LogFormat("[Cornflower Cipher #{0}] Backward Straddling Checkerboard Cipher: Row [{1}] = [{2}]", moduleId, i == 0 ? " " : rowDigits2[i - 1].ToString(), straddlingCheckerboard2.Substring(6 * i, 6).Join(" "));
        Debug.LogFormat("[Cornflower Cipher #{0}] Backward Straddling Checkerboard result: {1}", moduleId, encryptedDigits.Join(""));
        Debug.LogFormat("[Cornflower Cipher #{0}] Forward Straddling Checkerboard Cipher: KW1: {1}, D1: {2}, D2: {3}", moduleId, kw1, d1, d2);
        for (var i = 0; i < 5; i++)
            Debug.LogFormat("[Cornflower Cipher #{0}] Forward Straddling Checkerboard Cipher: Row [{1}] = [{2}]", moduleId, i == 0 ? " " : rowDigits1[i - 1].ToString(), straddlingCheckerboard1.Substring(6 * i, 6).Join(" "));
        Debug.LogFormat("[Cornflower Cipher #{0}] Forward Straddling Checkerboard result: {1}", moduleId, straddlingCheckerBoardEncrypted);

        var disp2 = straddlingCheckerBoardEncrypted + " " + chainRotationN;
        pages[0][2] = disp2.Substring(0, disp2.Length / 2);
        pages[1][0] = disp2.Substring(disp2.Length / 2);
        pages[1][1] = kw1;
        pages[1][2] = kw2;

        return encrypted;
    }

    private static string MakeStraddlingCheckerboard(bool keywordFirst, bool inColumns, string kw, int[] rowDigits)
    {
        var alphabet = (keywordFirst ? (kw + "ABCDEFGHIJKLMNOPQRSTUVWXYZ") : "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Except(kw).Concat(kw)).Distinct().Join("");
        for (var i = 0; i < 6; i++)
            if (rowDigits.Contains(i))
                alphabet = alphabet.Insert(inColumns ? 5 * i : i, ".");
        if (inColumns)
            alphabet = Enumerable.Range(0, 30).Select(i => alphabet[(i / 6) + 5 * (i % 6)]).Join("");
        return alphabet;
    }

    private int[] sequencing(string str)
    {
        return str.Select((ch, ix) => str.Count(c => c < ch) + str.Take(ix).Count(c => c == ch)).ToArray();
    }

    private string pickWord(int length)
    {
        var wl = wordList[length - 4];
        var ix = Rnd.Range(0, wl.Count);
        var word = wl[ix];
        wl.RemoveAt(ix);
        return word;
    }

    private string pickWord(int minLength, int maxLength)
    {
        return pickWord(Rnd.Range(minLength, maxLength + 1));
    }

    private string toBraille(string word)
    {
        return word.Select(ch => "⠁⠃⠉⠙⠑⠋⠛⠓⠊⠚⠅⠇⠍⠝⠕⠏⠟⠗⠎⠞⠥⠧⠺⠭⠽⠵"[ch - 'A']).Join("");
    }

    void left(KMSelectable arrow)
    {
        if (!moduleSolved)
        {
            Audio.PlaySoundAtTransform(sounds[0].name, transform);
            submitScreen = false;
            arrow.AddInteractionPunch();
            page = (page + pages.Length - 1) % pages.Length;
            getScreens();
        }
    }

    void right(KMSelectable arrow)
    {
        if (!moduleSolved)
        {
            Audio.PlaySoundAtTransform(sounds[0].name, transform);
            submitScreen = false;
            arrow.AddInteractionPunch();
            page = (page + 1) % pages.Length;
            getScreens();
        }
    }

    private void getScreens()
    {
        submitText.text = (page + 1) + "";
        screenTexts[0].text = pages[page][0];
        screenTexts[1].text = pages[page][1];
        screenTexts[2].text = pages[page][2];
        screenTexts[0].fontSize = 45;
        screenTexts[1].fontSize = page == 0 ? 45 : 35;
        screenTexts[2].fontSize = page == 0 ? 40 : 35;
    }

    void submitWord(KMSelectable submitButton)
    {
        if (!moduleSolved)
        {
            submitButton.AddInteractionPunch();
            if (screenTexts[2].text.Equals(answer))
            {
                Audio.PlaySoundAtTransform(sounds[2].name, transform);
                module.HandlePass();
                moduleSolved = true;
                screenTexts[2].text = "";
            }
            else
            {
                Audio.PlaySoundAtTransform(sounds[3].name, transform);
                module.HandleStrike();
                page = 0;
                getScreens();
                submitScreen = false;
            }
        }
    }

    void letterPress(KMSelectable pressed)
    {
        if (!moduleSolved)
        {
            pressed.AddInteractionPunch();
            Audio.PlaySoundAtTransform(sounds[1].name, transform);
            if (submitScreen)
            {
                if (screenTexts[2].text.Length < 6)
                    screenTexts[2].text = screenTexts[2].text + pressed.GetComponentInChildren<TextMesh>().text;
            }
            else
            {
                submitText.text = "SUB";
                screenTexts[0].text = "";
                screenTexts[1].text = "";
                screenTexts[2].text = pressed.GetComponentInChildren<TextMesh>().text;
                screenTexts[2].fontSize = 40;
                submitScreen = true;
            }
        }
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "!{0} right/left/r/l [move between screens] | !{0} submit answerword";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {

        if (command.EqualsIgnoreCase("right") || command.EqualsIgnoreCase("r"))
        {
            yield return null;
            rightArrow.OnInteract();
            yield return new WaitForSeconds(0.1f);

        }
        if (command.EqualsIgnoreCase("left") || command.EqualsIgnoreCase("l"))
        {
            yield return null;
            leftArrow.OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        string[] split = command.ToUpperInvariant().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        if (split.Length != 2 || !split[0].Equals("SUBMIT") || split[1].Length != 6) yield break;
        int[] buttons = split[1].Select(getPositionFromChar).ToArray();
        if (buttons.Any(x => x < 0)) yield break;

        yield return null;

        yield return new WaitForSeconds(0.1f);
        foreach (char let in split[1])
        {
            keyboard[getPositionFromChar(let)].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        submit.OnInteract();
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        if (submitScreen && !answer.StartsWith(screenTexts[2].text))
        {
            KMSelectable[] arrows = new KMSelectable[] { leftArrow, rightArrow };
            arrows[UnityEngine.Random.Range(0, 2)].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        int start = submitScreen ? screenTexts[2].text.Length : 0;
        for (int i = start; i < 6; i++)
        {
            keyboard[getPositionFromChar(answer[i])].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        submit.OnInteract();
        yield return new WaitForSeconds(0.1f);
    }

    private int getPositionFromChar(char c)
    {
        return "QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(c);
    }
}
