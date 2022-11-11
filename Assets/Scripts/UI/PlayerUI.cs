using System;
using Player;
using UnityEngine;

namespace UI
{
    public class PlayerUI : MonoBehaviour
    {
        private PlayerController _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            if (_player) return;
            Debug.LogError("Player not found, UI initialization failed!");
            enabled = false;
        }
    }
}