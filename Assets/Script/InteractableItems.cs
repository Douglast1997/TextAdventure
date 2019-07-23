using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> usableItemList;
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    [HideInInspector] public List<string> nounsInRoom = new List<string>();

    
    public Dictionary<string, string> openDictionary = new Dictionary<string, string>();
    [HideInInspector] public List<string> objectInRoom = new List<string>();

    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();

    List<string> nounsInInventory = new List<string>();
    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableObjectInRoom = currentRoom.interactableObjectsInRoom[i];

        if(!nounsInInventory.Contains(interactableObjectInRoom.noun))
        {
            nounsInRoom.Add(interactableObjectInRoom.noun);
            objectInRoom.Add(interactableObjectInRoom.noun);
            return interactableObjectInRoom.description;
        }

        return null;
    }

    public void AddActionResponsesToUseDictionary()
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory [i];

            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(noun);
            if (interactableObjectInInventory == null)
            {
                continue;
            }
            for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];   

                if (interaction.actionResponse == null )
                {
                    continue;
                }    

                if (!useDictionary.ContainsKey(noun))
                {
                    useDictionary.Add(noun, interaction.actionResponse);
                }         
            }

        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if(usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            }
            
        }
        return null;
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You look in your backpack, inside you have:");
        
        if(nounsInInventory.Count == 0)
        {
            controller.LogStringWithReturn("Nothing to use!");
        }
        else
        {
            for (int i = 0; i < nounsInInventory.Count; i++)
            {
                controller.LogStringWithReturn(nounsInInventory[i]);
            }
        }
    }

    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();
        openDictionary.Clear();
        objectInRoom.Clear();
    }

     public Dictionary<string, string> Open (string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];
     
        if(objectInRoom.Contains(noun))
        {
            //nounsInInventory.Add("magic");
            objectInRoom.Remove(noun);
            controller.LogStringWithReturn("You open the " + noun + " there is a Magic wand.");
            //AddActionResponsesToUseDictionary ();
            return openDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to open.");
            return null;
        }
    }

    public Dictionary<string, string> Take (string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];
        if(nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            nounsInRoom.Remove(noun);
            AddActionResponsesToUseDictionary ();
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }
    }

    public void UserItem(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];

        if(nounsInInventory.Contains(nounToUse))
        {
            if(useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if(!actionResult)
                {
                    controller.LogStringWithReturn("Hmm. Nothing happens.");
                }
            }
            else
            {
                controller.LogStringWithReturn("You can't use the "+ nounToUse);
            }
        }
        else
        {
            controller.LogStringWithReturn("There is no " + nounToUse + " in your inventory to use");
        }
    }
}
