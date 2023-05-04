using System.IO;

namespace GSCoder.Backend
{
    class File_Rename
    {
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

        public static void MoveFile(string project_path, string current_file, string new_name)
        {
            string current_file_path = project_path + "/" + current_file + ".gsc";
            string new_file_path = project_path + "/" + new_name + ".gsc";

            File.Move(current_file_path, new_file_path);
        }
    }
}