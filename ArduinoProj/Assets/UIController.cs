using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textField;

    private float _timer;

    [SerializeField, Range(0, 300)] private float _initialTime;

   public UnityEvent _timerFinishedEvent;
    // Start is called before the first frame update
    void Start()
    {
        _timer = _initialTime;

        if (_timerFinishedEvent == null)
            _timerFinishedEvent = new UnityEvent();

        _timerFinishedEvent.AddListener(OnTimerFinish);
    }

    // Update is called once per frame
    void Update()
    {
        
        _timer = Mathf.Max(0, _timer - Time.deltaTime);
        if(_timer==0) _timerFinishedEvent.Invoke();

        _textField.text = Mathf.Floor(_timer / 60).ToString("00") + ":" + Mathf.FloorToInt(_timer % 60).ToString("00");
    }


    void OnTimerFinish() {
        Debug.Log("CALL GAME OVER");
    }

}
