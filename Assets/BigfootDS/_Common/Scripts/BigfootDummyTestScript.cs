using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigfootDS;

namespace BigfootDS
{
    /// <summary>
    /// Use this script to sandbox & test other scripts, functions and features.
    /// </summary>
    public class BigfootDummyTestScript : MonoBehaviour
    {
        [ContextMenu("DTest D20 Roll")]
        public void DummyTestRollD20()
        {
            BigfootDiceAndCoins.RandomD20();
            BigfootDiceAndCoins.CoinFlip();
            BigfootDiceAndCoins.RandomDiceValue(7, 3);

        }
    }
}