using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Switches
{
    public class LightSwitch : MonoBehaviour, IInteractionTarget
    {
        [SerializeField] private LightSwitchSettings _settings;
        [SerializeField] private Light[] _lights;
        [SerializeField] private GameObject[] _activeObjects;
        [SerializeField] private bool _startsTurnedOn;

        private bool _isTurnedOn;

        private void Awake()
        {
            _isTurnedOn = _startsTurnedOn;
            ApplyState();
        }

        public InteractionHint GetHint(InteractionActor actor)
        {
            if (_isTurnedOn)
                return new InteractionHint(_settings.TurnOffInteractionText);

            return new InteractionHint(_settings.TurnOnInteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return HasAnyTarget() &&
                   HasAvailableInteractionText();
        }

        public void Press(InteractionActor actor)
        {
            _isTurnedOn = !_isTurnedOn;
            ApplyState();
        }

        public void Hold(InteractionActor actor, float deltaTime) { }

        public void Release(InteractionActor actor) { }

        private void ApplyState()
        {
            SetLightsState();
            SetActiveObjectsState();
        }

        private void SetLightsState()
        {
            if (_lights == null)
                return;

            foreach (Light light in _lights)
                    light.enabled = _isTurnedOn;
        }

        private void SetActiveObjectsState()
        {
            if (_activeObjects == null)
                return;

            foreach (GameObject activeObject in _activeObjects)
            {
                if (activeObject != null)
                    activeObject.SetActive(_isTurnedOn);
            }
        }

        private bool HasAnyTarget()
        {
            return HasAnyLight() ||
                   HasAnyActiveObject();
        }

        private bool HasAnyLight()
        {
            if (_lights == null)
                return false;

            for (int lightIndex = 0; lightIndex < _lights.Length; lightIndex++)
            {
                if (_lights[lightIndex] != null)
                    return true;
            }

            return false;
        }

        private bool HasAnyActiveObject()
        {
            if (_activeObjects == null)
                return false;

            for (int objectIndex = 0; objectIndex < _activeObjects.Length; objectIndex++)
            {
                if (_activeObjects[objectIndex] != null)
                    return true;
            }

            return false;
        }

        private bool HasAvailableInteractionText()
        {
            if (_isTurnedOn)
                return string.IsNullOrWhiteSpace(_settings.TurnOffInteractionText) == false;

            return string.IsNullOrWhiteSpace(_settings.TurnOnInteractionText) == false;
        }
    }
}
