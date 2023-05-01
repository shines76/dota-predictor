using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotaPredictor.Client.Models;
using Microsoft.AspNetCore.Components;

namespace DotaPredictor.Client.Components
{
    public partial class HeroCard : IEquatable<HeroCard>
    {

        [Parameter]
        public Hero Hero { get; set; } = default!;
        public bool Equals(HeroCard? other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Hero.Id == other.Hero.Id;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }

            HeroCard? other = obj as HeroCard;
            if (other == null) { return false; }

            return Equals(other);
        }


        public void SetHero(Hero hero)
        {
            Hero = hero;
        }

 
    }
}
