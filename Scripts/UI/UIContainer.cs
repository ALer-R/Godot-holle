using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class UIContainer : Container
{
    [Export] public ContainerType container { get; private set; }
    [Export] public Button ButtonNode { get; private set; }
    [Export] public TextureRect TextureNode { get; private set; }
    [Export] public Label LabelNode { get; private set; }
}
