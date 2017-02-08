using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class TeamMarker : NetworkBehaviour {

    public GameObject canvas;

    public int teamNumber = 0;
    public GameObject firstPersonCharacter;
    public Character[] characters;

    void Start()
    {
        SetLocalPlayerItems(false);
        if (isServer)
        {
            RpcPanelToSelectCharacter();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            SetLocalPlayerItems(false);
        }
    }

    void SetLocalPlayerItems(bool enabled)
    {
        gameObject.GetComponent<CharacterController>().enabled = enabled;
        gameObject.GetComponent<FirstPersonController>().enabled = enabled;
        gameObject.GetComponentInChildren<Camera>().enabled = enabled;
        gameObject.GetComponentInChildren<AudioListener>().enabled = enabled;
    }

    [ClientRpc]
    public void RpcPanelToSelectCharacter()
    {
        if (isLocalPlayer)
        {
            canvas = GameObject.Find("Canvas");
            canvas.GetComponent<CharacterSelector>().player = gameObject;
            canvas.GetComponent<CharacterSelector>().ShowSelectMenu(true);
        }
    }

    [Command]
    public void CmdSelectCharacter(int n)
    {
        RpcSelectCharacter(n);
        SelectCharacter(n);
    }

    [ClientRpc]
    public void RpcSelectCharacter(int n)
    {
        SelectCharacter(n);
    }


    public void SelectCharacter(int n)
    {
        SetLocalPlayerItems(isLocalPlayer);

        WeaponMarker weaponMarker = gameObject.GetComponentInChildren<WeaponMarker>();
        Character selectedCharacter = characters[n];
        GameObject model = Instantiate(selectedCharacter.model, gameObject.transform, false) as GameObject;
        LayerSetter.SetAllLayer(gameObject, LayerMask.NameToLayer("A"));//team select
        for (int i = 0; i < selectedCharacter.characterAbilities.Length; i++)
        {
            Debug.Log(selectedCharacter.characterAbilities[i]);
            AbilityCoolDown abilityCoolDown = weaponMarker.gameObject.AddComponent<AbilityCoolDown>();
            abilityCoolDown.Initialize(selectedCharacter.characterAbilities[i].CreateByCloning(), selectedCharacter.castAbilityAxisNames[i], weaponMarker.gameObject);
        }
    }
}
