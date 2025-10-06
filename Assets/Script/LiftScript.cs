using UnityEngine;


[System.Serializable]
public class LiftMovementPoint
{
    public Vector3 targetPosition;
    public Vector3 direction;
    public float speed = 3f;

    public LiftMovementPoint(Vector3 pos, Vector3 dir, float spd = 3f)
    {
        targetPosition = pos;
        direction = dir.normalized;
        speed = spd;
    }
}

public class LiftScript : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float defaultSpeed = 3f;
    public bool loopMovement = true; // �ړ��p�^�[�������[�v���邩
    public bool useLocalPosition = false; // ���[�J�����W���g�p���邩
    public bool sticky = false; // ��ɏ�����v���C���[���t���Ă��邩

    [Header("�ړ��|�C���g")]
    public LiftMovementPoint[] liftmovementpoint;

    [Header("�f�o�b�O���")]
    public bool showDebugInfo = true;
    public bool showPath = true;

    private int currentPointIndex = 0;
    private Vector3 startPosition;
    private bool isMoving = true;
    private Vector3 currentDirection;
    private float currentSpeed;
    public static LiftScript instance;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
        startPosition = transform.position;

        // �ړ��|�C���g���ݒ肳��Ă��Ȃ��ꍇ�A�f�t�H���g�̃p�^�[�����쐬
        if (liftmovementpoint == null || liftmovementpoint.Length == 0)
        {
            CreateDefaultMovementPoints();
        }

        // �ŏ��̈ړ����J�n
        if (liftmovementpoint.Length > 0)
        {
            SetNextTarget();
        }
    }

    void Update()
    {
        if (isMoving && liftmovementpoint.Length > 0)
        {
            MoveTowardsTarget();
        }
        float move = Input.GetAxis("Horizontal");
    }

    void MoveTowardsTarget()
    {
        LiftMovementPoint currentPoint = liftmovementpoint[currentPointIndex];
        Vector3 targetPos = useLocalPosition ?
            startPosition + currentPoint.targetPosition :
            currentPoint.targetPosition;

        // �ڕW�ʒu�Ɍ������Ĉړ�
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 movement = direction * currentSpeed * Time.deltaTime;

        // �ڕW�ʒu�ɓ��B�������`�F�b�N
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);

        if (distanceToTarget <= 0.1f)
        {
            // �ڕW�ʒu�ɓ��B
            transform.position = targetPos;
            ReachTarget();
        }
        else
        {
            // �ړ��𑱂���
            transform.position += movement;
        }
    }

    void ReachTarget()
    {
        if (showDebugInfo)
        {
            Debug.Log($"�ړ��|�C���g {currentPointIndex} �ɓ��B");
        }

        // ���̈ړ��|�C���g�Ɉړ�
        currentPointIndex++;

        if (currentPointIndex >= liftmovementpoint.Length)
        {
            if (loopMovement)
            {
                currentPointIndex = 0; // ���[�v
            }
            else
            {
                isMoving = false; // �ړ���~
                if (showDebugInfo)
                {
                    Debug.Log("�S�Ă̈ړ��|�C���g��ʉ߂��܂���");
                }
                return;
            }
        }

        SetNextTarget();
    }

    void SetNextTarget()
    {
        if (currentPointIndex < liftmovementpoint.Length)
        {
            LiftMovementPoint currentPoint = liftmovementpoint[currentPointIndex];
            currentDirection = currentPoint.direction;
            currentSpeed = currentPoint.speed > 0 ? currentPoint.speed : defaultSpeed;

            if (showDebugInfo)
            {
                Debug.Log($"���̖ڕW: �|�C���g {currentPointIndex}, ����: {currentDirection}, ���x: {currentSpeed}");
            }
        }
    }

    void CreateDefaultMovementPoints()
    {
        // �f�t�H���g�̈ړ��p�^�[���i�����`�j
        Vector3 basePos = transform.position;
        liftmovementpoint = new LiftMovementPoint[]
        {
            new LiftMovementPoint(basePos + Vector3.forward * 5f, Vector3.forward, defaultSpeed),
            new LiftMovementPoint(basePos + Vector3.forward * 5f + Vector3.right * 5f, Vector3.right, defaultSpeed),
            new LiftMovementPoint(basePos + Vector3.right * 5f, Vector3.back, defaultSpeed),
            new LiftMovementPoint(basePos, Vector3.left, defaultSpeed)
        };
    }

    // �ړ��|�C���g�𓮓I�ɐݒ肷�郁�\�b�h
    public void SetMovementPoints(LiftMovementPoint[] newPoints)
    {
        liftmovementpoint = newPoints;
        currentPointIndex = 0;
        isMoving = true;

        if (liftmovementpoint.Length > 0)
        {
            SetNextTarget();
        }
    }

    // �ړ��|�C���g��ǉ����郁�\�b�h
    public void AddMovementPoint(Vector3 position, Vector3 direction, float speed = 0f)
    {
        LiftMovementPoint[] newPoints = new LiftMovementPoint[liftmovementpoint.Length + 1];
        for (int i = 0; i < liftmovementpoint.Length; i++)
        {
            newPoints[i] = liftmovementpoint[i];
        }
        newPoints[liftmovementpoint.Length] = new LiftMovementPoint(position, direction, speed > 0 ? speed : defaultSpeed);
        liftmovementpoint = newPoints;
    }

    // �ړ����ꎞ��~/�ĊJ���郁�\�b�h
    public void SetMovementEnabled(bool enabled)
    {
        isMoving = enabled;
    }

    // ����̃|�C���g����ړ����J�n
    public void StartFromPoint(int pointIndex)
    {
        if (pointIndex >= 0 && pointIndex < liftmovementpoint.Length)
        {
            currentPointIndex = pointIndex;
            isMoving = true;
            SetNextTarget();
        }
    }

    // ���݂̈ړ���Ԃ��擾
    public bool IsMoving()
    {
        return isMoving;
    }

    // ���݂̃|�C���g�C���f�b�N�X���擾
    public int GetCurrentPointIndex()
    {
        return currentPointIndex;
    }

    // �f�o�b�O�p�̕`��
    void OnDrawGizmos()
    {
        if (!showDebugInfo) return;

        Vector3 basePos = Application.isPlaying ? startPosition : transform.position;

        if (liftmovementpoint != null && liftmovementpoint.Length > 0)
        {
            // �ړ��|�C���g��`��
            for (int i = 0; i < liftmovementpoint.Length; i++)
            {
                Vector3 pointPos = useLocalPosition ?
                    basePos + liftmovementpoint[i].targetPosition :
                    liftmovementpoint[i].targetPosition;

                // ���݂̖ڕW�|�C���g��ԂŁA����ȊO��ŕ\��
                Gizmos.color = (i == currentPointIndex) ? Color.red : Color.blue;
                Gizmos.DrawWireSphere(pointPos, 0.5f);

                // �|�C���g�ԍ���\��
                Gizmos.color = Color.white;

                // ��������ŕ\��
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(pointPos, liftmovementpoint[i].direction * 2f);
            }

            // �p�X����ŕ`��
            if (showPath)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < liftmovementpoint.Length; i++)
                {
                    Vector3 currentPos = useLocalPosition ?
                        basePos + liftmovementpoint[i].targetPosition :
                        liftmovementpoint[i].targetPosition;

                    Vector3 nextPos;
                    if (i == liftmovementpoint.Length - 1)
                    {
                        if (loopMovement)
                        {
                            nextPos = useLocalPosition ?
                                basePos + liftmovementpoint[0].targetPosition :
                                liftmovementpoint[0].targetPosition;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        nextPos = useLocalPosition ?
                            basePos + liftmovementpoint[i + 1].targetPosition :
                            liftmovementpoint[i + 1].targetPosition;
                    }

                    Gizmos.DrawLine(currentPos, nextPos);
                }
            }
        }

        // ���݂̈ړ�������\��
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, currentDirection * 2f);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {

    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("aaa");
    //    }
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("ego"))
    //    {
    //        SetMovementEnabled(false);
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("ego"))
    //    {
    //        SetMovementEnabled(true);
    //    }
    //}



}

