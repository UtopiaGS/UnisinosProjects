using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [Range(-.002f, 0.1f)]
    [SerializeField] private float _outlineValue;

    [SerializeField] Color OutlineColorTurn;
    [SerializeField] Color OutlineColorTarget;

    Renderer _renderes;
    // Start is called before the first frame update
    void Awake()
    {
        _renderes = GetComponentInChildren<Renderer>();      
    }

    public void SetSelectionColor() {
        SetOutlineShader(_outlineValue, OutlineColorTurn);
    }

    public void SetTargetColor() { }

    public void CleanSelection() {
        SetOutlineShader(0, Color.red);
    }

    void SetOutlineShader(float outlineValue, Color color) {
        for (int i = 0; i < _renderes.materials.Length; i++)
        {
           _renderes.sharedMaterials[i].SetFloat("_Outline", outlineValue);
            _renderes.sharedMaterials[i].SetColor("_OutlineColor", color);
        }

    }

    void OnMouseOver()
    {
        SetOutlineShader(_outlineValue, Color.red);
        //If your mouse hovers over the GameObject with the script attached, output this message
       // Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        SetOutlineShader(0, Color.red);
        //The mouse is no longer hovering over the GameObject so output this message each frame
       // Debug.Log("Mouse is no longer on GameObject.");
    }
}
