using UnityEngine;
using System.Collections;

public class CharacterSelector : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerSpawnPosition = new Vector3(0, 1, -7);
    public Character[] characters;

    public GameObject characterSelectPanel;
    public GameObject abilityPanel;

    void Start()
    {
        OnCharacterSelect(0);
    }

    public void OnCharacterSelect(int characterChoice)
    {
        //characterSelectPanel.SetActive(false);
        //abilityPanel.SetActive(true);
        GameObject spawnedPlayer = Instantiate(player, playerSpawnPosition, Quaternion.identity) as GameObject;
        LayerSetter.SetAllLayer(spawnedPlayer, LayerMask.NameToLayer("A"));
        WeaponMarker weaponMarker = spawnedPlayer.GetComponentInChildren<WeaponMarker>();
        //AbilityCoolDown[] coolDownButtons = spawnedPlayer.GetComponentsInChildren<AbilityCoolDown>();
        Character selectedCharacter = characters[characterChoice];
        for (int i = 0; i < selectedCharacter.characterAbilities.Length; i++)
        {
            Debug.Log(selectedCharacter.characterAbilities[i]);
            //Debug.Log(weaponMarker);
            AbilityCoolDown abilityCoolDown = weaponMarker.gameObject.AddComponent<AbilityCoolDown>();
            abilityCoolDown.abilityButtonAxisName = selectedCharacter.castAbilityAxisNames[i];
            abilityCoolDown.Initialize(selectedCharacter.characterAbilities[i], weaponMarker.gameObject);
        }
    }
}