using System.Diagnostics;

namespace ShutdownTimer;

public partial class Shutdown : Form
{
    public Shutdown(double seconds)
    {
        InitializeComponent();
        
        var label = new Label
        {
            Text = string.Format(Resources.localization.String.timeToOff, seconds),
            Location = new Point(ClientSize.Width / 2 - (ClientSize.Width - 20) / 2, ClientSize.Height / 2 - 20),
            AutoSize = true,
            Font = new Font("Arial", 15.0f),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
        };
        
        Controls.Add(label);

        StartCountdown(label, seconds);
    }

    private async void StartCountdown(Label label, double seconds)
    {
            for (var i = seconds; i >= 0 ; i--)
            {
                await Task.Delay(1000);
                label.Text = string.Format(Resources.localization.String.timeToOff, i - 1);
            }

            Process.Start("shutdown", "/h");
            Owner?.Close();
    }
}