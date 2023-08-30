using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixLeaderBoardArabic : MonoBehaviour
{
    public GameObject prefab;
    public ArabicFixer arabicFixer;
    public LeaderB leaderB;

    // Start is called before the first frame update
    void Start()
    {
        arabicFixer = prefab.GetComponentInChildren<ArabicFixer>();
    }

    // Update is called once per frame
   public void GetLeaderBoard()
    {
        arabicFixer.Start();
    }
}
