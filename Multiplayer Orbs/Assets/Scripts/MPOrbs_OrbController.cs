using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;
using System.Linq.Expressions;

public class MPOrbs_OrbController : MonoBehaviourPun
{
    public float accel = 6f;
    public float maxSpeed = 4f;
    public float jumpPower = 5;
    private bool jumpReady;
    //private Collider cld;
    private Rigidbody rb;
    public Color playerColor;
    private float lastYPos;
    private GameObject lastCollider;
    private Renderer rend;
    private Material baseMat;
    private Collider cld;

    [Header("Forms and Materials")]
    private bool isShifted;
    public bool metalFormEnabled;
    public Material metalMaterial;
    public bool gasFormEnabled;
    public Material gasMaterial;
    public bool rubberFormEnabled;
    public Material rubberMaterial;
    public PhysicMaterial rubberPhysicsMat;
    public bool fireFormEnabled;
    public Material fireMaterial;
    public GameObject fireObject;
    public Light fireLight;

    private Dictionary<string, Material> matDict = new Dictionary<string, Material>();

    // Start is called before the first frame update
    void Start()
    {
        int actNum = PhotonNetwork.LocalPlayer.ActorNumber % 4;
        switch (actNum)
        {
            case 0:
                fireFormEnabled = true;
                break;
            case 1:
                metalFormEnabled = true;
                break;
            case 2:
                gasFormEnabled = true;
                break;
            case 3:
                rubberFormEnabled = true;
                break;
        }
        if (fireObject != null)
            fireObject.SetActive(false);
        //if(PhotonNetwork.LocalPlayer.ActorNumber % 2 > 0)
        //{
        //    gasFormEnabled = true;
        //}
        //else
        //{
        //    metalFormEnabled = true;
        //}
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        matDict.Add("metal", metalMaterial);
        matDict.Add("gas", gasMaterial);
        matDict.Add("rubber", rubberMaterial);
        matDict.Add("fire", fireMaterial);
        cld = GetComponent<Collider>();
        if (!MPOrbs_PhotonGameManager.instance.colorChosen && photonView.IsMine)
        {
            playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            photonView.RPC("SyncOrbColor", RpcTarget.AllBuffered, /*SendMessageOptions.DontRequireReceiver,*/ playerColor.r, playerColor.g, playerColor.b );
        }

        //GetComponent<Renderer>().material.color = playerColor;
        //InvokeRepeating("CheckJumpRefresh", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Stop();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetOrb();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isShifted)
                ShiftForm();
            else
                UnshiftForm();
        }
        //if(Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    UnshiftForm();
        //}
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(!isShifted)
            {
                if (gasFormEnabled)
                {
                    metalFormEnabled = true;
                    gasFormEnabled = false;
                }
                else if (metalFormEnabled)
                {
                    rubberFormEnabled = true;
                    metalFormEnabled = false;
                }
                else if (rubberFormEnabled)
                {
                    fireFormEnabled = true;
                    rubberFormEnabled = false;
                }
                else if (fireFormEnabled)
                {
                    gasFormEnabled = true;
                    fireFormEnabled = false;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if(isShifted)
        {
            if(gasFormEnabled)
            {
                rb.AddForce(Physics.gravity * rb.mass * -0.7f);
                rb.velocity = 0.98f * rb.velocity;
            }
            if(metalFormEnabled)
            {
                rb.AddForce(Physics.gravity * rb.mass * 0.5f);
            }
        }
    }

    private void MoveRight()
    {
        if (rb.velocity.x <= maxSpeed)
        {
            rb.AddForce(accel * Vector3.right * rb.mass);
        }
        if (rb.velocity.x < 0)
        {
            rb.AddForce(accel * Vector3.right * rb.mass);
        }
    }

    private void MoveLeft()
    {
        if (rb.velocity.x >= -maxSpeed)
        {
            rb.AddForce(accel * Vector3.left * rb.mass);
        }
        if (rb.velocity.x > 0)
        {
            rb.AddForce(accel * Vector3.left * rb.mass);
        }
    }

    private void Stop()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        rb.angularVelocity = Vector3.zero;
    }

