using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class Utilities
    {
        public int RandomNumber(int start,int end)
        {
            Random r = new Random();
            int rInt = r.Next(start, end); //for ints
            
            return rInt;
            
            //double rDouble = r.NextDouble() * range; //for doubles
        }
    }
}