class project_infos
{
    public string name;
    public string game;
    public string path;
    

    public project_infos(string name, string game, string path)
    {
        this.name = name;
        this.game = game;
        this.path = path;
    }

    public string get_name()
    {
        return this.name;
    }

    public string get_game()
    {
        return this.game;
    }

    public string get_path()
    {
        return this.path;
    }

    public void set_name(string name)
    {
        this.name = name;
    }

    public void set_game(string game)
    {
        this.game = game;
    }

    public void set_path(string path)
    {
        this.path = path;
    }
}