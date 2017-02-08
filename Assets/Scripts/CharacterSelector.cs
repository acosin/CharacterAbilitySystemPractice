using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharacterSelector : NetworkBehaviour
{
    public GameObject player;
    public Vector3 playerSpawnPosition = new Vector3(0, 1, -7);
    public Character[] characters;

    public GameObject characterSelectPanel;
    //public GameObject abilityPanel;

    void Start()
    {
        //OnCharacterSelect(1);
    }
    [Client]
    public void ShowSelectMenu(bool show)
    {
        characterSelectPanel.SetActive(show);
    }

    [Client]
    public void OnCharacterSelect(int characterChoice)
    {
        characterSelectPanel.SetActive(false);
        //abilityPanel.SetActive(true);

        player.GetComponent<TeamMarker>().CmdSelectCharacter(characterChoice);

        //GameObject spawnedPlayer = player;//Instantiate(player, playerSpawnPosition, Quaternion.identity) as GameObject;
        //WeaponMarker weaponMarker = spawnedPlayer.GetComponentInChildren<WeaponMarker>();
        //Character selectedCharacter = characters[characterChoice];
        //GameObject model = Instantiate(selectedCharacter.model, spawnedPlayer.transform, false) as GameObject;
        //LayerSetter.SetAllLayer(spawnedPlayer, LayerMask.NameToLayer("A"));//team select
        //for (int i = 0; i < selectedCharacter.characterAbilities.Length; i++)
        //{
        //    Debug.Log(selectedCharacter.characterAbilities[i]);
        //    AbilityCoolDown abilityCoolDown = weaponMarker.gameObject.AddComponent<AbilityCoolDown>();
        //    abilityCoolDown.Initialize(selectedCharacter.characterAbilities[i], selectedCharacter.castAbilityAxisNames[i], weaponMarker.gameObject);
        //}
    }
}