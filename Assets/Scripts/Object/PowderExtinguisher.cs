using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowderExtinguisher : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Transform powderExtinguisherTransform;
    public float health { get; set; }
    private static PowderExtinguisher _instance;
    public static PowderExtinguisher Instance => _instance;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject key;
    [SerializeField] private Holder _holder;
    private bool isFirstStepDone;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        isFirstStepDone = false;
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        health = 100;
        powderExtinguisherTransform = GetComponent<Transform>();
    }

    void OnMouseDown()
    {
        if (isFirstStepDone)
        {
            screenPoint = Camera.main.WorldToScreenPoint(powderExtinguisherTransform.position);
            offset = powderExtinguisherTransform.position -
                     Camera.main.ScreenToWorldPoint(new Vector3(1, Input.mousePosition.y, screenPoint.z));
        }
        else
        {
            _animator.SetTrigger("FirstInst");
            _holder.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
       
       
    }

    void OnMouseDrag()
    {
        if (isFirstStepDone)
        {
            Vector3 cursorPoint = new Vector3(1, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            powderExtinguisherTransform.position = cursorPosition;
        }
    }

    private void OnMouseUp()
    {
        if (isFirstStepDone)
        {
            if (powderExtinguisherTransform.position.y > 4.5)
            {
                powderExtinguisherTransform.position = new Vector3(-11.82f, 4, 0);
            }
            else if (powderExtinguisherTransform.position.y < -7)
            {
                powderExtinguisherTransform.position = new Vector3(-11.82f, -7, 0);
            }
        }
    }

    private void OnEnable()
    {
        AnimationEventHandler.OnFinish += SetDisactiveKey;
    }

    private void OnDisable()
    {
        AnimationEventHandler.OnFinish -= SetDisactiveKey;
    }

    private void SetDisactiveKey()
    {
        key.SetActive(false);
        isFirstStepDone = true;
        UIManager.Instance.EndTutorial();
    }
}