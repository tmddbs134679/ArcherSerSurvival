using UnityEngine;

public class GroundReposition : MonoBehaviour
{
    private Transform playerTransform; // 플레이어 Transform

    public Transform[] tiles; // 9개의 타일 Transform 배열

    public Vector2 tileSize = new Vector2(20f, 20f); // 타일 하나의 크기

    private float repositionThresholdX;
    private float repositionThresholdY;
    private float jumpDistanceX;
    private float jumpDistanceY;

    void Start()
    {
        if (PlayerController.Instance != null)
        {
            playerTransform = PlayerController.Instance.transform;
        }
        else
        {
            Debug.LogError("PlayerController.Instance를 찾을 수 없습니다! GridReposition3x3 스크립트가 작동하지 않습니다.");
            enabled = false; // 스크립트 비활성화
            return;
        }

        // 타일 배열 유효성 검사
        if (tiles == null || tiles.Length != 9)
        {
            Debug.LogError("Tiles 배열이 할당되지 않았거나, 정확히 9개의 Transform을 포함해야 합니다!");
            enabled = false; // 스크립트 비활성화
            return;
        }

        const int tilesPerDimension = 3;
        repositionThresholdX = tileSize.x * (tilesPerDimension / 2.0f); 
        repositionThresholdY = tileSize.y * (tilesPerDimension / 2.0f); 

        jumpDistanceX = tileSize.x * tilesPerDimension; 
        jumpDistanceY = tileSize.y * tilesPerDimension; 
    }



    void Update()
    {
        if (playerTransform == null) return;

        Vector3 playerPosition = playerTransform.position; 

        foreach (Transform tile in tiles)
        {
            if (tile == null) continue; 

            Vector3 tilePosition = tile.position;
            Vector3 newPosition = tilePosition; 

            float diffX = playerPosition.x - tilePosition.x;
            float diffY = playerPosition.y - tilePosition.y;
            if (Mathf.Abs(diffX) > repositionThresholdX)
            {
                newPosition.x += Mathf.Sign(diffX) * jumpDistanceX;
            }

            if (Mathf.Abs(diffY) > repositionThresholdY)
            {
                newPosition.y += Mathf.Sign(diffY) * jumpDistanceY;
            }

            if (newPosition != tilePosition)
            {
                tile.position = newPosition;
            }
        }
    }
}

