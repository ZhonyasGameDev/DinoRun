using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateARandomObject : MonoBehaviour
{
    [SerializeField] private Reposition reposition;
    [SerializeField] private GameObject[] gameObjectArray;
    private GameObject lastDesactivatedObject;

    private void Update()
    {
        if (reposition.GetRepositioned())
        {
            // Activates the last deactivated object
            if (lastDesactivatedObject != null)
            {
                lastDesactivatedObject.SetActive(true);
            }

            int randomIndex = Random.Range(0, gameObjectArray.Length);

            // Desactivates a random object of the array 
            gameObjectArray[randomIndex].SetActive(false);

            lastDesactivatedObject = gameObjectArray[randomIndex];
        }


    }
}
