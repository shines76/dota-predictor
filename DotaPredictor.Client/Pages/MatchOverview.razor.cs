using DotaPredictor.Client.Components;
using DotaPredictor.Client.Models;
using DotaPredictor.Client.Services;
using DotaPredictor.DataBuilder.Models;
using DotaPredictor.DataBuilder.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaPredictor.Client.Pages
{
    public partial class MatchOverview
    {
        [Inject]
        private HeroDetailsService? _HeroDetailsService { get; set; }
        private List<Hero>? Heros { get; set; } = null;
        [Inject]
        private PredictorService? _PredictorService { get; set; }
        private TeamCard? TeamCardRadiant { get; set; }
        private TeamCard? TeamCardDire { get; set; }
        private User _user { get; set; } = new User();
        private readonly string filePath = Path.Combine(System.AppContext.BaseDirectory, "model.zip");
        private IEnumerable<MatchPrediction>? Suggestions;

        protected override async Task OnInitializedAsync()
        {
            var heros = await _HeroDetailsService.GetHeroListAsync();

            Heros = heros;

            await DownloadZipFile("https://github.com/cox5529/dota-predictor/raw/master/api/DotaPredictor.API/model.zip", filePath);

        }



        private void HeroCardClick(string color, Hero hero)
        {
            if (TeamCardRadiant != null && TeamCardDire != null)
            {
                if (color == "green")
                {
                    TeamCardRadiant.AddHeroCard(hero);
                }
                else
                {
                    TeamCardDire.AddHeroCard(hero);
                }

                SetCharacterSuggestions();
            }

        }

        private bool HeroHasBeenAdded(Hero hero)
        {
            if (TeamCardRadiant != null && TeamCardDire != null)
            {
                HeroCard heroCard = new HeroCard();
                heroCard.SetHero(hero);

                if (TeamCardRadiant.HeroCards.Contains(heroCard) || TeamCardDire.HeroCards.Contains(heroCard))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private void HandleHeroRemoved(EventCallBackArgs args)
        {
            if (args != null)
            {
                if (args.TeamCard._team.Equals(Team.Radiant))
                {
                    TeamCardRadiant.RemoveHeroCard(args.HeroCard);
                }
                else
                {
                    TeamCardDire.RemoveHeroCard(args.HeroCard);
                }
                StateHasChanged();
            }
        }

        private void ToggleUserTeam()
        {

            //add logic to update Suggested picks List


            if (_user._team.Equals(Team.Radiant))
            {
                _user._team = Team.Dire;
            }
            else
            {
                _user._team = Team.Radiant;
            }

        }

        private async void SetCharacterSuggestions()
        {
            if (TeamCardRadiant != null && TeamCardDire != null)
            {
                IEnumerable<int> allies;
                IEnumerable<int> enemies;

                if (_user._team.Equals(Team.Radiant))
                {
                    allies = TeamCardRadiant.GetListOfTeamHeros();
                    enemies = TeamCardDire.GetListOfTeamHeros();

                }
                else
                {
                    allies = TeamCardDire.GetListOfTeamHeros();
                    enemies = TeamCardDire.GetListOfTeamHeros();
                }
                _PredictorService.LoadModel(filePath);
                Suggestions = await _PredictorService.PredictHeroSuccesses(allies, enemies);

                StateHasChanged();
            }
        }
        private static async Task DownloadZipFile(string url, string filePath)
        {
            using (var httpClient = new HttpClient())
            {
                var fileContents = await httpClient.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(filePath, fileContents);
            }
        }


    }
}
