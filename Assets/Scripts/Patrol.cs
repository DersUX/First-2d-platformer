using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float Distance;
    [SerializeField] Transform GroundDetection;

    private bool _moovRight = true;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Speed", 1);
    }

    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, Distance);

        if (!groundInfo.collider)
        {
            if (_moovRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _moovRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _moovRight = true;
            }
        }
    }
}
