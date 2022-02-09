using UnityEngine;

public enum ObjectType { Ground, Above}
public class CoinObject : MovementForObject
{

    [SerializeField] GameObject coin;
    [SerializeField] GameObject audioPrefabs;

    private void Update()
    {
        coin.transform.Rotate(0f, 0f, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (moveForObject)
            {
                case ObjectType.Ground:
                    if (other.GetComponent<PlayerController>().isTouchingGround)
                    {
                        GameObject go = Instantiate(audioPrefabs);
                        inGameTracker.trackerInstance.CoinIncrement();
                        Destroy(go, 2.1f);
                        Destroy(coin);
                        Destroy(gameObject);
                    }
                    else
                        return;
                    break;

                case ObjectType.Above:
                    if (!other.GetComponent<PlayerController>().isTouchingGround)
                    {
                        GameObject go = Instantiate(audioPrefabs);
                        inGameTracker.trackerInstance.CoinIncrement();
                        Destroy(go, 2.1f);
                        Destroy(coin);
                        Destroy(gameObject);
                    }
                    else
                        return;
                    break;
            }
            
        }
    }

}
