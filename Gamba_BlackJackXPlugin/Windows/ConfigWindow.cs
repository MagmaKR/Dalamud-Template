using System;
using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Windowing;
using ECommons.DalamudServices;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;
    public static string vALUEHIT = string.Empty;
    private static string HitText = string.Empty;
    public static string StandValue = string.Empty;
    public static string DoubleDownValue = string.Empty;
    private static string savedText1, savedText2, savedText3, savedText4 = "";
    public Plugin plugin;
    public string DealerName;
    private string selectedPlayer = "Select a player";




    // We give this window a constant ID using ###
    // This allows for labels being dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public ConfigWindow(Plugin plugin) : base("Settings Menu###With a constant ID")
    {
        Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        Size = new Vector2(470, 350);
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void PreDraw()
    {
        // Flags must be added or removed before Draw() is being called, or they won't apply
        if (Configuration.IsConfigWindowMovable)
        {
            Flags &= ~ImGuiWindowFlags.NoMove;
        }
        else
        {
            Flags |= ImGuiWindowFlags.NoMove;
        }
    }

    public override void Draw()
    {


        ImGui.TextColored(ImGuiColors.DPSRed, "Settings meu");
        ImGui.Dummy(new Vector2(0, 40));
        ImGui.SetNextItemWidth(250f);
        {
            ImGui.InputTextWithHint("Hit emote", "emote done after player hits ", ref vALUEHIT, 20);
        }

        ImGui.SetNextItemWidth(250f);
        {
            ImGui.InputText("Additionall text for hit", ref HitText, 40);
        }

        ImGui.Dummy(new Vector2(0, 20));

        ImGui.SetNextItemWidth(250f);
        {
            ImGui.InputTextWithHint("Stand emote", "emote done after player stands", ref StandValue, 40);

        }
        ImGui.Dummy(new Vector2(0, 20));
        ImGui.SetNextItemWidth(250f);
        {
            ImGui.InputTextWithHint("Double down ", "emote done after player double downs ", ref DoubleDownValue, 50);
        }

        
        {
            DealerMembers();
        }
        Buttons();
    }

    public void Buttons()
    {
        ImGui.SetCursorPos(new Vector2(10, 300));
        if (ImGui.Button("Save"))
        {
            savedText1 = vALUEHIT;
            savedText2 = HitText;
            savedText3 = StandValue;
            savedText4 = DoubleDownValue;
        }

        ImGui.SetCursorPos(new Vector2(70, 300));
        if (ImGui.Button("Reset"))
        {
            vALUEHIT = string.Empty;
            HitText = string.Empty;
            StandValue = string.Empty;
            DoubleDownValue = string.Empty;
        }

        ImGui.SetCursorPos(new Vector2(130, 300));
        if (ImGui.Button("Load"))
        {
            vALUEHIT = savedText1;
            HitText = savedText2;
            StandValue = savedText3;
            DoubleDownValue = savedText4;

        }

    }

    public void DealerMembers()
    {
        var playerNames = new ArrayList();
        
       


        if (Svc.Party.Length > 0) // checks if the party is empty or has members 
        {
            ImGui.Text("Party members ");

            foreach (var members in Svc.Party)
            {
                playerNames.Add(members.Name.TextValue);

            }

        }
        else
        {
            ImGui.Text("No party Members found");
        }

        if (ImGui.BeginCombo("Dealer select", "select a dealer"))
        {

            foreach (string name in playerNames)
            {
                bool isSelected = (selectedPlayer == name);

                if (ImGui.Selectable(name, isSelected))
                {
                    selectedPlayer = name;
                }
                else
                {
                    ImGui.Text("no players have been found");
                }
           

                if (isSelected)
                {
                    ImGui.SetItemDefaultFocus();
                }
            }
            ImGui.EndCombo();
        }
       
    }



}
