using System.Globalization;
using System.Diagnostics;
using System.Resources;

namespace ShutdownTimer;

public partial class Form1 : Form
{
    private int _clock;

    private double _seconds;

    private readonly TextBox _textBox;
    
    public Form1()
    {
        InitializeComponent();
        var rm = new ResourceManager("ShutdownTimer.Resources.localization.String", typeof(Form1).Assembly);
        
        var label = new Label
        {
            Location = new Point(10, 20),
            Text = rm.GetString("label", CultureInfo.CurrentUICulture),
            Font = new Font("Arial", 10.0f),
            Width = ClientSize.Width - 20,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
        };
        
        _textBox = new TextBox
        {
            Location = new Point(10, 50),
            Width = ClientSize.Width - 20,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Font = new Font("Arial", 16.0f),
            PlaceholderText = rm.GetString("defaultSeconds", CultureInfo.CurrentUICulture)
        };

        var label1 = new Label
        {
            Location = new Point(10, 120),
            Width = ClientSize.Width - 20,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Font = new Font("Arial", 12.0f),
            Text = rm.GetString("unit", CultureInfo.CurrentUICulture)
        };
        
        var panel = new Panel
        {
            Location = new Point(10, 145),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Width = ClientSize.Width - 20,
            Height = 50
        };

        var sec = new Button
        {
            Text = rm.GetString("seconds", CultureInfo.CurrentUICulture),
            Width = panel.Width / 3 - 5,
            Height = 30,
            Location = new Point(5, 5),
            Anchor = AnchorStyles.Left,
            Font = new Font("Arial", 11.0f)
        };
        
        var minutes = new Button
        {
            Text = rm.GetString("minutes", CultureInfo.CurrentUICulture),
            Width = panel.Width / 3 - 5,
            Height = 30,
            Location = new Point(panel.Width / 2 - (panel.Width / 3 - 5) / 2, 5),
            Anchor = AnchorStyles.Left,
            Font = new Font("Arial", 11.0f)
        };
        
        var hours = new Button
        {
            Text = rm.GetString("hours", CultureInfo.CurrentUICulture),
            Width = panel.Width / 3 - 5,
            Height = 30,
            Location = new Point(2 * (panel.Width / 3), 5),
            Anchor = AnchorStyles.Left,
            Font = new Font("Arial", 11.0f)
        };
        
        var btn = new Button
        {
            Location = new Point(ClientSize.Width / 2 - hours.Width / 2, 249),
            Width = hours.Width,
            Height = 40,
            Text = rm.GetString("start", CultureInfo.CurrentUICulture),
            Font = new Font("Arial", 12.0f, FontStyle.Bold),
        };
        
        panel.Controls.Add(sec);
        panel.Controls.Add(minutes);
        panel.Controls.Add(hours);
        
        Controls.Add(label1);
        Controls.Add(panel);
        Controls.Add(_textBox);
        Controls.Add(label);
        
        sec.Click += (_, _) => PanelButtons_Click(0);
        minutes.Click += (_, _) => PanelButtons_Click(1);
        hours.Click += (_, _) => PanelButtons_Click(2);

        _textBox.KeyPress += (sender, e) =>
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            
            if (e.KeyChar is '.' or ',' && 
                ((sender as TextBox)!.Text.Contains('.') || (sender as TextBox)!.Text.Contains(',')))
            {
                e.Handled = true;
            }
        };
        
        btn.Click += (_, _) =>
        {
            _seconds = Math.Round(_seconds = _clock switch
            {
                0 or 1 or 2 when string.IsNullOrWhiteSpace(_textBox.Text) => 0,
                0 when _textBox.Text != "" => double.Parse(_textBox.Text),
                1 => double.Parse(_textBox.Text) * 60,
                2 => double.Parse(_textBox.Text) * 3600,
                _ => _seconds
            }, 0);

            var request = $"timeout /t {_seconds} /nobreak\ncd c:\\\nshutdown /h";

            const string path = "Shutdown.bat";
            
            File.WriteAllText(path, request);

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true,
            });
            
            Close();
        };
        
        Controls.Add(btn);

        ActiveControl = label;
    }

    private void PanelButtons_Click(int param)
    {
        if (string.IsNullOrWhiteSpace(_textBox.Text))
        {
            ChangePlaceholder(_textBox, param);
            _clock = param;
            return;
        }

        if (!double.TryParse(_textBox.Text, out var value))
        {
            ChangePlaceholder(_textBox, param);
            _clock = param;
            return;
        }

        _seconds = _clock switch
        {
            0 => value,
            1 => value * 60,
            2 => value * 3600,
            _ => _seconds
        };
        
        _clock = param;
    }

    private void ChangePlaceholder(TextBox textBox, int param)
    {
        textBox.PlaceholderText = param switch
        {
            0 => "Секунды (По умолчанию 0)",
            1 => "Минуты (По умолчанию 0)",
            2 => "Часы (По умолчанию 0)",
            _ => _textBox.PlaceholderText
        };
    }
}