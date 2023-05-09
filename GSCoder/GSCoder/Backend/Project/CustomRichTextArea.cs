using System;
using Eto.Drawing;
using Eto.Forms;

public class CustomRichTextArea : RichTextArea
{
    public CustomRichTextArea()
    {
        ID = "CustomRichTextArea";
        Wrap = true;
        AcceptsTab = true;
        BackgroundColor = project_infos.main_color;
        TextColor = project_infos.foreground_color;
    }
}