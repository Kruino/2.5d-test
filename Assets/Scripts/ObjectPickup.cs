using System;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isHolding;
    private Vector3 _initialMousePosition;
    private Vector3 _offset;
    private Camera _camera;
    private float _distToCamera;

    private void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }
 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    _distToCamera = Vector3.Distance(_camera.transform.position, transform.position);

                    _isHolding = true;
                    _rb.isKinematic = true;
                    _rb.velocity = Vector3.zero;
                    _rb.angularVelocity = Vector3.zero;
                    _initialMousePosition = Input.mousePosition;
                    var transform1 = transform;
                    var position = transform1.position;
                    _offset = position - _camera.ScreenToWorldPoint(new Vector3(_initialMousePosition.x, _initialMousePosition.y, _distToCamera));
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && _isHolding)
        {
            
            _isHolding = false;
            _rb.isKinematic = false;
        }

        if (!_isHolding) return;
        
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distToCamera);
        var objPosition = _camera.ScreenToWorldPoint(mousePosition) + _offset;
        _rb.MovePosition(objPosition);
    }
}
