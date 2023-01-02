using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoPeDT.Common
{
    [RequireComponent(typeof(Camera))]
    public class BlitToRenderTexture : MonoBehaviour
    {
        public RenderTexture targetRenderTexture;

        private Camera renderCamera;

        private void Start()
        {
            renderCamera = GetComponent<Camera>();
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(renderCamera.activeTexture, targetRenderTexture);
            Graphics.Blit(source, destination);
        }
    }
}
