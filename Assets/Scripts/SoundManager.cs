﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	private static SoundManager instance = null;

	public static SoundManager Instance {
		get { return Instance; }
	}

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (gameObject);
	}
}
