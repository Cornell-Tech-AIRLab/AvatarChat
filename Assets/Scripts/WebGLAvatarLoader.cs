using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.UI;
using BodyType = ReadyPlayerMe.Core.BodyType;

namespace ReadyPlayerMe.Samples.WebGLSample
{
    [RequireComponent(typeof(WebFrameHandler))]
    public class WebGLAvatarLoader : MonoBehaviour
    {
        private const string TAG = nameof(WebGLAvatarLoader);
        public GameObject avatar;
        private string avatarUrl = "";
        private WebFrameHandler webFrameHandler;
        public GameObject animationC;
        public GameObject buttonC;


        private void Start()
        {
            webFrameHandler = GetComponent<WebFrameHandler>();
            webFrameHandler.OnAvatarExport += HandleAvatarLoaded;
            webFrameHandler.OnUserSet += HandleUserSet;
            webFrameHandler.OnUserAuthorized += HandleUserAuthorized;
        }

        private void OnAvatarLoadCompleted(object sender, CompletionEventArgs args)
        {
            if (avatar) Destroy(avatar);
            avatar = args.Avatar;
            if (args.Metadata.BodyType == BodyType.HalfBody)
            {
                avatar.transform.position = new Vector3(0, 1, 0);
            }
            buttonC.SetActive(true);

        }

        private void OnAvatarLoadFailed(object sender, FailureEventArgs args)
        {
            SDKLogger.Log(TAG, $"Avatar Load failed with error: {args.Message}");
        }

        public void HandleAvatarLoaded(string newAvatarUrl)
        {
            LoadAvatarFromUrl(newAvatarUrl);
        }

        public void HandleUserSet(string userId)
        {
            SDKLogger.Log(TAG,$"User set: {userId}");
        }

        public void HandleUserAuthorized(string userId)
        {
            SDKLogger.Log(TAG,$"User authorized: {userId}");
        }

        public void LoadAvatarFromUrl(string newAvatarUrl)
        {
            var avatarLoader = new AvatarObjectLoader();
            avatarUrl = newAvatarUrl;
            avatarLoader.OnCompleted += OnAvatarLoadCompleted;
            avatarLoader.OnFailed += OnAvatarLoadFailed;
            avatarLoader.LoadAvatar(avatarUrl);
        }
       public void ApplyAnimations()
{
    if (avatar != null)
    {
        AnimationController animationController = FindObjectOfType<AnimationController>();
        if (animationController != null)
        {
            animationController.SetAvatar(avatar);
        }
        else
        {
            Debug.LogError("AnimationController not found!");
        }
    }
    else
    {
        Debug.LogError("Avatar is null!");
    }
}
    }
}
