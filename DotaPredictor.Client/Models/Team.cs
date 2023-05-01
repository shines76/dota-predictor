using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaPredictor.Client.Models
{
    public struct Team
    {
        private readonly string _team;

        public static readonly Team Radiant = new Team("Radiant");
        public static readonly Team Dire = new Team("Dire");

        private Team(string team)
        {
            if (team != "Radiant" && team != "Dire")
            {
                throw new ArgumentException($"Invalid team value: {team}. Must be Radiant or Dire.");
            }

            _team = team;
        }




        public override string ToString()
        {
            return _team;
        }

       
    }
}
