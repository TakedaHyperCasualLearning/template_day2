using System.Collections;
using System.Collections.Generic;
using Donuts;
using UnityEngine;

public class GameOverGroup : ComponentGroup<GameOverGroup, GameOverComponent> { }

public class GameOverSystem : AGameSystem, IUpdateSystem
{
    public void OnUpdate(float deltaTime)
    {
        entityManager.Foreach<GameOverGroup>(GameOver, deltaTime);
    }

    private void GameOver(GameOverGroup group, float deltaTime)
    {
        if (!gameStat.isGameOver)
        {
            group.data1.gameOverUI.SetActive(false);
            return;
        }
        group.data1.gameOverUI.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
