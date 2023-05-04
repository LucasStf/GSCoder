using System.IO;

namespace GSCoder.Backend
{
    class File_Delete
    {
        public static void DeleteFileTabPage(MainForm mainForm, string file_name)
        {
            var tabControl = MainForm.rightPanel.FindChild<Eto.Forms.TabControl>("tabControl");
            //get the tab page by his name
            var tabPage = tabControl.Pages;

            Eto.Forms.TabPage selectedTab = null;
            foreach (var tab in tabPage)
            {
                if (tab.Text == file_name)
                {
                    selectedTab = tab;
                    //rename the tab page
                    tabControl.Pages.Remove(tab);
                }
            }
        }

        public static void DeleteItemTreeGridView(MainForm mainForm, string file_name)
        {
            var treeGridView = MainForm.leftPanel.FindChild<Eto.Forms.TreeGridView>("treeGridView");
            var selectedItem = treeGridView.SelectedItem as Eto.Forms.TreeGridItem;

            if (selectedItem != null)
            {
                MainForm.treeGridItemCollection.Remove(selectedItem);
            }
        }

        public static void DeleteFile(string file_name, string project_path)
        {
            string file_path = project_path + "/" + file_name + ".gsc";
            File.Delete(file_path);
        }
    }
}