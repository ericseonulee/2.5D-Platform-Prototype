using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = -40.0f;
    [SerializeField]
    private float _jumpHeight = 3.0f;
    private float _verticalVelocity = 0.0f;
    private bool _doubleJump = true;

    private UIManager _uiManager;
    [SerializeField]
    private int _coins;
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private Transform _respawnPoint;

    // Start is called before the first frame update
    void Start() {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_controller == null) {
            Debug.LogError("Controller is null.");
        }

        if (_uiManager == null) {
            Debug.LogError("UI Manager is null.");
        }

        _uiManager.UpdateLivesDisplay(_lives);
        _uiManager.UpdateLivesDisplay(_coins);
    }   

    // Update is called once per frame
    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = 0.0f;

        if (Input.GetKeyDown(KeyCode.Space) && (_controller.isGrounded || _doubleJump)) {
            vertical = Mathf.Sqrt(2.0f * Mathf.Abs(_gravity) * _jumpHeight);
            if (!_controller.isGrounded) {
                _doubleJump = false;
            }
        }
        else if (!_controller.isGrounded) {
            vertical = _verticalVelocity + (_gravity * Time.deltaTime);
        }
        else {
            vertical = -1.0f;
            _doubleJump = true;
        }

        Vector3 velocity = new Vector3(horizontal * _speed, vertical, 0);

        _verticalVelocity = vertical;
        _controller.Move(velocity * Time.deltaTime);
        if (this.transform.position.y < -10) {
            Damage();
            this.transform.position = _respawnPoint.transform.position;
        }
    }

    public void AddCoins() {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage() {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1) {
            SceneManager.LoadScene(0);
        }
    }
}
