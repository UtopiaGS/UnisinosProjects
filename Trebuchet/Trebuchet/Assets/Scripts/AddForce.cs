using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddForce : MonoBehaviour
{
   
    public Slider slider;
    public Text forceTxt;
   

    public static AddForce Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        forceTxt.text = slider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeForceText() {
        forceTxt.text = slider.value.ToString();
        
    }

  
    public void ApplyForceAndBreak() {
        if (!RoundsController.Instance.CurrentTrebuchet.WasFired)
        {
            RoundsController.Instance.CurrentTrebuchet.ThrowProjectile(slider.value);
          
            Debug.Log("Throwing!");
           
            StartCoroutine(CanvasController.Instance.WaitForEnableNext());
        }
    }
}
