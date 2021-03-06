﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;
using Words;
public class redCipher : MonoBehaviour
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
        //answer = "ADJUST";
        Debug.LogFormat("[Red Cipher #{0}] Generated Word: {1}", moduleId, answer);

        pages = new string[2][];
        pages[0] = new string[3];
        pages[1] = new string[3];
        pages[0][0] = "";
        pages[0][1] = "";
        pages[0][2] = "";

        string encrypt = redcipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string redcipher(string word)
    {
        //Generating 3 Random Words
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
        string[] words = new string[3];
        for (int bb = 0; bb < 3; bb++)
        {
            int length = UnityEngine.Random.Range(0, wordList.Count);
            words[bb] = wordList[length][UnityEngine.Random.Range(0, wordList[length].Count)].ToUpperInvariant();
            wordList[length].Remove(words[bb]);
        }
        pages[1][0] = words[0].ToUpperInvariant(); ;
        pages[1][1] = words[1].ToUpperInvariant(); ;
        pages[1][2] = words[2].ToUpperInvariant(); ;
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
        words[0] = getKey(words[0].Replace("J", "I"), "ABCDEFGHIKLMNOPQRSTUVWXYZ", (snnums[0] - '0') % 2 == 1);
        words[1] = getKey(words[1].Replace("J", "I"), "ABCDEFGHIKLMNOPQRSTUVWXYZ", (snnums[1] - '0') % 2 == 0);
        words[2] = getKey(words[2].Replace("J", "I"), "ABCDEFGHIKLMNOPQRSTUVWXYZ", Bomb.GetSerialNumberNumbers().Last() % 2 == 1);

        Debug.LogFormat("[Red Cipher #{0}] Trisquare Key 1: {1}", moduleId, words[0]);
        Debug.LogFormat("[Red Cipher #{0}] Trisquare Key 2: {1}", moduleId, words[1]);
        Debug.LogFormat("[Red Cipher #{0}] Trisquare Key 3: {1}", moduleId, words[2]);
        Debug.LogFormat("[Red Cipher #{0}] Begin Trisquare Encryption", moduleId);
        encrypt = TrisquareEnc(encrypt, words[0], words[1], words[2]);

        Debug.LogFormat("[Red Cipher #{0}] CM Bifid Key 1: {1}", moduleId, words[0]);
        Debug.LogFormat("[Red Cipher #{0}] CM Bifid Key 2: {1}", moduleId, words[1]);
        Debug.LogFormat("[Red Cipher #{0}] Begin CM Bifid Encryption", moduleId);
        encrypt = CMBifidEnc(encrypt, words[0], words[1]);

        Debug.LogFormat("[Red Cipher #{0}] Playfair Key: {1}", moduleId, words[0]);
        Debug.LogFormat("[Red Cipher #{0}] Begin Playfair Encryption", moduleId);
        encrypt = PlayfairEnc(encrypt, words[0]);

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
                pages[0][1] = pages[0][1] + "" + alpha[UnityEngine.Random.Range(0, alpha.Length)];
            }
        }

        return encrypt;
    }
    string CMBifidEnc(string word, string kw1, string kw2)
    {
        string encrypt = "";
        string rows = "";
        string cols = "";
        string letters = word.ToUpper();
        for (int hh = 0; hh < 6; hh++)
        {
            int n1 = kw1.IndexOf(word[hh]);
            rows = rows + "" + (n1 / 5);
            cols = cols + "" + (n1 % 5);
        }
        string nums = rows + "" + cols;
        Debug.LogFormat("[Red Cipher #{0}] Rows|Columns: {1}", moduleId, rows + "|" + cols);

        for (int ii = 0; ii < 6; ii++)
        {
            encrypt = encrypt + "" + kw2[((nums[ii * 2] - '0') * 5) + (nums[(ii * 2) + 1] - '0')];
            Debug.LogFormat("[Red Cipher #{0}] {1} -> {2}", moduleId, letters[ii], encrypt[ii]);
        }
        return encrypt;
    }
    string TrisquareEnc(string word, string kw1, string kw2, string kw3)
    {
        string encrypt = "";
        for (int gg = 0; gg < 6; gg++)
        {
            int n1 = kw1.IndexOf(word[gg]);
            int n2 = kw2.IndexOf(word[gg + 1]);
            gg++;
            encrypt = encrypt + "" + kw3[((n1 / 5) * 5) + (n2 % 5)] + "" + kw3[(n1 % 5) + ((n2 / 5) * 5)];
            Debug.LogFormat("[Red Cipher #{0}] {1} -> {2}", moduleId, word[gg - 1] + "" + word[gg], encrypt[gg - 1] + "" + encrypt[gg]);
        }
        return encrypt;
    }
    string PlayfairEnc(string word, string key)
    {
        string encrypt = "";
        int col = 0;
        int row = 0;
        char[][] matrix = new char[5][];
        matrix[0] = new char[5];
        matrix[1] = new char[5];
        matrix[2] = new char[5];
        matrix[3] = new char[5];
        matrix[4] = new char[5];
        for (int dd = 0; dd < key.Length; dd++)
        {
            matrix[row][col] = key[dd];
            col++;
            if (col == 5)
            {
                col = 0;
                row++;
            }
        }
        for (int ee = 0; ee < word.Length; ee++)
        {
            int col1 = 0;
            int row1 = 0;
            int col2 = 0;
            int row2 = 0;
            char char1 = word[ee];
            ee++;
            char char2 = word[ee];
            for (int ff = 0; ff < 5; ff++)
            {
                for (int gg = 0; gg < 5; gg++)
                {
                    if (char1 == matrix[ff][gg])
                    {
                        col1 = gg;
                        row1 = ff;
                    }
                    if (char2 == matrix[ff][gg])
                    {
                        col2 = gg;
                        row2 = ff;
                    }
                }
            }
            if (row1 == row2 && col1 == col2)
            {
                col1 = col2;
                row1 = row2;
            }
            else if (row1 == row2)
            {
                col1 = correction(col1 + 1, 5);
                col2 = correction(col2 + 1, 5);
            }
            else if (col1 == col2)
            {
                row1 = correction(row1 + 1, 5);
                row2 = correction(row2 + 1, 5);
            }
            else
            {
                int col3 = col1;
                col1 = col2;
                col2 = col3;
            }
            encrypt = encrypt + "" + matrix[row1][col1] + "" + matrix[row2][col2];
            Debug.LogFormat("[Red Cipher #{0}] {1} -> {2}", moduleId, word[ee - 1] + "" + word[ee], encrypt[ee - 1] + "" + encrypt[ee]);
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
            screenTexts[1].fontSize = 40;
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
