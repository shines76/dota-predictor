using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaPredictor.Client.Models
{
    public class User
    {
        public Team? _team { get; set; }
        public User()
        {
            _team = Team.Radiant;
        }
    }
    
}
