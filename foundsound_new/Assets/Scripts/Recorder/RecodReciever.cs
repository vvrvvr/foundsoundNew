using UnityEngine;

public class RecordReciever : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Record record = other.GetComponent<Record>();
        if (record != null)
        {
            string recordName = record.RecordName;
            HandleRecord(recordName);
            record.DestroyWithDelay(0.5f);
        }
    }
    
    protected virtual void HandleRecord(string recordName)
    {
        Debug.Log("Record received with name: " + recordName);
    }
}