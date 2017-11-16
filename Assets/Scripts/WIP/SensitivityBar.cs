using UnityEngine;
using UnityEngine.UI;

public class SensitivityBar : MonoBehaviour {
    
    public Text sensitivityText;
    public MotionController playerControl;

    public void UpdateSensitivityText()
    {
        playerControl.movePercent = GetComponent<Slider>().value;
        sensitivityText.text = playerControl.movePercent.ToString();
    }
}
