using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Holder : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Transform holderTransform;
     
    [SerializeField] private GameObject PS;
    [SerializeField] private Animator powderAnimation;
    [SerializeField] private LineRenderer _lineRenderer;
  

    private void Awake()
    {
        holderTransform = GetComponent<Transform>();
    }

    void OnMouseDown(){
        screenPoint = Camera.main.WorldToScreenPoint(holderTransform.position);
        offset = holderTransform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        PS.SetActive(true);
        UIManager.Instance.PlaySoundPowder();

        powderAnimation.SetBool("isPressed",true);
    }
		
    void OnMouseDrag(){
        
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        holderTransform.position = cursorPosition;
        holderTransform.rotation = new Quaternion(0f,0f,90f,100f);
        PowderExtinguisher.Instance.health -= 0.005f;
         UIManager.Instance.UpdateHealthPowder();
         
    }

    private void OnMouseUp()
    {
        
        PS.SetActive(false);
        UIManager.Instance.StopSoundPowder();
        powderAnimation.SetBool("isPressed",false);
       
       
    }

    private void Update()
    {
     
        _lineRenderer.SetPosition(2, new Vector3(holderTransform.localPosition.x,holderTransform.localPosition.y,0));
        if (PowderExtinguisher.Instance.health < 0)
        {
           UIManager.Instance.GameOver();
        }
    }

   

  
}
