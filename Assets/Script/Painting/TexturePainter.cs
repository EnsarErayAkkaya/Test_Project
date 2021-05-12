using UnityEngine;
using Project.GameSystem;

namespace Project.Painting
{
	public class TexturePainter : MonoBehaviour
	{
		[SerializeField] private PaintingManager paintingManager;
		[SerializeField] private Collider paintingSurface;
		[SerializeField] private GameObject brushCursor; //The cursor that overlaps the model and our container for the brushes painted
		[SerializeField] private GameObject brushEntity;
		[SerializeField] private Transform brushContainer;
		[SerializeField] private Camera canvasCam;  //The camera that looks at the model, and the camera that looks at the canvas.
		[SerializeField] private Material baseMaterial; // The material of our base texture (Were we will save the painted texture)
		[SerializeField] private LayerMask paintLayer;
		[SerializeField] private float brushMoveSpeed;
		[SerializeField] private float brushSize = 1.0f; //The size of our brush

		private int brushCounter = 0;
		private int MAX_BRUSH_COUNT = 500; //To avoid having millions of brushes
		private bool saving = false; //Flag to check if we are saving the texture
		private Vector3 currentBrushPos;
		private void Start()
		{
			currentBrushPos = new Vector3(paintingSurface.bounds.center.x, paintingSurface.bounds.max.y + 1, paintingSurface.bounds.center.z);
		}

		private void Update()
		{
			if (paintingManager.SwerveInput.swerveVector != Vector2.zero)
			{
				MoveBrushCursor();
				Paint();
			}
		}

		// move brush cursor according to input we get from attached SwerveInput script
		private void MoveBrushCursor()
		{
			currentBrushPos += 
				new Vector3(paintingManager.SwerveInput.swerveVector.x, 0, paintingManager.SwerveInput.swerveVector.y).normalized 
				* brushMoveSpeed * Time.deltaTime;

			currentBrushPos.x = Mathf.Clamp(currentBrushPos.x, paintingSurface.bounds.min.x, paintingSurface.bounds.max.x);
			currentBrushPos.z = Mathf.Clamp(currentBrushPos.z, paintingSurface.bounds.min.z, paintingSurface.bounds.max.z);

			brushCursor.transform.position = currentBrushPos;
		}

		//The main action, instantiates a brush entity at the clicked position on the UV map
		void Paint()
		{
			if (saving)
				return;
			Vector3 uvWorldPosition = Vector3.zero;
			if (HitTestUVPosition(ref uvWorldPosition))
			{
				GameObject brushObj = Instantiate(brushEntity, brushContainer); //Paint a brush

				brushObj.GetComponent<SpriteRenderer>().color = paintingManager.BrushColor; //Set the brush color
				brushObj.transform.localPosition = uvWorldPosition; //The position of the brush (in the UVMap)
				brushObj.transform.localScale = Vector3.one * brushSize;//The size of the brush
				brushCounter++;
			}
			//If we reach the max brushes available, flatten the texture and clear the brushes
			if (brushCounter >= MAX_BRUSH_COUNT)
			{
				saving = true;
				SaveTexture();
			}
		}
		// sends ray fromt brush cursor to down and sets uvWorldPosition if it cannot react any mesh or collider returns false
		bool HitTestUVPosition(ref Vector3 uvWorldPosition)
		{
			RaycastHit hit;
			Ray cursorRay = new Ray(brushCursor.transform.position, Vector3.down);

			if (Physics.Raycast(cursorRay, out hit, paintLayer))
			{
				MeshCollider meshCollider = hit.collider as MeshCollider;

				if (meshCollider == null || meshCollider.sharedMesh == null)
					return false;

				Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

				uvWorldPosition.x = pixelUV.x - canvasCam.orthographicSize;//To center the UV on X
				uvWorldPosition.y = pixelUV.y - canvasCam.orthographicSize;//To center the UV on Y
				uvWorldPosition.z = 0.0f;

				return true;
			}
			return false;
		}
		// Saves render texture to base material on our render canvas
		void SaveTexture()
		{
			brushCounter = 0;			
			RenderTexture.active = paintingManager.CanvasTexture;

			Texture2D tex = new Texture2D(paintingManager.CanvasTexture.width, paintingManager.CanvasTexture.height, TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0, 0, paintingManager.CanvasTexture.width, paintingManager.CanvasTexture.height), 0, 0);
			tex.Apply();

			RenderTexture.active = null;

			baseMaterial.mainTexture = tex; //Put the painted texture as the base
			
			foreach (Transform child in brushContainer.transform)// destroy all brush instances
			{
				Destroy(child.gameObject);
			}
			
			saving = false;
		}
	}
}