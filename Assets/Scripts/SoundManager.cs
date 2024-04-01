using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //The sounds manager is in charge to play all the sounds in game

    public static SoundManager Instance { get; private set; }
    private AudioSource audioSource;
    [SerializeField] private AudioClip playerIsJumping;
    [SerializeField] private AudioClip playerDoDie;
    [SerializeField] private AudioClip playerOnGrounded;
    [SerializeField] private AudioClip newScore;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Player.Instance.OnIsJumping += Player_OnIsJumping;
        Player.Instance.OnIsDead += Player_OnIsDie;

        GameManager.Instance.OnNewScore += GameManager_OnNewScore;
        
    }

    private void GameManager_OnNewScore(object sender, System.EventArgs e)
    {
        PlaySound(newScore);
    }

    private void Player_OnIsJumping(object sender, Player.OnIsJumpingEventArgs e)
    {
        if (e.isJumping)
        {
            PlaySound(playerIsJumping);
        }
        else
        {
            PlaySound(playerOnGrounded);
        }
    }
    private void Player_OnIsDie(object sender, System.EventArgs e)
    {
        PlaySound(playerDoDie);
    }

    private void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        // AudioSource.PlayClipAtPoint(audioClip, position);
        audioSource.PlayOneShot(audioClip, volume);
    }


}
