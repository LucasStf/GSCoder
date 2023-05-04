using System.IO;
using Eto.Drawing;
using Eto.Forms;

namespace GSCoder.Front
{
    class Delete_file : Form
    {
        public Delete_file(MainForm form)
        {
            string file_name = Backend.utils.GetSelectedItemTreeGridView(form);

            Title = "Are you sure";
            Padding = new Padding(10);

            var label = new Label
            {
                Text = $"Are you sure you want to delete this file \"{file_name}\" ?",
                Wrap = WrapMode.Word
            };

            var buttonStack = new StackLayout
            {
                Orientation = Orientation.Horizontal,
                Spacing = 5
            };

            var deleteButton = new Button
            {
                Text = "Delete",
                Width = 80
            };
            deleteButton.Click += (sender, e) =>
            {
                Backend.File_Delete.DeleteFile(file_name, controllerProject.get_path());
                Backend.File_Delete.DeleteItemTreeGridView(form, file_name);
                Backend.File_Delete.DeleteFileTabPage(form, file_name);
                Close();
            };

            var cancelButton = new Button
            {
                Text = "Cancel",
                Width = 80
            };

            cancelButton.Click += (sender, e) =>
            {
                Close();
            };

            buttonStack.Items.Add(deleteButton);
            buttonStack.Items.Add(cancelButton);

            var layout = new StackLayout();
            layout.Items.Add(label);
            layout.Items.Add(buttonStack);

            Content = layout;
        }
    }
}