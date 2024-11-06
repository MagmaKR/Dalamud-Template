using System;
using System.IO;
using System.Numerics;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using ECommons.Automation;
using ECommons.DalamudServices;
using ImGuiNET;
using static SamplePlugin.Plugin;


namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private string GoatImagePath;
    private Plugin Plugin;
    private int bet;
    private int MinBet = 20000;
    private int MaxBet = 500000;
    private readonly IClientState clientState;
    
  

    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Plugin plugin, string goatImagePath)
        : base("BlackJack Gamba Manager##With a hidden ID", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(400, 300),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        GoatImagePath = goatImagePath;
        Plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.TextColored(ImGuiColors.TankBlue, "Barr-Berry-Nyans Gamba Plugin");
        var spacing = ImGui.GetScrollMaxY() == 0 ? 100f : 100f;
        ImGui.SameLine(ImGui.GetWindowWidth() - spacing);
        if (ImGui.Button("Show Settings"))
        {
            Plugin.ToggleConfigUI();
        }

        ImGui.BeginChild("Players", new Vector2(0, -100), true);
        ImGui.TextColored(ImGuiColors.TankBlue, "Party Members ");

        foreach (var member in Svc.Party)
        {
            if (member.Name.TextValue != Plugin.dealerName)
            {
                ImGui.Text(member.Name.TextValue);
                ImGui.SameLine();
                ImGui.InputInt($"Bet of player {member}", ref bet, 500000);
                ImGui.SameLine();
                ImGui.Text($"Card amount:  cards");
                ImGui.SameLine();
                if (ImGui.Button("Hit", new Vector2(60, 30)))
                {

                    Plugin.Chat.ExecuteCommand("/dice party 13");
                    Plugin.Chat.ExecuteCommand("/dice party 13");
                }

                ImGui.SameLine();
                if (ImGui.Button("Stand", new Vector2(60, 30)))
                {

                    ImGui.Text($"Player:{member} is standing");
                }

                ImGui.SameLine();
                ImGui.Button("Split", new Vector2(60, 30));
                ImGui.SameLine();
                ImGui.Button("DD", new Vector2(60, 30));
            }
            else
            {
                ImGui.Text("No players in party found");
            }
        }

        ImGui.SameLine(ImGui.GetWindowWidth() - 250);  // Adjust width as needed
        ImGui.BeginChild("LeaderboardSection", new (600, 0), true);
        ImGui.Text("Leaderboard:");

        ImGui.EndChild();
        ImGui.EndChild();

        ImGui.BeginChild("Bottom section Dealer", new Vector2(0, 90), true);

        ImGui.BeginChild("DealerSection", new Vector2(ImGui.GetWindowWidth() * 0.7f - 50f, 0), true);
        ImGui.Text("Dealer Section:");
        ImGui.SameLine();
        ImGui.Text(Plugin.dealerName != "" ? Plugin.dealerName : "No Dealer Selected");
        ImGui.Button("Dealer card amount ", new Vector2(250, 30));
        ImGui.SameLine();
        ImGui.Button("Reveal second card", new Vector2(150, 30));
        ImGui.SameLine();
        ImGui.Button("Give winnings", new Vector2(150, 30));
        ImGui.EndChild();
        ImGui.SameLine();
        {
            var BerryDinoPath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "C:\\Users\\Keegan\\Source\\Repos\\Dalamud-Template\\Gamba_BlackJackXPlugin\\Images\\BerryDino-removebg-preview.png");
            var BerryDinoImg = Plugin.TextureProvider.GetFromFile(BerryDinoPath).GetWrapOrDefault();

            var BlackJackPath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "C:\\Users\\Keegan\\Source\\Repos\\Dalamud-Template\\Gamba_BlackJackXPlugin\\Images\\blackJack-removebg-preview.png");
            var BlackJackImg = Plugin.TextureProvider.GetFromFile(BlackJackPath).GetWrapOrDefault();
            if (BerryDinoImg != null)
            {
                ImGui.Image(BerryDinoImg.ImGuiHandle, new Vector2(125, 80));
                ImGui.SameLine();
                ImGui.Image(BlackJackImg.ImGuiHandle, new Vector2(200, 100));
                ImGui.SameLine();
                ImGui.Text("Created by\n MagmaK");
               
            }
            else
            {
                ImGui.Text("Image not found");
            }

        }

       


        ImGui.EndChild();














        /*   if (Svc.Party.Length > 0) // checks if the party is empty or has members 
           {
               ImGui.Text("Party members ");

               foreach (var members in Svc.Party)
               {
                   ImGui.Text(members.Name.TextValue);
                   ImGui.SameLine();

                   //buttons for each player 

               }

           }
           else
           {
               ImGui.Text("No party Members found");
           }


           Plugin.PlayerNameUI.showPlayerNames();

           var spacing = ImGui.GetScrollMaxY() == 0 ? 100f : 100f;
           ImGui.SameLine(ImGui.GetWindowWidth() - spacing);
           if (ImGui.Button("Show Settings"))
           {
               Plugin.ToggleConfigUI();
           }



           ImGui.Spacing();

           PlayerInfo();


       }
       public void PlayerInfo()
       {



           String Text = "Player 1:name";
           ImGui.Text(Text);
           ImGui.SameLine();
           ImGui.NewLine();
           ImGui.PushItemWidth(200f);
               ImGui.InputInt("Bets", ref bet, 0);
               ImGui.PopItemWidth();


           if (bet < MinBet || bet > MaxBet)
               {
                   ImGui.Text($"Bet amount must be between {MinBet} and {MaxBet}.");
                   bet = 0; // Reset bet if it's out of bounds
               }
               else
               {
                   ImGui.Text($"Your bet amount is ({bet})");
               }
               ImGui.SameLine();
               ImGui.TextColored(ImGuiColors.DalamudRed, "Card Amount");
               ImGui.SameLine();

               if (ImGui.Button("Hit"))
               {
                   Plugin.Chat.ExecuteCommand("/dice party 13");
               }
               ImGui.SameLine();

               if (ImGui.Button("Stand"))
               {
                   Plugin.Chat.ExecuteCommand($"/p Player {Plugin.PlayerNameUI.showPlayerNames()}  is standing");
               }
        */
    }
}


