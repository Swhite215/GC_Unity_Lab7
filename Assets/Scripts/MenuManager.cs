using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public static bool playMusic = true;
	public static string level = "Level1";

	public Button musicOn;
	public Button musicOff;
	public Button level1;
	public Button level2;
	public Button level3;
	public Button begin;

	public Color onColor = new Color(0.2f, 0.8f, 0.2f, 1.0f);
	public Color offColor = new Color(0.8f, 0.2f, 0.2f, 1.0f);

	public void MusicOn ()
	{
		playMusic = true;
		SetColors();
	}

	public void MusicOff()
	{
		playMusic = false;
		SetColors();
	}

	public void Level1()
	{
		level = "Level1";
		SetColors();
	}

	public void Level2()
	{
		level = "Level2";
		SetColors();
	}

	public void Level3()
	{
		level = "Level3";
		SetColors();
	}

	public void Begin()
	{
		SceneManager.LoadScene(level);
	}

	private void SetColorOn (Button b)
	{
		ColorBlock cb = b.colors;
		cb.normalColor = onColor;
		b.colors = cb;
	}

	private void SetColorOff (Button b)
	{
		ColorBlock cb = b.colors;
		cb.normalColor = offColor;
		b.colors = cb;
	}

	private void SetColors()
	{
		if (playMusic)
		{
			SetColorOn(musicOn);
			SetColorOff(musicOff);
		}
		else
		{
			SetColorOn(musicOff);
			SetColorOff(musicOn);
		}

		switch (level)
		{
		case "Level1":
			SetColorOn(level1);
			SetColorOff(level2);
			break;
		case "Level2":
			SetColorOn(level2);
			SetColorOff(level1);
			break;
		case "Level3":
			SetColorOn (level3);
			SetColorOff (level1);
			SetColorOff (level2);
			break;
		}

		SetColorOn(begin);
	}

	void Start()
	{
		SetColors();
	}
}