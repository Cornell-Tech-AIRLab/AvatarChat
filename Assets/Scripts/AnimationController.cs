using System;
using System.Collections;
using System.Collections.Generic;
using ReadyPlayerMe.Core;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private EyeAnimationHandler animateEyes;
    private VoiceHandler animateVoice;
    public AudioSource voiceAudio;

    public void SetAvatar(GameObject avatar) {
        animateEyes = avatar.GetComponentInChildren<EyeAnimationHandler>();
        Debug.Log("Avatar has been set: " + avatar.name);
        if (animateEyes != null) {
            animateEyes.enabled = true;
        }
        LipsAnimation(avatar);
    }
 public void LipsAnimation(GameObject a) 
{
    animateVoice = a.GetComponentInChildren<VoiceHandler>();
    if (animateVoice != null) 
    {
        animateVoice.AudioSource = voiceAudio;
        if (voiceAudio.clip != null)
        {
            voiceAudio.mute = true;  
            voiceAudio.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to the AudioSource.");
        }
    }
    else
    {
        Debug.LogWarning("VoiceHandler not found on avatar.");
    }
}
}
