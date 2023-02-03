// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class treeLevelup : MonoBehaviour
// {
//     public int waterCount;
//     public int glueCount;
//     public int rootCount;
//     public int level;
//     public Sprite[] treeSprites;

//     private SpriteRenderer spriteRenderer;
//     private int[] fibo;

//     private void Start()
//     {
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         spriteRenderer.sprite = treeSprites[0];
//         level = 1;
//         GenerateFibo(10);
//     }

//     public void GiveMaterial(string material)
//     {
//         if (material == "Water")
//         {
//             waterCount++;
//         }
//         else if (material == "Glue")
//         {
//             glueCount++;
//         }
//         else if (material == "Root")
//         {
//             rootCount++;
//         }

//         if (waterCount >= fibonacciSequence[level - 1] && glueCount >= fibonacciSequence[level - 1] && rootCount >= fibonacciSequence[level - 1])
//         {
//             level++;
//             spriteRenderer.sprite = treeSprites[level - 1];
//             waterCount = glueCount = rootCount = 0;
//         }
//     }
//     private void GenerateFibo(int length)
//     {
//         fibo = new int[length];
//         fibo[0] = 1;
//         fibo[1] = 1;

//         for (int i = 2; i < length; i++)
//         {
//             fibo[i] = fibo[i - 1] + fibo[i - 2];
//         }
//     }
// }