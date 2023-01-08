using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager Instance;

    [SerializeField] private AudioSource MainTheme;

    private AudioSource[] Sources;

    private void Awake()
    {
        Sources = GetComponents<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClip(AudioClip _clip)
    {
        for (int i = 0; i < Sources.Length; i++)
        {
            if (!Sources[i].isPlaying)
            {
                Sources[i].clip = _clip;
                Sources[i].Play();
                break;
            }
        }
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
