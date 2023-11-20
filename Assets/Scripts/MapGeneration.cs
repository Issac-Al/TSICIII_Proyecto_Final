using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class MapGeneration : MonoBehaviour
{
    public List<GameObject> pieces;
    public List<GameObject> enemies;
    public List<GameObject> chests;
    public int mapSize;
    private Vector3 nextPiecePos = new Vector3 (0.37f, 0, 0.37f);
    private int randomPiece;
    int lastPiece = 3;
    private List<GameObject> corners, map;
    public Transform imageTarget;
    public GameObject player;

    void Start()
    {
        corners = new List<GameObject>();
        map = new List<GameObject>();
        InstantiateBoarders();
        player = Instantiate(player, Vector3.zero, Quaternion.identity, imageTarget);
        //GameObject[][] mapa;
        for (int i = 0; i < mapSize - 2; i++)
        {
            for (int j = 0; j < mapSize - 2; j++)
            {
                if (nextPiecePos == corners[0].transform.position || nextPiecePos == corners[1].transform.position
                    || nextPiecePos == corners[2].transform.position || nextPiecePos == corners[3].transform.position)
                {
                    Debug.Log("Es esquina");
                }
                else
                {
                    randomPiece = Random.Range(0, 8);
                }
                //Debug.Log(nextPiecePos);
                map.Add(Instantiate(pieces[randomPiece], nextPiecePos, Quaternion.identity, imageTarget));
                lastPiece = randomPiece;
                nextPiecePos.x += 0.37f;
            }
            nextPiecePos.z += 0.37f;
            nextPiecePos.x = 0.37f;
        }

        DetectarEncierro();
        InstantiateEnemiesAndChests();
        int bloqueSalida = Random.Range(0, map.Count);
        Transform bloqueSalidaPos = map[bloqueSalida].transform;
        Destroy(map[bloqueSalida]);
        map[bloqueSalida] = Instantiate(pieces[13], bloqueSalidaPos.position, Quaternion.identity, imageTarget);
        

    }

    void InstantiateBoarders()
    {
        corners.Add(Instantiate(pieces[3], new Vector3(0, 0, 0), Quaternion.identity, imageTarget));
        corners.Add(Instantiate(pieces[0], new Vector3((mapSize - 1) * 0.37f, 0, 0), Quaternion.identity, imageTarget));
        corners.Add(Instantiate(pieces[4], new Vector3(0, 0, (mapSize - 1) * 0.37f), Quaternion.identity, imageTarget));
        corners.Add(Instantiate(pieces[1], new Vector3((mapSize - 1) * 0.37f, 0, (mapSize - 1) * 0.37f), Quaternion.identity, imageTarget));
        Vector3 boarderPosX = new Vector3(0.37f, 0, 0);
        Vector3 boarderPosZ = new Vector3(0, 0, 0.37f);
        int randomPieceX = Random.Range(0, 8);
        int randomPieceZ = Random.Range(0, 8);
        corners.Add(Instantiate(pieces[7], boarderPosX, Quaternion.identity, imageTarget));
        boarderPosX.x += 0.37f;
        corners.Add(Instantiate(pieces[5], boarderPosZ, Quaternion.identity, imageTarget));
        boarderPosZ.z += 0.37f;

        for (int j = 0; j < mapSize-2; j++)
        {
            while (randomPieceX == 1 || randomPieceX == 2 || randomPieceX == 4 || randomPieceX == 5 || randomPieceX == 6 || randomPieceX == 8)
            {
                randomPieceX = Random.Range(0, 8);
            }
            randomPieceZ = Random.Range(3, 5);
            corners.Add(Instantiate(pieces[randomPieceX], boarderPosX, Quaternion.identity, imageTarget));
            boarderPosX.x += 0.37f;
            corners.Add(Instantiate(pieces[randomPieceZ], boarderPosZ, Quaternion.identity, imageTarget));
            boarderPosZ.z += 0.37f;
        }

        boarderPosX.x = 0.37f;
        boarderPosX.z = (mapSize - 1) * 0.37f;
        boarderPosZ.z = 0.37f;
        boarderPosZ.x = (mapSize - 1) * 0.37f;

        for (int i = 0; i < mapSize-2; i++)
        {
            randomPieceZ = Random.Range(0, 2);
            while (randomPieceX == 0 || randomPieceX == 2 || randomPieceX == 3 || randomPieceX == 5 || randomPieceX == 7 || randomPieceX == 8)
            {
                randomPieceX = Random.Range(0, 8);
            }
            corners.Add(Instantiate(pieces[6], boarderPosX, Quaternion.identity, imageTarget));
            boarderPosX.x += 0.37f;
            corners.Add(Instantiate(pieces[2], boarderPosZ, Quaternion.identity, imageTarget));
            boarderPosZ.z += 0.37f;
        }

    }

    void DetectarEncierro()
    {
        Debug.Log(map.Count - mapSize + 1);
        for (int i = 0; i < map.Count - mapSize + 1; i++)
        {
            Debug.Log(i);
            switch (map[i].name)
                {
                    case "EsquinaID(Clone)":
                    if (map[i + mapSize - 2].name == "EsquinaII(Clone)" || map[i + mapSize - 2].name == "EsquinaID(Clone)" || map[i + mapSize - 2].name == "EsquinaSI(Clone)" || map[i + mapSize - 2].name == "EsquinaSD(Clone)" || map[i + mapSize - 2].name == "ParedD 1(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 2].transform;
                        Destroy(map[i + mapSize - 2]);
                        map[i + mapSize - 2] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }

                    if (map[i + 1].name == "EsquinaID(Clone)" || map[i + 1].name == "EsquinaSI(Clone)" || map[i + 1].name == "EsquinaII(Clone)")
                    {
                        Transform piecePos = map[i + 1].transform;
                        Destroy(map[i + 1]);
                        map[i + 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
       
                    if (map[i + 1].name == "ParedIz 1(Clone)" || map[i + 1].name == "ParedIn 1(Clone)")
                    {
                        Transform piecePos = map[i + 1].transform;
                        Destroy(map[i + 1]);
                        map[i + 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                  

                    if (map[i + mapSize - 1].name == "EsquinaSI(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 1].transform;
                        Destroy(map[i + mapSize - 1]);
                        map[i + mapSize - 1] = Instantiate(pieces[6], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }


                    if (map[i + mapSize - 2].name == "ParedD 1(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 2].transform;
                        Destroy(map[i + mapSize - 2]);
                        map[i + mapSize - 2] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                    break;

                    case "EsquinaSD(Clone)":
                    if (map[i + 1].name == "EsquinaSI(Clone)" || map[i + 1].name == "EsquinaID(Clone)" || map[i + 1].name == "EsquinaSD(Clone)" || map[i + 1].name == "EsquinaII(Clone)")
                    {
                        Transform piecePos = map[i + 1].transform;
                        Destroy(map[i + 1]);
                        map[i + 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                    
                    if(map[i + 1].name == "ParedD 1(Clone)" || map[i + 1].name == "ParedIz 1(Clone)" || map[i + 1].name == "ParedS 1(Clone)")
                    {
                         Transform piecePos = map[i + 1].transform;
                         Destroy(map[i + 1]);
                         map[i + 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                         Debug.Log("se reemplazo una pieza");
                    }
                       
                    if(map[i + mapSize - 2].name == "EsquinaSD(Clone)" || map[i + mapSize - 2].name == "ParedD 1(Clone)" || map[i + mapSize - 2].name == "EsquinaII(Clone)")
                    {
                          Transform piecePos = map[i + mapSize - 2].transform;
                          Destroy(map[i + mapSize - 2]);
                          map[i + 5] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                          Debug.Log("se reemplazo una pieza");
                    }
                           
                    if(map[i + 1].name == "ParedIn 1(Clone)")
                    {
                          Transform piecePos = map[i].transform;
                          Destroy(map[i]);
                          map[i] = Instantiate(pieces[10], piecePos.position, Quaternion.identity, imageTarget);
                          Debug.Log("se reemplazo una pieza");
                    }
                      
                    break;

                    case "ParedD 1(Clone)":
                    if (map[i + 1].name == "EsquinaID(Clone)" || map[i + 1].name == "EsquinaSD(Clone)" || map[i + 1].name == "EsquinaII(Clone)")
                    {
                        Transform piecePos = map[i + 1].transform;
                        Destroy(map[i + 1]);
                        map[i + 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                    
                    if(map[i + 1].name == "ParedD 1(Clone)")
                    {
                         Transform piecePos = map[i].transform;
                         Destroy(map[i]);
                         map[i] = Instantiate(pieces[9], piecePos.position, Quaternion.identity, imageTarget);
                         Debug.Log("se reemplazo una pieza");
                    }

                    if (map[i + mapSize - 1].name == "ParedIz 1(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 1].transform;
                        Destroy(map[i + mapSize - 1]);
                        map[i + mapSize - 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }

                    if (map[i + 1].name == "ParedIz 1(Clone)")
                    {
                          Transform piecePos2 = map[i + 1].transform;
                          Destroy(map[i + 1]);
                          map[i+1] = Instantiate(pieces[8], piecePos2.position, Quaternion.identity, imageTarget);
                          Debug.Log("se reemplazo una pieza");
                    }
                           
                    if (map[i + mapSize - 2].name == "EsquinaSI(Clone)" || map[i + mapSize - 2].name == "EsquinaSD(Clone)")
                    {
                          Transform piecePos = map[i + mapSize - 2].transform;
                          Destroy(map[i + mapSize - 2]);
                          map[i + mapSize - 2] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                          Debug.Log("se reemplazo una pieza");
                    }
                               
                    if(map[i + mapSize - 2].name == "ParedD 1(Clone)")
                    {
                          Transform piecePos = map[i].transform;
                          Destroy(map[i]);
                          map[i] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                          Debug.Log("se reemplazo una pieza");
                    }
                                 
                    break;

                    case "EsquinaII(Clone)":
                    if (map[i + 1].name == "ParedIn 1(Clone)" || map[i + 1].name == "ParedIz 1(Clone)" || map[i + 1].name == "EsquinaII(Clone)")
                    {
                        Transform piecePos = map[i + 1].transform;
                        Destroy(map[i + 1]);
                        map[i + 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                   
                    if(map[i + 1].name == "EsquinaID(Clone)")
                    {
                        Transform piecePos = map[i + 1].transform;
                        Destroy(map[i + 1]);
                        map[i + 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }

                    if (map[i + (mapSize - 3)].name == "EsquinaII(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 3].transform;
                        Destroy(map[i + mapSize - 3]);
                        map[i + mapSize - 3] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }

                    if (map[i + mapSize - 2].name == "EsquinaII(Clone)" || map[i + mapSize - 2].name == "EsquinaID(Clone)" || map[i + mapSize - 2].name == "EsquinaSD(Clone)" || map[i + mapSize - 2].name == "ParedIn 1(Clone)" || map[i + mapSize - 2].name == "EsquinaID(Clone)" || map[i + mapSize - 2].name == "ParedIz 1(Clone)" || map[i + mapSize - 2].name == "EsquinaSI(Clone)")
                    {
                         Transform piecePos = map[i + mapSize - 2].transform;
                         Destroy(map[i + mapSize - 2]);
                         map[i + mapSize - 2] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                         Debug.Log("se reemplazo una pieza");
                    }
                        
                    
                    break;
                    case "EsquinaSI(Clone)":
                    if (map[i + mapSize - 2].name == "EsquinaID(Clone)" || map[i + mapSize - 2].name == "EsquinaSD(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 2].transform;
                        Destroy(map[i + mapSize - 2]);
                        map[i + mapSize - 2] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                   
                    if(map[i + 1].name == "ParedIz 1(Clone)" || map[i + 1].name == "EsquinaSI(Clone)" || map[i + 1].name == "ParedS 1(Clone)" || map[i + 1].name == "EsquinaII(Clone)" || map[i + 1].name == "EsquinaID(Clone)" || map[i + 1].name == "EsquinaSD(Clone)")
                    {
                         Transform piecePos = map[i + 1].transform;
                         Destroy(map[i + 1]);
                         map[i + 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                         Debug.Log("se reemplazo una pieza en EsquinaSI");
                    }

                    if (map[i + mapSize - 1].name == "ParedIn 1(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 1].transform;
                        Destroy(map[i + mapSize - 1]);
                        map[i + mapSize - 1] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                    
                    break;

                    case "ParedS 1(Clone)":
                    if (map[i + mapSize - 2].name == "EsquinaSD(Clone)" || map[i + mapSize - 2].name == "EsquinaSI(Clone)" || map[i + mapSize - 2].name == "ParedIn 1(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 2].transform;
                        Destroy(map[i + mapSize - 2]);
                        map[i + mapSize - 2] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                    if(map[i + 1].name == "EsquinaII(Clone)" || map[i + 1].name == "ParedIz 1(Clone)" || map[i + 1].name == "EsquinaSI(Clone)" || map[i + 1].name == "EsquinaSD(Clone)")
                    {
                        Transform piecePos2 = map[i + 1].transform;
                        Destroy(map[i+1]);
                        map[i+1] = Instantiate(pieces[8], piecePos2.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                    break;
                    case "ParedIn 1(Clone)":
                    if (map[i + mapSize - 2].name == "EsquinaSD(Clone)" || map[i + mapSize - 2].name == "ParedIz(Clone)" || map[i + mapSize - 2].name == "EsquinaSI(Clone)")
                    {
                        Transform piecePos = map[i + mapSize - 2].transform;
                        Destroy(map[i + mapSize - 2]);
                        map[i + mapSize - 2] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                    if(map[i + 1].name == "EsquinaID(Clone)" || map[i + 1].name == "EsquinaSI(Clone)")
                    {
                        Transform piecePos = map[i].transform;
                        Destroy(map[i]);
                        map[i] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                        
                    if(map[i + 1].name == "ParedIn 1(Clone)")
                    {
                        Transform piecePos = map[i].transform;
                        Destroy(map[i]);
                        map[i] = Instantiate(pieces[10], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }
                        
                    
                    break;
                    case "ParedIz 1(Clone)":
                    if (map[i + 1].name == "ParedIz 1(Clone)")
                    {
                        Transform piecePos = map[i].transform;
                        Destroy(map[i]);
                        map[i] = Instantiate(pieces[11], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }

                    if (map[i + mapSize - 2].name == "EsquinaSI(Clone)")
                    {
                        Transform piecePos = map[i].transform;
                        Destroy(map[i]);
                        map[i] = Instantiate(pieces[11], piecePos.position, Quaternion.identity, imageTarget);
                        Debug.Log("se reemplazo una pieza");
                    }

                    if (map[i + mapSize - 2].name == "ParedIz 1(Clone)")
                    {
                         Transform piecePos = map[i + mapSize - 2].transform;
                         Destroy(map[i + mapSize - 2]);
                         map[i + mapSize - 2] = Instantiate(pieces[8], piecePos.position, Quaternion.identity, imageTarget);
                         Debug.Log("se reemplazo una pieza");
                    }
                    
                    break;
            }
            //print(i);
        }
    }

    public void InstantiateEnemiesAndChests()
    {
        foreach (GameObject piece in map)
        {
            int instance = Random.Range(0, 10);
            Transform instanceRotPos = piece.transform.GetChild(0);
            GameObject prefab;
            if (instance > 5 && instance < 9)
            {
                prefab = Instantiate(enemies[Random.Range(0, enemies.Count)], instanceRotPos.position, Quaternion.identity, imageTarget);
                prefab.transform.rotation = instanceRotPos.rotation;
            }
            else
            {
                if (instance >= 9)
                {
                    prefab = Instantiate(chests[Random.Range(0, chests.Count)], instanceRotPos.position, Quaternion.identity, imageTarget);
                    prefab.transform.rotation = instanceRotPos.rotation;
                }
            }
        }

        foreach (GameObject corner in corners)
        {
            Transform instanceRotPos = corner.transform.GetChild(0);
            GameObject prefab;
            if (corner != corners[0])
            {
                int instance = Random.Range(0, 10);
                if (instance > 5 && instance < 9)
                {
                    prefab = Instantiate(enemies[Random.Range(0, enemies.Count)], instanceRotPos.position, Quaternion.identity, imageTarget);
                    prefab.transform.rotation = instanceRotPos.rotation;
                }
                else
                {
                    if (instance >= 9)
                    {
                        prefab = Instantiate(chests[Random.Range(0, chests.Count)], instanceRotPos.position, Quaternion.identity, imageTarget);
                        prefab.transform.rotation = instanceRotPos.rotation;
                    }
                }
            }
        }
    }
}
