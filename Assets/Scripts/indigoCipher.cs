using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;
using Words;
public class indigoCipher : MonoBehaviour
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
        Debug.LogFormat("[Indigo Cipher #{0}] Generated Word: {1}", moduleId, answer);

        pages = new string[2][];
        pages[0] = new string[3];
        pages[1] = new string[3];
        pages[0][0] = "";
        pages[0][1] = "";
        pages[0][2] = "";
        string encrypt = indigocipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string indigocipher(string word)
    {
        Debug.LogFormat("[Indigo Cipher #{0}] Begin Logic Encryption", moduleId);
        string encrypt = LogicEnc(word.ToUpper());
        string kw2 = encrypt.Split(' ')[1];
        Debug.LogFormat("[Indigo Cipher #{0}] Begin Condi Encryption", moduleId);
        encrypt = CondiEnc(encrypt.Split(' ')[0]);
        string kw1 = encrypt.Split(' ')[1];
        encrypt = encrypt.Split(' ')[0];
        Debug.LogFormat("[Indigo Cipher #{0}] Begin Fractionated Morse Encryption", moduleId);
        string kw2enc = FractionatedMorseEnc(kw2.ToUpper(), kw1.ToUpper());
        pages[0][2] = kw1.ToUpper();
        pages[0][1] = kw2enc.ToUpper();
        return encrypt;
    }
    string LogicEnc(string word)
    {
        int binary1s = UnityEngine.Random.Range(0, 3) + 2;
        string positions = "012345";
        string binary1pos = "";
        string binary0pos = "";
        for (int bb = 0; bb < binary1s; bb++)
        {
            int randnum = UnityEngine.Random.Range(0, positions.Length);
            binary1pos = binary1pos + "" + positions[randnum];
            positions = positions.Substring(0, randnum) + "" + positions.Substring(randnum + 1);
        }
        string b1 = "";
        positions = "012345";
        for (int cc = 0; cc < 6; cc++)
        {
            if (binary1pos.IndexOf(positions[cc]) >= 0)
                b1 = b1 + "1";
            else
            {
                b1 = b1 + "0";
                binary0pos = binary0pos + "" + cc;
            }

        }

        binary1s = UnityEngine.Random.Range(0, 3);

        char[] b2 = new char[6];
        char randpos = binary1pos[UnityEngine.Random.Range(0, binary1pos.Length)];
        b2[randpos - '0'] = '1';
        binary1pos = binary1pos.Substring(0, binary1pos.IndexOf(randpos)) + "" + binary1pos.Substring(binary1pos.IndexOf(randpos) + 1);
        positions = positions.Substring(0, positions.IndexOf(randpos)) + "" + positions.Substring(positions.IndexOf(randpos) + 1);

        randpos = binary1pos[UnityEngine.Random.Range(0, binary1pos.Length)];
        b2[randpos - '0'] = '0';
        binary1pos = binary1pos.Substring(0, binary1pos.IndexOf(randpos)) + "" + binary1pos.Substring(binary1pos.IndexOf(randpos) + 1);
        positions = positions.Substring(0, positions.IndexOf(randpos)) + "" + positions.Substring(positions.IndexOf(randpos) + 1);

        randpos = binary0pos[UnityEngine.Random.Range(0, binary0pos.Length)];
        b2[randpos - '0'] = '1';
        binary0pos = binary0pos.Substring(0, binary0pos.IndexOf(randpos)) + "" + binary0pos.Substring(binary0pos.IndexOf(randpos) + 1);
        positions = positions.Substring(0, positions.IndexOf(randpos)) + "" + positions.Substring(positions.IndexOf(randpos) + 1);

        randpos = binary0pos[UnityEngine.Random.Range(0, binary0pos.Length)];
        b2[randpos - '0'] = '0';
        binary0pos = binary0pos.Substring(0, binary0pos.IndexOf(randpos)) + "" + binary0pos.Substring(binary0pos.IndexOf(randpos) + 1);
        positions = positions.Substring(0, positions.IndexOf(randpos)) + "" + positions.Substring(positions.IndexOf(randpos) + 1);

        while (binary1s > 0)
        {
            binary1s--;
            randpos = positions[UnityEngine.Random.Range(0, positions.Length)];
            b2[randpos - '0'] = '1';
            positions = positions.Substring(0, positions.IndexOf(randpos)) + "" + positions.Substring(positions.IndexOf(randpos) + 1);
        }
        for (int ee = 0; ee < positions.Length; ee++)
        {
            b2[positions[ee] - '0'] = '0';
        }
        string b3 = "";
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        List<string> binalpha = new List<string>()
        {
            "00000",
            "00001",
            "00010",
            "00011",
            "00100",
            "00101",
            "00110",
            "00111",
            "01000",
            "01001",
            "01010",
            "01011",
            "01100",
            "01101",
            "01110",
            "01111",
            "10000",
            "10001",
            "10010",
            "10011",
            "10100",
            "10101",
            "10110",
            "10111",
            "11000",
            "11001"
        };
        int logicgate = UnityEngine.Random.Range(0, 8);
        string[] gates = { "AND", "OR", "XOR", "NAND", "NOR", "XNOR", "->", "<-" };
        Debug.LogFormat("[Indigo Cipher #{0}] Gate: {1}", moduleId, gates[logicgate]);
        string kw = "";
        string encrypt = "";
        string overenc = "";
        string overkw = "";
        for (int dd = 0; dd < 6; dd++)
        {
            string binlet = binalpha[alpha.IndexOf(word[dd])];
            string binenc = "";
            string binkw = "";
            for (int ee = 0; ee < 5; ee++)
            {
                switch (logicgate)
                {
                    case 0: //AND
                        if (binlet[ee] == '1')
                        {
                            binenc = binenc + "1";
                            binkw = binkw + "1";
                        }
                        else
                        {
                            string[] op1 = { "01", "10", "00" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1]; ;
                        }
                        break;
                    case 1: //OR
                        if (binlet[ee] == '1')
                        {
                            string[] op1 = { "01", "10", "11" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        else
                        {
                            binenc = binenc + "0";
                            binkw = binkw + "0";
                        }
                        break;
                    case 2: //XOR
                        if (binlet[ee] == '1')
                        {
                            string[] op1 = { "01", "10" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        else
                        {
                            string[] op1 = { "00", "11" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        break;
                    case 3: //NAND
                        if (binlet[ee] == '1')
                        {
                            string[] op1 = { "01", "10", "00" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        else
                        {
                            binenc = binenc + "1";
                            binkw = binkw + "1";
                        }
                        break;
                    case 4: //NOR
                        if (binlet[ee] == '1')
                        {
                            binenc = binenc + "0";
                            binkw = binkw + "0";
                        }
                        else
                        {
                            string[] op1 = { "01", "10", "11" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        break;
                    case 5: //XNOR
                        if (binlet[ee] == '1')
                        {
                            string[] op1 = { "11", "00" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        else
                        {
                            string[] op1 = { "10", "01" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        break;
                    case 6: //->
                        if (binlet[ee] == '1')
                        {
                            string[] op1 = { "11", "00", "01" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        else
                        {
                            binenc = binenc + "1";
                            binkw = binkw + "0";
                        }
                        break;
                    default: //<-
                        if (binlet[ee] == '1')
                        {
                            string[] op1 = { "11", "00", "10" };
                            string rand = op1[UnityEngine.Random.Range(0, op1.Length)];
                            binenc = binenc + "" + rand[0];
                            binkw = binkw + "" + rand[1];
                        }
                        else
                        {
                            binenc = binenc + "0";
                            binkw = binkw + "1";
                        }
                        break;
                }
            }
            Debug.LogFormat("[Indigo Cipher #{0}] {1} + {2} = {3}", moduleId, binenc, binkw, binlet);

            if (!(binalpha.Contains(binenc)))
            {
                overenc = overenc + "1";
                binenc = binenc.Replace("1", "*");
                binenc = binenc.Replace("0", "1");
                binenc = binenc.Replace("*", "0");
            }
            else
            {
                string tempenc = binenc.Replace("1", "*");
                tempenc = tempenc.Replace("0", "1");
                tempenc = tempenc.Replace("*", "0");
                if (binalpha.Contains(tempenc) && (UnityEngine.Random.Range(0, 3) == 0))
                {
                    overenc = overenc + "1";
                    binenc = tempenc.ToUpper();
                }
                else
                    overenc = overenc + "0";
            }

            if (!(binalpha.Contains(binkw)))
            {
                overkw = overkw + "1";
                binkw = binkw.Replace("1", "*");
                binkw = binkw.Replace("0", "1");
                binkw = binkw.Replace("*", "0");
            }
            else
            {
                string tempkw = binkw.Replace("1", "*");
                tempkw = tempkw.Replace("0", "1");
                tempkw = tempkw.Replace("*", "0");
                if (binalpha.Contains(tempkw) && (UnityEngine.Random.Range(0, 3) == 0))
                {
                    overkw = overkw + "1";
                    binkw = tempkw.ToUpper();
                }
                else
                    overkw = overkw + "0";
            }
            encrypt = encrypt + "" + alpha[binalpha.IndexOf(binenc)];
            kw = kw + "" + alpha[binalpha.IndexOf(binkw)];
            switch (logicgate)
            {
                case 0://AND
                    if (b1[dd] == '1' && b2[dd] == '1')
                        b3 = b3 + "1";
                    else
                        b3 = b3 + "0";
                    break;
                case 1://OR
                    if (b1[dd] == '1' || b2[dd] == '1')
                        b3 = b3 + "1";
                    else
                        b3 = b3 + "0";
                    break;
                case 2://XOR
                    if (b1[dd] != b2[dd])
                        b3 = b3 + "1";
                    else
                        b3 = b3 + "0";
                    break;
                case 3://NAND
                    if (b1[dd] == '1' && b2[dd] == '1')
                        b3 = b3 + "0";
                    else
                        b3 = b3 + "1";
                    break;
                case 4://NOR
                    if (b1[dd] == '1' || b2[dd] == '1')
                        b3 = b3 + "0";
                    else
                        b3 = b3 + "1";
                    break;
                case 5://XNOR
                    if (b1[dd] != b2[dd])
                        b3 = b3 + "0";
                    else
                        b3 = b3 + "1";
                    break;
                case 6://->
                    if (b1[dd] == '1' && b2[dd] == '0')
                        b3 = b3 + "0";
                    else
                        b3 = b3 + "1";
                    break;
                default://<-
                    if (b1[dd] == '0' && b2[dd] == '1')
                        b3 = b3 + "0";
                    else
                        b3 = b3 + "1";
                    break;
            }
        }
        Debug.LogFormat("[Indigo Cipher #{0}] Logic Encryption Keyword: {1}", moduleId, kw);
        Debug.LogFormat("[Indigo Cipher #{0}] Logic Encrypted Word: {1}", moduleId, encrypt);
        Debug.LogFormat("[Indigo Cipher #{0}] Binary 1: {1}", moduleId, b1);
        Debug.LogFormat("[Indigo Cipher #{0}] Binary 2: {1}", moduleId, b2[0] + "" + b2[1] + "" + b2[2] + "" + b2[3] + "" + b2[4] + "" + b2[5]);
        Debug.LogFormat("[Indigo Cipher #{0}] Binary 3: {1}", moduleId, b3);
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        for (int ff = 0; ff < 6; ff++)
        {
            int multi = 1;
            for (int gg = 0; gg < 5 - ff; gg++)
                multi *= 2;
            if (b1[ff] == '1')
                num1 += multi;
            if (b2[ff] == '1')
                num2 += multi;
            if (b3[ff] == '1')
                num3 += multi;
        }

        pages[1][0] = overenc;
        pages[1][1] = overkw;
        pages[1][2] = num1 + " ? " + num2 + " = " + num3;
        return encrypt + " " + kw;
    }
    string CondiEnc(string word)
    {
        int length = UnityEngine.Random.Range(0, wordList.Count);
        string kw = wordList[length][UnityEngine.Random.Range(0, wordList[length].Count)].ToUpper();
        wordList[length].Remove(kw.ToUpper());
        string key = getKey(kw.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetPortCount() % 2 == 1);
        Debug.LogFormat("[Indigo Cipher #{0}] Key: {1}", moduleId, key);
        int offset = Bomb.GetSerialNumberNumbers().Sum();
        string encrypt = "";
        for (int aa = 0; aa < 6; aa++)
        {
            Debug.LogFormat("[Indigo Cipher #{0}] Offset: {1}", moduleId, offset);
            encrypt = encrypt + "" + key[(key.IndexOf(word[aa]) + offset) % 26];
            Debug.LogFormat("[Indigo Cipher #{0}] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
            offset = key.IndexOf(word[aa]) + 1;
        }
        return encrypt + " " + kw;
    }
    string FractionatedMorseEnc(string word, string kw)
    {
        string key = getKey(kw.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetPortCount() % 2 == 1);
        Debug.LogFormat("[Indigo Cipher #{0}] Key: {1}", moduleId, key);
        string[] morse =
        {
            ".-",
            "-...",
            "-.-.",
            "-..",
            ".",
            "..-.",
            "--.",
            "....",
            "..",
            ".---",
            "-.-",
            ".-..",
            "--",
            "-.",
            "---",
            ".--.",
            "--.-",
            ".-.",
            "...",
            "-",
            "..-",
            "...-",
            ".--",
            "-..-",
            "-.--",
            "--.."
        };
        string[] morsekey =
        {
            "...",
            "..-",
            "..x",
            ".-.",
            ".--",
            ".-x",
            ".x.",
            ".x-",
            ".xx",
            "-..",
            "-.-",
            "-.x",
            "--.",
            "---",
            "--x",
            "-x.",
            "-x-",
            "-xx",
            "x..",
            "x.-",
            "x.x",
            "x-.",
            "x--",
            "x-x",
            "xx.",
            "xx-"
        };
        int counter = 0;
        string[] rows = { "", "", "" };
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < word.Length; aa++)
        {
            int numalpha = alpha.IndexOf(word[aa]);
            string morword = morse[numalpha];
            Debug.LogFormat("[Indigo Cipher #{0}] {1} -> {2}", moduleId, word[aa], morword);
            for (int bb = 0; bb < morword.Length; bb++)
            {
                rows[counter] = rows[counter] + "" + morword[bb];
                counter = (counter + 1) % 3;
            }
            if (aa != 5)
            {
                rows[counter] = rows[counter] + "x";
                counter = (counter + 1) % 3;
            }

        }
        if (rows[1].Length != rows[0].Length)
            rows[1] = rows[1] + "x";
        if (rows[2].Length != rows[0].Length)
            rows[2] = rows[2] + "x";
        Debug.LogFormat("[Indigo Cipher #{0}] Morse rows:", moduleId);
        Debug.LogFormat("[Indigo Cipher #{0}] {1}", moduleId, rows[0]);
        Debug.LogFormat("[Indigo Cipher #{0}] {1}", moduleId, rows[1]);
        Debug.LogFormat("[Indigo Cipher #{0}] {1}", moduleId, rows[2]);
        string encrypt = "";
        for (int bb = 0; bb < rows[0].Length; bb++)
        {
            string morenc = rows[0][bb] + "" + rows[1][bb] + "" + rows[2][bb];
            for (int cc = 0; cc < morsekey.Length; cc++)
            {
                if (morenc.Equals(morsekey[cc]))
                {
                    Debug.LogFormat("[Indigo Cipher #{0}] {1} -> {2}", moduleId, morenc, key[cc]);
                    encrypt = encrypt + "" + key[cc];
                    break;
                }
            }
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
        screenTexts[2].fontSize = 35;
        if (page == 1)
            screenTexts[1].fontSize = 40;
        else
            screenTexts[1].fontSize = 30;


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
