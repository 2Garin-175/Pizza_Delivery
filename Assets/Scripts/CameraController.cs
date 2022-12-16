using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTo;
    [SerializeField] private RectTransform fade;

    private void Start()
    {
        Fade(false);
    }

    private void LateUpdate()
    {
        if(followTo)
            transform.position = new Vector3(Mathf.Lerp(transform.position.x,followTo.position.x + 4f,0.02f), 8.98f, 3.49f);
    }

    public void Fade(bool close )
    {
        StartCoroutine(MakeFade(close));
    }
    public IEnumerator MakeFade(bool close)
    {
        float _xStep = close ? -66.666f : 66.666f;
        float _yStep = close ? -85.714f : 85.714f;

        for (int i = 0; i < 35; i++)
        {
            yield return new WaitForFixedUpdate();
            fade.sizeDelta += new Vector2(_xStep, _yStep);
        }
    }
}
