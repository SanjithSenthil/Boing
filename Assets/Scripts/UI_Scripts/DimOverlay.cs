using UnityEngine;

public class DimOverlayController : MonoBehaviour
{
    public GameObject dimOverlay; // Drag your DimOverlay here

    public void ShowDimOverlay(bool show)
    {
        if (dimOverlay != null)
        {
            dimOverlay.SetActive(show);
        }
    }
}
