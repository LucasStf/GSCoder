class project_infos
{
    public static string name;
    public static string game;
    public static string path;
    

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