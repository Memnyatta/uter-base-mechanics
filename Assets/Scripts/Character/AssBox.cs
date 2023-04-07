using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssBox : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _playerBody;

    [SerializeField] Vector3 _scaleDown = new Vector3(1.2f, 0.8f, 1.2f);
    [SerializeField] Vector3 _scaleUP = new Vector3(0.8f, 1.2f, 0.8f);

    [SerializeField] float _scaleKoefficient;

    private void Update()
    {
        Vector3 relativePosition = _playerTransform.InverseTransformPoint(transform.position);
        float interpolant = relativePosition.y * _scaleKoefficient;
        Vector3 scale = Lerp3(_scaleDown, Vector3.one, _scaleUP, interpolant);
        _playerBody.localScale = scale;
    }

    Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        if (t < 0)
        {
            return Vector3.Lerp(a, b, t + 1f);
        }
        else
        {
            return Vector3.Lerp(b, c, t);
        }
    }
}
