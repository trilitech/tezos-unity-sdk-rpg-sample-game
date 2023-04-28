
using RMC.Core.Interfaces;
using UnityEngine;

#pragma warning disable CS1998
namespace TezosSDKSamples.RPG.Objects
{
    /// <summary>
    /// Obstacle to block player movement of player character
    /// </summary>
    public class RockObstacle : MonoBehaviour, IIsVisible
    {
        //  Properties ------------------------------------
        
        /// <summary>
        /// Determines visibility
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return gameObject.activeSelf;
            }
            set
            {
                gameObject.SetActive(value);
            }
        }
        
        //  Fields ----------------------------------------
 
        //  Unity Methods  --------------------------------

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------

    }
}