using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float health { get; set; }
    private static Fire _instance;
    public static Fire Instance => _instance;
    [SerializeField] private ParticleSystemRenderer _particleSystem;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        health = 100;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Foam")
        {
            health -= 0.5f;
            UIManager.Instance.UpdateHealthFire();
            _particleSystem.minParticleSize = health/500;
            if (health <= 0)
            {
                UIManager.Instance.EndGame();
            }
        }
    }
}
