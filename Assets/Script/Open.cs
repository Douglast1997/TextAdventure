using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Open")]
public class Open : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        Dictionary<string,string> openDictionary = controller.interactableItems.Open(separatedInputWords);

        if(openDictionary != null)
        {
            controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(openDictionary, separatedInputWords[0], separatedInputWords[1]));
        }
    }
}
