namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class MatchObject : MonoBehaviour    //Data Field
    {
        [SerializeField]
        private int matchNumber = 0;
    }

    public partial class MatchObject : MonoBehaviour    //Function Field
    {
        public int GetMatchNumber()
        {
            return matchNumber;
        }
    }
}