using Eto.Drawing;

class project_infos
{
    public static string name;
    public static string game;
    public static string path;

    public static Color main_color = Color.FromArgb(40, 42, 54);
    public static Color foreground_color = Color.FromArgb(248, 248, 242);
    

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