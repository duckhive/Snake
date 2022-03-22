using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistManager : MonoBehaviour
{
    public static PlaylistManager Instance;

    public List<AudioClip> tracks;
    public int trackIndex;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        PlayRandomTrack();
    }
    
    public void PlayRandomTrack()
    {
        trackIndex = UnityEngine.Random.Range(0, tracks.Count);
        audioSource.clip = tracks[trackIndex];
        audioSource.Play();
    }
}
