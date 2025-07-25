using UnityEngine;


[System.Serializable]
public class MovementPoint
{
    public Vector3 targetPosition;
    public Vector3 direction;
    public float speed = 3f;

    public MovementPoint(Vector3 pos, Vector3 dir, float spd = 3f)
    {
        targetPosition = pos;
        direction = dir.normalized;
        speed = spd;
    }
}

public class EnemyScript : MonoBehaviour
{
    [Header("移動設定")]
    public float defaultSpeed = 3f;
    public bool loopMovement = true; // 移動パターンをループするか
    public bool useLocalPosition = false; // ローカル座標を使用するか

    [Header("移動ポイント")]
    public MovementPoint[] movementPoints;

    [Header("デバッグ情報")]
    public bool showDebugInfo = true;
    public bool showPath = true;

    private int currentPointIndex = 0;
    private Vector3 startPosition;
    private bool isMoving = true;
    private Vector3 currentDirection;
    private float currentSpeed;
    public static EnemyScript instance;

    void Start()
    {

        instance = this;
        startPosition = transform.position;

        // 移動ポイントが設定されていない場合、デフォルトのパターンを作成
        if (movementPoints == null || movementPoints.Length == 0)
        {
            CreateDefaultMovementPoints();
        }

        // 最初の移動を開始
        if (movementPoints.Length > 0)
        {
            SetNextTarget();
        }
    }

    void Update()
    {
        if (isMoving && movementPoints.Length > 0)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        MovementPoint currentPoint = movementPoints[currentPointIndex];
        Vector3 targetPos = useLocalPosition ?
            startPosition + currentPoint.targetPosition :
            currentPoint.targetPosition;

        // 目標位置に向かって移動
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 movement = direction * currentSpeed * Time.deltaTime;

        // 目標位置に到達したかチェック
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);

        if (distanceToTarget <= 0.1f)
        {
            // 目標位置に到達
            transform.position = targetPos;
            ReachTarget();
        }
        else
        {
            // 移動を続ける
            transform.position += movement;
        }
    }

    void ReachTarget()
    {
        if (showDebugInfo)
        {
            Debug.Log($"移動ポイント {currentPointIndex} に到達");
        }

        // 次の移動ポイントに移動
        currentPointIndex++;

        if (currentPointIndex >= movementPoints.Length)
        {
            if (loopMovement)
            {
                currentPointIndex = 0; // ループ
            }
            else
            {
                isMoving = false; // 移動停止
                if (showDebugInfo)
                {
                    Debug.Log("全ての移動ポイントを通過しました");
                }
                return;
            }
        }

        SetNextTarget();
    }

    void SetNextTarget()
    {
        if (currentPointIndex < movementPoints.Length)
        {
            MovementPoint currentPoint = movementPoints[currentPointIndex];
            currentDirection = currentPoint.direction;
            currentSpeed = currentPoint.speed > 0 ? currentPoint.speed : defaultSpeed;

            if (showDebugInfo)
            {
                Debug.Log($"次の目標: ポイント {currentPointIndex}, 方向: {currentDirection}, 速度: {currentSpeed}");
            }
        }
    }

    void CreateDefaultMovementPoints()
    {
        // デフォルトの移動パターン（正方形）
        Vector3 basePos = transform.position;
        movementPoints = new MovementPoint[]
        {
            new MovementPoint(basePos + Vector3.forward * 5f, Vector3.forward, defaultSpeed),
            new MovementPoint(basePos + Vector3.forward * 5f + Vector3.right * 5f, Vector3.right, defaultSpeed),
            new MovementPoint(basePos + Vector3.right * 5f, Vector3.back, defaultSpeed),
            new MovementPoint(basePos, Vector3.left, defaultSpeed)
        };
    }

    // 移動ポイントを動的に設定するメソッド
    public void SetMovementPoints(MovementPoint[] newPoints)
    {
        movementPoints = newPoints;
        currentPointIndex = 0;
        isMoving = true;

        if (movementPoints.Length > 0)
        {
            SetNextTarget();
        }
    }

    // 移動ポイントを追加するメソッド
    public void AddMovementPoint(Vector3 position, Vector3 direction, float speed = 0f)
    {
        MovementPoint[] newPoints = new MovementPoint[movementPoints.Length + 1];
        for (int i = 0; i < movementPoints.Length; i++)
        {
            newPoints[i] = movementPoints[i];
        }
        newPoints[movementPoints.Length] = new MovementPoint(position, direction, speed > 0 ? speed : defaultSpeed);
        movementPoints = newPoints;
    }

    // 移動を一時停止/再開するメソッド
    public void SetMovementEnabled(bool enabled)
    {
        isMoving = enabled;
    }

    // 特定のポイントから移動を開始
    public void StartFromPoint(int pointIndex)
    {
        if (pointIndex >= 0 && pointIndex < movementPoints.Length)
        {
            currentPointIndex = pointIndex;
            isMoving = true;
            SetNextTarget();
        }
    }

    // 現在の移動状態を取得
    public bool IsMoving()
    {
        return isMoving;
    }

    // 現在のポイントインデックスを取得
    public int GetCurrentPointIndex()
    {
        return currentPointIndex;
    }

    // デバッグ用の描画
    void OnDrawGizmos()
    {
        if (!showDebugInfo) return;

        Vector3 basePos = Application.isPlaying ? startPosition : transform.position;

        if (movementPoints != null && movementPoints.Length > 0)
        {
            // 移動ポイントを描画
            for (int i = 0; i < movementPoints.Length; i++)
            {
                Vector3 pointPos = useLocalPosition ?
                    basePos + movementPoints[i].targetPosition :
                    movementPoints[i].targetPosition;

                // 現在の目標ポイントを赤で、それ以外を青で表示
                Gizmos.color = (i == currentPointIndex) ? Color.red : Color.blue;
                Gizmos.DrawWireSphere(pointPos, 0.5f);

                // ポイント番号を表示
                Gizmos.color = Color.white;

                // 方向を矢印で表示
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(pointPos, movementPoints[i].direction * 2f);
            }

            // パスを線で描画
            if (showPath)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < movementPoints.Length; i++)
                {
                    Vector3 currentPos = useLocalPosition ?
                        basePos + movementPoints[i].targetPosition :
                        movementPoints[i].targetPosition;

                    Vector3 nextPos;
                    if (i == movementPoints.Length - 1)
                    {
                        if (loopMovement)
                        {
                            nextPos = useLocalPosition ?
                                basePos + movementPoints[0].targetPosition :
                                movementPoints[0].targetPosition;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        nextPos = useLocalPosition ?
                            basePos + movementPoints[i + 1].targetPosition :
                            movementPoints[i + 1].targetPosition;
                    }

                    Gizmos.DrawLine(currentPos, nextPos);
                }
            }
        }

        // 現在の移動方向を表示
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, currentDirection * 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ego"))
        {
            SetMovementEnabled(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ego"))
        {
            SetMovementEnabled(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ego"))
        {
            SetMovementEnabled(true);
        }
    }



}