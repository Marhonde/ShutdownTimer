namespace ShutdownTimer;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        var btn = new TextBox
        {
            Location = new Point
            {
                X = 149,
                Y = 189
            },
            
            Width = 200
        };
        
        Controls.Add(btn);
    }
}