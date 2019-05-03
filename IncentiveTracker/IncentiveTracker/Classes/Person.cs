using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveTracker
{
    public class Person
    {
        private string name;
        private int pointVal;

        public Person (string name)
        {
            this.pointVal = 0;
            this.name = name;
        }

        public string propName
        {
            get { return name; }
            set { name = value; }
        }

        public int propPointVal
        {
            get { return pointVal; }
            set { pointVal = value; }
        }

    }

}
