using UnityEngine;
using System.Collections;

public class SoundLocator {
	private static SoundManager _soundManager;

	public static SoundManager GetSoundManager()
	{
		return _soundManager;
	}

	public static void Provide(SoundManager service) 
	{
		_soundManager = service;
	}
}
