﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float vol = 0.7f;
	[Range(0f, 2f)]

	public float pitch = 1f;

	private AudioSource source;

	public void SetSource(AudioSource _source)
	{
		source = _source;
		source.clip = clip;
	}

	public void Play()
	{
		source.Play();
	}
}



public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	[Header("Sound")]
	[SerializeField]
	Sound[] sounds;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		for (int i = 0; i < sounds.Length; i++)
		{

			GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
			_go.transform.SetParent(this.transform);
			sounds[i].SetSource(_go.AddComponent<AudioSource>());	
		}

	}

	public void PlaySound(string _name)
	{
		if (sounds.Length > 0)
		{
			foreach (var _sound in sounds)
			{
				if (_sound.name == _name)
				{
					_sound.Play();
					return;
				}

			}
		}
		
	}


}
