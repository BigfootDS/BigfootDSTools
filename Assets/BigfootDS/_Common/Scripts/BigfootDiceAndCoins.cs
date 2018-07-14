using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigfootDS;

namespace BigfootDS
{
    /// <summary>
    /// The Bigfoot "Dice And Coins" class handles some quick-to-use functions for dice rolls and coin flips. Twenty-sided dice, D100s, coins, and even custom-sized dice are supported here.
    /// </summary>
    public class BigfootDiceAndCoins
    {
        [ContextMenu("Roll D20")]
        public void NonStaticD20Roll ()
        {
            
            RandomD20();
        }


        /// <summary>
        /// Generate a result from a traditional random 20-sided dice roll.
        /// </summary>
        public static int RandomD20()
        {
            int diceRollResult = Random.Range(1, 21);
            Debug.Log("Rolled a D20 and got a result of " + diceRollResult);
            return diceRollResult;
        }

        /// <summary>
        /// Generate a result from a traditional random 20-sided dice roll. Parameter for positive fudging is available.
        /// </summary>
        /// <param name="maximumFudge">The maximum amount a dice roll can be increased by due to "fudging".</param>
        public static int RandomD20(int maximumFudge)
        {
            int diceRollResult = Random.Range(1, 21) + Random.Range(0, maximumFudge +1);
            Debug.Log("Rolled a D20 and got a result of " + diceRollResult + ". Note: This dice roll was fudged!");
            return diceRollResult;
        }


        /// <summary>
        /// Generate a result from a traditional random 6-sided dice roll.
        /// </summary>
        public static int RandomD6()
        {
            int diceRollResult = Random.Range(1, 7);
            Debug.Log("Rolled a D6 and got a result of " + diceRollResult);
            return diceRollResult;
        }

        /// <summary>
        /// Generate a result from a traditional random 6-sided dice roll. Parameter for positive fudging is available.
        /// </summary>
        /// <param name="maximumFudge">The maximum amount a dice roll can be increased by due to "fudging".</param>
        public static int RandomD6(int maximumFudge)
        {
            int diceRollResult = Random.Range(1, 7) + Random.Range(0, maximumFudge + 1);
            Debug.Log("Rolled a D6 and got a result of " + diceRollResult + ". Note: This dice roll was fudged!");
            return diceRollResult;
        }




        /// <summary>
        /// Generate a result from a traditional random 12-sided dice roll.
        /// </summary>
        public static int RandomD12()
        {
            int diceRollResult = Random.Range(1, 13);
            Debug.Log("Rolled a D12 and got a result of " + diceRollResult);
            return diceRollResult;
        }

        /// <summary>
        /// Generate a result from a traditional random 12-sided dice roll. Parameter for positive fudging is available.
        /// </summary>
        /// <param name="maximumFudge">The maximum amount a dice roll can be increased by due to "fudging".</param>
        public static int RandomD12(int maximumFudge)
        {
            int diceRollResult = Random.Range(1, 13) + Random.Range(0, maximumFudge + 1);
            Debug.Log("Rolled a D12 and got a result of " + diceRollResult + ". Note: This dice roll was fudged!");
            return diceRollResult;
        }


        /// <summary>
        /// Generate a result from a traditional random 10-sided dice roll.
        /// </summary>
        public static int RandomD10()
        {
            int diceRollResult = Random.Range(1, 11);
            Debug.Log("Rolled a D10 and got a result of " + diceRollResult);
            return diceRollResult;
        }

        /// <summary>
        /// Generate a result from a traditional random 10-sided dice roll. Parameter for positive fudging is available.
        /// </summary>
        /// <param name="maximumFudge">The maximum amount a dice roll can be increased by due to "fudging".</param>
        public static int RandomD10(int maximumFudge)
        {
            int diceRollResult = Random.Range(1, 11) + Random.Range(0, maximumFudge + 1);
            Debug.Log("Rolled a D10 and got a result of " + diceRollResult + ". Note: This dice roll was fudged!");
            return diceRollResult;
        }





        /// <summary>
        /// Generate a result from a traditional random 100-sided dice roll.
        /// </summary>
        public static int RandomD100()
        {
            int diceRollResult = Random.Range(1, 101);
            Debug.Log("Rolled a D100 and got a result of " + diceRollResult);
            return diceRollResult;
        }

        /// <summary>
        /// Generate a result from a traditional random 100-sided dice roll. Parameter for positive fudging is available.
        /// </summary>
        /// <param name="maximumFudge">The maximum amount a dice roll can be increased by due to "fudging".</param>
        public static int RandomD100(int maximumFudge)
        {
            int diceRollResult = Random.Range(1, 101) + Random.Range(0, maximumFudge + 1);
            Debug.Log("Rolled a D100 and got a result of " + diceRollResult + ". Note: This dice roll was fudged!");
            return diceRollResult;
        }





        /// <summary>
        /// Generate a result from a traditional random 2-sided dice roll.
        /// </summary>
        public static int RandomD2()
        {
            int diceRollResult = Random.Range(1, 3);
            Debug.Log("Rolled a D2 and got a result of " + diceRollResult);
            return diceRollResult;
        }

        /// <summary>
        /// Generate a result from a traditional random 2-sided dice roll. Parameter for positive fudging is available.
        /// </summary>
        /// <param name="maximumFudge">The maximum amount a dice roll can be increased by due to "fudging".</param>
        public static int RandomD2(int maximumFudge)
        {
            int diceRollResult = Random.Range(1, 3) + Random.Range(0, maximumFudge + 1);
            Debug.Log("Rolled a D2 and got a result of " + diceRollResult + ". Note: This dice roll was fudged!");
            return diceRollResult;
        }


        /// <summary>
        /// Flip a coin! Get "Heads" or "Tails" as the result.
        /// </summary>
        /// <returns></returns>
        public static string CoinFlip ()
        {
            string coinFlipResult = "";
            int coinFlipResultTemp = Random.Range(1, 3);
            if (coinFlipResultTemp == 1)
            {
                coinFlipResult = "Heads";
            } else
            {
                coinFlipResult = "Tails";
            }
            //Debug.Log("Temp coinflip result was " + coinFlipResultTemp); // Making sure the odds are valid.
            Debug.Log("Flipped a coin and got a result of " + coinFlipResult);
            return coinFlipResult;
        }


        /// <summary>
        /// Generate a result from custom X-sided dice roll.
        /// </summary>
        /// <param name="diceSides">Specify the number of sides that your custom dice will have.</param>
        public static int RandomDiceValue(int diceSides)
        {
            int diceRollResult = Random.Range(1, diceSides + 1);
            Debug.Log("Rolled a dice with " + diceSides + " sides and got a result of " + diceRollResult);
            return diceRollResult;
        }

        /// <summary>
        /// Generate a result from custom X-sided dice roll.
        /// </summary>
        /// <param name="diceSides">Specify the number of sides that your custom dice will have.</param>
        /// <param name="maximumFudge">The maximum amount a dice roll can be increased by due to "fudging".</param>
        public static int RandomDiceValue(int diceSides, int maximumFudge)
        {
            int diceRollResult = Random.Range(1, diceSides + 1) + Random.Range(0, maximumFudge + 1);
            Debug.Log("Rolled a dice with " + diceSides + " sides and got a result of " + diceRollResult +". Note: This dice roll was fudged!");
            return diceRollResult;
        }

    }
}
