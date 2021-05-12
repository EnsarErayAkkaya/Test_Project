using Project.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.GameSystem
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private List<BaseCharacterMovement> characters;
        [SerializeField] private Collider startingField;
        [SerializeField] private float startWaitDuration;

        private void Start()
        {
            GameManager.instance.onRaceFinished += StopCharacters;
            GameManager.instance.onGameStart += CallStartEnumeration;
            GameManager.instance.onPlayerFailed += StopCharacters;
        }

        private Vector3 SelectRandomStartingPos()
        {
            float xPos = Random.Range(startingField.bounds.min.x, startingField.bounds.max.x);
            float yPos = startingField.bounds.max.y;
            float zPos = Random.Range(startingField.bounds.min.z, startingField.bounds.max.z);
            return new Vector3(xPos, yPos, zPos);
        }
        public void PlaceCharactersToStartingPoint()
        {
            foreach (BaseCharacterMovement character in characters)
            {
                character.SetPoisiton(SelectRandomStartingPos());
            }
        }
        public void FreeCharacters()
        {
            foreach (BaseCharacterMovement character in characters)
            {
                character.Resume(); // set can move true
            }
        }
        public void StopCharacters()
        {
            foreach (BaseCharacterMovement character in characters)
            {
                character.Stop(); // set can move true
            }
        }

        private void CallStartEnumeration() => StartCoroutine(StartEnumeration());
        private IEnumerator StartEnumeration()
        {
            PlaceCharactersToStartingPoint();
            yield return new WaitForSeconds(startWaitDuration);
            FreeCharacters();
        }
    }
}
