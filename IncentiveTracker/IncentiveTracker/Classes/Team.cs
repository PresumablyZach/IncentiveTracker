using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveTracker
{
    public class Team
    {
        private string teamName;
        private int pointVal;
        private List<Person> teamMembers;

        public Team(string name)
        {
            this.pointVal = 0;
            this.teamMembers = new List<Person>();
            this.teamName = name;
        }

        public void AddNewMember (Person newPerson)
        {
            Person addition = new Person(newPerson.propName);
            this.teamMembers.Add(addition);
        }

        public string propName
        {
            get { return teamName; }
            set { teamName = value; }
        }

        public int propPointVal
        {
            get { return pointVal; }
            set { pointVal = value; }
        }

    }
}
