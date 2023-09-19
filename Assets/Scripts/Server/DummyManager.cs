using Mirror;
using UnityEngine;

namespace HTNWIC.Server
{
    public class DummyManager : NetworkBehaviour
    {
        [SerializeField]
        private GameObject dummyPrefab;
        // Update is called once per frame
        void Update()
        {
            // spawn dummy prefab on keypress, set the position by sending a raycast from the camera to the mouse position
            if (isServerOnly && Input.GetKeyDown(KeyCode.T))
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject dummy = Instantiate(dummyPrefab, hit.point + new Vector3(0, 1, 0), Quaternion.identity);
                    NetworkServer.Spawn(dummy);
                }
            }

        }
    }
}
