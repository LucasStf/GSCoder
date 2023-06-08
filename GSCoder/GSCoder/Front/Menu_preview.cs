using Eto.Forms;
using Eto.Drawing;

namespace GSCoder.Front
{
    class Menu_preview
    {
        public static Panel Preview()
        {
            Panel panel = new Panel();

            var button = new Button
            {
                Text = "Preview the menu",
            };

            button.Click += (sender, e) =>
            {
                Backend.Project.project.ShowMenuPreview();
            };

            MainForm.drawable.Width = 300;
            MainForm.drawable.Height = 500;

            panel.Content = new StackLayout
            {
                Items =
                {
                    MainForm.drawable,
                    button,
                }
            };

            return panel;
        }
    }
}