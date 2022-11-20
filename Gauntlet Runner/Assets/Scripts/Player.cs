using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("References")]
    public GameManager _manager;
    public Material _normalMat;
    public Material _phasedMat;

    [Header("Gameplay")]
    public float _bounds = 3f;
    public float _strafeSpeed = 4f;
    public float _phaseCooldown = 2f;

    Renderer _mesh;
    Collider _collision;
    bool _canPhase = true;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        _collision = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime *
        _strafeSpeed;
        Vector3 position = transform.position;
        position.x += xMove;
        position.x = Mathf.Clamp(position.x, -_bounds, _bounds);
        transform.position = position;

        if (Input.GetButtonDown("Jump") && _canPhase)
        {
            _canPhase = false;
            _mesh.material = _phasedMat;
            _collision.enabled = false;
            Invoke("PhaseIn", _phaseCooldown);
        }
    }

    void PhaseIn()
    {
        _canPhase = true;
        _mesh.material = _normalMat;
        _collision.enabled = true;
    }
}
