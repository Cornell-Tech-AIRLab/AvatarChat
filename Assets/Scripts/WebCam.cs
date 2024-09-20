using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{

    [SerializeField] private RawImage img = default;
#if UNITY_IOS || UNITY_WEBGL
    private bool CheckPermissionAndRaiseCallbackIfGranted(UserAuthorization authenticationType, Action authenticationGrantedAction)
    {
        if (Application.HasUserAuthorization(authenticationType))
        {
            if (authenticationGrantedAction != null)
                authenticationGrantedAction();

            return true;
        }
        return false;
    }

    private IEnumerator AskForPermissionIfRequired(UserAuthorization authenticationType, Action authenticationGrantedAction)
    {
        if (!CheckPermissionAndRaiseCallbackIfGranted(authenticationType, authenticationGrantedAction))
        {
            yield return Application.RequestUserAuthorization(authenticationType);
            if (!CheckPermissionAndRaiseCallbackIfGranted(authenticationType, authenticationGrantedAction))
                Debug.LogWarning($"Permission {authenticationType} Denied");
        }
    }
#endif

    void Start()
    {
#if UNITY_IOS || UNITY_WEBGL
        StartCoroutine(AskForPermissionIfRequired(UserAuthorization.WebCam, () => { InitializeCamera(); }));
        return;
#endif
        InitializeCamera();
    }

    private void InitializeCamera()
    {
        WebCamTexture webcamTexture = new WebCamTexture();
        if (!webcamTexture.isPlaying) {
            webcamTexture.Play();
        }
        img.texture = webcamTexture;
    }
}