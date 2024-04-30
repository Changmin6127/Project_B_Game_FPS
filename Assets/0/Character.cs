using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public enum Kind { None, Player, Moster }

public partial class Character : NetworkBehaviour  //Data Field
{
    public Kind Kind = Kind.None;
    public bool isDeath = false;
}

public partial class Character : NetworkBehaviour  //Function Field
{

}