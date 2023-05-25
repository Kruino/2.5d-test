using UnityEngine;
using System.Collections.Generic;

public class ItemSelector : MonoBehaviour
{
    [System.Serializable]
    public class ItemDrop
    {
        public GameObject prefab;
        public int rarityPercentage;
    }
    
    public static GameObject SelectGameObjectByRarity(List<ItemDrop> itemDrops)
    {
        int totalRarity = 0;
        foreach (ItemDrop itemDrop in itemDrops)
        {
            totalRarity += itemDrop.rarityPercentage;
        }

        int randomNumber = Random.Range(1, totalRarity + 1);

        int cumulativeRarity = 0;
        foreach (ItemDrop itemDrop in itemDrops)
        {
            cumulativeRarity += itemDrop.rarityPercentage;
            if (randomNumber <= cumulativeRarity)
            {
                return itemDrop.prefab;
            }
        }

        // Return null if the list is empty or if something went wrong
        return null;
    }
}