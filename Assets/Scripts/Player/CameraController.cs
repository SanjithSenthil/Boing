using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform Player;
	public float m_speed = 0.1f;
	public Vector2 minCameraPos;
    public Vector2 maxCameraPos;
	Camera mycam;

	public void Start()
	{
		mycam = GetComponent<Camera> ();
	}

	public void Update()
	{

	//	mycam.orthographicSize = (Screen.height / 100f) / 0.7f;

		if (Player) 
		{
		
			Vector3 targetPos = Vector3.Lerp(transform.position, Player.position, m_speed);
			targetPos.z = -12;

			//clamp the camera to the level boundaries
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
}
