using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        Arena,
        Arena_Caixas,
        Arena_Cercas,
        CharacterSelect,
        GameOver,
        Guerreiros,
        MainMenu,
        Recordes,
        Ranking,
        Multiplayer
    }

    public static void Load (Scene cena) {
        SceneManager.LoadScene(cena.ToString());
    }
}
