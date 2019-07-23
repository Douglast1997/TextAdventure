using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Quit")]
public class Quit : InputAction
{
     public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if(separatedInputWords.Length == 1)
        {
            controller.LogStringWithReturn("\nPlease type 'quit yes' to beginner the game or 'quit no' to continue the game");
        }
        else
        {       
            controller.roomNavigation.quitAndBackToStartRoom(controller, separatedInputWords[1]);
        }
    }
}

