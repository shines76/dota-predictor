using DotaPredictor.Client.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaPredictor.Client.Components
{
    public partial class TeamCard
    {
        public LinkedList<HeroCard>? HeroCards { get; set; }

        [Parameter]
        public Team _team { get; set; }
        [Parameter]
        public EventCallback<EventCallBackArgs> RemoveHeroCardCallBack { get; set; }

        public TeamCard() { }

        protected override void OnInitialized()
        {
            HeroCards = new LinkedList<HeroCard>();
        }

        public void AddHeroCard(Hero hero)
        {
            if (HeroCards.Count < 5)
            {
                HeroCard? heroCard = new HeroCard();
                heroCard.SetHero(hero);

                HeroCards.AddLast(heroCard);
                StateHasChanged();
            }
        }

        public void RemoveHeroCard(HeroCard heroCard)
        {
            if (heroCard != null)
            {
                HeroCards.Remove(heroCard);

            }
        }

        protected async Task OnDelete(TeamCard teamCard, HeroCard heroCard)
        {
            EventCallBackArgs args = new EventCallBackArgs(teamCard, heroCard);
            await RemoveHeroCardCallBack.InvokeAsync(args);
        }
    }

    public class EventCallBackArgs
    {
        public TeamCard? TeamCard { get; set; }
        public HeroCard? HeroCard { get; set; }
        public EventCallBackArgs(TeamCard teamCard, HeroCard heroCard)
        {
            this.TeamCard = teamCard;
            this.HeroCard = heroCard;
        }

    }
}
