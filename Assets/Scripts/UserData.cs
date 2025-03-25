using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class UserData
{
        public string id;
        public string pass;
        public string name;
        public int cash;
        public int balance;
        
    public UserData() 
    {

    }
    public UserData(string id, string pass, string name)
    {
            this.id = id;
            this.pass = pass;
            this.name = name;
            this.cash = 100000;
            this.balance = 50000;
          
    }
    
}
