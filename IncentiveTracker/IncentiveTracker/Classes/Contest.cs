using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncentiveTracker
{
    public class Contest
    {
        private string contestName;
        private string authorName;
        private int numTeams;
        private List<Team> teams;


        public Contest(string name, string author, int numTeams, List<Team> teams)
        {
            this.contestName = name;
            this.authorName = author;
            this.numTeams = numTeams;
            this.teams = new List<Team>(teams);
        }

        public string propName
        {
            get { return contestName; }
            set { contestName = value; }
        }

        public string propAuthor
        {
            get { return authorName; }
            set { authorName = value; }
        }

        public int propNumTeams
        {
            get { return numTeams; }
            set { numTeams = value; }
        }

        public List<Team> propTeams
        {
            get { return teams; }
            set { teams = new List<Team>(value); }
        }

    }
}
