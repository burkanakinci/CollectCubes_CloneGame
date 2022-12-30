
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class LevelDataCreator : MonoBehaviour
{
    [SerializeField] private GameObject m_CubePrefab;
    [SerializeField] private Transform m_CubeParent;
    [HideInInspector] public string CreatedLevelName;
    [HideInInspector] public bool UseOpponent;
    [HideInInspector] public bool UseTimer;
    [HideInInspector] public int TimeSecondCount;
    [HideInInspector] public int ImageWidth;
    [HideInInspector] public LevelData LevelData;
    [HideInInspector] public Texture2D CreatedLevelTexture;
    private string savePath;

    #region Attributes
    #endregion
    public void SaveLevelData()
    {

        LevelData = ScriptableObject.CreateInstance<LevelData>();

        savePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/LevelData/" + CreatedLevelName + "LevelData.asset");

        LevelData.CubeColors = new List<Pixel>();
        LevelData.ObstaclePositions = new List<Vector3>();
        LevelData.CubeColors.AddRange(m_CubesColor);
        LevelData.UseTimer = UseTimer;
        LevelData.TimeSecondCount = TimeSecondCount;
        LevelData.UseOpponent = UseOpponent;
        LevelData.ImageWidth = ImageWidth;
        m_Obstacles = GameObject.FindGameObjectsWithTag(ObjectTags.OBSTACLE).ToList();
        m_Obstacles.ForEach(x =>
            {
                LevelData.ObstaclePositions.Add(x.transform.position);
            }
            );

        AssetDatabase.CreateAsset(LevelData, savePath);
        AssetDatabase.SaveAssets();
    }

    private List<GameObject> m_Obstacles;
    private List<Pixel> m_CubesColor = new List<Pixel>();
    private Cube m_TempSpawnedCube;
    public void SpawnCubes()
    {
        if (CreatedLevelTexture == null)
        {
            return;
        }

        ClearCubesOnScene();
        m_CubesColor.Clear();

        for (int _width = 0; _width < ImageWidth; _width++)
        {
            for (int _height = 0; _height < ImageWidth; _height++)
            {
                Debug.Log("asdd");
                if (CreatedLevelTexture.GetPixel(_width, _height).a > 0.0f)
                {
                    m_CubesColor.Add(new Pixel(_width, _height, CreatedLevelTexture.GetPixel(_width, _height)));
                    m_CubesColor[m_CubesColor.Count - 1].PixelColor.a = 1.0f;
                    m_TempSpawnedCube = Instantiate(
                        m_CubePrefab,
                        m_CubeParent.position + new Vector3(_width, 0.0f, _height),
                        Quaternion.identity,
                        m_CubeParent
                        ).GetComponent<Cube>();
                    m_TempSpawnedCube.Initialize();
                    m_TempSpawnedCube.SetCubeColor((m_CubesColor[m_CubesColor.Count - 1].PixelColor), false);
                }
            }
        }


    }

    private List<GameObject> m_CubesOnScene;
    public void ClearCubesOnScene()
    {
        m_CubesOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.CUBE).ToList();

        m_CubesOnScene.ForEach(x =>
        {
            DestroyImmediate(x);
        }
        );
    }
}
#endif

