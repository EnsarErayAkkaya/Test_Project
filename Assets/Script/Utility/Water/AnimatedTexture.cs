using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utility
{
    public class AnimatedTexture : MonoBehaviour
    {
        [SerializeField]
        private Vector2 speedVector;
        [SerializeField]
        private Renderer rendererRef;

        private Vector2 currentOffset;

        private void Start()
        {
            currentOffset = rendererRef.material.mainTextureOffset;
        }
        private void Update()
        {
            currentOffset += speedVector * Time.deltaTime;
            rendererRef.material.SetTextureOffset("_MainTex", currentOffset);
        }
    }
}