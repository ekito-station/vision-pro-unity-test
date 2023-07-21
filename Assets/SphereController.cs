using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereController : MonoBehaviour
{
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_VISIONOS
        var current = Touchscreen.current;
        if (current == null)
        {
            Debug.Log("Touchscreen is null.");
            return;
        }

        var touches = Touchscreen.current.touches;

        for (int i = 0; i < touches.Count; i++)
        {
            var touch = touches[i];
            if (touch.tapCount.ReadValue() > 0)
            {
                var pos = touch.position.ReadValue();
                var screenPos = new Vector3(pos.x, pos.y, 5);
                var worldPos = Camera.main.ScreenToWorldPoint(screenPos);
                transform.localPosition = worldPos;

                float worldPosX = worldPos.x;
                audioSource.panStereo = Mathf.Clamp(worldPosX / 5, -1f, 1f);
                Debug.Log("panStereo: " + audioSource.panStereo);
            }
        }
#endif
    }
}
