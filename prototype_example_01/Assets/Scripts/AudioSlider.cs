using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
	public Slider Slider;
	public AudioMixer Mixer;

	void Update()
	{
		Mixer.SetFloat("MasterVolume", Mathf.Log10(Slider.value) * 20.0f);
	}
}
