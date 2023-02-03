// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;

// //for Saving
// //1. Need datas for saving
// //2. Data changes to json
// //3. Save json outside

// //for Loading
// //1. Load json
// //2. json to data
// //3. use data

// public class PlayerData
// {
//     //name, level, items
//     public string name;
//     public int level;
//     public int item;
// }

// public class dataManager : MonoBehaviour
// {
//     public static dataManager instance;

//     PlayerData nowPlayer = new PlayerData();

//     string path;
//     string

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//         }
//         else if (instance != this)
//         {
//             Destroy(instance.gameObject);
//         }
//         DontDestroyOnLoad(this.gameObject);
//         path = Application.persistentDataPath;
//     }

//     void Start()
//     {
//         string data = JsonUtility.ToJson(nowPlayer);

//         print(path);

//         //File.WriteAllText(,data);
//     }
// }
