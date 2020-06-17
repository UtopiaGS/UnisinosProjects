using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SliderPingPong : MonoBehaviour
{
    private Slider _slider;
    private float _isThrowing;

    public static SliderPingPong Instance;

    bool _wasThrow;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _slider = GetComponent<Slider>();
    }

    public void StopPingPong() {
        _slider.DOKill();
        _wasThrow = true;
    }

    public void Reset()
    {
        _wasThrow = false;
        _slider.value = _slider.minValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_wasThrow)
        {
            if (_slider.value == _slider.minValue)
            {
                _slider.DOKill();
                _slider.DOValue(_slider.maxValue, 0.5f);
            }
            else if (_slider.value == _slider.maxValue)
            {

                _slider.DOKill();
                _slider.DOValue(_slider.minValue, 0.5f);
            }
        }
    }
}
