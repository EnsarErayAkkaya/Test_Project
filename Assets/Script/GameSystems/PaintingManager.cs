using Project.Player;
using Project.UI;
using System.Collections;
using UnityEngine;

namespace Project.GameSystem
{
    public class PaintingManager : MonoBehaviour
    {
        [SerializeField] private GameObject paintingParent;
        [SerializeField] private RenderTexture canvasTexture;
        [SerializeField] private SwerveInput swerveInput;
        [SerializeField] private GameUI gameUI;
        [SerializeField] private float staisfactoryFillAmount;
        [SerializeField] private float colorSimilarityThreshold;
        [SerializeField] private float paintingTransitionDuration;


        private float fillAmount;
        private Color brushColor = Color.red; //The selected color

        public Color BrushColor => brushColor;
        public RenderTexture CanvasTexture => canvasTexture;
        public SwerveInput SwerveInput => swerveInput;


        private void Start()
        {
            GameManager.instance.onRaceFinished += onGameFinished;
            swerveInput.enabled = false;
        }
        private void onGameFinished()
        {
            StartCoroutine(SetupPainting());
            //start animation
        }
        private IEnumerator SetupPainting()
        {
            paintingParent.SetActive(true);

            yield return new WaitForSeconds(paintingTransitionDuration);

            swerveInput.enabled = true;

            while (fillAmount < staisfactoryFillAmount)
            {
                if(swerveInput.swerveVector != Vector2.zero)
                {
                    // get texture of canvas texture
                    RenderTexture.active = CanvasTexture;
                    Texture2D tex = new Texture2D(CanvasTexture.width, CanvasTexture.height, TextureFormat.RGB24, false);
                    tex.ReadPixels(new Rect(0, 0, CanvasTexture.width, CanvasTexture.height), 0, 0);

                    fillAmount = CalculateFill(tex.GetPixels(), BrushColor, colorSimilarityThreshold);
                    gameUI.SetFillPercentImage(fillAmount);

                    yield return new WaitForSeconds(.5f); // this operations is heavy, for saving performance do this twice every second
                }
                else
                    yield return null;
            }
            GameManager.instance.EndGame();
        }
        //calculate matching calour percent 
        private float CalculateFill(Color[] colors, Color reference, float tolerance)
        {
            Vector3 target = new Vector3 { x = reference.r, y = reference.g, z = reference.b };
            int numHits = 0;
            const float sqrt_3 = 1.73205080757f;
            for (int i = 0; i < colors.Length; i++)
            {
                Vector3 next = new Vector3 { x = colors[i].r, y = colors[i].g, z = colors[i].b };
                float mag = Vector3.Magnitude(target - next) / sqrt_3;
                numHits += mag <= tolerance ? 1 : 0;
            }
            return (float)numHits / (float)colors.Length;
        }
    }
}