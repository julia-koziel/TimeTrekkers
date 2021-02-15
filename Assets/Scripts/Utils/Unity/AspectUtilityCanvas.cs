using UnityEngine;

/// <summary>
/// Set as only child of canvas, parent of all objects in canvas
/// </summary>
public class AspectUtilityCanvas : MonoBehaviour {
 
	public FloatVariable WantedAspectRatio;
	float wantedAspectRatio;
    RectTransform rectTransform;
 
	void Awake () {
		rectTransform = GetComponent<RectTransform>();
		wantedAspectRatio = WantedAspectRatio.Value;

		SetRect();
	}
 
	void SetRect () {
		float currentAspectRatio = (float)Screen.width / Screen.height;
		// If the current aspect ratio is already approximately equal to the desired aspect ratio,
		// use a full-screen Rect (in case it was set to something else previously)
		if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f) {
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			return;
		}
		// Pillarbox
		if (currentAspectRatio > wantedAspectRatio) {
			float inset = 1.0f - wantedAspectRatio/currentAspectRatio;
            
            rectTransform.anchorMax = new Vector2(1.0f-inset/2, 1.0f);
            rectTransform.anchorMin = new Vector2(inset/2, 0.0f);
		}
		// Letterbox
		else {
			float inset = 1.0f - currentAspectRatio/wantedAspectRatio;

            rectTransform.anchorMax = new Vector2(1.0f, 1.0f-inset/2);
            rectTransform.anchorMin = new Vector2(0.0f, inset/2);
		}

        // TODO check for aspectutilitycamera
	}
}