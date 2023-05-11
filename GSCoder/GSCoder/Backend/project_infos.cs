using Eto.Drawing;

class project_infos
{
    public static string name {get; set;}
    public static string game {get; set;}
    public static string path {get; set;}

    public static Color main_color = Colors.GhostWhite;
    public static Color foreground_color = Colors.Black;
    public static Color editor_background_color = Colors.GhostWhite;
    public static Color editor_lines_color = Colors.GhostWhite;
    public static Color log_background_color = Colors.GhostWhite;
    public static Color tabcontrol_background_color = Color.FromArgb(216,222,233, 0);
    
    public project_infos(string name, string game, string path)
    {
        project_infos.name = name;
        project_infos.game = game;
        project_infos.path = path;
    }
}