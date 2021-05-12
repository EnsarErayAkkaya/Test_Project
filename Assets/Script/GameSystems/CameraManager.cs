using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
namespace Project.GameSystem
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase playerCamera;
        [SerializeField] private CinemachineVirtualCameraBase paintingCamera;
        [SerializeField] private CinemachineVirtualCameraBase paintingTransitionCamera;
        [SerializeField] private float paintingTransitionDuration;
        
        private CinemachineVirtualCameraBase currentCamera;

        private void Awake()
        {
            currentCamera = playerCamera;
        }
        private void Start()
        {
            GameManager.instance.onRaceFinished += onGameFinished;
        }

        /// <summary>
        /// Change to transition camera beetwen player camera and painting camera
        /// </summary>
        public void ChangePaintingTransitionCamera()
        {
            paintingTransitionCamera.Priority = currentCamera.Priority;
            currentCamera.Priority -= 1;
            currentCamera = paintingTransitionCamera;
            Debug.Log("Changed to transition camera");
        }

        public void ChangePaintingCamera()
        {
            paintingCamera.Priority = currentCamera.Priority;
            currentCamera.Priority -= 1;
            currentCamera = paintingCamera;
            Debug.Log("Changed to painting camera");
        }
        public void ChangePlayerCamera()
        {
            if(currentCamera != playerCamera)
            {
                playerCamera.Priority = currentCamera.Priority;
                currentCamera.Priority -= 1;
                currentCamera = paintingCamera;
                Debug.Log("Changed to player camera");
            }
        }
        private void onGameFinished()
        {
            StartCoroutine(EnumerateCamaras());
        }
        private IEnumerator EnumerateCamaras()
        {
            ChangePaintingTransitionCamera();
            yield return new WaitForSeconds(paintingTransitionDuration);
            ChangePaintingCamera();
        }
    }
}