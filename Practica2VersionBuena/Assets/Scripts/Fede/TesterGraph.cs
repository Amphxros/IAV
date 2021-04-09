/*    
    Obra original:
        Copyright (c) 2018 Packt
        Unity 2018 Artificial Intelligence Cookbook - Second Edition, by Jorge Palacios
        https://github.com/PacktPublishing/Unity-2018-Artificial-Intelligence-Cookbook-Second-Edition
        MIT License

    Modificaciones:
        Copyright (C) 2020-2021 Federico Peinado
        http://www.federicopeinado.com

        Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
        Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).
        Contacto: email@federicopeinado.com
*/
namespace UCM.IAV.Navegacion
{

    using UnityEngine;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;

    // Posibles algoritmos para buscar caminos en grafos
    public enum TesterGraphAlgorithm
    {
        BFS, DFS, DIJKSTRA, ASTAR
    }

    //
    public class TesterGraph : MonoBehaviour
    {
        

        public GraphGrid graph;
        public TesterGraphAlgorithm algorithm;
        public bool smoothPath;
        public string vertexTag = "Vertex"; // Etiqueta de un nodo normal
        public string obstacleTag = "Wall"; // Etiqueta de un obstáculo, tipo pared...
        //public Material materialAntes;
        //public Material materialDespues;
        public Color pathColor;
        [Range(0.1f, 1f)]
        public float pathNodeRadius = .3f;

        Camera mainCamera;
        public GameObject teseo;
        public  GameObject salida;
        public GameObject minotauro;
        List<Vertex> path; // La variable con el camino calculado

        private bool started = false;

        private bool spaceBar = false;
        public float timeToActiveHelp = 2.5f;
        private float SpaceBarPressedTime = 0.0f;

        private bool modoAutomatico = false;

        public void initGame()
        {
            started = true;
            teseo.SetActive(true);
            salida.SetActive(true);
            minotauro.SetActive(true);
            salida.transform.position = graph.getSalida();
            minotauro.transform.position = new Vector3(graph.getCol() / 2 * graph.cellSize, 0, graph.getRow() / 2 * graph.cellSize);

        }
        // Despertar inicializando esto
        void Awake()
        {
            mainCamera = Camera.main;
            //srcObj = null;
            //dstObj = null;
            path = new List<Vertex>();
        }

        // Update is called once per frame
        void Update()
        {
            //// El origen se marca haciendo click
            //if (Input.GetMouseButtonDown(0))
            //{
            //    srcObj = GetNodeFromScreen(Input.mousePosition);
            //}
            //// El destino simplemente poniendo el ratón encima
            //dstObj = GetNodeFromScreen(Input.mousePosition);

            // Con la barra espaciadora se activa la búsqueda del camino mínimo
            if (started)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    smoothPath = !smoothPath;
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    SpaceBarPressedTime += Time.deltaTime;
                    spaceBar = true;

                    //Debug.Log("Clicando space bar ");
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    ShowPath(path, false);
                    SpaceBarPressedTime = 0.0f;
                    spaceBar = false;
                    modoAutomatico = false;
                    teseo.GetComponent<PlayerAutomatic>().enabled = false;
                    teseo.GetComponent<PlayerMovement>().enabled = true;
                }

                if (SpaceBarPressedTime > timeToActiveHelp)
                {
                    if (!modoAutomatico)
                    {
                        // Manda mensaje de control automatico
                        PlayerAutomatic automatic = teseo.GetComponent<PlayerAutomatic>();
                        automatic.enabled = true;
                        automatic.GetPathToExit(path);
                        teseo.GetComponent<PlayerMovement>().enabled = false;

                        ShowPath(path, true);
                       // Debug.Log("Cambiando a modo automatico");
                        modoAutomatico = true;
                    }
                }
                else if (SpaceBarPressedTime > 0.0f)
                {
                    teseo.GetComponent<PlayerMovement>().enabled =false;
                    path = new List<Vertex>();
                    switch (algorithm)
                    {
                        case TesterGraphAlgorithm.ASTAR:
                            path = graph.GetPathAstar(teseo, salida, null); // Se pasa la heurística
                            break;
                        default:
                        case TesterGraphAlgorithm.BFS:
                            path = graph.GetPathBFS(teseo, salida);
                            break;
                        case TesterGraphAlgorithm.DFS:
                            path = graph.GetPathDFS(teseo, salida);
                            break;
                        case TesterGraphAlgorithm.DIJKSTRA:
                            path = graph.GetPathDijkstra(teseo, salida);
                            break;
                    }
                    if (smoothPath)
                        path = graph.Smooth(path); // Suavizar el camino, una vez calculado

                    if (path.Count > 1)
                    {
                        ShowPath(path, true);
                      //  Debug.Log("Dibuja el camino de A*");
                    }
                }
                
                if (teseo.transform.position == salida.transform.position)
                {
                    teseo.SetActive(false);
                    minotauro.SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(0);
                }
            }
        }

        // Dibujado de artilugios en el editor
        

        // Mostrar el camino calculado
        
        // Cuantificación, cómo traduce de posiciones del espacio (la pantalla) a nodos
        private GameObject GetNodeFromScreen(Vector3 screenPosition)
        {
            GameObject node = null;
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit h in hits)
            {
                if (!h.collider.CompareTag(vertexTag) && !h.collider.CompareTag(obstacleTag))
                    continue;
                node = h.collider.gameObject;
                break;
            }
            return node;
        }
        public void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            if (ReferenceEquals(graph, null))
                return;

            Vertex v;
            if (!ReferenceEquals(teseo, null))
            {
                Gizmos.color = Color.magenta; // rojo es el nodo inicial
                v = graph.GetNearestVertex(teseo.transform.position);
                Gizmos.DrawSphere(v.transform.position, pathNodeRadius);
            }
            if (!ReferenceEquals(salida, null))
            {
                Gizmos.color = Color.yellow; // verde es el color del nodo de destino
                v = graph.GetNearestVertex(salida.transform.position);
                Gizmos.DrawSphere(v.transform.position, pathNodeRadius);
            }
            int i;
            Gizmos.color = pathColor;
            for (i = 0; i < path.Count; i++)
            {
                v = path[i];
                Gizmos.DrawSphere(v.transform.position, pathNodeRadius);
                if (smoothPath && i != 0)
                {
                    Vertex prev = path[i - 1];
                    Gizmos.DrawLine(v.transform.position, prev.transform.position);

                }

            }
        }
        public void ShowPath(List<Vertex> path, bool visible)
        {
            int i;
            for (i = 0; i < path.Count; i++)
            {
                Vertex v = path[i];
                Renderer r = v.GetComponentInChildren<Renderer>();
                if (ReferenceEquals(r, null))
                {
                    continue;
                }
                if (visible)
                    r.material.color = Color.yellow;
                else
                    r.material.color = Color.black;
                //Debug.Log("color cambiado");
            }
        }

    }
}
