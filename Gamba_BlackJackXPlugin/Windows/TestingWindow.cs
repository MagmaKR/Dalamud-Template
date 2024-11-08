using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class TestingWindow : Window, IDisposable
{

    public TestingWindow(Plugin plugin) : base("Secondary Window")
    {
        Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        Size = new Vector2(500, 550);
        SizeCondition = ImGuiCond.Always;


    }

    public override void Draw()
    {
        ImGui.Text("Testing window");
    }

    public void Dispose() { }

}
