using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using HintServiceMeow.Core.Utilities;
using MEC;
using UnityEngine;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace MeowTesting
{
    public class Main : Plugin<Config>
    {
        public override string Name { get; } = "MeowTesting";
        public override string Author { get; } = "Kanekuu";
        
        public override void OnEnabled()
        {
            Log.Info("MeowTesting has been enabled!");
            Exiled.Events.Handlers.Player.Verified += PlayerOnVerified;
        }
        
        public override void OnDisabled()
        {
            Log.Info("MeowTesting has been disabled!");
            Exiled.Events.Handlers.Player.Verified -= PlayerOnVerified;
        }


        public Hint _hudCenterDown;
        
        public void PlayerOnVerified(VerifiedEventArgs ev)
        {
            Timing.CallDelayed(4f, () =>
            {
                PlayerDisplay pd = PlayerDisplay.Get(ev.Player.ReferenceHub);
                _hudCenterDown = new HintServiceMeow.Core.Models.Hints.Hint()
                {
                    Text = string.Empty,
                    YCoordinate = 880,
                    FontSize = 30,
                };
                pd.AddHint(_hudCenterDown);

                
            });
        }

        public void ModifyHintForAllPlayers(string newHintContent, float time)
        {
            foreach (Player player in Player.List)
            {
                PlayerDisplay pd = PlayerDisplay.Get(player.ReferenceHub);
                _hudCenterDown = new HintServiceMeow.Core.Models.Hints.Hint()
                {
                    Text = newHintContent,
                    YCoordinate = 880,
                    FontSize = 30,
                };
                pd.AddHint(_hudCenterDown);
                Timing.RunCoroutine(ShowHintForTime(newHintContent, time));
            }
        }

        private IEnumerator<float> ShowHintForTime(string newHintContent, float Time)
        {
            _hudCenterDown.Text = newHintContent;
            yield return Timing.WaitForSeconds(Time);
            _hudCenterDown.Text = string.Empty;
        }
    }
}