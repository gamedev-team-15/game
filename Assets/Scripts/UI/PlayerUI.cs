using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUI : MonoBehaviour
    {
        private PlayerController _player;
        [SerializeField] private RectTransform effectsContainer;
        [SerializeField] private RectTransform abilitiesContainer;
        [SerializeField] private FIcon fIconPrefab;
        [SerializeField] private Slider healthBar;

        private const int MaxEffectIcons = 20;
        private readonly List<FIcon> _playerAbilities = new();
        private readonly List<FIcon> _effectIcons = new(MaxEffectIcons);

        
        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            foreach (var unused in _player.Abilities.AbilityContainers)
                _playerAbilities.Add(Instantiate(fIconPrefab, abilitiesContainer.transform));
            for(int i = 0; i < MaxEffectIcons; i++)
            {
                var fi = Instantiate(fIconPrefab, effectsContainer.transform);
                fi.gameObject.SetActive(false);
                fi.enabled = false;
                _effectIcons.Add(fi);
            }
            if (_player) return;
            Debug.LogError("Player not found, UI initialization failed!");
            enabled = false;
        }

        private void Update()
        {
            int i = 0;
            foreach (var modifier in _player.Stats.Modifiers)
            {
                if(i >= MaxEffectIcons) break;
                var fi = _effectIcons[i];
                _effectIcons[i].gameObject.SetActive(true);
                fi.FillAmount = modifier.RemainingTime;
                fi.Icon = modifier.Effect.Icon;
                i++;
            }
            for(; i < MaxEffectIcons; i++)
                _effectIcons[i].gameObject.SetActive(false);
            i = 0;
            foreach (var ability in _player.Abilities.AbilityContainers)
            {
                _playerAbilities[i].Icon = ability.AbilityData.Icon;
                _playerAbilities[i].FillAmount = ability.CooldownPercent;
                i++;
            }
            healthBar.value = _player.Health.HealthPercent;
        }
    }
}