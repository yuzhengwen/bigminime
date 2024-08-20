using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzuValen.Utils;

public class CollectiblesCounter : MonoBehaviourSingleton<CollectiblesCounter>
{
    public TMPro.TextMeshPro coinsText;
    public TMPro.TextMeshPro keysText;

    private void Start()
    {
        SetCoins(0);
        SetKeys(0);
    }
    public void SetCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void SetKeys(int keys)
    {
        keysText.text = keys.ToString();
    }
}
