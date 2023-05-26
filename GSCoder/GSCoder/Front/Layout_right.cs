using Eto.Forms;

namespace GSCoder.Front
{
    class Layout_right : Form    
    {
        public static StackLayout create_layout_right(GSCoder.MainForm form)
        {
            // create layout_right
            var layout_right = new StackLayout
            {
                ID = "layout_right",
                //Spacing = new Size(5, 5),
            };

            //layout_right.VerticalContentAlignment = VerticalAlignment.Stretch;
            //layout_right.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            //responsive layout

            return layout_right;
        }

        public static Panel CreateRightPanel()
        {
            // Créer un TabControl pour afficher les fichiers ouverts
            var tabControl = new TabControl()
            {
                ID = "tabControl",
            };

            // Créer un panneau pour contenir le TabControl
            var panel = new Panel
            {
                ID = "panel_right",
                Content = tabControl
            };

            var Preview = Menu_preview.Preview();

            var panel_preview = new Panel
            {
                ID = "panel_preview",
                Content = Preview
            };

            //create a splitter to put the panel to the left and the panel_preview to the right
            var splitter = new Splitter
            {
                ID = "splitter",
                Panel1 = panel,
                Panel2 = panel_preview,
                Orientation = Orientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1,
                Position = 700,
            };

            var main_panel = new Panel
            {
                ID = "main_panel",
                Content = splitter,
            };

            return main_panel;
        }
    }
}