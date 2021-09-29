using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;
using Words;
public class maroonCipher : MonoBehaviour
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
        answer = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
        wordList[2].Remove(answer.ToUpperInvariant());
        //answer = "AAAAAA";
        Debug.LogFormat("[Maroon Cipher #{0}] Generated Word: {1}", moduleId, answer);

        pages = new string[2][];
        pages[0] = new string[3];
        pages[1] = new string[3];
        pages[0][0] = "";
        pages[0][1] = "";
        pages[0][2] = "";
        string encrypt = marooncipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string marooncipher(string word)
    {
        int length = UnityEngine.Random.Range(0, 5);
        string keyword = wordList[length][UnityEngine.Random.Range(0, wordList[length].Count)];
        string key = getKey(keyword.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetPortCount() % 4 < 2);
        pages[0][1] = keyword.ToUpperInvariant();
        Debug.LogFormat("[Maroon Cipher #{0}] Generated Keyword: {1}", moduleId, pages[0][1]);
        Debug.LogFormat("[Maroon Cipher #{0}] Generated Key: {1}", moduleId, key);
        Debug.LogFormat("[Maroon Cipher #{0}] Begin Huffman Encryption", moduleId, key);
        string encrypt = HuffmanEnc(word.ToUpperInvariant(), key.ToUpperInvariant());
        Debug.LogFormat("[Maroon Cipher #{0}] Begin Redfence Transposition", moduleId, key);
        encrypt = RedfenceTrans(encrypt.ToUpperInvariant());
        Debug.LogFormat("[Maroon Cipher #{0}] Begin Monoalphabetic Encryption", moduleId, key);
        encrypt = MonoalphabeticEnc(encrypt.ToUpperInvariant(), key.ToUpperInvariant());
        return encrypt;
    }
    string HuffmanEnc(string word, string key)
    {
        ArrayList paths = HuffmanGen();
        for(int aa = 0; aa < paths.Count; aa++)
            Debug.LogFormat("[Maroon Cipher #{0}] {1}: {2}", moduleId, paths[aa], key[aa]);
        //Creating Path Needed to Make Diagram
        int depth = 0;
        string binary = "";
        for (int aa = 0; aa < paths.Count; aa++)
        {
            string path = (string)paths[aa];
            for (int bb = depth + 0; bb < path.Length; bb++)
                binary = binary + "1";
            binary = binary + "0";
            depth = path.LastIndexOf("L") + 1;
        }
        //Encrypting the word
        for (int aa = 0; aa < word.Length; aa++)
            binary = binary + "" + (string)paths[key.IndexOf(word[aa])];
        binary = binary.Replace("L", "0").Replace("R", "1");
        Debug.LogFormat("[Maroon Cipher #{0}] Generated Binary: {1}", moduleId, binary);
        string result = "";
        while (binary.Length > 4)
        {
            string temp = binaryToLet(binary.Substring(0, 4));
            if (temp.Length != 1)
            {
                temp = binaryToLet(binary.Substring(0, 5));
                binary = binary.Substring(5);
            }
            else
                binary = binary.Substring(4);
            result = result + "" + temp;
        }
        if(binary.Length > 0)
        {
            if(binary.Length == 4)
            {
                string temp = binaryToLet(binary);
                if (temp.Length != 1)
                    temp = binaryToLet(binary.Substring(0, 2)) + "" + binaryToLet(binary.Substring(2));
                result = result + "" + temp;
            }
            else if(binary.Length == 3)
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                    result = result + "" + binaryToLet(binary.Substring(0, 2)) + "" + binaryToLet(binary.Substring(2));
                else
                    result = result + "" + binaryToLet(binary.Substring(0, 1)) + "" + binaryToLet(binary.Substring(1));
            }
            else
                result = result + "" + binaryToLet(binary);
        }
        string encrypt = result.Substring(0, 6);
        result = result.Substring(6);
        Debug.LogFormat("[Maroon Cipher #{0}] Encrypted Word: {1}", moduleId, encrypt);
        Debug.LogFormat("[Maroon Cipher #{0}] Resulting Characters: {1}", moduleId, result);
        int divide = result.Length / 3;
        pages[1][0] = result.Substring(0, divide);
        pages[1][1] = result.Substring(divide, divide);
        pages[1][2] = result.Substring(divide * 2);
        return encrypt;
    }
    string binaryToLet(string bin)
    {
        switch (bin)
        {
            case "00000":
                return "A";
            case "00001":
                return "B";
            case "00010":
                return "C";
            case "00011":
                return "D";
            case "00100":
                return "E";
            case "00101":
                return "F";
            case "00110":
                return "G";
            case "00111":
                return "H";
            case "01000":
                return "I";
            case "01001":
                return "J";
            case "01010":
                return "K";
            case "01011":
                return "L";
            case "01100":
                return "M";
            case "01101":
                return "N";
            case "01110":
                return "O";
            case "01111":
                return "P";
            case "10000":
                return "Q";
            case "10001":
                return "R";
            case "10010":
                return "S";
            case "10011":
                return "T";
            case "1010":
                return "U";
            case "1011":
                return "V";
            case "1100":
                return "W";
            case "1101":
                return "X";
            case "1110":
                return "Y";
            case "1111":
                return "Z";
            case "00":
                return "2";
            case "01":
                return "3";
            case "10":
                return "4";
            case "11":
                return "5";
            case "0":
                return "6";
            case "1":
                return "7";
        }
        return "";
    }
    ArrayList HuffmanGen()
    {
        ArrayList numbers = new ArrayList();
        numbers.Add(UnityEngine.Random.Range(0, 7) + 10);
        //numbers.Add(10);
        numbers.Add(26 - (int)numbers[0]);
        ArrayList paths = new ArrayList() { "L", "R" };
        bool allOne = false;
        while(!(allOne))
        {
            ArrayList pathList = new ArrayList();
            ArrayList numberList = new ArrayList();
            for(int aa = 0; aa < numbers.Count; aa++)
            {
                if((int)numbers[aa] > 1)
                {
                    numberList.Add(UnityEngine.Random.Range(0, (int)numbers[aa] - 1) + 1);
                    //numberList.Add((int)numbers[aa] - 1);
                    numberList.Add((int)numbers[aa] - (int)numberList[numberList.Count - 1]);
                    pathList.Add(paths[aa] + "L");
                    pathList.Add(paths[aa] + "R");
                }
                else
                {
                    pathList.Add(paths[aa]);
                    numberList.Add(numbers[aa]);
                }
            }
            numbers = numberList;
            paths = pathList;
            allOne = true;
            for(int aa = 0; aa < numbers.Count; aa++)
            {
                if((int)numbers[aa] != 1)
                {
                    allOne = false;
                    break;
                }
            }
        }
        return paths;
    }
    string RedfenceTrans(string word)
    {
        string[] rows = new string[UnityEngine.Random.Range(0, 4) + 2];
        string poss = "";
        for(int aa = 0; aa < rows.Length; aa++)
        {
            rows[aa] = "";
            poss = poss + "" + (aa + 1);
        }
        int offset = 1;
        int row = 0;
        for(int aa = 0; aa < 6; aa++)
        {
            rows[row] = rows[row] + "" + word[aa];
            if ((row == 0 && offset == -1) || row == rows.Length - 1)
                offset = offset * -1;
            row += offset;
        }
        string key = "", encrypt = "";
        for(int aa = 0; aa < rows.Length; aa++)
        {
            key = key + "" + poss[UnityEngine.Random.Range(0, poss.Length)];
            poss = poss.Replace(key[aa] + "", "");
            encrypt = encrypt + "" + rows["12345".IndexOf(key[aa])].ToUpperInvariant();
            Debug.LogFormat("[Maroon Cipher #{0}] Row #{1}: {2}", moduleId, (aa + 1), rows[aa]);
        }
        Debug.LogFormat("[Maroon Cipher #{0}] Redfence Key: {1}", moduleId, key);
        Debug.LogFormat("[Maroon Cipher #{0}] {1} -> {2}", moduleId, word, encrypt);
        pages[0][2] = key.ToUpperInvariant();
        return encrypt;
    }
    string MonoalphabeticEnc(string word, string key)
    {
        string encrypt = "", alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < 6; aa++)
        {
            encrypt = encrypt + "" + key[alpha.IndexOf(word[aa])];
            Debug.LogFormat("[Maroon Cipher #{0}] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
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
        if (page == 0)
        {
            screenTexts[0].fontSize = 40;
            screenTexts[1].fontSize = 35;
            screenTexts[2].fontSize = 40;
        }
        else
        {
            screenTexts[0].fontSize = 35;
            screenTexts[1].fontSize = 35;
            screenTexts[2].fontSize = 35;
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
