using UnityEngine;
using System.Collections;

public class ScoreManager
{
	private static int score;
	private static int highScore;

	public static int GetScore ()
	{
		return score;
	}

	public static void AddScore (int x)
	{
		score += x;
	}

	public static void ResetScore ()
	{
		score = 0;
	}

	public static int GetHighScore ()
	{
		return highScore;
	}

	public static void UpdateHighScore ()
	{
		if (score > highScore) {
			highScore = score;
		}
	}
}
