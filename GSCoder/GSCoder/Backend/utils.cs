namespace GSCoder.Backend
{
    class utils
    {
        public static string GetSelectedItemTreeGridView(MainForm mainForm)
        {
            var treeGridView = MainForm.leftPanel.FindChild<Eto.Forms.TreeGridView>("treeGridView");
            var selectedItem = treeGridView.SelectedItem as Eto.Forms.TreeGridItem;

            if (selectedItem != null)
            {
                var file_name = selectedItem.Values[0].ToString();
                return file_name;
            }
            else
            {
                return null;
            }
        }

        public static void RenameItemTreeGridView(MainForm mainForm, string new_name)
        {
            var treeGridView = MainForm.leftPanel.FindChild<Eto.Forms.TreeGridView>("treeGridView");
            var selectedItem = treeGridView.SelectedItem as Eto.Forms.TreeGridItem;

            if (selectedItem != null)
            {
                selectedItem.Values[0] = new_name;
            }
        }

        public static void RenameTabPage(MainForm mainForm, string new_name, string current_file = null)
        {
            var tabControl = MainForm.rightPanel.FindChild<Eto.Forms.TabControl>("tabControl");
            //get the tab page by his name
            var tabPage = tabControl.Pages;

            Eto.Forms.TabPage selectedTab = null;
            foreach (var tab in tabPage)
            {
                if (tab.Text == current_file)
                {
                    selectedTab = tab;
                    //rename the tab page
                    tab.Text = new_name;
                    tab.Focus();
                }
            }

            if (selectedTab != null)
            {
                selectedTab.Text = new_name;
            }
        }
    }
}