using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private Transform _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>().transform;
        }

        private void LateUpdate()
        {
            if (!_player) return;
            // TODO: add camera smoothing
            var pos = _player.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
}
