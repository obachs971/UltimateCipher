using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;
using Words;
public class orangeCipher : MonoBehaviour
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
        Debug.LogFormat("[Orange Cipher #{0}] Generated Word: {1}", moduleId, answer);

        pages = new string[2][];
        pages[0] = new string[3];
        pages[1] = new string[3];
        pages[0][0] = "";
        pages[0][1] = "";
        pages[0][2] = "";
        string encrypt = orangecipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string orangecipher(string word)
    {

        string encrypt = "";
        bool[] b = { false, false, false, false, false, false };

        for (int aa = 0; aa < 6; aa++)
        {
            if (word[aa] == 'J')
            {
                encrypt = encrypt + "ABCDEFGHIKLMNOPQRSTUVWXYZ"[UnityEngine.Random.Range(0, 25)];
                b[aa] = true;
            }
            else
            {
                encrypt = encrypt + "" + word[aa];
            }
        }
        int length = UnityEngine.Random.Range(0, wordList.Count);
        string keyword1 = wordList[length][UnityEngine.Random.Range(0, wordList[length].Count)].ToUpper();
        wordList[length].Remove(keyword1.ToUpper());

        length = UnityEngine.Random.Range(0, wordList.Count);
        string keyword2 = wordList[length][UnityEngine.Random.Range(0, wordList[length].Count)].ToUpper();
        wordList[length].Remove(keyword2.ToUpper());

        string number = "";
        for (int aa = 0; aa < 4; aa++)
            number = number + "" + UnityEngine.Random.Range(0, 10);
        pages[1][1] = keyword1.ToUpper();
        pages[1][2] = number;

        string matrixa = keyword1.Replace('J', 'I');
        string matrixb = "AFLQVBGMRWCHNSXDIOTYEKPUZ";
        string matrixc;
        string matrixd = keyword2.Replace('J', 'I');
        //Creating Matrix C by converting Number to Words.
        matrixc = "";
        for (int dd = 0; dd < 4; dd++)
        {
            switch (number[dd])
            {
                case '0':
                    matrixc = matrixc + "ZERO";
                    break;
                case '1':
                    matrixc = matrixc + "ONE";
                    break;
                case '2':
                    matrixc = matrixc + "TWO";
                    break;
                case '3':
                    matrixc = matrixc + "THREE";
                    break;
                case '4':
                    matrixc = matrixc + "FOUR";
                    break;
                case '5':
                    matrixc = matrixc + "FIVE";
                    break;
                case '6':
                    matrixc = matrixc + "SIX";
                    break;
                case '7':
                    matrixc = matrixc + "SEVEN";
                    break;
                case '8':
                    matrixc = matrixc + "EIGHT";
                    break;
                case '9':
                    matrixc = matrixc + "NINE";
                    break;
            }
        }
        string snnums = "";
        string sn = Bomb.GetSerialNumber();
        for (int ff = 0; ff < 6; ff++)
        {
            switch (sn[ff])
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    snnums = snnums + "" + sn[ff];
                    break;
            }
        }
        matrixa = getKey(matrixa, "ABCDEFGHIKLMNOPQRSTUVWXYZ", Bomb.GetSerialNumberNumbers().Last() % 2 == 0);
        matrixc = getKey(matrixc, "ABCDEFGHIKLMNOPQRSTUVWXYZ", (snnums[1] - '0') % 2 == 1);
        matrixd = getKey(matrixd, "ABCDEFGHIKLMNOPQRSTUVWXYZ", (snnums[0] - '0') % 2 == 0);

        Debug.LogFormat("[Orange Cipher #{0}] Matrix A: {1}", moduleId, matrixa);
        Debug.LogFormat("[Orange Cipher #{0}] Matrix B: {1}", moduleId, matrixb);
        Debug.LogFormat("[Orange Cipher #{0}] Matrix C: {1}", moduleId, matrixc);
        Debug.LogFormat("[Orange Cipher #{0}] Matrix D: {1}", moduleId, matrixd);
        Debug.LogFormat("[Orange Cipher #{0}] Begin Foursquare Encryption", moduleId);
        encrypt = FoursquareEnc(encrypt, matrixa, matrixb, matrixc, matrixd);

        Debug.LogFormat("[Orange Cipher #{0}] Begin Bazeries Encryption", moduleId);
        encrypt = BazeriesEnc(encrypt, matrixb, matrixc, number);

        Debug.LogFormat("[Orange Cipher #{0}] Begin Collon Encryption", moduleId);
        keyword2 = CollonEnc(keyword2.Replace('J', 'I'), matrixa);
        pages[0][2] = keyword2.Substring(0, keyword2.Length / 2);
        pages[1][0] = keyword2.Substring(keyword2.Length / 2);

        string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < 6; aa++)
        {
            if (b[aa])
            {
                pages[0][1] = pages[0][1] + "" + encrypt[aa];
                encrypt = encrypt.Substring(0, aa) + "J" + encrypt.Substring(aa + 1);
            }
            else
            {
                pages[0][1] = pages[0][1] + "" + alpha[UnityEngine.Random.Range(0, 25)];
            }
        }
        return encrypt;
    }

    string FoursquareEnc(string word, string ma, string mb, string mc, string md)
    {
        string encrypt = "";
        for (int gg = 0; gg < 6; gg++)
        {
            int n1 = ma.IndexOf(word[gg]);
            int n2 = md.IndexOf(word[gg + 1]);
            gg++;
            encrypt = encrypt + "" + mb[((n1 / 5) * 5) + (n2 % 5)] + "" + mc[(n1 % 5) + ((n2 / 5) * 5)];
            Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}", moduleId, word[gg - 1] + "" + word[gg], encrypt[gg - 1] + "" + encrypt[gg]);
        }
        return encrypt;
    }
    string BazeriesEnc(string word, string mb, string mc, string num)
    {
        string encrypt = "";
        int n = 0;
        int subgroup = 0;
        for (int bb = 0; bb < num.Length; bb++)
            subgroup += (num[bb] - '0');
        subgroup = (subgroup % 4) + 2;
        for (int aa = 0; aa < word.Length; aa++)
        {
            char l = mc[mb.IndexOf(word[aa])];
            Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}", moduleId, word[aa], l);
            encrypt = encrypt + "" + l;
            n++;
            if (n == subgroup)
            {
                encrypt = encrypt + "|";
                n = 0;
            }
        }
        string[] spl = encrypt.Split('|');
        string encrypt2 = "";
        for (int cc = 0; cc < spl.Length; cc++)
        {

            char[] temp = spl[cc].ToCharArray();
            Array.Reverse(temp);
            encrypt2 = encrypt2 + "" + new string(temp);
        }
        Debug.LogFormat("[Orange Cipher #{0}] Subgroup Number: {1}", moduleId, subgroup);
        Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}", moduleId, encrypt, encrypt2);

        return encrypt2;
    }
    string CollonEnc(string word, string key)
    {
        string encrypt = "";
        for (int aa = 0; aa < word.Length; aa++)
        {
            int cursor = key.IndexOf(word[aa]);
            List<int> possRow = new List<int>() { 0, 1, 2, 3, 4 };
            List<int> possCol = new List<int>() { 0, 1, 2, 3, 4 };
            possRow.Remove(cursor % 5);
            possCol.Remove(cursor / 5);
            int row = ((cursor / 5) * 5) + possRow[UnityEngine.Random.Range(0, 4)];
            int col = (cursor % 5) + (possCol[UnityEngine.Random.Range(0, 4)] * 5);
            encrypt = encrypt + "" + key[row] + "" + key[col];
            Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}{3}", moduleId, word[aa], encrypt[aa * 2], encrypt[(aa * 2) + 1]);
        }
        return encrypt;
    }
    /*string ADFGXEnc(string word, string key, string kw)
    {
        string encrypt = "";
        string adfgx = "ADFGX";
        for(int aa = 0; aa < word.Length; aa++)
        {
            int num = key.IndexOf(word[aa]);
            encrypt = encrypt + "" + adfgx[num / 5] + "" + adfgx[num % 5];
            Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}", moduleId, word[aa], encrypt[aa * 2] + "" + encrypt[(aa * 2) + 1]);
        }
        int[] numrows = new int[kw.Length];
        
        for (int bb = 0; bb < kw.Length; bb++)
        {
            numrows[bb] = encrypt.Length / kw.Length;
            if (bb < (encrypt.Length % kw.Length))
                numrows[bb]++;
        }
        char[][] letters = new char[numrows[0]][];
        for(int cc = 0; cc < letters.Length; cc++)
        {
            letters[cc] = new char[kw.Length];
        }
        int cursor = 0;
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for(int dd = 0; dd < alpha.Length; dd++)
        {
            for(int ee = 0; ee < kw.Length; ee++)
            {
                if(kw[ee].ToString().IndexOf(alpha[dd]) >= 0)
                {
                    string templog = "";
                    for(int ff = 0; ff < numrows[ee]; ff++)
                    {
                        letters[ff][ee] = encrypt[cursor];
                        templog = templog + "" + encrypt[cursor];
                        cursor++;
                    }
                    Debug.LogFormat("[Orange Cipher #{0}] {1}: {2}", moduleId, kw[ee], templog);
                }
            }
            if (cursor >= encrypt.Length)
                break;
        }
        string logoutput = kw.ToUpper() + "\n";
        int row = 0;
        int col = 0;
        encrypt = "";
        for (int gg = 0; gg < cursor; gg++)
        {
            encrypt = encrypt + "" + letters[row][col];
            logoutput = logoutput + "" + letters[row][col];
            col++;
            if(col >= letters[row].Length)
            {
                col = 0;
                row++;
                logoutput = logoutput + "\n";
            }
        }
        Debug.LogFormat("[Orange Cipher #{0}] {1}", moduleId, logoutput);
        return encrypt;
    }*/
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
        if (page == 0)
        {
            screenTexts[0].fontSize = 40;
            screenTexts[1].fontSize = 40;
            screenTexts[2].fontSize = 35;
        }
        else
        {
            screenTexts[0].fontSize = 35;
            screenTexts[1].fontSize = 35;
            screenTexts[2].fontSize = 40;
        }
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
