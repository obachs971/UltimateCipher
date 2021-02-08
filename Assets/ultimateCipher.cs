using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KModkit;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class ultimateCipher : MonoBehaviour
{
	// Token: 0x06000021 RID: 33 RVA: 0x0000E024 File Offset: 0x0000C424
	private void Awake()
	{
		moduleId = ultimateCipher.moduleIdCounter++;
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
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000E114 File Offset: 0x0000C514
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
			pages = new string[21][];
			chosenbackgroundcolors = new Material[21];
			fontsizes = new int[21][];
			arrowLetters = new string[21][];
			for (int i = 0; i < 21; i++)
			{
				pages[i] = new string[3];
				fontsizes[i] = new int[3];
			}
			chosenscreencolors = new Material[21];
			chosentextcolors = new Color[21];
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
				10
			};
			chosenbackgroundcolors[0] = backgroundcolors[11];
			chosentextcolors[0] = Color.white;
			chosenscreencolors[0] = screencolors[0];
			page = 20;
			string text = pinkanswer.ToUpperInvariant();
			for (int j = 9; j >= 0; j--)
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
				}
				page -= 2;
			}
			arrowLetters[0] = new string[2];
			arrowLetters[0][0] = "<";
			arrowLetters[0][1] = ">";
			pages[0][0] = text.ToUpperInvariant();
			pinkfontsizes = new int[21][];
			pinkpages = new string[21][];
			pinkchosenbackgroundcolors = new Material[21];
			pinkchosenscreencolors = new Material[21];
			pinkchosentextcolors = new Color[21];
			pinkarrowLetters = new string[21][];
			for (int k = 0; k < 21; k++)
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
		TPPoints = 140;
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
			pages = new string[21][];
			chosenbackgroundcolors = new Material[21];
			fontsizes = new int[21][];
			arrowLetters = new string[21][];
			for (int i = 0; i < 21; i++)
			{
				pages[i] = new string[3];
				fontsizes[i] = new int[3];
			}
			chosenscreencolors = new Material[21];
			chosentextcolors = new Color[21];
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
				10
			};
			chosenbackgroundcolors[0] = backgroundcolors[12];
			chosentextcolors[0] = Color.black;
			chosenscreencolors[0] = screencolors[1];
			page = 20;
			string text = cyananswer.ToUpperInvariant();
			for (int j = 9; j >= 0; j--)
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
				}
				page -= 2;
			}
			arrowLetters[0] = new string[2];
			arrowLetters[0][0] = "<";
			arrowLetters[0][1] = ">";
			pages[0][0] = text.ToUpperInvariant();
			cyanfontsizes = new int[21][];
			cyanpages = new string[21][];
			cyanchosenbackgroundcolors = new Material[21];
			cyanchosenscreencolors = new Material[21];
			cyanchosentextcolors = new Color[21];
			cyanarrowLetters = new string[21][];
			for (int k = 0; k < 21; k++)
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
		TPPoints = 140;
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
			pages = new string[41][];
			chosenbackgroundcolors = new Material[41];
			fontsizes = new int[41][];
			arrowLetters = new string[41][];
			for (int i = 0; i < 41; i++)
			{
				pages[i] = new string[3];
				fontsizes[i] = new int[3];
			}
			chosenscreencolors = new Material[41];
			chosentextcolors = new Color[41];
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
				-1,
				-2,
				-3,
				-4,
				-5,
				-6,
				-7,
				-8,
				-9,
				-10
			};
			chosenbackgroundcolors[0] = backgroundcolors[0];
			chosentextcolors[0] = Color.white;
			chosenscreencolors[0] = screencolors[0];
			page = 40;
			string text = trueanswer.ToUpperInvariant();
			for (int j = 19; j >= 0; j--)
			{
				int num = list[UnityEngine.Random.Range(0, list.Count)];
				list.Remove(num);
				bool flag = num < 0;
				if (flag)
				{
					num *= -1;
				}
				chosenbackgroundcolors[j * 2 + 1] = backgroundcolors[correction(num, 11)];
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
				}
				page -= 2;
			}
			arrowLetters[0] = new string[2];
			arrowLetters[0][0] = "<";
			arrowLetters[0][1] = ">";
			pages[0][0] = text.ToUpperInvariant();
			truefontsizes = new int[41][];
			truepages = new string[41][];
			truechosenbackgroundcolors = new Material[41];
			truechosenscreencolors = new Material[41];
			truechosentextcolors = new Color[41];
			truearrowLetters = new string[41][];
			for (int k = 0; k < 41; k++)
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
		TPPoints = 300;
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
			4, 4, 4
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
			}
			page -= 2;
		}
		arrowLetters[0] = new string[2];
		arrowLetters[0][0] = "<";
		arrowLetters[0][1] = ">";
		return text;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000FAC4 File Offset: 0x0000DEC4
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
			Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Begin Railfence Transposition", new object[]
			{
				moduleId
			});
			text = RailfenceTrans(text.ToUpperInvariant(), invert);
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
		Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Begin Railfence Transposition", new object[]
		{
			moduleId
		});
		text2 = RailfenceTrans(text2.ToUpperInvariant(), invert);
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

	// Token: 0x06000029 RID: 41 RVA: 0x00010694 File Offset: 0x0000EA94
	private string RailfenceTrans(string word, bool invert)
	{
		string[] array = new string["0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumber().ToString()[1]) % 4 + 2];
		int num = 1;
		int num2 = 0;
		if (invert)
		{
			array[0] = "*";
			for (int i = 1; i < 6; i++)
			{
				num2 += num;
				array[num2] += "*";
				if (num2 == array.Length - 1)
				{
					num = -1;
				}
				else if (num2 == 0)
				{
					num = 1;
				}
			}
			num2 = 0;
			for (int j = 0; j < array.Length; j++)
			{
				int length = array[j].Length;
				array[j] = word.Substring(num2, array[j].Length);
				num2 += array[j].Length;
				Debug.LogFormat("[Ultimate Cipher #{0}] [INV BLACK] Railfence Row #{1}: {2}", new object[]
				{
					moduleId,
					j + 1,
					array[j]
				});
			}
			string text = word[0].ToString();
			if (array.Length == 2)
			{
				text = string.Concat(new object[]
				{
					array[0][0],
					string.Empty,
					array[1][0],
					string.Empty,
					array[0][1],
					string.Empty,
					array[1][1],
					string.Empty,
					array[0][2],
					string.Empty,
					array[1][2]
				});
			}
			else
			{
				num2 = 0;
				int num3 = 0;
				num = 1;
				for (int k = 1; k < 6; k++)
				{
					num2 += num;
					text = text + string.Empty + array[num2][num3];
					if (num2 == array.Length - 1)
					{
						num = -1;
						num3++;
					}
					else if (num2 == 0)
					{
						num = 1;
						num3++;
					}
				}
			}
			return text;
		}
		array[0] = word[0].ToString();
		for (int l = 1; l < 6; l++)
		{
			num2 += num;
			array[num2] = array[num2] + string.Empty + word[l];
			if (num2 == array.Length - 1)
			{
				num = -1;
			}
			else if (num2 == 0)
			{
				num = 1;
			}
		}
		string text2 = string.Empty;
		for (int m = 0; m < array.Length; m++)
		{
			text2 = text2 + string.Empty + array[m].ToUpperInvariant();
			Debug.LogFormat("[Ultimate Cipher #{0}] [BLACK] Railfence Row #{1}: {2}", new object[]
			{
				moduleId,
				m + 1,
				array[m]
			});
		}
		return text2;
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
					text4.Replace(c2 + string.Empty, string.Empty);
				}
				c = text3[UnityEngine.Random.Range(0, text3.Length)];
				text3.Replace(c + string.Empty, string.Empty);
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
		pages[page][2] = text4;
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
			text10 = CollonEnc(text3.ToUpperInvariant(), text5, invert);
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
			text10 = CollonEnc(text3.ToUpperInvariant(), text5, invert);
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
		string key = getKey(array2[0].ToUpperInvariant(), "ABCDEFGHIKLMNOPQRSTUVWXYZ", (text2[0] - '0') % '\u0002' == '\u0001');
		string key2 = getKey(array2[1].ToUpperInvariant(), "ABCDEFGHIKLMNOPQRSTUVWXYZ", (text2[1] - '0') % '\u0002' == '\0');
		string key3 = getKey(array2[2].ToUpperInvariant(), "ABCDEFGHIKLMNOPQRSTUVWXYZ", Bomb.GetSerialNumberNumbers().Last<int>() % 2 == 1);
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
				background.material = backgroundcolors[11];
				screenTexts[2].text = string.Empty;
				numpages = 21;
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
				numpages = 21;
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
				numpages = 41;
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
				Audio.PlaySoundAtTransform(sounds[5].name, base.transform);
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
				Audio.PlaySoundAtTransform(sounds[4].name, base.transform);
			}
			else if (screenTexts[2].text.Equals(answer))
			{
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

	// Token: 0x04000025 RID: 37
	public TextMesh[] screenTexts;

	// Token: 0x04000026 RID: 38
	public KMBombInfo Bomb;

	// Token: 0x04000027 RID: 39
	public KMBombModule module;

	// Token: 0x04000028 RID: 40
	public AudioClip[] sounds;

	// Token: 0x04000029 RID: 41
	public KMAudio Audio;

	// Token: 0x0400002A RID: 42
	public TextMesh submitText;

	// Token: 0x0400002B RID: 43
	public TextMesh[] arrowText;

	// Token: 0x0400002C RID: 44
	private bool zenMode;

	// Token: 0x0400002D RID: 45
	private bool ZenModeActive;

	// Token: 0x0400002E RID: 46
	private List<List<string>> wordList = new List<List<string>>
	{
		new List<string>
		{
			"ALSO",
			"AREA",
			"ABLE",
			"AWAY",
			"ACID",
			"AGED",
			"ACRE",
			"ARCH",
			"AXIS",
			"ALLY",
			"ALAS",
			"AURA",
			"ATOM",
			"AXES",
			"ACHE",
			"AMEN",
			"ACNE",
			"AXLE",
			"AQUA",
			"BEEN",
			"BACK",
			"BOTH",
			"BEST",
			"BOOK",
			"BASE",
			"BODY",
			"BILL",
			"BLUE",
			"BALL",
			"BORN",
			"BABY",
			"BEAT",
			"BAND",
			"BEAR",
			"BELL",
			"BUSY",
			"BOND",
			"BUSH",
			"BOAT",
			"BOWL",
			"BIRD",
			"BLOW",
			"BONE",
			"BATH",
			"BOSS",
			"BELT",
			"BOMB",
			"BURN",
			"BASS",
			"BOLD",
			"BEEF",
			"BENT",
			"BEND",
			"BIKE",
			"BOOT",
			"BEAM",
			"BLEW",
			"BARN",
			"BUZZ",
			"BEAN",
			"BATS",
			"BUST",
			"BOLT",
			"BURY",
			"BOIL",
			"BAKE",
			"BUMP",
			"BAIT",
			"BULB",
			"BLUR",
			"BEAD",
			"COME",
			"CAME",
			"COST",
			"CALL",
			"CITY",
			"CASE",
			"CARE",
			"CASH",
			"CLUB",
			"CORE",
			"CODE",
			"CARD",
			"COLD",
			"COPY",
			"CELL",
			"CAST",
			"CHIP",
			"COOL",
			"CAMP",
			"COOK",
			"COAL",
			"CREW",
			"CHAT",
			"CROP",
			"COAT",
			"CALM",
			"CAKE",
			"CORN",
			"CLAY",
			"COIN",
			"CART",
			"CAPE",
			"CHEF",
			"CLUE",
			"CAGE",
			"CAVE",
			"CURB",
			"CLIP",
			"CROW",
			"CONE",
			"CUBE",
			"CORK",
			"CALF",
			"COIL",
			"CANE",
			"CUTE",
			"CHOP",
			"CRAB",
			"COMB",
			"CURL",
			"CLAW",
			"CLOG",
			"CLAP",
			"CRIB",
			"CLAM",
			"COZY",
			"DOWN",
			"DATA",
			"DAYS",
			"DATE",
			"DONE",
			"DEAL",
			"DOOR",
			"DEEP",
			"DARK",
			"DUTY",
			"DROP",
			"DRAW",
			"DREW",
			"DESK",
			"DISK",
			"DEAR",
			"DUST",
			"DIAL",
			"DENY",
			"DAWN",
			"DISH",
			"DRAG",
			"DECK",
			"DIRT",
			"DARE",
			"DEAF",
			"DUCK",
			"DOCK",
			"DRUM",
			"DEER",
			"DOME",
			"DASH",
			"DOLL",
			"DAMP",
			"DIVE",
			"DEED",
			"DENT",
			"DOVE",
			"DICE",
			"DUSK",
			"DART",
			"DINE",
			"DRIP",
			"DUET",
			"DUEL",
			"EVEN",
			"EACH",
			"EVER",
			"EASY",
			"ELSE",
			"EAST",
			"EDGE",
			"EASE",
			"EARN",
			"EXIT",
			"ECHO",
			"EXAM",
			"EDIT",
			"EPIC",
			"EYES",
			"EARS",
			"FROM",
			"FULL",
			"FORM",
			"FIND",
			"FACT",
			"FREE",
			"FACE",
			"FIRM",
			"FOOD",
			"FELT",
			"FEET",
			"FALL",
			"FAST",
			"FIRE",
			"FINE",
			"FILM",
			"FLOW",
			"FOOT",
			"FAIR",
			"FILE",
			"FELL",
			"FLAT",
			"FISH",
			"FARM",
			"FILL",
			"FAIL",
			"FEED",
			"FORT",
			"FATE",
			"FAME",
			"FLAG",
			"FLEW",
			"FOLK",
			"FOLD",
			"FLED",
			"FOAM",
			"FORK",
			"FAKE",
			"FLEX",
			"FLIP",
			"FOIL",
			"FADE",
			"FLEE",
			"FROG",
			"FUSE",
			"FONT",
			"FLAW",
			"GOOD",
			"GIVE",
			"GAME",
			"GAVE",
			"GOES",
			"GOAL",
			"GONE",
			"GROW",
			"GAIN",
			"GOLD",
			"GREW",
			"GOLF",
			"GIFT",
			"GATE",
			"GLAD",
			"GEAR",
			"GRAY",
			"GREY",
			"GRID",
			"GRIP",
			"GRAB",
			"GLOW",
			"GILL",
			"GLUE",
			"GRIN",
			"GOAT",
			"GRAM",
			"GASP",
			"GERM",
			"GLEE",
			"GONG",
			"GUST",
			"HAVE",
			"HIGH",
			"HERE",
			"HOME",
			"HELP",
			"HELD",
			"HALF",
			"HEAD",
			"HAND",
			"HARD",
			"HOPE",
			"HOLD",
			"HOUR",
			"HALL",
			"HEAR",
			"HUGE",
			"HILL",
			"HOST",
			"HAIR",
			"HEAT",
			"HOLE",
			"HIRE",
			"HUNT",
			"HERO",
			"HIDE",
			"HOOK",
			"HINT",
			"HORN",
			"HOOD",
			"HEAL",
			"HEEL",
			"HAWK",
			"HEIR",
			"HERB",
			"HERD",
			"HAIL",
			"HOSE",
			"HIKE",
			"HARE",
			"HUSH",
			"HOWL",
			"INTO",
			"IDEA",
			"ITEM",
			"IRON",
			"INCH",
			"INFO",
			"ICON",
			"IDLE",
			"IDOL",
			"ICED",
			"ITCH",
			"JUST",
			"JOIN",
			"JUMP",
			"JURY",
			"JAZZ",
			"JOKE",
			"JUNK",
			"JINX",
			"JARS",
			"JOBS",
			"KNOW",
			"KEEP",
			"KIND",
			"KNEW",
			"KING",
			"KEPT",
			"KICK",
			"KNEE",
			"KNIT",
			"KNOT",
			"KITE",
			"KNOB",
			"KIWI",
			"KELP",
			"LIKE",
			"LAST",
			"LONG",
			"LIFE",
			"LOOK",
			"LESS",
			"LINE",
			"LEFT",
			"LIVE",
			"LEAD",
			"LATE",
			"LOSS",
			"LIST",
			"LOST",
			"LAND",
			"LOVE",
			"LACK",
			"LINK",
			"LOSE",
			"LAKE",
			"LOAD",
			"LAID",
			"LANE",
			"LIFT",
			"LOCK",
			"LUCK",
			"LOUD",
			"LOOP",
			"LEAP",
			"LAWN",
			"LEAF",
			"LION",
			"LAMB",
			"LAMP",
			"LAZY",
			"LEAK",
			"LIME",
			"LURE",
			"LIED",
			"LAVA",
			"LIAR",
			"LOAF",
			"LAIR",
			"MORE",
			"MOST",
			"MADE",
			"MUCH",
			"MANY",
			"MAKE",
			"MUST",
			"MEET",
			"MOVE",
			"MEAN",
			"MAIN",
			"MIND",
			"MASS",
			"MAIL",
			"MISS",
			"MINE",
			"MILE",
			"MODE",
			"MILL",
			"MILK",
			"MEAL",
			"MEAT",
			"MENU",
			"MOOD",
			"MOON",
			"MILD",
			"MESS",
			"MYTH",
			"MASK",
			"MALL",
			"MICE",
			"MELT",
			"MOSS",
			"MIST",
			"MINT",
			"MOLD",
			"MONK",
			"MAZE",
			"MATH",
			"MOLE",
			"MUTE",
			"MIME",
			"MULE",
			"MOTH",
			"MOAT",
			"NEED",
			"NEXT",
			"NEWS",
			"NAME",
			"NOTE",
			"NEAR",
			"NONE",
			"NICE",
			"NECK",
			"NOSE",
			"NOON",
			"NEAT",
			"NEST",
			"NAIL",
			"NEON",
			"NOUN",
			"NUMB",
			"NEWT",
			"NOOK",
			"ONLY",
			"OVER",
			"OPEN",
			"ONCE",
			"ONTO",
			"OKAY",
			"OURS",
			"OVEN",
			"OVAL",
			"ODOR",
			"OATH",
			"OMIT",
			"OMEN",
			"OOZE",
			"OBOE",
			"ORCA",
			"OGRE",
			"OINK",
			"ORES",
			"PART",
			"PAST",
			"PLAN",
			"PLAY",
			"PAID",
			"PARK",
			"PAGE",
			"POOR",
			"PLUS",
			"POST",
			"PASS",
			"PATH",
			"PICK",
			"POOL",
			"PACE",
			"PORT",
			"PAIR",
			"PUSH",
			"PULL",
			"PEAK",
			"PURE",
			"PINK",
			"PACK",
			"PALM",
			"PIPE",
			"PLOT",
			"PLUG",
			"PUMP",
			"POET",
			"PALE",
			"POSE",
			"POEM",
			"POND",
			"POLE",
			"PILE",
			"POUR",
			"PLEA",
			"PORK",
			"PROP",
			"PIER",
			"PEEL",
			"PONY",
			"PINT",
			"PLUM",
			"PEEK",
			"POKE",
			"PEAR",
			"PAWN",
			"PLOW",
			"PERK",
			"QUIT",
			"QUIZ",
			"REAL",
			"RATE",
			"RISK",
			"ROLE",
			"ROOM",
			"ROAD",
			"READ",
			"REST",
			"ROSE",
			"RICH",
			"RISE",
			"RULE",
			"RACE",
			"ROCK",
			"RELY",
			"RING",
			"ROLL",
			"RARE",
			"RAIN",
			"RENT",
			"RIDE",
			"RAIL",
			"ROOF",
			"RANK",
			"RUSH",
			"ROOT",
			"REAR",
			"RICE",
			"RACK",
			"RAMP",
			"ROPE",
			"RODE",
			"RIPE",
			"RUIN",
			"RASH",
			"ROAR",
			"RUBY",
			"RUST",
			"RAFT",
			"ROAM",
			"ROBE",
			"RAKE",
			"SAID",
			"SOME",
			"SUCH",
			"SAME",
			"SITE",
			"SHOW",
			"SEEN",
			"SIDE",
			"SURE",
			"SIZE",
			"SALE",
			"SOLD",
			"SAFE",
			"SOON",
			"STOP",
			"STEP",
			"SELL",
			"STAY",
			"SENT",
			"SORT",
			"SEND",
			"SIGN",
			"STAR",
			"SAVE",
			"SHOP",
			"SELF",
			"SEEK",
			"SLOW",
			"SOFT",
			"SPOT",
			"SEAT",
			"SUIT",
			"SHIP",
			"SALT",
			"SONG",
			"SOIL",
			"SNOW",
			"SAND",
			"SEED",
			"SLIP",
			"SPIN",
			"SING",
			"SHED",
			"SEAL",
			"SOUP",
			"SOLO",
			"SLOT",
			"SCAN",
			"SINK",
			"SOAP",
			"SILK",
			"SHOE",
			"SLIM",
			"SWIM",
			"SWAP",
			"SNAP",
			"STIR",
			"SANG",
			"SPUN",
			"SAIL",
			"SKIP",
			"SLID",
			"SWAN",
			"SOUR",
			"SLAM",
			"SANK",
			"SODA",
			"SOFA",
			"SUNK",
			"SLAB",
			"SOAR",
			"THAT",
			"THIS",
			"THEY",
			"THEM",
			"THEN",
			"TIME",
			"THAN",
			"TAKE",
			"TEAM",
			"TOOK",
			"TERM",
			"TOLD",
			"TURN",
			"TYPE",
			"TRUE",
			"TELL",
			"TEST",
			"TALK",
			"TOWN",
			"TEXT",
			"TASK",
			"TOUR",
			"TRIP",
			"TEND",
			"THIN",
			"TREE",
			"TAPE",
			"TINY",
			"TONE",
			"TALL",
			"TOLL",
			"TANK",
			"TALE",
			"TWIN",
			"TUNE",
			"TUBE",
			"TAIL",
			"TIER",
			"TRAP",
			"TEAR",
			"TIDE",
			"TORN",
			"TAXI",
			"TRIM",
			"TRIO",
			"TRAY",
			"TENT",
			"TOSS",
			"TIRE",
			"TORE",
			"TILE",
			"TIDY",
			"TILT",
			"TUCK",
			"TUNA",
			"TAME",
			"THAW",
			"TOAD",
			"TOIL",
			"TACO",
			"TEAL",
			"THUD",
			"USED",
			"UPON",
			"UNIT",
			"USER",
			"URGE",
			"UNDO",
			"USES",
			"VERY",
			"VIEW",
			"VAST",
			"VARY",
			"VOID",
			"VINE",
			"VERB",
			"VEST",
			"VENT",
			"VIAL",
			"WITH",
			"WERE",
			"WILL",
			"WHAT",
			"WHEN",
			"WELL",
			"WORK",
			"WANT",
			"WEEK",
			"WENT",
			"WIDE",
			"WEST",
			"WIRE",
			"WORD",
			"WALL",
			"WISH",
			"WALK",
			"WAIT",
			"WOOD",
			"WIND",
			"WARM",
			"WILD",
			"WEAR",
			"WEAK",
			"WAVE",
			"WASH",
			"WING",
			"WISE",
			"WORE",
			"WORN",
			"WOLF",
			"WRAP",
			"WOOL",
			"WIPE",
			"WORM",
			"WARP",
			"WELD",
			"WAND",
			"WAVY",
			"WAXY",
			"XYLO",
			"YOUR",
			"YEAR",
			"YARD",
			"YARN",
			"YOGA",
			"YELL",
			"YAWN",
			"ZONE",
			"ZOOM"
		},
		new List<string>
		{
			"ABOUT",
			"AFTER",
			"AMONG",
			"AGAIN",
			"ABOVE",
			"ALONG",
			"AWARD",
			"ALLOW",
			"ALONE",
			"AHEAD",
			"APPLY",
			"AWARE",
			"AVOID",
			"AGENT",
			"ASSET",
			"AGREE",
			"ADULT",
			"APART",
			"AUDIO",
			"ASIDE",
			"ARRAY",
			"ALIVE",
			"ARGUE",
			"APPLE",
			"ACUTE",
			"ADMIT",
			"ARENA",
			"ACTOR",
			"ALERT",
			"ALBUM",
			"ALTER",
			"ANGLE",
			"ALARM",
			"ADAPT",
			"ANGEL",
			"ANKLE",
			"ALIEN",
			"ARROW",
			"ALLEY",
			"AWAKE",
			"AMEND",
			"ARMOR",
			"ALIGN",
			"ALTAR",
			"ALLOY",
			"AMBER",
			"ATTIC",
			"AGILE",
			"AROMA",
			"APRON",
			"ACORN",
			"ADORE",
			"AMUSE",
			"ABYSS",
			"BOARD",
			"BEGAN",
			"BRING",
			"BUILT",
			"BLACK",
			"BASIC",
			"BELOW",
			"BUILD",
			"BEGIN",
			"BREAK",
			"BROWN",
			"BEACH",
			"BRAND",
			"BLOCK",
			"BEGUN",
			"BRIEF",
			"BROKE",
			"BOUND",
			"BOOST",
			"BUYER",
			"BAKER",
			"BLIND",
			"BREAD",
			"BENCH",
			"BURST",
			"BONUS",
			"BRICK",
			"BLEND",
			"BRUSH",
			"BLANK",
			"BUNCH",
			"BRAVE",
			"BLOWN",
			"BLAST",
			"BATCH",
			"BRASS",
			"BACON",
			"BAKED",
			"BLOOM",
			"BERRY",
			"BEARD",
			"BRAKE",
			"BOXER",
			"BURNT",
			"BADGE",
			"BLAND",
			"BLISS",
			"BUNNY",
			"BULKY",
			"BLUFF",
			"BLINK",
			"COULD",
			"CHIEF",
			"CAUSE",
			"CLASS",
			"CLOSE",
			"CLEAR",
			"CHILD",
			"COVER",
			"CROSS",
			"CARRY",
			"CLAIM",
			"CHECK",
			"CIVIL",
			"CHAIN",
			"COAST",
			"CLEAN",
			"CHAIR",
			"CYCLE",
			"CABLE",
			"COUNT",
			"CATCH",
			"CROWD",
			"CROWN",
			"CLOCK",
			"CHART",
			"CHEAP",
			"CRASH",
			"CHASE",
			"CURVE",
			"CLICK",
			"CRAFT",
			"CLIMB",
			"CRAZY",
			"CLOUD",
			"CARGO",
			"COLOR",
			"COMIC",
			"CLOTH",
			"CHAOS",
			"CANAL",
			"CLIFF",
			"CEASE",
			"CHARM",
			"CREEK",
			"CABIN",
			"CRANE",
			"CLASH",
			"CORAL",
			"CHEER",
			"CANDY",
			"CHILL",
			"CREST",
			"CHALK",
			"COUCH",
			"CRUST",
			"CHESS",
			"CHUNK",
			"CRAWL",
			"DAILY",
			"DRIVE",
			"DEPTH",
			"DRAWN",
			"DOUBT",
			"DREAM",
			"DRINK",
			"DANCE",
			"DELAY",
			"DOZEN",
			"DROVE",
			"DRESS",
			"DEBUT",
			"DEALT",
			"DRILL",
			"DRIED",
			"DAIRY",
			"DENSE",
			"DRAIN",
			"DIARY",
			"DERBY",
			"DRIFT",
			"DIGIT",
			"DECAY",
			"DEBIT",
			"DRANK",
			"DUSTY",
			"DODGE",
			"DISCO",
			"DAISY",
			"DOUGH",
			"DWARF",
			"DIZZY",
			"DINER",
			"DONUT",
			"EVERY",
			"EARLY",
			"EVENT",
			"EXTRA",
			"ENJOY",
			"ENTER",
			"EQUAL",
			"ENTRY",
			"EARTH",
			"EXIST",
			"ERROR",
			"EMPTY",
			"EXACT",
			"EAGER",
			"EAGLE",
			"ESSAY",
			"ELDER",
			"ELBOW",
			"EATEN",
			"EQUIP",
			"ERASE",
			"EVADE",
			"FIRST",
			"FOUND",
			"FIELD",
			"FINAL",
			"FORCE",
			"FRONT",
			"FOCUS",
			"FLOOR",
			"FIXED",
			"FIBER",
			"FRESH",
			"FIFTH",
			"FRAME",
			"FORUM",
			"FALSE",
			"FAULT",
			"FRUIT",
			"FUNNY",
			"FLASH",
			"FLUID",
			"FLOOD",
			"FENCE",
			"FANCY",
			"FROST",
			"FLOAT",
			"FLIES",
			"FLAME",
			"FORGE",
			"FAINT",
			"FLOUR",
			"FEAST",
			"FAIRY",
			"FAVOR",
			"FLUSH",
			"FLAIR",
			"FLARE",
			"FUZZY",
			"FROZE",
			"FLUTE",
			"FOYER",
			"FUDGE",
			"FLASK",
			"GROUP",
			"GOING",
			"GREAT",
			"GIVEN",
			"GREEN",
			"GUIDE",
			"GRAND",
			"GLASS",
			"GROWN",
			"GRADE",
			"GIANT",
			"GUEST",
			"GUESS",
			"GRASS",
			"GRAIN",
			"GRASP",
			"GRAPH",
			"GLORY",
			"GAUGE",
			"GHOST",
			"GRILL",
			"GRAMS",
			"GREET",
			"GLOVE",
			"GOOSE",
			"GRAPE",
			"GLIDE",
			"GRAVY",
			"GEESE",
			"GENIE",
			"HOUSE",
			"HUMAN",
			"HEART",
			"HOTEL",
			"HAPPY",
			"HEAVY",
			"HORSE",
			"HABIT",
			"HEDGE",
			"HONEY",
			"HURRY",
			"HANDY",
			"HONOR",
			"HATCH",
			"HOBBY",
			"HAIRY",
			"HASTE",
			"HINGE",
			"HUSKY",
			"HUMID",
			"HOUND",
			"HUMOR",
			"HIPPO",
			"HYENA",
			"ISSUE",
			"IMAGE",
			"IDEAL",
			"INDEX",
			"INPUT",
			"INNER",
			"IMPLY",
			"IRONY",
			"IVORY",
			"ICING",
			"IDIOM",
			"INTRO",
			"ITCHY",
			"INGOT",
			"IGLOO",
			"ITEMS",
			"IDEAS",
			"JOINT",
			"JUDGE",
			"JUICE",
			"JEWEL",
			"JOLLY",
			"JELLY",
			"JUMBO",
			"JUICY",
			"JOKER",
			"JUMPS",
			"KNOCK",
			"KITTY",
			"KAYAK",
			"KNEEL",
			"KARAT",
			"KNEAD",
			"KOALA",
			"KABOB",
			"KAZOO",
			"LOCAL",
			"LARGE",
			"LEVEL",
			"LATER",
			"LIGHT",
			"LOWER",
			"LEAVE",
			"LEARN",
			"LIVES",
			"LEAST",
			"LINKS",
			"LIMIT",
			"LUNCH",
			"LAYER",
			"LABEL",
			"LOGIC",
			"LUCKY",
			"LAUGH",
			"LASER",
			"LOYAL",
			"LOBBY",
			"LIVER",
			"LODGE",
			"LEMON",
			"LEVER",
			"LITER",
			"LEAPT",
			"LYRIC",
			"LUNAR",
			"LOUSY",
			"LEDGE",
			"LOGIN",
			"LEAKY",
			"LOOPY",
			"MIGHT",
			"MONEY",
			"MAJOR",
			"MARCH",
			"MONTH",
			"MEDIA",
			"MODEL",
			"MUSIC",
			"MATCH",
			"MAYBE",
			"MEANT",
			"MIXED",
			"METAL",
			"MOTOR",
			"MINOR",
			"MOUTH",
			"MOVIE",
			"MAGIC",
			"MOUNT",
			"MOUSE",
			"MINUS",
			"MAKER",
			"MERIT",
			"MEDAL",
			"METER",
			"MERGE",
			"MIDST",
			"MARSH",
			"MANOR",
			"MUMMY",
			"MAPLE",
			"MOIST",
			"MERRY",
			"MOTTO",
			"MUDDY",
			"MESSY",
			"MIMIC",
			"MUTED",
			"MIXER",
			"MOVER",
			"MOTEL",
			"MURKY",
			"MAGMA",
			"MISTY",
			"MANGO",
			"MELON",
			"MOOSE",
			"MORPH",
			"MEDIC",
			"NEVER",
			"NEEDS",
			"NORTH",
			"NIGHT",
			"NOTED",
			"NOVEL",
			"NOISE",
			"NURSE",
			"NINTH",
			"NOBLE",
			"NERVE",
			"NOISY",
			"NEEDY",
			"NUDGE",
			"NIFTY",
			"NINJA",
			"NACHO",
			"NAILS",
			"NEIGH",
			"OTHER",
			"OFTEN",
			"ORDER",
			"OFFER",
			"OCCUR",
			"OCEAN",
			"OUTER",
			"OPERA",
			"OLIVE",
			"ORBIT",
			"OUNCE",
			"ONION",
			"OASIS",
			"OTTER",
			"PLACE",
			"POINT",
			"POWER",
			"PRESS",
			"PARTY",
			"PRICE",
			"PAPER",
			"PHONE",
			"PLANT",
			"PRIME",
			"PRIOR",
			"PIECE",
			"PHASE",
			"PROVE",
			"PEACE",
			"PROUD",
			"PRINT",
			"PANEL",
			"PHOTO",
			"POUND",
			"PILOT",
			"PLATE",
			"PRIZE",
			"PRIDE",
			"PLAIN",
			"PAINT",
			"PITCH",
			"PLANE",
			"PIANO",
			"PATCH",
			"PANIC",
			"PAUSE",
			"PEARL",
			"PLAZA",
			"PIZZA",
			"PINCH",
			"PASTE",
			"POLAR",
			"PATIO",
			"PILES",
			"PEACH",
			"PORCH",
			"PIXEL",
			"POKER",
			"PERIL",
			"PUPPY",
			"PEDAL",
			"PIVOT",
			"PRISM",
			"PLANK",
			"PANDA",
			"QUITE",
			"QUICK",
			"QUIET",
			"QUEEN",
			"QUOTE",
			"QUEST",
			"QUERY",
			"QUEUE",
			"QUILT",
			"QUIRK",
			"QUAIL",
			"QUILL",
			"QUART",
			"QUARK",
			"QUACK",
			"RIGHT",
			"RANGE",
			"ROUND",
			"REACH",
			"READY",
			"RADIO",
			"ROYAL",
			"RAPID",
			"RAISE",
			"RIVER",
			"ROUTE",
			"RATIO",
			"ROUGH",
			"RIVAL",
			"REPLY",
			"RALLY",
			"REACT",
			"ROCKY",
			"RIGID",
			"RELAX",
			"REALM",
			"RADAR",
			"RELAY",
			"RISKY",
			"RENEW",
			"RANCH",
			"ROBOT",
			"RUSTY",
			"ROAST",
			"RUMOR",
			"ROGUE",
			"RAINY",
			"RAMPS",
			"RINSE",
			"REUSE",
			"RAVEN",
			"RECAP",
			"RHYME",
			"RHINO",
			"RELIC",
			"ROOMY",
			"REMIX",
			"STILL",
			"STATE",
			"SINCE",
			"SMALL",
			"STAFF",
			"SHARE",
			"SOUTH",
			"SHORT",
			"STOCK",
			"STUDY",
			"SPACE",
			"STORY",
			"STAGE",
			"SPEED",
			"SOUND",
			"SHOWN",
			"SPENT",
			"SPEND",
			"SERVE",
			"SPEAK",
			"SCALE",
			"STYLE",
			"STAND",
			"SHALL",
			"STORE",
			"SOILD",
			"SHEET",
			"STOOD",
			"SHAPE",
			"SUITE",
			"SCENE",
			"STONE",
			"STUFF",
			"SHIFT",
			"SCORE",
			"SPLIT",
			"STEEL",
			"SCOPE",
			"SPOKE",
			"SPORT",
			"SLEEP",
			"SMART",
			"SIGHT",
			"SIXTH",
			"SKILL",
			"STICK",
			"SMILE",
			"SOLVE",
			"SHOCK",
			"SWEET",
			"SUPER",
			"SUGAR",
			"STORM",
			"STUCK",
			"SHELF",
			"SHELL",
			"SPARE",
			"SHIRT",
			"STEAM",
			"SLIDE",
			"SWING",
			"SHORE",
			"SWEPT",
			"SOLAR",
			"SPELL",
			"SHAKE",
			"SHEEP",
			"SWIFT",
			"STAMP",
			"SPRAY",
			"SAUCE",
			"STACK",
			"THERE",
			"THEIR",
			"THESE",
			"THOSE",
			"TODAY",
			"THINK",
			"THIRD",
			"TOTAL",
			"TRADE",
			"THING",
			"TABLE",
			"TRACK",
			"TRIED",
			"TWICE",
			"TRAIN",
			"TRULY",
			"TRUTH",
			"TREND",
			"TRICK",
			"TOUGH",
			"TOWER",
			"THROW",
			"TEACH",
			"TASTE",
			"THICK",
			"TOPIC",
			"TIRED",
			"THREW",
			"TRUCK",
			"TRACE",
			"TRAIL",
			"TENTH",
			"TWIST",
			"TIGER",
			"THUMB",
			"TENSE",
			"TOKEN",
			"TOAST",
			"TOWEL",
			"TORCH",
			"TRASH",
			"TASTY",
			"TRAIT",
			"TIMER",
			"THORN",
			"UNDER",
			"UNTIL",
			"URBAN",
			"UPPER",
			"USUAL",
			"USAGE",
			"UPSET",
			"UNITY",
			"ULTRA",
			"UNITE",
			"UNLIT",
			"UDDER",
			"UNZIP",
			"VISIT",
			"VALUE",
			"VOICE",
			"VIDEO",
			"VITAL",
			"VALID",
			"VAGUE",
			"VIVID",
			"VOCAL",
			"VALVE",
			"VAPOR",
			"VAULT",
			"VIGOR",
			"VOWEL",
			"WHICH",
			"WOULD",
			"WHERE",
			"WORLD",
			"WHILE",
			"WATER",
			"WHOLE",
			"WHITE",
			"WORTH",
			"WRITE",
			"WRONG",
			"WATCH",
			"WROTE",
			"WASTE",
			"WORSE",
			"WORST",
			"WHEEL",
			"WIDTH",
			"WHEAT",
			"WIRES",
			"WIRED",
			"WRIST",
			"WEIRD",
			"WEIGH",
			"WAIST",
			"WAGON",
			"WIDEN",
			"WRECK",
			"WHALE",
			"WINDY",
			"WHISK",
			"WALTZ",
			"YOUNG",
			"YOUTH",
			"YIELD",
			"YACHT",
			"YEAST",
			"YODEL",
			"YELLS",
			"ZEBRA"
		},
		new List<string>
		{
			"ALUMNI",
			"AROUND",
			"ACROSS",
			"ALWAYS",
			"ACCESS",
			"ALMOST",
			"ACTION",
			"ACTUAL",
			"ANNUAL",
			"AMOUNT",
			"ANYONE",
			"ACTIVE",
			"ANSWER",
			"AGENCY",
			"APPEAR",
			"AFFECT",
			"ACCEPT",
			"ADVOCE",
			"APPEAL",
			"ATTACK",
			"AUTHOR",
			"ANIMAL",
			"ACTING",
			"ASSUME",
			"ASSIST",
			"ATTEND",
			"ANYWAY",
			"ASPECT",
			"AFFORD",
			"ARTIST",
			"ALPACA",
			"AFRAID",
			"AGENDA",
			"ARRIVE",
			"ADVISE",
			"ALLIED",
			"ABSENT",
			"ADJUST",
			"AUTUMN",
			"ACCENT",
			"ABSORB",
			"ASLEEP",
			"ANCHOR",
			"ATOMIC",
			"ATTACH",
			"ATTAIN",
			"ASSERT",
			"ABSURD",
			"ASSIGN",
			"ADMIRE",
			"ARCADE",
			"ARCHER",
			"ABRUPT",
			"AFFIRM",
			"ASHORE",
			"ACCUSE",
			"ANALOG",
			"ALMOND",
			"APATHY",
			"ASCEND",
			"BEFORE",
			"BETTER",
			"BECOME",
			"BEHIND",
			"BECAME",
			"BEYOND",
			"BUDGET",
			"BOTTOM",
			"BRANCH",
			"BOUGHT",
			"BATTLE",
			"BRIDGE",
			"BROKEN",
			"BANDIT",
			"BACKED",
			"BRIGHT",
			"BEHALF",
			"BEAUTY",
			"BAYOUS",
			"BORDER",
			"BREATH",
			"BOTTLE",
			"BELONG",
			"BUTTON",
			"BARELY",
			"BESIDE",
			"BREACH",
			"BITTER",
			"BOTHER",
			"BUTTER",
			"BAOBAB",
			"BUTLER",
			"BASKET",
			"BALLET",
			"BRONZE",
			"BARREL",
			"BORROW",
			"BEHAVE",
			"BUNDLE",
			"BANNER",
			"BANKER",
			"BOXING",
			"BREEZE",
			"BUBBLE",
			"BINARY",
			"BUCKET",
			"BOUNCE",
			"BROWSE",
			"BUFFET",
			"BANANA",
			"BOILER",
			"BEACON",
			"BEWARE",
			"BAKERY",
			"BOILED",
			"BUMPER",
			"BINDER",
			"BEAVER",
			"BADGER",
			"BAMBOO",
			"CHANGE",
			"COMMON",
			"COURSE",
			"COMING",
			"CREATE",
			"CHOICE",
			"CREDIT",
			"CHARGE",
			"CHANCE",
			"CLIENT",
			"CLOSED",
			"COUPLE",
			"CENTER",
			"CHOOSE",
			"CHOSEN",
			"CAUGHT",
			"COWBOY",
			"CORNER",
			"CLOSER",
			"COFFEE",
			"CUSTOM",
			"CIRCLE",
			"CAMERA",
			"COLUMN",
			"COPPER",
			"CASTLE",
			"COMPLY",
			"CARBON",
			"COSTLY",
			"CASUAL",
			"CARING",
			"COMEDY",
			"COTTON",
			"COMMIT",
			"CARPET",
			"CATTLE",
			"CLEVER",
			"CRUISE",
			"CONVEY",
			"COLLAR",
			"CANYON",
			"CHERRY",
			"COUPON",
			"CANVAS",
			"CEMENT",
			"CHORUS",
			"CANNON",
			"CALLER",
			"CIRCUS",
			"CANDLE",
			"COOLER",
			"COOLED",
			"CRUNCH",
			"CEREAL",
			"CLOSET",
			"CELLAR",
			"COSMIC",
			"CATBOY",
			"DESIGN",
			"DEMAND",
			"DIRECT",
			"DEGREE",
			"DOUBLE",
			"DAMAGE",
			"DEVICE",
			"DETAIL",
			"DOCTOR",
			"DECIDE",
			"DESIRE",
			"DEPEND",
			"DANGER",
			"DEFINE",
			"DEVILS",
			"DEALER",
			"DEFEAT",
			"DESERT",
			"DEFEND",
			"DETECT",
			"DECENT",
			"DIVINE",
			"DENIAL",
			"DRAGON",
			"DONATE",
			"DRAWER",
			"DELETE",
			"DEPART",
			"DOMINO",
			"DONKEY",
			"DELUXE",
			"DIALOG",
			"DECEIT",
			"DEFUSE",
			"DEDUCE",
			"DEDUCT",
			"DEBRIS",
			"DIVERT",
			"DEMISE",
			"DOMAIN",
			"DEBATE",
			"DECADE",
			"DIFFER",
			"DIGEST",
			"DEVOTE",
			"DEVISE",
			"DISMAY",
			"EITHER",
			"ENOUGH",
			"EFFECT",
			"EXPECT",
			"ENERGY",
			"EASILY",
			"EXCEPT",
			"ENABLE",
			"EFFORT",
			"ENGINE",
			"EDITOR",
			"EXPAND",
			"EXPERT",
			"EXTEND",
			"ENDING",
			"EATING",
			"ESCAPE",
			"EXPORT",
			"EMPIRE",
			"ENGAGE",
			"ENTITY",
			"EXCUSE",
			"EXEMPT",
			"EXOTIC",
			"EVOLVE",
			"EXPOSE",
			"EXPIRE",
			"ESTEEM",
			"ENDURE",
			"ELDEST",
			"EMBARK",
			"ENCORE",
			"EDIBLE",
			"EMBLEM",
			"ENIGMA",
			"EXPEND",
			"EUREKA",
			"ERRAND",
			"ELIXIR",
			"EXHALE",
			"ENDEAR",
			"EQUATE",
			"EMBRYO",
			"ENZYME",
			"ENTIRE",
			"ESTATE",
			"FUTURE",
			"FAMILY",
			"FORMER",
			"FOURTH",
			"FIGURE",
			"FOLLOW",
			"FRIEND",
			"FACTOR",
			"FORCED",
			"FORMAL",
			"FOREST",
			"FAMOUS",
			"FACING",
			"FLIGHT",
			"FAIRLY",
			"FELLOW",
			"FINISH",
			"FORMAT",
			"FORGET",
			"FLYING",
			"FALLEN",
			"FOUGHT",
			"FINGER",
			"FABRIC",
			"FROZEN",
			"FILTER",
			"FARMER",
			"FLOWER",
			"FISHER",
			"FUSION",
			"FLAVOR",
			"FIERCE",
			"FREEZE",
			"FORGOT",
			"FOSSIL",
			"FINITE",
			"FINALE",
			"FADING",
			"FAULTY",
			"FOLDER",
			"FACADE",
			"FRENZY",
			"FALCON",
			"FRIDGE",
			"FUNGUS",
			"FORBID",
			"FIASCO",
			"FIDDLE",
			"FLUFFY",
			"FERRET",
			"FAUCET",
			"GROWTH",
			"GLOBAL",
			"GROUND",
			"GARDEN",
			"GOLDEN",
			"GATHER",
			"GLANCE",
			"GARAGE",
			"GENTLE",
			"GUITAR",
			"GENIUS",
			"GAMBLE",
			"GALAXY",
			"GRAVEL",
			"GAMING",
			"GALLON",
			"GARLIC",
			"GRADES",
			"GIFTED",
			"GOTTEN",
			"GINGER",
			"GROOVE",
			"GREASE",
			"GLOOMY",
			"GREEDY",
			"GRASSY",
			"GREASY",
			"GUTTER",
			"GOALIE",
			"GLIDER",
			"GIGGLE",
			"GALLOP",
			"GRUDGE",
			"GADGET",
			"GRUMPY",
			"GOBLIN",
			"GOBLET",
			"GLITCH",
			"GEYSER",
			"GAZEBO",
			"GROOVY",
			"GALORE",
			"GRIEVE",
			"GRANNY",
			"GOVERN",
			"GUILTY",
			"GENDER",
			"GOPHER",
			"HEALTH",
			"HAPPEN",
			"HANDLE",
			"HARDLY",
			"HARDER",
			"HIDDEN",
			"HEIGHT",
			"HONEST",
			"HUNGRY",
			"HEAVEN",
			"HORROR",
			"HUNTER",
			"HARBOR",
			"HYBRID",
			"HEATED",
			"HEATER",
			"HAMMER",
			"HAZARD",
			"HUNGER",
			"HOCKEY",
			"HOLLOW",
			"HUMBLE",
			"HOOKED",
			"HEROIC",
			"HELMET",
			"HASSLE",
			"HURDLE",
			"HOURLY",
			"HUMANE",
			"HINDER",
			"HOPPER",
			"HERBAL",
			"HALVES",
			"HELPER",
			"HELPED",
			"HANGAR",
			"HUMMER",
			"HARDEN",
			"HAMPER",
			"HELIUM",
			"HIATUS",
			"HERESY",
			"HUSTLE",
			"HORRID",
			"HOMELY",
			"HEALER",
			"HOOVES",
			"HICCUP",
			"HIJACK",
			"INCOME",
			"IMPACT",
			"INSIDE",
			"INDEED",
			"ISLAND",
			"INTEND",
			"INTENT",
			"INVEST",
			"IMPORT",
			"IGNORE",
			"INFORM",
			"IMMUNE",
			"INVITE",
			"INSIST",
			"INTACT",
			"INDOOR",
			"INSERT",
			"IRONIC",
			"INSURE",
			"INSECT",
			"INSULT",
			"INWARD",
			"INVENT",
			"INSANE",
			"INJECT",
			"INVADE",
			"INFECT",
			"IMPAIR",
			"IMPEDE",
			"IGNITE",
			"INJURE",
			"INJURY",
			"INDIGO",
			"INHALE",
			"INVERT",
			"IMPURE",
			"INFAMY",
			"INDENT",
			"ICEBOX",
			"ICICLE",
			"INFEST",
			"INDUCT",
			"INFUSE",
			"IMPART",
			"INDUCE",
			"IMPOSE",
			"ITSELF",
			"INFANT",
			"INVOKE",
			"JUNIOR",
			"JERSEY",
			"JACKET",
			"JUNGLE",
			"JUMPER",
			"JUMPED",
			"JARGON",
			"JAGUAR",
			"JOYFUL",
			"JUMBLE",
			"JOYOUS",
			"JIGSAW",
			"JUGGLE",
			"JINGLE",
			"JESTER",
			"JUICED",
			"JUICER",
			"JAILER",
			"JAILED",
			"JAILOR",
			"JIGGLE",
			"JETWAY",
			"JETLAG",
			"JOCKEY",
			"JUNKER",
			"JASPER",
			"JAUNTY",
			"JOINED",
			"JOVIAL",
			"JINGLY",
			"JIVING",
			"JINXED",
			"JINXES",
			"JAMMED",
			"JAMMER",
			"JANGLY",
			"JEWELS",
			"JOKERS",
			"JOKILY",
			"JOKING",
			"JOULES",
			"JOGGER",
			"JUDGER",
			"JUKING",
			"JURIES",
			"JURORS",
			"JUSTLY",
			"JUSTLE",
			"JUICES",
			"JACKED",
			"JACKER",
			"KNIGHT",
			"KIDNEY",
			"KEEPER",
			"KINDLY",
			"KETTLE",
			"KARATE",
			"KITTEN",
			"KICKER",
			"KICKED",
			"KEYPAD",
			"KINDLE",
			"KINGLY",
			"KAZOOS",
			"KELVIN",
			"KERNEL",
			"KENNEL",
			"KEENED",
			"KEENLY",
			"KEELED",
			"KEBOBS",
			"KNOCKS",
			"KRAKEN",
			"LITTLE",
			"LEADER",
			"LIKELY",
			"LIVING",
			"LATEST",
			"LETTER",
			"LEAGUE",
			"LISTEN",
			"LAUNCH",
			"LENGTH",
			"LEAVES",
			"LINKED",
			"LOSING",
			"LIGHTS",
			"LIQUID",
			"LEGACY",
			"LUXURY",
			"LAWYER",
			"LESSON",
			"LOVELY",
			"LESSER",
			"LOADED",
			"LINEAR",
			"LANDED",
			"LOCATE",
			"LAYOUT",
			"LOVING",
			"LEGEND",
			"LIVELY",
			"LOUNGE",
			"LONELY",
			"LATELY",
			"LADDER",
			"LEGION",
			"LOCKER",
			"LAPTOP",
			"LAWFUL",
			"LINGER",
			"LUMBER",
			"LOSSEN",
			"LAGOON",
			"LIZARD",
			"LOTION",
			"LOCALE",
			"LIVERY",
			"LOATHE",
			"LOADER",
			"LOCUST",
			"MANIAC",
			"MARKET",
			"MAKING",
			"MEMBER",
			"MATTER",
			"MIDDLE",
			"MOVING",
			"MANAGE",
			"MOMENT",
			"MODERN",
			"METHOD",
			"MINUTE",
			"MEMORY",
			"MASTER",
			"MANNER",
			"MYSELF",
			"MEDIUM",
			"MUSKET",
			"MAINLY",
			"MOTION",
			"MOBILE",
			"MARKED",
			"MUSEUM",
			"MOSTLY",
			"MUTUAL",
			"MARGIN",
			"MODULE",
			"MINING",
			"MANUAL",
			"MODEST",
			"MIRROR",
			"MATURE",
			"MUSCLE",
			"MATRIX",
			"MEDIAN",
			"MODIFY",
			"MORALE",
			"MARBLE",
			"MOTIVE",
			"MARKER",
			"METRIC",
			"MENTOR",
			"MAGNET",
			"MELODY",
			"MONKEY",
			"MEADOW",
			"MYSTIC",
			"MAYHEM",
			"MAKEUP",
			"MANTLE",
			"MAILED",
			"MAILER",
			"MOLTEN",
			"MEMOIR",
			"MIRAGE",
			"MUTANT",
			"MISLED",
			"MISUSE",
			"NUMBER",
			"NEARLY",
			"NATURE",
			"NATION",
			"NORMAL",
			"NOTICE",
			"NATIVE",
			"NOBODY",
			"NARROW",
			"NEARBY",
			"NIGHTS",
			"NEEDLE",
			"NOTIFY",
			"NOVICE",
			"NICKEL",
			"NOZZLE",
			"NIMBLE",
			"NAPKIN",
			"NEGATE",
			"NECTAR",
			"NUGGET",
			"NODDLE",
			"NIBBLE",
			"NOTATE",
			"NEATLY",
			"NICEST",
			"NINJAS",
			"NOBLES",
			"NOSIER",
			"NOSILY",
			"NOVELS",
			"NUDGER",
			"NUDGES",
			"NURSED",
			"NUANCE",
			"OFFICE",
			"OPTION",
			"OBTAIN",
			"OUTPUT",
			"ONLINE",
			"OBJECT",
			"ORANGE",
			"OFFSET",
			"ORIGIN",
			"OXYGEN",
			"OCCUPY",
			"OPPOSE",
			"OUTLET",
			"OUTFIT",
			"ORDEAL",
			"ONWARD",
			"OYSTER",
			"OUTLAW",
			"OUTAGE",
			"OBTUSE",
			"OUTWIT",
			"OCELOT",
			"OBEYED",
			"OCCURS",
			"OCTAVE",
			"OCTANE",
			"OCULAR",
			"OFFERS",
			"OLIVES",
			"OLDEST",
			"OMELET",
			"ONIONS",
			"OPENLY",
			"OUNCES",
			"OVERDO",
			"OVERLY",
			"OWNING",
			"PEOPLE",
			"PUBLIC",
			"PERIOD",
			"PLEASE",
			"POLICY",
			"PERSON",
			"POLICE",
			"PROFIT",
			"PLAYER",
			"PRETTY",
			"PARENT",
			"PROPER",
			"PICKED",
			"PLENTY",
			"PROVEN",
			"PURSUE",
			"PARTLY",
			"PREFER",
			"PRINCE",
			"POCKET",
			"PACKED",
			"PALACE",
			"PHRASE",
			"PLANET",
			"PACKET",
			"POETRY",
			"PORTAL",
			"POWDER",
			"POLISH",
			"PLASMA",
			"PROMPT",
			"PARADE",
			"PURPLE",
			"PEPPER",
			"POSTER",
			"PENCIL",
			"POTATO",
			"PURITY",
			"PUZZLE",
			"POLITE",
			"PICKUP",
			"POETIC",
			"PICNIC",
			"PARDON",
			"PLAQUE",
			"PILLOW",
			"PILLAR",
			"PASTRY",
			"PIGEON",
			"PEANUT",
			"QUARTZ",
			"QUARRY",
			"QUAINT",
			"QUIVER",
			"QUENCH",
			"QUEASY",
			"QUICHE",
			"QUOTES",
			"QUOTER",
			"QUALMS",
			"QUAILS",
			"QUAKES",
			"QUAKED",
			"QUARKS",
			"QUACKS",
			"QUEENS",
			"QUEUES",
			"QUEUED",
			"QUIRKS",
			"QUIRKY",
			"QUILLS",
			"REPORT",
			"RESULT",
			"REALLY",
			"RECENT",
			"RECORD",
			"RETURN",
			"RATHER",
			"REASON",
			"REVIEW",
			"REFORM",
			"REDUCE",
			"REMAIN",
			"REGION",
			"RAISED",
			"RELIEF",
			"RISING",
			"REMOTE",
			"RETAIN",
			"REGARD",
			"REMOVE",
			"RATING",
			"RELATE",
			"REPAIR",
			"RARELY",
			"RULING",
			"RESORT",
			"REPEAT",
			"ROBUST",
			"REVEAL",
			"REPLAY",
			"RECALL",
			"RANDOM",
			"REWARD",
			"RIDING",
			"RESCUE",
			"RUBBER",
			"REVISE",
			"REFUSE",
			"RESIST",
			"RETIRE",
			"RENTAL",
			"REMIND",
			"REJECT",
			"RHYTHM",
			"REMEDY",
			"RUNNER",
			"RECIPE",
			"RITUAL",
			"RIBBON",
			"ROCKET",
			"RABBIT",
			"RESIGN",
			"REMARK",
			"RADIUS",
			"REFUGE",
			"REFUND",
			"REPAID",
			"RIPPED",
			"ROSTER",
			"ROTARY",
			"REDEEM",
			"REVIVE",
			"RIDDEN",
			"RUNWAY",
			"REVOLT",
			"REFINE",
			"ROTTEN",
			"RECKON",
			"REPEAL",
			"RELISH",
			"ROTATE",
			"REVERT",
			"REFLEX",
			"RUBBLE",
			"REOPEN",
			"SHOULD",
			"SYSTEM",
			"SECOND",
			"SCHOOL",
			"STRONG",
			"SIZZLE",
			"SINGLE",
			"SOCIAL",
			"SERIES",
			"STREET",
			"SENIOR",
			"SIMPLY",
			"SOURCE",
			"SUPPLY",
			"SIMPLE",
			"SEASON",
			"SUMMER",
			"SAYING",
			"SAFETY",
			"SECTOR",
			"STATUS",
			"SIGNED",
			"SQUARE",
			"SECURE",
			"SURVEY",
			"SEARCH",
			"SPRING",
			"SCREEN",
			"STUDIO",
			"SPREAD",
			"SELECT",
			"SPEECH",
			"SYMBOL",
			"SPIRIT",
			"STABLE",
			"SOUGHT",
			"SAMPLE",
			"SCHEME",
			"SILVER",
			"SIGNAL",
			"STRIKE",
			"SEVERE",
			"SECRET",
			"SWITCH",
			"SAVING",
			"STEADY",
			"STRUCK",
			"STREAM",
			"SMOOTH",
			"SURELY",
			"SOLELY",
			"SUMMIT",
			"SUDDEN",
			"SLIGHT",
			"SPOKEN",
			"SILENT",
			"SETTLE",
			"STRICT",
			"SUBMIT",
			"STRING",
			"STOLEN",
			"SHADOW",
			"SINGER",
			"SOCCER",
			"SUPERB",
			"SERIAL",
			"SUBTLE",
			"SOONER",
			"STATIC",
			"SHIELD",
			"STANCE",
			"SCRIPT",
			"SACRED",
			"SIERRA",
			"SELDOM",
			"SALMON",
			"SHOWER",
			"SPHERE",
			"SPRINT",
			"SUNSET",
			"STRIVE",
			"STEREO",
			"SCARCE",
			"THOUGH",
			"TAKING",
			"TRYING",
			"TARGET",
			"TRAVEL",
			"THEORY",
			"TETRIS",
			"THANKS",
			"TOWARD",
			"TIMING",
			"TALENT",
			"TAUGHT",
			"TEAPOT",
			"TICKET",
			"TISSUE",
			"TENNIS",
			"TIMELY",
			"TENDER",
			"THROWN",
			"TACKLE",
			"TURKEY",
			"TRIPLE",
			"TEMPLE",
			"THROAT",
			"TIMBER",
			"TUNNEL",
			"TONGUE",
			"TRAGIC",
			"TROPHY",
			"TITLED",
			"THESIS",
			"TOILET",
			"THEIRS",
			"TAILOR",
			"THREAD",
			"THRIVE",
			"TOMATO",
			"THRILL",
			"TRICKY",
			"THRONE",
			"TACTIC",
			"TAXING",
			"TRENDY",
			"THIRST",
			"TUMBLE",
			"TURTLE",
			"TRIVIA",
			"TANGLE",
			"THWART",
			"TYCOON",
			"TESTER",
			"TRIPOD",
			"TINKER",
			"TUXEDO",
			"UNITED",
			"UNIQUE",
			"UNLESS",
			"USEFUL",
			"UNABLE",
			"UPDATE",
			"UNLIKE",
			"URGENT",
			"UNFAIR",
			"UPWARD",
			"UPTIME",
			"UNPAID",
			"UPLOAD",
			"UNUSED",
			"UPHELD",
			"UNREST",
			"UNEVEN",
			"UNLOCK",
			"UNSURE",
			"UTMOST",
			"UNEASY",
			"USABLE",
			"UNSAFE",
			"UNSEEN",
			"UPTAKE",
			"UPHOLD",
			"UNTRUE",
			"UNVEIL",
			"UNJUST",
			"UNISON",
			"UMPIRE",
			"UNFOLD",
			"UPSIDE",
			"UNREAL",
			"UNWISE",
			"UNLOAD",
			"UTOPIA",
			"UPROAR",
			"UNRULY",
			"UNWIND",
			"URCHIN",
			"UNWELL",
			"UPROOT",
			"UNWRAP",
			"VALLEY",
			"VOLUME",
			"VISION",
			"VISUAL",
			"VENDOR",
			"VERSUS",
			"VICTIM",
			"VARIED",
			"VIABLE",
			"VIRTUE",
			"VESSEL",
			"VERBAL",
			"VACUUM",
			"VACANT",
			"VIEWER",
			"VECTOR",
			"VERIFY",
			"VELVET",
			"VOYAGE",
			"VANITY",
			"VIOLIN",
			"VIOLET",
			"VANISH",
			"VEILED",
			"VORTEX",
			"VERSED",
			"VOLLEY",
			"VIGOUR",
			"VOODOO",
			"VERTEX",
			"VACATE",
			"VERMIN",
			"VAULTS",
			"VARIES",
			"VASTLY",
			"VALUED",
			"VALUES",
			"VALVES",
			"VEGGIE",
			"VEXING",
			"VIKING",
			"VIEWED",
			"WYVERN",
			"WITHIN",
			"WEIGHT",
			"WINDOW",
			"WINTER",
			"WEALTH",
			"WINNER",
			"WONDER",
			"WEEKLY",
			"WORKER",
			"WOODEN",
			"WISDOM",
			"WORTHY",
			"WARMTH",
			"WIRING",
			"WIZARD",
			"WALNUT",
			"WALLET",
			"WEAKEN",
			"WANDER",
			"WOLVES",
			"WAITER",
			"WEAKLY",
			"WORSEN",
			"WASHER",
			"WRENCH",
			"WREATH",
			"WITHER",
			"WAFFLE",
			"WEASEL",
			"WETTER",
			"WOBBLE",
			"WIGGLE",
			"WHIMSY",
			"WIDGET",
			"WELDER",
			"WOEFUL",
			"WADDLE",
			"WHEEZE",
			"WALRUS",
			"WEBBED",
			"WALKER",
			"WALKED",
			"WHEELS",
			"WORKED",
			"WRITES",
			"YELLOW",
			"YEARLY",
			"YOGURT",
			"YONDER",
			"YACHTS",
			"ZODIAC",
			"ZOMBIE",
			"ZIPPER",
			"ZIGZAG",
			"ZEALOT",
			"ZINGER",
			"ZAPPED",
			"ZAPPER",
			"ZIGGED",
			"ZAGGED",
			"ZOOMED",
			"ZEBRAS",
			"ZEROES"
		},
		new List<string>
		{
			"ANOTHER",
			"AGAINST",
			"ALREADY",
			"ABILITY",
			"AVERAGE",
			"ACCOUNT",
			"ADDRESS",
			"ACHIEVE",
			"APPLIED",
			"ATTEMPT",
			"ARTICLE",
			"ADVANCE",
			"ACQUIRE",
			"ABSENCE",
			"ATTRACT",
			"ANYBODY",
			"ANCIENT",
			"ANALYST",
			"ARRIVAL",
			"ACADEMY",
			"ADVISER",
			"AUCTION",
			"ANXIETY",
			"ARRANGE",
			"ANXIOUS",
			"APPROVE",
			"ARCHIVE",
			"AMATEUR",
			"APPOINT",
			"AWKWARD",
			"ANALYZE",
			"AUDITOR",
			"ANYMORE",
			"ALRIGHT",
			"ATHLETE",
			"ANTIQUE",
			"ANALOGY",
			"ANYTIME",
			"AWESOME",
			"ARTWORK",
			"AMUSING",
			"AMNESTY",
			"ANATOMY",
			"APOLOGY",
			"AQUATIC",
			"AGILITY",
			"APPLAUD",
			"ALLERGY",
			"AUDIBLE",
			"ABOLISH",
			"AMBIENT",
			"ANTENNA",
			"ADAPTER",
			"ABDOMEN",
			"AEROBIC",
			"ASPHALT",
			"ALGEBRA",
			"BETWEEN",
			"BECAUSE",
			"BELIEVE",
			"BENEFIT",
			"BROUGHT",
			"BILLION",
			"BALANCE",
			"BANKING",
			"BENEATH",
			"BEDROOM",
			"BINDING",
			"BATTERY",
			"BARRIER",
			"BIOLOGY",
			"BUILDER",
			"BARGAIN",
			"BIDDING",
			"BROADEN",
			"BELOVED",
			"BLANKET",
			"BIZARRE",
			"BICYCLE",
			"BUFFALO",
			"BOWLING",
			"BALLOON",
			"BRIGADE",
			"BOULDER",
			"BANQUET",
			"BOILING",
			"BAGGAGE",
			"BONDING",
			"BRACKET",
			"BALCONY",
			"BRUSHED",
			"BOYCOTT",
			"BLASTED",
			"BLATANT",
			"BANANAS",
			"BLOSSOM",
			"BRITTLE",
			"BUOYANT",
			"BISCUIT",
			"BRAVERY",
			"BURGLAR",
			"BROWNIE",
			"BOLOGNA",
			"COMPANY",
			"CONTROL",
			"CERTAIN",
			"CURRENT",
			"COUNTRY",
			"CONTACT",
			"CAPITAL",
			"CENTRAL",
			"COUNCIL",
			"CENTURY",
			"COMPLEX",
			"COLLEGE",
			"CONTENT",
			"CONCERN",
			"CHAPTER",
			"CHANNEL",
			"CONTAIN",
			"CULTURE",
			"CALLING",
			"CONCEPT",
			"CONTEXT",
			"CONDUCT",
			"CLASSES",
			"CORRECT",
			"CAPABLE",
			"CUTTING",
			"CLOSING",
			"COMMENT",
			"CRUCIAL",
			"CLASSIC",
			"COMMAND",
			"CIRCUIT",
			"CAREFUL",
			"COMPETE",
			"COUNTER",
			"CLOTHES",
			"COMFORT",
			"CARRIER",
			"COMBINE",
			"CAPTURE",
			"COLLECT",
			"CLIMATE",
			"COMPARE",
			"CAPTAIN",
			"CHAMBER",
			"CONFIRM",
			"CABINET",
			"CHARITY",
			"COMPACT",
			"CAPTION",
			"CONNECT",
			"CRYSTAL",
			"CONCERT",
			"CONVERT",
			"CONSIST",
			"CLOSURE",
			"CALIBER",
			"CHICKEN",
			"CAUTION",
			"CHECKED",
			"CEILING",
			"COURAGE",
			"CITIZEN",
			"CONSULT",
			"CURIOUS",
			"CLARITY",
			"CROWDED",
			"CLUSTER",
			"CREATOR",
			"COTTAGE",
			"CLARIFY",
			"CLEANER",
			"CALCIUM",
			"CONSOLE",
			"CRUSHED",
			"CURTAIN",
			"CRICKET",
			"CONSUME",
			"CARTOON",
			"COSTUME",
			"DEVELOP",
			"DESPITE",
			"DISCUSS",
			"DRIVING",
			"DELIVER",
			"DIGITAL",
			"DISPLAY",
			"DECLINE",
			"DEALING",
			"DRAWING",
			"DYNAMIC",
			"DESKTOP",
			"DEFICIT",
			"DEPOSIT",
			"DISPUTE",
			"DENSITY",
			"DEFENCE",
			"DISTANT",
			"DIAMOND",
			"DEFAULT",
			"DESTROY",
			"DEFENSE",
			"DESERVE",
			"DELIGHT",
			"DIGNITY",
			"DILEMMA",
			"DECLARE",
			"DISMISS",
			"DESTINY",
			"DESCENT",
			"DURABLE",
			"DIAGRAM",
			"DISPOSE",
			"DICTATE",
			"DIPLOMA",
			"DOLPHIN",
			"DISLIKE",
			"DISTURB",
			"DIFFUSE",
			"DRUMMER",
			"DESCEND",
			"DECENCY",
			"DISCARD",
			"DISTORT",
			"DECIMAL",
			"DASHING",
			"DECEIVE",
			"DECODER",
			"DISCORD",
			"DISABLE",
			"EXAMPLE",
			"ECONOMY",
			"EXACTLY",
			"EVENING",
			"EXPENSE",
			"EXPRESS",
			"EXPLAIN",
			"EXCITED",
			"ENHANCE",
			"ELEMENT",
			"EDITION",
			"EXAMINE",
			"EXTREME",
			"EXPLORE",
			"ELDERLY",
			"EVIDENT",
			"EXHIBIT",
			"EXCLUDE",
			"ESSENCE",
			"ELEGANT",
			"EPISODE",
			"EMBRACE",
			"EXTRACT",
			"EMOTION",
			"EROSION",
			"EDUCATE",
			"EMPEROR",
			"EMINENT",
			"ETERNAL",
			"ELUSIVE",
			"EXHAUST",
			"ECOLOGY",
			"ECLIPSE",
			"ELASTIC",
			"EMULATE",
			"EXPLODE",
			"EMERALD",
			"EMPATHY",
			"EXTINCT",
			"EMBARGO",
			"ENLARGE",
			"ELEVATE",
			"EVASION",
			"ENCRYPT",
			"EQUATOR",
			"EPITOME",
			"EXCERPT",
			"ENVIOUS",
			"EVASIVE",
			"ENTROPY",
			"FURTHER",
			"FORWARD",
			"FOREIGN",
			"FINANCE",
			"FEATURE",
			"FEELING",
			"FINDING",
			"FREEDOM",
			"FORTUNE",
			"FASHION",
			"FOUNDER",
			"FACTORY",
			"FORMULA",
			"FISHING",
			"FILLING",
			"FITNESS",
			"FOREVER",
			"FICTION",
			"FANTASY",
			"FITTING",
			"FRAGILE",
			"FACTUAL",
			"FATIGUE",
			"FORGIVE",
			"FOOTAGE",
			"FACTION",
			"FEATHER",
			"FESTIVE",
			"FURNACE",
			"FURNISH",
			"FLICKER",
			"FANFARE",
			"FLATTEN",
			"GENERAL",
			"GALLERY",
			"GENUINE",
			"GRAPHIC",
			"GRAVITY",
			"GESTURE",
			"GENERIC",
			"GRADUAL",
			"GLIMPSE",
			"GROCERY",
			"GEOLOGY",
			"GRANITE",
			"GARMENT",
			"GRIFFIN",
			"GARBAGE",
			"GOURMET",
			"GLITTER",
			"GLACIAL",
			"GARNISH",
			"GORILLA",
			"GIMMICK",
			"GRUMBLE",
			"GRIZZLY",
			"GONDOLA",
			"GRIMACE",
			"GAZELLE",
			"GIRAFFE",
			"GELATIN",
			"GYMNAST",
			"HOWEVER",
			"HISTORY",
			"HIDEOUT",
			"HOLDING",
			"HELPING",
			"HUNDRED",
			"HOLIDAY",
			"HEALTHY",
			"HEARING",
			"HEAVILY",
			"HELPFUL",
			"HIGHWAY",
			"HORIZON",
			"HARVEST",
			"HARMONY",
			"HOPEFUL",
			"HALFWAY",
			"HARNESS",
			"HYGIENE",
			"HONESTY",
			"HALLWAY",
			"HAMSTER",
			"HAMMOCK",
			"HEXAGON",
			"INCLUDE",
			"IMPROVE",
			"INVOLVE",
			"INITIAL",
			"INSTEAD",
			"IMAGINE",
			"INTENSE",
			"INSTALL",
			"IDEALLY",
			"IMMENSE",
			"IMAGERY",
			"INSPIRE",
			"IMPULSE",
			"INSPECT",
			"ISOLATE",
			"INHIBIT",
			"INVALID",
			"IMPRESS",
			"INDULGE",
			"INVERSE",
			"IMPLANT",
			"INERTIA",
			"IMITATE",
			"INTEGER",
			"INFERNO",
			"INFLATE",
			"IMMERSE",
			"JUSTICE",
			"JOURNAL",
			"JOURNEY",
			"JUSTIFY",
			"JEALOUS",
			"JACKPOT",
			"JEWELRY",
			"JUKEBOX",
			"JANITOR",
			"JUGGLER",
			"JUGGLES",
			"JUMBLED",
			"JOGGING",
			"JUICING",
			"JUMPING",
			"KINGDOM",
			"KITCHEN",
			"KEYWORD",
			"KEYNOTE",
			"KINETIC",
			"KARAOKE",
			"KETCHUP",
			"KNUCKLE",
			"KEYHOLE",
			"KNEECAP",
			"KEYCARD",
			"KNEEPAD",
			"KEYPADS",
			"KINSHIP",
			"KINDRED",
			"LEADING",
			"LIMITED",
			"LIBRARY",
			"LARGELY",
			"LEARNED",
			"LEISURE",
			"LOYALTY",
			"LANDING",
			"LIBERTY",
			"LEATHER",
			"LENGTHY",
			"LECTURE",
			"LIGHTLY",
			"LIGHTER",
			"LOADING",
			"LAUNDRY",
			"LUGGAGE",
			"LOCKING",
			"LUCKILY",
			"LETTUCE",
			"LATENCY",
			"LOBSTER",
			"LOOKOUT",
			"LAGGING",
			"LANTERN",
			"LIGHTEN",
			"LIKABLE",
			"LENIENT",
			"LOCATOR",
			"LOCATES",
			"MILLION",
			"MANAGER",
			"MEETING",
			"MEDICAL",
			"MEANING",
			"MORNING",
			"MEASURE",
			"MESSAGE",
			"MACHINE",
			"MISSION",
			"MAXIMUM",
			"MINIMUM",
			"MONITOR",
			"MONTHLY",
			"MASSIVE",
			"MISSING",
			"MENTION",
			"MUSICAL",
			"MISTAKE",
			"MINIMAL",
			"MIXTURE",
			"MYSTERY",
			"MINERAL",
			"MODULAR",
			"MOLDING",
			"MARKING",
			"MAPPING",
			"MONSTER",
			"MANDATE",
			"MIRACLE",
			"MAGICAL",
			"MIGRATE",
			"MASSAGE",
			"MANSION",
			"MASTERY",
			"MILEAGE",
			"MUSTARD",
			"MAMMOTH",
			"MODULUS",
			"MODESTY",
			"MELODIC",
			"MAILBOX",
			"MOVABLE",
			"MANATEE",
			"MUSTANG",
			"MIDTERM",
			"MONTAGE",
			"MERMAID",
			"NETWORK",
			"NOTHING",
			"NATURAL",
			"NEITHER",
			"NUCLEAR",
			"NURSING",
			"NERVOUS",
			"NOTABLE",
			"NEUTRAL",
			"NURSERY",
			"NOVELTY",
			"NOTEPAD",
			"OFFICER",
			"OUTSIDE",
			"OVERALL",
			"OPENING",
			"OPERATE",
			"OPINION",
			"OBVIOUS",
			"OUTCOME",
			"OUTLOOK",
			"ONGOING",
			"ORGANIC",
			"OUTDOOR",
			"OPTIMAL",
			"OUTLINE",
			"OBSERVE",
			"OFFLINE",
			"OBSCURE",
			"OVERLAP",
			"OUTWARD",
			"ORCHARD",
			"OVERDUE",
			"ODYSSEY",
			"OVERLAY",
			"OUTPAST",
			"OCTOPUS",
			"OUTCAST",
			"OATMEAL",
			"OSTRICH",
			"OFFBEAT",
			"OCTAGON",
			"OVERUSE",
			"OUTLAST",
			"OUTLIER",
			"ORIGAMI",
			"PROVIDE",
			"PROCESS",
			"PRIVATE",
			"PRODUCT",
			"PROBLEM",
			"PROJECT",
			"PRESENT",
			"PERHAPS",
			"PROGRAM",
			"PRIMARY",
			"POPULAR",
			"PRODUCE",
			"PARTNER",
			"PERCENT",
			"PURPOSE",
			"PICTURE",
			"PATIENT",
			"PAYMENT",
			"PACKAGE",
			"PROTECT",
			"PREVENT",
			"PERFECT",
			"PATTERN",
			"PERFORM",
			"PROMOTE",
			"PROFILE",
			"PROMISE",
			"PLASTIC",
			"PORTION",
			"PREMIUM",
			"PREPARE",
			"PENDING",
			"PREDICT",
			"PASSAGE",
			"PRECISE",
			"PROTEIN",
			"PARKING",
			"PASSION",
			"PUSHING",
			"PROCEED",
			"PENALTY",
			"PIONEER",
			"PARTIAL",
			"PICKING",
			"PAINTED",
			"PRINTER",
			"PRIVACY",
			"PUBLISH",
			"PHOENIX",
			"PASSIVE",
			"PHYSICS",
			"PURSUIT",
			"PAINTER",
			"PACKING",
			"PREVIEW",
			"PREMISE",
			"PRETEND",
			"POSTURE",
			"PERSIST",
			"PREVAIL",
			"PATHWAY",
			"PARADOX",
			"PYRAMID",
			"PENGUIN",
			"PROSPER",
			"POULTRY",
			"POTTERY",
			"PITCHER",
			"PROVOKE",
			"PORTRAY",
			"PUDDING",
			"QUALITY",
			"QUARTER",
			"QUALIFY",
			"QUINTET",
			"QUANTUM",
			"QUIVERS",
			"QUERIES",
			"QUIETLY",
			"QUIETER",
			"QUICKLY",
			"QUICKER",
			"RELEASE",
			"RELATED",
			"RUNNING",
			"RECEIVE",
			"REQUIRE",
			"REFLECT",
			"RESPECT",
			"REGULAR",
			"READING",
			"REQUEST",
			"REALITY",
			"RESPOND",
			"REMOVED",
			"REMOVES",
			"REPLACE",
			"REALIZE",
			"RESERVE",
			"ROUTINE",
			"REVERSE",
			"RECOVER",
			"RESOLVE",
			"RAILWAY",
			"REMOVAL",
			"RESTORE",
			"RECEIPT",
			"RENEWAL",
			"RELAXED",
			"RANKING",
			"REFUSAL",
			"ROYALTY",
			"REFINED",
			"RAINBOW",
			"RIGHTLY",
			"REVIVAL",
			"RELIEVE",
			"RESTING",
			"REBUILD",
			"REFEREE",
			"REUNION",
			"REFRAIN",
			"RESIDUE",
			"REPLICA",
			"RELIANT",
			"ROCKING",
			"RAMPANT",
			"RECYCLE",
			"REWRITE",
			"REFRESH",
			"RIPPING",
			"RECITAL",
			"REVOLVE",
			"SEVENTH",
			"SERVICE",
			"SUPPORT",
			"SEVERAL",
			"SUBJECT",
			"SPECIAL",
			"SECTION",
			"SUCCESS",
			"SOCIETY",
			"SOMEONE",
			"SCIENCE",
			"SERIOUS",
			"STATION",
			"SETTING",
			"STUDENT",
			"SURFACE",
			"SUGGEST",
			"SHOWING",
			"STORAGE",
			"SPEAKER",
			"SESSION",
			"SITTING",
			"SUMMARY",
			"SPECIES",
			"SHORTLY",
			"STUDIED",
			"SURGERY",
			"STRANGE",
			"SEGMENT",
			"SUCCEED",
			"SURVIVE",
			"SUPREME",
			"SATISFY",
			"SILENCE",
			"STRETCH",
			"SUPPOSE",
			"SKILLED",
			"SURPLUS",
			"SUSTAIN",
			"STADIUM",
			"STOMACH",
			"SHERIFF",
			"SHELTER",
			"SHALLOW",
			"SPECIFY",
			"SHELVES",
			"SPOTTED",
			"STATUTE",
			"SCRATCH",
			"SURGEON",
			"STARTER",
			"SAILING",
			"SQUEEZE",
			"SCHOLAR",
			"SHUTTLE",
			"SCANNER",
			"STYLISH",
			"SOARING",
			"SMASHED",
			"SCENERY",
			"STANDBY",
			"SHORTEN",
			"THROUGH",
			"TRAFFIC",
			"TROUBLE",
			"TEACHER",
			"TURNING",
			"TYPICAL",
			"TONIGHT",
			"TELLING",
			"TOTALLY",
			"THEATER",
			"TENSION",
			"TRIBUTE",
			"TOURIST",
			"THERMAL",
			"TRIUMPH",
			"TRAINER",
			"TERRAIN",
			"TEXTURE",
			"TEXTILE",
			"THUNDER",
			"TRAILER",
			"TAPPING",
			"TURMOIL",
			"TRACTOR",
			"TWELFTH",
			"TRIVIAL",
			"TEDIOUS",
			"TRACING",
			"TIGHTEN",
			"TURBINE",
			"THINNER",
			"TODDLER",
			"TANGLED",
			"TRIDENT",
			"TRILOGY",
			"TROLLEY",
			"TRUMPET",
			"TORNADO",
			"THIRSTY",
			"TSUNAMI",
			"UNKNOWN",
			"UNUSUAL",
			"UTILITY",
			"UNIFORM",
			"UPGRADE",
			"UNCLEAR",
			"UNAWARE",
			"UTILIZE",
			"URGENCY",
			"USELESS",
			"UNCOVER",
			"UNHEARD",
			"UNLUCKY",
			"UNLEASH",
			"UNICORN",
			"UNKEMPT",
			"UNSCREW",
			"VARIOUS",
			"VARIETY",
			"VERSION",
			"VILLAGE",
			"VENTURE",
			"VICTORY",
			"VEHICLE",
			"VISIBLE",
			"VIRTUAL",
			"VISITOR",
			"VOLTAGE",
			"VINTAGE",
			"VITAMIN",
			"VIBRANT",
			"VACANCY",
			"VARIANT",
			"VINEGAR",
			"VOLCANO",
			"VANILLA",
			"VAMPIRE",
			"VIBRATE",
			"VULTURE",
			"WITHOUT",
			"WORKING",
			"WEBSITE",
			"WHETHER",
			"WRITTEN",
			"WRITING",
			"WAITING",
			"WELCOME",
			"WEATHER",
			"WEEKEND",
			"WALKING",
			"WARNING",
			"WEARING",
			"WITNESS",
			"WASHING",
			"WEALTHY",
			"WARRIOR",
			"WHISTLE",
			"WHISPER",
			"WEEKDAY",
			"WORKOUT",
			"WRESTLE",
			"WEBPAGE",
			"WRAPPER",
			"WHISKER",
			"WHIMPER",
			"YAWNING",
			"YOUNGER",
			"YOGURTS",
			"YELLING",
			"ZEALOUS",
			"ZOOLOGY",
			"ZIPLOCK",
			"ZOMBIES",
			"ZIPPERS",
			"ZIPPING"
		},
		new List<string>
		{
			"ALTHOUGH",
			"ANALYSIS",
			"APPROACH",
			"ACTUALLY",
			"ANYTHING",
			"ACTIVITY",
			"ADDITION",
			"ACHIEVED",
			"ACCEPTED",
			"ACQUIRED",
			"AFFECTED",
			"APPROVAL",
			"AUDIENCE",
			"ALLIANCE",
			"AIRCRAFT",
			"ANYWHERE",
			"ACADEMIC",
			"ACCURATE",
			"ASSEMBLY",
			"ARGUMENT",
			"ADEQUATE",
			"ATTACHED",
			"APPARENT",
			"ACCIDENT",
			"ACCURACY",
			"ANNOUNCE",
			"ABSOLUTE",
			"ADJUSTED",
			"ASSUMING",
			"ABSTRACT",
			"ADJACENT",
			"AVIATION",
			"ARTISTIC",
			"ADVOCATE",
			"APPENDIX",
			"ATHLETIC",
			"APPETITE",
			"ANIMATED",
			"AMBITION",
			"ABNORMAL",
			"ABUNDANT",
			"ASSEMBLE",
			"ALLOCATE",
			"ANTELOPE",
			"ALTITUDE",
			"AUTOMATE",
			"AQUARIUM",
			"APPLAUSE",
			"ADHESIVE",
			"ALLERGIC",
			"AUDITION",
			"ALPHABET",
			"ANCESTOR",
			"ANTIDOTE",
			"BUSINESS",
			"BUILDING",
			"BECOMING",
			"BREAKING",
			"BIRTHDAY",
			"BULLETIN",
			"BATHROOM",
			"BASEBALL",
			"BOUNDARY",
			"BACKBONE",
			"BRIEFING",
			"BROCHURE",
			"BACKWARD",
			"BASEMENT",
			"BEVERAGE",
			"BALLROOM",
			"BARBECUE",
			"BOUNCING",
			"BACKYARD",
			"BEGINNER",
			"BLOOMING",
			"BAREFOOT",
			"BROCCOLI",
			"BLIZZARD",
			"BRACELET",
			"BACKPACK",
			"BOOKMARK",
			"BOOKCASE",
			"BILLIARD",
			"BLOWFISH",
			"CHILDREN",
			"CONTINUE",
			"COMPLETE",
			"COMPUTER",
			"CUSTOMER",
			"CONTRACT",
			"CRITICAL",
			"CONSUMER",
			"CONSIDER",
			"CAPACITY",
			"CAMPAIGN",
			"CREATION",
			"CHEMICAL",
			"CATEGORY",
			"CONTRAST",
			"COVERAGE",
			"COVERING",
			"CONSTANT",
			"CREATIVE",
			"CHAMPION",
			"CONCRETE",
			"CEREMONY",
			"CLOTHING",
			"CALENDAR",
			"COMPOSED",
			"CROSSING",
			"COLLAPSE",
			"COMPOUND",
			"CORRIDOR",
			"CONTRARY",
			"CONCLUDE",
			"CONFUSED",
			"COLORFUL",
			"CIRCULAR",
			"CATCHING",
			"CONVINCE",
			"CAUTIOUS",
			"CREATURE",
			"COURTESY",
			"CARRIAGE",
			"COMPOSER",
			"CRITIQUE",
			"CYLINDER",
			"CREDIBLE",
			"COSMETIC",
			"CASSETTE",
			"CRESCENT",
			"CRACKING",
			"CHERRFUL",
			"CITATION",
			"CARNIVAL",
			"CLASSIFY",
			"COMEDIAN",
			"DIRECTOR",
			"DECISION",
			"DIVISION",
			"DIRECTLY",
			"DISTRICT",
			"DELIVERY",
			"DOCUMENT",
			"DISTANCE",
			"DATABASE",
			"DESCRIBE",
			"DECREASE",
			"DISCOVER",
			"DISTINCT",
			"DISCOUNT",
			"DESIGNER",
			"DURATION",
			"DOUBTFUL",
			"DIAMETER",
			"DEFINITE",
			"DELICATE",
			"DECISIVE",
			"DONATION",
			"DOWNWARD",
			"DISAGREE",
			"DOWNLOAD",
			"DIMINISH",
			"DEVOTION",
			"DISPATCH",
			"DETECTOR",
			"DISGUISE",
			"DISCREET",
			"DISSOLVE",
			"DIAGONAL",
			"DECORATE",
			"DINOSAUR",
			"DISTRACT",
			"DEDICATE",
			"EXCHANGE",
			"EVIDENCE",
			"EVERYONE",
			"EXERCISE",
			"EMPLOYEE",
			"EXTERNAL",
			"EXCITING",
			"EMPHASIS",
			"ENTIRELY",
			"ELECTRIC",
			"ESTIMATE",
			"ENORMOUS",
			"EXPOSURE",
			"ENGINEER",
			"EVALUATE",
			"EVERYDAY",
			"ELIGIBLE",
			"ENTRANCE",
			"EQUATION",
			"ENVELOPE",
			"ENDEAVOR",
			"ELEVATED",
			"EXTERIOR",
			"ENSEMBLE",
			"ELEPHANT",
			"ELEVENTH",
			"ELEVATOR",
			"ETERNITY",
			"ERUPTION",
			"EDUCATOR",
			"EVACUATE",
			"EXPONENT",
			"EGGPLANT",
			"EPILOGUE",
			"FUNCTION",
			"FINISHED",
			"FACILITY",
			"FOOTBALL",
			"FRIENDLY",
			"FAMILIAR",
			"FLEXIBLE",
			"FIREWALL",
			"FEATURED",
			"FREQUENT",
			"FORECAST",
			"FESTIVAL",
			"FEEDBACK",
			"FLOATING",
			"FRACTION",
			"FRONTIER",
			"FEASIBLE",
			"FORESTRY",
			"FOUNTAIN",
			"FABULOUS",
			"FLOURISH",
			"FRAGMENT",
			"FRICTION",
			"FOREHEAD",
			"FLASHING",
			"FORTRESS",
			"FAVORITE",
			"FARMLAND",
			"FORCEFUL",
			"FIXATION",
			"FLAWLESS",
			"FRIGHTEN",
			"FEARLESS",
			"FRAGRANT",
			"FINALIZE",
			"FOLLOWER",
			"FIRMWARE",
			"FLAMINGO",
			"FARTHEST",
			"FIDDLING",
			"GUIDANCE",
			"GENERATE",
			"GRAPHICS",
			"GRADUATE",
			"GRATEFUL",
			"GENEROUS",
			"GUARDIAN",
			"GREETING",
			"GLORIOUS",
			"GEOMETRY",
			"GORGEOUS",
			"GIGANTIC",
			"GRAFFITI",
			"GASOLINE",
			"GLOSSARY",
			"GRAPHITE",
			"GRASPING",
			"GIVEAWAY",
			"GOLDFISH",
			"GALACTIC",
			"GULLIBLE",
			"GRIDLOCK",
			"GEMSTONE",
			"GARGOYLE",
			"HOSPITAL",
			"HARDWARE",
			"HANDLING",
			"HOMELAND",
			"HANDSOME",
			"HOMEWORK",
			"HEADACHE",
			"HUMOROUS",
			"HARMLESS",
			"HALLMARK",
			"HUMIDITY",
			"HESITANT",
			"HANDMADE",
			"HEDGEHOG",
			"HYPNOSIS",
			"HORSEMAN",
			"HATCHING",
			"HEIRLOOM",
			"HOLOGRAM",
			"HAZELNUT",
			"INDUSTRY",
			"INTEREST",
			"INCREASE",
			"INVOLVED",
			"INTERNAL",
			"IDENTIFY",
			"INDICATE",
			"INFORMED",
			"IDENTITY",
			"INCIDENT",
			"INTERIOR",
			"INSTANCE",
			"INSPIRED",
			"ISOLATED",
			"INDIRECT",
			"INFORMAL",
			"INNOCENT",
			"INTERACT",
			"INVASION",
			"INTERVAL",
			"INITIATE",
			"INCLINED",
			"INCOMING",
			"INVITING",
			"IMMINENT",
			"INFINITE",
			"ILLUSION",
			"INSTINCT",
			"INFLATED",
			"IGNORANT",
			"INSTRUCT",
			"INFINITY",
			"INNOVATE",
			"INTRIGUE",
			"JUNCTION",
			"JEOPARDY",
			"JOYSTICK",
			"JALAPENO",
			"JUNKYARD",
			"JOKESTER",
			"JIGGLING",
			"JINGLING",
			"JEWELLER",
			"JOKINGLY",
			"JOYOUSLY",
			"KEYBOARD",
			"KINDNESS",
			"KNITTING",
			"KANGAROO",
			"KILOGRAM",
			"KNAPSACK",
			"KNOCKOFF",
			"LANGUAGE",
			"LEARNING",
			"LOCATION",
			"LIGHTING",
			"LEVERAGE",
			"LAUGHTER",
			"LATITUDE",
			"LAVENDER",
			"LUMINOUS",
			"LEMONADE",
			"LOCALIZE",
			"MATERIAL",
			"MAJORITY",
			"MAINTAIN",
			"MOVEMENT",
			"MAGAZINE",
			"MULTIPLE",
			"MOUNTAIN",
			"MEDICINE",
			"MODELING",
			"MOMENTUM",
			"MODERATE",
			"MIDNIGHT",
			"MOBILITY",
			"MAGNETIC",
			"MAXIMIZE",
			"MEDIEVAL",
			"MINIMIZE",
			"MOLECULE",
			"MARATHON",
			"MOISTURE",
			"METAPHOR",
			"MISTAKEN",
			"MARITIME",
			"MUSICIAN",
			"MANIFEST",
			"MONUMENT",
			"MUSCULAR",
			"MANEUVER",
			"MOTIVATE",
			"METALLIC",
			"MULTIPLY",
			"MYSTICAL",
			"MATTRESS",
			"MUTATION",
			"MECHANIC",
			"MAJESTIC",
			"MUSHROOM",
			"MOSQUITO",
			"MAGICIAN",
			"NUMEROUS",
			"NEGATIVE",
			"NOTEBOOK",
			"NAVIGATE",
			"NOTATION",
			"NECKLACE",
			"NEIGHBOR",
			"NEGATION",
			"OFFERING",
			"ORIGINAL",
			"OFFICIAL",
			"ORDINARY",
			"OPPOSITE",
			"OCCASION",
			"OVERVIEW",
			"OPTIONAL",
			"ORGANIZE",
			"OPTIMISM",
			"OBSTACLE",
			"ORGANISM",
			"OPTIMIZE",
			"OVERLOAD",
			"OVERRIDE",
			"OBLIVION",
			"OVERTURE",
			"ORNAMENT",
			"OINTMENT",
			"OBSIDIAN",
			"POSSIBLE",
			"POSITION",
			"PROPERTY",
			"PERSONAL",
			"PRACTICE",
			"PROBABLY",
			"PREVIOUS",
			"POSITIVE",
			"PURCHASE",
			"PRESSURE",
			"PROGRESS",
			"PHYSICAL",
			"PLATFORM",
			"PRIORITY",
			"PARALLEL",
			"PRINTING",
			"PAINTING",
			"PORTABLE",
			"PERIODIC",
			"PRECIOUS",
			"PLEASANT",
			"PORTRAIT",
			"PERSUADE",
			"PATIENCE",
			"PASSWORD",
			"PARADISE",
			"PREMIERE",
			"PECULIAR",
			"POLISHED",
			"PARTICLE",
			"PAVEMENT",
			"PENTAGON",
			"PLUMBING",
			"PROHIBIT",
			"PROCLAIM",
			"QUESTION",
			"QUANTITY",
			"QUADRANT",
			"QUILTING",
			"QUARRIES",
			"QUARTERS",
			"RESEARCH",
			"REQUIRED",
			"RECEIVED",
			"RESPONSE",
			"REMEMBER",
			"RELEVANT",
			"RESOURCE",
			"RECOVERY",
			"RELIABLE",
			"RELATIVE",
			"REGISTER",
			"REACTION",
			"REPEATED",
			"RESIDENT",
			"RELATION",
			"REVISION",
			"RESTRICT",
			"ROTATION",
			"RETRIEVE",
			"REGULATE",
			"REVERSAL",
			"REGISTRY",
			"REACTIVE",
			"RAILROAD",
			"RELOCATE",
			"REFORMED",
			"READABLE",
			"REFINERY",
			"REDESIGN",
			"RHYTHMIC",
			"RESTRAIN",
			"RECREATE",
			"REDEFINE",
			"RADIATOR",
			"REDIRECT",
			"RATTLING",
			"RESTROOM",
			"REHEARSE",
			"RENOVATE",
			"RECHARGE",
			"REINDEER",
			"RESISTOR",
			"SOFTWARE",
			"STANDARD",
			"SECURITY",
			"SPECIFIC",
			"SOLUTION",
			"STRATEGY",
			"STRENGTH",
			"SEPARATE",
			"STANDING",
			"SLIGHTLY",
			"STRAIGHT",
			"SPEAKING",
			"SCHEDULE",
			"SUPPOSED",
			"SURPRISE",
			"SOMEBODY",
			"STRUGGLE",
			"SHOULDER",
			"SENTENCE",
			"SHIPPING",
			"SEQUENCE",
			"SURVIVAL",
			"SWIMMING",
			"SITUATED",
			"SEASONAL",
			"SHORTAGE",
			"SYNDROME",
			"SCENARIO",
			"SENSIBLE",
			"SUBURBAN",
			"SIMPLIFY",
			"SYMPATHY",
			"SYMBOLIC",
			"SUNSHINE",
			"SHIPMENT",
			"SANDWICH",
			"SHOWCASE",
			"SPELLING",
			"SPINNING",
			"SYMPHONY",
			"SPLENDID",
			"SPECIMEN",
			"SURROUND",
			"SHOCKING",
			"SINGULAR",
			"STIRRING",
			"SIDEWAYS",
			"SKELETON",
			"SIMULATE",
			"SYMMETRY",
			"SLIPPERY",
			"SCRAMBLE",
			"TOGETHER",
			"TRAINING",
			"THINKING",
			"TRANSFER",
			"THOUSAND",
			"TEACHING",
			"TOMORROW",
			"TRACKING",
			"THOROUGH",
			"TRAVELED",
			"TROPICAL",
			"TRIANGLE",
			"TERRIFIC",
			"TREASURE",
			"TRANSMIT",
			"TIMELINE",
			"TEENAGER",
			"TRILLION",
			"TEXTBOOK",
			"TEAMWORK",
			"TROUSERS",
			"THRILLER",
			"THANKFUL",
			"TUTORIAL",
			"TUMBLING",
			"TRAVERSE",
			"TRIMMING",
			"THROTTLE",
			"TEASPOON",
			"TELEPORT",
			"TRANQUIL",
			"TORTOISE",
			"TALISMAN",
			"ULTIMATE",
			"UNIVERSE",
			"UMBRELLA",
			"UPSTAIRS",
			"UPCOMING",
			"UNCOMMON",
			"UNSPOKEN",
			"UNDERDOG",
			"UNLISTED",
			"UNTITLED",
			"UNUSABLE",
			"USERNAME",
			"VALUABLE",
			"VARIABLE",
			"VERTICAL",
			"VELOCITY",
			"VACATION",
			"VIGOROUS",
			"VARIANCE",
			"VITALITY",
			"VOLCANIC",
			"VALIDATE",
			"VINEYARD",
			"VIGILANT",
			"VIRTUOUS",
			"VOCALIST",
			"VOLITION",
			"VILLAGER",
			"VAPORIZE",
			"VANQUISH",
			"VERTICES",
			"WIRELESS",
			"WEIGHTED",
			"WORKSHOP",
			"WILDLIFE",
			"WITHDRAW",
			"WARRANTY",
			"WITHDREW",
			"WORKLOAD",
			"WARDROBE",
			"WRAPPING",
			"WORKABLE",
			"WRECKAGE",
			"WITHHOLD",
			"WONDROUS",
			"WINDMILL",
			"WEARABLE",
			"WEREWOLF",
			"WRITABLE",
			"WINGSPAN",
			"WASHROOM",
			"WISHBONE",
			"XENOLITH",
			"YOURSELF",
			"YIELDING",
			"YEARBOOK",
			"ZUCCHINI"
		}
	};

	// Token: 0x0400002F RID: 47
	private int TPPoints;

	// Token: 0x04000030 RID: 48
	private int UcipherPoints;

	// Token: 0x04000031 RID: 49
	private string[][] pages;

	// Token: 0x04000032 RID: 50
	private string[][] arrowLetters;

	// Token: 0x04000033 RID: 51
	private Color[] chosentextcolors;

	// Token: 0x04000034 RID: 52
	private Material[] chosenscreencolors;

	// Token: 0x04000035 RID: 53
	private int[][] fontsizes;

	// Token: 0x04000036 RID: 54
	private Material[] chosenbackgroundcolors;

	// Token: 0x04000037 RID: 55
	private string answer;

	// Token: 0x04000038 RID: 56
	private int page;

	// Token: 0x04000039 RID: 57
	private bool submitScreen;

	// Token: 0x0400003A RID: 58
	private static int moduleIdCounter = 1;

	// Token: 0x0400003B RID: 59
	private int moduleId;

	// Token: 0x0400003C RID: 60
	private bool moduleSolved;

	// Token: 0x0400003D RID: 61
	public KMSelectable leftArrow;

	// Token: 0x0400003E RID: 62
	public KMSelectable rightArrow;

	// Token: 0x0400003F RID: 63
	public KMSelectable submit;

	// Token: 0x04000040 RID: 64
	public KMSelectable[] keyboard;

	// Token: 0x04000041 RID: 65
	public MeshRenderer background;

	// Token: 0x04000042 RID: 66
	public Material[] backgroundcolors;

	// Token: 0x04000043 RID: 67
	public Material[] screencolors;

	// Token: 0x04000044 RID: 68
	public MeshRenderer[] screens;

	// Token: 0x04000045 RID: 69
	public MeshRenderer[] screentextmat;

	// Token: 0x04000046 RID: 70
	public Font[] fonts;

	// Token: 0x04000047 RID: 71
	public Material[] fontmat;

	// Token: 0x04000048 RID: 72
	private int numpages;

	// Token: 0x04000049 RID: 73
	private Color[] ultchosentextcolors;

	// Token: 0x0400004A RID: 74
	private Material[] ultchosenscreencolors;

	// Token: 0x0400004B RID: 75
	private int[][] ultfontsizes;

	// Token: 0x0400004C RID: 76
	private Material[] ultchosenbackgroundcolors;

	// Token: 0x0400004D RID: 77
	private string[][] ultpages;

	// Token: 0x0400004E RID: 78
	private string ultanswer;

	// Token: 0x0400004F RID: 79
	private string[][] ultarrowLetters;

	// Token: 0x04000050 RID: 80
	private string pinkanswer;

	// Token: 0x04000051 RID: 81
	private int[][] pinkfontsizes;

	// Token: 0x04000052 RID: 82
	private Material[] pinkchosenbackgroundcolors;

	// Token: 0x04000053 RID: 83
	private string[][] pinkpages;

	// Token: 0x04000054 RID: 84
	private Color[] pinkchosentextcolors;

	// Token: 0x04000055 RID: 85
	private Material[] pinkchosenscreencolors;

	// Token: 0x04000056 RID: 86
	private string[][] pinkarrowLetters;

	// Token: 0x04000057 RID: 87
	private string cyananswer;

	// Token: 0x04000058 RID: 88
	private int[][] cyanfontsizes;

	// Token: 0x04000059 RID: 89
	private Material[] cyanchosenbackgroundcolors;

	// Token: 0x0400005A RID: 90
	private string[][] cyanpages;

	// Token: 0x0400005B RID: 91
	private Color[] cyanchosentextcolors;

	// Token: 0x0400005C RID: 92
	private Material[] cyanchosenscreencolors;

	// Token: 0x0400005D RID: 93
	private string[][] cyanarrowLetters;

	// Token: 0x0400005E RID: 94
	private string trueanswer;

	// Token: 0x0400005F RID: 95
	private int[][] truefontsizes;

	// Token: 0x04000060 RID: 96
	private Material[] truechosenbackgroundcolors;

	// Token: 0x04000061 RID: 97
	private string[][] truepages;

	// Token: 0x04000062 RID: 98
	private Color[] truechosentextcolors;

	// Token: 0x04000063 RID: 99
	private Material[] truechosenscreencolors;

	// Token: 0x04000064 RID: 100
	private string[][] truearrowLetters;

	// Token: 0x04000065 RID: 101
	private bool pinkuc;

	// Token: 0x04000066 RID: 102
	private bool pinkcalc;

	// Token: 0x04000067 RID: 103
	private bool cyanuc;

	// Token: 0x04000068 RID: 104
	private bool cyancalc;

	// Token: 0x04000069 RID: 105
	private bool trueuc;

	// Token: 0x0400006A RID: 106
	private bool truecalc;

	// Token: 0x0400006B RID: 107
	private string TwitchHelpMessage = "Move to other screens using !{0} right|left|r|l|. Submit the decrypted word with !{0} submit qwertyuiopasdfghjklzxcvbnm";
}
