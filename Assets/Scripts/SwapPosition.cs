using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPosition : MonoBehaviour
{
    [SerializeField] private Reposition reposition;    
    [SerializeField] private GameObject[] gameObjectArray;
    private Vector3[] startPositions;
    private bool[] usedIndexes;

    private void Start()
    {
        usedIndexes = new bool[gameObjectArray.Length];
        startPositions = new Vector3[gameObjectArray.Length];

        //Fill the array with original positions of the GameObjects
        for (int i = 0; i < gameObjectArray.Length; i++)
        {
            startPositions[i] = gameObjectArray[i].transform.position;
        }
    }

    private void Update()
    {
        if (reposition.GetRepositioned())
        {
            for (int i = 0; i < gameObjectArray.Length; i++)
            {
                // Generate a random index
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, gameObjectArray.Length);

                } while (usedIndexes[randomIndex]);

                usedIndexes[randomIndex] = true;
                // Debug.Log($"Generate a randomIndex: {randomIndex}");

                //Gets a random position from one of the elements in the array using the original values
                Vector3 newRandomPosition = startPositions[randomIndex];

                //Assing a random position to the current GameObject
                gameObjectArray[i].transform.position = newRandomPosition;

            }

            //Reset usedIndexes for the next round
            for (int i = 0; i < usedIndexes.Length; i++)
            {
                usedIndexes[i] = false;
            }
            
        }
    }

}
