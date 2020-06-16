using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class RegisterPlayerRound : MonoBehaviour
{

    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private Button _enterNameButton;
    [SerializeField] private Image _panel;
    [SerializeField] private CanvasGroup _group;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RegisterNameButton() {
        PlayerPrefs.SetString(Constants.CurrentPlayerName, _nameField.text);
        PlayerPrefs.SetInt(Constants.CurrentPlayerScore, 0);
        _group.DOFade(0.0f, 1.0f).OnComplete(() => { _panel.gameObject.SetActive(false); });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
