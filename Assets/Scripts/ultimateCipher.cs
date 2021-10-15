using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KModkit;
using UnityEngine;
using Words;
using Rnd = UnityEngine.Random;
public class ultimateCipher : MonoBehaviour
{
	private int numPCpages = 27;
	private int numTRUpages = 53;
	private List<List<string>> wordList = new List<List<string>>();
	public TextMesh[] screenTexts;
	public KMBombInfo Bomb;
	public KMBombModule module;
	public AudioClip[] sounds;
	public KMAudio Audio;
	public TextMesh submitText;
	public TextMesh[] arrowText;
	private bool zenMode;
	private bool ZenModeActive;
	private int TPPoints;
	private int UcipherPoints;
	private string[][] pages;
	private string[][] arrowLetters;
	private Color[] chosentextcolors;
	private Material[] chosenscreencolors;
	private int[][] fontsizes;
	private Material[] chosenbackgroundcolors;
	private string answer;
	private int page;
	private bool submitScreen;
	private static int moduleIdCounter = 1;
	private int moduleId;
	private bool moduleSolved;
	public KMSelectable leftArrow;
	public KMSelectable rightArrow;
	public KMSelectable submit;
	public KMSelectable[] keyboard;
	public MeshRenderer background;
	public Material[] backgroundcolors;
	public Material[] screencolors;
	public MeshRenderer[] screens;
	public MeshRenderer[] screentextmat;
	public Font[] fonts;
	public Material[] fontmat;
	private int numpages;
	private Color[] ultchosentextcolors;
	private Material[] ultchosenscreencolors;
	private int[][] ultfontsizes;
	private Material[] ultchosenbackgroundcolors;
	private string[][] ultpages;
	private string ultanswer;
	private string[][] ultarrowLetters;
	private string pinkanswer;
	private int[][] pinkfontsizes;
	private Material[] pinkchosenbackgroundcolors;
	private string[][] pinkpages;
	private Color[] pinkchosentextcolors;
	private Material[] pinkchosenscreencolors;
	private string[][] pinkarrowLetters;
	private string cyananswer;
	private int[][] cyanfontsizes;
	private Material[] cyanchosenbackgroundcolors;
	private string[][] cyanpages;
	private Color[] cyanchosentextcolors;
	private Material[] cyanchosenscreencolors;
	private string[][] cyanarrowLetters;
	private string trueanswer;
	private int[][] truefontsizes;
	private Material[] truechosenbackgroundcolors;
	private string[][] truepages;
	private Color[] truechosentextcolors;
	private Material[] truechosenscreencolors;
	private string[][] truearrowLetters;
	private bool pinkuc;
	private bool pinkcalc;
	private bool cyanuc;
	private bool cyancalc;
	private bool trueuc;
	private bool truecalc;
	private static bool playing;
	private void Awake()
	{
		moduleId = ultimateCipher.moduleIdCounter++;
		Data data = new Data();
		wordList = data.allWords;
		wordList[2].Remove("CANCEL");
		wordList[2].Remove("PRISSY");
		KMSelectable kmselectable = leftArrow;
		kmselectable.OnInteract = (KMSelectable.OnInteractHandler)Delegate.Combine(kmselectable.OnInteract, new KMSelectable.OnInteractHandler(delegate ()
		{
			left(leftArrow);
			return false;
		}));
		KMSelectable kmselectable2 = rightArrow;
		kmselectable2.OnInteract = (KMSelectable.OnInteractHandler)Delegate.Combine(kmselectable2.OnInteract, new KMSelectable.OnInteractHandler(delegate ()
		{
			right(rightArrow);
			return false;
		}));
		KMSelectable kmselectable3 = submit;
		kmselectable3.OnInteract = (KMSelectable.OnInteractHandler)Delegate.Combine(kmselectable3.OnInteract, new KMSelectable.OnInteractHandler(delegate ()
		{
			submitWord(submit);
			return false;
		}));
		KMSelectable[] array = keyboard;
		for (int i = 0; i < array.Length; i++)
		{
			KMSelectable pressedButton3 = array[i];
			KMSelectable pressedButton = pressedButton3;
			KMSelectable pressedButton2 = pressedButton;
			pressedButton2.OnInteract = (KMSelectable.OnInteractHandler)Delegate.Combine(pressedButton2.OnInteract, new KMSelectable.OnInteractHandler(delegate ()
			{
				letterPress(pressedButton);
				return false;
			}));
		}
		playing = false;
	}
	private void Start()
	{
		UcipherPoints = 0;
		pinkuc = false;
		pinkcalc = false;
		cyanuc = false;
		cyancalc = false;
		numpages = 7;
		submitText.text = "1";
		ultanswer = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
		wordList[2].Remove(ultanswer.ToUpperInvariant());
		answer = ultanswer.ToUpperInvariant();
		Debug.LogFormat("[Ultimate Cipher #{0}] Generated Word: {1}", new object[]
		{
			moduleId,
			answer
		});
		chosenscreencolors = new Material[7];
		chosentextcolors = new Color[7];
		fontsizes = new int[7][];
		chosenbackgroundcolors = new Material[7];
		pages = new string[7][];
		for (int i = 0; i < pages.Length; i++)
		{
			pages[i] = new string[3];
			pages[i][0] = string.Empty;
			pages[i][1] = string.Empty;
			pages[i][2] = string.Empty;
			fontsizes[i] = new int[3];
		}
		ultpages = new string[7][];
		ultfontsizes = new int[7][];
		ultchosenbackgroundcolors = new Material[7];
		ultchosenscreencolors = new Material[7];
		ultchosentextcolors = new Color[7];
		ultarrowLetters = new string[7][];
		string text = ultimatecipher(answer);
		pages[0][0] = text.ToUpperInvariant();
		page = 0;
		for (int j = 0; j < 7; j++)
		{
			ultpages[j] = new string[3];
			ultfontsizes[j] = new int[3];
			ultarrowLetters[j] = new string[2];
			ultpages[j][0] = pages[j][0].ToUpperInvariant();
			ultpages[j][1] = pages[j][1].ToUpperInvariant();
			ultpages[j][2] = pages[j][2].ToUpperInvariant();
			ultfontsizes[j][0] = fontsizes[j][0];
			ultfontsizes[j][1] = fontsizes[j][1];
			ultfontsizes[j][2] = fontsizes[j][2];
			ultchosenbackgroundcolors[j] = chosenbackgroundcolors[j];
			ultchosenscreencolors[j] = chosenscreencolors[j];
			ultchosentextcolors[j] = chosentextcolors[j];
			ultarrowLetters[j][0] = arrowLetters[j][0].ToUpperInvariant();
			ultarrowLetters[j][1] = arrowLetters[j][1].ToUpperInvariant();
		}
		TPPoints = UcipherPoints;
		zenMode = ZenModeActive;
		getScreens();
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000E444 File Offset: 0x0000C844
	private void pinkcipher()
	{
		if (!pinkcalc)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Pink Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			pinkcalc = true;
			pinkanswer = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
			wordList[2].Remove(pinkanswer.ToUpperInvariant());
			Debug.LogFormat("[Ultimate Cipher #{0}] Pink Generated Word: {1}", new object[]
			{
				moduleId,
				pinkanswer
			});
			pages = new string[numPCpages][];
			chosenbackgroundcolors = new Material[numPCpages];
			fontsizes = new int[numPCpages][];
			arrowLetters = new string[numPCpages][];
			for (int i = 0; i < numPCpages; i++)
			{
				pages[i] = new string[3];
				fontsizes[i] = new int[3];
			}
			chosenscreencolors = new Material[numPCpages];
			chosentextcolors = new Color[numPCpages];
			fontsizes[0][0] = 44;
			fontsizes[0][1] = 40;
			fontsizes[0][2] = 40;
			List<int> list = new List<int>
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13
			};
			chosenbackgroundcolors[0] = backgroundcolors[backgroundcolors.Length - 2];
			chosentextcolors[0] = Color.white;
			chosenscreencolors[0] = screencolors[0];
			page = numPCpages - 1;
			string text = pinkanswer.ToUpperInvariant();
			for (int j = list.Count - 1; j >= 0; j--)
			{
				int num = list[UnityEngine.Random.Range(0, list.Count)];
				list.Remove(num);

				chosenbackgroundcolors[j * 2 + 1] = backgroundcolors[num];
				chosenbackgroundcolors[j * 2 + 2] = chosenbackgroundcolors[j * 2 + 1];
				arrowLetters[page] = new string[2];
				arrowLetters[page - 1] = new string[2];
				arrowLetters[page][0] = "<";
				arrowLetters[page][1] = ">";
				arrowLetters[page - 1][0] = "<";
				arrowLetters[page - 1][1] = ">";
				switch (num)
				{
					case 1:
						text = redcipher(text.ToUpperInvariant(), false);
						break;
					case 2:
						text = orangecipher(text.ToUpperInvariant(), false);
						break;
					case 3:
						text = yellowcipher(text.ToUpperInvariant(), false);
						break;
					case 4:
						text = greencipher(text.ToUpperInvariant(), false);
						break;
					case 5:
						text = bluecipher(text.ToUpperInvariant(), false);
						break;
					case 6:
						text = indigocipher(text.ToUpperInvariant(), false);
						break;
					case 7:
						text = violetcipher(text.ToUpperInvariant(), false);
						break;
					case 8:
						text = whitecipher(text.ToUpperInvariant(), false);
						break;
					case 9:
						text = graycipher(text.ToUpperInvariant(), false);
						break;
					case 10:
						text = blackcipher(text.ToUpperInvariant(), false);
						break;
					case 11:
						text = browncipher(text.ToUpperInvariant(), false);
						break;
					case 12:
						text = marooncipher(text.ToUpperInvariant(), false);
						break;
					case 13:
						text = cornflowercipher(text.ToUpperInvariant(), false);
						break;
				}
				page -= 2;
			}
			arrowLetters[0] = new string[2];
			arrowLetters[0][0] = "<";
			arrowLetters[0][1] = ">";
			pages[0][0] = text.ToUpperInvariant();
			pinkfontsizes = new int[numPCpages][];
			pinkpages = new string[numPCpages][];
			pinkchosenbackgroundcolors = new Material[numPCpages];
			pinkchosenscreencolors = new Material[numPCpages];
			pinkchosentextcolors = new Color[numPCpages];
			pinkarrowLetters = new string[numPCpages][];
			for (int k = 0; k < numPCpages; k++)
			{
				pinkpages[k] = new string[3];
				pinkfontsizes[k] = new int[3];
				pinkarrowLetters[k] = new string[2];
				pinkpages[k][0] = pages[k][0];
				pinkpages[k][1] = pages[k][1];
				pinkpages[k][2] = pages[k][2];
				pinkfontsizes[k][0] = fontsizes[k][0];
				pinkfontsizes[k][1] = fontsizes[k][1];
				pinkfontsizes[k][2] = fontsizes[k][2];
				pinkchosenbackgroundcolors[k] = chosenbackgroundcolors[k];
				pinkchosenscreencolors[k] = chosenscreencolors[k];
				pinkchosentextcolors[k] = chosentextcolors[k];
				pinkarrowLetters[k][0] = arrowLetters[k][0].ToUpperInvariant();
				pinkarrowLetters[k][1] = arrowLetters[k][1].ToUpperInvariant();
			}
		}
		TPPoints = 174;
		answer = pinkanswer.ToUpperInvariant();
		page = 0;
		getScreens();
	}

	// Token: 0x06000024 RID: 36 RVA: 0x0000EA4C File Offset: 0x0000CE4C
	private void cyancipher()
	{
		if (!cyancalc)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Cyan Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			cyancalc = true;
			cyananswer = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
			wordList[2].Remove(cyananswer.ToUpperInvariant());
			Debug.LogFormat("[Ultimate Cipher #{0}] Cyan Generated Word: {1}", new object[]
			{
				moduleId,
				cyananswer
			});
			pages = new string[numPCpages][];
			chosenbackgroundcolors = new Material[numPCpages];
			fontsizes = new int[numPCpages][];
			arrowLetters = new string[numPCpages][];
			for (int i = 0; i < numPCpages; i++)
			{
				pages[i] = new string[3];
				fontsizes[i] = new int[3];
			}
			chosenscreencolors = new Material[numPCpages];
			chosentextcolors = new Color[numPCpages];
			fontsizes[0][0] = 44;
			fontsizes[0][1] = 40;
			fontsizes[0][2] = 40;
			List<int> list = new List<int>
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13
			};
			chosenbackgroundcolors[0] = backgroundcolors[backgroundcolors.Length - 1];
			chosentextcolors[0] = Color.black;
			chosenscreencolors[0] = screencolors[1];
			page = numPCpages - 1;
			string text = cyananswer.ToUpperInvariant();
			for (int j = list.Count - 1; j >= 0; j--)
			{
				int num = list[UnityEngine.Random.Range(0, list.Count)];
				list.Remove(num);
				chosenbackgroundcolors[j * 2 + 1] = backgroundcolors[num];
				chosenbackgroundcolors[j * 2 + 2] = chosenbackgroundcolors[j * 2 + 1];
				arrowLetters[page] = new string[2];
				arrowLetters[page - 1] = new string[2];
				arrowLetters[page][0] = "<";
				arrowLetters[page][1] = ">";
				arrowLetters[page - 1][0] = "<";
				arrowLetters[page - 1][1] = ">";
				switch (num)
				{
					case 1:
						text = redcipher(text.ToUpperInvariant(), true);
						break;
					case 2:
						text = orangecipher(text.ToUpperInvariant(), true);
						break;
					case 3:
						text = yellowcipher(text.ToUpperInvariant(), true);
						break;
					case 4:
						text = greencipher(text.ToUpperInvariant(), true);
						break;
					case 5:
						text = bluecipher(text.ToUpperInvariant(), true);
						break;
					case 6:
						text = indigocipher(text.ToUpperInvariant(), true);
						break;
					case 7:
						text = violetcipher(text.ToUpperInvariant(), true);
						break;
					case 8:
						text = whitecipher(text.ToUpperInvariant(), true);
						break;
					case 9:
						text = graycipher(text.ToUpperInvariant(), true);
						break;
					case 10:
						text = blackcipher(text.ToUpperInvariant(), true);
						break;
					case 11:
						text = browncipher(text.ToUpperInvariant(), true);
						break;
					case 12:
						text = marooncipher(text.ToUpperInvariant(), true);
						break;
					case 13:
						text = cornflowercipher(text.ToUpperInvariant(), true);
						break;
				}
				page -= 2;
			}
			arrowLetters[0] = new string[2];
			arrowLetters[0][0] = "<";
			arrowLetters[0][1] = ">";
			pages[0][0] = text.ToUpperInvariant();
			cyanfontsizes = new int[numPCpages][];
			cyanpages = new string[numPCpages][];
			cyanchosenbackgroundcolors = new Material[numPCpages];
			cyanchosenscreencolors = new Material[numPCpages];
			cyanchosentextcolors = new Color[numPCpages];
			cyanarrowLetters = new string[numPCpages][];
			for (int k = 0; k < numPCpages; k++)
			{
				cyanpages[k] = new string[3];
				cyanfontsizes[k] = new int[3];
				cyanarrowLetters[k] = new string[2];
				cyanpages[k][0] = pages[k][0];
				cyanpages[k][1] = pages[k][1];
				cyanpages[k][2] = pages[k][2];
				cyanfontsizes[k][0] = fontsizes[k][0];
				cyanfontsizes[k][1] = fontsizes[k][1];
				cyanfontsizes[k][2] = fontsizes[k][2];
				cyanchosenbackgroundcolors[k] = chosenbackgroundcolors[k];
				cyanchosenscreencolors[k] = chosenscreencolors[k];
				cyanchosentextcolors[k] = chosentextcolors[k];
				cyanarrowLetters[k][0] = arrowLetters[k][0].ToUpperInvariant();
				cyanarrowLetters[k][1] = arrowLetters[k][1].ToUpperInvariant();
			}
		}
		TPPoints = 174;
		answer = cyananswer.ToUpperInvariant();
		page = 0;
		getScreens();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000F054 File Offset: 0x0000D454
	private void truecipher()
	{
		if (!truecalc)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin True Ultimate Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			truecalc = true;
			trueanswer = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
			wordList[2].Remove(trueanswer.ToUpperInvariant());
			Debug.LogFormat("[Ultimate Cipher #{0}] True UC Generated Word: {1}", new object[]
			{
				moduleId,
				trueanswer
			});
			pages = new string[numTRUpages][];
			chosenbackgroundcolors = new Material[numTRUpages];
			fontsizes = new int[numTRUpages][];
			arrowLetters = new string[numTRUpages][];
			for (int i = 0; i < numTRUpages; i++)
			{
				pages[i] = new string[3];
				fontsizes[i] = new int[3];
			}
			chosenscreencolors = new Material[numTRUpages];
			chosentextcolors = new Color[numTRUpages];
			fontsizes[0][0] = 44;
			fontsizes[0][1] = 40;
			fontsizes[0][2] = 40;
			List<int> list = new List<int>
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				-1,
				-2,
				-3,
				-4,
				-5,
				-6,
				-7,
				-8,
				-9,
				-10,
				-11,
				-12,
				-13
			};
			chosenbackgroundcolors[0] = backgroundcolors[0];
			chosentextcolors[0] = Color.white;
			chosenscreencolors[0] = screencolors[0];
			page = numTRUpages - 1;
			string text = trueanswer.ToUpperInvariant();
			for (int j = list.Count - 1; j >= 0; j--)
			{
				int num = list[UnityEngine.Random.Range(0, list.Count)];
				list.Remove(num);
				bool flag = num < 0;
				if (flag)
				{
					num *= -1;
				}
				chosenbackgroundcolors[j * 2 + 1] = backgroundcolors[num];
				chosenbackgroundcolors[j * 2 + 2] = chosenbackgroundcolors[j * 2 + 1];
				arrowLetters[page] = new string[2];
				arrowLetters[page - 1] = new string[2];
				arrowLetters[page][0] = "<";
				arrowLetters[page][1] = ">";
				arrowLetters[page - 1][0] = "<";
				arrowLetters[page - 1][1] = ">";
				switch (num)
				{
					case 1:
						text = redcipher(text.ToUpperInvariant(), flag);
						break;
					case 2:
						text = orangecipher(text.ToUpperInvariant(), flag);
						break;
					case 3:
						text = yellowcipher(text.ToUpperInvariant(), flag);
						break;
					case 4:
						text = greencipher(text.ToUpperInvariant(), flag);
						break;
					case 5:
						text = bluecipher(text.ToUpperInvariant(), flag);
						break;
					case 6:
						text = indigocipher(text.ToUpperInvariant(), flag);
						break;
					case 7:
						text = violetcipher(text.ToUpperInvariant(), flag);
						break;
					case 8:
						text = whitecipher(text.ToUpperInvariant(), flag);
						break;
					case 9:
						text = graycipher(text.ToUpperInvariant(), flag);
						break;
					case 10:
						text = blackcipher(text.ToUpperInvariant(), flag);
						break;
					case 11:
						text = browncipher(text.ToUpperInvariant(), flag);
						break;
					case 12:
						text = marooncipher(text.ToUpperInvariant(), flag);
						break;
				}
				page -= 2;
			}
			arrowLetters[0] = new string[2];
			arrowLetters[0][0] = "<";
			arrowLetters[0][1] = ">";
			pages[0][0] = text.ToUpperInvariant();
			truefontsizes = new int[numTRUpages][];
			truepages = new string[numTRUpages][];
			truechosenbackgroundcolors = new Material[numTRUpages];
			truechosenscreencolors = new Material[numTRUpages];
			truechosentextcolors = new Color[numTRUpages];
			truearrowLetters = new string[numTRUpages][];
			for (int k = 0; k < numTRUpages; k++)
			{
				truepages[k] = new string[3];
				truefontsizes[k] = new int[3];
				truearrowLetters[k] = new string[2];
				truepages[k][0] = pages[k][0];
				truepages[k][1] = pages[k][1];
				truepages[k][2] = pages[k][2];
				truefontsizes[k][0] = fontsizes[k][0];
				truefontsizes[k][1] = fontsizes[k][1];
				truefontsizes[k][2] = fontsizes[k][2];
				truechosenbackgroundcolors[k] = chosenbackgroundcolors[k];
				truechosenscreencolors[k] = chosenscreencolors[k];
				truechosentextcolors[k] = chosentextcolors[k];
				truearrowLetters[k][0] = arrowLetters[k][0].ToUpperInvariant();
				truearrowLetters[k][1] = arrowLetters[k][1].ToUpperInvariant();
			}
		}
		TPPoints = 348;
		screenTexts[0].text = string.Empty;
		screenTexts[0].font = fonts[0];
		screentextmat[0].material = fontmat[0];
		base.StartCoroutine(loop());
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000F6FC File Offset: 0x0000DAFC
	private string ultimatecipher(string word)
	{
		fontsizes[0][0] = 44;
		fontsizes[0][1] = 40;
		fontsizes[0][2] = 40;
		arrowLetters = new string[7][];
		List<int> list = new List<int>
		{
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13
		};
		chosenbackgroundcolors[0] = backgroundcolors[0];
		chosentextcolors[0] = Color.white;
		chosenscreencolors[0] = screencolors[0];
		page = 6;
		string text = word.ToUpperInvariant();
		for (int i = 2; i >= 0; i--)
		{
			int num = list[UnityEngine.Random.Range(0, list.Count)];
			list.Remove(num);
			//num = 13;
			chosenbackgroundcolors[i * 2 + 1] = backgroundcolors[num];
			chosenbackgroundcolors[i * 2 + 2] = chosenbackgroundcolors[i * 2 + 1];
			arrowLetters[page] = new string[2];
			arrowLetters[page - 1] = new string[2];
			arrowLetters[page][0] = "<";
			arrowLetters[page][1] = ">";
			arrowLetters[page - 1][0] = "<";
			arrowLetters[page - 1][1] = ">";
			switch (num)
			{
				case 1:
					text = redcipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 14;
					break;
				case 2:
					text = orangecipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 17;
					break;
				case 3:
					text = yellowcipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 16;
					break;
				case 4:
					text = greencipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 10;
					break;
				case 5:
					text = bluecipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 10;
					break;
				case 6:
					text = indigocipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 17;
					break;
				case 7:
					text = violetcipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 14;
					break;
				case 8:
					text = whitecipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 14;
					break;
				case 9:
					text = graycipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 13;
					break;
				case 10:
					text = blackcipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 16;
					break;
				case 11:
					text = browncipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 17;
					break;
				case 12:
					text = marooncipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 16;
					break;
				case 13:
					text = cornflowercipher(text.ToUpperInvariant(), UnityEngine.Random.Range(0, 2) == 0);
					UcipherPoints += 16;
					break;
			}
			page -= 2;
		}
		arrowLetters[0] = new string[2];
		arrowLetters[0][0] = "<";
		arrowLetters[0][1] = ">";
		return text;
	}
	string cornflowercipher(string word, bool invert)
	{
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 40;
		fontsizes[page - 1][2] = 40;
		fontsizes[page][0] = 40;
		fontsizes[page][1] = 35;
		fontsizes[page][2] = 35;
		var bitsInt = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumber().First());
		var bits = Enumerable.Range(0, 5).Select(bit => (bitsInt & (1 << bit)) != 0).ToArray();
		string encrypt;
		if(invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Cornflower Cipher", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			// Stunted Blind Polybius Cipher
			string[] temp = StuntedBlindPolybiusEnc(word, bits, invert);

			// Chain Rotation Cipher
			var chainRotationN = Rnd.Range(1, 10);
			encrypt = ChainRotationEnc(temp[0], chainRotationN, invert);
			

			// Straddling Checkerboard Cipher
			var kw1 = pickWord(4, 8);
			var kw2 = pickWord(4, 8);
			var encryptKW3 = StraddlingCheckerboardEnc(temp[1], kw1, kw2, bits, invert);

			pages[page - 1][1] = chainRotationN.ToString();
			pages[page - 1][2] = encryptKW3.Substring(0, encryptKW3.Length / 2);
			pages[page][0] = encryptKW3.Substring(encryptKW3.Length / 2);
			pages[page][1] = kw1;
			pages[page][2] = kw2;
		}
		else
		{
			chosentextcolors[page] = Color.white;
			chosenscreencolors[page] = screencolors[0];
			chosentextcolors[page - 1] = Color.white;
			chosenscreencolors[page - 1] = screencolors[0];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Cornflower Cipher", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			// Chain Rotation Cipher
			var chainRotationN = Rnd.Range(1, 10);
			encrypt = ChainRotationEnc(word, chainRotationN, invert);
			// Stunted Blind Polybius Cipher
			string[] temp = StuntedBlindPolybiusEnc(encrypt, bits, invert);
			encrypt = temp[0];

			// Straddling Checkerboard Cipher
			var kw1 = pickWord(4, 8);
			var kw2 = pickWord(4, 8);
			var encryptKW3 = StraddlingCheckerboardEnc(temp[1], kw1, kw2, bits, invert);

			pages[page - 1][1] = temp[0].Substring(6) + " " + chainRotationN;
			pages[page - 1][2] = encryptKW3.Substring(0, encryptKW3.Length / 2);
			pages[page][0] = encryptKW3.Substring(encryptKW3.Length / 2);
			pages[page][1] = kw1;
			pages[page][2] = kw2;
		}

		return encrypt.Substring(0, 6);
	}
	string ChainRotationEnc(string word, int N, bool invert)
	{
		var encrypt = "";
		if(invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Before Chain Rotation Cipher: {1}", moduleId, word);
			while (word.Length > 0)
			{
				var amt = N % word.Length;
				word = word.Substring(amt) + word.Substring(0, amt);
				var obt = word[0];
				word = word.Substring(1);
				if (encrypt.Length > 0)
					obt = (char)((obt - 'A' + encrypt[encrypt.Length - 1] - 'A' + 1) % 26 + 'A');
				encrypt = encrypt + obt;
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] {1} -> {2}", moduleId, word, encrypt);
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] After Chain Rotation Cipher: {1}", moduleId, encrypt);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Chain Rotation Cipher amount: {1}", moduleId, N);
		}
		else
		{
			while (word.Length > 0)
			{
				var obt = word[word.Length - 1];
				word = word.Remove(word.Length - 1);
				if (word.Length > 0)
					obt = (char)('A' + (obt - 'A' + 52 - (word[word.Length - 1] - 'A' + 1)) % 26);
				encrypt = obt + encrypt;
				var amt = N % encrypt.Length;
				encrypt = encrypt.Substring(encrypt.Length - amt) + encrypt.Substring(0, encrypt.Length - amt);
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Before Chain Rotation Cipher: {1}", moduleId, encrypt);
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Chain Rotation Cipher amount: {1}", moduleId, N);
		}
		return encrypt;
	}
	string[] StuntedBlindPolybiusEnc(string word, bool[] bits, bool invert)
	{
		string encrypt, kw3;
		if (invert)
		{
			var braille1 = brailleDots.Where(dots => Enumerable.Range(0, 3).All(i => dots.Contains((char)(i + '4')) == (word[i] > 'P'))).PickRandom();
			var braille2 = brailleDots.Where(dots => Enumerable.Range(0, 3).All(i => dots.Contains((char)(i + '4')) == (word[i + 3] > 'P'))).PickRandom();
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Braille 5: {1}", moduleId, braille1);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Braille 6: {1}", moduleId, braille2);
			word = word.Select(ch => ch > 'P' ? (char)(ch - 13) : ch).Join("");
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] After ROT-13: {1}", moduleId, word);
			string[] temp = FindKW3(bits[0], word, wordList[4].ToList());
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] KW3: {1}", moduleId, temp[0]);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Braille 1-4: {1}", moduleId, temp[1]);
			kw3 = temp[0];
			wordList[4].Remove(kw3);
			encrypt = temp[1] + (char)(Array.IndexOf(brailleDots, braille1) + 'A') + (char)(Array.IndexOf(brailleDots, braille2) + 'A');
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Braille: {1}", moduleId, encrypt.Select(ch => "⠁⠃⠉⠙⠑⠋⠛⠓⠊⠚⠅⠇⠍⠝⠕⠏⠟⠗⠎⠞⠥⠧⠺⠭⠽⠵"[ch - 'A']).Join(""));
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Encrypted Word: {1}", moduleId, encrypt);
		}
		else
		{
			kw3 = pickWord(8);
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Braille: {1}", moduleId, toBraille(word));
			var brailleDots = "1,12,14,145,15,124,1245,125,24,245,13,123,134,1345,135,1234,12345,1235,234,2345,136,1236,2456,1346,13456,1356"
				.Split(',').Select(d => Enumerable.Range(0, 6).Select(i => d.Contains((char)('1' + i))).ToArray()).ToArray();

			// “nibble” = 4 bits. Some nibbles are a 2×2 square within a Braille letter, some are the bottom 2 dots of one Braille letter and the top 2 dots of the next
			var brailleNibbles = new int[(word.Length * 3 + 1) / 2];
			for (int i = 0; i < word.Length; i++)
				for (var dot = 0; dot < 6; dot++)
					if (brailleDots[word[i] - 'A'][dot])
						brailleNibbles[(dot % 3 + 3 * i) / 2] |= 1 << ((dot % 3 + 3 * i) % 2) * 2 + dot / 3;

			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Braille nibbles: {1}", moduleId, brailleNibbles.Select(i => "⠀⠁⠈⠉⠂⠃⠊⠋⠐⠑⠘⠙⠒⠓⠚⠛"[i]).Join(""));

			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] KW3: {1}", moduleId, kw3);
			var colSeq = sequencing(kw3.Substring(0, 4));
			var rowSeq = sequencing(kw3.Substring(4));
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Blind Polybius columns: {1}; rows: {2}", moduleId, colSeq.Select(i => i + 1).Join(""), rowSeq.Select(i => i + 1).Join(""));

			var polybius = (bits[0] ? (kw3 + "ABCDEFGHIJKLMNOP") : "ABCDEFGHIJKLMNOP".Except(kw3).Concat(kw3)).Distinct().Where(ch => ch <= 'P').Join("");
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Stunted Polybius square: {1}", moduleId, polybius);

			encrypt = brailleNibbles.Select(nibble => polybius[colSeq[nibble % 4] + 4 * rowSeq[nibble / 4]]).Join("");
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Encrypted word: {1}", moduleId, encrypt);
		}
		return new string[] { encrypt, kw3};
	}
	private static readonly string[] brailleDots = { "1", "12", "14", "145", "15", "124", "1245", "125", "24", "245", "13", "123", "134", "1345", "135", "1234", "12345", "1235", "234", "2345", "136", "1236", "2456", "1346", "13456", "1356" };

	private string[] FindKW3(bool bit0, string word, List<string> eightLetterWords)
	{
		while (eightLetterWords.Count > 0)
		{
			var kw3ix = Rnd.Range(0, eightLetterWords.Count);
			var kw3 = eightLetterWords[kw3ix];
			eightLetterWords.RemoveAt(kw3ix);
			var colSeq = sequencing(kw3.Substring(0, 4));
			var rowSeq = sequencing(kw3.Substring(4));
			var polybius = (bit0 ? (kw3 + "ABCDEFGHIJKLMNOP") : "ABCDEFGHIJKLMNOP".Except(kw3).Concat(kw3)).Distinct().Where(ch => ch <= 'P').Join("");

			var stunted = word.Select(ch => polybius.IndexOf(ch)).Select(i => Array.IndexOf(colSeq, i % 4) + 4 * Array.IndexOf(rowSeq, i / 4)).ToArray();

			var braille1 = (stunted[0] & 1) | ((stunted[0] & 4) >> 1) | ((stunted[1] & 1) << 2) | ((stunted[0] & 2) << 2) | ((stunted[0] & 8) << 1) | ((stunted[1] & 2) << 4);
			var braille1ltr = Array.IndexOf(brailleDots, Enumerable.Range(1, 6).Where(bit => (braille1 & (1 << (bit - 1))) != 0).Join(""));
			if (braille1ltr == -1)
				continue;
			var braille2 = ((stunted[1] & 4) >> 2) | ((stunted[2] & 1) << 1) | ((stunted[2] & 4) << 0) | ((stunted[1] & 8) << 0) | ((stunted[2] & 2) << 3) | ((stunted[2] & 8) << 2);
			var braille2ltr = Array.IndexOf(brailleDots, Enumerable.Range(1, 6).Where(bit => (braille2 & (1 << (bit - 1))) != 0).Join(""));
			if (braille2ltr == -1)
				continue;

			var braille3 = (stunted[3] & 1) | ((stunted[3] & 4) >> 1) | ((stunted[4] & 1) << 2) | ((stunted[3] & 2) << 2) | ((stunted[3] & 8) << 1) | ((stunted[4] & 2) << 4);
			var braille3ltr = Array.IndexOf(brailleDots, Enumerable.Range(1, 6).Where(bit => (braille3 & (1 << (bit - 1))) != 0).Join(""));
			if (braille3ltr == -1)
				continue;

			var braille4 = ((stunted[4] & 4) >> 2) | ((stunted[5] & 1) << 1) | ((stunted[5] & 4) << 0) | ((stunted[4] & 8) << 0) | ((stunted[5] & 2) << 3) | ((stunted[5] & 8) << 2);
			var braille4ltr = Array.IndexOf(brailleDots, Enumerable.Range(1, 6).Where(bit => (braille4 & (1 << (bit - 1))) != 0).Join(""));
			if (braille4ltr == -1)
				continue;


			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Blind Polybius columns: {1}; rows: {2}", moduleId, colSeq.Select(i => i + 1).Join(""), rowSeq.Select(i => i + 1).Join(""));
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Stunted Polybius square: {1}", moduleId, polybius);
			return new string[] { kw3, String.Format("{0}{1}{2}{3}", (char)('A' + braille1ltr), (char)('A' + braille2ltr), (char)('A' + braille3ltr), (char)('A' + braille4ltr)) };
		

		}
		return null;
	}
	string StraddlingCheckerboardEnc(string word, string kw1, string kw2, bool[] bits, bool invert)
	{
		var d1 = Bomb.GetIndicators().Count() % 6;
		var d2 = Bomb.GetPortCount() % 6;
		if (d2 == d1)
			d2 = (d2 + 1) % 6;

		var d3 = Bomb.GetBatteryCount() % 6;
		var d4 = Bomb.GetOnIndicators().Count() % 6;
		if (d4 == d3)
			d4 = (d4 + 1) % 6;

		var rowDigits2 = Enumerable.Range(0, 6).Where(d => d != d3 && d != d4).ToArray();
		var straddlingCheckerboard2 = MakeStraddlingCheckerboard(bits[1], bits[2], kw2, rowDigits2);

		var encryptedDigits = new List<int>();
		foreach (var ch in word)
		{
			var ix = straddlingCheckerboard2.IndexOf(ch);
			if (ix >= 6)
				encryptedDigits.Add(rowDigits2[ix / 6 - 1]);
			encryptedDigits.Add(ix % 6);
		}

		// Forward Straddling Checkerboard Cipher

		var rowDigits1 = Enumerable.Range(0, 6).Where(d => d != d1 && d != d2).ToArray();
		var straddlingCheckerboard1 = MakeStraddlingCheckerboard(bits[3], bits[4], kw1, rowDigits1);

		var encrypt = "";
		for (var i = 0; i < encryptedDigits.Count; i++)
		{
			if (encryptedDigits[i] == d1 || encryptedDigits[i] == d2)
				encrypt += straddlingCheckerboard1[encryptedDigits[i]];
			else
			{
				if (i == encryptedDigits.Count - 1)
					encryptedDigits.Add(rowDigits2.Where(d => d != d1 && d != d2).First());
				encrypt += straddlingCheckerboard1[(Array.IndexOf(rowDigits1, encryptedDigits[i]) + 1) * 6 + encryptedDigits[i + 1]];
				i++;
			}
		}
		if(invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Backward Straddling Checkerboard Cipher: KW2: {1}, D3: {2}, D4: {3}", moduleId, kw2, d3, d4);
			for (var i = 0; i < 5; i++)
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Backward Straddling Checkerboard Cipher: Row [{1}] = [{2}]", moduleId, i == 0 ? " " : rowDigits2[i - 1].ToString(), straddlingCheckerboard2.Substring(6 * i, 6).Join(" "));
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Backward Straddling Checkerboard result: {1}", moduleId, encryptedDigits.Join(""));
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Forward Straddling Checkerboard Cipher: KW1: {1}, D1: {2}, D2: {3}", moduleId, kw1, d1, d2);
			for (var i = 0; i < 5; i++)
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Forward Straddling Checkerboard Cipher: Row [{1}] = [{2}]", moduleId, i == 0 ? " " : rowDigits1[i - 1].ToString(), straddlingCheckerboard1.Substring(6 * i, 6).Join(" "));
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV CORNFLOWER] Forward Straddling Checkerboard result: {1}", moduleId, encrypt);
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Backward Straddling Checkerboard Cipher: KW2: {1}, D3: {2}, D4: {3}", moduleId, kw2, d3, d4);
			for (var i = 0; i < 5; i++)
				Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Backward Straddling Checkerboard Cipher: Row [{1}] = [{2}]", moduleId, i == 0 ? " " : rowDigits2[i - 1].ToString(), straddlingCheckerboard2.Substring(6 * i, 6).Join(" "));
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Backward Straddling Checkerboard result: {1}", moduleId, encryptedDigits.Join(""));
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Forward Straddling Checkerboard Cipher: KW1: {1}, D1: {2}, D2: {3}", moduleId, kw1, d1, d2);
			for (var i = 0; i < 5; i++)
				Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Forward Straddling Checkerboard Cipher: Row [{1}] = [{2}]", moduleId, i == 0 ? " " : rowDigits1[i - 1].ToString(), straddlingCheckerboard1.Substring(6 * i, 6).Join(" "));
			Debug.LogFormat("[Ultimate Cipher #{0}] [CORNFLOWER] Forward Straddling Checkerboard result: {1}", moduleId, encrypt);
		}
		
		return encrypt;
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
	string marooncipher(string word, bool invert)
	{
		fontsizes[page][0] = 35;
		fontsizes[page][1] = 35;
		fontsizes[page][2] = 35;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 35;
		fontsizes[page - 1][2] = 40;
		int length = UnityEngine.Random.Range(0, 5);
		string keyword = wordList[length][UnityEngine.Random.Range(0, wordList[length].Count)];
		string key = getKey(keyword.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetPortCount() % 4 < 2);
		pages[page - 1][1] = keyword.ToUpperInvariant();
		string encrypt = "";
		if(invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Maroon Cipher", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] Generated Keyword: {1}", moduleId, pages[0][1]);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] Generated Key: {1}", moduleId, key);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] Begin Monoalphabetic Encryption", moduleId, key);
			encrypt = MonoalphabeticEnc(word.ToUpperInvariant(), key.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] Begin Redfence Transposition", moduleId, key);
			encrypt = RedfenceTrans(encrypt.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] Begin Huffman Encryption", moduleId, key);
			encrypt = HuffmanEnc(encrypt.ToUpperInvariant(), key.ToUpperInvariant(), invert);
		}
		else
		{
			chosentextcolors[page] = Color.white;
			chosenscreencolors[page] = screencolors[0];
			chosentextcolors[page - 1] = Color.white;
			chosenscreencolors[page - 1] = screencolors[0];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Maroon Cipher", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Generated Keyword: {1}", moduleId, pages[0][1]);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Generated Key: {1}", moduleId, key);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Begin Huffman Encryption", moduleId, key);
			encrypt = HuffmanEnc(word.ToUpperInvariant(), key.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Begin Redfence Transposition", moduleId, key);
			encrypt = RedfenceTrans(encrypt.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Begin Monoalphabetic Encryption", moduleId, key);
			encrypt = MonoalphabeticEnc(encrypt.ToUpperInvariant(), key.ToUpperInvariant(), invert);
		}
		return encrypt;
	}
	string HuffmanEnc(string word, string key, bool invert)
	{
		ArrayList paths = HuffmanGen();
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
		string result = "", encrypt = "";
		//Encrypting the word
		if (invert)
		{
			for (int aa = 0; aa < paths.Count; aa++)
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] {1}: {2}", moduleId, paths[aa], key[aa]);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] Generated Binary: {1}", moduleId, binary);
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
			if (binary.Length > 0)
			{
				if (binary.Length == 4)
				{
					string temp = binaryToLet(binary);
					if (temp.Length != 1)
						temp = binaryToLet(binary.Substring(0, 2)) + "" + binaryToLet(binary.Substring(2));
					result = result + "" + temp;
				}
				else if (binary.Length == 3)
				{
					if (UnityEngine.Random.Range(0, 2) == 0)
						result = result + "" + binaryToLet(binary.Substring(0, 2)) + "" + binaryToLet(binary.Substring(2));
					else
						result = result + "" + binaryToLet(binary.Substring(0, 1)) + "" + binaryToLet(binary.Substring(1));
				}
				else
					result = result + "" + binaryToLet(binary);
			}
			encrypt = result.Substring(0, 6);
			result = result.Substring(6);
			binary = "";
			for (int aa = 0; aa < word.Length; aa++)
				binary = binary + "" + letToBinary(word[aa]);
			binary = binary.Replace("0", "L").Replace("1", "R");
			bool flag = true;
			string result2 = "";
			while(flag)
			{
				flag = false;
				for(int aa = 0; aa < paths.Count; aa++)
				{
					string temp = (string)paths[aa];
					if(binary.StartsWith(temp))
					{
						result2 = result2 + "" + key[aa];
						binary = binary.Substring(temp.Length);
						flag = true;
						break;
					}
				}
			}
			binary = binary.Replace("L", "0").Replace("R", "1");
			while(binary.Length > 1)
			{
				result2 = result2 + "" + binaryToLet(binary.Substring(0, 2));
				binary = binary.Substring(2);
			}
			if(binary.Length > 0)
				result2 = result2 + "" + binaryToLet(binary);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Encrypted Word: {1}", moduleId, encrypt);
			pages[page][0] = result.ToUpperInvariant();
			pages[page][1] = result2.Substring(0, result2.Length / 2);
			pages[page][2] = result2.Substring(result2.Length / 2);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Page 2 Top Screen: {1}", moduleId, pages[page][0]);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Page 2 Middle Screen: {1}", moduleId, pages[page][1]);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Page 2 Bottom Screen: {1}", moduleId, pages[page][2]);
		}
		else
		{
			for (int aa = 0; aa < paths.Count; aa++)
				Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] {1}: {2}", moduleId, paths[aa], key[aa]);
			for (int aa = 0; aa < word.Length; aa++)
				binary = binary + "" + (string)paths[key.IndexOf(word[aa])];
			binary = binary.Replace("L", "0").Replace("R", "1");
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Generated Binary: {1}", moduleId, binary);
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
			if (binary.Length > 0)
			{
				if (binary.Length == 4)
				{
					string temp = binaryToLet(binary);
					if (temp.Length != 1)
						temp = binaryToLet(binary.Substring(0, 2)) + "" + binaryToLet(binary.Substring(2));
					result = result + "" + temp;
				}
				else if (binary.Length == 3)
				{
					if (UnityEngine.Random.Range(0, 2) == 0)
						result = result + "" + binaryToLet(binary.Substring(0, 2)) + "" + binaryToLet(binary.Substring(2));
					else
						result = result + "" + binaryToLet(binary.Substring(0, 1)) + "" + binaryToLet(binary.Substring(1));
				}
				else
					result = result + "" + binaryToLet(binary);
			}
			encrypt = result.Substring(0, 6);
			result = result.Substring(6);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Encrypted Word: {1}", moduleId, encrypt);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Resulting Characters: {1}", moduleId, result);
			int divide = result.Length / 3;
			pages[page][0] = result.Substring(0, divide);
			pages[page][1] = result.Substring(divide, divide);
			pages[page][2] = result.Substring(divide * 2);
		}
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
	string letToBinary(char let)
	{
		string[] bins =
		{
			"00000","00001","00010","00011","00100","00101","00110","00111","01000","01001","01010","01011","01100",
			"01101","01110","01111","10000","10001","10010","10011","1010","1011","1100","1101","1110","1111"
		};
		string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		return bins[alpha.IndexOf(let)];
	}
	ArrayList HuffmanGen()
	{
		ArrayList numbers = new ArrayList();
		numbers.Add(UnityEngine.Random.Range(0, 7) + 10);
		//numbers.Add(10);
		numbers.Add(26 - (int)numbers[0]);
		ArrayList paths = new ArrayList() { "L", "R" };
		bool allOne = false;
		while (!(allOne))
		{
			ArrayList pathList = new ArrayList();
			ArrayList numberList = new ArrayList();
			for (int aa = 0; aa < numbers.Count; aa++)
			{
				if ((int)numbers[aa] > 1)
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
			for (int aa = 0; aa < numbers.Count; aa++)
			{
				if ((int)numbers[aa] != 1)
				{
					allOne = false;
					break;
				}
			}
		}
		return paths;
	}
	string RedfenceTrans(string word, bool invert)
	{
		string[] rows = new string[UnityEngine.Random.Range(0, 4) + 2];
		string poss = "";
		for (int aa = 0; aa < rows.Length; aa++)
		{
			rows[aa] = "";
			poss = poss + "" + (aa + 1);
		}
		int offset = 1;
		int row = 0;
		string encrypt = "";
		if(invert)
		{
			for (int aa = 0; aa < 6; aa++)
			{
				rows[row] = rows[row] + "*";
				if ((row == 0 && offset == -1) || row == rows.Length - 1)
					offset = offset * -1;
				row += offset;
			}
			string key = "", order = "12345";
			int cur = 0;
			for (int aa = 0; aa < rows.Length; aa++)
			{
				key = key + "" + poss[UnityEngine.Random.Range(0, poss.Length)];
				poss = poss.Replace(key[aa] + "", "");
				rows[order.IndexOf(key[aa])] = word.Substring(cur, rows[order.IndexOf(key[aa])].Length);
				cur += rows[order.IndexOf(key[aa])].Length;
			}
			for(int aa = 0; aa < rows.Length; aa++)
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] Row #{1}: {2}", moduleId, (aa + 1), rows[aa]);
			row = 0;
			offset = 1;
			for (int aa = 0; aa < 6; aa++)
			{
				encrypt = encrypt + "" + rows[row][0];
				rows[row] = rows[row].Substring(1);
				if ((row == 0 && offset == -1) || row == rows.Length - 1)
					offset = offset * -1;
				row += offset;
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] Redfence Key: {1}", moduleId, key);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] {1} -> {2}", moduleId, word, encrypt);
			pages[page - 1][2] = key.ToUpperInvariant();
		}
		else
		{
			for (int aa = 0; aa < 6; aa++)
			{
				rows[row] = rows[row] + "" + word[aa];
				if ((row == 0 && offset == -1) || row == rows.Length - 1)
					offset = offset * -1;
				row += offset;
			}
			string key = "", order = "12345";
			for (int aa = 0; aa < rows.Length; aa++)
			{
				key = key + "" + poss[UnityEngine.Random.Range(0, poss.Length)];
				poss = poss.Replace(key[aa] + "", "");
				Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Row #{1}: {2}", moduleId, (aa + 1), rows[aa]);
				encrypt = encrypt + "" + rows[order.IndexOf(key[aa])].ToUpperInvariant();
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] Redfence Key: {1}", moduleId, key);
			Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] {1} -> {2}", moduleId, word, encrypt);
			pages[page - 1][2] = key.ToUpperInvariant();
		}
		return encrypt;
	}
	string MonoalphabeticEnc(string word, string key, bool invert)
	{
		string encrypt = "", alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		if(invert)
		{
			for (int aa = 0; aa < 6; aa++)
			{
				encrypt = encrypt + "" + alpha[key.IndexOf(word[aa])];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV MAROON] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
			}
		}
		else
		{
			for (int aa = 0; aa < 6; aa++)
			{
				encrypt = encrypt + "" + key[alpha.IndexOf(word[aa])];
				Debug.LogFormat("[Ultimate Cipher #{0}] [MAROON] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
			}
		}
		return encrypt;
	}
	string browncipher(string word, bool invert)
	{
		fontsizes[page][0] = 40;
		fontsizes[page][1] = 40;
		fontsizes[page][2] = 40;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 40;
		fontsizes[page - 1][2] = 40;
		string kw = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
		wordList[2].Remove(kw.ToUpperInvariant());
		if (invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Brown Cipher", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] Begin Strip Encryption", moduleId);
			string encrypt = StripEnc(word.ToUpperInvariant(), kw.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] Begin M-209 Encryption", moduleId);
			encrypt = M209Enc(encrypt.ToUpperInvariant(), kw.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] Begin Book Encryption", moduleId);
			BookEnc(kw.ToUpperInvariant(), invert);
			return encrypt;
		}
		else
		{
			chosentextcolors[page] = Color.white;
			chosenscreencolors[page] = screencolors[0];
			chosentextcolors[page - 1] = Color.white;
			chosenscreencolors[page - 1] = screencolors[0];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Brown Cipher", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", moduleId);
			Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] Begin M-209 Encryption", moduleId);
			string encrypt = M209Enc(word.ToUpperInvariant(), kw.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] Begin Strip Encryption", moduleId);
			encrypt = StripEnc(encrypt.ToUpperInvariant(), kw.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] Begin Book Encryption", moduleId);
			BookEnc(kw.ToUpperInvariant(), invert);
			return encrypt;
		}
	}
	string M209Enc(string word, string kw, bool invert)
	{
		string[] binary =
		{
			"111000",
			"000010",
			"101101",
			"011100",
			"101110",
			"100011",
			"011000",
			"101010",
			"110101",
			"011010",

			"111001",
			"001011",
			"000111",
			"101100",
			"100110",
			"111011",
			"001101",
			"111101",

			"011001",
			"110000",
			"101110",
			"100101",
			"011111",
			"111010",
			"010110",
			"000101",
			"000011",

			"010001",
			"010000",
			"010010",
			"100001",
			"001010",
			"001100",
			"110111",
			"111100",
			"000100"
		};
		string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string alphaSerial = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string encrypt = "";
		string[] rotors = new string[6];
		int[] lugs = new int[6];
		for (int aa = 0; aa < 6; aa++)
		{
			rotors[aa] = binary[alphaSerial.IndexOf(Bomb.GetSerialNumber()[aa])];
			lugs[aa] = (alpha.IndexOf(kw[aa]) % 13) + 1;
			if(invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] Rotor #{1}: {2}", moduleId, (aa + 1), rotors[aa]);
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] Number of Lugs triggering Rotor #{1}: {2}", moduleId, (aa + 1), lugs[aa]);
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] Rotor #{1}: {2}", moduleId, (aa + 1), rotors[aa]);
				Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] Number of Lugs triggering Rotor #{1}: {2}", moduleId, (aa + 1), lugs[aa]);
			}
		}
		for (int aa = 0; aa < 6; aa++)
		{
			int num = 25 - alpha.IndexOf(word[aa]);
			for (int bb = 0; bb < 6; bb++)
			{
				if (rotors[bb][aa] == '1')
					num += lugs[bb];
			}
			encrypt = encrypt + "" + alpha[num % 26];
			if(invert)
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
			else
				Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
		}
		return encrypt;
	}
	string StripEnc(string word, string kw, bool invert)
	{
		string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string[] stripTable =
		{
			"NIPTLVZWHACBFQGMSRXYUOJEDK", "SFHLNYGCUQWXDEVKOIZMTBPJAR",
			"WJCXPNVERYSQBDIFZTMGOUKHLA", "GHEJXBFPLUMKNRZCAWOSQYVDIT",
			"RUJVMCTAIHXGEPLNDQYOKFZBWS", "LDWSYHKBOVGCRMNETPIQAJXFZU",
			"ONMIRKQYDJETCZWGLFBXHASVUP", "MTVYBRXNFDJOASUQPEKLIWHGCZ",
			"ECTNHOWFBMLVYJSRXGUZDIAPKQ", "UAQWGTIKNFHLVXMZEODCJPBRSY",
			"DSYKUJATZLOEMIRBFHNWPVCQGX", "PWKGSAOQECRMZLXJYUHTFNDIVB",
			"BQXRZMSVATKPJOFIGDLUWHENYC", "VLAQDZJXMKFIGHOUWSREBCYTPN",
			"TYGUOXDLPNZWIKESBAFVMRQCJH", "FOIDVWYSJZPUXBTAHNERCMLKQG",
			"QZREKPBGSXUNWVCTMYJFLDOAHI", "HGOACUNIKRQZPWJYVLTBSEFXMD",
			"KEFBQDHZWSARTNPLCVGIYXMUOJ", "ZRLCJEMHTPVAOUQDNXWKGSIYBF",
			"ABZPIGRDVOYSLCHXUKQJNTWMFE", "CPUMFQLRGIDJHYAVKBSNXZTOEW",
			"IVSZTFUOYWNDKGBPJCAHEQRLXM", "XKNFESCJQBTYUADHIMVPZLGWRO",
			"JXDHWIEMCGBFQTYORZPAVKUSNL", "YMBOALPUXEIHSFKWQJCDRGNZTV"
		};
		string[] strips = new string[6];
		for (int aa = 0; aa < 6; aa++)
		{
			strips[aa] = stripTable[alpha.IndexOf(kw[aa])].ToUpperInvariant();
			int cursor = strips[aa].IndexOf(word[aa]);
			strips[aa] = strips[aa].Substring(cursor) + "" + strips[aa].Substring(0, cursor);
			if (invert)
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] Key Row #{1}: {2}", moduleId, (aa + 1), strips[aa]);
			else
				Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] Key Row #{1}: {2}", moduleId, (aa + 1), strips[aa]);
		}
		int column;
		if(invert)
		{
			column = 25 - (Bomb.GetSerialNumberNumbers().Sum() % 25);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] Column Chosen: {1}", moduleId, (column + 1));
		}
		else
		{
			column = (Bomb.GetSerialNumberNumbers().Sum() % 25) + 1;
			Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] Column Chosen: {1}", moduleId, (column + 1));
		}
		string encrypt = "";
		for (int aa = 0; aa < 6; aa++)
		{
			encrypt = encrypt + "" + strips[aa][column];
			if(invert)
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
			else
				Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] {1} -> {2}", moduleId, word[aa], encrypt[aa]);
		}
		return encrypt;
	}
	void BookEnc(string word, bool invert)
	{
		var tempsnlets = Bomb.GetSerialNumberLetters().ToList();
		string snlets = tempsnlets[0] + "" + tempsnlets[tempsnlets.Count - 1];
		pages[page - 1][1] = "";
		pages[page - 1][2] = "";
		pages[page][0] = "";
		pages[page][1] = "";
		pages[page][2] = "";
		string[][][] diary =
		{
			new string[][]
			{
				new string[]{"FIRST","OF","ALL","I","WANT","TO","GET","SOMETHING","STRAIGHT","THIS","IS","A","JOURNAL","NOT","A","DIARY","I","KNOW","WHAT","IT","SAYS","ON","THE","COVER","BUT","WHEN","MOM","WENT","OUT","TO","BUY","THIS","THING","I","SPECIFICALLY","SAID","TO","MAKE","SURE","IT","DIDNT","SAY","DIARY","ON","IT","SO","DONT","EXPECT","ME","TO","BE","ALL","DEAR","DIARY","THIS","AND","DEAR","DIARY","THAT"},
				new string[]{"ALL","I","NEED","IS","FOR","SOME","JERK","TO","CATCH","ME","CARRYING","THIS","THING","AROUND","AND","GET","THE","WRONG","IDEA"},
				new string[]{"THE","OTHER","THING","I","WANT","TO","CLEAR","UP","RIGHT","AWAY","IS","HOW","THIS","WAS","NOT","MY","IDEA","IT","WAS","MOMS"},
				new string[]{"THE","DEAL","IS","THAT","IF","I","WRITE","IN","THIS","BOOK","A","LITTLE","BIT","EACH","DAY","I","GET","OUT","OF","ONE","CHORE","ON","SATURDAYS","SO","OF","COURSE","I","PICKED","THE","ONE","I","HATE","THE","MOST","BUT","IF","RODRICK","EVER","FINDS","OUT","HES","SCRUBBING","TOILETS","BECAUSE","OF","THIS","BOOK","IM","DEAD"},
				new string[]{"OH","YEAH","RODRICKS","MY","BROTHER","I","TRY","TO","AVOID","HIM","ANYWAY","BUT","NOW","THAT","I","STRUCK","THIS","DEAL","WITH","MOM","I","BETTER","BE","EXTRA","CAREFUL"},
				new string[]{"ANYWAY","I","THINK","MOM","HAS","THIS","IDEA","IM","GOING","TO","WRITE","DOWN","MY","FEELINGS","AND","ALL","THAT","BUT","SHES","NOT","ACTUALLY","ALLOWED","TO","READ","IT","SO","I","FIGURE","ILL","JUST","WRITE","WHAT","I","WANT"},
				new string[]{"THE","REAL","REASON","I","AGREED","TO","DO","THIS","AT","ALL","IS","BECAUSE","I","FIGURE","LATER","ON","WHEN","IM","RICH","AND","FAMOUS","ILL","HAVE","BETTER","THINGS","TO","DO","THAN","ANSWER","PEOPLES","STUPID","QUESTIONS","ALL","DAY","LONG"},
				new string[]{"LIKE","I","SAID","ONE","DAY","I","WILL","DEFINITELY","BE","FAMOUS","BUT","FOR","NOW","IM","STUCK","IN","THE","SEVENTH","GRADE","WITH","A","BUNCH","OF","MORONS"},
				new string[]{"TODAY","IS","THE","FIRST","DAY","OF","SCHOOL","AND","RIGHT","NOW","WERE","JUST","WAITING","AROUND","FOR","THE","TEACHER","TO","HURRY","UP","AND","FINISH","THE","SEATING","CHART"},
				new string[]{"SO","I","FIGURED","I","MIGHT","AS","WELL","WRITE","IN","THIS","BOOK","AND","JUST","GET","IT","OVER","WITH","FOR","THE","DAY"},
				new string[]{"BUT","ILL","TELL","YOU","SOMETHING","ON","THE","FIRST","DAY","OF","SCHOOL","YOU","GOT","TO","BE","REAL","CAREFUL","OF","WHERE","YOU","SIT","YOU","WALK","INTO","THE","CLASSROOM","AND","JUST","PLUNK","YOUR","STUFF","DOWN","ON","ANY","OLD","DESK","AND","THE","NEXT","THING","YOU","KNOW","THE","TEACHER","IS","SAYING","I","HOPE","YOU","ALL","LIKE","WHERE","YOURE","SITTING","BECAUSE","THESE","ARE","YOUR","PERMANENT","SEATS"},
				new string[]{"SO","IN","THIS","CLASS","I","GOT","STUCK","WITH","CHRIS","HOSEY","IN","FRONT","OF","ME","AND","LIONEL","JAMES","IN","BACK","OF","ME","OTHA","HARRIS","CAME","IN","LATE","AND","ALMOST","SAT","NEXT","TO","ME","BUT","LUCKILY","I","DID","SOME","QUICK","THINKING","AND","GOT","MYSELF","OUT","OF","THAT","ONE"}
			},
			new string[][]
			{
				new string[]{"IM","THINKING","FOR","NEXT","PERIOD","I","SHOULD","JUST","SIT","IN","THE","MIDDLE","OF","A","BUNCH","OF","CUTE","GIRLS","AS","SOON","AS","I","STEP","IN","THE","ROOM"},
				new string[]{"THEN","AGAIN","IF","I","DO","THAT","IT","JUST","PROVES","THAT","I","DIDNT","LEARN","A","THING","FROM","LAST","YEAR"},
				new string[]{"PLUS","THE","OTHER","THING","I","GOT","TO","THINK","ABOUT","IS","THAT","GIRLS","DONT","LET","YOU","COPY","OFF","OF","THEM","WHICH","COULD","BE","A","REAL","PROBLEM","IN","A","CLASS","LIKE","PRE","ALGEBRA"},
				new string[]{"SPEAKING","OF","SEATING","SOMETHING","THAT","REALLY","STUNK","TODAY","IS","HOW","IN","HOME","ROOM","I","GOT","STUCK","WITH","SOME","TEACHER","WHO","HAD","RODRICK","IN","HIS","CLASS","A","FEW","YEARS","BACK"},
				new string[]{"THE","ONLY","GOOD","THING","I","CAN","THINK","ABOUT","THE","FIRST","DAY","OF","SCHOOL","IS","THAT","SOME","OF","THE","TEACHERS","ARE","NEW","AND","SO","YOU","CAN","SLIDE","A","LITTLE"},
				new string[]{"ANYWAY","THE","TEACHER","IS","ALMOST","DONE","WITH","THE","SEATING","CHART","AND","I","THINK","I","WROTE","ENOUGH","IN","THIS","BOOK","TO","KEEP","MOM","OFF","MY","BACK","FOR","TODAY"},
				new string[]{"THIS","MORNING","MOM","MADE","ME","LEND","MY","BROTHER","RODRICK","SOME","OF","MY","MONEY","SO","HE","COULD","BUY","LUNCH","WHICH","REALLY","STUNK","IM","STILL","MAD","AT","RODRICK","FOR","THE","TRICK","HE","PULLED","ON","ME","AT","THE","BEGINNING","OF","THE","SUMMER","SO","IM","REALLY","NOT","LOOKING","TO","DO","HIM","ANY","FAVORS"},
				new string[]{"WHAT","HAPPENED","WAS","THAT","ON","THE","FIRST","DAY","OF","SUMMER","VACATION","HE","WOKE","ME","UP","IN","THE","MIDDLE","OF","THE","NIGHT","DRESSED","UP","IN","HIS","SCHOOL","CLOTHES","HE","TOLD","ME","I","SLEPT","THROUGH","THE","WHOLE","SUMMER","BUT","THAT","LUCKILY","I","HAD","WOKEN","UP","IN","TIME","FOR","THE","FIRST","DAY","OF","SCHOOL"},
				new string[]{"YOU","MIGHT","THINK","IM","PRETTY","DUMB","FOR","FALLING","FOR","THAT","ONE","BUT","I","WAS","TOO","GROGGY","TO","KNOW","ANY","BETTER","AND","PLUS","RODRICK","HAD","SET","MY","CLOCK","AHEAD","AND","PULLED","THE","BLINDS","SHUT"},
				new string[]{"SO","I","JUST","GOT","UP","AND","GOT","DRESSED","AND","WENT","DOWNSTAIRS","TO","FIX","MYSELF","SOME","BREAKFAST","I","MUSTVE","MADE","A","BIG","RACKET","BECAUSE","THE","NEXT","THING","I","KNEW","DAD","WAS","IN","MY","FACE","WONDERING","WHAT","THE","HECK","I","WAS","DOING","EATING","CHEERIOS","AT",snlets[1] + "","AM"},
				new string[]{"THE","THING","ABOUT","DAD","WHEN","HE","COMES","DOWNSTAIRS","LATE","AT","NIGHT","IS","THAT","HES","ALWAYS","JUST","WEARING","A","TEE","SHIRT","AND","SOME","BOXER","SHORTS","I","DONT","KNOW","WHICH","IS","WORSE","GETTING","YELLED","OUT","OR","HAVING","TO","SEE","YOUR","FATHER","IN","HIS","UNDERWEAR"},
				new string[]{"I","KEEP","MEANING","TO","ASK","HIM","TO","PLEASE","PUT","ON","SOME","MORE","CLOTHES","THE","NEXT","TIME","HE","COMES","DOWNSTAIRS","BUT","THE","RIGHT","OPPORTUNITY","NEVER","COMES","UP"},
				new string[]{"ANYWAY","IT","TOOK","ME","A","COUPLE","OF","MINUTES","TO","FIGURE","OUT","WHAT","ALL","WAS","GOING","ON","WHEN","I","TOLD","DAD","RODRICK","TRICKED","ME","DAD","STOMPED","ON","DOWN","TO","RODRICKS","ROOM","IN","THE","BASEMENT","AND","I","FOLLOWED","ALONG"},
				new string[]{"I","WAS","PRETTY","EXCITED","TO","FINALLY","SEE","RODRICK","GET","WHAT","WAS","COMING","TO","HIM"}
			},
			new string[][]
			{
				new string[]{"BUT","WHEN","WE","GOT","DOWN","THERE","RODRICK","HAD","COVERED","UP","HIS","TRACKS","PRETTY","GOOD","AND","YOU","WOULD","NEVER","KNOW","HE","HAD","BEEN","UP","TO","SOMETHING"},
				new string[]{"DAD","JUST","THREW","UP","HIS","HANDS","AND","WENT","BACK","UP","TO","BED","SO","NOW","DAD","THOUGHT","I","WAS","AN","IDIOT","AND","A","LIAR"},
				new string[]{"COME","TO","THINK","OF","IT","EVER","SINCE","DAD","HAS","BEEN","REAL","SUSPICIOUS","AROUND","ME","LIKE","IM","TURNING","INTO","A","BAD","KID","OR","SOMETHING"},
				new string[]{"ILL","PUT","IT","TO","YOU","THIS","WAY","IF","IM","GOING","TO","DO","SOMETHING","BAD","AND","TAKE","THE","HEAT","LIKE","I","DID","THAT","NIGHT","YOU","BETTER","BELIEVE","IM","GOING","TO","COME","UP","WITH","SOMETHING","A","LOT","MORE","SATISFYING","THAN","EATING","A","BOWL","OF","CHEERIOS","IN","THE","MIDDLE","OF","THE","NIGHT"},
				new string[]{"TODAY","IN","SOCIAL","STUDIES","I","SCORED","PRETTY","BIG","THE","TEACHER","MADE","US","SIT","IN","ALPHABETICAL","ORDER","SO","THE","WAY","THINGS","FELL","OUT","I","ENDED","UP","RIGHT","NEXT","TO","ALEX","ARUDA","WHO","IS","THE","SMARTEST","KID","IN","THE","CLASS"},
				new string[]{"HES","SUPER","EASY","TO","COPY","OFF","OF","BECAUSE","HE","ALWAYS","FINISHES","HIS","TESTS","EARLY","AND","THEN","JUST","PUTS","HIS","PAPER","ON","THE","FLOOR","NEXT","TO","HIM","WHILE","HE","READS","A","SCIENCE","FICTION","NOVEL","OR","SOMETHING"},
				new string[]{"KIDS","WHOSE","LAST","NAMES","START","WITH","THE","EARLY","LETTERS","ALWAYS","END","UP","BEING","THE","SMARTEST","BECAUSE","THEY","GET","CALLED","ON","FIRST","SOME","PEOPLE","THINK","THATS","NOT","TRUE","BUT","IF","YOU","WANT","TO","COME","OVER","TO","MY","SCHOOL","I","CAN","PROVE","IT"},
				new string[]{"I","CAN","ONLY","THINK","OF","ONE","KID","WHO","BROKE","THE","LAST","NAME","RULE","AND","THATS","PETER","UTEGER","HE","WAS","THE","SMARTEST","KID","UNTIL","THE","MIDDLE","OF","THE","FIFTH","GRADE","THATS","WHEN","A","COUPLE","OF","US","STARTED","GIVING","HIM","A","HARD","TIME","ABOUT","WHAT","HIS","INITIALS","SPELLED","EVERY","CHANCE","WE","GOT"},
				new string[]{"NOW","HE","DOESNT","RAISE","HIS","HAND","AT","ALL","WHICH","MAKES","FOR","OTHER","KIDS","TO","STEP","FORWARD","AND","TAKE","THE","SMARTEST","KID","TITLE"},
				new string[]{"I","FEEL","A","LITTLE","BAD","ABOUT","THE","WHOLE","PU","THING","BECAUSE","IM","ONE","OF","THE","GUYS","WHO","STARTED","IT","BUT","ITS","HARD","NOT","TO","TAKE","CREDIT","FOR","IT","WHENEVER","IT","COMES","UP"},
				new string[]{"I","FIGURED","OUT","ANOTHER","GOOD","THING","ABOUT","WRITING","THIS","JOURNAL","WHEN","IM","FAMOUS","I","CAN","CASH","IN","ON","IT","I","JUST","HAVE","TO","REMEMBER","TO","KEEP","IT","AWAY","FROM","MANNY","MY","LITTLE","BROTHER"},
				new string[]{"IF","YOU","HAVE","SOMETHING","VALUABLE","IN","THE","HOUSE","BELIEVE","ME","MANNY","WILL","FIND","A","WAY","TO","DESTROY","IT"},
				new string[]{"BACK","BEFORE","MANNY","CAME","ALONG","I","REMEMBER","I","WAS","ALL","EXCITED","ABOUT","GETTING","A","LITTLE","BROTHER","AFTER","ALL","THOSE","YEARS","OF","RODRICK","PICKING","ON","ME","I","FIGURED","IT","WAS","MY","TURN","TO","BE","A","LITTLE","HIGHER","ON","THE","TOTEM","POLE"}
			},
			new string[][]
			{
				new string[]{"BUT","BEING","A","BIG","BROTHER","DIDNT","TURN","OUT","LIKE","I","EXPECTED","AT","ALL","MOM","AND","DAD","PROTECT","MANNY","SO","I","CANT","PICK","ON","HIM","EVEN","IF","HE","DOES","SOMETHING","TO","TICK","ME","OFF"},
				new string[]{"PLUS","HES","NEVER","GOTTEN","PUNISHED","FOR","ANYTHING","AND","BELIEVE","ME","HES","DESERVED","IT","A","BUNCH","OF","TIMES"},
				new string[]{"JUST","THE","OTHER","DAY","HE","SOMEHOW","GOT","INTO","MY","ROOM","AND","USED","A","BUNCH","OF","MAGIC","MARKERS","TO","DECORATE","MY","DOOR","I","THOUGHT","MOM","AND","DAD","WOULD","REALLY","LET","HIM","HAVE","IT","BUT","AS","USUAL","I","WAS","WRONG"},
				new string[]{"SO","NOW","IM","STUCK","WAKING","UP","TO","THIS","HORRIBLE","DRAWING","STARING","AT","ME","EVERYDAY","MOM","WONT","LET","ME","PAINT","OVER","IT","OR","EVEN","COVER","IT","UP","WITH","A","POSTER","BECAUSE","SHE","SAYS","IT","MIGHT","HURT","MANNYS","FEELINGS"},
				new string[]{"THE","ONLY","GOOD","THING","ABOUT","GETTING","A","LITTLE","BROTHER","IS","THAT","NOW","RODRICK","DOESNT","MAKE","ME","SELL","HIS","STUPID","CHOCOLATE","BARS","FOR","SCHOOL","FUNDRAISERS","ANY","MORE"},
				new string[]{"THE","WORST","THING","ABOUT","MANNY","IS","THAT","WHEN","HE","WAS","REAL","LITTLE","HE","COULDNT","PRONOUNCE","BROTHER","SO","HE","STARTED","CALLING","ME","BUBBY","AND","MOM","AND","DAD","DIDNT","MAKE","HIM","CALL","ME","MY","REAL","NAME","WHEN","HE","COULD","SAY","IT"},
				new string[]{"LUCKILY","NONE","OF","MY","FRIENDS","HAVE","FOUND","OUT","YET","BUT","I","HAVE","HAD","SOME","REALLY","CLOSE","CALLS"},
				new string[]{"YESTERDAY","WAS","THE","FIRST","DAY","OF","PE","AND","WE","STARTED","THE","FOOTBALL","UNIT","THE","FIRST","THING","I","DID","WAS","SNEAK","OFF","TO","THE","BASKETBALL","COURTS","AND","CHECK","TO","SEE","IF","THE","CHEESE","WAS","STILL","WHERE","IT","WAS","AT","THE","END","OF","LAST","SCHOOL","YEAR","AND","SURE","ENOUGH","IT","WAS"},
				new string[]{"THAT","THING","HAS","BEEN","SITTING","ON","THE","COURT","SINCE","AT","LEAST","LAST","FALL","AND","IT","HAS","CAUSED","A","WHOLE","LOT","OF","TROUBLE","ITS","ALL","MOLDLY","AND","NASTY","AND","EVER","SINCE","IT","SHOWED","UP","PEOPLE","HAVE","BEEN","TRYING","TO","AVOID","IT"},
				new string[]{"TO","GIVE","YOU","AN","IDEA","OF","HOW","PEOPLE","WILL","GO","OUT","OF","THEIR","WAY","TO","STAY","AWAY","FROM","THE","CHEESE","ITS","SITTING","RIGHT","UNDER","THE","ONLY","HOOP","WITH","A","NET","IN","IT","BUT","NOBODYS","PLAYED","ON","THAT","COURT","FOR","A","YEAR"},
				new string[]{"DARNELL","WASHINGTON","TRIPPED","AND","FELL","AND","BRUSHED","THE","CHEESE","WITH","HIS","FINGER","LAST","YEAR","AND","STARTED","THIS","WHOLE","THING","CALLED","THE","CHEESE","TOUCH","ITS","BASICALLY","LIKE","COOTIES","WHERE","IF","YOU","GET","TOUCHED","WITH","THE","CHEESE","TOUCH","THEN","YOU","HAVE","IT","UNTIL","YOU","PASS","IT","ON","TO","SOMEBODY","ELSE"},
				new string[]{"THE","ONLY","WAY","TO","PROTECT","YOURSELF","FROM","THE","CHEESE","TOUCH","WAS","TO","CROSS","YOUR","FINGERS","BUT","IT","WAS","REALLY","HARD","TO","REMEMBER","TO","KEEP","YOUR","FINGERS","CROSSED","ALL","THE","TIME","ESPECIALLY","WHEN","WHOEVER","HAD","THE","CHEESE","TOUCH","WAS","LOOKING","FOR","HIS","NEXT","VICTIM"}
			},
			new string[][]
			{
				new string[]{"SO","I","TAPED","MY","FINGERS","TOGETHER","FOR","THE","LAST","COUPLE","WEEKS","OF","SCHOOL","I","ENDED","UP","GETTING","A","D","IN","HANDWRITING","BUT","IT","WAS","TOTALLY","WORTH","IT"},
				new string[]{"THIS","ONE","KID","NAMED","ABE","HALL","GOT","THE","CHEESE","TOUCH","IN","APRIL","AND","NOBODY","WOULD","EVEN","SIT","AT","THE","SAME","TABLE","WITH","HIM","AT","LUNCH","FOR","THE","WHOLE","REST","OF","THE","YEAR"},
				new string[]{"THIS","SUMMER","HE","MOVED","AWAY","TO","CALIFORNIA","AND","HE","TOOK","THE","CHEESE","TOUCH","WITH","HIM","NOBODY","HAS","TOUCHED","THE","CHEESE","EVER","SINCE","THEN","NOT","EVEN","WITH","A","STICK"},
				new string[]{"WELL","THE","FIRST","WEEK","OF","SCHOOL","IS","FINALLY","OVER","SO","I","CAN","SLEEP","LATE","AGAIN","MOST","KIDS","SET","THEIR","ALARMS","AND","GET","UP","EARLY","ON","SATURDAY","MORNING","TO","WATCH","CARTOONS","OR","WHATEVER","BUT","NOT","ME","THE","WAY","I","KNOW","ITS","TIME","FOR","ME","TO","CRAWL","OUT","OF","BED","IS","WHEN","I","CANT","STAND","THE","TASTE","OF","MY","BREATH","ANYMORE"},
				new string[]{"UNFORTUNATELY","DAD","WAKES","UP","AT",snlets[0] + "" + snlets[0] + "" + snlets[0], "IN","THE","MORNING","NO","MATTER","WHAT","DAY","IT","IS","AND","HE","IS","NOT","REAL","CONSIDERATE","OF","THE","FACT","THAT","I","AM","TRYING","TO","ENJOY","MY","SATURDAY"},
				new string[]{"OTHER","THAN","THE","SATURDAY","MORNING","VACUUMING","ME","AND","DAD","GET","ALONG","PRETTY","GOOD","BUT","RODRICK","AND","DAD","IS","ANOTHER","STORY","IT","DOESNT","HELP","THAT","RODRICK","IS","A","TEENAGER","WHICH","IS","DADS","LEAST","FAVORITE","TYPE","OF","PERSON"},
				new string[]{"I","THINK","IF","THERE","WAS","A","PETITION","TO","SHIP","ALL","OF","THE","TEENAGERS","IN","THE","STATE","TO","AUSTRALIA","OR","ALCATRAZ","OR","SOMETHING","DAD","WOULD","BE","THE","FIRST","PERSON","TO","SIGN","IT"},
				new string[]{"AND","THE","FIRST","TEENAGER","HED","PUT","ON","THE","BOAT","WOULD","BE","THIS","KID","NAMED","LENWOOD","HEATH","LENWOOD","IS","ALWAYS","TOILET","PAPERING","PEOPLES","HOUSES","AND","GENERALLY","STIRRING","UP","TROUBLE","IN","THE","NEIGHBORHOOD"},
				new string[]{"DAD","HAS","SEEMED","A","LOT","MORE","RELAXED","EVER","SINCE","AUGUST","WHEN","LENWOODS","DAD","SHIPPED","HIM","OFF","TO","SOME","MILITARY","ACADEMY","IN","PENNSYLVANIA"},
				new string[]{"WHILE","IM","ON","THE","SUBJECT","OF","SATURDAY","I","SHOULD","MENTION","SOME","OF","MY","OTHER","GRIPES","FIRST","OF","ALL","THERES","NOTHING","ON","TV","AFTER",snlets[1] + "" + snlets[0] + "" + snlets[0], "PM","EXCEPT","GOLF","AND","BOWLING","SECOND","OF","ALL","THE","SUN","COMES","RIGHT","THROUGH","THE","SLIDING","GLASS","WINDOW","AND","YOU","CAN","HARDLY","SEE","WHATS","ON","THE","TV","ANYWAY","AND","ON","TOP","OF","THAT","YOU","GET","ALL","SWEATY","AND","STICK","TO","THE","COUCH","ITS","PRACTICALLY","LIKE","A","CONSIPRACY","AGAINST","KIDS","TO","MAKE","THEM","GO","OUTSIDE","AND","DO","SOMETHING","BESIDES","WATCH","TV"},
				new string[]{"TODAY","AFTER","DAD","WOKE","ME","UP","I","DECIDED","TO","JUST","SKIP","THE","WHOLE","TV","THING","AND","GO","OVER","TO","ROWLEYS"},
				new string[]{"I","KNOW","I","HAVENT","MENTIONED","ROWLEY","IN","THIS","JOURNAL","YET","EVEN","THOUGH","HES","TECHNICALLY","MY","BEST","FRIEND","BUT","THERES","A","PRETTY","GOOD","REASON","FOR","THAT"},
				new string[]{"ROWLEY","KIND","OF","TICKED","ME","OFF","ON","THE","FIRST","DAY","OF","SCHOOL","WITH","SOMETHING","HE","SAID","AT","THE","END","OF","THE","DAY","WHEN","WE","WERE","GETTING","OUR","STUFF","FROM","OUR","LOCKERS"}
			},
			new string[][]
			{
				new string[]{"I","TOLD","ROWLEY","AT","LEAST","A","BILLION","TIMES","THIS","SUMMER","THAT","NOW","THAT","WERE","IN","MIDDLE","SCHOOL","YOURE","SUPPOSED","TO","SAY","HANG","OUT","NOT","PLAY","BUT","NO","MATTER","HOW","MANY","TIMES","I","KICK","HIM","IN","THE","SHINS","WHEN","HE","SAYS","PLAY","HE","ALWAYS","FORGETS","FOR","THE","NEXT","TIME"},
				new string[]{"SO","I","GUESS","YOU","COULD","SAY","IVE","BEEN","AVOIDING","ROWLEY","THIS","WEEK","IVE","BEEN","TRYING","TO","BE","MORE","CAREFUL","ABOUT","MY","IMAGE","EVER","SINCE","THIS","SUMMER","WHEN","WE","GOT","CAUGHT","PLAYING","VIKINGS","AND","INDIANS","IN","THE","WOODS","BY","A","COUPLE","OF","EIGHTH","GRADERS"},
				new string[]{"WHAT","REALLY","BURNED","ME","UP","ABOUT","THAT","WHOLE","INCIDENT","IS","HOW","THAT","GUY","CALLED","ME","A","NERD","NOW","ILL","ADMIT","IM","NOT","EXACTLY","THE","MOST","MACHO","GUY","AROUND","IN","TERMS","OF","WANTING","TO","DO","PUSHUPS","ALL","THE","TIME","OR","WHATEVER","SO","IF","YOU","WANT","TO","CALL","ME","A","WIMP","THEN","FINE","BUT","I","KNOW","ONE","THING","FOR","SURE","AND","ITS","THAT","I","AM","NOT","A","NERD"},
				new string[]{"THE","TROUBLE","WITH","NERDS","IS","THAT","THEY","GIVE","WIMPY","KIDS","LIKE","ME","A","BAD","NAME","BECAUSE","PEOPLE","END","UP","LUMPING","US","ALL","IN","THE","SAME","CATEGORY","WHEN","I","THINK","OF","NERDS","I","THINK","OF","TEACHERS","PETS","AND","TICKLE","FIGHTS","AND","HALL","MONITORS","AND","THAT","IS","NOT","ME"},
				new string[]{"NOW","ROWLEY","CAN","SPEAK","FOR","HIMSELF","ON","THE","WHOLE","NERD","THING","BUT","I","WILL","JUST","MENTION","AS","A","SIDE","NOTE","THAT","HE","IS","THE","ONLY",snlets[1] + "" + snlets[0] + "YEAROLD","I","KNOW","WHO","STILL","HAS","A","BABYSITTER"},
				new string[]{"ROWLEY","MOVED","HERE","A","COUPLE","YEARS","AGO","AND","I","KIND","OF","TOOK","HIM","UNDER","MY","WING","MY","FORMER","BEST","FRIEND","BEN","MOVED","TO","PISCATAWAY","AND","I","FIGURED","ID","BETTER","FIND","MYSELF","A","NEW","FRIEND","TO","HANG","OUT","WITH","SO","HERE","COMES","ROWLEY","STRAIGHT","OUT","OF","OHIO","HIS","MOM","BOUGHT","HIM","SOME","BOOK","CALLED","HOW","TO","MAKE","FRIENDS","IN","NEW","PLACES","AND","HE","SHOWED","UP","AT","MY","DOOR","TRYING","OUT","ALL","THESE","GIMMICKS"},
				new string[]{"ALL","THAT","KID","WOULDVE","HAD","TO","HAVE","DONE","IS","TO","HAVE","COME","RIGHT","OUT","AND","TOLD","ME","HE","HAD","A","PLAYSTATION","WITH",snlets[0] + "" + snlets[0], "GAMES","AND","IT","WOULD","HAVE","SEALED","THE","DEAL"},
				new string[]{"THE","BEST","THING","ABOUT","HAVING","ROWLEY","AROUND","IS","THAT","I","GET","A","CHANCE","TO","USE","ALL","THE","TRICKS","RODRICK","USED","ON","ME","THAT","I","COULD","NEVER","GET","AWAY","WITH","PULLING","ON","MANNY"},
				new string[]{"ANOTHER","BONUS","ABOUT","ROWLEY","IS","THAT","HE","HAS","NEVER","SQUEALED","ON","ME","NOT","EVEN","ONCE","SO","IN","SOME","WAYS","I","GUESS","YOU","COULD","SAY","HES","THE","PERFECT","FRIEND"},
				new string[]{"TODAY","WAS","A","REALLY","BAD","DAY","DAD","ENDED","UP","RUNNING","INTO","MR","SWANN","AT","CHURCH","AND","MR","SWANN","WAS","TELLING","DAD","HOW","GREAT","BISHOP","GARRIGAN","HIGH","SCHOOL","IS","WHERE","HIS","SON","DAN","GOES"},
				new string[]{"DAD","SEEMED","REAL","INTERESTED","WHICH","IS","A","VERY","BAD","SIGN","FOR","ME","NOW","IM","SURE","BISHOP","GARRIGAN","IS","A","FINE","SCHOOL","AND","ALL","THAT","EXCEPT","FOR","THE","FACT","THAT","IT","IS","ALL","BOYS","NUMBER","ONE","I","WANT","TO","GO","TO","CROSSLAND","HIGH","SCHOOL","WHERE","THERE","ARE","BOYS","AND","GIRLS","AND","NUMBER","TWO","I","WOULDNT","SURVIVE","THE","FIRST","DAY","AT","BISHOP","GARRIGAN"}
			},
			new string[][]
			{
				new string[]{"RODRICK","DOESNT","HAVE","TO","WORRY","ABOUT","GETTING","SENT","TO","BISHOP","GARRIGAN","BECAUSE","HE","IS","ALREADY","A","JUNIOR","AT","CROSSLAND","BUT","I","HAD","DEFINITELY","BETTER","FIGURE","A","WAY","OUT","OF","THIS"},
				new string[]{"MR","SWANN","WENT","ON","AND","ON","ABOUT","HOW","BISHOP","GARRIGAN","MAKES","MEN","OUT","OF","BOYS","AND","FROM","THE","WAY","DAD","KEPT","LOOKING","OVER","AT","ME","I","KNEW","I","WAS","IN","TROUBLE","IT","DOESNT","HELP","THAT","MR","SWANN","HAS","THREE","BOYS","WHO","ARE","THE","SAME","AGES","AS","US","HEFFLEY","BOYS","AND","DADS","CARPOOL","PASSES","BY","THEIR","HOUSE","EVERY","NIGHT"},
				new string[]{"AS","FAR","AS","THE","WHOLE","MAKING","MEN","OUT","OF","BOYS","IDEA","GOES","I","THINK","THE","SWANN","BOYS","HAVE","A","PRETTY","GOOD","HEAD","START"},
				new string[]{"IVE","STILL","GOT","TWO","YEARS","BEFORE","I","GO","TO","HIGH","SCHOOL","AND","HOPEFULLY","DAD","WILL","FORGET","ABOUT","BISHOP","GARRIGAN","BY","THEN"},
				new string[]{"BUT","IF","THINGS","LOOK","BAD","DOWN","THE","ROAD","I","BETTER","START","WORKING","ON","MOM","TO","CHANGE","MY","FATE"},
				new string[]{"TODAY","I","WOKE","UP","AND","AT","FIRST","I","THOUGHT","IT","WAS","STILL","SUMMER","VACATION","WHICH","IS","A","REALLY","BAD","WAY","TO","START","A","SCHOOL","DAY"},
				new string[]{"THE","NEW","THING","IS","THAT","I","HAVE","TO","FIX","MANNY","HIS","CEREAL","EVERY","MORNING","WHILE","MOM","GETS","READY","FOR","WORK","MANNY","TAKES","HIS","BOWL","AND","SITS","RIGHT","IN","FRONT","OF","THE","TV","ON","HIS","PLASTIC","POTTY"},
				new string[]{"ITS","NOT","LIKE","HES","NOT","POTTY","TRAINED","BUT","HE","GOT","IN","THE","HABIT","OF","DOING","THIS","WHEN","HE","WAS","TWO","AND","HE","JUST","NEVER","QUIT"},
				new string[]{"THE","WORST","PART","IS","THAT","AFTER","HES","DONE","HE","DUMPS","WHATEVER","HE","DIDNT","EAT","RIGHT","INTO","THE","POTTY","AND","ITS","ALWAYS","ME","WHO","HAS","TO","CLEAN","IT","UP"},
				new string[]{"MOM","ALWAYS","GETS","ON","ME","ABOUT","NOT","FINISHING","MY","BREAKFAST","BUT","IF","YOU","HAD","TO","SCRAPE","A","BUNCH","OF","CHEERIOS","OUT","OF","A","POTTY","EVERY","MORNING","I","BET","YOU","WOULDNT","HAVE","ANY","APPETITE","EITHER"},
				new string[]{"TODAY","AT","SCHOOL","WE","GOT","ASSIGNED","TO","READING","GROUPS","I","WAS","LOOKING","FORWARD","TO","FINDING","OUT","WHICH","GROUP","I","WAS","GOING","TO","GET","PUT","INTO","BECAUSE","I","WANTED","TO","SEE","IF","A","BIG","PLAN","I","HATCHED","AT","THE","END","OF","LAST","YEAR","WAS","GOING","TO","WORK"},
				new string[]{"NOW","THEY","DONT","COME","RIGHT","OUT","AND","TELL","YOU","IF","YOURE","IN","THE","HARD","GROUP","OR","THE","EASY","GROUP","BUT","YOU","CAN","FIGURE","IT","OUT","RIGHT","AWAY","BY","LOOKING","AT","THE","COVERS","OF","THE","BOOKS","THEY","GIVE","YOU"},
				new string[]{"I","WAS","PRETTY","MAD","TO","FIND","OUT","I","GOT","PUT","IN","THE","HARD","GROUP","TODAY","WHICH","MEANT","MY","PLAN","FAILED","I","WAS","HOPING","TO","GET","IN","THE","EASY","GROUP","BECAUSE","THEY","ONLY","HAVE","TO","READ","ABOUT","A","TENTH","OF","THE","STUFF","THAT","THE","KIDS","IN","THE","HARD","GROUP","HAVE","TO","READ","AND","THERES","A","WHOLE","LOT","LESS","HOMEWORK"}
			},
			new string[][]
			{
				new string[]{"AT","THE","END","OF","LAST","YEAR","I","DID","MY","BEST","TO","MUFF","UP","MY","SCREENING","TEST","TO","MAKE","SURE","I","DIDNT","GET","PUT","IN","THE","HARD","GROUP"},
				new string[]{"ANOTHER","THING","I","DID","TO","MAKE","SURE","I","DIDNT","GET","PUT","IN","THE","HARD","GROUP","WAS","TO","MAKE","SURE","I","DIDNT","TRY","TO","HARD","ON","MY","ENDOFTHEYEAR","ESSAY"},
				new string[]{"THEY","MAKE","YOU","DO","THIS","FOUR","PAGE","PAPER","AT","THE","END","OF","THE","YEAR","WHICH","IS","ANOTHER","WAY","THEY","FIGURE","OUT","HOW","TO","PLACE","YOU"},
				new string[]{"IM","GUESSING","MOM","STEPPED","IN","AND","MADE","SURE","I","GOT","PUT","IN","THE","HARD","CLASSES","BECAUSE","SHE","KNOWS","THE","PRINCIPAL","OF","THE","SCHOOL"},
				new string[]{"MOMS","ALWAYS","SAYING","HOW","IM","A","REAL","SMART","KID","BUT","I","JUST","DONT","APPLY","MYSELF"},
				new string[]{"YOU","MIGHT","WONDER","WHY","ID","WANT","TO","GET","PUT","IN","THE","EASY","CLASSES","SINCE","I","PROBABLY","DESERVE","TO","BE","IN","THE","HARD","CLASSES","BUT","I","HAVE","A","PRETTY","GOOD","ANSWER","FOR","THAT"},
				new string[]{"IF","THERES","ONE","THING","I","LEARNED","FROM","RODRICK","ITS","TO","SET","PEOPLES","EXPECTATIONS","REAL","LOW","SO","YOU","END","UP","SURPRISING","EVERYONE","BY","DOING","ALMOST","NOTHING","AT","ALL"},
				new string[]{"IN","FACT","HE","DID","SOMETHING","ON","FRIDAY","THAT","TOTALLY","PROVES","MY","POINT"},
				new string[]{"ANYWAY","I","GUESS","IM","GLAD","MY","PLAN","DIDNT","WORK","BECAUSE","I","NOTICED","AT","LEAST","TWO","OF","THE","KIDS","IN","THE","BINK","SAYS","BOO","GROUP","WERE","HOLDING","THEIR","BOOKS","UPSIDE","DOWN","AND","I","DONT","THINK","THEY","WERE","JOKING","AROUND"},
				new string[]{"TODAY","AT","LUNCH","I","GOT","TO","LISTEN","TO","ALBERT","SANDY","BRAG","ABOUT","HOW","HIS","PARENTS","BOUGHT","HIM","A",snlets[1] + "" + snlets[0], "INCH","TV","AND","A","DVD","PLAYER","AND","A","BUNCH","OF","OTHER","STUFF","FOR","HIS","BEDROOM"},
				new string[]{"IT","REALLY","MAKES","ME","MAD","BECAUSE","MY","WHOLE","GOAL","THIS","SUMMER","WAS","TO","SAVE","UP","FOR","A","TV","SO","I","DIDNT","HAVE","TO","HANG","OUT","WITH","THE","REST","OF","MY","FAMILY","AND","WATCH","WHAT","THEY","WANT","TO","WATCH","ALL","THE","TIME"},
				new string[]{"SO","I","SPENT","THE","WHOLE","SUMMER","TAKING","CARE","OF","MR","AND","MRS","ROSES","DOG","WHILE","THEY","WERE","ON","A","TRIP"},
				new string[]{"THE","DEAL","WAS","THAT","I","HAD","TO","GO","OVER","TO","THEIR","HOUSE","TWICE","A","DAY","TO","TAKE","THEIR","DOG","STEVIE","OUT","AND","I","WAS","SUPPOSED","TO","GET","THREE","BUCKS","A","DAY","FOR","DOING","IT"},
				new string[]{"THE","BIG","PROBLEM","WITH","STEVIE","IS","THAT","I","GUESS","HE","IS","TOTALLY","SHY","WHEN","IT","COMES","TO","GOING","TO","THE","BATHROOM","IN","FRONT","OF","STRANGERS","SO","I","WASTED","A","WHOLE","LOT","OF","MY","SUMMER","STANDING","THERE","WAITING","FOR","THIS","STUPID","DOG","TO","HURRY","UP","AND","GO"},
				new string[]{"SO","ID","WAIT","AND","WAIT","AND","NOTHING","WOULD","HAPPEN","SO","ID","JUST","GO","HOME","BUT","EVERY","TIME","I","CAME","BACK","TO","THE","HOUSE","LATER","ON","STEVIE","HAD","MADE","A","MESS","IN","THE","KITCHEN","I","FINALLY","FIGURED","OUT","STEVIE","WAS","JUST","HOLDING","IT","UNTIL","THE","COAST","WAS","CLEAR"}
			},
			new string[][]
			{
				new string[]{"IN","FACT","ONE","DAY","I","TRIED","AN","EXPERIMENT","WHERE","I","LEFT","AND","THEN","CAME","BACK","FIVE","MINUTES","LATER","AND","SURE","ENOUGH","STEVIE","HAD","POOPED","RIGHT","ON","THE","KITCHEN","FLOOR"},
				new string[]{"AND","ITS","NOT","LIKE","I","DIDNT","GIVE","THE","DOG","A","CHANCE","TO","GO","THE","ROSES","HAVE","SATELLITE","TV","AND","TONS","OF","JUNK","FOOD","SO","I","BASICALLY","SPENT","THREE","HOURS","A","DAY","ON","MR","ROSES","LAZBOY","WITH","THE","AIR","CONDITIONER","ON","FULL","BLAST"},
				new string[]{"SO","THIS","ONE","DAY","I","FINALLY","FIGURED","OUT","IT","WAS","A","BIG","HASSLE","TO","CLEAN","UP","THIS","DOGS","MESS","EVERY","SINGLE","DAY","SO","I","DECIDED","TO","JUST","SAVE","MYSELF","SOME","TIME","AND","CLEAN","IT","UP","ALL","AT","ONCE"},
				new string[]{"I","LET","THINGS","GO","FOR","ABOUT","A","WEEK","THEN","THE","NIGHT","BEFORE","THE","ROSES","WERE","SUPPOSED","TO","GET","BACK","I","HEADED","UP","THE","HILL","WITH","ALL","MY","CLEANING","STUFF"},
				new string[]{"AND","WOULDNT","YOU","KNOW","IT","THE","ROSES","CAME","HOME","A","DAY","EARLY"},
				new string[]{"TO","MAKE","A","LONG","STORY","SHORT","I","DIDNT","GET","PAID","A","SINGLE","CENT","NOT","EVEN","FOR","THE","DAYS","I","DID","MY","JOB","LIKE","I","WAS","SUPPOSED","TO","SO","HEARING","ALBERT","SANDY","BRAGGING","ABOUT","HIS","TV","JUST","REMINDED","ME","HOW","I","GOT","STIFFED","AND","PUT","ME","IN","A","BAD","MOOD","FOR","THE","REST","OF","THE","DAY"},
				new string[]{"YOU","KNOW","HOW","I","SAID","MOM","IS","ALWAYS","TAKING","MANNYS","SIDE","WELL","TODAY","WAS","SOME","MORE","PROOF","OF","THAT"},
				new string[]{"I","MADE","MANNY","HIS","CEREAL","LIKE","I","ALWAYS","DO","BUT","THIS","TIME","I","ACCIDENTALLY","POURED","THE","MILK","IN","BEFORE","THE","CEREAL","AND","WHEN","I","POURED","THE","CEREAL","IN","ON","TOP","OF","THAT","MANNY","JUST","ABOUT","LOST","HIS","MIND","HE","MADE","A","HUGE","RACKET","CRYING","AND","HOLLERING","AND","ALL","THAT","AND","MOM","CAME","DOWN","TO","SEE","WHAT","WAS","GOING","ON","SO","I","TOLD","HER","WHAT","HAPPENED","AND","I","FIGURED","SHE","WOULD","JUST","TELL","MANNY","TO","PIPE","DOWN","AND","EAT","HIS","STUPID","CEREAL"},
				new string[]{"BUT","INSTEAD","SHE","SAYS","I","WOULDNT","EAT","IT","EITHER","AND","THEN","SHE","GIVES","MANNY","A","BIG","HUG","AND","MAKES","ME","POUR","HIM","A","NEW","BOWL","OF","CEREAL","THIS","TIME","IN","THE","RIGHT","ORDER"},
				new string[]{"I","GUESS","I","SHOULDVE","EXPECTED","IT","A","COUPLE","WEEKS","AGO","WHEN","MANNY","WAS","AT","DAYCARE","HE","OPENED","UP","HIS","LUNCHBOX","AND","WHEN","HE","TOOK","OUT","HIS","SANDWHICH","HE","HAD","A","FIT","BECAUSE","IT","WAS","CUT","IN","TWO","HALVES","NOT","FOUR","SQUARES","LIKE","HE","USUALLY","GETS","IT","SO","THE","DAYCARE","PEOPLE","HAD","TO","CALL","MOM","UP","AT","WORK","TO","GET","HER","TO","COME","OVER","SO","MANNY","WOULD","CALM","DOWN"},
				new string[]{"TODAY","I","GOT","TOTALLY","DISSED","BY","CHRISTINE","COOLIDGE","I","ASKED","HER","IF","SHE","WOULD","BE","MY","LAB","PARTNER","FOR","SCIENCE","AND","SHE","TOLD","ME","SHE","ALREADY","HAD","A","PARTNER"},
				new string[]{"BUT","THEN","LATER","ON","IN","THE","CLASS","I","SAW","HER","WALK","UP","TO","BRYCE","ANDERSON","AND","ASK","HIM","IF","HE","WOULD","BE","HER","LAB","PARTNER","IM","NOT","SURPRISED","SHE","WENT","AFTER","BRYCE","BECAUSE","HE","IS","THE","MOST","POPULAR","KID","IN","OUR","GRADE","FOR","SOMETHING","LIKE","THREE","YEARS","RUNNING","BUT","SHE","DIDNT","HAVE","TO","GO","AND","LIE","ABOUT","IT","EITHER"}
			},
			new string[][]
			{
				new string[]{"IT","USED","TO","BE","A","LOT","MORE","SIMPLE","WITH","GIRLS","WAY","BACK","IN","THE","THIRD","GRADE","THE","DEAL","WAS","IF","YOU","WERE","THE","FASTEST","RUNNER","IN","YOUR","GRADE","YOU","WERE","THE","KING","AND","IN","MY","GRADE","THAT","WAS","RONNIE","JONES"},
				new string[]{"NOWADAYS","ITS","A","LOT","MORE","COMPLICATED","AND","IM","SURE","KIDS","LIKE","RONNIE","JONES","ARE","SITTING","AROUND","SCRATCHING","THEIR","HEADS","WONDERING","WHAT","HAPPENED"},
				new string[]{"NOT","ONLY","DOES","BRYCE","ANDERSON","GET","ALL","THE","GIRLS","BUT","HE","ALSO","HAS","A","BIG","GROUP","OF","CRONIES","THAT","FOLLOWS","HIM","AROUND","AND","BASICALLY","WORSHIPS","EVERY","WORD","THAT","COMES","OUT","OF","HIS","MOUTH"},
				new string[]{"THE","THING","THAT","REALLY","STINKS","IS","THAT","I","HAVE","ALWAYS","BEEN","INTO","GIRLS","BUT","KIDS","LIKE","BRYCE","HAVE","ONLY","COME","AROUND","TO","LIKING","GIRLS","IN","THE","PAST","COUPLE","OF","YEARS","I","REMEMBER","HOW","BRYCE","USED","TO","BE","BACK","IN","FOURTH","AND","FIFTH","GRADE"},
				new string[]{"AND","OF","COURSE","NOW","I","DONT","GET","ANY","CREDIT","FROM","THE","GIRLS","FOR","STICKING","WITH","THEM","FOR","ALL","THIS","TIME"},
				new string[]{"LIKE","I","SAID","BRYCE","IS","THE","MOST","POPULAR","KID","IN","THE","SEVENTH","GRADE","SO","THAT","LEAVES","ALL","THE","REST","OF","US","SCRAMBLING","TO","MOVE","UP","THE","LADDER"},
				new string[]{"THE","BEST","WAY","TO","FIGURE","OUT","HOW","POPULAR","YOU","ARE","IS","TO","GET","A","HOLD","OF","ONE","OF","THE","SLAM","BOOKS","THAT","GETS","PASSED","AROUND","BASICALLY","THEYRE","NOTEBOOKS","WHERE","PEOPLE","PUT","RANKINGS","DOWN","FOR","MOST","POPULAR","BOY","MOST","POPULAR","GIRL","BEST","HAIR","CUTEST","BUTT","AND","ALL","OF","THAT"},
				new string[]{"THE","PROBLEM","WITH","THESE","BOOKS","THOUGH","IS","THAT","THEYRE","IN","REGULAR","NOTEBOOKS","WHICH","ONLY","HAVE",snlets[0] + "" + snlets[0], "LINES","ON","EACH","PAGE","SO","PEOPLE","LIKE","ME","WHO","DONT","MAKE","THE","TOP",snlets[0] + "" + snlets[0], "HAVE","TO","GUESS","WHERE","THEY","RANK","THE","BEST","I","CAN","FIGURE","RIGHT","NOW","IS","THAT","IM","SOMEWHERE","AROUND",snlets[1] + "" + snlets[0] + "ND","OR",snlets[1] + "" + snlets[1] + "RD","MOST","POPULAR","BUT","I","THINK","IM","ABOUT","TO","MOVE","UP","A","RANK","BECAUSE","CHARLIE","DAVIES","(WHO","IS","A","REALLY","NICE","KID)","GETS","HIS","BRACES","ON","TUESDAY"},
				new string[]{"WHAT","SOMEONE","NEEDS","TO","DO","IS","START","UP","A","SLAM","BOOK","WITH","ONE","OF","THOSE","YELLOW","LEGAL","PADS","OF","PAPER","BECAUSE","THEY","HAVE","SOMETHING","LIKE",snlets[1] + "" + snlets[0], "LINES","IN","THEM","SO","AT","LEAST","KIDS","LIKE","ME","COULD","GET","A","BETTER","PICTURE","OF","WHERE","THEY","STAND","BUT","THE","PROBLEM","IS","THAT","IT","TAKES","A","POPULAR","KID","TO","START","A","SLAM","BOOK","AND","IM","SURE","IF","I","STARTED","ONE","IT","WOULD","GET","FILLED","OUT","BY","ALL","NERDS"},
				new string[]{"TODAY","I","GOT","A","HOLD","OF","A","SLAM","BOOK","AND","I","WAS","TRYING","TO","EXPLAIN","ALL","THIS","TO","ROWLEY","ON","THE","BUS","RIDE","HOME","BUT","HONESTLY","SOMETIMES","WITH","HIM","I","FEEL","LIKE","ITS","JUST","IN","ONE","EAR","AND","OUT","THE","OTHER"},
				new string[]{"TONIGHT","DAD","WAS","SHOWING","ME","THE","NEWEST","STUFF","HE","ADDED","TO","HIS","CIVIL","WAR","DIORAMA","IN","THE","BASEMENT","AND","I","GOT","TO","ADMIT","ITS","PRETTY","COOL"},
				new string[]{"DADS","NOT","LIKE","THE","REGULAR","KINDS","OF","DADS","YOU","SEE","ON","TV","SITTING","AROUND","WATCHING","FOOTBALL","AND","DRINKING","BEER","AND","ALL","OF","THAT"}
			},
			new string[][]
			{
				new string[]{"ANY","FREE","SECOND","HE","GETS","YOU","CAN","BE","SURE","HES","DOWN","IN","HIS","WORKROOM","PAINTING","HIS","LITTLE","SOLDIERS","OR","MOVING","STUFF","AROUND","THE","BATTLEFIELD","TRYING","TO","MAKE","IT","AS","ACCURATE","AS","POSSIBLE"},
				new string[]{"DAD","WOULD","BE","HAPPY","TO","SPEND","THE","WHOLE","WEEKEND","WORKING","ON","HIS","DIORAMA","BUT","MOM","USUALLY","HAS","OTHER","IDEAS","MOM","LIKES","TO","RENT","THESE","ROMANTIC","COMEDIES","AND","DAD","HAS","TO","WATCH","THEM","WITH","HER","WHETHER","HE","WANTS","TO","OR","NOT"},
				new string[]{"A","COUPLE","WEEKS","AGO","WHEN","MOM","RENTED","ONE","OF","THESE","MOVIES","DAD","TRIED","TO","GET","CLEVER","AND","FAKE","OUT","MOM","WHEN","SHE","GOT","UP","TO","GO","TO","THE","BATHROOM","DAD","STUFFED","A","BUNCH","OF","PILLOWS","UNDER","THE","BLANKETS","TO","MAKE","IT","SEEM","LIKE","HE","HAD","FALLEN","ASLEEP"},
				new string[]{"SO","MOM","WATCHED","THE","REST","OF","THE","MOVIE","AND","DIDNT","CATCH","ON","THAT","DAD","HAD","MADE","A","DECOY","UNTIL","THE","MOVIE","WAS","OVER"},
				new string[]{"DAD","WAS","IN","THE","DOG","HOUSE","FOR","A","LONG","TIME","AFTER","THAT","ONE"},
				new string[]{"ANOTHER","THING","I","SHOULD","MENTION","ABOUT","DADS","WORKROOM","IS","THAT","HE","IS","REAL","PROTECTIVE","OF","IT","HE","KEEPS","THE","DOOR","BOLTED","SHUT","WITH","ONE","OF","THOSE","COMBINATION","LOCKS","SO","I","HARDLY","EVER","EVEN","GET","TO","STEP","FOOT","IN","THERE"},
				new string[]{"I","DONT","EVEN","THINK","MANNY","KNOWS","THE","DIORAMA","EXISTS","IVE","SEEN","DAD","SAY","SOME","THINGS","TO","MANNY","TO","MAKE","SURE","MANNY","KEEPS","CLEAR","OF","THAT","PART","OF","THE","BASEMENT"},
				new string[]{"ROWLEY","CAME","OVER","TONIGHT","AND","DAD","GETS","REAL","EDGY","WHEN","ROWLEYS","AROUND","FOR","SOME","REASON","DAD","HAS","IT","IN","HIS","HEAD","THAT","ROWLEY","IS","A","KLUTZ","AND","THAT","HES","GOING","TO","BREAK","SOMETHING","EVERY","TIME","HE","COMES","OVER"},
				new string[]{"DAD","TOLD","ME","ABOUT","THIS","NIGHTMARE","HE","ALWAYS","HAS","ABOUT","ROWLEY","RUINING","HIS","BATTLEFIELD","IN","ONE","KLUTZY","MOVE"},
				new string[]{"SO","EVERY","TIME","ROWLEY","COMES","OVER","HE","GETS","THE","SAME","GREETING","THE","BASEMENT","IS","OFFLIMITS"},
				new string[]{"TODAY","I","GOT","OUT","OF","BED","EARLY","BECAUSE","THE","VACUUMING","THING","WAS","MORE","THAN","I","COULD","HANDLE","I","WATCHED","CARTOONS","FOR","A","WHILE","BUT","THEN","I","REMEMBERED","THE","OTHER","REASON","I","DONT","GET","UP","EARLY","ON","SATURDAYS","WITH","DAD","DOING","HIS","CHORES","ALL","AROUND","YOU","IT","MAKES","YOU","FEEL","GUILTY","AND","TAKES","ALL","THE","JOY","OUT","OF","JUST","LAYING","AROUND","DOING","NOTHING"},
				new string[]{"TONIGHT","IM","GOING","TO","SPEND","THE","NIGHT","AT","ROWLEYS","ITS","A","PRETTY","BIG","DEAL","BECAUSE","I","HAVENT","SPENT","THE","NIGHT","OVER","THERE","IN","SOMETHING","LIKE","A","YEAR","AND","A","HALF"},
				new string[]{"THE","MAIN","REASON","I","HAVENT","SLEPT","OVER","THERE","IN","SO","LONG","IS","BECAUSE","ROWLEYS","DAD","REALLY","DOESNT","LIKE","ME"}
			},
			new string[][]
			{
				new string[]{"IT","ALL","GOES","BACK","TO","SOMETHING","THAT","HAPPENED","LAST","JUNE","WE","WERE","WATCHING","SOME","CORNY","MOVIE","ROWLEY","HAD","WHERE","THERE","WERE","THESE","KIDS","THAT","TAUGHT","THEMSELVES","A","SECRET","LANGUAGE","THAT","ONLY","THEY","COULD","UNDERSTAND"},
				new string[]{"ME","AND","ROWLEY","THOUGHT","THE","WHOLE","SECRET","LANGUAGE","THING","WAS","PRETTY","COOL","BUT","WE","COULDNT","FIGURE","OUT","HOW","TO","DO","IT","LIKE","THE","KIDS","IN","THE","MOVIE"},
				new string[]{"SO","WE","DECIDED","TO","MAKE","UP","OUR","OWN","SECRET","LANGUAGE","AND","WE","TRIED","IT","OUT","OVER","DINNER"},
				new string[]{"WELL","ROWLEYS","DAD","MUST","HAVE","CRACKED","OUR","CODE","BECAUSE","THE","NEXT","THING","I","KNEW","I","WAS","GOING","HOME","EARLY"},
				new string[]{"AND","THAT","WAS","THE","LAST","TIME","I","WAS","INVITED","FOR","A","SLEEPOVER"},
				new string[]{"SO","I","DONT","KNOW","WHY","MR","JEFFERSON","IS","GIVING","ME","A","SECOND","CHANCE","EITHER","HES","GOTTEN","OVER","THE","WHOLE","SECRET","LANGUAGE","THING","OR","HES","FORGOTTEN","WHY","HE","DOESNT","LIKE","ME"},
				new string[]{"THE","SLEEPOVER","AT","ROWLEYS","LAST","NIGHT","ENDED","UP","BEING","A","NIGHTMARE","THE","FIRST","HINT","I","HAD","THAT","THINGS","WERE","GOING","TO","GO","WRONG","WAS","WHEN","ROWLEYS","MOM","TOLD","US","THATS","ENOUGH","TV","FOR","THE","NIGHT","AT",snlets[1] + "" + snlets[0] + "" + snlets[0]},
				new string[]{"I","WAS","LIKE","WELL","WHAT","ARE","WE","SUPPOSED","TO","DO","NOW","AND","SHE","SAID","YOU","COULD","READ","A","BOOK"},
				new string[]{"SO","OF","COURSE","I","THOUGHT","SHE","WAS","JOKING","BUT","RIGHT","WHEN","I","WAS","TELLING","ROWLEY","HOW","I","THOUGHT","HIS","MOM","WAS","PRETTY","FUNNY","SHE","SHOWED","UP","AGAIN","WITH","HER","ARMS","FULL","OF","BOOKS"},
				new string[]{"I","REALIZED","RIGHT","THEN","I","WAS","IN","FOR","A","PRETTY","LONG","NIGHT","SINCE","THE","TV","WAS","OFF","LIMITS","VIDEO","GAMES","WERE","OUT","TOO","SO","I","TRIED","TO","THINK","UP","WAYS","WE","COULD","KEEP","OURSELVES","ENTERTAINED","I","BROKE","OUT","SOME","BOARD","GAMES","BUT","ROWLEY","HAD","TO","TAKE","A","BATHROOM","BREAK","SOMETHING","LIKE","EVERY","FIVE","MINUTES","SO","IT","MADE","OUR","GAME","OF","RISK","GO","ON","FOREVER"},
				new string[]{"EVERY","TIME","ROWLEY","CAME","BACK","FROM","A","BATHROOM","BREAK","HE","WOULD","RUN","DOWN","STAIRS","AND","KICK","THIS","GIANT","SOMBRERO","ACROSS","THE","ROOM"},
				new string[]{"IT","WAS","FUNNY","THE","FIRST","TEN","TIMES","OR","SO","BUT","AFTER","A","WHILE","IT","REALLY","STARTED","GETTING","ON","MY","NERVES","SO","THIS","ONE","TIME","WHEN","HE","WAS","UPSTAIRS","I","PUT","ONE","OF","HIS","DADS","DUMBELLS","UNDER","THE","HAT","TO","SEE","IF","HE","WOULD","STILL","KICK","IT"},
				new string[]{"AND","SURE","ENOUGH","ROWLEY","COMES","RUNNING","DOWN","THE","STAIRS","AND","GIVES","THE","HAT","A","BIG","KICK"},
				new string[]{"ROWLEYS","DAD","WAS","DOWN","THE","STAIRS","IN","NO","TIME","FLAT","I","DONT","THINK","ROWLEY","KNEW","I","PUT","THE","DUMBELL","UNDER","THE","HAT","BUT","ROWLEYS","DAD","SEEMED","PRETTY","SUSPICIOUS"}
			},
			new string[][]
			{
				new string[]{"ANYWAY","I","GUESS","HE","DIDNT","HAVE","ENOUGH","HARD","EVIDENCE","OR","HE","WOULD","HAVE","SENT","ME","HOME","RIGHT","THEN","I","FELT","A","LITTLE","BIT","BAD","ABOUT","DOING","WHAT","I","DID","BUT","IF","YOU","THINK","ABOUT","IT","IF","ROWLEYS","PARENTS","HADNT","MAKE","US","TURN","OFF","THE","TV","THIS","NEVER","WOULD","HAVE","HAPPENED"},
				new string[]{"AT",snlets[1] + "" + snlets[0] + "" + snlets[0], "ROWLEYS","MOM","CAME","DOWN","TO","SAY","IT","WAS","LIGHTS","OUT","IF","I","WOULDVE","KNOWN","ROWLEYS","BEDTIME","ON","WEEKENDS","WAS",snlets[1] + "" + snlets[0] + "" + snlets[0], "BELIEVE","ME","I","NEVER","WOULD","HAVE","COME","OVER"},
				new string[]{"AND","THEN","I","FOUND","OUT","ANOTHER","UGLY","SURPRISE","THERE","WAS","NO","GUEST","BED","SO","I","HAD","TO","SLEEP","IN","THE","SAME","BED","WITH","ROWLEY","I","TRIED","TO","LAY","AS","FAR","AWAY","FROM","ROWLEY","AS","POSSIBLE","BUT","IT","WAS","IMPOSSIBLE","TO","GET","TO","SLEEP","WITH","HALF","OF","MY","BODY","HANGING","OFF","THE","BED"},
				new string[]{"ROWLEY","FELL","ASLEEP","RIGHT","AWAY","BUT","IT","MUSTVE","TAKEN","ME","TWO","HOURS","BUT","RIGHT","WHEN","I","FINALLY","STARTED","TO","DRIFT","OFF","ROWLEY","LETS","OUT","THIS","SCREAM","WHICH","SCARED","ME","SO","BAD","I","DROPPED","RIGHT","OUT","OF","THE","BED","AND","ONTO","THE","HARDWOOD","FLOOR"},
				new string[]{"ROWLEYS","PARENTS","CAME","RUNNING","IN","AND","ROWLEY","STARTED","BABBLING","ALL","OF","THIS","INCOHERENT","GIBBERISH"},
				new string[]{"IT","TURNS","OUT","HE","HAD","A","NIGHTMARE","THAT","A","CHICKEN","WAS","HIDING","UNDERNEATH","HIM","AND","THATS","WHAT","MADE","HIM","YELL","OUT","BUT","I","THINK","ROWLEY","WAS","SO","OUT","OF","IT","HE","DIDNT","REALLY","REALIZE","IT","WAS","JUST","A","DREAM"},
				new string[]{"SO","ROWLEYS","PARENTS","TOOK","HIM","INTO","THEIR","ROOM","AND","SPENT","THE","NEXT","TWENTY","MINUTES","CALMING","HIM","DOWN","AND","TELLING","HIM","IT","WAS","JUST","A","DREAM","AND","HOW","THERE","REALLY","WAS","NO","CHICKEN"},
				new string[]{"MAN","IF","I","WOKE","MY","DAD","UP","WITH","SOME","NONSENSE","ABOUT","A","CHICKEN","YOU","BETTER","BELIEVE","HE","WOULDNT","BE","GIVING","ME","A","BIG","HUG","AND","TELLING","ME","EVERYTHING","WAS","OK","BUT","THAT","JUST","GOES","TO","SHOW","HOW","DIFFERENT","MY","PARENTS","ARE","FROM","ROWLEYS"},
				new string[]{"AND","I","JUST","WANTED","TO","MAKE","A","NOTE","THAT","NOBODY","SEEMED","ALL","THAT","CONCERNED","THAT","I","TOOK","A","THREEFOOT","FALL","ONTO","THE","FLOOR","EVEN","THOUGH","THAT","HAPPENED","FOR","REAL","AND","NOT","JUST","IN","SOME","STUPID","DREAM"},
				new string[]{"I","THINK","ROWLEY","SPENT","THE","NIGHT","IN","HIS","PARENTS","BED","WHICH","WAS","JUST","FINE","BY","ME","BECAUSE","WITHOUT","ROWLEY","AND","HIS","NIGHTMARES","I","WAS","FINALLY","ABLE","TO","GET","SOME","SLEEP"},
				new string[]{"BUT","THE","SECOND","I","WOKE","UP","THIS","MORNING","I","CAME","HOME","AND","POURED","MYSELF","A","BIG","BOWL","OF","JUNK","CEREAL","AND","DID","MY","BEST","TO","FORGET","ABOUT","THE","WHOLE","EXPERIENCE"},
				new string[]{"I","COULDNT","WAIT","FOR","SCHOOL","TO","BE","OVER","WITH","TODAY","SO","I","COULD","GO","HOME","AND","PLAY","TWISTED","WIZARD","A","VIDEO","GAME","IVE","BEEN","PLAYING","FOR","FIVE","DAYS","STRAIGHT"}
			},
			new string[][]
			{
				new string[]{"THE","ONLY","PROBLEM","WITH","TWISTED","WIZARD","IS","THAT","YOU","CANT","SAVE","YOUR","PROGRESS","SO","YOU","HAVE","TO","JUST","LEAVE","IT","ON","ALL","THE","TIME","SO","IMAGINE","HOW","I","FELT","TODAY","WHEN","I","REALIZED","I","GOT","HOME","ABOUT","FIVE","SECONDS","TOO","LATE"},
				new string[]{"BELIEVE","ME","FROM","NOW","ON","I","AM","GOING","TO","PUT","A","PIECE","OF","BLACK","TAPE","OVER","THE","POWER","LIGHT","SO","IT","NEVER","HAPPENS","AGAIN"},
				new string[]{"I","DONT","KNOW","IF","IVE","MENTIONED","IT","BEFORE","BUT","I","AM","SUPER","GOOD","AT","VIDEO","GAMES","I","DONT","KNOW","ANYONE","WHO","HAS","BEAT","AS","MANY","GAMES","AS","ME","AND","IVE","GOT","ALL","MY","VICTORIES","ON","VIDEO","TAPE","TO","PROVE","IT"},
				new string[]{"UNFORTUNATELY","DAD","DOES","NOT","EXACTLY","APPRECIATE","MY","VIDEO","GAME","SKILLS","HE","IS","ALWAYS","GETTING","ON","ME","ABOUT","GOING","OUTSIDE","AND","DOING","SOMETHING","ACTIVE"},
				new string[]{"SO","TODAY","I","TRIED","TO","EXPLAIN","TO","HIM","THAT","WITH","VIDEO","GAMES","YOU","CAN","PLAY","SPORTS","LIKE","FOOTBALL","AND","SOCCER","AND","YOU","DONT","EVEN","HAVE","TO","GET","ALL","HOT","AND","SWEATY"},
				new string[]{"BUT","AS","USUAL","DAD","DIDNT","GET","MY","LOGIC","HES","A","PRETTY","SMART","GUY","IN","GENERAL","BUT","WHEN","IT","COMES","TO","COMMON","SENSE","SOMETIMES","I","WONDER","ABOUT","HIM"},
				new string[]{"ANYWAYS","THATS","HOW","I","FOUND","MYSELF","SHUT","OUT","OF","THE","HOUSE","THIS","AFTERNOON"},
				new string[]{"IM","SURE","DAD","WOULD","DISMANTLE","MY","VIDEO","GAME","SYSTEM","IF","HE","COULD","FIGURE","OUT","HOW","TO","BUT","LUCKILY","THE","PEOPLE","WHO","MAKE","THESE","THINGS","MAKE","THEM","PARENTPROOF"},
				new string[]{"SO","LIKE","I","SAID","I","WAS","SHUT","OUT","OF","THE","HOUSE","LOOKING","FOR","SOME","WAY","TO","ENTERTAIN","MYSELF"},
				new string[]{"WHAT","I","ALWAYS","DO","WHEN","DAD","MAKES","ME","GO","OUTSIDE","IS","I","JUST","GO","OVER","TO","COLLINS","HOUSE"},
				new string[]{"IM","NOT","A","HUGE","FAN","OF","COLLINS","BUT","HE","HAS","TWO","THINGS","GOING","FOR","HIM","ONE","HE","DOESNT","MIND","WATCHING","ME","PLAY","HIS","VIDEO","GAMES","AND","TWO","HIS","DAD","HAS","EVERY","SPIDER","MAN","COMIC","BOOK","SINCE",snlets[1] + "" + snlets[1] + "" + snlets[0] + "" + snlets[0]},
				new string[]{"I","WOULD","GO","OVER","TO","ROWLEYS","HOUSE","TO","PLAY","VIDEO","GAMES","BUT","HE","ALWAYS","WANT","TO","TAKE","TURNS","WHICH","REALLY","BREAKS","MY","CONCENTRATION"},
				new string[]{"PLUS","ROWLEY","DOESNT","UNDERSTAND","THAT","IF","HE","PLAYS","USING","MY","MEMORY","CARD","IT","WILL","REALLY","SCREW","UP","MY","STATS"},
				new string[]{"THE","OTHER","THING","ABOUT","ROWLEY","IS","THAT","HES","NOT","A","SERIOUS","GAMER","LIKE","ME","HES","GOT","THIS","ONE","RACING","GAME","CALLED","FORMULA","ONE","RACING","IF","YOU","EVER","WANT","TO","BEAT","HIM","IN","IT","JUST","NAME","YOUR","CAR","SOMETHING","STUPID","AND","WATCH","WHAT","HAPPENS"},
				new string[]{"SO","TODAY","I","PLAYED","TWISTED","WIZARD","OVER","AT","COLLINS","UNTIL","IT","WAS","TIME","TO","COME","HOME","FOR","DINNER"}
			},
			new string[][]
			{
				new string[]{"ON","MY","WAY","UP","THE","HILL","I","MADE","SURE","TO","JUMP","THROUGH","THE","THOMPSONS","SPRINKLER","TO","MAKE","IT","LOOK","LIKE","I","WAS","ALL","HOT","AND","SWEATY","AND","THEN","I","TIMED","MY","ENTRANCE","PERFECTLY"},
				new string[]{"SO","MY","TRICK","WORKED","ON","DAD","BUT","IT","KIND","OF","BACKFIRED","WITH","MOM","BECAUSE","WHEN","SHE","SAW","ME","SHE","MADE","ME","TAKE","A","SHOWER","BEFORE","DINNER"},
				new string[]{"DAD","MUST","HAVE","BEEN","PRETTY","PROUD","OF","HIMSELF","FOR","HIS","IDEA","TO","KICK","ME","OUT","OF","THE","HOUSE","YESTERDAY","BECAUSE","HE","DID","IT","AGAIN","TODAY"},
				new string[]{"I","WAS","ACTUALLY","GOING","TO","GO","OUTSIDE","ANYWAY","BECAUSE","ROWLEY","HAD","GOTTEN","THIS","NEW","MODEL","ROCKET","WE","WANTED","TO","TRY","OUT","SO","WE","WENT","DOWN","TO","THE","SCHOOL","AND","SET","IT","OFF","BUT","THE","WIND","CARRIED","IT","ALL","THE","WAY","TO","THE","WOODS","AT","THE","END","OF","THE","FOOTBALL","FIELD"},
				new string[]{"I","DONT","KNOW","IF","I","EVER","MENTIONED","IT","BEFORE","BUT","THERES","A","BULLY","WHO","HANGS","OUT","IN","THOSE","WOODS","NAMED","HERBIE","REAMER","KIDS","LIKE","ME","AND","ROWLEY","STAY","AS","FAR","AWAY","FROM","THOSE","WOODS","AS","POSSIBLE"},
				new string[]{"ITS","A","REAL","PITY","TOO","BECAUSE","LIKE","I","SAID","IT","WAS","A","BRANDNEW","ROCKET"},
				new string[]{"I","HAVE","NO","IDEA","HOW","OLD","HERBIE","REAMER","IS","OR","WHERE","HE","LIVES","I","GUESS","ITS","POSSIBLE","THAT","HE","LIVES","RIGHT","THERE","IN","THE","WOODS","LIKE","A","WILD","ANIMAL","ALL","I","KNOW","IS","THAT","HES","BEEN","AROUND","LONG","ENOUGH","THAT","HE","TERRORIZED","RODRICK","AND","HIS","FRIENDS","WHEN","THEY","WERE","IN","MY","GRADE"},
				new string[]{"THE","THING","THAT","STINKS","IS","THAT","HERBIE","REAMERS","WOODS","ARE","RIGHT","BETWEEN","MY","HOUSE","AND","THE","SCHOOL","SO","IF","WE","COULD","CUT","THROUGH","THE","WOODS","IT","WOULD","SAVE","US","SOMETHING","LIKE","TWENTY","MINUTES","OF","WALKING"},
				new string[]{"I","WAS","TELLING","DAD","ALL","ABOUT","HERBIE","REAMER","THE","OTHER","DAY","DAD","TOLD","ME","ABOUT","THE","BULLY","FROM","WHEN","HE","WAS","GROWING","UP","SAM","SHARMAN"},
				new string[]{"DAD","SAID","SAM","SHARMAN","DID","THIS","PINCH","WHERE","HE","GRABBED","YOUR","SKIN","AND","TWISTED","IT","AROUND","TWO","TIMES"},
				new string[]{"DAD","TOLD","ME","THE","WAY","ALL","THE","NEIGHBORHOOD","KIDS","DEALT","WITH","SAM","SHARMAN","WAS","THAT","THEY","BANDED","TOGETHER","AND","TOLD","THE","PRINCIPAL","ON","HIM","DAD","SAID","SAM","CRIED","AND","THAT","HE","NEVER","DID","THE","SAM","SHARMAN","PINCH","AGAIN","AND","NOW","HES","AN","AIR","CONDITIONER","REPAIRMAN","AND","APPARENTLY","NOW","HES","A","REALLY","NICE","GUY"},
				new string[]{"WELL","FROM","THE","SOUND","OF","SAM","SHARMAN","HE","WOULDNT","LAST","TWO","SECONDS","AGAINST","HERBIE","REAMER","BUT","I","DIDNT","WANT","TO","HURT","DADS","FEELINGS","SO","I","JUST","TRIED","TO","ACT","IMPRESSED","BY","HIS","STORY"},
				new string[]{"AFTER","WE","LOST","THE","ROCKET","WE","WENT","UP","TO","ROWLEYS","TO","PLAY","CARDS","BUT","I","LOST","TRACK","OF","TIME","AND","I","WAS","LATE","FOR","DINNER","AT","MY","HOUSE","SO","ON","MY","WAY","DOWN","THE","HILL","I","TRIED","TO","THINK","OF","A","GOOD","EXCUSE","TO","GET","ME","OUT","OF","TROUBLE","WITH","MOM"}
			},
			new string[][]
			{
				new string[]{"MOM","WAS","PRETTY","HOT","AT","ME","FOR","BEING","LATE","JUST","LIKE","I","EXPECTED","SO","I","TOLD","HER","THAT","THE","CLOCK","IN","ROWLEYS","KITCHEN","MUST","BE","WRONG","AND","THAT","I","THOUGHT","I","WAS","RIGHT","ON","TIME"},
				new string[]{"AND","DO","YOU","KNOW","WHAT","MOM","DID","SHE","CALLED","ROWLEYS","MOM","AND","CAUGHT","ME","RED","HANDED"},
				new string[]{"SO","MOM","WAS","REALLY","MAD","THAT","I","LIED","BUT","AS","FAR","AS","BEING","TRICKY","GOES","MOM","SHOULDNT","BLAME","ME","BECAUSE","I","LEARNED","EVERYTHING","I","KNOW","FROM","HER"},
				new string[]{"I","REMEMBER","THIS","ONE","TIME","WHEN","I","WAS","IN","THE","SECOND","GRADE","AND","MOM","COULDNT","GET","ME","TO","BRUSH","MY","TEETH","SO","SHE","MADE","THIS","PRETEND","CALL","TO","THE","DENTIST","AND","I","TOTALLY","FELL","FOR","IT"},
				new string[]{"IN","FACT","THATS","WHEN","I","STARTED","BRUSHING","MY","TEETH","FIVE","TIMES","A","DAY"},
				new string[]{"MOM","SAID","SHE","WAS","GOING","TO","THINK","ABOUT","WHAT","MY","PUNISHMENT","SHOULD","BE","FOR","TELLING","A","LIE","AND","SHED","LET","ME","KNOW","AS","SOON","AS","SHE","CAME","UP","WITH","SOMETHING","THAT","FIT","THE","CRIME"},
				new string[]{"SEE","THATS","THE","DIFFERENCE","BETWEEN","MOM","AND","DAD","DAD","IS","PRETTY","SIMPLE","IF","YOU","MESS","UP","IN","FRONT","OF","HIM","HE","JUST","THROWS","WHATEVER","IS","IN","HIS","HAND","AT","YOU"},
				new string[]{"BUT","MOMS","A","LOT","MORE","CRAFTY","WITH","HER","PUNISHMENTS","SHE","THINKS","ABOUT","IT","FOR","A","FEW","DAYS","AND","THE","WAITING","ENDS","UP","BEING","JUST","ABOUT","AS","BAD","AS","THE","PUNISHMENT"},
				new string[]{"IN","THE","MEANTIME","YOU","END","UP","DOING","ALL","THESE","NICE","THINGS","HOPING","ITLL","GET","YOU","OFF","EASIER"},
				new string[]{"BUT","THEN","AFTER","A","FEW","DAYS","JUST","WHEN","YOU","FORGET","ABOUT","THE","PUNISHMENT","THATS","COMING","THATS","WHEN","SHE","GETS","YOU"},
				new string[]{"THE","ONLY","GOOD","THING","ABOUT","MOMS","PUNISHMENTS","IS","THAT","SHES","PRETTY","SOFT","SO","IF","YOU","JUST","LAY","LOW","FOR","A","WHILE","YOU","CAN","PRETTY","MUCH","ALWAYS","GET","OUT","OF","THE","PUNISHMENT","EARLY"},
				new string[]{"ANYWAY","THATS","WHAT","IM","COUNTING","ON","WHILE","I","WAIT","FOR","THIS","PUNISHMENT","TO","GET","HANDED","DOWN"},
				new string[]{"WELL","NOW","IVE","GONE","AND","DONE","IT","REMEMBER","HOW","I","SAID","YESTERDAY","THAT","WHILE","YOURE","WAITING","FOR","MOM","TO","HAND","DOWN","HER","PUNISHMENT","YOU","END","UP","DOING","ALL","THIS","GOOD","STUFF","TO","MAKE","HER","CHANGE","HER","MIND","IT","WAS","THAT","KIND","OF","THINKING","THAT","GOT","ME","IN","THIS","EXTRA","TROUBLE","IM","IN","NOW"},
				new string[]{"TODAY","AFTER","SCHOOL","I","THOUGHT","MAYBE","IF","I","WASHED","MOMS","CAR","BEFORE","SHE","GOT","HOME","SHE","MIGHT","GO","EASY","ON","ME","WITH","THE","PUNISHMENT","THING","SO","THATS","WHAT","I","DID","BUT","I","MADE","THE","MISTAKE","OF","USING","BRILLO","TO","RUB","OFF","ALL","THE","BUGS","AND","TAR","SPOTS","SO","I","WAS","IN","FOR","THE","SURPRISE","OF","MY","LIFE","WHEN","I","RINSED","THE","CAR","OFF"}
			},
			new string[][]
			{
				new string[]{"I","TOTALLY","RUINED","THE","PAINT","JOB","I","THOUGHT","ABOUT","JUST","DENYING","EVERYTHING","WHEN","MOM","ASKED","ME","HOW","HER","CAR","GOT","SCRATCHED","UP","BUT","MANNY","WAS","THERE","TO","SEE","EVERYTHING"},
				new string[]{"MANNY","HAS","BEEN","TELLING","ON","ME","SINCE","HE","COULD","TALK","IN","FACT","HE","HAS","TOLD","ON","ME","FOR","STUFF","I","DID","BEFORE","HE","COULD","TALK","ONE","TIME","WHEN","I","WAS","EIGHT","I","BROKE","THE","SLIDING","GLASS","DOOR","BUT","MOM","AND","DAD","COULDNT","PEG","IT","ON","ME","BUT","MANNY","WAS","THERE","TO","WITNESS","IT","AND","HE","SQUEALED","ON","ME","THREE","YEARS","LATER"},
				new string[]{"SO","AFTER","MANNY","STARTED","TALKING","I","HAD","TO","START","WORRYING","ABOUT","ALL","THE","BAD","THINGS","HE","SAW","ME","DO","WHEN","HE","WAS","REAL","SMALL"},
				new string[]{"I","USED","TO","BE","A","TATTLE","TALE","MYSELF","UNTIL","I","LEARNED","MY","LESSON","THIS","ONE","TIME","I","TOLD","ON","RODRICK","FOR","SAYING","A","BAD","WORD","MOM","ASKED","ME","WHICH","WORD","HE","SAID","SO","I","SPELLED","IT","OUT","AND","IT","WAS","A","LONG","ONE","TOO","NOT","ONLY","DID","RODRICK","GET","AWAY","SCOT","FREE","BUT","I","GOT","PUNISHED","FOR","KNOWING","HOW","TO","SPELL","A","BAD","WORD"},
				new string[]{"LIKE","I","SAID","I","KNEW","I","COULDNT","TALK","MANNY","OUT","OF","TELLING","ON","ME","FOR","USING","THE","BRILLO","ON","THE","CAR","SO","I","DECIDED","TO","TRY","A","TRICK","IVE","BEEN","SAVING","UP","FOR","A","SITUATION","LIKE","THIS"},
				new string[]{"I","PACKED","UP","A","BAG","AND","MADE","IT","SEEM","LIKE","I","WAS","GOING","TO","RUN","AWAY","FROM","HOME","RATHER","THAN","FACE","MOM","AND","DAD","FOR","WHAT","I","DID"},
				new string[]{"I","GOT","TO","GIVE","RODRICK","CREDIT","FOR","THIS","ONE","HE","USED","TO","PULL","IT","ON","ME","WHEN","HE","DID","SOMETHING","BAD","AND","HE","KNEW","I","WAS","GOING","TO","TELL","ON","HIM"},
				new string[]{"HED","BASICALLY","JUST","WALK","OUTSIDE","AND","THEN","COME","HOME","FIVE","MINUTES","LATER","BUT","BY","THAT","TIME","I","WAS","IN","PIECES","AND","COULDNT","EVEN","REMEMBER","THE","BAD","THING","THAT","HE","DID"},
				new string[]{"SO","I","LEFT","THE","HOUSE","AND","WAITED","FIVE","MINUTES","AND","THEN","CAME","BACK","INSIDE","I","WAS","EXPECTING","TO","FIND","MANNY","IN","THE","FOYER","BAWLING","LIKE","A","BABY","BUT","HE","WASNT","THERE","AT","ALL"},
				new string[]{"I","WENT","AROUND","THE","HOUSE","LOOKING","FOR","HIM","AND","I","STARTED","TO","GET","REALLY","NERVOUS","BECAUSE","IM","NOT","SUPPOSED","TO","LEAVE","MANNY","ALONE"},
				new string[]{"BUT","I","FOUND","HIM","IN","THE","KITCHEN","AND","GUESS","WHAT","HE","WAS","HALFWAY","FINISHED","WITH","MY","GIANT","HERSHEYS","KISS","IVE","BEEN","SAVING","SINCE","SUMMER"},
				new string[]{"SO","THINGS","TURNED","FROM","BAD","TO","WORSE"},
				new string[]{"WHEN","MOM","GOT","HOME","I","ACTUALLY","SPILLED","THE","BEANS","TO","HER","ABOUT","THE","CAR","AS","FAST","AS","I","COULD"},
				new string[]{"I","WAS","BASICALLY","TRYING","TO","KEEP","MANNY","QUIET","ABOUT","HOW","I","LEFT","THE","HOUSE","WHICH","WOULD","HAVE","GOTTEN","ME","IN","A","LOT","MORE","TROUBLE","THAN","RUINING","HER","CAR"}
			},
			new string[][]
			{
				new string[]{"MOM","JUST","LISTENED","WITH","A","FROWN","ON","HER","FACE","AND","THEN","TOLD","ME","WED","HAVE","TO","WAIT","UNTIL","DAD","GOT","HOME","TO","SEE","WHAT","HE","THOUGHT","ABOUT","WHAT","I","DID"},
				new string[]{"I","FIGURED","SHE","MIGHT","DO","THAT","SO","I","PULLED","ANOTHER","TRICK","I","LEARNED","FROM","RODRICK","I","INVITED","GRAMMA","OVER","FOR","DINNER","I","FIGURED","NOTHING","TOO","BAD","COULD","HAPPEN","IF","SHE","WAS","AROUND","AND","I","KNEW","I","COULD","USE","ANY","PROTECTION","I","COULD","GET"},
				new string[]{"SO","AT",snlets[0] + "" + snlets[0] + "" + snlets[0], "ON","THE","DOT","DAD","CAME","HOME","AND","OF","COURSE","HES","IN","A","GREAT","MOOD","FOR","SOME","REASON","THAT","ALWAYS","HAPPENS","TO","ME","WHEN","IM","IN","WAITFORDADTOGETHOME","KIND","OF","TROUBLE","IT","STINKS","BECAUSE","YOU","KNOW","HES","JUST","GONNA","BE","THAT","MUCH","MADDER","WHEN","HE","FINDS","OUT","WHAT","YOU","DID"},
				new string[]{"MOM","KEPT","QUIET","BECAUSE","GRAMMA","WAS","AROUND","SO","AT","LEAST","THAT","PART","OF","MY","PLAN","WORKED","OUT","AFTER","DINNER","I","JUST","SNUCK","UP","TO","MY","ROOM","AS","QUIETLY","AS","I","COULD","I","THINK","MOM","IS","TELLING","DAD","ABOUT","THE","CAR","RIGHT","NOW","BECAUSE","ITS","REAL","QUIET","DOWNSTAIRS"},
				new string[]{"RODRICK","HASNT","MADE","ME","FEEL","ANY","BETTER","ABOUT","THIS","WHOLE","THING","WHENEVER","IM","IN","TROUBLE","HE","TELLS","ME","THAT","IM","PROBABLY","GOING","TO","GET","THE","BELT"},
				new string[]{"AS","FAR","AS","I","KNOW","DAD","DOESNT","BELIEVE","IN","THAT","KIND","OF","PUNISHMENT","BUT","RODRICK","ALWAYS","SEEMS","TO","THINK","DAD","IS","GOING","TO","MAKE","AN","EXCEPTION","FOR","ME"},
				new string[]{"I","HAVE","DONE","MY","BEST","TO","MAKE","SURE","DAD","READS","ARTICLES","FROM","PARENTING","MAGAZINES","FROM","TIME","TO","TIME","TO","LET","HIM","KNOW","THAT","KIND","OF","THING","DOESNT","FLY","THESE","DAYS"},
				new string[]{"I","JUST","CUT","THOSE","ARTICLES","OUT","AND","SLIP","THEM","INTO","WHATEVER","BOOK","HES","READING","AT","THE","TIME"},
				new string[]{"I","THINK","I","HEAR","DAD","HEADING","UP","THE","STAIRS","NOW"},
				new string[]{"IF","THESE","ARE","THE","LAST","WORDS","I","EVER","WRITE","THEN","I","LEAVE","ALL","MY","COMIC","BOOKS","AND","ACTION","FIGURES","TO","ROWLEY","AND","PLEASE","THROW","THE","OTHER","HALF","OF","MY","HERSHEYS","KISS","AWAY","SO","MANNY","DOESNT","GET","IT"},
				new string[]{"WELL","I","LIVED","TO","SEE","ANOTHER","DAY","LAST","NIGHT","WHEN","DAD","KNOCKED","ON","MY","DOOR","I","PEEKED","THROUGH","THE","CRACK","AND","SAW","THAT","HE","WASNT","WEARING","THE","BELT","SO","I","LET","HIM","IN"},
				new string[]{"DAD","WASNT","EVEN","ALL","THAT","MAD","MOSTLY","BECAUSE","IT","WASNT","HIS","CAR","THAT","GOT","SCRAPED","UP","SO","HE","JUST","TOLD","ME","NOT","TO","USE","BRILLO","TO","CLEAN","A","CAR","AGAIN","AND","THAT","WAS","THAT"},
				new string[]{"MOM","WAS","ANOTHER","STORY","HER","PUNISHMENT","FOR","RUINING","HER","CAR","WAS","THAT","I","HAVE","TO","CLEAN","THE","WHOLE","BASEMENT"},
				new string[]{"AND","SHE","SAID","IF","SHE","CATCHES","ME","LYING","AGAIN","SHELL","TAKE","AWAY","MY","VIDEO","GAMES","FOR","GOOD","SO","I","BETTER","BE","PRETTY","HONEST","FROM","NOW","ON"},
				new string[]{"SO","NOW","THAT","ALL","THAT","STUFF","WAS","BEHIND","ME","I","COULD","SWITCH","MY","FOCUS","BACK","TO","SCHOOL"}
			},
			new string[][]
			{
				new string[]{"WE","JUST","GOT","OUR","FIRST","BIG","ASSIGNMENT","IN","ENGLISH","WHICH","IS","TO","DO","A","BOOK","REPORT","IVE","BEEN","MILKING","THE","SAME","BOOK","FOR","THE","PAST","FIVE","YEARS","ENCYCLOPEDIA","BROWN","DOES","IT","AGAIN","THERE","ARE","ABOUT",snlets[1] + "" + snlets[0], "SHORT","STORIES","IN","THERE","BUT","I","ALWAYS","JUST","TREAT","ONE","SHORT","STORY","LIKE","ITS","THE","WHOLE","BOOK","AND","THE","TEACHER","NEVER","NOTICES"},
				new string[]{"AND","TO","GIVE","YOU","AN","IDEA","OF","HOW","SHORT","EACH","STORY","IS","I","CAN","FINISH","ONE","IN","ABOUT","THREE","AND","A","HALF","MINUTES","WITHOUT","REALLY","TRYING"},
				new string[]{"THESE","ENCYCLOPEDIA","BROWN","STORIES","ARE","ALWAYS","THE","SAME","ITS","ALWAYS","ABOUT","HOW","SOMEBODY","COMMITS","SOME","LAME","CRIME","LIKE","STEALING","A","FISH","AND","THEN","ENCYCLOPEDIA","FIGURES","OUT","WHO","DID","IT","AND","MAKES","THEM","LOOK","STUPID"},
				new string[]{"I","HAVE","TO","SAY","THAT","NO","MATTER","HOW","HARD","I","TRY","I","HAVE","NEVER","FIGURED","OUT","ONE","OF","THESE","STORIES","BEFORE","THE","END","SO","I","GUESS","IM","NOT","AS","BRAINY","AS","ENCYCLOPEDIA"},
				new string[]{"IM","KIND","OF","AN","EXPERT","AT","WRITING","THESE","BOOK","REPORTS","BY","NOW","SO","I","KNOW","HOW","TO","WRITE","EXACTLY","WHAT","A","TEACHER","WANTS","TO","HEAR"},
				new string[]{"SO","FOR","ME","BOOK","REPORTS","ARE","NO","SWEAT"},
				new string[]{"I","FORGOT","TO","MENTION","THAT","THIS","AFTERNOON","I","TOOK","CARE","OF","PART","ONE","OF","MOMS","PUNISHMENT"},
				new string[]{"NOW","IVE","JUST","GOT","TO","KEEP","MY","NOSE","CLEAN","FOR","A","WHILE","AND","ILL","BE","OFF","THE","HOOK"},
				new string[]{"SO","FAR","ITS","BEEN","ABOUT",snlets[1] + "" + snlets[1], "HOURS","AND","IVE","KEPT","MY","PROMISE","TO","MOM","ABOUT","THE","WHOLE","HONESTY","THING","IT","REALLY","HASNT","BEEN","AS","BAD","AS","I","THOUGHT","AND","IN","FACT","IT","HAS","BEEN","KIND","OF","LIBERATING"},
				new string[]{"IVE","BEEN","IN","A","COUPLE","SITUATIONS","ALREADY","WHERE","IVE","BEEN","A","LOT","MORE","TRUTHFUL","THAN","I","WOULD","HAVE","BEEN","JUST","A","DAY","OR","TWO","AGO","LIKE","TODAY","WHEN","MAX","SMEDLEY","STARTED","TELLING","ME","HIS","BIG","PLANS"},
				new string[]{"AND","AT","ROWLEYS","GRANDPAS","BIRTHDAY","PARTY"},
				new string[]{"BUT","MOST","PEOPLE","DONT","SEEM","TO","REALLY","APPRECIATE","A","PERSON","AS","HONEST","AS","ME","LETS","JUST","SAY","I","CANT","UNDERSTAND","HOW","GEORGE","WASHINGTON","EVER","GOT","ELECTED","PRESIDENT"},
				new string[]{"IM","ACTUALLY","LOOKING","FORWARD","TO","TOMORROW","BECAUSE","THERE","ARE","A","COUPLE","OF","TEACHERS","WHO","COULD","USE","A","GOOD","DOES","OF","TRUTHFULNESS"},
				new string[]{"I","THINK","THE","HONESTY","PROMISE","HAS","BEEN","CANCELLED","AND","NOT","BY","ME","BUT","BY","MOM","TODAY","I","ANSWERED","THE","PHONE","AND","IT","WAS","MRS","GRETCHEN","FROM","THE","PTA","AND","SHE","WANTED","TO","TALK","TO","MOM","BUT","MOM","SIGNALED","TO","ME","THAT","I","SHOULD","TELL","MRS","GRETCHEN","THAT","SHE","WASNT","HOME","I","DIDNT","KNOW","IF","IT","WAS","A","TRICK","OR","WHAT","BUT","I","KNEW","THAT","I","WASNT","GOING","TO","GO","AND","BREAK","MY","HONESTY","STREAK","OVER","A","THING","LIKE","THIS","SO","I","MADE","MOM","GO","OUTSIDE","ON","THE","FRONT","PORCH","BEFORE","I","WOULD","SAY","A","WORD","TO","MRS","GRETCHEN"}
			},
			new string[][]
			{
				new string[]{"MOM","DIDNT","COME","RIGHT","OUT","AND","SAY","THE","HONESTY","DEAL","IS","OFF","BUT","SHE","DIDNT","SPEAK","TO","ME","FOR","THE","REST","OF","THE","NIGHT","SO","I","FIGURE","I","CAN","JUST","GO","BACK","TO","HOW","I","WAS","BEFORE"},
				new string[]{"THE","ONLY","OTHER","THING","THAT","HAPPENED","TODAY","THATS","WORTH","MENTIONING","IS","THAT","RODRICK","BROKE","HIS","OWN","SATURDAY","SLEEPING","RECORD","AT",snlets[1] + "" + snlets[1] + "" + snlets[0], "DAD","SAID","ENOUGH","IS","ENOUGH","AND","MADE","RODRICK","GET","OUT","OF","HIS","BED","IN","THE","BASEMENT"},
				new string[]{"BUT","RODRICK","JUST","TOOK","ALL","HIS","SLEEPING","GEAR","UPSTAIRS","AND","PLOPPED","HIMSELF","ON","THE","COUCH","UNTIL","IT","WAS","TIME","FOR","DINNER"},
				new string[]{"EVER","SINCE","MOM","DANGLED","THE","IDEA","OF","TAKING","AWAY","MY","VIDEO","GAMES","IVE","BEEN","TRYING","TO","BE","ON","MY","BEST","BEHAVIOR","SO","YOU","WOULDNT","THINK","I","COULD","GET","IN","TROUBLE","IN","THE","FIFTEEN","MINUTES","IT","TAKES","TO","DRIVE","TO","CHURCH","BUT","THATS","EXACTLY","WHAT","I","DID"},
				new string[]{"I","WAS","TRYING","TO","HAVE","SOME","FUN","WITH","MANNY","BY","MAKING","FUNNY","FACES","AT","HIM","IN","THE","BACK","SEAT","OF","THE","CAR","BUT","WHEN","I","FINALLY","GOT","A","LAUGH","OUT","OF","HIM","HE","SPIT","HIS","JUICE","ALL","OVER","THE","CAR","SEAT"},
				new string[]{"THAT","JUST","MADE","MANNY","LAUGH","EXTRA","HARD","BUT","THEN","THE","NEXT","THING","YOU","KNOW","MOM","SAYS","HE","COULD","HAVE","CHOKED","TO","DEATH"},
				new string[]{"WELL","I","GUESS","THAT","THOUGHT","WAS","JUST","TOO","MUCH","FOR","MANNY","TO","TAKE","ALL","I","KNOW","IS","THAT","THE","REST","OF","THE","RIDE","WAS","PRETTY","MISERABLE","FOR","EVERYONE"},
				new string[]{"SO","YOU","CAN","SEE","HOW","I","CAN","GO","FROM","BEING","THE","HERO","TO","THE","GOAT","IN","NO","TIME","FLAT"},
				new string[]{"AT","CHURCH","MY","OLD","BEST","FRIEND","BEN","WAS","SITTING","UP","FRONT","WITH","HIS","FAMILY","MOM","DOESNT","LET","US","SIT","TOO","CLOSE","TO","BENS","FAMILY","BECAUSE","ME","AND","BEN","USED","TO","ALWAYS","GET","INTO","GIGGLE","FITS","WHEN","WED","SIT","NEAR","EACH","OTHER"},
				new string[]{"OUR","BIG","ROUTINE","WAS","THAT","AT","THE","PART","IN","CHURCH","WHERE","YOURE","SUPPOSED","TO","SAY","PEACE","BE","WITH","YOU","AND","SHAKE","HANDS","WED","SAY","PEAS","BE","WITH","YOU","(LIKE","THE","VEGETABLE)"},
				new string[]{"MOM","SAID","IF","WE","DIDNT","STOP","LAUGHING","IN","CHURCH","SHE","WAS","GOING","TO","SEPARATE","US","SO","WE","BEHAVED","OURSELVES","FOR","A","WHILE"},
				new string[]{"BUT","THIS","ONE","SUNDAY","DURING","THE","PEACE","BE","WITH","YOU","PART","BEN","ACTUALLY","HANDED","ME","A","COUPLE","OF","DRIED","UP","PEAS","HE","HAD","BEEN","CARRYING","IN","HIS","POCKET","AND","WE","BOTH","TOTALLY","LOST","IT"},
				new string[]{"MOM","KEPT","GOOD","ON","HER","PROMISE","BECAUSE","WE","HAVENT","EVEN","SAT","ON","THE","SAME","SIDE","OF","THE","CHURCH","AS","BEN","SINCE","THAT","DAY"},
				new string[]{"ON","THE","WAY","HOME","FROM","CHURCH","WE","PASSED","BY","THE","SMEDLEYS","WHO","WERE","OUT","IN","THEIR","FRONT","YARD","THERE","ARE","ABOUT","SIX","BOYS","IN","THAT","FAMILY","BUT","ITS","HARD","TO","TELL","ANY","OF","THEM","APART"}
			},
			new string[][]
			{
				new string[]{"THE","SMEDLEYS","BIG","DREAM","IN","LIFE","IS","TO","WIN","THE","GRAND","PRIZE","ON","AMERICAS","FUNNIEST","HOME","VIDEOS","SO","THEYRE","ALWAYS","TRYING","TO","STAGE","SOME","KIND","OF","ACCIDENT"},
				new string[]{"I","BET","FOR","EVERY","TEN","TIMES","YOU","SEE","A","GUY","GET","HIT","IN","THE","GROIN","WITH","A","GOLF","BALL","ON","THAT","SHOW","NINE","OF","THEM","ARE","SENT","IN","BY","FAMILIES","LIKE","THE","SMEDLEYS","JUST","TRYING","TO","MAKE","A","BUCK"},
				new string[]{"TODAY","AT","SCHOOL","I","WAS","LOOKING","AT","THE","INSIDE","COVER","OF","MY","PREALGEBRA","BOOK","AND","I","SAW","THAT","IT","USED","TO","BELONG","TO","JIMMY","JURY","WHO","IS","THE","MOST","POPULAR","KID","IN","THE",snlets[0] + "TH","GRADE","I","FIGURED","I","MIGHT","BE","ABLE","TO","TRANSLATE","THIS","INTO","SOME","PRETTY","BIG","POPULARITY","POINTS","OF","MY","OWN","BUT","THE","PROBLEM","WAS","THAT","JIMMY","JURY","DIDNT","WRITE","HIS","NAME","ANYWHERE","ON","THE","OUTSIDE","OF","THE","BOOK","SO","I","TOOK","CARE","OF","THAT","DETAIL","ON","MY","OWN"},
				new string[]{"UNFORTUNATELY","I","ALSO","GOT","BRIAN","GLEESONS","SCIENCE","BOOK","SO","THINGS","SORT","OF","EVENED","OUT"},
				new string[]{"IN","PHYS","ED","TODAY","A","COUPLE","GIRLS","CAUSED","A","STIR","WHEN","THEY","PRESENTED","A","PETITION","TO","MR","UNDERWOOD","THE","GYM","TEACHER"},
				new string[]{"THE","WAY","THEY","GOT","THE","IDEA","OF","WRITING","A","PETITION","WAS","BECAUSE","IN","HISTORY","WERE","STUDYING","MARTIN","LUTHER","WHO","IS","A","GUY","WHO","WROTE","A","LIST","OF","DEMANDS","AND","POSTED","THEM","ON","A","CHURCH","DOOR"},
				new string[]{"SO","THE","GIRLS","GOT","IT","IN","THEIR","HEADS","THAT","IT","WAS","UNFAIR","THAT","THEY","HAD","TO","DO","GIRL","PUSHUPS","WHILE","THE","GUYS","GET","TO","DO","BOY","PUSHUPS","AND","THEY","ALL","SIGNED","THEIR","NAMES","TO","A","LIST","TO","PROTEST"},
				new string[]{"IF","I","WAS","THEM","I","WOULDNT","COMPLAIN","GIRL","PUSHUPS","ARE","ABOUT","TEN","TIMES","EASIER","THAN","BOY","PUSHUPS"},
				new string[]{"WITH","GIRL","PUSHUPS","YOU","GET","TO","KEEP","YOUR","KNEES","ON","THE","GROUND","SO","YOU","ONLY","HAVE","TO","WORK","HALF","AS","HARD"},
				new string[]{"SO","I","THINK","MR","UNDERWOOD","SURPRISED","THEM","WHEN","HE","SAID","SURE","YOU","GIRLS","CAN","GO","AHEAD","AND","DO","BOY","PUSHUPS"},
				new string[]{"I","THINK","THE","GIRLS","WERE","EXPECTING","A","LOT","BIGGER","FIGHT","AND","NOW","I","KNOW","AT","LEAST","HALF","OF","THEM","WISH","THEY","COULD","TAKE","THAT","PETITION","BACK"},
				new string[]{"I","KIND","OF","GOT","INSPIRED","BY","THE","WHOLE","EPISODE","AND","I","STARTED","TO","PUT","TOGETHER","A","PETITION","SAYING","WE","BOYS","WANTED","TO","BE","ALLOWED","TO","DO","GIRL","PUSHUPS"},
				new string[]{"BUT","WHEN","I","SAW","THE","GROUP","OF","GUYS","WHO","WAS","INTERESTED","IN","SIGNING","IT","I","DECIDED","TO","JUST","BAG","IT"}
			},
			new string[][]
			{
				new string[]{"TONIGHT","WAS","A","PRETTY","BIG","DEAL","FOR","ME","BECAUSE","IT","WAS","THE","START","OF","THE","NEW","TV","SEASON","I","HAVE","HAD","TO","WATCH","FIVE","MONTHS","OF","RERUNS","SO","YOU","CAN","PROBABLY","UNDERSTAND","THAT","I","WAS","FIRED","UP","TO","FINALLY","SEE","SOMETHING","NEW"},
				new string[]{"DAD","PUT","MANNY","TO","BED","AND","MOM","MADE","POPCORN","AND","I","WAS","ALL","SET","FOR","SOME","SERIOUS","TELEVISION","WATCHING","BUT","FIVE","MINUTES","INTO","THE","FIRST","SHOW","MANNY","MAKES","AN","APPEARANCE","IN","THE","FAMILY","ROOM"},
				new string[]{"BUT","INSTEAD","OF","PUTTING","MANNY","RIGHT","BACK","TO","BED","MOM","LET","HIM","STAY","UP","AND","WATCH","TV","WITH","US"},
				new string[]{"AND","HERES","THE","KICKER","MOM","MADE","ME","CHANGE","THE","CHANNEL","BECAUSE","ON","THE","SHOW","I","WAS","WATCHING","THE","KIDS","HAD","A","DISRESPECTFUL","ATTITUDE","TOWARDS","THE","ADULTS","AND","SHE","DIDNT","WANT","MANNY","EXPOSED","TO","THAT","SORT","OF","THING"},
				new string[]{"COP","SHOWS","WERE","OUT","TOO","BECAUSE","OF","THE","VIOLENCE","SO","GUESS","WHAT","MOM","MADE","ME","TURN","TO","THE","CARTOON","CHANNEL","WHICH","IS","EXACTLY","WHAT","MANNY","WAS","WATCHING","BEFORE","HE","WENT","TO","BED"},
				new string[]{"MAN","I","WAS","STEAMED","WHEN","I","WAS","A","KID","THERE","WASNT","ANY","OF","THIS","GETTING","OUT","OF","BED","AND","COMING","BACK","DOWNSTAIRS","STUFF"},
				new string[]{"I","THINK","I","DID","IT","ONCE","OR","TWICE","BUT","DAD","PUT","A","STOP","TO","IT","REAL","QUICK"},
				new string[]{"THERE","WAS","THIS","BOOK","DAD","USED","TO","READ","TO","ME","BEFORE","I","WENT","TO","SLEEP","CALLED","A","LIGHT","IN","THE","ATTIC"},
				new string[]{"IT","WAS","AN","AWESOME","BOOK","BUT","ON","THE","BACK","OF","IT","THERE","WAS","A","PICTURE","OF","THE","GUY","WHO","WROTE","IT","SHEL","SILVERSTEIN"},
				new string[]{"HAVE","YOU","EVER","SEEN","THAT","GUY","ALL","I","CAN","SAY","IS","THAT","HE","LOOKS","MORE","LIKE","A","BURGLAR","OR","A","PIRATE","THAN","A","GUY","WHO","WRITES","POEMS","FOR","KIDS"},
				new string[]{"WELL","DAD","USED","TO","LEAVE","THAT","BOOK","RIGHT","ON","MY","END","TABLE","EVERY","NIGHT","WITH","THE","BACK","OF","THE","BOOK","FACING","UP","AND","IT","REALLY","GAVE","ME","THE","HEEBIE","JEEBIES"},
				new string[]{"I","THINK","DAD","CAUGHT","ON","THAT","SHEL","SILVERSTEIN","KIND","OF","FREAKED","ME","OUT","BECAUSE","THE","FIRST","NIGHT","AFTER","I","PULLED","THE","KIND","OF","THING","MANNY","PULLED","TONIGHT","DAD","READ","ME","SOME","POEMS","FROM","A","LIGHT","IN","THE","ATTIC","AND","THEN","SAID","IF","YOU","GET","UP","AGAIN","TONIGHT","I","WONDER","IF","YOULL","RUN","INTO","SHEL","SILVERSTEIN","IN","THE","HALLWAY"},
				new string[]{"WELL","DAD","REALLY","HAD","MY","NUMBER","WITH","THAT","TRICK","I","NEVER","GOT","UP","AGAIN","NOT","EVEN","TO","GO","TO","THE","BATHROOM","I","WOULD","RATHER","HAVE","WET","THE","BED","THAN","TO","FIND","THAT","GUY","CREEPING","AROUND","UPSTAIRS"}
			},
			new string[][]
			{
				new string[]{"LAST","NIGHT","MANNY","FINALLY","WENT","TO","BED","AND","I","FINALLY","GOT","TO","WATCH","SOME","OF","THE","NEW","SHOWS","BUT","IVE","GOT","TO","SAY","IT","WASNT","WORTH","THE","WAIT"},
				new string[]{"ALL","THE","NEW","SITCOMS","ARE","THE","SAME","THEY","BASICALLY","TAKE","ONE","LAME","JOKE","AND","THEN","DRIVE","IT","INTO","THE","GROUND","FOR","THE","NEXT",snlets[1] + "" + snlets[0], "MINUTES","I","WANTED","TO","SEE","IF","I","COULD","WRITE","A","BETTER","SHOW","THAN","THESE","CLOWNS","WHO","ARE","MAKING","THOUSANDS","OF","DOLLARS","SO","AT","LUNCH","TODAY","I","GAVE","IT","A","SHOT"},
				new string[]{"I","SHOWED","MY","DRAWINGS","TO","MOM","AND","SHE","SURPRISED","ME","BY","BEING","REALLY","INTERESTED","SHE","TOLD","ME","THAT","IF","I","REALLY","WANTED","TO","WRITE","A","TV","SHOW","I","HAD","TO","COME","UP","WITH","A","WHOLE","PLOT","WITH","A","BEGINNING","AND","END","I","THINK","SHE","WAS","JUST","HAPPY","I","WAS","SHOWING","AN","INTEREST","IN","SOMETHING","OTHER","THAN","A","VIDEO","GAME","FOR","ONCE"},
				new string[]{"IT","SEEMS","LIKE","EVERY","NEW","SHOW","THAT","COMES","OUT","NOWADAYS","IS","ABOUT","SOME","DAD","WHO","DOES","OR","SAYS","SOMETHING","REALLY","IGNORANT","AT","THE","BEGINNING","OF","THE","SHOW","BUT","THEN","BY","THE","END","OF","THE","SHOW","HE","COMES","AROUND","AND","REALIZES","HE","WAS","BEING","A","NINCOMPOOP","SO","THE","NEXT","SHOW","I","WROTE","(CALLED","WISE","UP","MR","LOCKERMAN)","IS","BASED","ON","THAT","KIND","OF","IDEA"},
				new string[]{"I","SHOWED","THIS","SCRIPT","TO","MOM","WHEN","I","WAS","DONE","TO","BE","HONEST","WITH","YOU","I","DONT","THINK","SHE","REALLY","APPROVES","OF","MY","TYPE","OF","HUMOR","BUT","SHE","IS","PRETTY","EXCITED","TO","SEE","ME","WORKING","ON","THIS","STUFF","USUALLY","WHEN","MOM","GETS","TOO","ENTHUSIASTIC","ABOUT","SOMETHING","IM","DOING","THATS","MY","SIGNAL","TO","BACK","OFF","BUT","MOM","SAID","SHES","GOING","TO","TRY","TO","GET","A","HOLD","OF","A","VIDEO","CAMERA","FROM","WORK","TO","LET","ME","PUT","SOME","OF","MY","IDEAS","ON","FILM","SO","I","GUESS","I","CAN","DEAL","WITH","IT","FOR","NOW"},
				new string[]{"LAST","NIGHT","I","WAS","SO","EXCITED","ABOUT","GETTING","MY","HANDS","ON","A","VIDEO","CAMERA","I","COULDNT","SLEEP","SO","TODAY","IN","SCHOOL","I","FAKED","ABOUT","BEING","SICK","IN","GYM","SO","I","COULD","WRITE","DOWN","MY","IDEA","FOR","A","MOVIE"},
				new string[]{"MOM","WAS","ABLE","TO","GET","A","CAMERA","FROM","WORK","SO","I","FINISHED","UP","THE","SCRIPT","AND","BROUGHT","IT","UP","TO","ROWLEYS","HOUSE","TO","SHOW","HIM","I","THINK","ITS","MY","BEST","STUFF","YET","AND","THATS","SAYING","SOMETHING"},
				new string[]{"WELL","ROWLEYS","REACTION","WASNT","QUITE","WHAT","I","WAS","HOPING","FOR"},
				new string[]{"YOUD","THINK","ROWLEY","WOULD","BE","GRATEFUL","THAT","I","WAS","GOING","TO","MAKE","HIM","A","BIG","STAR","AND","OF","COURSE","I","GOT","NO","THANKS","FOR","WRITING","JUICY","ROLES","FOR","HIS","PARENTS"},
				new string[]{"TODAY","AFTER","SCHOOL","I","WALKED","UP","TO","ROWLEYS","HOUSE","TO","SHOW","HIM","SOME","REWRITES","I","DID","FOR","THE","BOY","WHOSE","FAMILY","THOUGHT","HE","WAS","A","DOG","BUT","ROWLEY","WOULDNT","ANSWER","THE","DOOR"},
				new string[]{"I","STARTED","TO","HEAD","HOME","AND","THEN","ALL","OF","THE","SUDDEN","MOM","PULLS","UP","ALONG","SIDE","ME","WITH","MACKIE","CREAVY","IN","THE","BACK","SEAT","OF","THE","CAR"},
				new string[]{"OH","MAN","I","COMPLETELY","FORGOT","ABOUT","SOCCER","DAD","MAKES","ME","DO","IT","EVERY","YEAR","SO","IM","WELL","ROUNDED"}
			},
			new string[][]
			{
				new string[]{"SO","I","GUESS","IVE","GOT","TO","PUT","MY","FILM","CAREER","ON","HOLD","AND","GO","GET","MY","SHINS","KICKED","FOR","A","COUPLE","OF","MONTHS"},
				new string[]{"THE","FIRST","NIGHT","OF","SOCCER","PRACTICE","IS","ALWAYS","THE","SAME","THEY","START","OFF","BY","DOING","THIS","SKILLS","TEST","TO","SEE","HOW","GOOD","YOU","ARE","USUALLY","I","DONT","CARE","HOW","I","GET","RANKED","BUT","THIS","YEAR","THE","GUY","WHO","WAS","DOING","THE","TESTING","WAS","MR","MATTHEWS","WHO","IS","THE","FATHER","OF","PIPER","MATTHEWS","THE","PRETTIEST","GIRL","IN","OUR","CHURCH","SO","I","FIGURED","ID","BETTER","DO","MY","BEST","IF","I","WANTED","TO","IMPRESS","MY","FUTURE","FATHERINLAW"},
				new string[]{"EVEN","THOUGH","I","TRIED","MY","HARDEST","I","STILL","GOT","RANKED","PRE","ALPHA","MINUS","WHICH","IS","ADULT","CODE","WORDS","FOR","YOU","STINK"},
				new string[]{"THE","NEXT","THING","THEY","DO","IS","PUT","EVERYONE","ON","A","TEAM","THEY","BASICALLY","TRY","TO","SPREAD","OUT","THE","REALLY","AWFUL","KIDS","LIKE","ME","SO","NO","ONE","TEAM","HAS","TOO","MANY","TERRIBLE","PLAYERS"},
				new string[]{"AND","WOULDNT","YOU","KNOW","MY","LUCK","I","GOT","PUT","ON","KENNY","KEITHS","TEAM","SO","THAT","MEANS","MY","COACH","IS","MR","KEITH","SAME","AS","LAST","YEAR"},
				new string[]{"MR","KEITH","HATES","ME","AND","I","TRACE","IT","ALL","BACK","TO","THE","FIRST","DAY","OF","PRACTICE","LAST","YEAR","A","BUNCH","OF","US","TERRIBLE","PLAYERS","WERE","SLACKING","OFF","HANGING","OUT","BY","THE","WATER","JUG","WHEN","MR","KEITH","YELLED","FOR","US","TO","GET","BACK","ON","THE","PLAYING","FIELD"},
				new string[]{"SO","AS","A","JOKE","I","RAN","BACKWARDS","WITH","MY","REAR","END","POINTED","AT","MR","KEITH"},
				new string[]{"I","THINK","IT","WOULD","HAVE","BEEN","FUNNIER","IF","ALL","THE","OTHER","GUYS","HAD","DONE","THE","SAME","THING","I","DID","BUT","THEY","KIND","OF","HUNG","ME","OUT","TO","DRY"},
				new string[]{"ANYWAY","YOU","CAN","PROBABLY","GUESS","THAT","MR","KEITH","DID","NOT","FIND","MY","JOKE","SO","AMUSING","AND","FROM","THEN","ON","HE","MADE","THINGS","PRETTY","MISERABLE","FOR","ME"},
				new string[]{"RIGHT","BEFORE","THE","FIRST","GAME","HE","GAVE","US","ALL","OUR","POSITIONS","AND","HE","TOLD","ME","MY","POSITION","WAS","SHAG","I","DIDNT","KNOW","A","WHOLE","LOT","ABOUT","SOCCER","BUT","I","WAS","PRETTY","PROUD","THAT","I","HAD","MY","VERY","OWN","POSITION","I","REMEMBER","BRAGGING","TO","RODRICK","ABOUT","IT"},
				new string[]{"BUT","RODRICK","KNEW","A","THING","OR","TWO","ABOUT","SOCCER","AND","HE","TOLD","ME","THAT","THE","SHAG","ISNT","ACTUALLY","A","REAL","POSITION","ON","THE","FIELD","ITS","JUST","A","KID","WHO","CHASES","ALL","THE","BALLS","THAT","GO","OUT","OF","BOUNDS"},
				new string[]{"AND","SURE","ENOUGH","RODRICK","WAS","RIGHT","MR","KEITH","NEVER","PUT","ME","IN","A","GAME","AND","I","WASNT","EVEN","THE","WORST","KID","ON","OUR","TEAM","WE","HAD","COLLIN","AND","MACKIE","CREAVY","AND","A","COUPLE","OTHER","KIDS","WHO","CAN","BARELY","KICK","A","SOCCER","BALL","AND","THERE","I","WAS","CHASING","BALLS","INTO","THE","STREET","AND","LET","ME","JUST","SAY","SOMETHING","IN","DEFENSE","OF","ALL","THOSE","SHAGS","OUT","THERE","SHAG","MIGHT","NOT","BE","THE","MOST","NOBLE","POSITION","IN","SOCCER","BUT","IT","IS","DEFINITELY","THE","MOST","STRESSFUL"}
			},
			new string[][]
			{
				new string[]{"TONIGHT","I","WAS","ALL","SET","TO","GO","OVER","TO","COLLINS","HOUSE","BUT","I","FOUND","OUT","DAD","HAD","RENTED","A","MOVIE","SO","I","CHANGED","MY","PLANS","AND","STAYED","HOME","WHENEVER","DAD","GETS","A","MOVIE","HE","NEVER","CHECKS","THE","RATING","SO","ITS","ALWAYS","WORTH","HANGING","AROUND","AND","SEEING","WHAT","HE","PICKED","OUT","AND","HALF","THE","TIME","HE","GETS","SOMETHING","MOM","WOULD","NEVER","LET","ME","WATCH","ON","MY","OWN"},
				new string[]{"THE","ONLY","DOWN","SIDE","ABOUT","WATCHING","MOVIES","WITH","DAD","IS","THAT","IF","THERE","IS","EVER","A","SCENE","WITH","ANYTHING","THE","LEAST","BIT","INAPPROPRIATE","SOMEHOW","MOM","SHOWS","UP","AT","THE","WORST","MOMENT","AND","MAKES","YOU","FEEL","ASHAMED","FOR","WATCHING","IT"},
				new string[]{"LUCKILY","I","HAVE","MASTERED","THE","KIND","OF","RESPONSE","THAT","GETS","ME","OFF","THE","HOOK","EVERY","TIME","ESPECIALLY","DURING","THE","RACY","SCENES"},
				new string[]{"I","JUST","MAKE","SURE","I","HEAD","BACK","DOWNSTAIRS","LATER","ON","TO","CATCH","UP","ON","ANYTHING","I","MISSED"},
				new string[]{"TODAY","AFTER","CHURCH","MOM","AND","I","WENT","OVER","TO","GRAMMAS","TO","CHECK","UP","ON","HER","MOM","WAS","PRETTY","WORRIED","BECAUSE","GRAMMA","HASNT","BEEN","ANSWERING","HER","PHONE","FOR","A","FEW","DAYS","SO","MOM","WANTED","TO","MAKE","SURE","GRAMMA","WAS","OK","BUT","WHEN","WE","GOT","THERE","WE","FOUND","GRAMMA","SITTING","IN","HER","KITCHEN","CLIPPING","COUPONS","LIKE","USUAL","SO","WHEN","MOM","ASKED","GRAMMA","WHY","SHE","HASNT","BEEN","ANSWERING","THE","TELEPHONE","GRAMMA","SAID","CORDLESS","TELEPHONES","ERASE","THE","MEMORY","OF","THE","ELDERLY"},
				new string[]{"WELL","THAT","KIND","OF","SET","MOM","OFF","BECAUSE","SHE","KNEW","EXACTLY","WHERE","GRAMMA","WAS","GETTING","HER","INFORMATION","FROM","THE","SUPERMARKET","TABLOIDS","BUT","SOMEHOW","GRAMMA","KEEPS","GETTING","A","HOLD","OF","THESE","THINGS","EVEN","THOUGH","SHE","DOESNT","DRIVE"},
				new string[]{"SO","WHEN","MOM","CONFRONTED","GRAMMA","ON","IT","AND","SAID","WHERE","DID","YOU","READ","THAT","MOM","GRAMMA","KNEW","SHE","WAS","CORNERED"},
				new string[]{"SO","MOM","FOUND","WHERE","GRAMMA","WAS","STASHING","THE","TABLOIDS","AND","WE","TOOK","IT","HOME","WITH","US","TO","THROW","AWAY","WHAT","MOM","DOESNT","KNOW","IS","THAT","I","ALWAYS","DIG","THOSE","THINGS","OUT","OF","THE","TRASH","AND","READ","THEM","WHEN","NO","ONES","AROUND"},
				new string[]{"THERES","ACTUALLY","A","BUNCH","OF","GOOD","STUFF","IN","THERE","LIKE","HOROSCOPES","AND","PREDICTIONS","IN","FACT","THE","REASON","I","TAKE","SCHOOL","WITH","A","GRAIN","OF","SALT","IS","BECAUSE","THIS","ONE","TABLOID","SAYS","THE","WHOLE","EAST","COAST","IS","GOING","TO","BE","UNDERWATER","WITHIN","FIVE","YEARS"},
				new string[]{"I","DONT","KNOW","IF","I","EVER","MENTIONED","THIS","BEFORE","BUT","EVERY","MORNING","WHEN","DAD","WAKES","ME","UP","HE","GIVES","THE","SAME","EXACT","SPEECH","ITS","TEN","OF","SEVEN","MOMS","IN","THE","SHOWER","AND","I","WANT","YOU","IN","THERE","THE","SECOND","SHES","OUT","SO","YOURE","NOT","LATE","FOR","YOUR","BUS","LETS","MOVE","IT","HUP","HUP","HUP"},
				new string[]{"I","DONT","KNOW","WHERE","DAD","GETS","HIS","MORNING","ENERGY","BUT","I","DEFINITELY","DID","NOT","INHERIT","THAT","GENE","FROM","HIM","AFTER","HE","WAKES","ME","UP","I","PROP","MYSELF","UP","ON","MY","ELBOW","AND","TRY","MY","HARDEST","NOT","TO","FALL","BACK","ASLEEP"}
			},
			new string[][]
			{
				new string[]{"THIS","ONE","DAY","I","ACCIDENTALLY","FELL","BACK","ASLEEP","AFTER","DAD","WOKE","ME","UP","AND","BELIEVE","ME","IT","WAS","THE","LAST","TIME","I","EVER","MADE","THAT","MISTAKE"},
				new string[]{"AT","SCHOOL","THIS","MORNING","THERE","WERE","A","BUNCH","OF","KIDS","FROM","MRS","BUNNS","HOMEROOM","CLASS","STANDING","AROUND","IN","THE","HALLWAY","TRIPPING","EVERY","OTHER","KID","THAT","WALKED","BY"},
				new string[]{"ITS","REALLY","SAD","TO","SEE","WHAT","PASSES","FOR","COMEDY","THESE","DAYS","BACK","IN","THE","FIFTH","GRADE","ME","AND","BEN","WERE","AN","AWESOME","COMEDY","TEAM","AND","WE","HAD","SOME","REALLY","GOOD","ROUTINES"},
				new string[]{"BUT","EVER","SINCE","BEN","LEFT","TOWN","THE","FUNNIEST","THING","THAT","EVER","HAPPENS","IN","SCHOOL","IS","WHEN","SOME","POOR","KID","DROPS","HIS","LUNCH","TRAY","IN","THE","CAFETERIA"},
				new string[]{"I","TRIED","TO","PICK","THE","COMEDY","THING","BACK","UP","WHEN","ROWLEY","CAME","ALONG","BUT","THINGS","NEVER","REALLY","WORKED","OUT"},
				new string[]{"I","THOUGHT","OF","ANOTHER","GOOD","REASON","TO","KEEP","A","JOURNAL","WHEN","I","GET","RICH","AND","FAMOUS","I","CAN","PULL","OUT","THIS","BOOK","TO","REMIND","MYSELF","WHY","I","SHOULDNT","LET","RODRICK","SWIM","IN","MY","POOL","OR","USE","MY","BOWLING","ALLEY","OR","ANYTHING","ELSE","LIKE","THAT"},
				new string[]{"TONIGHT","RODRICK","PULLED","HIS","GETOUTOFDOINGTHEDISHES","ROUTINE","JUST","LIKE","HE","DOES","EVERY","NIGHT","DAD","HAS","A","RULE","THAT","WERE","NOT","ALLOWED","TO","WATCH","TV","UNTIL","THE","DISHES","ARE","DONE","BUT","RIGHT","AFTER","DINNER","RODRICK","ALWAYS","GOES","UPSTAIRS","TO","THE","BATHROOM","AND","DOESNT","COME","DOWN","FOR","SOMETHING","LIKE",snlets[0] + "" + snlets[1], "MINUTES","BY","THAT","TIME","IVE","DONE","ALL","THE","DISHES","MYSELF"},
				new string[]{"WELL","TONIGHT","I","SAID","ENOUGH","IS","ENOUGH","AND","I","WENT","TO","COMPLAIN","TO","MOM","AND","DAD","BUT","OF","COURSE","RODRICK","HAD","AN","EXCUSE"},
				new string[]{"ALL","I","CAN","SAY","IS","THAT","IF","RODRICK","WANTS","TO","HANG","OUT","IN","MY","MANSION","WHEN","WERE","GROWN","UP","HE","BETTER","BRING","A","TOWEL","AND","SOME","SPONGES","BECAUSE","HES","GOING","TO","BE","DOING","A","WHOLE","LOT","OF","DISHES"},
				new string[]{"TONIGHT","SOCCER","PRACTICE","ENDED","A","FEW","MINUTES","EARLY","SO","THE","COACH","COULD","HAND","OUT","UNIFORMS","AND","WE","COULD","COME","UP","WITH","A","TEAM","NAME"},
				new string[]{"I","SUGGESTED","TWISTED","WIZARDS","AND","SAID","MAYBE","WE","COULD","GET","THE","GAME","ZONE","TO","SPONSOR","US","BUT","OF","COURSE","MY","PERFECTLY","GOOD","IDEA","GOT","SHOT","DOWN"},
				new string[]{"A","BUNCH","OF","OTHER","IDEAS","GOT","TOSSED","AROUND","UNTIL","SOME","IDIOT","CAME","UP","WITH","THE","NAME","RED","SOX","I","COULDNT","BELIEVE","IT","WHEN","A","BUNCH","OF","KIDS","THOUGHT","THAT","WAS","A","REALLY","GOOD","IDEA","AND","GUESS","WHAT","THATS","WHAT","EVERYONE","VOTED","ON"},
				new string[]{"NOW","NUMBER","ONE","THE","RED","SOX","IS","A","BASEBALL","TEAM","NOT","A","SOCCER","TEAM","AND","NUMBER","TWO","OUR","UNIFORMS","ARE","BLUE","INCLUDING","THE","SOCKS","BUT","OF","COURSE","NOBODY","WOULD","LISTEN","TO","ME"}
			}
		};
		string storedLetters = "";
		List<List<string>> storedPositions = new List<List<string>>();
		string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		for (int aa = 0; aa < word.Length; aa++)
		{
			if (!(storedLetters.Contains(word[aa])))
			{
				storedLetters = storedLetters + "" + word[aa];
				List<string> tempStorage = new List<string>();
				for (int bb = 0; bb < diary.Length; bb++)
				{
					for (int cc = 0; cc < diary[bb].Length; cc++)
					{
						for (int dd = 0; dd < diary[bb][cc].Length; dd++)
						{
							for (int ee = 0; ee < diary[bb][cc][dd].Length; ee++)
							{
								if (diary[bb][cc][dd][ee] == word[aa])
								{
									if (dd < 9)
										tempStorage.Add(alpha[bb] + "" + alpha[cc] + "0" + (dd + 1) + "" + alpha[ee]);
									else
										tempStorage.Add(alpha[bb] + "" + alpha[cc] + "" + (dd + 1) + "" + alpha[ee]);
								}

							}
						}
					}
				}
				storedPositions.Add(tempStorage);
			}
			int cursor = storedLetters.IndexOf(word[aa]);
			string temp = storedPositions[cursor][UnityEngine.Random.Range(0, storedPositions[cursor].Count)];
			if(invert)
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BROWN] {1} -> {2}", moduleId, word[aa], temp);
			else
				Debug.LogFormat("[Ultimate Cipher #{0}] [BROWN] {1} -> {2}", moduleId, word[aa], temp);
			pages[page - 1][1] = pages[page - 1][1] + "" + temp[0];
			pages[page - 1][2] = pages[page - 1][2] + "" + temp[1];
			pages[page][0] = pages[page][0] + "" + temp[2];
			pages[page][1] = pages[page][1] + "" + temp[3];
			pages[page][2] = pages[page][2] + "" + temp[4];
		}
	}
	private string blackcipher(string word, bool invert)
	{
		fontsizes[page][0] = 35;
		fontsizes[page][1] = 40;
		fontsizes[page][2] = 25;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 35;
		fontsizes[page - 1][2] = 35;
		if (invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Black Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Begin Digrafid Encryption", new object[]
			{
				moduleId
			});
			string text = DigrafidEnc(word.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Begin Scytale Transposition", new object[]
			{
				moduleId
			});
			text = ScytaleTrans(text.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Begin Enigma Encryption", new object[]
			{
				moduleId
			});
			return EnigmaEnc(text.ToUpperInvariant(), invert);
		}
		chosentextcolors[page] = Color.white;
		chosenscreencolors[page] = screencolors[0];
		chosentextcolors[page - 1] = Color.white;
		chosenscreencolors[page - 1] = screencolors[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] Begin Black Cipher", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Begin Enigma Encryption", new object[]
		{
			moduleId
		});
		string text2 = EnigmaEnc(word.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Begin Scytale Transposition", new object[]
		{
			moduleId
		});
		text2 = ScytaleTrans(text2.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Begin Digrafid Encryption", new object[]
		{
			moduleId
		});
		return DigrafidEnc(text2.ToUpperInvariant(), invert);
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000FDCC File Offset: 0x0000E1CC
	private string EnigmaEnc(string word, bool invert)
	{
		string[][] array = new string[][]
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
		string[][] array2 = new string[][]
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
		string[] array3 = new string[]
		{
			"I",
			"II",
			"III",
			"IV",
			"V",
			"VI",
			"VII",
			"VIII"
		};
		string text = "01234567";
		string text2 = "ABC";
		string[][] array4 = new string[5][];
		int num = UnityEngine.Random.Range(0, 3);
		array4[0] = array2[num];
		string text3 = text2[num] + string.Empty;
		string text4 = string.Empty;
		for (int i = 0; i < 3; i++)
		{
			char c = text[UnityEngine.Random.Range(0, text.Length)];
			text = text.Replace(c + string.Empty, string.Empty);
			array4[i + 1] = array[(int)(c - '0')];
			text3 = text3 + "-" + array3[(int)(c - '0')];
			num = UnityEngine.Random.Range(0, 26);
			array4[i + 1][0] = array4[i + 1][0].Substring(num) + string.Empty + array4[i + 1][0].Substring(0, num);
			array4[i + 1][1] = array4[i + 1][1].Substring(num) + string.Empty + array4[i + 1][1].Substring(0, num);
			text4 = text4 + string.Empty + array4[i + 1][1][0];
		}
		array4[4] = new string[]
		{
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ",
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ"
		};
		string text5 = string.Empty;
		int num2 = UnityEngine.Random.Range(3, 6);
		string text6 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		for (int j = 0; j < num2; j++)
		{
			char c2 = text6[UnityEngine.Random.Range(0, text6.Length)];
			text6 = text6.Replace(c2 + string.Empty, string.Empty);
			char c3 = text6[UnityEngine.Random.Range(0, text6.Length)];
			text6 = text6.Replace(c3 + string.Empty, string.Empty);
			text5 = string.Concat(new object[]
			{
				text5,
				string.Empty,
				c2,
				string.Empty,
				c3,
				"-"
			});
			for (int k = 0; k < 2; k++)
			{
				array4[4][k] = array4[4][k].Replace(c2, '1');
				array4[4][k] = array4[4][k].Replace(c3, c2);
				array4[4][k] = array4[4][k].Replace('1', c3);
			}
		}
		text5 = text5.Substring(0, text5.Length - 1);
		pages[page][0] = text3.ToUpperInvariant();
		pages[page][1] = text4.ToUpperInvariant();
		pages[page][2] = text5.ToUpperInvariant();
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Rotor Config: {1}", new object[]
			{
				moduleId,
				text3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Rotor Letters: {1}", new object[]
			{
				moduleId,
				text4
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Plugboard: {1}", new object[]
			{
				moduleId,
				text5
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Rotor Config: {1}", new object[]
			{
				moduleId,
				text3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Rotor Letters: {1}", new object[]
			{
				moduleId,
				text4
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Plugboard: {1}", new object[]
			{
				moduleId,
				text5
			});
		}
		string text7 = string.Empty;
		for (int l = 0; l < 6; l++)
		{
			char c4 = word[l];
			string text8 = c4 + string.Empty;
			for (int m = array4.Length - 1; m > 0; m--)
			{
				c4 = array4[m - 1][0][array4[m][1].IndexOf(c4)];
				text8 = text8 + "->" + c4;
			}
			c4 = array4[0][0][array4[0][1].IndexOf(c4)];
			text8 = text8 + "->" + c4;
			for (int n = 0; n < array4.Length - 1; n++)
			{
				c4 = array4[n + 1][1][array4[n][0].IndexOf(c4)];
				text8 = text8 + "->" + c4;
			}
			text7 = text7 + string.Empty + c4;
			if (array4[2][1][0] == array4[2][2][0] || array4[2][1][0] == array4[2][2][1])
			{
				array4[2][0] = array4[2][0].Substring(1) + string.Empty + array4[2][0][0];
				array4[2][1] = array4[2][1].Substring(1) + string.Empty + array4[2][1][0];
				array4[1][0] = array4[1][0].Substring(1) + string.Empty + array4[1][0][0];
				array4[1][1] = array4[1][1].Substring(1) + string.Empty + array4[1][1][0];
			}
			else if (array4[3][1][0] == array4[3][2][0] || array4[3][1][0] == array4[3][2][1])
			{
				array4[2][0] = array4[2][0].Substring(1) + string.Empty + array4[2][0][0];
				array4[2][1] = array4[2][1].Substring(1) + string.Empty + array4[2][1][0];
			}
			array4[3][0] = array4[3][0].Substring(1) + string.Empty + array4[3][0][0];
			array4[3][1] = array4[3][1].Substring(1) + string.Empty + array4[3][1][0];
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] {1}", new object[]
				{
					moduleId,
					text8
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] {1}", new object[]
				{
					moduleId,
					text8
				});
			}
		}
		return text7;
	}

	string ScytaleTrans(string word, bool invert)
	{
		string[] letterrows = new string[("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumber()[1]) % 4) + 2];
		string encrypt = "";
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Number of Rows: ({1} % 4) + 2 = {2}", moduleId, Bomb.GetSerialNumber()[1], letterrows.Length);
			for (int aa = 0; aa < letterrows.Length; aa++)
				letterrows[aa] = "";
			for (int aa = 0; aa < 6; aa++)
				letterrows[aa % letterrows.Length] = letterrows[aa % letterrows.Length] + "*";
			int cur = 0;
			for (int aa = 0; aa < letterrows.Length; aa++)
			{
				letterrows[aa] = word.Substring(cur, letterrows[aa].Length);
				cur += letterrows[aa].Length;
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Scytale Row #{1}: {2}", moduleId, (aa + 1), letterrows[aa]);
			}
			for (int aa = 0; aa < 6; aa++)
				encrypt = encrypt + "" + letterrows[aa % letterrows.Length][aa / letterrows.Length];
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] {1} -> {2}", moduleId, word, encrypt);
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Number of Rows: ({1} % 4) + 2 = {2}", moduleId, Bomb.GetSerialNumber()[1], letterrows.Length);
			for (int aa = 0; aa < letterrows.Length; aa++)
				letterrows[aa] = "";
			for (int aa = 0; aa < 6; aa++)
				letterrows[aa % letterrows.Length] = letterrows[aa % letterrows.Length] + "" + word[aa];
			
			for (int aa = 0; aa < letterrows.Length; aa++)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Scytale Row #{1}: {2}", moduleId, (aa + 1), letterrows[aa]);
				encrypt = encrypt + "" + letterrows[aa];
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] {1} -> {2}", moduleId, word, encrypt);
		}
		return encrypt;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00010994 File Offset: 0x0000ED94
	private string DigrafidEnc(string word, bool invert)
	{
		int index = UnityEngine.Random.Range(0, wordList.Count);
		string text = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
		wordList[index].Remove(text.ToUpperInvariant());
		index = UnityEngine.Random.Range(0, wordList.Count);
		string text2 = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
		wordList[index].Remove(text2.ToUpperInvariant());
		pages[page - 1][1] = text.ToUpperInvariant();
		pages[page - 1][2] = text2.ToUpperInvariant();
		if(invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Keyword A: {1}", moduleId, text);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Keyword B: {1}", moduleId, text2);
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Keyword A: {1}", moduleId, text);
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Keyword B: {1}", moduleId, text2);
		}
		string key = getKey(text.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumberLetters().First<char>()) % 2 == 0);
		string key2 = getKey(text2.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumberLetters().Last<char>()) % 2 == 1);
		string text3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ#";
		string text4 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ#";
		char c = text3[UnityEngine.Random.Range(0, text3.Length)];
		char c2 = text4[UnityEngine.Random.Range(0, text4.Length)];
		text3 = text3.Replace(c + string.Empty, string.Empty);
		text4 = text4.Replace(c2 + string.Empty, string.Empty);
		bool flag = true;
		string[] array = new string[]
		{
			"123",
			"456",
			"789"
		};
		string text7;
		do
		{
			string text5 = key.Replace(c, '#') + string.Empty + c;
			string text6 = key2.Replace(c2, '#') + string.Empty + c2;
			string[] array2 = new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			};
			List<string> list = new List<string>();
			for (int i = 0; i < 3; i++)
			{
				int num = text5.IndexOf(word[i * 2]);
				array2[0] = array2[0] + string.Empty + (num % 9 + 1);
				int num2 = num / 9;
				num = text6.IndexOf(word[i * 2 + 1]);
				array2[2] = array2[2] + string.Empty + (num % 9 + 1);
				int index2 = num / 9;
				array2[1] = array2[1] + string.Empty + array[num2][index2];
				list.Add(string.Concat(new object[]
				{
					word[i * 2],
					string.Empty,
					word[i * 2 + 1],
					" -> ",
					array2[0][i],
					string.Empty,
					array2[1][i],
					string.Empty,
					array2[2][i]
				}));
			}
			text7 = string.Empty;
			for (int j = 0; j < 3; j++)
			{
				text7 = text7 + string.Empty + text5[(int)(array2[j][0] - '0' - '\u0001' + (array2[j][1] - '0' - '\u0001') / '\u0003' * '\t')];
				text7 = text7 + string.Empty + text6[(int)(array2[j][2] - '0' - '\u0001' + (array2[j][1] - '0' - '\u0001') % '\u0003' * '\t')];
				list.Add(string.Concat(new object[]
				{
					"Digrafid Row #",
					j + 1,
					": ",
					array2[j],
					" -> ",
					text7[j * 2],
					string.Empty,
					text7[j * 2 + 1]
				}));
			}
			if (text7.Contains('#'))
			{
				if (text3.Length == 0)
				{
					text3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ#";
					c2 = text4[UnityEngine.Random.Range(0, text4.Length)];
					text4 = text4.Replace(c2 + string.Empty, string.Empty);
				}
				c = text3[UnityEngine.Random.Range(0, text3.Length)];
				text3 = text3.Replace(c + string.Empty, string.Empty);
			}
			else
			{
				flag = false;
				if (invert)
				{
					Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Key A: {1}", new object[]
					{
						moduleId,
						text5
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Key B: {1}", new object[]
					{
						moduleId,
						text6
					});
					for (int k = 0; k < list.Count; k++)
					{
						Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] {1}", new object[]
						{
							moduleId,
							list[k]
						});
					}
				}
				else
				{
					Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Key A: {1}", new object[]
					{
						moduleId,
						text5
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Key B: {1}", new object[]
					{
						moduleId,
						text6
					});
					for (int l = 0; l < list.Count; l++)
					{
						Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] {1}", new object[]
						{
							moduleId,
							list[l]
						});
					}
				}
				arrowLetters[page][0] = c + string.Empty;
				arrowLetters[page][1] = c2 + string.Empty;
				arrowLetters[page - 1][0] = c + string.Empty;
				arrowLetters[page - 1][1] = c2 + string.Empty;
			}
		}
		while (flag);
		return text7;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00011068 File Offset: 0x0000F468
	private string graycipher(string word, bool invert)
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
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Gray Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Begin Bit Switch Encryption", new object[]
			{
				moduleId
			});
			string text = BitSwitchEnc(word.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Begin Columnar Transposition", new object[]
			{
				moduleId
			});
			text = ColumnTrans(text.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Begin Portax Encryption", new object[]
			{
				moduleId
			});
			return PortaxEnc(text.ToUpperInvariant(), invert);
		}
		chosentextcolors[page] = Color.white;
		chosenscreencolors[page] = screencolors[0];
		chosentextcolors[page - 1] = Color.white;
		chosenscreencolors[page - 1] = screencolors[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] Begin Gray Cipher", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Begin Portax Encryption", new object[]
		{
			moduleId
		});
		string text2 = PortaxEnc(word.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Begin Columnar Transposition", new object[]
		{
			moduleId
		});
		text2 = ColumnTrans(text2.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Begin Bit Switch Encryption", new object[]
		{
			moduleId
		});
		return BitSwitchEnc(text2.ToUpperInvariant(), invert);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00011370 File Offset: 0x0000F770
	private string PortaxEnc(string word, bool invert)
	{
		string text = string.Concat(new object[]
		{
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[UnityEngine.Random.Range(0, 26)],
			string.Empty,
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[UnityEngine.Random.Range(0, 26)],
			string.Empty,
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[UnityEngine.Random.Range(0, 26)]
		});
		pages[page][0] = text.ToUpperInvariant();
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Key: {1}", new object[]
			{
				moduleId,
				text
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Key: {1}", new object[]
			{
				moduleId,
				text
			});
		}
		char[] array = new char[6];
		for (int i = 0; i < 3; i++)
		{
			string[] array2 = new string[]
			{
				"ABCDEFGHIJKLM",
				"NOPQRSTUVWXYZ",
				"ACEGIKMOQSUWY",
				"BDFHJLNPRTVXZ"
			};
			int num = array2[2].IndexOf(text[i]);
			if (num < 0)
			{
				num = array2[3].IndexOf(text[i]);
			}
			array2[1] = array2[1].Substring(num) + string.Empty + array2[1].Substring(0, num);
			array2[2] = array2[2].Substring(num) + string.Empty + array2[2].Substring(0, num);
			array2[3] = array2[3].Substring(num) + string.Empty + array2[3].Substring(0, num);
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Slides:", new object[]
				{
					moduleId
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}", new object[]
				{
					moduleId,
					array2[0]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}", new object[]
				{
					moduleId,
					array2[1]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}", new object[]
				{
					moduleId,
					array2[2]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}", new object[]
				{
					moduleId,
					array2[3]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Slides:", new object[]
				{
					moduleId
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}", new object[]
				{
					moduleId,
					array2[0]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}", new object[]
				{
					moduleId,
					array2[1]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}", new object[]
				{
					moduleId,
					array2[2]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}", new object[]
				{
					moduleId,
					array2[3]
				});
			}
			int num2 = array2[0].IndexOf(word[i]);
			int num3 = 0;
			if (num2 < 0)
			{
				num2 = array2[1].IndexOf(word[i]);
				num3 = 1;
			}
			int num4 = array2[2].IndexOf(word[i + 3]);
			int num5 = 2;
			if (num4 < 0)
			{
				num4 = array2[3].IndexOf(word[i + 3]);
				num5 = 3;
			}
			if (num2 == num4)
			{
				array[i] = array2[(num3 + 1) % 2][num2];
				array[i + 3] = array2[(num5 - 1) % 2 + 2][num4];
			}
			else
			{
				array[i] = array2[num3][num4];
				array[i + 3] = array2[num5][num2];
			}
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1}{2} -> {3}{4}", new object[]
				{
					moduleId,
					word[i],
					word[i + 3],
					array[i],
					array[i + 3]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1}{2} -> {3}{4}", new object[]
				{
					moduleId,
					word[i],
					word[i + 3],
					array[i],
					array[i + 3]
				});
			}
		}
		return string.Concat(new object[]
		{
			array[0],
			string.Empty,
			array[1],
			string.Empty,
			array[2],
			string.Empty,
			array[3],
			string.Empty,
			array[4],
			string.Empty,
			array[5]
		});
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00011870 File Offset: 0x0000FC70
	private string ColumnTrans(string word, bool invert)
	{
		int num = UnityEngine.Random.Range(0, 5) + 2;
		string text = "123456".Substring(0, num);
		string text2 = string.Empty;
		for (int i = 0; i < num; i++)
		{
			int num2 = UnityEngine.Random.Range(0, text.Length);
			text2 = text2 + string.Empty + text[num2];
			text = text.Substring(0, num2) + string.Empty + text.Substring(num2 + 1);
		}
		string[] array = new string[num];
		pages[page - 1][2] = text2.ToUpperInvariant();
		if (invert)
		{
			for (int j = 0; j < 6; j++)
			{
				array[j % num] = array[j % num] + "*";
			}
			string text3 = string.Empty;
			int num3 = 0;
			for (int k = 0; k < num; k++)
			{
				string text4 = k + 1 + string.Empty;
				int num4 = text2.IndexOf(text4[0]);
				int length = array[num4].Length;
				array[num4] = string.Empty;
				for (int l = 0; l < length; l++)
				{
					array[num4] = array[num4] + string.Empty + word[num3];
					num3++;
				}
			}
			for (int m = 0; m < num; m++)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Column {1}: {2}", new object[]
				{
					moduleId,
					m + 1,
					array[m]
				});
			}
			for (int n = 0; n < 6; n++)
			{
				text3 = text3 + string.Empty + array[n % array.Length][0];
				array[n % array.Length] = array[n % array.Length].Substring(1);
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1} -> {2}", new object[]
			{
				moduleId,
				word,
				text3
			});
			return text3;
		}
		for (int num5 = 0; num5 < 6; num5++)
		{
			array[num5 % num] = array[num5 % num] + string.Empty + word[num5];
		}
		string text5 = string.Empty;
		for (int num6 = 0; num6 < num; num6++)
		{
			string text6 = num6 + 1 + string.Empty;
			text5 = text5 + string.Empty + array[text2.IndexOf(text6[0])];
			Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Column {1}: {2}", new object[]
			{
				moduleId,
				num6 + 1,
				array[text2.IndexOf(text6[0])]
			});
		}
		Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1} -> {2}", new object[]
		{
			moduleId,
			word,
			text5
		});
		return text5;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00011B98 File Offset: 0x0000FF98
	private string BitSwitchEnc(string word, bool invert)
	{
		string text = string.Empty;
		string text2 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string[] array = new string[]
		{
			"21453",
			"21534",
			"31524",
			"31452",
			"41523",
			"41532",
			"51234",
			"51423",
			"51432",
			"23154",
			"25134",
			"24153",
			"34152",
			"35124",
			"45123",
			"45132",
			"54123",
			"54132",
			"24513",
			"25413",
			"34512",
			"35214",
			"35412",
			"43512",
			"45213",
			"53412",
			"54213",
			"24531",
			"25431",
			"34521",
			"35421",
			"43251",
			"43521",
			"45231",
			"53421",
			"54231"
		};
		List<string> list = new List<string>
		{
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
			"11001",
			"11010"
		};
		string text4 = array[text2.IndexOf(Bomb.GetSerialNumber()[0])];
		string text5 = string.Empty;
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] Scrambler: {1}", new object[]
			{
				moduleId,
				text4
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] Scrambler: {1}", new object[]
			{
				moduleId,
				text4
			});
		}
		for (int i = 0; i < 6; i++)
		{
			string text6 = list[text3.IndexOf(word[i])];
			string text7 = scrambling(text6, text4, invert);
			if (!list.Contains(text7))
			{
				text7 = text7.Replace("1", "*");
				text7 = text7.Replace("0", "1");
				text7 = text7.Replace("*", "0");
				text5 += "1";
			}
			else
			{
				string text8 = text7.Replace("1", "*");
				text8 = text8.Replace("0", "1");
				text8 = text8.Replace("*", "0");
				if (list.Contains(text8) && UnityEngine.Random.Range(0, 3) == 0)
				{
					text7 = text8.ToUpperInvariant();
					text5 += "1";
				}
				else
				{
					text5 += "0";
				}
			}
			text = text + string.Empty + text3[list.IndexOf(text7)];
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GRAY] {1} -> {2} + {3} -> {4} -> {5}", new object[]
				{
					moduleId,
					word[i],
					text6,
					text5[i],
					text7,
					text[i]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [GRAY] {1} -> {2} + {3} -> {4} -> {5}", new object[]
				{
					moduleId,
					word[i],
					text6,
					text5[i],
					text7,
					text[i]
				});
			}
		}
		pages[page - 1][1] = text5;
		return text;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000120B4 File Offset: 0x000104B4
	private string scrambling(string bin, string scrambler, bool invert)
	{
		char[] array = new char[5];
		if (invert)
		{
			for (int i = 0; i < 5; i++)
			{
				array[i] = bin[(int)(scrambler[i] - '0' - '\u0001')];
			}
		}
		else
		{
			for (int j = 0; j < 5; j++)
			{
				array[(int)(scrambler[j] - '0' - '\u0001')] = bin[j];
			}
		}
		return string.Concat(new object[]
		{
			array[0],
			string.Empty,
			array[1],
			string.Empty,
			array[2],
			string.Empty,
			array[3],
			string.Empty,
			array[4]
		});
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00012184 File Offset: 0x00010584
	private string whitecipher(string word, bool invert)
	{
		fontsizes[page][0] = 35;
		fontsizes[page][1] = 35;
		fontsizes[page][2] = 40;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 40;
		fontsizes[page - 1][2] = 40;
		string text = string.Empty;
		List<int> list = new List<int>();
		switch (UnityEngine.Random.Range(0, 6))
		{
			case 0:
				list.Add(4);
				list.Add(4);
				break;
			case 1:
				list.Add(4);
				list.Add(0);
				list.Add(0);
				break;
			case 2:
				list.Add(3);
				list.Add(1);
				list.Add(0);
				break;
			case 3:
				list.Add(2);
				list.Add(2);
				list.Add(0);
				break;
			case 4:
				list.Add(2);
				list.Add(1);
				list.Add(1);
				break;
			default:
				list.Add(0);
				list.Add(0);
				list.Add(0);
				list.Add(0);
				break;
		}
		list.Shuffle<List<int>>();
		for (int i = 0; i < list.Count; i++)
		{
			string text2 = wordList[list[i]][UnityEngine.Random.Range(0, wordList[list[i]].Count)].ToUpperInvariant();
			wordList[list[i]].Remove(text2.ToUpperInvariant());
			text = text + string.Empty + text2.ToUpperInvariant();
		}
		if (invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted White Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Begin Base Caesar Encryption", new object[]
			{
				moduleId
			});
			string text3 = BaseCaesarEnc(word.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Begin Sean Encryption", new object[]
			{
				moduleId
			});
			text3 = SeanEnc(text3.ToUpperInvariant(), text.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Begin Grille Transposition", new object[]
			{
				moduleId
			});
			GrilleTrans(text.ToUpperInvariant(), invert);
			return text3;
		}
		chosentextcolors[page] = Color.white;
		chosenscreencolors[page] = screencolors[0];
		chosentextcolors[page - 1] = Color.white;
		chosenscreencolors[page - 1] = screencolors[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] Begin White Cipher", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Begin Sean Encryption", new object[]
		{
			moduleId
		});
		string text4 = SeanEnc(word.ToUpperInvariant(), text.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Begin Base Caesar Encryption", new object[]
		{
			moduleId
		});
		text4 = BaseCaesarEnc(text4.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Begin Grille Transposition", new object[]
		{
			moduleId
		});
		GrilleTrans(text.ToUpperInvariant(), invert);
		return text4;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000125F8 File Offset: 0x000109F8
	private string SeanEnc(string word, string kw1, bool invert)
	{
		string text = string.Empty;
		string key = getKey(kw1.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetOnIndicators().Count<string>() % 2 == 0);
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Key: {1}", new object[]
			{
				moduleId,
				key
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Key: {1}", new object[]
			{
				moduleId,
				key
			});
		}
		string[] array = new string[]
		{
			key.Substring(0, 13),
			key.Substring(13)
		};
		bool flag = "ZABCDEFGHIJKLMNOPQRSTUVWXY".IndexOf(Bomb.GetSerialNumberLetters().ToArray<char>()[1]) % 2 == 1;
		if (invert)
		{
			if (flag)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Alphanumeric position of the 2nd letter of the Serial Number is odd. Turning the key clockwise.", new object[]
				{
					moduleId
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Alphanumeric position of the 2nd letter of the Serial Number is even. Turning the key counter-clockwise.", new object[]
				{
					moduleId
				});
			}
		}
		else if (flag)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Alphanumeric position of the 2nd letter of the Serial Number is odd. Turning the key clockwise.", new object[]
			{
				moduleId
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Alphanumeric position of the 2nd letter of the Serial Number is even. Turning the key counter-clockwise.", new object[]
			{
				moduleId
			});
		}
		for (int i = 0; i < 6; i++)
		{
			if (array[0].IndexOf(word[i]) >= 0)
			{
				text = text + string.Empty + array[1][array[0].IndexOf(word[i])];
			}
			else
			{
				text = text + string.Empty + array[0][array[1].IndexOf(word[i])];
			}
			if (flag)
			{
				char c = array[0][12];
				char c2 = array[1][0];
				array[0] = c2 + string.Empty + array[0].Substring(0, 12);
				array[1] = array[1].Substring(1) + string.Empty + c;
				if (invert)
				{
					Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1} -> {2} ", new object[]
					{
						moduleId,
						word[i],
						text[i]
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1}", new object[]
					{
						moduleId,
						array[0]
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1}", new object[]
					{
						moduleId,
						array[1]
					});
				}
				else
				{
					Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1} -> {2} ", new object[]
					{
						moduleId,
						word[i],
						text[i]
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1}", new object[]
					{
						moduleId,
						array[0]
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1}", new object[]
					{
						moduleId,
						array[1]
					});
				}
			}
			else
			{
				char c3 = array[0][0];
				char c4 = array[1][12];
				array[0] = array[0].Substring(1) + string.Empty + c4;
				array[1] = c3 + string.Empty + array[1].Substring(0, 12);
				if (invert)
				{
					Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1} -> {2} ", new object[]
					{
						moduleId,
						word[i],
						text[i]
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1}", new object[]
					{
						moduleId,
						array[0]
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1}", new object[]
					{
						moduleId,
						array[1]
					});
				}
				else
				{
					Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1} -> {2} ", new object[]
					{
						moduleId,
						word[i],
						text[i]
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1}", new object[]
					{
						moduleId,
						array[0]
					});
					Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1}", new object[]
					{
						moduleId,
						array[1]
					});
				}
			}
		}
		return text;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00012AC0 File Offset: 0x00010EC0
	private string BaseCaesarEnc(string word, bool invert)
	{
		int num = UnityEngine.Random.Range(1, 25) + 26 * UnityEngine.Random.Range(1, 6);
		int num2 = 0;
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Generated Offset: {1}", new object[]
			{
				moduleId,
				num
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Generated Offset: {1}", new object[]
			{
				moduleId,
				num
			});
		}
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text2 = string.Empty;
		string text3 = string.Empty;
		for (int i = 0; i < 6; i++)
		{
			if (invert)
			{
				text2 = text2 + string.Empty + text[correction(text.IndexOf(word[i]) + num, 26)];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2[i]
				});
			}
			else
			{
				text2 = text2 + string.Empty + text[correction(text.IndexOf(word[i]) - num, 26)];
				Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2[i]
				});
			}
			num2 += text.IndexOf(text2[i]) + 1;
			text3 = string.Concat(new object[]
			{
				text3,
				string.Empty,
				text.IndexOf(text2[i]) + 1,
				" + "
			});
		}
		num2 = num2 % 8 + 2;
		text3 = string.Concat(new object[]
		{
			"((",
			text3.Substring(0, text3.Length - 2),
			") % 8) + 2 = ",
			num2
		});
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Generated Base: {1}", new object[]
			{
				moduleId,
				text3
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Generated Base: {1}", new object[]
			{
				moduleId,
				text3
			});
		}
		string text4 = string.Empty;
		for (int num3 = num; num3 != 0; num3 /= num2)
		{
			text4 = num3 % num2 + string.Empty + text4;
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1} -> {2}", new object[]
			{
				moduleId,
				num,
				text4
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1} -> {2}", new object[]
			{
				moduleId,
				num,
				text4
			});
		}
		pages[page - 1][1] = text4.ToUpperInvariant();
		return text2;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00012DD8 File Offset: 0x000111D8
	private void GrilleTrans(string word, bool invert)
	{
		int num = Bomb.GetPortCount() % 4;
		int[] array = new int[]
		{
			0,
			12,
			13,
			4,
			8,
			5,
			9,
			1,
			10,
			2,
			14,
			3,
			15,
			6,
			7,
			11
		};
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < num; j++)
			{
				array[i] -= 4;
			}
			array[i] = correction(array[i], 16);
		}
		string text = string.Empty;
		for (int k = 0; k < 16; k++)
		{
			text = text + string.Empty + word[array[k]];
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] Clockwise rotations: {1}", new object[]
			{
				moduleId,
				num
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV WHITE] {1} -> {2}", new object[]
			{
				moduleId,
				word,
				text
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] Clockwise rotations: {1}", new object[]
			{
				moduleId,
				num
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [WHITE] {1} -> {2}", new object[]
			{
				moduleId,
				word,
				text
			});
		}
		pages[page][0] = text.Substring(0, 8);
		pages[page][1] = text.Substring(8);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00012F58 File Offset: 0x00011358
	private string violetcipher(string word, bool invert)
	{
		fontsizes[page][0] = 35;
		fontsizes[page][1] = 40;
		fontsizes[page][2] = 40;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 40;
		fontsizes[page - 1][2] = 40;
		string text = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
		wordList[2].Remove(text.ToUpperInvariant());
		if (invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Violet Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] Begin Porta Encryption", new object[]
			{
				moduleId
			});
			string text2 = PortaEnc(word.ToUpperInvariant(), text, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] Begin Route Transposition", new object[]
			{
				moduleId
			});
			text2 = RouteTrans(text2.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] Begin Quagmire Encryption", new object[]
			{
				moduleId
			});
			return QuagmireEnc(text2, text.ToUpperInvariant(), invert);
		}
		chosentextcolors[page] = Color.white;
		chosenscreencolors[page] = screencolors[0];
		chosentextcolors[page - 1] = Color.white;
		chosenscreencolors[page - 1] = screencolors[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] Begin Violet Cipher", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] Begin Quagmire Encryption", new object[]
		{
			moduleId
		});
		string text3 = QuagmireEnc(word, text.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] Begin Route Transposition", new object[]
		{
			moduleId
		});
		text3 = RouteTrans(text3.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] Begin Porta Encryption", new object[]
		{
			moduleId
		});
		return PortaEnc(text3.ToUpperInvariant(), text, invert);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000132A8 File Offset: 0x000116A8
	private string PortaEnc(string word, string kw1, bool invert)
	{
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string[] array = new string[]
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
		string text2 = string.Empty;
		for (int i = 0; i < 6; i++)
		{
			if (text.IndexOf(word[i]) < 13)
			{
				text2 = text2 + string.Empty + array[text.IndexOf(kw1[i]) / 2][text.IndexOf(word[i])];
			}
			else
			{
				text2 = text2 + string.Empty + text[array[text.IndexOf(kw1[i]) / 2].IndexOf(word[i])];
			}
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2[i]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2[i]
				});
			}
		}
		return text2;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x0001344C File Offset: 0x0001184C
	private string RouteTrans(string word, bool invert)
	{
		string text = UnityEngine.Random.Range(1, 3) + string.Empty + UnityEngine.Random.Range(1, 7);
		pages[page - 1][2] = text;
		string text2 = string.Empty;
		string text3 = string.Empty;
		char c = text[0];
		if (c != '1')
		{
			switch (text[1])
			{
				case '1':
					text3 = "126354";
					break;
				case '2':
					text3 = "231465";
					break;
				case '3':
					text3 = "342516";
					break;
				case '4':
					text3 = "453621";
					break;
				case '5':
					text3 = "564132";
					break;
				default:
					text3 = "615243";
					break;
			}
		}
		else
		{
			switch (text[1])
			{
				case '1':
					text3 = "123654";
					break;
				case '2':
					text3 = "234165";
					break;
				case '3':
					text3 = "345216";
					break;
				case '4':
					text3 = "456321";
					break;
				case '5':
					text3 = "561432";
					break;
				default:
					text3 = "612543";
					break;
			}
		}
		string text4 = "123456";
		for (int i = 0; i < 6; i++)
		{
			if (invert)
			{
				text2 = text2 + string.Empty + word[text4.IndexOf(text3[i])];
			}
			else
			{
				text2 = text2 + string.Empty + word[text3.IndexOf(text4[i])];
			}
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1} + {2} => {3}", new object[]
			{
				moduleId,
				word,
				text,
				text2
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1} + {2} => {3}", new object[]
			{
				moduleId,
				word,
				text,
				text2
			});
		}
		return text2;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0001366C File Offset: 0x00011A6C
	private string QuagmireEnc(string word, string kw1, bool invert)
	{
		int index = UnityEngine.Random.Range(0, wordList.Count);
		string text = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
		wordList[index].Remove(text.ToUpperInvariant());
		string key = getKey(text.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetOnIndicators().Count<string>() % 2 == 0);
		string[] array = new string[7];
		array[0] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		for (int i = 0; i < 6; i++)
		{
			array[i + 1] = key.Substring(key.IndexOf(kw1[i])) + string.Empty + key.Substring(0, key.IndexOf(kw1[i]));
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] Quagmire Rows: ", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", new object[]
			{
				moduleId,
				array[1]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", new object[]
			{
				moduleId,
				array[2]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", new object[]
			{
				moduleId,
				array[3]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", new object[]
			{
				moduleId,
				array[4]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", new object[]
			{
				moduleId,
				array[5]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1}", new object[]
			{
				moduleId,
				array[6]
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] Quagmire Rows: ", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", new object[]
			{
				moduleId,
				array[1]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", new object[]
			{
				moduleId,
				array[2]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", new object[]
			{
				moduleId,
				array[3]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", new object[]
			{
				moduleId,
				array[4]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", new object[]
			{
				moduleId,
				array[5]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1}", new object[]
			{
				moduleId,
				array[6]
			});
		}
		pages[page][0] = text.ToUpperInvariant();
		pages[page - 1][1] = kw1.ToUpperInvariant();
		string text2 = string.Empty;
		for (int j = 0; j < 6; j++)
		{
			if (invert)
			{
				text2 = text2 + string.Empty + array[0][array[j + 1].IndexOf(word[j])];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV VIOLET] {1} -> {2}", new object[]
				{
					moduleId,
					word[j],
					text2[j]
				});
			}
			else
			{
				text2 = text2 + string.Empty + array[j + 1][array[0].IndexOf(word[j])];
				Debug.LogFormat("[Ultimate Cipher #{0}] [VIOLET] {1} -> {2}", new object[]
				{
					moduleId,
					word[j],
					text2[j]
				});
			}
		}
		return text2;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00013A80 File Offset: 0x00011E80
	private string indigocipher(string word, bool invert)
	{
		fontsizes[page][0] = 40;
		fontsizes[page][1] = 40;
		fontsizes[page][2] = 35;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 30;
		fontsizes[page - 1][2] = 35;
		int index = UnityEngine.Random.Range(0, wordList.Count);
		string text = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
		wordList[index].Remove(text.ToUpperInvariant());
		string key = getKey(text.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetPortCount() % 2 == 1);
		if (invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Indigo Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Begin Condi Encryption", new object[]
			{
				moduleId
			});
			string text2 = CondiEnc(word.ToUpperInvariant(), key.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Begin Logic Encryption", new object[]
			{
				moduleId
			});
			text2 = LogicEnc(text2.ToUpperInvariant(), invert);
			string text3 = text2.Split(new char[]
			{
				' '
			})[1];
			text2 = text2.Split(new char[]
			{
				' '
			})[0];
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Begin Fractionated Morse Encryption", new object[]
			{
				moduleId
			});
			string text4 = FractionatedMorseEnc(text3.ToUpperInvariant(), key.ToUpperInvariant(), invert);
			pages[page - 1][2] = text.ToUpperInvariant();
			pages[page - 1][1] = text4.ToUpperInvariant();
			return text2;
		}
		chosentextcolors[page] = Color.white;
		chosenscreencolors[page] = screencolors[0];
		chosentextcolors[page - 1] = Color.white;
		chosenscreencolors[page - 1] = screencolors[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] Begin Indigo Cipher", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Begin Logic Encryption", new object[]
		{
			moduleId
		});
		string text5 = LogicEnc(word.ToUpperInvariant(), invert);
		string text6 = text5.Split(new char[]
		{
			' '
		})[1];
		text5 = text5.Split(new char[]
		{
			' '
		})[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Begin Condi Encryption", new object[]
		{
			moduleId
		});
		text5 = CondiEnc(text5, key.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Begin Fractionated Morse Encryption", new object[]
		{
			moduleId
		});
		string text7 = FractionatedMorseEnc(text6.ToUpperInvariant(), key.ToUpperInvariant(), invert);
		pages[page - 1][2] = text.ToUpperInvariant();
		pages[page - 1][1] = text7.ToUpperInvariant();
		return text5;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00013ED0 File Offset: 0x000122D0
	private string LogicEnc(string word, bool invert)
	{
		int i = UnityEngine.Random.Range(0, 3) + 2;
		string text = "012345";
		string text2 = string.Empty;
		string text3 = string.Empty;
		for (int j = 0; j < i; j++)
		{
			int num = UnityEngine.Random.Range(0, text.Length);
			text2 = text2 + string.Empty + text[num];
			text = text.Substring(0, num) + string.Empty + text.Substring(num + 1);
		}
		string text4 = string.Empty;
		text = "012345";
		for (int k = 0; k < 6; k++)
		{
			if (text2.IndexOf(text[k]) >= 0)
			{
				text4 += "1";
			}
			else
			{
				text4 += "0";
				text3 = text3 + string.Empty + k;
			}
		}
		i = UnityEngine.Random.Range(0, 3);
		char[] array = new char[6];
		char c = text2[UnityEngine.Random.Range(0, text2.Length)];
		array[(int)(c - '0')] = '1';
		text2 = text2.Substring(0, text2.IndexOf(c)) + string.Empty + text2.Substring(text2.IndexOf(c) + 1);
		text = text.Substring(0, text.IndexOf(c)) + string.Empty + text.Substring(text.IndexOf(c) + 1);
		c = text2[UnityEngine.Random.Range(0, text2.Length)];
		array[(int)(c - '0')] = '0';
		text2 = text2.Substring(0, text2.IndexOf(c)) + string.Empty + text2.Substring(text2.IndexOf(c) + 1);
		text = text.Substring(0, text.IndexOf(c)) + string.Empty + text.Substring(text.IndexOf(c) + 1);
		c = text3[UnityEngine.Random.Range(0, text3.Length)];
		array[(int)(c - '0')] = '1';
		text3 = text3.Substring(0, text3.IndexOf(c)) + string.Empty + text3.Substring(text3.IndexOf(c) + 1);
		text = text.Substring(0, text.IndexOf(c)) + string.Empty + text.Substring(text.IndexOf(c) + 1);
		c = text3[UnityEngine.Random.Range(0, text3.Length)];
		array[(int)(c - '0')] = '0';
		text3 = text3.Substring(0, text3.IndexOf(c)) + string.Empty + text3.Substring(text3.IndexOf(c) + 1);
		text = text.Substring(0, text.IndexOf(c)) + string.Empty + text.Substring(text.IndexOf(c) + 1);
		while (i > 0)
		{
			i--;
			c = text[UnityEngine.Random.Range(0, text.Length)];
			array[(int)(c - '0')] = '1';
			text = text.Substring(0, text.IndexOf(c)) + string.Empty + text.Substring(text.IndexOf(c) + 1);
		}
		for (int l = 0; l < text.Length; l++)
		{
			array[(int)(text[l] - '0')] = '0';
		}
		string text5 = string.Empty;
		string text6 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		List<string> list = new List<string>
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
		int num2 = UnityEngine.Random.Range(0, 8);
		string[] array2 = new string[]
		{
			"AND",
			"OR",
			"XOR",
			"NAND",
			"NOR",
			"XNOR",
			"->",
			"<-"
		};
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Gate: {1}", new object[]
			{
				moduleId,
				array2[num2]
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Gate: {1}", new object[]
			{
				moduleId,
				array2[num2]
			});
		}
		string text7 = string.Empty;
		string text8 = string.Empty;
		string text9 = string.Empty;
		string text10 = string.Empty;
		for (int m = 0; m < 6; m++)
		{
			string text11 = list[text6.IndexOf(word[m])];
			string text12 = string.Empty;
			string text13 = string.Empty;
			for (int n = 0; n < 5; n++)
			{
				switch (num2)
				{
					case 0:
						if (text11[n] == '1')
						{
							text12 += "1";
							text13 += "1";
						}
						else
						{
							string[] array3 = new string[]
							{
							"01",
							"10",
							"00"
							};
							string text14 = array3[UnityEngine.Random.Range(0, array3.Length)];
							text12 = text12 + string.Empty + text14[0];
							text13 = text13 + string.Empty + text14[1];
						}
						break;
					case 1:
						if (text11[n] == '1')
						{
							string[] array4 = new string[]
							{
							"01",
							"10",
							"11"
							};
							string text15 = array4[UnityEngine.Random.Range(0, array4.Length)];
							text12 = text12 + string.Empty + text15[0];
							text13 = text13 + string.Empty + text15[1];
						}
						else
						{
							text12 += "0";
							text13 += "0";
						}
						break;
					case 2:
						if (text11[n] == '1')
						{
							string[] array5 = new string[]
							{
							"01",
							"10"
							};
							string text16 = array5[UnityEngine.Random.Range(0, array5.Length)];
							text12 = text12 + string.Empty + text16[0];
							text13 = text13 + string.Empty + text16[1];
						}
						else
						{
							string[] array6 = new string[]
							{
							"00",
							"11"
							};
							string text17 = array6[UnityEngine.Random.Range(0, array6.Length)];
							text12 = text12 + string.Empty + text17[0];
							text13 = text13 + string.Empty + text17[1];
						}
						break;
					case 3:
						if (text11[n] == '1')
						{
							string[] array7 = new string[]
							{
							"01",
							"10",
							"00"
							};
							string text18 = array7[UnityEngine.Random.Range(0, array7.Length)];
							text12 = text12 + string.Empty + text18[0];
							text13 = text13 + string.Empty + text18[1];
						}
						else
						{
							text12 += "1";
							text13 += "1";
						}
						break;
					case 4:
						if (text11[n] == '1')
						{
							text12 += "0";
							text13 += "0";
						}
						else
						{
							string[] array8 = new string[]
							{
							"01",
							"10",
							"11"
							};
							string text19 = array8[UnityEngine.Random.Range(0, array8.Length)];
							text12 = text12 + string.Empty + text19[0];
							text13 = text13 + string.Empty + text19[1];
						}
						break;
					case 5:
						if (text11[n] == '1')
						{
							string[] array9 = new string[]
							{
							"11",
							"00"
							};
							string text20 = array9[UnityEngine.Random.Range(0, array9.Length)];
							text12 = text12 + string.Empty + text20[0];
							text13 = text13 + string.Empty + text20[1];
						}
						else
						{
							string[] array10 = new string[]
							{
							"10",
							"01"
							};
							string text21 = array10[UnityEngine.Random.Range(0, array10.Length)];
							text12 = text12 + string.Empty + text21[0];
							text13 = text13 + string.Empty + text21[1];
						}
						break;
					case 6:
						if (text11[n] == '1')
						{
							string[] array11 = new string[]
							{
							"11",
							"00",
							"01"
							};
							string text22 = array11[UnityEngine.Random.Range(0, array11.Length)];
							text12 = text12 + string.Empty + text22[0];
							text13 = text13 + string.Empty + text22[1];
						}
						else
						{
							text12 += "1";
							text13 += "0";
						}
						break;
					default:
						if (text11[n] == '1')
						{
							string[] array12 = new string[]
							{
							"11",
							"00",
							"10"
							};
							string text23 = array12[UnityEngine.Random.Range(0, array12.Length)];
							text12 = text12 + string.Empty + text23[0];
							text13 = text13 + string.Empty + text23[1];
						}
						else
						{
							text12 += "0";
							text13 += "1";
						}
						break;
				}
			}
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1} + {2} = {3}", new object[]
				{
					moduleId,
					text12,
					text13,
					text11
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1} + {2} = {3}", new object[]
				{
					moduleId,
					text12,
					text13,
					text11
				});
			}
			if (!list.Contains(text12))
			{
				text9 += "1";
				text12 = text12.Replace("1", "*");
				text12 = text12.Replace("0", "1");
				text12 = text12.Replace("*", "0");
			}
			else
			{
				string text24 = text12.Replace("1", "*");
				text24 = text24.Replace("0", "1");
				text24 = text24.Replace("*", "0");
				if (list.Contains(text24) && UnityEngine.Random.Range(0, 3) == 0)
				{
					text9 += "1";
					text12 = text24.ToUpperInvariant();
				}
				else
				{
					text9 += "0";
				}
			}
			if (!list.Contains(text13))
			{
				text10 += "1";
				text13 = text13.Replace("1", "*");
				text13 = text13.Replace("0", "1");
				text13 = text13.Replace("*", "0");
			}
			else
			{
				string text25 = text13.Replace("1", "*");
				text25 = text25.Replace("0", "1");
				text25 = text25.Replace("*", "0");
				if (list.Contains(text25) && UnityEngine.Random.Range(0, 3) == 0)
				{
					text10 += "1";
					text13 = text25.ToUpperInvariant();
				}
				else
				{
					text10 += "0";
				}
			}
			text8 = text8 + string.Empty + text6[list.IndexOf(text12)];
			text7 = text7 + string.Empty + text6[list.IndexOf(text13)];
			switch (num2)
			{
				case 0:
					if (text4[m] == '1' && array[m] == '1')
					{
						text5 += "1";
					}
					else
					{
						text5 += "0";
					}
					break;
				case 1:
					if (text4[m] == '1' || array[m] == '1')
					{
						text5 += "1";
					}
					else
					{
						text5 += "0";
					}
					break;
				case 2:
					if (text4[m] != array[m])
					{
						text5 += "1";
					}
					else
					{
						text5 += "0";
					}
					break;
				case 3:
					if (text4[m] == '1' && array[m] == '1')
					{
						text5 += "0";
					}
					else
					{
						text5 += "1";
					}
					break;
				case 4:
					if (text4[m] == '1' || array[m] == '1')
					{
						text5 += "0";
					}
					else
					{
						text5 += "1";
					}
					break;
				case 5:
					if (text4[m] != array[m])
					{
						text5 += "0";
					}
					else
					{
						text5 += "1";
					}
					break;
				case 6:
					if (text4[m] == '1' && array[m] == '0')
					{
						text5 += "0";
					}
					else
					{
						text5 += "1";
					}
					break;
				default:
					if (text4[m] == '0' && array[m] == '1')
					{
						text5 += "0";
					}
					else
					{
						text5 += "1";
					}
					break;
			}
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Logic Encryption Keyword: {1}", new object[]
			{
				moduleId,
				text7
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Logic Encrypted Word: {1}", new object[]
			{
				moduleId,
				text8
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Binary 1: {1}", new object[]
			{
				moduleId,
				text4
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Binary 2: {1}", new object[]
			{
				moduleId,
				string.Concat(new object[]
				{
					array[0],
					string.Empty,
					array[1],
					string.Empty,
					array[2],
					string.Empty,
					array[3],
					string.Empty,
					array[4],
					string.Empty,
					array[5]
				})
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Binary 3: {1}", new object[]
			{
				moduleId,
				text5
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Logic Encryption Keyword: {1}", new object[]
			{
				moduleId,
				text7
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Logic Encrypted Word: {1}", new object[]
			{
				moduleId,
				text8
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Binary 1: {1}", new object[]
			{
				moduleId,
				text4
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Binary 2: {1}", new object[]
			{
				moduleId,
				string.Concat(new object[]
				{
					array[0],
					string.Empty,
					array[1],
					string.Empty,
					array[2],
					string.Empty,
					array[3],
					string.Empty,
					array[4],
					string.Empty,
					array[5]
				})
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Binary 3: {1}", new object[]
			{
				moduleId,
				text5
			});
		}
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		for (int num6 = 0; num6 < 6; num6++)
		{
			int num7 = 1;
			for (int num8 = 0; num8 < 5 - num6; num8++)
			{
				num7 *= 2;
			}
			if (text4[num6] == '1')
			{
				num3 += num7;
			}
			if (array[num6] == '1')
			{
				num4 += num7;
			}
			if (text5[num6] == '1')
			{
				num5 += num7;
			}
		}
		pages[page][0] = text9;
		pages[page][1] = text10;
		pages[page][2] = string.Concat(new object[]
		{
			num3,
			" ? ",
			num4,
			" = ",
			num5
		});
		return text8 + " " + text7;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x000151D0 File Offset: 0x000135D0
	private string CondiEnc(string word, string key, bool invert)
	{
		int num = Bomb.GetSerialNumberNumbers().Sum();
		string text = string.Empty;
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Key: {1}", new object[]
			{
				moduleId,
				key
			});
			for (int i = 0; i < 6; i++)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Offset: {1}", new object[]
				{
					moduleId,
					num
				});
				text = text + string.Empty + key[correction(key.IndexOf(word[i]) - num, 26)];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text[i]
				});
				num = key.IndexOf(text[i]) + 1;
			}
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Key: {1}", new object[]
			{
				moduleId,
				key
			});
			for (int j = 0; j < 6; j++)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Offset: {1}", new object[]
				{
					moduleId,
					num
				});
				text = text + string.Empty + key[(key.IndexOf(word[j]) + num) % 26];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1} -> {2}", new object[]
				{
					moduleId,
					word[j],
					text[j]
				});
				num = key.IndexOf(word[j]) + 1;
			}
		}
		return text;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000153A8 File Offset: 0x000137A8
	private string FractionatedMorseEnc(string word, string key, bool invert)
	{
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Key: {1}", new object[]
			{
				moduleId,
				key
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Key: {1}", new object[]
			{
				moduleId,
				key
			});
		}
		string[] array = new string[]
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
		string[] array2 = new string[]
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
		int num = 0;
		string[] array3 = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		for (int i = 0; i < word.Length; i++)
		{
			int num2 = text.IndexOf(word[i]);
			string text2 = array[num2];
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2
				});
			}
			for (int j = 0; j < text2.Length; j++)
			{
				array3[num] = array3[num] + string.Empty + text2[j];
				num = (num + 1) % 3;
			}
			if (i != 5)
			{
				array3[num] += "x";
				num = (num + 1) % 3;
			}
		}
		if (array3[1].Length != array3[0].Length)
		{
			array3[1] = array3[1] + "x";
		}
		if (array3[2].Length != array3[0].Length)
		{
			array3[2] = array3[2] + "x";
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] Morse rows:", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1}", new object[]
			{
				moduleId,
				array3[0]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1}", new object[]
			{
				moduleId,
				array3[1]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1}", new object[]
			{
				moduleId,
				array3[2]
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] Morse rows:", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1}", new object[]
			{
				moduleId,
				array3[0]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1}", new object[]
			{
				moduleId,
				array3[1]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1}", new object[]
			{
				moduleId,
				array3[2]
			});
		}
		string text3 = string.Empty;
		for (int k = 0; k < array3[0].Length; k++)
		{
			string text4 = string.Concat(new object[]
			{
				array3[0][k],
				string.Empty,
				array3[1][k],
				string.Empty,
				array3[2][k]
			});
			for (int l = 0; l < array2.Length; l++)
			{
				if (text4.Equals(array2[l]))
				{
					if (invert)
					{
						Debug.LogFormat("[Ultimate Cipher #{0}] [INV INDIGO] {1} -> {2}", new object[]
						{
							moduleId,
							text4,
							key[l]
						});
					}
					else
					{
						Debug.LogFormat("[Ultimate Cipher #{0}] [INDIGO] {1} -> {2}", new object[]
						{
							moduleId,
							text4,
							key[l]
						});
					}
					text3 = text3 + string.Empty + key[l];
					break;
				}
			}
		}
		return text3;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0001599C File Offset: 0x00013D9C
	private string bluecipher(string word, bool invert)
	{
		fontsizes[page][0] = 35;
		fontsizes[page][1] = 40;
		fontsizes[page][2] = 40;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 40;
		fontsizes[page - 1][2] = 40;
		string text = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
		wordList[2].Remove(text.ToUpperInvariant());
		if (invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Blue Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] Begin Atbash Encryption", new object[]
			{
				moduleId
			});
			string text2 = Atbash(word.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] Begin Vigenere Encryption", new object[]
			{
				moduleId
			});
			text2 = VigenereEnc(text2.ToUpperInvariant(), text.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] Begin Tridigital Encryption", new object[]
			{
				moduleId
			});
			TridigitalEnc(text.ToUpperInvariant(), invert);
			return text2;
		}
		chosentextcolors[page] = Color.white;
		chosenscreencolors[page] = screencolors[0];
		chosentextcolors[page - 1] = Color.white;
		chosenscreencolors[page - 1] = screencolors[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] Begin Blue Cipher", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] Begin Vigenere Encryption", new object[]
		{
			moduleId
		});
		string text3 = VigenereEnc(word.ToUpperInvariant(), text.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] Begin Atbash Encryption", new object[]
		{
			moduleId
		});
		text3 = Atbash(text3.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] Begin Tridigital Encryption", new object[]
		{
			moduleId
		});
		TridigitalEnc(text.ToUpperInvariant(), invert);
		return text3;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00015CF4 File Offset: 0x000140F4
	private string VigenereEnc(string word, string kw, bool invert)
	{
		string text = "ZABCDEFGHIJKLMNOPQRSTUVWXY";
		string text2 = string.Empty;
		if (invert)
		{
			for (int i = 0; i < 6; i++)
			{
				text2 = text2 + string.Empty + text[correction(text.IndexOf(word[i]) - text.IndexOf(kw[i]), 26)];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] {1} - {2} = {3}", new object[]
				{
					moduleId,
					word[i],
					kw[i],
					text2[i]
				});
			}
		}
		else
		{
			for (int j = 0; j < 6; j++)
			{
				text2 = text2 + string.Empty + text[(text.IndexOf(word[j]) + text.IndexOf(kw[j])) % 26];
				Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] {1} + {2} = {3}", new object[]
				{
					moduleId,
					word[j],
					kw[j],
					text2[j]
				});
			}
		}
		return text2;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00015E44 File Offset: 0x00014244
	private string Atbash(string word, bool invert)
	{
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text2 = string.Empty;
		for (int i = 0; i < 6; i++)
		{
			text2 = text2 + string.Empty + text[25 - text.IndexOf(word[i])];
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2[i]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2[i]
				});
			}
		}
		return text2;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00015F1C File Offset: 0x0001431C
	private void TridigitalEnc(string word, bool invert)
	{
		int index = UnityEngine.Random.Range(0, wordList.Count);
		string text = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
		wordList[index].Remove(text.ToUpperInvariant());
		string key = getKey(text.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetIndicators().Count<string>() % 2 == 0);
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] Tridigital Key: {1}", new object[]
			{
				moduleId,
				key
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] Tridigital Key: {1}", new object[]
			{
				moduleId,
				key
			});
		}
		pages[page - 1][1] = string.Empty;
		pages[page - 1][2] = string.Empty;
		pages[page][0] = text.ToUpperInvariant();
		for (int i = 0; i < word.Length; i++)
		{
			pages[page - 1][1] = pages[page - 1][1] + string.Empty + (key.IndexOf(word[i]) / 9 + 1);
			pages[page - 1][2] = pages[page - 1][2] + string.Empty + (key.IndexOf(word[i]) % 9 + 1);
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLUE] {1} -> {2}{3}", new object[]
				{
					moduleId,
					word[i],
					pages[page - 1][1][i],
					pages[page - 1][2][i]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [BLUE] {1} -> {2}{3}", new object[]
				{
					moduleId,
					word[i],
					pages[page - 1][1][i],
					pages[page - 1][2][i]
				});
			}
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000161B8 File Offset: 0x000145B8
	private string greencipher(string word, bool invert)
	{
		fontsizes[page][0] = 40;
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
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Green Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Begin Ragbaby Encryption", new object[]
			{
				moduleId
			});
			string text = RagbabyEnc(word.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Begin Mechanical Encryption", new object[]
			{
				moduleId
			});
			string text2 = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
			wordList[2].Remove(text2.ToUpperInvariant());
			text = MechanicalEnc(text.ToUpperInvariant(), text2.ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Begin Homophonic Encryption", new object[]
			{
				moduleId
			});
			text2 = HomophonicEnc(text2, invert);
			pages[page - 1][1] = text2.Substring(0, 6);
			pages[page - 1][2] = text2.Substring(6);
			return text;
		}
		chosentextcolors[page] = Color.white;
		chosenscreencolors[page] = screencolors[0];
		chosentextcolors[page - 1] = Color.white;
		chosenscreencolors[page - 1] = screencolors[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] Begin Green Cipher", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Begin Mechanical Encryption", new object[]
		{
			moduleId
		});
		string text3 = wordList[2][UnityEngine.Random.Range(0, wordList[2].Count)].ToUpperInvariant();
		wordList[2].Remove(text3.ToUpperInvariant());
		string text4 = MechanicalEnc(word.ToUpperInvariant(), text3.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Begin Ragbaby Encryption", new object[]
		{
			moduleId
		});
		text4 = RagbabyEnc(text4.ToUpperInvariant(), invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Begin Homophonic Encryption", new object[]
		{
			moduleId
		});
		text3 = HomophonicEnc(text3, invert);
		pages[page - 1][1] = text3.Substring(0, 6);
		pages[page - 1][2] = text3.Substring(6);
		return text4;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000165B0 File Offset: 0x000149B0
	private string HomophonicEnc(string word, bool invert)
	{
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text2 = string.Concat(new object[]
		{
			text[UnityEngine.Random.Range(0, 26)],
			string.Empty,
			text[UnityEngine.Random.Range(0, 26)],
			string.Empty,
			text[UnityEngine.Random.Range(0, 26)]
		});
		int[][] array = new int[3][];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new int[26];
			for (int j = 0; j < 26; j++)
			{
				array[i][j] = j + 1 + 26 * i;
			}
		}
		for (int k = 0; k < 3; k++)
		{
			int num = text.IndexOf(text2[k]);
			for (int l = 0; l < num; l++)
			{
				array[k] = HomophonicRot(array[k]);
			}
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Row {1}: {2}", new object[]
				{
					moduleId,
					k + 1,
					array[k][0]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Row {1}: {2}", new object[]
				{
					moduleId,
					k + 1,
					array[k][0]
				});
			}
		}
		string text3 = "001122";
		string text4 = string.Empty;
		string text5 = string.Empty;
		for (int m = 0; m < 6; m++)
		{
			int num2 = UnityEngine.Random.Range(0, text3.Length);
			string text6 = array[(int)(text3[num2] - '0')][text.IndexOf(word[m])] + string.Empty;
			text3 = text3.Substring(0, num2) + string.Empty + text3.Substring(num2 + 1);
			if (text6.Length == 1)
			{
				text6 = "0" + text6[0];
			}
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] {1} -> {2}", new object[]
				{
					moduleId,
					word[m],
					text6
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] {1} -> {2}", new object[]
				{
					moduleId,
					word[m],
					text6
				});
			}
			text4 = text4 + string.Empty + text6[0];
			text5 = text5 + string.Empty + text6[1];
		}
		pages[page][0] = text2.ToUpperInvariant();
		return text4 + string.Empty + text5;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000168AC File Offset: 0x00014CAC
	private int[] HomophonicRot(int[] n)
	{
		int[] array = new int[26];
		for (int i = 1; i < 26; i++)
		{
			array[i] = n[i - 1];
		}
		array[0] = n[25];
		return array;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000168E4 File Offset: 0x00014CE4
	private string RagbabyEnc(string word, bool invert)
	{
		int index = UnityEngine.Random.Range(0, wordList.Count);
		string text = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
		wordList[index].Remove(text.ToUpperInvariant());
		pages[page][1] = text.ToUpperInvariant();
		string key = getKey(text.ToUpperInvariant(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetOffIndicators().Count<string>() % 2 == 0);
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] Generated Key: {1}", new object[]
			{
				moduleId,
				key
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] Generated Key: {1}", new object[]
			{
				moduleId,
				key
			});
		}
		string text2 = string.Empty;
		for (int i = 0; i < 6; i++)
		{
			if (invert)
			{
				text2 = text2 + string.Empty + key[correction(key.IndexOf(word[i]) - (i + 1), 26)];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2[i]
				});
			}
			else
			{
				text2 = text2 + string.Empty + key[(key.IndexOf(word[i]) + (i + 1)) % 26];
				Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] {1} -> {2}", new object[]
				{
					moduleId,
					word[i],
					text2[i]
				});
			}
		}
		return text2;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00016AD8 File Offset: 0x00014ED8
	private string MechanicalEnc(string word, string kw, bool invert)
	{
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string[] array = new string[]
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
		string text2 = string.Empty;
		if (invert)
		{
			for (int i = 0; i < 6; i++)
			{
				int num = text.IndexOf(kw[i]);
				int index = array[num].IndexOf(word[i]);
				text2 = text2 + string.Empty + text[index];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV GREEN] {1} + {2} -> {3}", new object[]
				{
					moduleId,
					kw[i],
					word[i],
					text2[i]
				});
			}
		}
		else
		{
			for (int j = 0; j < 6; j++)
			{
				int num2 = text.IndexOf(kw[j]);
				int index2 = text.IndexOf(word[j]);
				text2 = text2 + string.Empty + array[num2][index2];
				Debug.LogFormat("[Ultimate Cipher #{0}] [GREEN] {1} + {2} -> {3}", new object[]
				{
					moduleId,
					kw[j],
					word[j],
					text2[j]
				});
			}
		}
		return text2;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00016D20 File Offset: 0x00015120
	private string yellowcipher(string word, bool invert)
	{
		fontsizes[page][0] = 40;
		fontsizes[page][1] = 35;
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
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Yellow Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Begin Trifid Encryption", new object[]
			{
				moduleId
			});
			string text = TrifidEnc(word.ToUpperInvariant(), invert);
			string[] array = text.Split(new char[]
			{
				' '
			});
			text = array[0].ToUpperInvariant();
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Begin Morbit Encryption", new object[]
			{
				moduleId
			});
			string text2 = MorbitEnc(array[1].ToUpperInvariant(), invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Begin Hill Encryption", new object[]
			{
				moduleId
			});
			text = HillEnc(text, invert);
			int num = text2.Length / 3;
			pages[page - 1][1] = text2.Substring(0, num);
			pages[page - 1][2] = text2.Substring(num, num);
			pages[page][0] = text2.Substring(num + num);
			return text;
		}
		chosentextcolors[page] = Color.white;
		chosenscreencolors[page] = screencolors[0];
		chosentextcolors[page - 1] = Color.white;
		chosenscreencolors[page - 1] = screencolors[0];
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] Begin Yellow Cipher", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Begin Hill Encryption", new object[]
		{
			moduleId
		});
		string text3 = HillEnc(word, invert);
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Begin Trifid Encryption", new object[]
		{
			moduleId
		});
		text3 = TrifidEnc(text3, invert);
		string[] array2 = text3.Split(new char[]
		{
			' '
		});
		text3 = array2[0].ToUpperInvariant();
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Begin Morbit Encryption", new object[]
		{
			moduleId
		});
		string text4 = MorbitEnc(array2[1].ToUpperInvariant(), invert);
		int num2 = text4.Length / 3;
		pages[page - 1][1] = text4.Substring(0, num2);
		pages[page - 1][2] = text4.Substring(num2, num2);
		pages[page][0] = text4.Substring(num2 + num2);
		return text3;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0001710C File Offset: 0x0001550C
	private string MorbitEnc(string word, bool invert)
	{
		string[] array = new string[]
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
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text2 = string.Empty;
		for (int i = 0; i < word.Length; i++)
		{
			text2 = text2 + string.Empty + array[text.IndexOf(word[i])] + "X";
		}
		if (text2.Length % 2 == 1)
		{
			text2 = text2.Substring(0, text2.Length - 1);
		}
		char[] array2 = new char[8];
		int num = 0;
		string text3 = "12345678";
		string text4 = wordList[4][UnityEngine.Random.Range(0, wordList[4].Count)].ToUpperInvariant();
		wordList[4].Remove(text4.ToUpperInvariant());
		for (int j = 0; j < text.Length; j++)
		{
			for (int k = 0; k < text4.Length; k++)
			{
				if (text4[k].ToString().IndexOf(text[j]) >= 0)
				{
					array2[k] = text3[num];
					num++;
				}
			}
		}
		text3 = string.Concat(new object[]
		{
			array2[0],
			string.Empty,
			array2[1],
			string.Empty,
			array2[2],
			string.Empty,
			array2[3],
			string.Empty,
			array2[4],
			string.Empty,
			array2[5],
			string.Empty,
			array2[6],
			string.Empty,
			array2[7]
		});
		string text5 = string.Empty;
		for (int l = 0; l < text2.Length; l++)
		{
			string text6 = text2[l] + string.Empty + text2[l + 1];
			l++;
			switch (text6)
			{
				case "..":
					text5 = text5 + string.Empty + text3[0];
					break;
				case ".-":
					text5 = text5 + string.Empty + text3[1];
					break;
				case ".X":
					text5 = text5 + string.Empty + text3[2];
					break;
				case "-.":
					text5 = text5 + string.Empty + text3[3];
					break;
				case "--":
					text5 = text5 + string.Empty + text3[4];
					break;
				case "-X":
					text5 = text5 + string.Empty + text3[5];
					break;
				case "X.":
					text5 = text5 + string.Empty + text3[6];
					break;
				case "X-":
					text5 = text5 + string.Empty + text3[7];
					break;
			}
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Morbit Key: {1}", new object[]
			{
				moduleId,
				text3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Morbit Encrypted Word: {1}", new object[]
			{
				moduleId,
				text5
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Morbit Key: {1}", new object[]
			{
				moduleId,
				text3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Morbit Encrypted Word: {1}", new object[]
			{
				moduleId,
				text5
			});
		}
		pages[page][1] = text4.ToUpperInvariant();
		return text5;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000176A8 File Offset: 0x00015AA8
	private string TrifidEnc(string word, bool inverse)
	{
		List<string> list = new List<string>();
		string[] array = new string[]
		{
			"11111111122222222233333333",
			"11122233311122233311122233",
			"12312312312312312312312312"
		};
		string[] array2 = new string[3];
		bool flag;
		int index;
		string text;
		string key;
		string text2;
		do
		{
			flag = false;
			index = UnityEngine.Random.Range(0, wordList.Count);
			text = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
			while (list.Contains(text))
			{
				index = UnityEngine.Random.Range(0, wordList.Count);
				text = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
			}
			list.Add(text.ToUpperInvariant());
			key = getKey(text, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Bomb.GetBatteryCount() % 2 == 1);
			text2 = string.Empty;
			array2[0] = string.Empty;
			array2[1] = string.Empty;
			array2[2] = string.Empty;
			if (inverse)
			{
				for (int i = 0; i < 6; i++)
				{
					int index2 = key.IndexOf(word[i]);
					array2[i / 2] = array2[i / 2] + string.Empty + array[0][index2];
					array2[i / 2] = array2[i / 2] + string.Empty + array[1][index2];
					array2[i / 2] = array2[i / 2] + string.Empty + array[2][index2];
				}
				for (int j = 0; j < 6; j++)
				{
					string text3 = string.Concat(new object[]
					{
						array2[0][j],
						string.Empty,
						array2[1][j],
						string.Empty,
						array2[2][j]
					});
					if (text3.Equals("333"))
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				for (int k = 0; k < 6; k++)
				{
					int index3 = key.IndexOf(word[k]);
					array2[0] = array2[0] + string.Empty + array[0][index3];
					array2[1] = array2[1] + string.Empty + array[1][index3];
					array2[2] = array2[2] + string.Empty + array[2][index3];
				}
				for (int l = 0; l < 3; l++)
				{
					string text4 = string.Concat(new object[]
					{
						array2[l][0],
						string.Empty,
						array2[l][1],
						string.Empty,
						array2[l][2]
					});
					string text5 = string.Concat(new object[]
					{
						array2[l][3],
						string.Empty,
						array2[l][4],
						string.Empty,
						array2[l][5]
					});
					if (text4.Equals("333") || text5.Equals("333"))
					{
						flag = true;
						break;
					}
				}
			}
		}
		while (flag);
		if (inverse)
		{
			wordList[index].Remove(text.ToUpperInvariant());
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Trifid Key: {1}", new object[]
			{
				moduleId,
				key
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Trifid Numbers:", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1}", new object[]
			{
				moduleId,
				array2[0]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1}", new object[]
			{
				moduleId,
				array2[1]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1}", new object[]
			{
				moduleId,
				array2[2]
			});
			for (int m = 0; m < 6; m++)
			{
				string text6 = string.Concat(new object[]
				{
					array2[0][m],
					string.Empty,
					array2[1][m],
					string.Empty,
					array2[2][m]
				});
				string text7 = "931";
				int num = 0;
				for (int n = 0; n < 3; n++)
				{
					num += (int)((text7[n] - '0') * (text6[n] - '0' - '\u0001'));
				}
				text2 = text2 + string.Empty + key[num];
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Trifid Encrypted Word: {1}", new object[]
			{
				moduleId,
				text2
			});
			return text2 + " " + text;
		}
		wordList[index].Remove(text.ToUpperInvariant());
		list.Clear();
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Trifid Key: {1}", new object[]
		{
			moduleId,
			key
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Trifid Numbers:", new object[]
		{
			moduleId
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] {1}", new object[]
		{
			moduleId,
			array2[0]
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] {1}", new object[]
		{
			moduleId,
			array2[1]
		});
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] {1}", new object[]
		{
			moduleId,
			array2[2]
		});
		for (int num2 = 0; num2 < 3; num2++)
		{
			string[] array3 = new string[]
			{
				string.Concat(new object[]
				{
					array2[num2][0],
					string.Empty,
					array2[num2][1],
					string.Empty,
					array2[num2][2]
				}),
				string.Concat(new object[]
				{
					array2[num2][3],
					string.Empty,
					array2[num2][4],
					string.Empty,
					array2[num2][5]
				})
			};
			string text8 = "931";
			for (int num3 = 0; num3 < 2; num3++)
			{
				int num4 = 0;
				for (int num5 = 0; num5 < 3; num5++)
				{
					num4 += (int)((text8[num5] - '0') * (array3[num3][num5] - '0' - '\u0001'));
				}
				text2 = text2 + string.Empty + key[num4];
			}
		}
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Trifid Encrypted Word: {1}", new object[]
		{
			moduleId,
			text2
		});
		return text2 + " " + text;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00017E78 File Offset: 0x00016278
	private string HillEnc(string word, bool invert)
	{
		string text = string.Empty;
		int[] array = new int[4];
		array[1] = UnityEngine.Random.Range(0, 26);
		array[2] = UnityEngine.Random.Range(0, 26);
		if (array[1] * array[2] % 2 == 1)
		{
			array[0] = UnityEngine.Random.Range(1, 13) * 2;
		}
		else
		{
			array[0] = UnityEngine.Random.Range(0, 13) * 2 + 1;
			while ((array[0] - array[1] * array[2]) % 13 == 0)
			{
				array[0] = UnityEngine.Random.Range(0, 13) * 2 + 1;
			}
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] NUMBER A: {1}", new object[]
			{
				moduleId,
				array[0]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] NUMBER B: {1}", new object[]
			{
				moduleId,
				array[1]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] NUMBER C: {1}", new object[]
			{
				moduleId,
				array[2]
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] NUMBER A: {1}", new object[]
			{
				moduleId,
				array[0]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] NUMBER B: {1}", new object[]
			{
				moduleId,
				array[1]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] NUMBER C: {1}", new object[]
			{
				moduleId,
				array[2]
			});
		}
		int[] array2 = new int[]
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25
		};
		int aa;
		for (aa = 0; aa < 26; aa++)
		{
			int num = aa * array[0] - array[1] * array[2];
			if (num % 2 == 0 || num % 13 == 0)
			{
				array2 = (from val in array2
						  where val != aa
						  select val).ToArray<int>();
			}
		}
		array[3] = array2[UnityEngine.Random.Range(0, array2.Length)];
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] NUMBER D: {1}", new object[]
			{
				moduleId,
				array[3]
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] NUMBER D: {1}", new object[]
			{
				moduleId,
				array[3]
			});
		}
		string text2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		if (invert)
		{
			int[] array3 = new int[]
			{
				array[3],
				array[1] * -1,
				array[2] * -1,
				array[0]
			};
			int num2 = array[0] * array[3] - array[1] * array[2];
			int num3 = 1;
			while (correction(num3 * num2, 26) != 1)
			{
				num3++;
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] N: {1}", new object[]
			{
				moduleId,
				num3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Inverse of numbers:", new object[]
			{
				moduleId
			});
			for (int i = 0; i < 4; i++)
			{
				array3[i] = correction(array3[i] * num3, 26);
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1} -> {2}", new object[]
				{
					moduleId,
					array[i],
					array3[i]
				});
			}
			for (int j = 0; j < 6; j++)
			{
				string text3 = string.Empty;
				int num4 = (text2.IndexOf(word[j]) + 1) % 26;
				j++;
				int num5 = (text2.IndexOf(word[j]) + 1) % 26;
				text3 = string.Concat(new object[]
				{
					text3,
					string.Empty,
					num4,
					" ",
					num5
				});
				int num6 = (array3[0] * num4 + array3[1] * num5) % 26;
				int num7 = (array3[2] * num4 + array3[3] * num5) % 26;
				text3 = string.Concat(new object[]
				{
					text3,
					" -> ",
					num6,
					" ",
					num7
				});
				if (num6 == 0)
				{
					num6 = 26;
				}
				if (num7 == 0)
				{
					num7 = 26;
				}
				text = string.Concat(new object[]
				{
					text,
					string.Empty,
					text2[num6 - 1],
					string.Empty,
					text2[num7 - 1]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] {1}", new object[]
				{
					moduleId,
					text3
				});
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV YELLOW] Hill Encrypted Word: {1}", new object[]
			{
				moduleId,
				text
			});
			pages[page][2] = string.Concat(new object[]
			{
				array[0],
				"-",
				array[1],
				"-",
				array[2],
				"-",
				array[3]
			});
			return text;
		}
		for (int k = 0; k < 6; k++)
		{
			string text4 = string.Empty;
			int num8 = (text2.IndexOf(word[k]) + 1) % 26;
			k++;
			int num9 = (text2.IndexOf(word[k]) + 1) % 26;
			text4 = string.Concat(new object[]
			{
				text4,
				string.Empty,
				num8,
				" ",
				num9
			});
			int num10 = (array[0] * num8 + array[1] * num9) % 26;
			int num11 = (array[2] * num8 + array[3] * num9) % 26;
			text4 = string.Concat(new object[]
			{
				text4,
				" -> ",
				num10,
				" ",
				num11
			});
			if (num10 == 0)
			{
				num10 = 26;
			}
			if (num11 == 0)
			{
				num11 = 26;
			}
			text = string.Concat(new object[]
			{
				text,
				string.Empty,
				text2[num10 - 1],
				string.Empty,
				text2[num11 - 1]
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] {1}", new object[]
			{
				moduleId,
				text4
			});
		}
		Debug.LogFormat("[Ultimate Cipher #{0}] [YELLOW] Hill Encrypted Word: {1}", new object[]
		{
			moduleId,
			text
		});
		pages[page][2] = string.Concat(new object[]
		{
			array[0],
			"-",
			array[1],
			"-",
			array[2],
			"-",
			array[3]
		});
		return text;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000185B8 File Offset: 0x000169B8
	private string orangecipher(string word, bool invert)
	{
		fontsizes[page][0] = 35;
		fontsizes[page][1] = 35;
		fontsizes[page][2] = 40;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 40;
		fontsizes[page - 1][2] = 35;
		string text = string.Empty;
		bool[] array = new bool[6];
		for (int i = 0; i < 6; i++)
		{
			if (word[i] == 'J')
			{
				text += "ABCDEFGHIKLMNOPQRSTUVWXYZ"[UnityEngine.Random.Range(0, 25)];
				array[i] = true;
			}
			else
			{
				text = text + string.Empty + word[i];
			}
		}
		int index = UnityEngine.Random.Range(0, wordList.Count);
		string text2 = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
		wordList[index].Remove(text2.ToUpperInvariant());
		index = UnityEngine.Random.Range(0, wordList.Count);
		string text3 = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
		wordList[index].Remove(text3.ToUpperInvariant());
		string text4 = string.Empty;
		for (int j = 0; j < 4; j++)
		{
			text4 = text4 + string.Empty + UnityEngine.Random.Range(0, 10);
		}
		pages[page][1] = text2.ToUpperInvariant();
		pages[page][2] = text4.ToUpperInvariant();
		string text5 = text2.Replace('J', 'I');
		string text6 = "AFLQVBGMRWCHNSXDIOTYEKPUZ";
		string text7 = text3.Replace('J', 'I');
		string text8 = string.Empty;
		for (int k = 0; k < text4.Length; k++)
		{
			switch (text4[k])
			{
				case '0':
					text8 += "ZERO";
					break;
				case '1':
					text8 += "ONE";
					break;
				case '2':
					text8 += "TWO";
					break;
				case '3':
					text8 += "THREE";
					break;
				case '4':
					text8 += "FOUR";
					break;
				case '5':
					text8 += "FIVE";
					break;
				case '6':
					text8 += "SIX";
					break;
				case '7':
					text8 += "SEVEN";
					break;
				case '8':
					text8 += "EIGHT";
					break;
				case '9':
					text8 += "NINE";
					break;
			}
		}
		text8 = text8.Replace('J', 'I');
		string text9 = string.Empty;
		string serialNumber = Bomb.GetSerialNumber();
		for (int l = 0; l < 6; l++)
		{
			switch (serialNumber[l])
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
					text9 = text9 + string.Empty + serialNumber[l];
					break;
			}
		}
		text5 = getKey(text5, "ABCDEFGHIKLMNOPQRSTUVWXYZ", Bomb.GetSerialNumberNumbers().Last<int>() % 2 == 0);
		text8 = getKey(text8, "ABCDEFGHIKLMNOPQRSTUVWXYZ", (text9[1] - '0') % '\u0002' == '\u0001');
		text7 = getKey(text7, "ABCDEFGHIKLMNOPQRSTUVWXYZ", (text9[0] - '0') % '\u0002' == '\0');
		string text10;
		if (invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Orange Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Matrix A: {1}", new object[]
			{
				moduleId,
				text5
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Matrix B: {1}", new object[]
			{
				moduleId,
				text6
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Matrix C: {1}", new object[]
			{
				moduleId,
				text8
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Matrix D: {1}", new object[]
			{
				moduleId,
				text7
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Begin Bazeries Encryption", new object[]
			{
				moduleId
			});
			text = BazeriesEnc(text, text6, text8, text4, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Begin Foursquare Encryption", new object[]
			{
				moduleId
			});
			text = FoursquareEnc(text, text5, text6, text8, text7, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Begin Collon Encryption", new object[]
			{
				moduleId
			});
			text10 = CollonEnc(text3.Replace('J', 'I'), text5, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Encrypted Keyword: {1}", new object[]
			{
				moduleId,
				text10
			});
		}
		else
		{
			chosentextcolors[page] = Color.white;
			chosenscreencolors[page] = screencolors[0];
			chosentextcolors[page - 1] = Color.white;
			chosenscreencolors[page - 1] = screencolors[0];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Orange Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Matrix A: {1}", new object[]
			{
				moduleId,
				text5
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Matrix B: {1}", new object[]
			{
				moduleId,
				text6
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Matrix C: {1}", new object[]
			{
				moduleId,
				text8
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Matrix D: {1}", new object[]
			{
				moduleId,
				text7
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Begin Foursquare Encryption", new object[]
			{
				moduleId
			});
			text = FoursquareEnc(text, text5, text6, text8, text7, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Begin Bazeries Encryption", new object[]
			{
				moduleId
			});
			text = BazeriesEnc(text, text6, text8, text4, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Begin Collon Encryption", new object[]
			{
				moduleId
			});
			text10 = CollonEnc(text3.Replace('J', 'I'), text5, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Encrypted Keyword: {1}", new object[]
			{
				moduleId,
				text10
			});
		}
		pages[page - 1][2] = text10.Substring(0, text10.Length / 2);
		pages[page][0] = text10.Substring(text10.Length / 2);
		string text11 = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
		for (int m = 0; m < 6; m++)
		{
			if (array[m])
			{
				pages[page - 1][1] = pages[page - 1][1] + string.Empty + text[m];
				text = text.Substring(0, m) + "J" + text.Substring(m + 1);
			}
			else
			{
				pages[page - 1][1] = pages[page - 1][1] + string.Empty + text11[UnityEngine.Random.Range(0, 25)];
			}
		}
		return text;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00018EF4 File Offset: 0x000172F4
	private string FoursquareEnc(string word, string ma, string mb, string mc, string md, bool invert)
	{
		string text = string.Empty;
		for (int i = 0; i < 6; i++)
		{
			if (invert)
			{
				int num = mb.IndexOf(word[i]);
				int num2 = mc.IndexOf(word[i + 1]);
				i++;
				text = string.Concat(new object[]
				{
					text,
					string.Empty,
					ma[num / 5 * 5 + num2 % 5],
					string.Empty,
					md[num % 5 + num2 / 5 * 5]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1} -> {2}", new object[]
				{
					moduleId,
					word[i - 1] + string.Empty + word[i],
					text[i - 1] + string.Empty + text[i]
				});
			}
			else
			{
				int num3 = ma.IndexOf(word[i]);
				int num4 = md.IndexOf(word[i + 1]);
				i++;
				text = string.Concat(new object[]
				{
					text,
					string.Empty,
					mb[num3 / 5 * 5 + num4 % 5],
					string.Empty,
					mc[num3 % 5 + num4 / 5 * 5]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1} -> {2}", new object[]
				{
					moduleId,
					word[i - 1] + string.Empty + word[i],
					text[i - 1] + string.Empty + text[i]
				});
			}
		}
		return text;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000190E8 File Offset: 0x000174E8
	private string BazeriesEnc(string word, string mb, string mc, string num, bool invert)
	{
		string text = string.Empty;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < num.Length; i++)
		{
			num3 += (int)(num[i] - '0');
		}
		num3 = num3 % 4 + 2;
		for (int j = 0; j < word.Length; j++)
		{
			char c;
			if (invert)
			{
				c = mb[mc.IndexOf(word[j])];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1} -> {2}", new object[]
				{
					moduleId,
					word[j],
					c
				});
			}
			else
			{
				c = mc[mb.IndexOf(word[j])];
				Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1} -> {2}", new object[]
				{
					moduleId,
					word[j],
					c
				});
			}
			text = text + string.Empty + c;
			num2++;
			if (num2 == num3)
			{
				text += "|";
				num2 = 0;
			}
		}
		string[] array = text.Split(new char[]
		{
			'|'
		});
		string text2 = string.Empty;
		for (int k = 0; k < array.Length; k++)
		{
			char[] array2 = array[k].ToCharArray();
			Array.Reverse(array2);
			text2 = text2 + string.Empty + new string(array2);
		}
		if (invert)
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] Subgroup Number: {1}", new object[]
			{
				moduleId,
				num3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1} -> {2}", new object[]
			{
				moduleId,
				text,
				text2
			});
		}
		else
		{
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] Subgroup Number: {1}", new object[]
			{
				moduleId,
				num3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1} -> {2}", new object[]
			{
				moduleId,
				text,
				text2
			});
		}
		return text2;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00019320 File Offset: 0x00017720
	private string CollonEnc(string word, string key, bool invert)
	{
		string text = string.Empty;
		for (int i = 0; i < word.Length; i++)
		{
			int num = key.IndexOf(word[i]);
			List<int> list = new List<int>
			{
				0,
				1,
				2,
				3,
				4
			};
			List<int> list2 = new List<int>
			{
				0,
				1,
				2,
				3,
				4
			};
			list.Remove(num % 5);
			list2.Remove(num / 5);
			int index = num / 5 * 5 + list[UnityEngine.Random.Range(0, 4)];
			int index2 = num % 5 + list2[UnityEngine.Random.Range(0, 4)] * 5;
			text = string.Concat(new object[]
			{
				text,
				string.Empty,
				key[index],
				string.Empty,
				key[index2]
			});
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV ORANGE] {1} -> {2}{3}", new object[]
				{
					moduleId,
					word[i],
					text[i * 2],
					text[i * 2 + 1]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [ORANGE] {1} -> {2}{3}", new object[]
				{
					moduleId,
					word[i],
					text[i * 2],
					text[i * 2 + 1]
				});
			}
		}
		return text;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000194EC File Offset: 0x000178EC
	private string redcipher(string word, bool invert)
	{
		fontsizes[page][0] = 35;
		fontsizes[page][1] = 35;
		fontsizes[page][2] = 35;
		fontsizes[page - 1][0] = 40;
		fontsizes[page - 1][1] = 40;
		fontsizes[page - 1][2] = 40;
		string text = string.Empty;
		bool[] array = new bool[6];
		for (int i = 0; i < 6; i++)
		{
			if (word[i] == 'J')
			{
				text += "ABCDEFGHIKLMNOPQRSTUVWXYZ"[UnityEngine.Random.Range(0, 25)];
				array[i] = true;
			}
			else
			{
				text = text + string.Empty + word[i];
			}
		}
		string[] array2 = new string[3];
		for (int j = 0; j < 3; j++)
		{
			int index = UnityEngine.Random.Range(0, wordList.Count);
			array2[j] = wordList[index][UnityEngine.Random.Range(0, wordList[index].Count)].ToUpperInvariant();
			wordList[index].Remove(array2[j].ToUpperInvariant());
		}
		pages[page][0] = array2[0].ToUpperInvariant();
		pages[page][1] = array2[1].ToUpperInvariant();
		pages[page][2] = array2[2].ToUpperInvariant();
		string text2 = string.Empty;
		string serialNumber = Bomb.GetSerialNumber();
		for (int k = 0; k < 6; k++)
		{
			switch (serialNumber[k])
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
					text2 = text2 + string.Empty + serialNumber[k];
					break;
			}
		}
		string key = getKey(array2[0].Replace('J', 'I'), "ABCDEFGHIKLMNOPQRSTUVWXYZ", (text2[0] - '0') % '\u0002' == '\u0001');
		string key2 = getKey(array2[1].Replace('J', 'I'), "ABCDEFGHIKLMNOPQRSTUVWXYZ", (text2[1] - '0') % '\u0002' == '\0');
		string key3 = getKey(array2[2].Replace('J', 'I'), "ABCDEFGHIKLMNOPQRSTUVWXYZ", Bomb.GetSerialNumberNumbers().Last<int>() % 2 == 1);
		if (invert)
		{
			chosentextcolors[page] = Color.black;
			chosenscreencolors[page] = screencolors[1];
			chosentextcolors[page - 1] = Color.black;
			chosenscreencolors[page - 1] = screencolors[1];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Inverted Red Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Playfair Key: {1}", new object[]
			{
				moduleId,
				key
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Begin Playfair Encryption", new object[]
			{
				moduleId
			});
			text = PlayfairEnc(text, key, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] CM Bifid Key 1: {1}", new object[]
			{
				moduleId,
				key
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] CM Bifid Key 2: {1}", new object[]
			{
				moduleId,
				key2
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Begin CM Bifid Encryption", new object[]
			{
				moduleId
			});
			text = CMBifidEnc(text, key, key2, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Trisquare Key 1: {1}", new object[]
			{
				moduleId,
				key
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Trisquare Key 2: {1}", new object[]
			{
				moduleId,
				key2
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Trisquare Key 3: {1}", new object[]
			{
				moduleId,
				key3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Begin Trisquare Encryption", new object[]
			{
				moduleId
			});
			text = TrisquareEnc(text, key, key2, key3, invert);
		}
		else
		{
			chosentextcolors[page] = Color.white;
			chosenscreencolors[page] = screencolors[0];
			chosentextcolors[page - 1] = Color.white;
			chosenscreencolors[page - 1] = screencolors[0];
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] Begin Red Cipher", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] ----------------------------------------", new object[]
			{
				moduleId
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Trisquare Key 1: {1}", new object[]
			{
				moduleId,
				key
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Trisquare Key 2: {1}", new object[]
			{
				moduleId,
				key2
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Trisquare Key 3: {1}", new object[]
			{
				moduleId,
				key3
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Begin Trisquare Encryption", new object[]
			{
				moduleId
			});
			text = TrisquareEnc(text, key, key2, key3, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] CM Bifid Key 1: {1}", new object[]
			{
				moduleId,
				key
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] CM Bifid Key 2: {1}", new object[]
			{
				moduleId,
				key2
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Begin CM Bifid Encryption", new object[]
			{
				moduleId
			});
			text = CMBifidEnc(text, key, key2, invert);
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Playfair Key: {1}", new object[]
			{
				moduleId,
				key
			});
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Begin Playfair Encryption", new object[]
			{
				moduleId
			});
			text = PlayfairEnc(text, key, invert);
		}
		string text3 = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
		for (int l = 0; l < 6; l++)
		{
			if (array[l])
			{
				pages[page - 1][1] = pages[page - 1][1] + string.Empty + text[l];
				text = text.Substring(0, l) + "J" + text.Substring(l + 1);
			}
			else
			{
				pages[page - 1][1] = pages[page - 1][1] + string.Empty + text3[UnityEngine.Random.Range(0, text3.Length)];
			}
		}
		return text;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00019C8C File Offset: 0x0001808C
	private string CMBifidEnc(string word, string kw1, string kw2, bool invert)
	{
		string text = string.Empty;
		string text2 = word.ToUpperInvariant();
		if (invert)
		{
			string text3 = string.Empty;
			for (int i = 0; i < 6; i++)
			{
				int num = kw2.IndexOf(word[i]);
				text3 = text3 + string.Empty + num / 5;
				text3 = text3 + string.Empty + num % 5;
			}
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] Rows|Columns: {1}", new object[]
			{
				moduleId,
				text3
			});
			for (int j = 0; j < 6; j++)
			{
				text = text + string.Empty + kw1[(int)((text3[j] - '0') * '\u0005' + (text3[j + 6] - '0'))];
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] {1} -> {2} -> {3}", new object[]
				{
					moduleId,
					text2[j],
					text3[j] + string.Empty + text3[j + 6],
					text[j]
				});
			}
		}
		else
		{
			string text4 = string.Empty;
			string text5 = string.Empty;
			for (int k = 0; k < 6; k++)
			{
				int num2 = kw1.IndexOf(word[k]);
				text4 = text4 + string.Empty + num2 / 5;
				text5 = text5 + string.Empty + num2 % 5;
			}
			string text6 = text4 + string.Empty + text5;
			Debug.LogFormat("[Ultimate Cipher #{0}] [RED] Rows|Columns: {1}", new object[]
			{
				moduleId,
				text4 + "|" + text5
			});
			for (int l = 0; l < 6; l++)
			{
				text = text + string.Empty + kw2[(int)((text6[l * 2] - '0') * '\u0005' + (text6[l * 2 + 1] - '0'))];
				Debug.LogFormat("[Ultimate Cipher #{0}] [RED] {1} -> {2} -> {3}", new object[]
				{
					moduleId,
					text2[l],
					text6[l * 2] + string.Empty + text6[l * 2 + 1],
					text[l]
				});
			}
		}
		return text;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00019F34 File Offset: 0x00018334
	private string TrisquareEnc(string word, string kw1, string kw2, string kw3, bool invert)
	{
		string text = string.Empty;
		for (int i = 0; i < 6; i++)
		{
			if (invert)
			{
				int num = kw3.IndexOf(word[i]);
				i++;
				int num2 = kw3.IndexOf(word[i]);
				text = string.Concat(new object[]
				{
					text,
					string.Empty,
					kw1[num / 5 * 5 + num2 % 5],
					string.Empty,
					kw2[num % 5 + num2 / 5 * 5]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] {1} -> {2}", new object[]
				{
					moduleId,
					word[i - 1] + string.Empty + word[i],
					text[i - 1] + string.Empty + text[i]
				});
			}
			else
			{
				int num3 = kw1.IndexOf(word[i]);
				int num4 = kw2.IndexOf(word[i + 1]);
				i++;
				text = string.Concat(new object[]
				{
					text,
					string.Empty,
					kw3[num3 / 5 * 5 + num4 % 5],
					string.Empty,
					kw3[num3 % 5 + num4 / 5 * 5]
				});
				Debug.LogFormat("[Ultimate Cipher #{0}] [RED] {1} -> {2}", new object[]
				{
					moduleId,
					word[i - 1] + string.Empty + word[i],
					text[i - 1] + string.Empty + text[i]
				});
			}
		}
		return text;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x0001A128 File Offset: 0x00018528
	private string PlayfairEnc(string word, string key, bool invert)
	{
		string text = string.Empty;
		int num = 0;
		int num2 = 0;
		char[][] array = new char[][]
		{
			new char[5],
			new char[5],
			new char[5],
			new char[5],
			new char[5]
		};
		for (int i = 0; i < key.Length; i++)
		{
			array[num2][num] = key[i];
			num++;
			if (num == 5)
			{
				num = 0;
				num2++;
			}
		}
		for (int j = 0; j < word.Length; j++)
		{
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			char c = word[j];
			j++;
			char c2 = word[j];
			for (int k = 0; k < 5; k++)
			{
				for (int l = 0; l < 5; l++)
				{
					if (c == array[k][l])
					{
						num3 = l;
						num4 = k;
					}
					if (c2 == array[k][l])
					{
						num5 = l;
						num6 = k;
					}
				}
			}
			if (num4 == num6 && num3 == num5)
			{
				num3 = num5;
				num4 = num6;
			}
			else if (num4 == num6)
			{
				if (invert)
				{
					num3 = correction(num3 - 1, 5);
					num5 = correction(num5 - 1, 5);
				}
				else
				{
					num3 = correction(num3 + 1, 5);
					num5 = correction(num5 + 1, 5);
				}
			}
			else if (num3 == num5)
			{
				if (invert)
				{
					num4 = correction(num4 - 1, 5);
					num6 = correction(num6 - 1, 5);
				}
				else
				{
					num4 = correction(num4 + 1, 5);
					num6 = correction(num6 + 1, 5);
				}
			}
			else
			{
				int num7 = num3;
				num3 = num5;
				num5 = num7;
			}
			text = string.Concat(new object[]
			{
				text,
				string.Empty,
				array[num4][num3],
				string.Empty,
				array[num6][num5]
			});
			if (invert)
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV RED] {1} -> {2}", new object[]
				{
					moduleId,
					word[j - 1] + string.Empty + word[j],
					text[j - 1] + string.Empty + text[j]
				});
			}
			else
			{
				Debug.LogFormat("[Ultimate Cipher #{0}] [RED] {1} -> {2}", new object[]
				{
					moduleId,
					word[j - 1] + string.Empty + word[j],
					text[j - 1] + string.Empty + text[j]
				});
			}
		}
		return text;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x0001A434 File Offset: 0x00018834
	private string getKey(string k, string alpha, bool start)
	{
		for (int i = 0; i < k.Length; i++)
		{
			for (int j = i + 1; j < k.Length; j++)
			{
				if (k[i] == k[j])
				{
					k = k.Substring(0, j) + string.Empty + k.Substring(j + 1);
					j--;
				}
			}
			alpha = alpha.Replace(k[i].ToString(), string.Empty);
		}
		if (start)
		{
			return k + string.Empty + alpha;
		}
		return alpha + string.Empty + k;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x0001A4E6 File Offset: 0x000188E6
	private int correction(int p, int max)
	{
		while (p < 0)
		{
			p += max;
		}
		while (p >= max)
		{
			p -= max;
		}
		return p;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0001A50C File Offset: 0x0001890C
	private void left(KMSelectable arrow)
	{
		if (!moduleSolved)
		{
			Audio.PlaySoundAtTransform(sounds[0].name, base.transform);
			submitScreen = false;
			arrow.AddInteractionPunch(1f);
			page--;
			page = correction(page, numpages);
			getScreens();
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0001A580 File Offset: 0x00018980
	private void right(KMSelectable arrow)
	{
		if (!moduleSolved)
		{
			Audio.PlaySoundAtTransform(sounds[0].name, base.transform);
			submitScreen = false;
			arrow.AddInteractionPunch(1f);
			page++;
			page = correction(page, numpages);
			getScreens();
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0001A5F4 File Offset: 0x000189F4
	private void getScreens()
	{
		submitText.text = page + 1 + string.Empty;
		if (pinkuc)
		{
			screenTexts[0].text = pinkpages[page][0];
			screenTexts[1].text = pinkpages[page][1];
			screenTexts[2].text = pinkpages[page][2];
			screenTexts[0].fontSize = pinkfontsizes[page][0];
			screenTexts[1].fontSize = pinkfontsizes[page][1];
			screenTexts[2].fontSize = pinkfontsizes[page][2];
			background.material = pinkchosenbackgroundcolors[page];
			arrowText[0].text = pinkarrowLetters[page][0];
			arrowText[1].text = pinkarrowLetters[page][1];
		}
		else if (cyanuc)
		{
			screenTexts[0].text = cyanpages[page][0];
			screenTexts[1].text = cyanpages[page][1];
			screenTexts[2].text = cyanpages[page][2];
			screenTexts[0].fontSize = cyanfontsizes[page][0];
			screenTexts[1].fontSize = cyanfontsizes[page][1];
			screenTexts[2].fontSize = cyanfontsizes[page][2];
			background.material = cyanchosenbackgroundcolors[page];
			arrowText[0].text = cyanarrowLetters[page][0];
			arrowText[1].text = cyanarrowLetters[page][1];
		}
		else if (trueuc)
		{
			screenTexts[0].text = truepages[page][0];
			screenTexts[1].text = truepages[page][1];
			screenTexts[2].text = truepages[page][2];
			screenTexts[0].fontSize = truefontsizes[page][0];
			screenTexts[1].fontSize = truefontsizes[page][1];
			screenTexts[2].fontSize = truefontsizes[page][2];
			screenTexts[0].color = truechosentextcolors[page];
			screenTexts[1].color = truechosentextcolors[page];
			screenTexts[2].color = truechosentextcolors[page];
			screens[0].material = truechosenscreencolors[page];
			screens[1].material = truechosenscreencolors[page];
			screens[2].material = truechosenscreencolors[page];
			background.material = truechosenbackgroundcolors[page];
			arrowText[0].text = truearrowLetters[page][0];
			arrowText[1].text = truearrowLetters[page][1];
		}
		else
		{
			screenTexts[0].text = ultpages[page][0];
			screenTexts[1].text = ultpages[page][1];
			screenTexts[2].text = ultpages[page][2];
			screenTexts[0].fontSize = ultfontsizes[page][0];
			screenTexts[1].fontSize = ultfontsizes[page][1];
			screenTexts[2].fontSize = ultfontsizes[page][2];
			screenTexts[0].color = ultchosentextcolors[page];
			screenTexts[1].color = ultchosentextcolors[page];
			screenTexts[2].color = ultchosentextcolors[page];
			screens[0].material = ultchosenscreencolors[page];
			screens[1].material = ultchosenscreencolors[page];
			screens[2].material = ultchosenscreencolors[page];
			background.material = ultchosenbackgroundcolors[page];
			arrowText[0].text = ultarrowLetters[page][0];
			arrowText[1].text = ultarrowLetters[page][1];
		}
		if (page == 0)
		{
			screenTexts[0].font = fonts[1];
			screentextmat[0].material = fontmat[1];
		}
		else
		{
			screenTexts[0].font = fonts[0];
			screentextmat[0].material = fontmat[0];
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0001AC08 File Offset: 0x00019008
	private void submitWord(KMSelectable submitButton)
	{
		if (!moduleSolved)
		{
			submitButton.AddInteractionPunch(1f);
			if (screenTexts[2].text.Equals("PINKUC"))
			{
				screenTexts[0].color = Color.white;
				screenTexts[1].color = Color.white;
				screenTexts[2].color = Color.white;
				screens[0].material = screencolors[0];
				screens[1].material = screencolors[0];
				screens[2].material = screencolors[0];
				background.material = backgroundcolors[12];
				screenTexts[2].text = string.Empty;
				numpages = numPCpages;
				pinkuc = true;
				cyanuc = false;
				trueuc = false;
				pinkcipher();
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
				screenTexts[2].text = string.Empty;
				numpages = numPCpages;
				cyanuc = true;
				pinkuc = false;
				trueuc = false;
				cyancipher();
			}
			else if (screenTexts[2].text.Equals("TRUEUC"))
			{
				screenTexts[0].color = Color.white;
				screenTexts[1].color = Color.white;
				screenTexts[2].color = Color.white;
				screens[0].material = screencolors[0];
				screens[1].material = screencolors[0];
				screens[2].material = screencolors[0];
				screenTexts[2].text = string.Empty;
				numpages = numTRUpages;
				cyanuc = false;
				pinkuc = false;
				trueuc = true;
				truecipher();
			}
			else if (screenTexts[2].text.Equals("CANCEL"))
			{
				answer = ultanswer.ToUpperInvariant();
				TPPoints = UcipherPoints;
				pinkuc = false;
				cyanuc = false;
				trueuc = false;
				numpages = 7;
				page = 0;
				getScreens();
			}
			else if (screenTexts[2].text.Equals("MUSICA"))
			{
				page = 0;
				getScreens();
				if (!(playing))
				{
					Audio.PlaySoundAtTransform(sounds[5].name, base.transform);
					StartCoroutine(songTime(818f));
				}
			}
			else if (screenTexts[2].text.Equals("IKTPQN"))
			{
				submitText.text = "<3";
				screenTexts[0].font = fonts[0];
				screentextmat[0].material = fontmat[0];
				screenTexts[0].color = Color.white;
				screenTexts[1].color = Color.white;
				screenTexts[2].color = Color.white;
				screens[0].material = screencolors[0];
				screens[1].material = screencolors[0];
				screens[2].material = screencolors[0];
				screenTexts[0].fontSize = 35;
				screenTexts[1].fontSize = 40;
				screenTexts[2].fontSize = 25;
				screenTexts[0].text = "A-II-I-VI";
				screenTexts[1].text = "PTG";
				screenTexts[2].text = "SE-AN";
			}
			else if (screenTexts[2].text.Equals("PRISSY"))
			{
				submitText.text = "<3";
				screenTexts[0].font = fonts[0];
				screentextmat[0].material = fontmat[0];
				screenTexts[0].color = Color.white;
				screenTexts[1].color = Color.white;
				screenTexts[2].color = Color.white;
				screens[0].material = screencolors[0];
				screens[1].material = screencolors[0];
				screens[2].material = screencolors[0];
				screenTexts[0].fontSize = 40;
				screenTexts[1].fontSize = 40;
				screenTexts[2].fontSize = 40;
				screenTexts[0].text = "THANK";
				screenTexts[1].text = "YOU FOR";
				screenTexts[2].text = "PLAYING";
				if(!(playing))
				{
					Audio.PlaySoundAtTransform(sounds[4].name, base.transform);
					StartCoroutine(songTime(307f));
				}
			}
			else if (screenTexts[2].text.Equals(answer))
			{
				if (pinkuc)
				{
					background.material = backgroundcolors[11];
					screenTexts[0].text = "TRU";
					screenTexts[1].text = string.Empty;
					screenTexts[2].text = string.Empty;
				}
				else if (cyanuc)
				{
					background.material = backgroundcolors[12];
					screenTexts[0].text = "EUC";
					screenTexts[1].text = string.Empty;
					screenTexts[2].text = string.Empty;
				}
				else if (trueuc)
				{
					background.material = backgroundcolors[0];
					screenTexts[0].text = "IKTPQN";
					screenTexts[1].text = string.Empty;
					screenTexts[2].text = string.Empty;
				}
				else
				{
					background.material = backgroundcolors[0];
					screenTexts[0].text = "PINKUC";
					screenTexts[1].text = "CYANUC";
					screenTexts[2].text = string.Empty;
				}
				screens[0].material = screencolors[0];
				screens[1].material = screencolors[0];
				screens[2].material = screencolors[0];
				screenTexts[0].color = Color.white;
				screenTexts[0].color = Color.white;
				screenTexts[0].color = Color.white;
				screenTexts[0].font = fonts[0];
				screentextmat[0].material = fontmat[0];
				Audio.PlaySoundAtTransform(sounds[2].name, base.transform);
				module.HandlePass();
				moduleSolved = true;
			}
			else
			{
				Audio.PlaySoundAtTransform(sounds[3].name, base.transform);
				module.HandleStrike();
				page = 0;
				getScreens();
			}
			submitScreen = false;
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0001B470 File Offset: 0x00019870
	private void letterPress(KMSelectable pressed)
	{
		if (!moduleSolved)
		{
			screenTexts[0].fontSize = 40;
			screenTexts[1].fontSize = 40;
			screenTexts[2].fontSize = 40;
			pressed.AddInteractionPunch(1f);
			Audio.PlaySoundAtTransform(sounds[1].name, base.transform);
			if (submitScreen)
			{
				if (screenTexts[2].text.Length < 6)
				{
					screenTexts[2].text = screenTexts[2].text + string.Empty + pressed.GetComponentInChildren<TextMesh>().text;
				}
			}
			else
			{
				submitText.text = "SUB";
				screenTexts[0].text = string.Empty;
				screenTexts[1].text = string.Empty;
				screenTexts[2].text = pressed.GetComponentInChildren<TextMesh>().text;
				submitScreen = true;
			}
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0001B588 File Offset: 0x00019988
	private IEnumerator loop()
	{
		for (int aa = 0; aa < 8; aa++)
		{
			switch (aa)
			{
				case 1:
					screenTexts[0].text = "WELCOME";
					break;
				case 2:
					screenTexts[1].text = "TO";
					break;
				case 3:
					screenTexts[2].text = "THE";
					break;
				case 4:
					screenTexts[0].text = string.Empty;
					screenTexts[1].text = string.Empty;
					screenTexts[2].text = string.Empty;
					break;
				case 5:
					screenTexts[0].text = "TRUE";
					break;
				case 6:
					screenTexts[1].text = "ULTIMATE";
					break;
				case 7:
					screenTexts[2].text = "CIPHER";
					break;
			}
			for (int bb = 1; bb < 11; bb++)
			{
				background.material = backgroundcolors[bb];
				yield return new WaitForSeconds(0.12f);
			}
		}
		answer = trueanswer.ToUpperInvariant();
		page = 0;
		getScreens();
		yield break;
	}
	private IEnumerator songTime(float time)
	{
		playing = true;
		yield return new WaitForSeconds(time);
		playing = false;
	}
	// Token: 0x06000059 RID: 89 RVA: 0x0001B5A4 File Offset: 0x000199A4
	private IEnumerator ProcessTwitchCommand(string command)
	{
		if (command.EqualsIgnoreCase("right") || command.EqualsIgnoreCase("r"))
		{
			yield return null;
			rightArrow.OnInteract.Invoke();
			yield return new WaitForSeconds(0.1f);
		}
		if (command.EqualsIgnoreCase("left") || command.EqualsIgnoreCase("l"))
		{
			yield return null;
			leftArrow.OnInteract.Invoke();
			yield return new WaitForSeconds(0.1f);
		}
		string[] split = command.ToUpperInvariant().Split(new string[]
		{
			" "
		}, StringSplitOptions.RemoveEmptyEntries);
		if (split.Length != 2 || !split[0].Equals("SUBMIT") || split[1].Length != 6)
		{
			yield break;
		}
		int[] buttons = split[1].Select(new Func<char, int>(getPositionFromChar)).ToArray<int>();
		if (buttons.Any((int x) => x < 0))
		{
			yield break;
		}
		yield return null;
		yield return new WaitForSeconds(0.1f);
		foreach (char let in split[1])
		{
			keyboard[getPositionFromChar(let)].OnInteract.Invoke();
			yield return new WaitForSeconds(0.1f);
		}
		if (screenTexts[2].text.Equals(answer) && !zenMode)
		{
			yield return "awardpointsonsolve " + TPPoints;
		}
		yield return new WaitForSeconds(0.1f);
		submit.OnInteract.Invoke();
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0001B5C8 File Offset: 0x000199C8
	private IEnumerator TwitchHandleForcedSolve()
	{
		if (submitScreen && !answer.StartsWith(screenTexts[2].text))
		{
			KMSelectable[] arrows = new KMSelectable[]
			{
				leftArrow,
				rightArrow
			};
			arrows[UnityEngine.Random.Range(0, 2)].OnInteract.Invoke();
			yield return new WaitForSeconds(0.1f);
		}
		int start = (!submitScreen) ? 0 : screenTexts[2].text.Length;
		for (int i = start; i < 6; i++)
		{
			keyboard[getPositionFromChar(answer[i])].OnInteract.Invoke();
			yield return new WaitForSeconds(0.1f);
		}
		submit.OnInteract.Invoke();
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0001B5E3 File Offset: 0x000199E3
	private int getPositionFromChar(char c)
	{
		return "QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(c);
	}

	

	// Token: 0x0400006B RID: 107
	private string TwitchHelpMessage = "Move to other screens using !{0} right|left|r|l|. Submit the decrypted word with !{0} submit qwertyuiopasdfghjklzxcvbnm";
}
