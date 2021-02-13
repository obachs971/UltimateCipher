using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;
using Words;
public class violetCipher : MonoBehaviour
{

    public TextMesh[] screenTexts;
    public KMBombInfo Bomb;
    public KMBombModule module;
    public AudioClip[] sounds;
    public KMAudio Audio;
    public TextMesh submitText;
    private List<List<string>> wordList = new List<List<string>>();
    private string[][] pages;
    private string answer;
    private int page;
    private bool submitScreen;
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    public KMSelectable leftArrow;
    public KMSelectable rightArrow;
    public KMSelectable submit;
    public KMSelectable[] keyboard;
    void Awake()
    {
        moduleId = moduleIdCounter++;
        Data data = new Data();
        wordList = data.allWords;
        leftArrow.OnInteract += delegate () { left(leftArrow); return false; };
        rightArrow.OnInteract += delegate () { right(rightArrow); return false; };
        submit.OnInteract += delegate () { submitWord(submit); return false; };
        foreach (KMSelectable keybutton in keyboard)
        {
            KMSelectable pressedButton = keybutton;
            pressedButton.OnInteract += delegate () { letterPress(pressedButton); return false; };
        }
    }
    // Use this for initialization
    void Start()

    {
        submitText.text = "1";
        //Generating random word
        answer = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpper();
        wordList[2].Remove(answer.ToUpper());
        Debug.LogFormat("[Violet Cipher #{0}] Generated Word: {1}", moduleId, answer);
        pages = new string[2][];
        pages[0] = new string[3];
        pages[1] = new string[3];
        pages[0][0] = "";
        pages[0][1] = "";
        pages[0][2] = "";
        string encrypt = violetcipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string violetcipher(string word)
    {
        Debug.LogFormat("[Violet Cipher #{0}] Begin Quagmire Encryption", moduleId);
        string kw1 = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpper();
        wordList[2].Remove(kw1.ToUpper());
        string encrypt = QuagmireEnc(word, kw1.ToUpper());
        Debug.LogFormat("[Violet Cipher #{0}] Begin Route Transposition", moduleId);
        encrypt = RouteTrans(encrypt.ToUpper());
        Debug.LogFormat("[Violet Cipher #{0}] Begin Porta Encryption", moduleId);
        encrypt = PortaEnc(encrypt.ToUpper(), kw1);
        return encrypt;
    }
    string PortaEnc(string word, string kw1)
    {
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string[] chart =
        {
            "NOPQRSTUVWXYZ",
            "OPQRSTUVWXYZN",
            "PQRSTUVWXYZNO",
            "QRSTUVWXYZNOP",
            "RSTUVWXYZNOPQ",
            "STUVWXYZNOPQR",
            "TUVWXYZNOPQRS",
            "UVWXYZNOPQRST",
            "VWXYZNOPQRSTU",
            "WXYZNOPQRSTUV",
            "XYZNOPQRSTUVW",
            "YZNOPQRSTUVWX",
            "ZNOPQRSTUVWXY"
        };
        string encrypt = "";
        for (int aa = 0; aa < 6; aa++)
        {
            if (alpha.IndexOf(word[aa]) < 13)
                encrypt = encrypt + "" + chart[alpha.IndexOf(kw1[aa]) / 2][alpha.IndexOf(word[aa])];
            else
                encrypt = encrypt + "" + alpha[chart[alpha.IndexOf(kw1[aa]) / 2].IndexOf(word[aa])];
            Debug.LogFormat("[Violet Cipher #{0}] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
        }
        return encrypt;
    }
    string RouteTrans(string word)
    {
        string routenumber = UnityEngine.Random.Range(1, 3) + "" + UnityEngine.Random.Range(1, 7);
        pages[0][2] = routenumber;
        string encrypt = "";
        string cipher = "";
        switch (routenumber[0])
        {
            case '1':
                switch (routenumber[1])
                {
                    case '1':
                        cipher = "123654";
                        break;
                    case '2':
                        cipher = "234165";
                        break;
                    case '3':
                        cipher = "345216";
                        break;
                    case '4':
                        cipher = "456321";
                        break;
                    case '5':
                        cipher = "561432";
                        break;
                    default:
                        cipher = "612543";
                        break;
                }
                break;
            default:
                switch (routenumber[1])
                {
                    case '1':
                        cipher = "126354";
                        break;
                    case '2':
                        cipher = "231465";
                        break;
                    case '3':
                        cipher = "342516";
                        break;
                    case '4':
                        cipher = "453621";
                        break;
                    case '5':
                        cipher = "564132";
                        break;
                    default:
                        cipher = "615243";
                        break;
                }
                break;
        }
        string order = "123456";
        for (int aa = 0; aa < 6; aa++)
        {
            encrypt = encrypt + "" + word[cipher.IndexOf(order[aa])];
        }
        Debug.LogFormat("[Violet Cipher #{0}] {1} + {2} => {3}", moduleId, word, routenumber, encrypt);
        return encrypt;
    }
    string QuagmireEnc(string word, string kw1)
    {
        int length = UnityEngine.Random.Range(0, wordList.Count);
        string kw2 = wordList[length][UnityEngine.Random.Range(0, wordList[length].Count)].ToUpper();
        wordList[length].Remove(kw2.ToUpper());
        string key = getKey(kw2.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetOnIndicators().Count() % 2 == 0);
        string[] cipher = new string[7];
        cipher[0] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < 6; aa++)
            cipher[aa + 1] = key.Substring(key.IndexOf(kw1[aa])) + "" + key.Substring(0, key.IndexOf(kw1[aa]));
        Debug.LogFormat("[Violet Cipher #{0}] Quagmire Rows: ", moduleId);
        Debug.LogFormat("[Violet Cipher #{0}] {1}", moduleId, cipher[1]);
        Debug.LogFormat("[Violet Cipher #{0}] {1}", moduleId, cipher[2]);
        Debug.LogFormat("[Violet Cipher #{0}] {1}", moduleId, cipher[3]);
        Debug.LogFormat("[Violet Cipher #{0}] {1}", moduleId, cipher[4]);
        Debug.LogFormat("[Violet Cipher #{0}] {1}", moduleId, cipher[5]);
        Debug.LogFormat("[Violet Cipher #{0}] {1}", moduleId, cipher[6]);
        pages[1][0] = kw2.ToUpper();
        pages[0][1] = kw1.ToUpper();
        string encrypt = "";
        for (int bb = 0; bb < 6; bb++)
        {
            encrypt = encrypt + "" + cipher[bb + 1][cipher[0].IndexOf(word[bb])];
            Debug.LogFormat("[Violet Cipher #{0}] {1} -> {2}", moduleId, word[bb], encrypt[bb]);
        }
        return encrypt;
    }
    string getKey(string k, string alpha, bool start)
    {
        for (int aa = 0; aa < k.Length; aa++)
        {
            for (int bb = aa + 1; bb < k.Length; bb++)
            {
                if (k[aa] == k[bb])
                {
                    k = k.Substring(0, bb) + "" + k.Substring(bb + 1);
                    bb--;
                }
            }
            alpha = alpha.Replace(k[aa].ToString(), "");
        }
        if (start)
            return (k + "" + alpha);
        else
            return (alpha + "" + k);
    }
    int correction(int p, int max)
    {
        while (p < 0)
            p += max;
        while (p >= max)
            p -= max;
        return p;
    }
    void left(KMSelectable arrow)
    {
        if (!moduleSolved)
        {
            Audio.PlaySoundAtTransform(sounds[0].name, transform);
            submitScreen = false;
            arrow.AddInteractionPunch();
            page--;
            page = correction(page, pages.Length);
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
            page++;
            page = correction(page, pages.Length);
            getScreens();
        }
    }
    private void getScreens()
    {
        submitText.text = (page + 1) + "";
        screenTexts[0].text = pages[page][0];
        screenTexts[1].text = pages[page][1];
        screenTexts[2].text = pages[page][2];
        screenTexts[0].fontSize = 40;
        screenTexts[1].fontSize = 40;
        screenTexts[2].fontSize = 40;
        if (page == 1)
            screenTexts[0].fontSize = 35;

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
                {
                    screenTexts[2].text = screenTexts[2].text + "" + pressed.GetComponentInChildren<TextMesh>().text;
                }
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
    private string TwitchHelpMessage = "Move to other screens using !{0} right|left|r|l|. Submit the decrypted word with !{0} submit qwertyuiopasdfghjklzxcvbnm";
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
