using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;

    public Room startRoom;
    public bool startGame = true;

    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom()
    {
        for(int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionary.Add(currentRoom.exits [i].keyString, currentRoom.exits [i].valueRoom);
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }

    public void quitAndBackToStartRoom(GameController controller, string quitGame)
    {
        if(quitGame == "yes")
        { 
            currentRoom = startRoom;
            startGame = true;
            controller.LogStringWithReturn("You quit the game and back to the beginner");
            controller.DisplayRoomText();
        }
        else if(quitGame == "no")
        {
            controller.LogStringWithReturn("Ok let's continue the game! I'll tell the description of the room again");
            controller.DisplayRoomText();
        }
        else
        {
            controller.LogStringWithReturn("Sorry invalid word, please type 'quit yes' or 'quit no'");
        }
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        if (exitDictionary.ContainsKey(directionNoun))
        {
            if(startGame)
            {
                startRoom = currentRoom;
                startGame = false;
            }

            currentRoom = exitDictionary [directionNoun];
            controller.LogStringWithReturn("You head off to the " + directionNoun);
            controller.DisplayRoomText();
        }
        else
        {
            controller.LogStringWithReturn("There is no path to the " + directionNoun);
        }
    }

    public void ClearExits()
    {
        exitDictionary.Clear ();
    }
}
