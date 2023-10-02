using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    [SerializeField] private AudioSource audioSource;
    [field: SerializeField] public AudioClip ButtonClickClip { get; private set; }
    [field: SerializeField] public AudioClip TrueEffectSource { get; private set; }
    [field: SerializeField] public AudioClip FalseEffectSource { get; private set; }
    [field: SerializeField] public AudioClip TimeOverEffectSource { get; private set; }
    [field: SerializeField] public AudioClip NextQuestionSoundEffect { get; private set; }

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Play(AudioClip audioClip)
    {
        
        audioSource.PlayOneShot(audioClip);
    }
}