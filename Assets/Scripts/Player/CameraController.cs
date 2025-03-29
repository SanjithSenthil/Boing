using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform Player;
	public float m_speed = 0.1f;
	public Vector2 minCameraPos;
	public Vector2 maxCameraPos;
	public float panDuration = 3f;
	public float returnDuration = 2f;
	public float waitBeforeReturn = 3f;
	public Transform levelBounds;
	Camera mycam;
	private float originalSize;
	private Vector3 originalPosition;
	private bool isPanning = false;

	public void Start()
	{
		mycam = GetComponent<Camera>();
		originalSize = mycam.orthographicSize;
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.C) && !isPanning)
		{
			ShowFullLevel();
		}

		if (Player && !isPanning)
		{
			Vector3 targetPos = Vector3.Lerp(transform.position, Player.position, m_speed);
			targetPos.z = -12;

			float camHeight = mycam.orthographicSize;
			float camWidth = camHeight * mycam.aspect;
			float minX = minCameraPos.x + camWidth;
			float maxX = maxCameraPos.x - camWidth;
			float minY = minCameraPos.y + camHeight;
			float maxY = maxCameraPos.y - camHeight;

			targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
			targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

			transform.position = targetPos;
		}
	}

	public void ShowFullLevel()
	{
		if (levelBounds == null)
		{
			Debug.LogError("Level bounds not assigned!");
			return;
		}

		originalSize = mycam.orthographicSize;
		originalPosition = transform.position;

		// Calculate bounds using Renderer instead of Collider
		Bounds bounds = levelBounds.GetComponent<Renderer>().bounds;
		Vector3 levelCenter = bounds.center;

		float zoomOutSize = CalculateMaxZoom(bounds);

		StartCoroutine(PanAndZoomOut(levelCenter, zoomOutSize));
	}

	IEnumerator PanAndZoomOut(Vector3 levelCenter, float zoomOutSize)
	{
		isPanning = true;
		float elapsedTime = 0;

		// Smoothly pan and zoom out
		while (elapsedTime < panDuration)
		{
			transform.position = Vector3.Lerp(originalPosition, new Vector3(levelCenter.x, levelCenter.y, -12), elapsedTime / panDuration);
			mycam.orthographicSize = Mathf.Lerp(originalSize, zoomOutSize, elapsedTime / panDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.position = new Vector3(levelCenter.x, levelCenter.y, -12);
		mycam.orthographicSize = zoomOutSize;

		yield return new WaitForSeconds(waitBeforeReturn);

		StartCoroutine(ReturnToPlayer());
	}

	IEnumerator ReturnToPlayer()
{
    float elapsedTime = 0;
    float xOffset = 10.7f; // Right shift (positive) or Left shift (negative)
    float yOffset = 3f; // Up shift (positive) or Down shift (negative)

    Bounds bounds = levelBounds.GetComponent<Renderer>().bounds;

    // Smoothly return to player
    while (elapsedTime < returnDuration)
    {
        Vector3 targetPos = new Vector3(Player.position.x + xOffset, Player.position.y + yOffset, -12);

        // Calculate camera size for clamping
        float camHeight = mycam.orthographicSize;
        float camWidth = camHeight * mycam.aspect;

        // Apply boundary clamping to prevent showing background
        float minX = bounds.min.x + camWidth;
        float maxX = bounds.max.x - camWidth;
        float minY = bounds.min.y + camHeight;
        float maxY = bounds.max.y - camHeight;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        // Smooth movement and zoom
        transform.position = Vector3.Lerp(transform.position, targetPos, elapsedTime / returnDuration);
        mycam.orthographicSize = Mathf.Lerp(mycam.orthographicSize, originalSize, elapsedTime / returnDuration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Final adjustment to ensure correct position
    Vector3 finalTargetPos = new Vector3(Player.position.x + xOffset, Player.position.y + yOffset, -12);
    finalTargetPos.x = Mathf.Clamp(finalTargetPos.x, bounds.min.x + mycam.orthographicSize * mycam.aspect, bounds.max.x - mycam.orthographicSize * mycam.aspect);
    finalTargetPos.y = Mathf.Clamp(finalTargetPos.y, bounds.min.y + mycam.orthographicSize, bounds.max.y - mycam.orthographicSize);

    transform.position = finalTargetPos;
    mycam.orthographicSize = originalSize;
    isPanning = false;
}

	float CalculateMaxZoom(Bounds bounds)
	{
		float aspectRatio = mycam.aspect;
		float sizeX = bounds.extents.x / aspectRatio - 1;
		float sizeY = bounds.extents.y - 1;
		return Mathf.Max(sizeX, sizeY);
	}
}
