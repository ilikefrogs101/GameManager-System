using System;
using System.Collections;

public partial class GameManager
{
    public string lastLoadedScene;

    public static GameManager Instance;
    private Dictionary<string, GameManagerModule> managerModules = new();

    public override void _Ready()
    {
        Instance ??= this;
        InitImportantModules();
    }

    /// <summary>
    /// Adds modules that will almost always be required
    /// </summary>
    private void InitImportantModules()
    {
        AddModule<SteamManager>();
        AddModule<MenuActionManager>();
        AddModule<MusicManager>();
    }

    /// <summary>
    /// Adds a module to the GameManagerModules dictionary
    /// </summary>
    public void AddModule(GameManagerModule moduleToAdd)
    {
        managerModules.Add(moduleToAdd.GetType().Name, moduleToAdd);
    }

    /// <summary>
    /// Adds a new GameManagerModule of type T to the GameManagerModules dictionary
    /// </summary>
    public void AddModule<T>() where T : GameManagerModule
    {
        GameManagerModule newModule = (T)Activator.CreateInstance(typeof(T));
        managerModules.Add(typeof(T).Name, newModule);
    }

    /// <summary>
    /// Adds a new GameManagerModule of type T to the GameManagerModules dictionary then returns it
    /// </summary>
    public T AddModule<T>() where T : GameManagerModule
    {
        GameManagerModule newModule = (T)Activator.CreateInstance(typeof(T));
        managerModules.Add(typeof(T).Name, newModule);
	return newModule;
    }

    /// <summary>
    /// Removes the module of type T from the GameManagerModules dictionary
    /// </summary>
    public void RemoveModule<T>() where T : GameManagerModule
    {
        managerModules.Remove(typeof(T).Name);
    }

    /// <summary>
    /// Retrieves the type T module from the GameManagerModules dictionary unless it doesn't exist where it will return null
    /// </summary>
    public T GetModule<T>() where T : GameManagerModule
    {
        if(HasModule<T>())
            return (T)managerModules[typeof(T).Name];
        else
        {
            GD.Print($"GameManagerModule of type {typeof(T).Name} not found in modules dictionary");
            return null;
        }
    }

    /// <summary>
    /// Checks if the GameManagerModules dictionary contains a module of type T
    /// </summary>
    public bool HasModule<T>() where T : GameManagerModule
    {
        return managerModules.ContainsKey(typeof(T).Name);
    }
}
