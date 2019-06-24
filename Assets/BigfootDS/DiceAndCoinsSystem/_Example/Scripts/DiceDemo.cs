using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigfootDS;

namespace BigfootDS
{
    /// <summary>
    /// Use this script to sandbox & test other scripts, functions and features.
    /// </summary>

    public class DiceDemo : MonoBehaviour
    {

        bool isCoinFlippingOnUpdate = false;


        public int numOfHeads;
        public int numOfTails;
        public List<string> coinFlipResults = new List<string>();

        [ContextMenu("DTest D20 Roll")]
        public void DummyTestRollD20()
        {
            BigfootDiceAndCoins.RandomD20();
        }

        [ContextMenu("DTest Coin flip")]
        public void DummyCoinFlip()
        {
            BigfootDiceAndCoins.CoinFlip();
        }

        [ContextMenu("DTest D7 Roll")]
        public void DummyRollArbitraryDice()
        {
            BigfootDiceAndCoins.RandomDiceValue(7, 3);
        }


        public void ToggleCoinFlipOnUpdate()
        {
            isCoinFlippingOnUpdate = !isCoinFlippingOnUpdate;
        }

        private void Update()
        {
            if (isCoinFlippingOnUpdate)
            {
                string tempCoinFlipResult = BigfootDiceAndCoins.CoinFlip();
                if (tempCoinFlipResult == "Heads")
                {
                    numOfHeads++;
                }
                else
                {
                    numOfTails++;
                }
                coinFlipResults.Add(tempCoinFlipResult);
            }
        }

    }
}