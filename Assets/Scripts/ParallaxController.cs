using UnityEngine;

public class ParallaxFollowCamera : MonoBehaviour
{
    [Header("Layers")]
    public Transform[] layerRoots;
    public float[] parallaxFactors;

    private Transform cam;
    private Vector3 lastCamPos;
    private float[] spriteWidths;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;

        spriteWidths = new float[layerRoots.Length];

        for (int i = 0; i < layerRoots.Length; i++)
        {
            SpriteRenderer sr = layerRoots[i].GetChild(0).GetComponent<SpriteRenderer>();
            spriteWidths[i] = sr.bounds.size.x;
        }
    }

    void LateUpdate()
    {
        Vector3 camDelta = cam.position - lastCamPos;

        for (int i = 0; i < layerRoots.Length; i++)
        {
            Transform layer = layerRoots[i];
            float factor = parallaxFactors[i];
            float width = spriteWidths[i];
            int count = layer.childCount;

            // 1️⃣ Parallax movement
            layer.position += new Vector3(camDelta.x * factor, 0f, 0f);

            // 2️⃣ Wrap sprites ahead of camera
            for (int j = 0; j < count; j++)
            {
                Transform sprite = layer.GetChild(j);

                // If sprite is far behind camera → move it ahead
                if (sprite.position.x < cam.position.x - width)
                {
                    sprite.position += Vector3.right * width * count;
                }
            }
        }

        lastCamPos = cam.position;
    }
}
