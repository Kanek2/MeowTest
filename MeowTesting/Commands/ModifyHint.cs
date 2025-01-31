using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features.Extensions;
using Exiled.Permissions.Extensions;
using RemoteAdmin;

namespace MeowTesting.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))] 
    public class ModifyHintCommand : ICommand
    {
        public string Command { get; } = "modifyhint";
        public string[] Aliases { get; } = { "mhint" };
        public string Description { get; } = "Modifies the hint text for a specified duration.";
        private Main pluginInstance = new Main();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 2)
            {
                response = "Usage: modifyhint <text> <duration>";
                return false;
            }

            string newHintContent = string.Join(" ", arguments.Take(arguments.Count - 1));

            if (!float.TryParse(arguments.Last(), out float duration))
            {
                response = "Invalid duration.";
                return false;
            }

            pluginInstance.ModifyHintForAllPlayers(newHintContent, duration);

            response = $"Hint modified to '{newHintContent}' for {duration} seconds.";
            return true;
        }
    }
}