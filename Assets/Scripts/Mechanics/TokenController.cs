using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class animates all token instances in a scene.
    /// This allows a single update call to animate hundreds of sprite 
    /// animations.
    /// If the tokens property is empty, it will automatically find and load 
    /// all token instances in the scene at runtime.
    /// </summary>
    public class TokenController : MonoBehaviour
    {
        
        

        void Awake()
        {
            //if tokens are empty, find all instances.
            //if tokens are not empty, they've been added at editor time.

            //Register all tokens so they can work with this controller.

        }

    }
}