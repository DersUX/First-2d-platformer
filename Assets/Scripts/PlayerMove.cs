using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private Rigidbody2D _player;
    private bool _isFacingRight = true;

    private Vector2 _force = new Vector2(100, 290);
    private bool _inAir;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float move = Input.GetAxis("Horizontal");

        _animator.SetFloat("Speed", Mathf.Abs(move));

        if (Mathf.Abs(move) == 0 && _animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            _animator.SetTrigger("Stop");
        }

        _player.velocity = new Vector2(move * _speed, _player.velocity.y);

        if (Input.GetKey(KeyCode.Space) && !_inAir)
        {
            _inAir = true;
            _player.AddForce(_force);
        }

        if (move > 0 && !_isFacingRight)
            Flip();
        else if (move < 0 && _isFacingRight)
            Flip();
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            _inAir = false;

        if (collision.gameObject.tag == "Enemy")
        {
            _player.constraints = RigidbodyConstraints2D.FreezePositionX;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}