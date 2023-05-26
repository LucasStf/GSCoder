using Eto.Forms;
using System;

namespace GSCoder.Gtkapp
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new Application(Eto.Platforms.Gtk);
            Gtk.Settings.Default.ApplicationPreferDarkTheme = true;
            
            //load a gtk css file
            Gtk.CssProvider cssProvider = new Gtk.CssProvider();
            cssProvider.LoadFromPath("nordic_theme.css");

            Gtk.StyleContext.AddProviderForScreen(Gdk.Screen.Default, cssProvider, 800);
            app.Run(new MainForm());
        }
    }
}