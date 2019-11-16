using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;

public class ultimateCipher : MonoBehaviour {
    
    public TextMesh[] screenTexts;
    public string[] wordList;
    public KMBombInfo Bomb;
    public KMBombModule module;
    public AudioClip[] sounds;
    public KMAudio Audio;
    public TextMesh submitText;
   
    
    private string[] matrixWordList =
      {
                "ACID",
                "BUST",
                "CODE",
                "DAZE",
                "ECHO",
                "FILM",
                "GOLF",
                "HUNT",
                "ITCH",
                "JURY",
                "KING",
                "LIME",
                "MONK",
                "NUMB",
                "ONLY",
                "PREY",
                "QUIT",
                "RAVE",
                "SIZE",
                "TOWN",
                "URGE",
                "VERY",
                "WAXY",
                "XYLO",
                "YARD",
                "ZERO",
                "ABORT",
                "BLEND",
                "CRYPT",
                "DWARF",
                "EQUIP",
                "FANCY",
                "GIZMO",
                "HELIX",
                "IMPLY",
                "JOWLS",
                "KNIFE",
                "LEMON",
                "MAJOR",
                "NIGHT",
                "OVERT",
                "POWER",
                "QUILT",
                "RUSTY",
                "STOMP",
                "TRASH",
                "UNTIL",
                "VIRUS",
                "WHISK",
                "XERIC",
                "YACHT",
                "ZEBRA",
                "ADVICE",
                "BUTLER",
                "CAVITY",
                "DIGEST",
                "ELBOWS",
                "FIXURE",
                "GOBLET",
                "HANDLE",
                "INDUCT",
                "JOKING",
                "KNEADS",
                "LENGTH",
                "MOVIES",
                "NIMBLE",
                "OBTAIN",
                "PERSON",
                "QUIVER",
                "RACHET",
                "SAILOR",
                "TRANCE",
                "UPHELD",
                "VANISH",
                "WALNUT",
                "XYLOSE",
                "YANKED",
                "ZODIAC",
                "ALREADY",
                "BROWSED",
                "CAPITOL",
                "DESTROY",
                "ERASING",
                "FLASHED",
                "GRIMACE",
                "HIDEOUT",
                "INFUSED",
                "JOYRIDE",
                "KETCHUP",
                "LOCKING",
                "MAILBOX",
                "NUMBERS",
                "OBSCURE",
                "PHANTOM",
                "QUIETLY",
                "REFUSAL",
                "SUBJECT",
                "TRAGEDY",
                "UNKEMPT",
                "VENISON",
                "WARSHIP",
                "XANTHIC",
                "YOUNGER",
                "ZEPHYRS",
                "ADVOCATE",
                "BACKFLIP",
                "CHIMNEYS",
                "DISTANCE",
                "EXPLOITS",
                "FOCALIZE",
                "GIFTWRAP",
                "HOVERING",
                "INVENTOR",
                "JEALOUSY",
                "KINSFOLK",
                "LOCKABLE",
                "MERCIFUL",
                "NOTECARD",
                "OVERCAST",
                "PERILOUS",
                "QUESTION",
                "RAINCOAT",
                "STEALING",
                "TREASURY",
                "UPDATING",
                "VERTICAL",
                "WISHBONE",
                "XENOLITH",
                "YEARLONG",
                "ZEALOTRY"
        };

