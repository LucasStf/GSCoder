using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;

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
    }
}