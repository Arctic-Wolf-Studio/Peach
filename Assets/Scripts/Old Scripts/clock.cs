using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class clock : MonoBehaviour
{
    int maxTime;
    public Slider slider;
    //public TextMeshProUGUI display; Clock

    // Start is called before the first frame update
    void Start()
    {
        //display = GetComponent<TextMeshProUGUI>(); Clock
        maxTime = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value <= maxTime) {
            slider.value =  2 * Time.time;
        }
        
        //Clock
        /*if (maxTime >= 0)
            display.text = ((maxTime - Time.time).ToString("F0"));
        if(display.text == 0.ToString())
            gameObject.SetActive(false);*/
    }
}
