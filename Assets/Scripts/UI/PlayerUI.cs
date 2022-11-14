using System.Collections.Generic;
using Player;
using UnityEngine;

namespace UI
{
    public class PlayerUI : MonoBehaviour
    {
        private PlayerController _player;
        [SerializeField] private GameObject effectsContainer;
        [SerializeField] private GameObject abilitiesContainer;
        [SerializeField] private FIcon fIconPrefab;

        private const int MaxEffectIcons = 20;
        private List<FIcon> _effectIcons = new(MaxEffectIcons);

        
        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            foreach (var ability in _player.Abilities.AbilityContainers)
                Instantiate(fIconPrefab, abilitiesContainer.transform).AttachData(new FIcon.FIconData(ability.AbilityData.Icon, ability));
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
                fi.SetFill(modifier.RemainingTime);
                fi.SetIcon(modifier.Effect.Icon);
                i++;
            }
            for(; i < MaxEffectIcons; i++)
                _effectIcons[i].gameObject.SetActive(false);
        }
    }
}