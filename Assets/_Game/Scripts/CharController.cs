using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InputSystem))]
public class CharController : MonoBehaviour
{
    public float directionalMovementBlendInterpolationFactor = 5.0f;
    public float rotationMovementBlendInterpolationFactor = 10.0f;
    public bool combatMode = false;

    private Animator animator = null;
    private InputSystem inputSystem = null;

    private float forwardInput = 0.0f;
    private float strafeInput = 0.0f;
    private bool principalFireInput = false;
    private bool secondaryFireInput = false;
    private bool useBackgroundElement = false;

    private float forwardBlend = 0.0f;
    private float strafeBlend = 0.0f;

    public bool IsTurnedAround { get { return isTrunedAround; } }
    private bool isTrunedAround = false;

    private Ray ray;
    private Transform cameraFocus;
    [SerializeField]
    private float maxInterractionDist = 2.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputSystem = GetComponent<InputSystem>();
        cameraFocus = GameObject.Find("CameraFocus").transform;

        animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
    }

    private void Update()
    {
        forwardInput = inputSystem.GetForward();
        strafeInput = inputSystem.GetStrafe();

        if (Input.GetKeyDown(KeyCode.R))
            combatMode = !combatMode;

        principalFireInput = Input.GetButton("Fire1");
        secondaryFireInput = Input.GetButton("Fire2");
        useBackgroundElement = Input.GetButton("Action");

        ray.origin = cameraFocus.position;
        ray.direction = cameraFocus.position - Camera.main.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<Usable>() != null && useBackgroundElement && hit.distance <= maxInterractionDist)
                hit.transform.GetComponent<Usable>().Use();
        }
    }

    void OnDrawGizmos()
    {
        //Debug
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(hit.point, 0.5f);
        }
        //
    }

    private void FixedUpdate()
    {
        Vector3 groundProjectedDir = Vector3.ProjectOnPlane(inputSystem.GetLookDir(), Vector3.up);

        float angle = Vector3.Angle(Vector3.forward, groundProjectedDir);

        if (Vector3.Dot(groundProjectedDir, Vector3.right) < 0.0f)
            angle = -angle;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0.0f, angle, 0.0f));

        float multiplier = Input.GetKey(KeyCode.LeftShift) ? 2.0f : 1.0f;

        float finalForwardInput = forwardInput;
        float finalStrafeInput = strafeInput;

        if (!combatMode)
        {
            if (forwardInput < 0.0f)
                isTrunedAround = true;
            else if (forwardInput > 0.0f)
                isTrunedAround = false;
        }
        else
        {
            isTrunedAround = false;

            if (secondaryFireInput)
                multiplier = 1.0f;
        }

        if (isTrunedAround)
        {
            finalForwardInput = Mathf.Abs(forwardInput);
            finalStrafeInput = -strafeInput;
            targetRotation = targetRotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        forwardBlend = Mathf.Lerp(forwardBlend, finalForwardInput * multiplier, Time.fixedDeltaTime * directionalMovementBlendInterpolationFactor);
        strafeBlend = Mathf.Lerp(strafeBlend, finalStrafeInput * multiplier, Time.fixedDeltaTime * directionalMovementBlendInterpolationFactor);

        animator.SetFloat("SpeedForward", forwardBlend);
        animator.SetFloat("SpeedStrafe", strafeBlend);
        animator.SetBool("CombatMode", combatMode);
        animator.SetBool("PrincipalFire", principalFireInput);
        animator.SetBool("AlternateFire", secondaryFireInput);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationMovementBlendInterpolationFactor);
    }
}