    private string[][] pages;
    private Color[] chosentextcolors;
    private Material[] chosenscreencolors;
    private int[][] fontsizes;
    private Material[] chosenbackgroundcolors;
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
    public MeshRenderer background;
    public Material[] backgroundcolors;
    public Material[] screencolors;
    public MeshRenderer[] screens;
    public TextMesh[] texts;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        leftArrow.OnInteract += delegate () { left(leftArrow); return false; };
        rightArrow.OnInteract += delegate () { right(rightArrow); return false; };
        submit.OnInteract += delegate () { submitWord(submit); return false; };
        foreach(KMSelectable keybutton in keyboard)
        {
            KMSelectable pressedButton = keybutton;
            pressedButton.OnInteract += delegate () { letterPress(pressedButton); return false; };
        }
    }
        // Use this for initialization
        void Start ()
    
    {
        submitText.text = "1";
        //Generating random word
        answer = wordList[UnityEngine.Random.Range(0, wordList.Length)].ToUpper();
        Debug.LogFormat("[Ultimate Cipher #{0}] Generated Word: {1}", moduleId, answer);
        chosenscreencolors = new Material[7];
        chosentextcolors = new Color[7];
        fontsizes = new int[7][];
        chosenbackgroundcolors = new Material[7];
        pages = new string[7][];
        for(int aa = 0; aa < pages.Length; aa++)
        {
            pages[aa] = new string[3];
            pages[aa][0] = "";
            pages[aa][1] = "";
            pages[aa][2] = "";
            fontsizes[aa] = new int[3];
        }
        string encrypt = ultimatecipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string ultimatecipher(string word)
    {
        fontsizes[0][0] = 40;
        fontsizes[0][1] = 40;
        fontsizes[0][2] = 40;
        int[] colornums = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        string possnums = "0123456789";
        chosenbackgroundcolors[0] = backgroundcolors[0];
        chosentextcolors[0] = Color.white;
        chosenscreencolors[0] = screencolors[0];
        page = 6;
        string encrypt = word.ToUpper();
        for (int aa = 2; aa >= 0; aa--)
        {
            int rand = UnityEngine.Random.Range(0, possnums.Length);
            int num = colornums[possnums[rand] - '0'];
            possnums = possnums.Replace(possnums[rand] + "", "");
            chosenbackgroundcolors[(aa * 2) + 1] = backgroundcolors[num];
            chosenbackgroundcolors[(aa * 2) + 2] = chosenbackgroundcolors[(aa * 2) + 1];
            switch(num)
            {
                case 1:
                    encrypt = redcipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 2:
                    encrypt = orangecipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 3:
                    encrypt = yellowcipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 4:
                    encrypt = greencipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 5:
                    encrypt = bluecipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 6:
                    encrypt = indigocipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 7:
                    encrypt = violetcipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 8:
                    encrypt = whitecipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 9:
                    encrypt = graycipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
                case 10:
                    encrypt = blackcipher(encrypt.ToUpper(), UnityEngine.Random.Range(0, 2) == 0);
                    break;
            }
            page -= 2;
        }
        return encrypt;
    }
    string blackcipher(string word, bool invert)
    {
        fontsizes[page][0] = 35;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 25;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 40;
        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Black Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Begin Digrafid Encryption", moduleId);
            string encrypt = DigrafidEnc(word.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Begin Railfence Transposition", moduleId);
            encrypt = RailfenceTrans(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Begin Enigma Encryption", moduleId);
            encrypt = EnigmaEnc(encrypt.ToUpper(), invert);
            return encrypt;
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Black Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Begin Enigma Encryption", moduleId);
            string encrypt = EnigmaEnc(word.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Begin Railfence Transposition", moduleId);
            encrypt = RailfenceTrans(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Begin Digrafid Encryption", moduleId);
            encrypt = DigrafidEnc(encrypt.ToUpper(), invert);
            return encrypt;
        }
    }
    string EnigmaEnc(string word, bool invert)
    {
        string[][] rotors =
        {
           new string[]
             {
                "EKMFLGDQVZNTOWYHXUSPAIBRCJ",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "DQ"
            },
           new string[]
           {
                "AJDKSIRUXBLHWTMCQGZNPYFVOE",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "ER"
            },
           new string[]
           {
                "BDFHJLCPRTXVZNYEIWGAKMUSQO",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "IV"
            },
           new string[]
           {
                "ESOVPZJAYQUIRHXLNFTGKDCMWB",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "JW"
            },
           new string[]
           {
                "VZBRGITYUPSDNHLXAWMJQOFECK",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "ZM"
            },
           new string[]
           {
                "JPGVOUMFYQBENHZRDKASXLICTW",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "LY"
            },
           new string[]
           {
                "NZJHGRCXMYSWBOUFAIVLPEKQDT",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "HU"
            },
           new string[]
           {
                "FKQHTLXOCBJSPDZRAMEWNIUYGV",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "CP"
            }
        };
        string[][] reflectors =
        {
            new string[]
             {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "LUSNPQOMJIYAHDGEFXCVBTZRKW"
             },
            new string[]
             {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "XQUMFEPOWLTJDZHGBVYKCRIASN"
             },
            new string[]
            {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "ESKOAQMJYHCPGTDLFUBNRXZVIW"
            }
        };
        string[] roman = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII" };
        string rotornums = "01234567";
        string reflectorlets = "ABC";
        string[][] rotorsetup = new string[5][];
        //Generate Reflector
        int num = UnityEngine.Random.Range(0, 3);
        rotorsetup[0] = reflectors[num];
        string rotorconfig = reflectorlets[num] + "";
        string rotorlets = "";
        //Generate Rotors
        for (int aa = 0; aa < 3; aa++)
        {
            char pickedrotor = rotornums[UnityEngine.Random.Range(0, rotornums.Length)];
            rotornums = rotornums.Replace(pickedrotor + "", "");
            rotorsetup[aa + 1] = rotors[pickedrotor - '0'];
            rotorconfig = rotorconfig + "-" + roman[pickedrotor - '0'];
            num = UnityEngine.Random.Range(0, 26);
            rotorsetup[aa + 1][0] = rotorsetup[aa + 1][0].Substring(num) + "" + rotorsetup[aa + 1][0].Substring(0, num);
            rotorsetup[aa + 1][1] = rotorsetup[aa + 1][1].Substring(num) + "" + rotorsetup[aa + 1][1].Substring(0, num);
            rotorlets = rotorlets + "" + rotorsetup[aa + 1][1][0];
        }
        rotorsetup[4] = new string[]
            {"ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"};
        //Generating Plugboard
        string plugboard = "";
        int numletterswaps = UnityEngine.Random.Range(3, 6);
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < numletterswaps; aa++)
        {
            char l1 = alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
            alphabet = alphabet.Replace(l1 + "", "");
            char l2 = alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
            alphabet = alphabet.Replace(l2 + "", "");
            plugboard = plugboard + "" + l1 + "" + l2 + "-";
            for (int bb = 0; bb < 2; bb++)
            {
                rotorsetup[4][bb] = rotorsetup[4][bb].Replace(l1, '1');
                rotorsetup[4][bb] = rotorsetup[4][bb].Replace(l2, l1);
                rotorsetup[4][bb] = rotorsetup[4][bb].Replace('1', l2);
            }
        }
        plugboard = plugboard.Substring(0, plugboard.Length - 1);
        pages[page][0] = rotorconfig.ToUpper();
        pages[page][1] = rotorlets.ToUpper();
        pages[page][2] = plugboard.ToUpper();
        //Start Encrypting
        string encrypt = "";
        for (int cc = 0; cc < 6; cc++)
        {
            //Letter Encryption
            char let = word[cc];
            string logoutput = let + "";
            for (int dd = rotorsetup.Length - 1; dd > 0; dd--)
            {
                let = rotorsetup[dd - 1][0][rotorsetup[dd][1].IndexOf(let)];
                logoutput = logoutput + "->" + let;
            }
            let = rotorsetup[0][0][rotorsetup[0][1].IndexOf(let)];
            logoutput = logoutput + "->" + let;
            for (int ee = 0; ee < rotorsetup.Length - 1; ee++)
            {
                let = rotorsetup[ee + 1][1][rotorsetup[ee][0].IndexOf(let)];
                logoutput = logoutput + "->" + let;
            }
            encrypt = encrypt + "" + let;
            //Rotor Turning
            if (rotorsetup[2][1][0] == rotorsetup[2][2][0] || rotorsetup[2][1][0] == rotorsetup[2][2][1])
            {
                rotorsetup[2][0] = rotorsetup[2][0].Substring(1) + "" + rotorsetup[2][0][0];
                rotorsetup[2][1] = rotorsetup[2][1].Substring(1) + "" + rotorsetup[2][1][0];
                rotorsetup[1][0] = rotorsetup[1][0].Substring(1) + "" + rotorsetup[1][0][0];
                rotorsetup[1][1] = rotorsetup[1][1].Substring(1) + "" + rotorsetup[1][1][0];
            }
            else if (rotorsetup[3][1][0] == rotorsetup[3][2][0] || rotorsetup[3][1][0] == rotorsetup[3][2][1])
            {
                rotorsetup[2][0] = rotorsetup[2][0].Substring(1) + "" + rotorsetup[2][0][0];
                rotorsetup[2][1] = rotorsetup[2][1].Substring(1) + "" + rotorsetup[2][1][0];
            }
            rotorsetup[3][0] = rotorsetup[3][0].Substring(1) + "" + rotorsetup[3][0][0];
            rotorsetup[3][1] = rotorsetup[3][1].Substring(1) + "" + rotorsetup[3][1][0];
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] {1}", moduleId, logoutput);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] {1}", moduleId, logoutput);
        }
        return encrypt;
    }
    string RailfenceTrans(string word, bool invert)
    {
        string[] letterrows = new string[(Bomb.GetPortCount() % 4) + 2];
        int offset = 1;
        int cur = 0;
        if(invert)
        {
            letterrows[0] = "*";
            for (int aa = 1; aa < 6; aa++)
            {
                cur += offset;
                letterrows[cur] = letterrows[cur] + "*";
                if (cur == letterrows.Length - 1)
                    offset = -1;
                else if (cur == 0)
                    offset = 1;
            }
            cur = 0;
            for(int bb = 0; bb < letterrows.Length; bb++)
            {
                int num = letterrows[bb].Length;
                letterrows[bb] = word.Substring(cur, letterrows[bb].Length);
                cur += letterrows[bb].Length;
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Railfence Row #{1}: {2}", moduleId, bb + 1, letterrows[bb]);
            }
            string encrypt = word[0].ToString();
            if(letterrows.Length == 2)
            {
                encrypt = letterrows[0][0] + "" + letterrows[1][0] + "" + letterrows[0][1] + "" + letterrows[1][1] + "" + letterrows[0][2] + "" + letterrows[1][2];
            }
            else
            {
                cur = 0;
                int curx = 0;
                offset = 1;
                for (int aa = 1; aa < 6; aa++)
                {
                    cur += offset;
                    encrypt = encrypt + "" + letterrows[cur][curx];
                    if(cur == (letterrows.Length - 1))
                    {
                        offset = -1;
                        curx++;
                    }
                    else if(cur == 0)
                    {
                        offset = 1;
                        curx++;
                    }
                }
            }
            return encrypt;
        }
        else
        {
            letterrows[0] = word[0].ToString();
            for (int aa = 1; aa < 6; aa++)
            {
                cur += offset;
                letterrows[cur] = letterrows[cur] + "" + word[aa];
                if (cur == letterrows.Length - 1)
                    offset = -1;
                else if (cur == 0)
                    offset = 1;
            }
            string encrypt = "";
            for (int bb = 0; bb < letterrows.Length; bb++)
            {
                encrypt = encrypt + "" + letterrows[bb].ToUpper();
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Railfence Row #{1}: {2}", moduleId, bb + 1, letterrows[bb]);
            }
            return encrypt;
        }
        
    }
    string DigrafidEnc(string word, bool invert)
    {
        string kw1 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        string kw2 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        while (kw1.Equals(kw2))
            kw2 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        string key1 = getKey(kw1.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumberLetters().First()) % 2 == 0);
        string key2 = getKey(kw2.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumberLetters().Last()) % 2 == 1);
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Key A: {1}", moduleId, key1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Key B: {1}", moduleId, key2);
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Key A: {1}", moduleId, key1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Key B: {1}", moduleId, key2);
        }
        key1 = key1 + "#";
        key2 = key2 + "#";
        string[] numbers = { "", "", "" };
        string[] grid3x3 = { "123", "456", "789" };
        for (int aa = 0; aa < 3; aa++)
        {
            int num = key1.IndexOf(word[(aa * 2)]);
            numbers[0] = numbers[0] + "" + ((num % 9) + 1);
            int row = num / 9;
            num = key2.IndexOf(word[(aa * 2) + 1]);
            numbers[2] = numbers[2] + "" + ((num % 9) + 1);
            int col = num / 9;
            numbers[1] = numbers[1] + "" + grid3x3[row][col];
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] {1}{2} -> {3}{4}{5}", moduleId, word[aa * 2], word[(aa * 2) + 1], numbers[0][aa], numbers[1][aa], numbers[2][aa]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] {1}{2} -> {3}{4}{5}", moduleId, word[aa * 2], word[(aa * 2) + 1], numbers[0][aa], numbers[1][aa], numbers[2][aa]);
        }
        string encrypt = "";
        for (int bb = 0; bb < 3; bb++)
        {
            encrypt = encrypt + "" + key1[((numbers[bb][0] - '0') - 1) + (((numbers[bb][1] - '0') - 1) / 3) * 9];
            encrypt = encrypt + "" + key2[((numbers[bb][2] - '0') - 1) + (((numbers[bb][1] - '0') - 1) % 3) * 9];
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Digrafid Row #{1}: {2} -> {3}{4}", moduleId, bb + 1, numbers[bb], encrypt[bb * 2], encrypt[(bb * 2) + 1]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Digrafid Row #{1}: {2} -> {3}{4}", moduleId, bb + 1, numbers[bb], encrypt[bb * 2], encrypt[(bb * 2) + 1]);
        }
        if (encrypt.Contains('#'))
        {
            if(invert)
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] ERROR!!! REGENERATING KEYWORDS!!!", moduleId);
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] PAY NO ATTENTION TO THE ENCRYPTION ABOVE!!!", moduleId);
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] --------------------------------------------", moduleId);
            }
            else
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] ERROR!!! REGENERATING KEYWORDS!!!", moduleId);
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] PAY NO ATTENTION TO THE ENCRYPTION ABOVE!!!", moduleId);
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] --------------------------------------------", moduleId);
            }
            return DigrafidEnc(word.ToUpper(), invert);
        }

        else
        {
            pages[page - 1][1] = kw1.ToUpper();
            pages[page - 1][2] = kw2.ToUpper();
            return encrypt;
        }
    }
    string graycipher(string word, bool invert)
    {
        fontsizes[page][0] = 40;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 40;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 40;
        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Gray Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Begin Ragbaby Encryption", moduleId);
            string encrypt = RagbabyEnc(word.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Begin Bit Switch Encryption", moduleId);
            encrypt = BitSwitchEnc(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Begin Portax Encryption", moduleId);
            encrypt = PortaxEnc(encrypt.ToUpper(), invert);
            return encrypt;
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Gray Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Begin Portax Encryption", moduleId);
            string encrypt = PortaxEnc(word.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Begin Bit Switch Encryption", moduleId);
            encrypt = BitSwitchEnc(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Begin Ragbaby Encryption", moduleId);
            encrypt = RagbabyEnc(encrypt.ToUpper(), invert);
            return encrypt;
        }
    }
    string PortaxEnc(string word, bool invert)
    {
        char letter = Bomb.GetSerialNumberLetters().ToArray()[1];
        string[] slides =
        {
            "ABCDEFGHIJKLM",
            "NOPQRSTUVWXYZ",
            "ACEGIKMOQSUWY",
            "BDFHJLNPRTVXZ"
        };
        int num = slides[2].IndexOf(letter);
        if (num < 0)
            num = slides[3].IndexOf(letter);
        slides[1] = slides[1].Substring(num) + "" + slides[1].Substring(0, num);
        slides[2] = slides[2].Substring(num) + "" + slides[2].Substring(0, num);
        slides[3] = slides[3].Substring(num) + "" + slides[3].Substring(0, num);
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Slides:", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}", moduleId, slides[0]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}", moduleId, slides[1]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}", moduleId, slides[2]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}", moduleId, slides[3]);
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Slides:", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}", moduleId, slides[0]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}", moduleId, slides[1]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}", moduleId, slides[2]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}", moduleId, slides[3]);
        }
        char[] letterenc = new char[6];
        for (int aa = 0; aa < 3; aa++)
        {
            int num1 = slides[0].IndexOf(word[aa]);
            int col1 = 0;
            if (num1 < 0)
            {
                num1 = slides[1].IndexOf(word[aa]);
                col1 = 1;
            }
            int num2 = slides[2].IndexOf(word[aa + 3]);
            int col2 = 2;
            if (num2 < 0)
            {
                num2 = slides[3].IndexOf(word[aa + 3]);
                col2 = 3;
            }
            if (num1 == num2)
            {
                letterenc[aa] = slides[(col1 + 1) % 2][num1];
                letterenc[aa + 3] = slides[((col2 - 1) % 2) + 2][num2];
            }
            else
            {
                letterenc[aa] = slides[col1][num2];
                letterenc[aa + 3] = slides[col2][num1];
            }
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}{2} -> {3}{4}", moduleId, word[aa], word[aa + 3], letterenc[aa], letterenc[aa + 3]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}{2} -> {3}{4}", moduleId, word[aa], word[aa + 3], letterenc[aa], letterenc[aa + 3]);
        }
        string encrypt = letterenc[0] + "" + letterenc[1] + "" + letterenc[2] + "" + letterenc[3] + "" + letterenc[4] + "" + letterenc[5];
        return encrypt;
    }
    string BitSwitchEnc(string word, bool invert)
    {

        string encrypt = "";
        string order = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string p7alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string[] scramblers = {
              "21453", "21534", "31524", "31452", "41523", "41532", "51234",
              "51423", "51432", "23154", "25134", "24153", "34152", "35124",
              "45123", "45132", "54123", "54132", "24513", "25413", "34512",
              "35214", "35412", "43512", "45213", "53412", "54213", "24531",
              "25431", "34521", "35421", "43251", "43521", "45231", "53421",
              "54231"
        };
        string[] binary = {
            "00001", "00010", "00011", "00100", "00101", "00110", "00111",
            "01000", "01001", "01010", "01011", "01100", "01101", "01110",
            "01111", "10000", "10001", "10010", "10011", "10100", "10101",
            "10110", "10111", "11000", "11001", "11010", "11011", "11100",
            "11101", "11110", "11111"
         };
        string scrambler = scramblers[order.IndexOf(Bomb.GetSerialNumber()[0])];
        string binarytrue = "";
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Scrambler: {1}", moduleId, scrambler);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Scrambler: {1}", moduleId, scrambler);
        for (int aa = 0; aa < 6; aa++)
        {
            string binarylet = binary[p7alphabet.IndexOf(word[aa])];
            string convbin = scrambling(binarylet, scrambler, invert);
            for (int cc = 0; cc < binary.Length; cc++)
            {
                if (convbin.Equals(binary[cc]))
                {
                    if (cc >= 26)
                        binarytrue = binarytrue + "1";
                    else
                        binarytrue = binarytrue + "0";
                    encrypt = encrypt + "" + p7alphabet[cc % 26];
                    break;
                }
            }
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1} -> {2} -> {3} -> {4}", moduleId, word[aa], binarylet, convbin, encrypt[aa]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1} -> {2} -> {3} -> {4}", moduleId, word[aa], binarylet, convbin, encrypt[aa]);
        }
        pages[page - 1][2] = binarytrue;
        return encrypt;
    }
    string scrambling(string bin, string scrambler, bool invert)
    {
        char[] c = new char[5];
        if(invert)
        {
            for (int aa = 0; aa < 5; aa++)
                c[aa] = bin[scrambler[aa] - '0' - 1];
        }
        else
        {
            for (int aa = 0; aa < 5; aa++)
                c[scrambler[aa] - '0' - 1] = bin[aa];
        }
        
        return (c[0] + "" + c[1] + "" + c[2] + "" + c[3] + "" + c[4]);
    }
    string RagbabyEnc(string word, bool invert)
    {
        string kw = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        pages[page - 1][1] = kw.ToUpper();
        string key = getKey(kw.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetOffIndicators().Count() % 2 == 0);
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Generated Key: {1}", moduleId, key);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Generated Key: {1}", moduleId, key);
        string encrypt = "";
        for (int aa = 0; aa < 6; aa++)
        {
            if(invert)
            {
                encrypt = encrypt + "" + key[correction(key.IndexOf(word[aa]) - (aa + 1), 26)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
            }
            else
            {
                encrypt = encrypt + "" + key[(key.IndexOf(word[aa]) + (aa + 1)) % 26];
                Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
            }
        }
        return encrypt;
    }
    string whitecipher(string word, bool invert)
    {
        fontsizes[page][0] = 35;
        fontsizes[page][1] = 35;
        fontsizes[page][2] = 40;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 40;
        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted White Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Begin Base Caesar Encryption", moduleId);
            string encrypt = BaseCaesarEnc(word.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Begin Sean Encryption", moduleId);
            encrypt = SeanEnc(encrypt.ToUpper(), invert);
            string kw1 = encrypt.Split(' ')[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Begin Grille Transposition", moduleId);
            kw1 = GrilleTrans(kw1, invert);
            pages[page][0] = kw1.Substring(0, 8);
            pages[page][1] = kw1.Substring(8);
            return encrypt.Split(' ')[0];
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin White Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Begin Sean Encryption", moduleId);
            string encrypt = SeanEnc(word, invert);
            string kw1 = encrypt.Split(' ')[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Begin Base Caesar Encryption", moduleId);
            encrypt = BaseCaesarEnc(encrypt.Split(' ')[0], invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Begin Grille Transposition", moduleId);
            kw1 = GrilleTrans(kw1, invert);
            pages[page][0] = kw1.Substring(0, 8);
            pages[page][1] = kw1.Substring(8);
            return encrypt;
        }
    }
    string SeanEnc(string word, bool invert)
    {
        string encrypt = "";
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string kw1 = "";
        for (int aa = 0; aa < 16; aa++)
            kw1 = kw1 + "" + alpha[UnityEngine.Random.Range(0, alpha.Length)];

        string key = getKey(kw1.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetOnIndicators().Count() % 2 == 0);
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Key: {1}", moduleId, key);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Key: {1}", moduleId, key);
        string[] cipher = { key.Substring(0, 13), key.Substring(13) };
        for (int bb = 0; bb < 6; bb++)
        {
            if (cipher[0].IndexOf(word[bb]) >= 0)
                encrypt = encrypt + "" + cipher[1][cipher[0].IndexOf(word[bb])];
            else
                encrypt = encrypt + "" + cipher[0][cipher[1].IndexOf(word[bb])];
            if(invert)
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1} -> {2} ", moduleId, word[bb], encrypt[bb]);
                char top = cipher[0][0];
                char bot = cipher[1][12];
                cipher[0] = cipher[0].Substring(1) + "" + bot;
                cipher[1] = top + "" + cipher[1].Substring(0, 12);
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Key: {1}", moduleId, cipher[0] + "" + cipher[1]);
            }
            else
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1} -> {2} ", moduleId, word[bb], encrypt[bb]);
                char top = cipher[0][12];
                char bot = cipher[1][0];
                cipher[0] = bot + "" + cipher[0].Substring(0, 12);
                cipher[1] = cipher[1].Substring(1) + "" + top;
                Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Key: {1}", moduleId, cipher[0] + "" + cipher[1]);
            }
        }
        return encrypt + " " + kw1;
    }
    string BaseCaesarEnc(string word, bool invert)
    {
        int offset = UnityEngine.Random.Range(1, 25) + (26 * UnityEngine.Random.Range(1, 6));
        int sum = 0;
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Generated Offset: {1}", moduleId, offset);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Generated Offset: {1}", moduleId, offset);
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string encrypt = "";
        string logoutput = "";
        for (int aa = 0; aa < 6; aa++)
        {
            if(invert)
            {
                encrypt = encrypt + "" + alpha[correction(alpha.IndexOf(word[aa]) + offset, 26)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
            }
            else
            {
                encrypt = encrypt + "" + alpha[correction(alpha.IndexOf(word[aa]) - offset, 26)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
            }
            sum += (alpha.IndexOf(encrypt[aa]) + 1);
            logoutput = logoutput + "" + (alpha.IndexOf(encrypt[aa]) + 1) + " + ";
            
        }
        sum = (sum % 8) + 2;
        logoutput = "((" + logoutput.Substring(0, logoutput.Length - 2) + ") % 8) + 2 = " + sum;
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Generated Base: {1}", moduleId, logoutput);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Generated Base: {1}", moduleId, logoutput);
        string baseOffset = "";
        while (offset != 0)
        {
            baseOffset = (offset % sum) + "" + baseOffset;
            offset = offset / sum;
        }
        pages[page - 1][1] = baseOffset.ToUpper();
        return encrypt;
    }
    string GrilleTrans(string word, bool invert)
    {
        int num = Bomb.GetPortCount() % 4;
        int[] pos = { 0, 12, 13, 4, 8, 5, 9, 1, 10, 2, 14, 3, 15, 6, 7, 11 }; ;

        for (int aa = 0; aa < 16; aa++)
        {
            for (int bb = 0; bb < num; bb++)
                pos[aa] -= 4;
            pos[aa] = correction(pos[aa], 16);
        }
        string encrypt = "";
        for (int cc = 0; cc < 16; cc++)
            encrypt = encrypt + "" + word[pos[cc]];
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Clockwise rotations: {1}", moduleId, num);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1} -> {2}", moduleId, word, encrypt);
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Clockwise rotations: {1}", moduleId, num);
            Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1} -> {2}", moduleId, word, encrypt);
        }
        
        return encrypt;
    }
    string violetcipher(string word, bool invert)
    {
        fontsizes[page][0] = 40;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 40;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 40;
        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Violet Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] Begin Porta Encryption", moduleId);
            string kw1 = matrixWordList[UnityEngine.Random.Range(0, 26) + 52];
            string encrypt = PortaEnc(word.ToUpper(), kw1, invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] Begin Route Transposition", moduleId);
            encrypt = RouteTrans(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] Begin Quagmire Encryption", moduleId);
            encrypt = QuagmireEnc(encrypt, kw1.ToUpper(), invert);
            return encrypt;
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Violet Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] Begin Quagmire Encryption", moduleId);
            string kw1 = matrixWordList[UnityEngine.Random.Range(0, 26) + 52];
            string encrypt = QuagmireEnc(word, kw1.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] Begin Route Transposition", moduleId);
            encrypt = RouteTrans(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] Begin Porta Encryption", moduleId);
            encrypt = PortaEnc(encrypt.ToUpper(), kw1, invert);
            return encrypt;
        }
    }
    string PortaEnc(string word, string kw1, bool invert)
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
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
        }
        return encrypt;
    }
    string RouteTrans(string word, bool invert)
    {
        string routenumber = UnityEngine.Random.Range(1, 2) + "" + UnityEngine.Random.Range(1, 6);
        pages[page - 1][2] = routenumber;
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
            if(invert)
                encrypt = encrypt + "" + word[order.IndexOf(cipher[aa])];
            else
                encrypt = encrypt + "" + word[cipher.IndexOf(order[aa])];
        }
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1} -> {2}", moduleId, word, encrypt);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1} -> {2}", moduleId, word, encrypt);
        return encrypt;
    }
    string QuagmireEnc(string word, string kw1, bool invert)
    {

        string kw2 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        string key = getKey(kw2.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetOnIndicators().Count() % 2 == 0);
        string[] cipher = new string[7];
        cipher[0] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < 6; aa++)
            cipher[aa + 1] = key.Substring(key.IndexOf(kw1[aa])) + "" + key.Substring(0, key.IndexOf(kw1[aa]));
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] Quagmire Rows: ", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", moduleId, cipher[1]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", moduleId, cipher[2]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", moduleId, cipher[3]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", moduleId, cipher[4]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", moduleId, cipher[5]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", moduleId, cipher[6]);
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] Quagmire Rows: ", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", moduleId, cipher[1]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", moduleId, cipher[2]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", moduleId, cipher[3]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", moduleId, cipher[4]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", moduleId, cipher[5]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", moduleId, cipher[6]);
        }
        

        pages[page][0] = kw2.ToUpper();
        pages[page - 1][1] = kw1.ToUpper();
        string encrypt = "";
        for (int bb = 0; bb < 6; bb++)
        {
            if(invert)
            {
                encrypt = encrypt + "" + cipher[0][cipher[bb + 1].IndexOf(word[bb])];
                Debug.LogFormat("[Violet Cipher #{0}] {1} -> {2}", moduleId, word[bb], encrypt[bb]);
            }
            else
            {
                encrypt = encrypt + "" + cipher[bb + 1][cipher[0].IndexOf(word[bb])];
                Debug.LogFormat("[Violet Cipher #{0}] {1} -> {2}", moduleId, word[bb], encrypt[bb]);
            }
            
        }
        return encrypt;
    }
    string indigocipher(string word, bool invert)
    {
        fontsizes[page][0] = 40;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 35;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 30;
        fontsizes[page - 1][2] = 40;
        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Indigo Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Begin Condi Encryption", moduleId);
            string encrypt = CondiEnc(word.ToUpper(), invert);
            string kw1 = encrypt.Split(' ')[1];
            encrypt = encrypt.Split(' ')[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Begin Logic Encryption", moduleId);
            encrypt = LogicEnc(encrypt.ToUpper(), invert);
            string kw2 = encrypt.Split(' ')[1];
            encrypt = encrypt.Split(' ')[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Begin Fractionated Morse Encryption", moduleId);
            string kw2enc = FractionatedMorseEnc(kw2.ToUpper(), kw1.ToUpper(), invert);
            pages[page - 1][2] = kw1.ToUpper();
            pages[page - 1][1] = kw2enc.ToUpper();
            return encrypt;
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Indigo Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Begin Logic Encryption", moduleId);
            string encrypt = LogicEnc(word.ToUpper(), invert);
            string kw2 = encrypt.Split(' ')[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Begin Condi Encryption", moduleId);
            encrypt = CondiEnc(encrypt.Split(' ')[0], invert);
            string kw1 = encrypt.Split(' ')[1];
            encrypt = encrypt.Split(' ')[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Begin Fractionated Morse Encryption", moduleId);
            string kw2enc = FractionatedMorseEnc(kw2.ToUpper(), kw1.ToUpper(), invert);
            pages[page - 1][2] = kw1.ToUpper();
            pages[page - 1][1] = kw2enc.ToUpper();
            return encrypt;
        }
    }
    string LogicEnc(string word, bool invert)
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
        string[] binalpha =
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
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Gate: {1}", moduleId, gates[logicgate]);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Gate: {1}", moduleId, gates[logicgate]);
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
            int numenc = 0;
            int numkw = 0;
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1} + {2} = {3}", moduleId, binenc, binkw, binlet);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1} + {2} = {3}", moduleId, binenc, binkw, binlet);

            for (int ff = 0; ff < 5; ff++)
            {
                int mult = 1;
                for (int gg = 0; gg < (4 - ff); gg++)
                    mult = mult * 2;
                if (binenc[ff] == '1')
                    numenc = numenc + mult;
                if (binkw[ff] == '1')
                    numkw = numkw + mult;
            }

            if (numenc >= 26)
                overenc = overenc + "1";
            else
                overenc = overenc + "0";
            if (numkw >= 26)
                overkw = overkw + "1";
            else
                overkw = overkw + "0";
            numenc = numenc % 26;
            numkw = numkw % 26;
            encrypt = encrypt + "" + alpha[numenc];
            kw = kw + "" + alpha[numkw];
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
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Logic Encryption Keyword: {1}", moduleId, kw);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Logic Encrypted Word: {1}", moduleId, encrypt);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Binary 1: {1}", moduleId, b1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Binary 2: {1}", moduleId, b2[0] + "" + b2[1] + "" + b2[2] + "" + b2[3] + "" + b2[4] + "" + b2[5]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Binary 3: {1}", moduleId, b3);
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Logic Encryption Keyword: {1}", moduleId, kw);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Logic Encrypted Word: {1}", moduleId, encrypt);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Binary 1: {1}", moduleId, b1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Binary 2: {1}", moduleId, b2[0] + "" + b2[1] + "" + b2[2] + "" + b2[3] + "" + b2[4] + "" + b2[5]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Binary 3: {1}", moduleId, b3);
        }
        
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

        pages[page][0] = overenc;
        pages[page][1] = overkw;
        pages[page][2] = num1 + " ? " + num2 + " = " + num3;
        return encrypt + " " + kw;
    }
    string CondiEnc(string word, bool invert)
    {
        string kw = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)].ToUpper();
        string key = getKey(kw.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetPortCount() % 2 == 1);
        int offset = Bomb.GetSerialNumberNumbers().Sum();
        string encrypt = "";
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Key: {1}", moduleId, key);
            for (int aa = 0; aa < 6; aa++)
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Offset: {1}", moduleId, offset);
                encrypt = encrypt + "" + key[correction(key.IndexOf(word[aa]) - offset, 26)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
                offset = key.IndexOf(encrypt[aa]) + 1;
            }
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Key: {1}", moduleId, key);
            for (int aa = 0; aa < 6; aa++)
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Offset: {1}", moduleId, offset);
                encrypt = encrypt + "" + key[(key.IndexOf(word[aa]) + offset) % 26];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
                offset = key.IndexOf(word[aa]) + 1;
            }
        }
        
        return encrypt + " " + kw;
    }
    string FractionatedMorseEnc(string word, string kw, bool invert)
    {
        string key = getKey(kw.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetPortCount() % 2 == 1);
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Key: {1}", moduleId, key);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Key: {1}", moduleId, key);
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
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1} -> {2}", moduleId, word[aa], morword);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1} -> {2}", moduleId, word[aa], morword);
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
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Morse rows:", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1}", moduleId, rows[0]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1}", moduleId, rows[1]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1}", moduleId, rows[2]);
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Morse rows:", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1}", moduleId, rows[0]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1}", moduleId, rows[1]);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1}", moduleId, rows[2]);
        }
        
        string encrypt = "";
        for (int bb = 0; bb < rows[0].Length; bb++)
        {
            string morenc = rows[0][bb] + "" + rows[1][bb] + "" + rows[2][bb];
            for (int cc = 0; cc < morsekey.Length; cc++)
            {
                if (morenc.Equals(morsekey[cc]))
                {
                    if(invert)
                        Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1} -> {2}", moduleId, morenc, key[cc]);
                    else
                        Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1} -> {2}", moduleId, morenc, key[cc]);
                    encrypt = encrypt + "" + key[cc];
                    break;
                }
            }
        }
        return encrypt;
    }
    string bluecipher(string word, bool invert)
    {
        fontsizes[page][0] = 40;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 40;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 40;
        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Blue Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] Begin Atbash Encryption", moduleId);
            string encrypt = Atbash(word.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] Begin Letter Transposition", moduleId);
            encrypt = LetterTrans(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] Begin Vigenere Encryption", moduleId);
            encrypt = VigenereEnc(encrypt.ToUpper(), invert);
            return encrypt;
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Blue Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] Begin Columnar Transposition", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] Begin Vigenere Encryption", moduleId);
            string encrypt = VigenereEnc(word.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] Begin Letter Transposition", moduleId);
            encrypt = LetterTrans(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] Begin Atbash Encryption", moduleId);
            encrypt = Atbash(encrypt.ToUpper(), invert);
            return encrypt;
        }
    }
    string VigenereEnc(string word, bool invert)
    {
        string kw = matrixWordList[UnityEngine.Random.Range(0, 26) + 52];
        pages[page][0] = kw.ToUpper();
        string key = "BNUPRELIAVGFDHOXCWMQYSJKZT";
        string encrypt = "";
        if(invert)
        {
            for (int aa = 0; aa < 6; aa++)
            {
                encrypt = encrypt + "" + key[correction(key.IndexOf(word[aa]) - key.IndexOf(kw[aa]), 26)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] {1} - {2} = {3}", moduleId, word[aa], kw[aa], encrypt[aa]);
            }
        }
        else
        {
            for (int aa = 0; aa < 6; aa++)
            {
                encrypt = encrypt + "" + key[(key.IndexOf(word[aa]) + key.IndexOf(kw[aa])) % 26];
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] {1} + {2} = {3}", moduleId, word[aa], kw[aa], encrypt[aa]);
            }
        }

        return encrypt;
    }
    string LetterTrans(string word, bool invert)
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
        string[] swaps = { "", "" };
        for (int aa = 0; aa < 6; aa++)
        {
            int n1 = UnityEngine.Random.Range(0, 10);
            int n2 = UnityEngine.Random.Range(0, 10);
            if(invert)
            {
                swaps[0] = swaps[0] + "" + n1;
                swaps[1] = swaps[1] + "" + n2;
            }
            else
            {
                swaps[0] = n1 + "" + swaps[0];
                swaps[1] = n2 + "" + swaps[1];
            }
            
            string instruction = table[n2][n1].ToUpper();
            string logoutput = instruction.ToUpper() + ": " + encrypt + " -> ";
            if (instruction.Equals("RV"))
            {
                string conv = "";
                for (int bb = 0; bb < 6; bb++)
                    conv = encrypt[bb] + "" + conv;
                encrypt = conv.ToUpper();
            }
            else if (instruction[0] == 'R')
            {
                if(invert)
                {
                    for (int bb = 0; bb < (instruction[1] - '0'); bb++)
                        encrypt = encrypt[5] + "" + encrypt.Substring(0, 5);
                }
                else
                {
                    for (int bb = 0; bb < (instruction[1] - '0'); bb++)
                        encrypt = encrypt.Substring(1) + "" + encrypt[0];
                }
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
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] {1}", moduleId, logoutput);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] {1}", moduleId, logoutput);
        }
        pages[page - 1][1] = swaps[0].ToUpper();
        pages[page - 1][2] = swaps[1].ToUpper();
        return encrypt;
    }
    string Atbash(string word, bool invert)
    {
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string encrypt = "";
        for (int aa = 0; aa < 6; aa++)
        {
            encrypt = encrypt + "" + alpha[25 - alpha.IndexOf(word[aa])];
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
        }
        return encrypt;
    }
    string greencipher(string word, bool invert)
    {
        fontsizes[page][0] = 40;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 40;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 40;
        if(invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Green Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Begin Columnar Transposition", moduleId);
            string encrypt = ColumnTrans(word.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Begin Mechanical Encryption", moduleId);
            string kw = matrixWordList[UnityEngine.Random.Range(0, 26) + 52];
            encrypt = MechanicalEnc(encrypt.ToUpper(), kw.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Begin Homophonic Encryption", moduleId);
            kw = HomophonicEnc(kw, invert);
            pages[page - 1][1] = kw.Substring(0, 6);
            pages[page - 1][2] = kw.Substring(6);
            return encrypt;
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Green Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Begin Mechanical Encryption", moduleId);
            string kw = matrixWordList[UnityEngine.Random.Range(0, 26) + 52];
            string encrypt = MechanicalEnc(word.ToUpper(), kw.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Begin Columnar Transposition", moduleId);
            encrypt = ColumnTrans(encrypt.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Begin Homophonic Encryption", moduleId);
            kw = HomophonicEnc(kw, invert);
            pages[page - 1][1] = kw.Substring(0, 6);
            pages[page - 1][2] = kw.Substring(6);
            return encrypt;
        }
    }
    string HomophonicEnc(string word, bool invert)
    {
        string encrypt = "";
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string kw = alpha[UnityEngine.Random.Range(0, 26)] + "" + alpha[UnityEngine.Random.Range(0, 26)] + "" + alpha[UnityEngine.Random.Range(0, 26)];
        int[][] nums = new int[3][];
        for (int aa = 0; aa < nums.Length; aa++)
        {
            nums[aa] = new int[26];
            for (int bb = 0; bb < 26; bb++)
                nums[aa][bb] = (bb + 1) + (26 * aa);
        }

        for (int cc = 0; cc < 3; cc++)
        {
            int rotations = alpha.IndexOf(kw[cc]);
            for (int dd = 0; dd < rotations; dd++)
                nums[cc] = HomophonicRot(nums[cc]);
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Row {1}: {2}", moduleId, cc + 1, nums[cc][0]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Row {1}: {2}", moduleId, cc + 1, nums[cc][0]);
        }
        for (int dd = 0; dd < 6; dd++)
        {
            string num = nums[UnityEngine.Random.Range(0, 3)][alpha.IndexOf(word[dd])] + "";
            if (num.Length == 1)
                num = "0" + num[0];
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] {1} -> {2}", moduleId, word[dd], num);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] {1} -> {2}", moduleId, word[dd], num);
            encrypt = encrypt + "" + num;
        }
        pages[page][0] = kw.ToUpper();
        return encrypt;
    }
    int[] HomophonicRot(int[] n)
    {
        int[] c = new int[26];
        for (int aa = 1; aa < 26; aa++)
            c[aa] = n[aa - 1];
        c[0] = n[25];
        return c;
    }
    string ColumnTrans(string word, bool invert)
    {
        int numcols = UnityEngine.Random.Range(0, 5) + 2;
        string temp = "123456".Substring(0, numcols);
        string nums = "";
        for (int aa = 0; aa < numcols; aa++)
        {
            int randnum = UnityEngine.Random.Range(0, temp.Length);
            nums = nums + "" + temp[randnum];
            temp = temp.Substring(0, randnum) + "" + temp.Substring(randnum + 1);
        }
        string[] columns = new string[numcols];
        pages[page][1] = nums.ToUpper();
        if (invert)
        {
            for (int bb = 0; bb < 6; bb++)
            {
                columns[bb % numcols] = columns[bb % numcols] + "*";
            }
            string encrypt = "";
            int numlets = 0;
            for (int cc = 0; cc < numcols; cc++)
            {
                string find = (cc + 1) + "";
                int cur = nums.IndexOf(find[0]);
                int repeat = columns[cur].Length;
                columns[cur] = "";
                for(int dd = 0; dd < repeat; dd++)
                {
                    columns[cur] = columns[cur] + "" + word[numlets];
                    numlets++;
                }
            }
            for (int cc = 0; cc < numcols; cc++)
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Column {1}: {2}", moduleId, cc + 1, columns[cc]);
            }
            for (int ee = 0; ee < 6; ee++)
            {
                encrypt = encrypt + "" + columns[ee % columns.Length][0];
                columns[ee % columns.Length] = columns[ee % columns.Length].Substring(1);
            }
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] {1} -> {2}", moduleId, word, encrypt);
            return encrypt;
        }
       else
        {
            for (int bb = 0; bb < 6; bb++)
            {
                columns[bb % numcols] = columns[bb % numcols] + "" + word[bb];
            }
            string encrypt = "";
            for (int cc = 0; cc < numcols; cc++)
            {
                string find = (cc + 1) + "";
                encrypt = encrypt + "" + (columns[nums.IndexOf(find[0])]);
                Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Column {1}: {2}", moduleId, cc + 1, columns[nums.IndexOf(find[0])]);
            }
            Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] {1} -> {2}", moduleId, word, encrypt);
            return encrypt;
        }
    }
    string MechanicalEnc(string word, string kw, bool invert)
    {
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string[] table =
        {
            "UFHKQIPLXNZESGBVMCWJRDOTYA",
            "IWCZYMLKJODGFSQRNBTXHUEVAP",
            "WBSMEJTUCPFAHZOQLIKNYVGXRD",
            "GRINQVWOTYAJXBMHCFKLDUSZEP",
            "DLTVSUIKWCXRFJZANYHMQOGEPB",
            "FSVCEIUJKPGNTYHBLRQOXMADWZ",
            "JOCYWFPADKHIUVTSMENGQLZBRX",
            "BPHORAKNUETDZYQIMSFJGVWCXL",
            "ANDSQWTGXKFPCOVBLMYEZHRJIU",
            "AQJPBUSGWNXZVDYLETCOFHRIMK",
            "BHFTDGERXJAMUNZVYKOSPILCWQ",
            "JHUKDMSNEBICZYWLXQFPORTAVG",
            "ASNTZDBGWYILEORCQFXJPKHMVU",
            "RPCQABVLGWFENIKYMDUTSJXOZH",
            "YIXNVWQSUHFOMZDGKJPCTBELAR",
            "IMPCZLEGJARNTWSYFQDOUBKHVX",
            "JGKOXMUBAVRTFYCNPWQZESILHD",
            "SVHDBZNMKWJIEUYFXRQPLGCATO",
            "TZXGOPNBWAIYRHQLVKJSCDUEFM",
            "DJQZYWTPKIXCVABFNUEOLHSGRM",
            "CJOEDYHBNIXZRTPWGALFKUSMVQ",
            "FEHLYOBGRXQKVZUIMJTNACDPSW",
            "MOGAPTHIZXRFKLYSVDBWUQNECJ",
            "RXMSBPWOEJADIYNQLGKCTUHZFV",
            "ZJVWFBEOTKRDHSCPIGQNAYLUXM",
            "VWFXUEKRLBQTMCHSGJOZYDAPIN"
        };
        string encrypt = "";
        if(invert)
        {
            for (int aa = 0; aa < 6; aa++)
            {
                int row = alpha.IndexOf(kw[aa]);
                int col = table[row].IndexOf(word[aa]);
                encrypt = encrypt + "" + alpha[col];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] {1} + {2} -> {3}", moduleId, kw[aa], word[aa], encrypt[aa]);
            }
        }
        else
        {
            for (int aa = 0; aa < 6; aa++)
            {
                int row = alpha.IndexOf(kw[aa]);
                int col = alpha.IndexOf(word[aa]);
                encrypt = encrypt + "" + table[row][col];
                Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] {1} + {2} -> {3}", moduleId, kw[aa], word[aa], encrypt[aa]);
            }
        }
       
        return encrypt;
    }
    string yellowcipher(string word, bool invert)
    {
        fontsizes[page][0] = 40;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 35;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 40;
        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Yellow Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Begin Trifid Encryption", moduleId);
            string encrypt = TrifidEnc(word.ToUpper(), invert);
            string[] split = encrypt.Split(' ');
            encrypt = split[0].ToUpper();
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Begin Morbit Encryption", moduleId);
            string kw1 = MorbitEnc(split[1].ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Begin Hill Encryption", moduleId);
            encrypt = HillEnc(encrypt, invert);
            int num = kw1.Length / 3;
            pages[page - 1][1] = kw1.Substring(0, num);
            pages[page - 1][2] = kw1.Substring(num, num);
            pages[page][0] = kw1.Substring(num + num);
            return encrypt;
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Yellow Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Begin Hill Encryption", moduleId);
            string encrypt = HillEnc(word, invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Begin Trifid Encryption", moduleId);
            encrypt = TrifidEnc(encrypt, invert);
            string[] split = encrypt.Split(' ');
            encrypt = split[0].ToUpper();
            Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Begin Morbit Encryption", moduleId);
            string kw1 = MorbitEnc(split[1].ToUpper(), invert);
            int num = kw1.Length / 3;
            pages[page - 1][1] = kw1.Substring(0, num);
            pages[page - 1][2] = kw1.Substring(num, num);
            pages[page][0] = kw1.Substring(num + num);
            return encrypt;
        }

    }
    string MorbitEnc(string word, bool invert)
    {

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


        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string encrypt = "";

        for (int aa = 0; aa < word.Length; aa++)
            encrypt = encrypt + "" + morse[alpha.IndexOf(word[aa])] + "X";
        if (encrypt.Length % 2 == 1)
            encrypt = encrypt.Substring(0, encrypt.Length - 1);

        char[] nums = new char[8];
        int items = 0;
        string key = "12345678";
        string kw = matrixWordList[UnityEngine.Random.Range(0, 26) + 104];
        for (int cc = 0; cc < alpha.Length; cc++)
        {
            for (int dd = 0; dd < kw.Length; dd++)
            {
                if (kw[dd].ToString().IndexOf(alpha[cc]) >= 0)
                {
                    nums[dd] = key[items];
                    items++;
                }
            }
        }
        key = nums[0] + "" + nums[1] + "" + nums[2] + "" + nums[3] + "" + nums[4] + "" + nums[5] + "" + nums[6] + "" + nums[7];
        string encrypt2 = "";
        for (int ee = 0; ee < encrypt.Length; ee++)
        {
            string morsepair = encrypt[ee] + "" + encrypt[ee + 1];
            ee++;
            switch (morsepair)
            {
                case "..":
                    encrypt2 = encrypt2 + "" + key[0];
                    break;
                case ".-":
                    encrypt2 = encrypt2 + "" + key[1];
                    break;
                case ".X":
                    encrypt2 = encrypt2 + "" + key[2];
                    break;
                case "-.":
                    encrypt2 = encrypt2 + "" + key[3];
                    break;
                case "--":
                    encrypt2 = encrypt2 + "" + key[4];
                    break;
                case "-X":
                    encrypt2 = encrypt2 + "" + key[5];
                    break;
                case "X.":
                    encrypt2 = encrypt2 + "" + key[6];
                    break;
                case "X-":
                    encrypt2 = encrypt2 + "" + key[7];
                    break;
            }
        }
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Morbit Key: {1}", moduleId, key);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Morbit Encrypted Word: {1}", moduleId, encrypt2);
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Morbit Key: {1}", moduleId, key);
            Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Morbit Encrypted Word: {1}", moduleId, encrypt2);
        }
        
        pages[page][1] = kw.ToUpper();

        return encrypt2;
    }
    string TrifidEnc(string word, bool inverse)
    {
        string kw = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        int snnum = Bomb.GetSerialNumberNumbers().First();
        string key = getKey(kw, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetBatteryCount() % 2 == 1);
        string encrypt = "";
        string[] grid =
        {
            "11111111122222222233333333",
            "11122233311122233311122233",
            "12312312312312312312312312"
        };
        string[] numbers =
            {
                "", "", ""
            };
        if(inverse)
        {
            for (int aa = 0; aa < 6; aa++)
            {
                int cursor = key.IndexOf(word[aa]);
                numbers[aa / 2] = numbers[aa / 2] + "" + grid[0][cursor];
                numbers[aa / 2] = numbers[aa / 2] + "" + grid[1][cursor];
                numbers[aa / 2] = numbers[aa / 2] + "" + grid[2][cursor];
            }
            bool flag = true;
            for (int bb = 0; bb < 6; bb++)
            {
                string n1 = numbers[0][bb] + "" + numbers[1][bb] + "" + numbers[2][bb];
                if (n1.Equals("333"))
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Trifid Key: {1}", moduleId, key);
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Trifid Numbers:", moduleId);
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1}", moduleId, numbers[0]);
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1}", moduleId, numbers[1]);
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1}", moduleId, numbers[2]);
                for (int bb = 0; bb < 6; bb++)
                {
                    string nums = numbers[0][bb] + "" + numbers[1][bb] + "" + numbers[2][bb];
                    string pos = "931";
                    int num = 0;
                    for (int dd = 0; dd < 3; dd++)
                    {
                        num += ((pos[dd] - '0') * (nums[dd] - '0' - 1));
                    }
                    encrypt = encrypt + "" + key[num];
                }
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Trifid Encrypted Word: {1}", moduleId, encrypt);
                return encrypt + " " + kw;
            }
            else
            {
                return TrifidEnc(word, inverse);
            }
        }
        else
        {
            for (int aa = 0; aa < 6; aa++)
            {
                int cursor = key.IndexOf(word[aa]);
                numbers[0] = numbers[0] + "" + grid[0][cursor];
                numbers[1] = numbers[1] + "" + grid[1][cursor];
                numbers[2] = numbers[2] + "" + grid[2][cursor];
            }
            bool flag = true;
            for (int bb = 0; bb < 3; bb++)
            {
                string n1 = numbers[bb][0] + "" + numbers[bb][1] + "" + numbers[bb][2];
                string n2 = numbers[bb][3] + "" + numbers[bb][4] + "" + numbers[bb][5];
                if (n1.Equals("333") || n2.Equals("333"))
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Trifid Key: {1}", moduleId, key);
                Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Trifid Numbers:", moduleId);
                Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] {1}", moduleId, numbers[0]);
                Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] {1}", moduleId, numbers[1]);
                Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] {1}", moduleId, numbers[2]);
                for (int bb = 0; bb < 3; bb++)
                {
                    string[] nums = new string[2];
                    nums[0] = numbers[bb][0] + "" + numbers[bb][1] + "" + numbers[bb][2];
                    nums[1] = numbers[bb][3] + "" + numbers[bb][4] + "" + numbers[bb][5];
                    string pos = "931";
                    for (int cc = 0; cc < 2; cc++)
                    {
                        int num = 0;
                        for (int dd = 0; dd < 3; dd++)
                        {
                            num += ((pos[dd] - '0') * (nums[cc][dd] - '0' - 1));
                        }
                        encrypt = encrypt + "" + key[num];
                    }
                }
                Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Trifid Encrypted Word: {1}", moduleId, encrypt);
                return encrypt + " " + kw;
            }
            else
            {
                return TrifidEnc(word, inverse);
            }
        }
      
    }
    string HillEnc(string word, bool invert)
    {
        string encrypt = "";
        int[] numbers = new int[4];
        numbers[1] = UnityEngine.Random.Range(0, 26);
        numbers[2] = UnityEngine.Random.Range(0, 26);
        if ((numbers[1] * numbers[2]) % 2 == 1)
            numbers[0] = UnityEngine.Random.Range(1, 13) * 2;
        else
            numbers[0] = (UnityEngine.Random.Range(0, 13) * 2) + 1;

        int[] nums = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
        for (int aa = 0; aa < 26; aa++)
        {
            int num = (aa * numbers[0]) - (numbers[1] * numbers[2]);
            if ((num % 2 == 0) || (num % 13 == 0))
                nums = nums.Where(val => val != aa).ToArray();
        }

        numbers[3] = nums[UnityEngine.Random.Range(0, nums.Length - 1)];
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        if(invert)
        {
            int[] invnum = {numbers[3] * 1, numbers[1] * (-1), numbers[2] * (-1), numbers[0] * 1};
            int adbc = (numbers[0] * numbers[3]) - (numbers[1] * numbers[2]);
            int numN = 1;
            while (correction(numN * adbc, 26) != 1)
                numN++;
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] N: {1}", moduleId, numN);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Inverse of numbers:", moduleId);
            for (int cc = 0; cc < 4; cc++)
            {
                invnum[cc] = correction(invnum[cc] * numN, 26);
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1} -> {2}", moduleId, numbers[cc], invnum[cc]);
            }
                

            for (int bb = 0; bb < 6; bb++)
            {
                string logoutput = "";
                int n1 = (alpha.IndexOf(word[bb]) + 1) % 26;
                bb++;
                int n2 = (alpha.IndexOf(word[bb]) + 1) % 26;
                logoutput = logoutput + "" + n1 + " " + n2;
                int l1 = ((invnum[0] * n1) + (invnum[1] * n2)) % 26;
                int l2 = ((invnum[2] * n1) + (invnum[3] * n2)) % 26;
                logoutput = logoutput + " -> " + l1 + " " + l2;
                if (l1 == 0)
                    l1 = 26;
                if (l2 == 0)
                    l2 = 26;
                encrypt = encrypt + "" + alpha[l1 - 1] + "" + alpha[l2 - 1];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1}", moduleId, logoutput);
            }
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Hill Encrypted Word: {1}", moduleId, encrypt);
            pages[page][2] = numbers[0] + "-" + numbers[1] + "-" + numbers[2] + "-" + numbers[3];
            return encrypt;
        }
        else
        {
            for (int bb = 0; bb < 6; bb++)
            {
                string logoutput = "";
                int n1 = (alpha.IndexOf(word[bb]) + 1) % 26;
                bb++;
                int n2 = (alpha.IndexOf(word[bb]) + 1) % 26;
                logoutput = logoutput + "" + n1 + " " + n2;
                int l1 = ((numbers[0] * n1) + (numbers[1] * n2)) % 26;
                int l2 = ((numbers[2] * n1) + (numbers[3] * n2)) % 26;
                logoutput = logoutput + " -> " + l1 + " " + l2;
                if (l1 == 0)
                    l1 = 26;
                if (l2 == 0)
                    l2 = 26;
                encrypt = encrypt + "" + alpha[l1 - 1] + "" + alpha[l2 - 1];
                Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] {1}", moduleId, logoutput);
            }
            Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Hill Encrypted Word: {1}", moduleId, encrypt);
            pages[page][2] = numbers[0] + "-" + numbers[1] + "-" + numbers[2] + "-" + numbers[3];
            return encrypt;
        }
    }
    string orangecipher(string word, bool invert)
    {
        fontsizes[page][0] = 37;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 40;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 37;
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
        string keyword1 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        string keyword2 = "";
        bool flag = true;
        while (flag)
        {
            flag = false;
            keyword2 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
            if (keyword2.IndexOf('J') >= 0 || keyword2.EqualsIgnoreCase(keyword1))
                flag = true;
        }
        string number = UnityEngine.Random.Range(0, 10000) + "";


        pages[page][1] = keyword1.ToUpper();
        pages[page][2] = number;

        string matrixa = keyword1.Replace('J', 'I');
        string matrixb = "AFLQVBGMRWCHNSXDIOTYEKPUZ";
        string matrixc;
        string matrixd = keyword2.Replace('J', 'I');
        int numberplace = 0;

        //Creating Matrix C by converting Number to Words.
        if (number.Equals("0"))
            matrixc = "ZERO";
        else
        {
            matrixc = "";
            for (int dd = number.Length - 1; dd >= 0; dd--)
            {
                switch (numberplace)
                {
                    case 0:
                        switch (number[dd])
                        {
                            case '1':
                                matrixc = "ONE" + matrixc;
                                break;
                            case '2':
                                matrixc = "TWO" + matrixc;
                                break;
                            case '3':
                                matrixc = "THREE" + matrixc;
                                break;
                            case '4':
                                matrixc = "FOUR" + matrixc;
                                break;
                            case '5':
                                matrixc = "FIVE" + matrixc;
                                break;
                            case '6':
                                matrixc = "SIX" + matrixc;
                                break;
                            case '7':
                                matrixc = "SEVEN" + matrixc;
                                break;
                            case '8':
                                matrixc = "EIGHT" + matrixc;
                                break;
                            case '9':
                                matrixc = "NINE" + matrixc;
                                break;
                        }
                        break;
                    case 1:
                        switch (number[dd])
                        {
                            case '1':
                                switch (number[dd + 1])
                                {
                                    case '1':
                                        matrixc = "ELEVEN";
                                        break;
                                    case '2':
                                        matrixc = "TWELVE";
                                        break;
                                    case '3':
                                        matrixc = "THIRTEEN";
                                        break;
                                    case '4':
                                        matrixc = "FOURTEEN";
                                        break;
                                    case '5':
                                        matrixc = "FIFTEEN";
                                        break;
                                    case '6':
                                        matrixc = "SIXTEEN";
                                        break;
                                    case '7':
                                        matrixc = "SEVENTEEN";
                                        break;
                                    case '8':
                                        matrixc = "EIGHTEEN";
                                        break;
                                    case '9':
                                        matrixc = "NINETEEN";
                                        break;
                                    case '0':
                                        matrixc = "TEN";
                                        break;
                                }
                                break;
                            case '2':
                                matrixc = "TWENTY" + matrixc;
                                break;
                            case '3':
                                matrixc = "THIRTY" + matrixc;
                                break;
                            case '4':
                                matrixc = "FORTY" + matrixc;
                                break;
                            case '5':
                                matrixc = "FIFTY" + matrixc;
                                break;
                            case '6':
                                matrixc = "SIXTY" + matrixc;
                                break;
                            case '7':
                                matrixc = "SEVENTY" + matrixc;
                                break;
                            case '8':
                                matrixc = "EIGHTY" + matrixc;
                                break;
                            case '9':
                                matrixc = "NINETY" + matrixc;
                                break;
                        }
                        break;
                    case 2:
                        switch (number[dd])
                        {
                            case '1':
                                matrixc = "ONEHUNDRED" + matrixc;
                                break;
                            case '2':
                                matrixc = "TWOHUNDRED" + matrixc;
                                break;
                            case '3':
                                matrixc = "THREEHUNDRED" + matrixc;
                                break;
                            case '4':
                                matrixc = "FOURHUNDRED" + matrixc;
                                break;
                            case '5':
                                matrixc = "FIVEHUNDRED" + matrixc;
                                break;
                            case '6':
                                matrixc = "SIXHUNDRED" + matrixc;
                                break;
                            case '7':
                                matrixc = "SEVENHUNDRED" + matrixc;
                                break;
                            case '8':
                                matrixc = "EIGHTHUNDRED" + matrixc;
                                break;
                            case '9':
                                matrixc = "NINEHUNDRED" + matrixc;
                                break;
                        }
                        break;
                    case 3:
                        switch (number[dd])
                        {
                            case '1':
                                matrixc = "ONETHOUSAND" + matrixc;
                                break;
                            case '2':
                                matrixc = "TWOTHOUSAND" + matrixc;
                                break;
                            case '3':
                                matrixc = "THREETHOUSAND" + matrixc;
                                break;
                            case '4':
                                matrixc = "FOURTHOUSAND" + matrixc;
                                break;
                            case '5':
                                matrixc = "FIVETHOUSAND" + matrixc;
                                break;
                            case '6':
                                matrixc = "SIXTHOUSAND" + matrixc;
                                break;
                            case '7':
                                matrixc = "SEVENTHOUSAND" + matrixc;
                                break;
                            case '8':
                                matrixc = "EIGHTTHOUSAND" + matrixc;
                                break;
                            case '9':
                                matrixc = "NINETHOUSAND" + matrixc;
                                break;
                        }
                        break;
                }
                numberplace++;
            }
        }
        matrixc = matrixc.Replace('J', 'I');
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
        string kw2encrypt;
        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Orange Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);

            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Matrix A: {1}", moduleId, matrixa);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Matrix B: {1}", moduleId, matrixb);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Matrix C: {1}", moduleId, matrixc);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Matrix D: {1}", moduleId, matrixd);
            
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Begin Bazeries Encryption", moduleId);
            encrypt = BazeriesEnc(encrypt, matrixb, matrixc, number, invert);

            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Begin Foursquare Encryption", moduleId);
            encrypt = FoursquareEnc(encrypt, matrixa, matrixb, matrixc, matrixd, invert);

            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Begin ADFGX Encryption", moduleId);
            kw2encrypt = ADFGXEnc(keyword2.ToUpper(), matrixa, keyword1.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Encrypted Keyword: {1}", moduleId, kw2encrypt);
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Orange Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);

            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Matrix A: {1}", moduleId, matrixa);
            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Matrix B: {1}", moduleId, matrixb);
            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Matrix C: {1}", moduleId, matrixc);
            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Matrix D: {1}", moduleId, matrixd);

            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Begin Foursquare Encryption", moduleId);
            encrypt = FoursquareEnc(encrypt, matrixa, matrixb, matrixc, matrixd, invert);

            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Begin Bazeries Encryption", moduleId);
            encrypt = BazeriesEnc(encrypt, matrixb, matrixc, number, invert);

            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Begin ADFGX Encryption", moduleId);
            kw2encrypt = ADFGXEnc(keyword2.ToUpper(), matrixa, keyword1.ToUpper(), invert);
            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Encrypted Keyword: {1}", moduleId, kw2encrypt);
        }
        
        pages[page - 1][2] = kw2encrypt.Substring(0, kw2encrypt.Length / 2);
        pages[page][0] = kw2encrypt.Substring(kw2encrypt.Length / 2);


        string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < 6; aa++)
        {
            if (b[aa])
            {
                pages[page - 1][1] = pages[page - 1][1] + "" + encrypt[aa];
                encrypt = encrypt.Substring(0, aa) + "J" + encrypt.Substring(aa + 1);
            }
            else
            {
                pages[page - 1][1] = pages[page - 1][1] + "" + alpha[UnityEngine.Random.Range(0, 25)];
            }
        }
        return encrypt;
    }

    string FoursquareEnc(string word, string ma, string mb, string mc, string md, bool invert)
    {
        string encrypt = "";
        for (int gg = 0; gg < 6; gg++)
        {
            if(invert)
            {
                int n1 = mb.IndexOf(word[gg]);
                int n2 = mc.IndexOf(word[gg + 1]);
                gg++;
                encrypt = encrypt + "" + ma[((n1 / 5) * 5) + (n2 % 5)] + "" + md[(n1 % 5) + ((n2 / 5) * 5)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1} -> {2}", moduleId, word[gg - 1] + "" + word[gg], encrypt[gg - 1] + "" + encrypt[gg]);
            }
            else
            {
                int n1 = ma.IndexOf(word[gg]);
                int n2 = md.IndexOf(word[gg + 1]);
                gg++;
                encrypt = encrypt + "" + mb[((n1 / 5) * 5) + (n2 % 5)] + "" + mc[(n1 % 5) + ((n2 / 5) * 5)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1} -> {2}", moduleId, word[gg - 1] + "" + word[gg], encrypt[gg - 1] + "" + encrypt[gg]);
            }
        }
        return encrypt;
    }
    string BazeriesEnc(string word, string mb, string mc, string num, bool invert)
    {
        string encrypt = "";
        int n = 0;
        int subgroup = 0;
        for (int bb = 0; bb < num.Length; bb++)
            subgroup += (num[bb] - '0');
        subgroup = (subgroup % 4) + 2;
        for (int aa = 0; aa < word.Length; aa++)
        {
            char l;
            if(invert)
            {
                l = mb[mc.IndexOf(word[aa])];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1} -> {2}", moduleId, word[aa], l);
            }
            else
            {
                l = mc[mb.IndexOf(word[aa])];
                Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1} -> {2}", moduleId, word[aa], l);
            }
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
        if(invert)
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Subgroup Number: {1}", moduleId, subgroup);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1} -> {2}", moduleId, encrypt, encrypt2);
        }
        else
        {
            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Subgroup Number: {1}", moduleId, subgroup);
            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1} -> {2}", moduleId, encrypt, encrypt2);
        }
        

        return encrypt2;
    }
    string ADFGXEnc(string word, string key, string kw, bool invert)
    {
        string encrypt = "";
        string adfgx = "ADFGX";
        for (int aa = 0; aa < word.Length; aa++)
        {
            int num = key.IndexOf(word[aa]);
            encrypt = encrypt + "" + adfgx[num / 5] + "" + adfgx[num % 5];
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1} -> {2}", moduleId, word[aa], encrypt[aa * 2] + "" + encrypt[(aa * 2) + 1]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1} -> {2}", moduleId, word[aa], encrypt[aa * 2] + "" + encrypt[(aa * 2) + 1]);
        }
        int[] numrows = new int[kw.Length];

        for (int bb = 0; bb < kw.Length; bb++)
        {
            numrows[bb] = encrypt.Length / kw.Length;
            if (bb < (encrypt.Length % kw.Length))
                numrows[bb]++;
        }
        char[][] letters = new char[numrows[0]][];
        for (int cc = 0; cc < letters.Length; cc++)
        {
            letters[cc] = new char[kw.Length];
        }
        int cursor = 0;
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int dd = 0; dd < alpha.Length; dd++)
        {
            for (int ee = 0; ee < kw.Length; ee++)
            {
                if (kw[ee].ToString().IndexOf(alpha[dd]) >= 0)
                {
                    string templog = "";
                    for (int ff = 0; ff < numrows[ee]; ff++)
                    {
                        letters[ff][ee] = encrypt[cursor];
                        templog = templog + "" + encrypt[cursor];
                        cursor++;
                    }
                    if(invert)
                        Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1}: {2}", moduleId, kw[ee], templog);
                    else
                        Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1}: {2}", moduleId, kw[ee], templog);
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
            if (col >= letters[row].Length)
            {
                col = 0;
                row++;
                logoutput = logoutput + "\n";
            }
        }
        if(invert)
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1}", moduleId, logoutput);
        else
            Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1}", moduleId, logoutput);
        return encrypt;
    }
    string redcipher(string word, bool invert)
    {
        fontsizes[page][0] = 40;
        fontsizes[page][1] = 40;
        fontsizes[page][2] = 40;
        fontsizes[page - 1][0] = 40;
        fontsizes[page - 1][1] = 40;
        fontsizes[page - 1][2] = 40;
        
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
            words[bb] = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
            bool flag = false;
            for (int cc = 0; cc < bb; cc++)
            {
                if (words[bb].EqualsIgnoreCase(words[cc]))
                {
                    flag = true;
                    break;
                }
            }
            while (flag)
            {
                words[bb] = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
                flag = false;
                for (int cc = 0; cc < bb; cc++)
                {
                    if (words[bb].EqualsIgnoreCase(words[cc]))
                    {
                        flag = true;
                        break;
                    }
                }
            }

        }
        pages[page][0] = words[0].ToUpper();
        pages[page][1] = words[1].ToUpper();
        pages[page][2] = words[2].ToUpper();

        string kw1;
        string kw2;
        string kw3;
        int numports = Bomb.GetPortCount();
        if (numports == 0)
        {
            kw1 = words[0].Replace('J', 'I');
            kw2 = words[1].Replace('J', 'I');
            kw3 = words[2].Replace('J', 'I');
        }
        else if (numports == 1)
        {
            kw1 = words[0].Replace('J', 'I');
            kw2 = words[2].Replace('J', 'I');
            kw3 = words[1].Replace('J', 'I');
        }
        else if (numports == 2)
        {
            kw1 = words[1].Replace('J', 'I');
            kw2 = words[0].Replace('J', 'I');
            kw3 = words[2].Replace('J', 'I');
        }
        else if (numports == 3)
        {
            kw1 = words[1].Replace('J', 'I');
            kw2 = words[2].Replace('J', 'I');
            kw3 = words[0].Replace('J', 'I');
        }
        else if (numports == 4)
        {
            kw1 = words[2].Replace('J', 'I');
            kw2 = words[0].Replace('J', 'I');
            kw3 = words[1].Replace('J', 'I');
        }
        else
        {
            kw1 = words[2].Replace('J', 'I');
            kw2 = words[1].Replace('J', 'I');
            kw3 = words[0].Replace('J', 'I');
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
        kw1 = getKey(kw1, "ABCDEFGHIKLMNOPQRSTUVWXYZ", (snnums[0] - '0') % 2 == 1);
        kw2 = getKey(kw2, "ABCDEFGHIKLMNOPQRSTUVWXYZ", (snnums[1] - '0') % 2 == 0);
        kw3 = getKey(kw3, "ABCDEFGHIKLMNOPQRSTUVWXYZ", Bomb.GetSerialNumberNumbers().Last() % 2 == 1);


        if (invert)
        {
            chosentextcolors[page] = Color.black;
            chosenscreencolors[page] = screencolors[1];
            chosentextcolors[page - 1] = Color.black;
            chosenscreencolors[page - 1] = screencolors[1];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Red Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Playfair Key: {1}", moduleId, kw1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Begin Playfair Encryption", moduleId);
            encrypt = PlayfairEnc(encrypt, kw1, invert);

            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] CM Bifid Key 1: {1}", moduleId, kw1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] CM Bifid Key 2: {1}", moduleId, kw2);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Begin CM Bifid Encryption", moduleId);
            encrypt = CMBifidEnc(encrypt, kw1, kw2, invert);

            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Trisquare Key 1: {1}", moduleId, kw1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Trisquare Key 2: {1}", moduleId, kw2);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Trisquare Key 3: {1}", moduleId, kw3);
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Begin Trisquare Encryption", moduleId);
            encrypt = TrisquareEnc(encrypt, kw1, kw2, kw3, invert);
        }
        else
        {
            chosentextcolors[page] = Color.white;
            chosenscreencolors[page] = screencolors[0];
            chosentextcolors[page - 1] = Color.white;
            chosenscreencolors[page - 1] = screencolors[0];
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] Begin Red Cipher", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Trisquare Key 1: {1}", moduleId, kw1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Trisquare Key 2: {1}", moduleId, kw2);
            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Trisquare Key 3: {1}", moduleId, kw3);
            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Begin Trisquare Encryption", moduleId);
            encrypt = TrisquareEnc(encrypt, kw1, kw2, kw3, invert);

            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] CM Bifid Key 1: {1}", moduleId, kw1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] CM Bifid Key 2: {1}", moduleId, kw2);
            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Begin CM Bifid Encryption", moduleId);
            encrypt = CMBifidEnc(encrypt, kw1, kw2, invert);

            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Playfair Key: {1}", moduleId, kw1);
            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Begin Playfair Encryption", moduleId);
            encrypt = PlayfairEnc(encrypt, kw1, invert);
        }
        

        string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < 6; aa++)
        {
            if (b[aa])
            {
                pages[page - 1][1] = pages[page - 1][1] + "" + encrypt[aa];
                encrypt = encrypt.Substring(0, aa) + "J" + encrypt.Substring(aa + 1);
            }
            else
            {
                pages[page - 1][1] = pages[page - 1][1] + "" + alpha[UnityEngine.Random.Range(0, alpha.Length)];
            }
        }

        return encrypt;
    }
    string CMBifidEnc(string word, string kw1, string kw2, bool invert)
    {
        string encrypt = "";
        string letters = word.ToUpper();
        if (invert)
        {
            string nums = "";
            for (int hh = 0; hh < 6; hh++)
            {
                int n1 = kw2.IndexOf(word[hh]);
                nums = nums + "" + (n1 / 5);
                nums = nums + "" + (n1 % 5);
            }
            
            Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Rows|Columns: {1}", moduleId, nums);
            for (int ii = 0; ii < 6; ii++)
            {
                encrypt = encrypt + "" + kw1[((nums[ii] - '0') * 5) + (nums[(ii + 6)] - '0')];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] {1} -> {2} -> {3}", moduleId, letters[ii], nums[ii] + "" + nums[ii + 6], encrypt[ii]);
            }
        }
        else
        {
            string rows = "";
            string cols = "";
            for (int hh = 0; hh < 6; hh++)
            {
                int n1 = kw1.IndexOf(word[hh]);
                rows = rows + "" + (n1 / 5);
                cols = cols + "" + (n1 % 5);
            }
            string nums = rows + "" + cols;
            Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Rows|Columns: {1}", moduleId, rows + "|" + cols);

            for (int ii = 0; ii < 6; ii++)
            {
                encrypt = encrypt + "" + kw2[((nums[ii * 2] - '0') * 5) + (nums[(ii * 2) + 1] - '0')];
                Debug.LogFormat("[Ultimate Cipher #{0}] [RED] {1} -> {2} -> {3}", moduleId, letters[ii], nums[ii * 2] + "" + nums[(ii * 2) + 1],encrypt[ii]);
            }
        }

        
        return encrypt;
    }
    string TrisquareEnc(string word, string kw1, string kw2, string kw3, bool invert)
    {
        string encrypt = "";
        for (int gg = 0; gg < 6; gg++)
        {
            if(invert)
            {
                int n1 = kw3.IndexOf(word[gg]);
                gg++;
                int n2 = kw3.IndexOf(word[gg]);
                encrypt = encrypt + "" + kw1[((n1 / 5) * 5) + (n2 % 5)] + "" + kw2[(n1 % 5) + ((n2 / 5) * 5)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] {1} -> {2}", moduleId, word[gg - 1] + "" + word[gg], encrypt[gg - 1] + "" + encrypt[gg]);
            }
            else
            {
                int n1 = kw1.IndexOf(word[gg]);
                int n2 = kw2.IndexOf(word[gg + 1]);
                gg++;
                encrypt = encrypt + "" + kw3[((n1 / 5) * 5) + (n2 % 5)] + "" + kw3[(n1 % 5) + ((n2 / 5) * 5)];
                Debug.LogFormat("[Ultimate Cipher #{0}] [RED] {1} -> {2}", moduleId, word[gg - 1] + "" + word[gg], encrypt[gg - 1] + "" + encrypt[gg]);
            }
            
        }
        return encrypt;
    }
    string PlayfairEnc(string word, string key, bool invert)
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
                if(invert)
                {
                    col1 = correction(col1 - 1, 5);
                    col2 = correction(col2 - 1, 5);
                }
                else
                {
                    col1 = correction(col1 + 1, 5);
                    col2 = correction(col2 + 1, 5);
                }
            }
            else if (col1 == col2)
            {
                if(invert)
                {
                    row1 = correction(row1 - 1, 5);
                    row2 = correction(row2 - 1, 5);
                }
                else
                {
                    row1 = correction(row1 + 1, 5);
                    row2 = correction(row2 + 1, 5);
                }
            }
            else
            {
                int col3 = col1;
                col1 = col2;
                col2 = col3;
            }
            encrypt = encrypt + "" + matrix[row1][col1] + "" + matrix[row2][col2];
            if(invert)
                Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] {1} -> {2}", moduleId, word[ee - 1] + "" + word[ee], encrypt[ee - 1] + "" + encrypt[ee]);
            else
                Debug.LogFormat("[Ultimate Cipher #{0}] [RED] {1} -> {2}", moduleId, word[ee - 1] + "" + word[ee], encrypt[ee - 1] + "" + encrypt[ee]);
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
        if(!moduleSolved)
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
        if(!moduleSolved)
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

        screenTexts[0].fontSize = fontsizes[page][0];
        screenTexts[1].fontSize = fontsizes[page][1];
        screenTexts[2].fontSize = fontsizes[page][2];

        screenTexts[0].color = chosentextcolors[page];
        screenTexts[1].color = chosentextcolors[page];
        screenTexts[2].color = chosentextcolors[page];

        screens[0].material = chosenscreencolors[page];
        screens[1].material = chosenscreencolors[page];
        screens[2].material = chosenscreencolors[page];
        background.material = chosenbackgroundcolors[page];
        
            

    }
    void submitWord(KMSelectable submitButton)
    {
        if(!moduleSolved)
        {
            submitButton.AddInteractionPunch();
            if (screenTexts[2].text.Equals("PINKUC"))
            {
                screenTexts[0].color = Color.white;
                screenTexts[1].color = Color.white;
                screenTexts[2].color = Color.white;

                screens[0].material = screencolors[0];
                screens[1].material = screencolors[0];
                screens[2].material = screencolors[0];
                background.material = backgroundcolors[11];

                screenTexts[2].text = "";
            }
            else if (screenTexts[2].text.Equals("CYANUC"))
            {
                screenTexts[0].color = Color.black;
                screenTexts[1].color = Color.black;
                screenTexts[2].color = Color.black;

                screens[0].material = screencolors[1];
                screens[1].material = screencolors[1];
                screens[2].material = screencolors[1];
                background.material = backgroundcolors[12];

                screenTexts[2].text = "";
            }
            else if (screenTexts[2].text.Equals("PRISSY"))
            {
                Audio.PlaySoundAtTransform(sounds[4].name, transform);
            }
            else if (screenTexts[2].text.Equals(answer))
            {
                background.material = chosenbackgroundcolors[0];
                screens[0].material = chosenscreencolors[0];
                screens[1].material = chosenscreencolors[0];
                screens[2].material = chosenscreencolors[0];
                Audio.PlaySoundAtTransform(sounds[2].name, transform);
                module.HandlePass();
                moduleSolved = true;
                screenTexts[0].text = "PINKUC";
                screenTexts[1].text = "CYANUC";
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
        if(!moduleSolved)
        {
            screenTexts[0].fontSize = 40;
            screenTexts[1].fontSize = 40;
            screenTexts[2].fontSize = 40;
            pressed.AddInteractionPunch();
            Audio.PlaySoundAtTransform(sounds[1].name, transform);
            if (submitScreen)
            {
                if(screenTexts[2].text.Length < 6)
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
                submitScreen = true;
            }
        }
    }
#pragma warning disable 414
    private string TwitchHelpMessage = "Move to other screens using !{0} right|left|r|l|. Submit the decrypted word with !{0} submit qwertyuiopasdfghjklzxcvbnm. Temp Manual: http://66.195.8.98/~obachs971/Ultimate%20Cipher/UltimateCipher.html";
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

    private int getPositionFromChar(char c)
    {
        return "QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(c);
    }
}