    private void Jump()
    {
        if (jumpReady)
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, 0);
        Invoke("ClearJump", 0.1f);
    }

    private void ResetOrb()
    {
        photonView.RPC("ResetOrbRPC", RpcTarget.All);
        rb.velocity = Vector3.zero;
    }

    [PunRPC]
    public void ResetOrbRPC()
    {
        transform.position = transform.parent.position;
        //rb.velocity = Vector3.zero;
    }

    private void ShiftForm()
    {
        if(metalFormEnabled)
        {
            photonView.RPC("ShiftFormRPC", RpcTarget.AllBuffered, "metal");
        }
        else if (gasFormEnabled)
        {
            photonView.RPC("ShiftFormRPC", RpcTarget.AllBuffered, "gas");
        }
        else if (rubberFormEnabled)
        {
            photonView.RPC("ShiftFormRPC", RpcTarget.AllBuffered, "rubber");
        }
        else if (fireFormEnabled)
        {
            photonView.RPC("ShiftFormRPC", RpcTarget.AllBuffered, "fire");
        }
    }

    [PunRPC]
    public void ShiftFormRPC(string formToAssume)
    {
        if(formToAssume == "metal")
        {
            rb.mass = 100f;
            transform.localScale = Vector3.one * 1.15f;
            rend.material = metalMaterial;
            rend.material.color = playerColor;
            //rb.velocity = 0.01f * rb.velocity;
            accel = accel / 2f;
            jumpPower = jumpPower * 3f / 4f;
        }
        else if(formToAssume == "gas")
        {
            rb.mass = 0.1f;
            //rb.useGravity = false;
            rend.material = gasMaterial;
            rend.material.color = new Color (playerColor.r, playerColor.g, playerColor.b, 0.5f);
        }
        else if(formToAssume == "rubber")
        {
            rb.mass = 0.5f;
            rend.material = rubberMaterial;
            rend.material.color = playerColor;
            cld.material.bounciness = 1f;
            cld.material.bounceCombine = PhysicMaterialCombine.Maximum;
            maxSpeed = maxSpeed * 1.5f;
            //cld.material = rubberPhysicsMat;
        }
        else if (formToAssume == "fire")
        {
            rb.mass = 0.2f;
            fireObject.SetActive(true);
            rend.material = fireMaterial;
            rend.material.color = playerColor;
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", playerColor);
            fireLight.color = playerColor;
        }
        isShifted = true;
    }

    private void UnshiftForm()
    {
        if (metalFormEnabled)
        {
            photonView.RPC("UnshiftFormRPC", RpcTarget.AllBuffered, "metal");
        }
        else if (gasFormEnabled)
        {
            photonView.RPC("UnshiftFormRPC", RpcTarget.AllBuffered, "gas");
        }
        else if (rubberFormEnabled)
        {
            photonView.RPC("UnshiftFormRPC", RpcTarget.AllBuffered, "rubber");
        }
        else if (fireFormEnabled)
        {
            photonView.RPC("UnshiftFormRPC", RpcTarget.AllBuffered, "fire");
        }
    }

    [PunRPC]
    public void UnshiftFormRPC(string formToRevert)
    {
        if (formToRevert == "metal")
        {
            rb.mass = 1f;
            rb.transform.localScale = Vector3.one;
            accel = 2f * accel;
            jumpPower = jumpPower * 4f / 3f;
        }
        else if (formToRevert == "gas")
        {
            rb.mass = 1f;
            //rb.useGravity = true;
        }
        else if (formToRevert == "rubber")
        {
            rb.mass = 1f;
            cld.material.bounciness = 0f;
            maxSpeed = maxSpeed * 2f / 3f;
        }
        else if (formToRevert == "fire")
        {
            rb.mass = 5f * rb.mass;
            transform.localScale = Vector3.one;
            fireObject.SetActive(false);
        }
        rend.material = baseMat;
        rend.material.color = playerColor;
        isShifted = false;
    }

    private void ClearJump()
    {
        jumpReady = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((jumpReady == false) && (collision.transform.gameObject.layer == LayerMask.NameToLayer("Ground")) && (collision.GetContact(0).point.y < transform.position.y) && (rb.velocity.y < 0))
        {
            jumpReady = true;
            //print("JumpReady restored by OnCollisionEnter");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((jumpReady == false) && (collision.gameObject == lastCollider) && (collision.transform.gameObject.layer == LayerMask.NameToLayer("Ground")) && (collision.GetContact(0).point.y < (transform.position.y - 0.15f)))
        {
            jumpReady = true;
            //print("JumpReady restored by OnCollisionStay");
            //print(collision.GetContact(0).point);
            //print(transform.position.y);
            //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //obj.transform.position = collision.GetContact(0).point;
            //obj.transform.localScale = Vector3.one * 0.01f;
        }
        lastCollider = collision.gameObject;
    }

    private void CheckJumpRefresh()
    {
        if (transform.position.y == lastYPos)
        {
            jumpReady = true;
            //print("JumpReady restored by CheckJumpRefresh");
        }
        lastYPos = transform.position.y;
    }

    [PunRPC]
    public void SyncOrbColor(float r, float g, float b)
    {
        playerColor = new Color(r, g, b, 1);
        GetComponent<Renderer>().material.color = playerColor;
    }
}
