using System.ComponentModel;

namespace ShutdownTimer;

partial class Shutdown
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // Shutdown
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(589, 161);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Таймер гибернации";
        FormClosed += Shutdown_FormClosed;
        ResumeLayout(false);
    }

    #endregion
}