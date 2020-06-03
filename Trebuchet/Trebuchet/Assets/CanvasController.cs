using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Button NextBtn;
    public Image PanelFade;
    public static CanvasController Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        DeactivateNextBtn();
    }

    public void DeactivateNextButtonColor() {
        NextBtn.image.DOFade(0.0f, 0.5f).OnComplete(() => DeactivateNextBtn());
    }

    public void DeactivateNextBtn()
    {
        NextBtn.gameObject.SetActive(false);
        PanelFade.gameObject.SetActive(false);
    }
    public IEnumerator WaitForEnableNext()
    {
      
        yield return new WaitForSeconds(3f);
        NextBtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        NextBtn.image.DOFade(1.0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
       
    }

    public IEnumerator FadePanel()
    {
        PanelFade.gameObject.SetActive(true);
        PanelFade.DOFade(1.0f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        NextBtn.gameObject.SetActive(false);
        PanelFade.DOFade(0.0f, 2.5f).OnComplete(() => { PanelFade.gameObject.SetActive(false); });
    }

    public void FadeOutAndFadeIn() {
        StartCoroutine(FadePanel());
    }
}
