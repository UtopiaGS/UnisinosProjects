using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [Range(-.002f, 0.1f)]
    [SerializeField] private float _outlineValue;
    Renderer _renderes;
    // Start is called before the first frame update
    void Start()
    {
        _renderes = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetOutlineShader(float outlineValue) {
        for (int i = 0; i < _renderes.materials.Length; i++)
        {
           _renderes.sharedMaterials[i].SetFloat("_Outline", outlineValue);
        }

    }

    void OnMouseOver()
    {

        SetOutlineShader(_outlineValue);
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        SetOutlineShader(0);
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
