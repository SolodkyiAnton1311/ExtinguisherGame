using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("Sliders")] [SerializeField] private Slider powderExtiguSlider;
    [SerializeField] private Slider fireSlider;

    [Header("Texts")] [SerializeField] private TextMeshProUGUI hpBarPowder;
    [SerializeField] private TextMeshProUGUI hpBarFire;

    [Header("Sounds")] 
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource powderSound;

    [Header("Tutorial")]
    [SerializeField] private GameObject firstStep;

    [SerializeField] private GameObject secondStep;
    [Header("Views")]
    [SerializeField] private GameObject endGameView;
    [SerializeField] private GameObject gameOverView;
 


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySoundFire()
    {
        fireSound.Play();
    }
    public void StopSoundFire()
    {
        fireSound.Stop();   
    }
    public void PlaySoundPowder()
    {
        powderSound.Play();
    }
    public void StopSoundPowder()
    {
        powderSound.Stop();
    }

    public void UpdateHealthPowder()
    {
        powderExtiguSlider.value = PowderExtinguisher.Instance.health;
        hpBarPowder.text = powderExtiguSlider.value.ToString();
    }

    public void UpdateHealthFire()
    {
        fireSlider.value = Fire.Instance.health;
        hpBarFire.text = fireSlider.value.ToString();
    }

    public void EndTutorial()
    {
        secondStep.SetActive(true);
        firstStep.SetActive(false);
    }
    public void EndGame()
    {
        secondStep.SetActive(false);
        endGameView.SetActive(true);
    }
    public void GameOver()
    {
        gameOverView.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}