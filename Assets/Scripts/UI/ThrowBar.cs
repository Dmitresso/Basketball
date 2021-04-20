using UnityEngine;
using UnityEngine.UI;

public class ThrowBar : MonoBehaviour {
    [SerializeField] private float fillSpeed = 10f;
    
    private Slider slider;
    private bool fillUp;

    [HideInInspector] public float currentValue;

    private void Awake() {
        slider = GetComponentInChildren<Slider>();
    }
    

    private void Update() {
        currentValue = slider.value;
        var i = Time.deltaTime * fillSpeed;
        
        
        if (fillUp) slider.value += i;
        else slider.value -= i;

        if (slider.value >= slider.maxValue) fillUp = false;
        if (slider.value <= slider.minValue) fillUp = true;
    }
}
