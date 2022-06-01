using TMPro;
using UnityEngine;

public class BoxBallUI : MonoBehaviour
{
	public BoxBall BoxBall;
	public TextMeshProUGUI Text;
	
	public void Update()
	{
		Text.text = "Bounce #: " + BoxBall.NumBounces +
		            "\nSpeed: " + BoxBall.GetSpeed() + " m/s";
	}
}
