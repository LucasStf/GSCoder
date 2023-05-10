using Eto.Drawing;

class project_infos
{
    public static string name;
    public static string game;
    public static string path;

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

class controllerProject
{
    public static string get_name()
    {
        return project_infos.name;
    }
    
    public static string get_game()
    {
        return project_infos.game;
    }

    public static string get_path()
    {
        return project_infos.path;
    }

    public static void set_name(string name)
    {
        project_infos.name = name;
    }

    public static void set_game(string game)
    {
        project_infos.game = game;
    }

    public static void set_path(string path)
    {
        project_infos.path = path;
    }
}

class theme
{
    public static void SetWhiteTheme()
    {
        project_infos.main_color = Color.FromArgb(248, 248, 242);
        project_infos.foreground_color = Color.FromArgb(56, 58, 75, 200);
        project_infos.editor_background_color = Color.FromArgb(248, 248, 242);
        project_infos.editor_lines_color = Color.FromArgb(56, 58, 75, 200);
        project_infos.log_background_color = Color.FromArgb(248, 248, 242);
    }

    public static void SetDraculaTheme()
    {
        project_infos.main_color = Color.FromArgb(68, 71, 90);
        project_infos.foreground_color = Color.FromArgb(248, 248, 242);
        project_infos.editor_background_color = Color.FromArgb(68, 71, 90);
        project_infos.editor_lines_color = Color.FromArgb(56, 58, 75, 200);
        project_infos.log_background_color = Color.FromArgb(68, 71, 90);
    }
}