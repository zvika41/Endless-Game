using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float laneDistance; //distance between 2 lanes
    [SerializeField] private float jumpForce;
    
    private float _gravity;
    private CharacterController _characterController;
    private Vector3 _playerDirection;
    private int _currentLane;
    
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _currentLane = 1;
        _gravity = -20;
    }

    private void Update()
    {
        _playerDirection.z = speed;
       
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _playerDirection.y = -1;
            Jump();
        }
        else
        {
            _playerDirection.y += _gravity * Time.deltaTime;
        }
        
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentLane++;
            
            if (_currentLane == 3)
            {
                _currentLane = 2;
            }
        }
        
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentLane--;
        
            if (_currentLane == -1)
            {
                _currentLane = 0;
            }
        }

        Transform currentTransform = transform;
        Vector3 position = currentTransform.position;
        Vector3 playerCurrentPos = position.z * currentTransform.forward + position.y * currentTransform.up;

        if (_currentLane == 0)
        {
            playerCurrentPos += Vector3.left * laneDistance;
        }
        else if (_currentLane == 2)
        {
            playerCurrentPos += Vector3.right * laneDistance;
        }
        
        transform.position = Vector3.Lerp(transform.position, playerCurrentPos, 80 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _characterController.Move(_playerDirection * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _playerDirection.y = jumpForce;
    }
}
