using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _slowIcon;
    [SerializeField] private float _maxMoveSpeed;
    private float _currentMoveSpeed;
    private AIPath _aIPath;

    private void Start()
    {
        _slowIcon.enabled = false;
        _currentMoveSpeed = _maxMoveSpeed;
        _aIPath = GetComponent<AIPath>();
        _aIPath.maxSpeed = _currentMoveSpeed;
    }

    public void Slow(float pcnt)
    {
        if (_currentMoveSpeed != _maxMoveSpeed)
        {
            //prevent slows from stacking
            return;
        }

        //change movement speed
        _currentMoveSpeed = _currentMoveSpeed * (1 - pcnt);
        _aIPath.maxSpeed = _currentMoveSpeed;

        if (_slowIcon != null)
            {
                //show slow effect icon
                _slowIcon.enabled = true;
            }
    }
    public void RemoveSlow()
    {
        //change movement speed
        _currentMoveSpeed = _maxMoveSpeed;
        _aIPath.maxSpeed = _currentMoveSpeed;
        
        if (_slowIcon != null)
            {
                //hide slow effect icon
                _slowIcon.enabled = false;
            }
    }
}
