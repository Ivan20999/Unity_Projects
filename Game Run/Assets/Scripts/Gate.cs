using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField] int _value;
    [SerializeField] DeformationType _deformationType;
    [SerializeField] GateAppeareance _gateAppeareance;

    private void OnValidate()
    {
        _gateAppeareance.UpdateVisual(_deformationType, _value);
    }

    private void OnTriggerEnter(Collider other)
    {

        PlayerModifier playerModifier = other.attachedRigidbody.GetComponent<PlayerModifier>();
        if (playerModifier)
        {
            if (_deformationType == DeformationType.Width)
            {
                playerModifier.AddWidth(_value);
            }
            else if (_deformationType == DeformationType.Height)
            {
                playerModifier.AddHeight(_value);
            }

            Destroy(gameObject);

        }


    }
}