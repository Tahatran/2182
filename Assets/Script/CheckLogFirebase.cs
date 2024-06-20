// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CheckLogFirebase : MonoBehaviour
// {
//     public static CheckLogFirebase instance;
//     public float TimeStart = 0;
//     public float TimeEnd = 0;
//     public float TotalTime = 0;
//     public float TimePause = 0;
//     public int TotalNumberTries = 1;

//     public static CheckLogFirebase Instance
//     {
//         get
//         {
//             if (instance == null)
//             {
//                 instance = FindObjectOfType<CheckLogFirebase>();

//                 if (instance == null)
//                 {
//                     var singletonObject = new GameObject($"Singleton - {nameof(CheckLogFirebase)}");
//                     instance = singletonObject.AddComponent<CheckLogFirebase>();
//                 }
//             }

//             return instance;
//         }
//     }
//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//         }
//         DontDestroyOnLoad(this);
//     }
//     public void TotalTimeMap()
//     {
//         float total = TimeEnd;
//         TotalTime = total - TimeStart;
//     }
//     public void TotalTimePause()
//     {
//         TimePause = Time.time - TimeStart;
//     }
//     // Start is called before the first frame update
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }
// }
