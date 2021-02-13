using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;
using Words;
public class blueCipher : MonoBehaviour
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
        Debug.LogFormat("[Blue Cipher #{0}] Generated Word: {1}", moduleId, answer);
        pages = new string[2][];
        pages[0] = new string[3];
        pages[1] = new string[3];
        pages[0][0] = "";
        pages[0][1] = "";
        pages[0][2] = "";
        string encrypt = bluecipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string bluecipher(string word)
    {
        Debug.LogFormat("[Blue Cipher #{0}] Begin Vigenere Encryption", moduleId);
        string kw = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpper();
        wordList[2].Remove(kw.ToUpper());
        string encrypt = VigenereEnc(word.ToUpper(), kw.ToUpper());
        Debug.LogFormat("[Blue Cipher #{0}] Begin Atbash Encryption", moduleId);
        encrypt = Atbash(encrypt.ToUpper());
        Debug.LogFormat("[Blue Cipher #{0}] Begin Tridigital Encryption", moduleId);
        TridigitalEnc(kw);
        return encrypt;
    }
    string VigenereEnc(string word, string kw)
    {
        string key = "ZABCDEFGHIJKLMNOPQRSTUVWXY";
        string encrypt = "";
        for (int aa = 0; aa < 6; aa++)
        {
            encrypt = encrypt + "" + key[(key.IndexOf(word[aa]) + key.IndexOf(kw[aa])) % 26];
            Debug.LogFormat("[Blue Cipher #{0}] {1} + {2} = {3}", moduleId, word[aa], kw[aa], encrypt[aa]);
        }
        return encrypt;
    }
    /*
    string LetterTrans(string word)
    {
        string encrypt = word.ToUpper();
        string[][] table =
        {
            new string[] {"34", "16", "14", "24", "25", "23", "45", "34", "R4", "24"},
            new string[] {"26", "14", "RV", "15", "R2", "34", "56", "R3", "26", "25"},
            new string[] {"23", "RV", "R1", "RV", "12", "25", "36", "46", "R2", "25"},
            new string[] {"35", "12", "46", "24", "45", "R5", "13", "15", "26", "R5"},
            new string[] {"R1", "13", "14", "16", "35", "12", "35", "R3", "25", "R4"},
            new string[] {"23", "45", "R3", "46", "16", "36", "R4", "R5", "34", "R2"},
            new string[] {"13", "12", "RV", "12", "R3", "35", "36", "15", "36", "23"},
            new string[] {"45", "24", "56", "R4", "R5", "R2", "35", "23", "56", "46"},
            new string[] {"RV", "26", "R1", "13", "13", "56", "15", "15", "24", "34"},
            new string[] {"36", "R1", "14", "56", "16", "45", "16", "14", "26", "46"}
        };
        string[] swaps = {"", ""};
        for(int aa = 0; aa < 6; aa++)
        {
            int n1 = UnityEngine.Random.Range(0, 10);
            int n2 = UnityEngine.Random.Range(0, 10);
            swaps[0] = n1 + "" + swaps[0];
            swaps[1] = n2 + "" + swaps[1];
            string instruction = table[n2][n1].ToUpper();
            string logoutput = instruction.ToUpper() + ": " + encrypt + " -> ";
            if (instruction.Equals("RV"))
            {
                string conv = "";
                for (int bb = 0; bb < 6;bb++)
                    conv = encrypt[bb] + "" + conv;
                encrypt = conv.ToUpper();
            }
            else if (instruction[0] == 'R')
            {
                for (int bb = 0; bb < (instruction[1] - '0'); bb++)
                    encrypt = encrypt.Substring(1) + "" + encrypt[0];
            }
            else
            {
                char[] lets = { encrypt[0], encrypt[1], encrypt[2], encrypt[3], encrypt[4], encrypt[5] };
                lets[instruction[0] - '0' - 1] = encrypt[instruction[1] - '0' - 1];
                lets[instruction[1] - '0' - 1] = encrypt[instruction[0] - '0' - 1];
                encrypt = "";
                for (int bb = 0; bb < 6; bb++)
                    encrypt = encrypt + "" + lets[bb];
            }
            logoutput = logoutput + "" + encrypt.ToUpper();
            Debug.LogFormat("[Blue Cipher #{0}] {1}", moduleId, logoutput);
        }
        pages[0][1] = swaps[0].ToUpper();
        pages[0][2] = swaps[1].ToUpper();
        return encrypt;
    }*/
    string Atbash(string word)
    {
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string encrypt = "";
        for (int aa = 0; aa < 6; aa++)
        {
            encrypt = encrypt + "" + alpha[25 - alpha.IndexOf(word[aa])];
            Debug.LogFormat("[Blue Cipher #{0}] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
        }
        return encrypt;
    }
    void TridigitalEnc(string word)
    {
        int length = UnityEngine.Random.Range(0, wordList.Count);
        string kw = wordList[length][UnityEngine.Random.Range(0, wordList[length].Count)].ToUpper();
        wordList[length].Remove(kw.ToUpper());
        string key = getKey(kw.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetIndicators().Count() % 2 == 0);
        Debug.LogFormat("[Blue Cipher #{0}] Tridigital Key: {1}", moduleId, key);
        pages[0][1] = "";
        pages[0][2] = "";
        pages[1][0] = kw.ToUpper();
        for (int aa = 0; aa < word.Length; aa++)
        {
            pages[0][1] = pages[0][1] + "" + ((key.IndexOf(word[aa]) / 9) + 1);
            pages[0][2] = pages[0][2] + "" + ((key.IndexOf(word[aa]) % 9) + 1);
            Debug.LogFormat("[Blue Cipher #{0}] {1} -> {2}{3}", moduleId, word[aa], pages[0][1][aa], pages[0][2][aa]);
        }
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
        screenTexts[1].fontSize = 40;
        screenTexts[2].fontSize = 40;
        if (page == 0)
            screenTexts[0].fontSize = 40;
        else
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
