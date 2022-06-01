using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectsToggler : MonoBehaviour
{
	public Volume PostProcessVolume;
	public BoxBall BoxBall;
	public ParticleSystem BoxBallTrail;
	public TextMeshProUGUI Label;
	public bool EffectsEnabled = true;

	void Update()
	{
		Label.text = "[E] Effects " + (EffectsEnabled ? "Enabled" : "Disabled");

		if (Input.GetKeyDown(KeyCode.E))
		{
			ToggleEffects();
		}
	}

	void ToggleEffects()
	{
		EffectsEnabled = !EffectsEnabled;
		BoxBallTrail.gameObject.SetActive(EffectsEnabled);
		PostProcessVolume.gameObject.SetActive(EffectsEnabled);
		BoxBall.RenderEffects = EffectsEnabled;

	}
}
