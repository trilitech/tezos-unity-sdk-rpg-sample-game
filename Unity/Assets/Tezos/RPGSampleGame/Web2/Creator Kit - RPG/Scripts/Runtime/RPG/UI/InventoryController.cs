using System.Collections;
using System.Collections.Generic;
using RPGM.Core;
using RPGM.Gameplay;
using RPGM.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPGM.UI
{
    public class InventoryController : MonoBehaviour
    {
        public Transform elementPrototype;
        public float stepSize = 1;

        Vector2 firstItem;
        GameModel model = Schedule.GetModel<GameModel>();
        
        public Image _image;

        void Start()
        {
            firstItem = elementPrototype.localPosition;
            elementPrototype.gameObject.SetActive(false);
            Refresh();
        }

        // Update is called once per frame
        public void Refresh()
        {
            var cursor = firstItem;
            for (var i = 1; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
            var displayCount = 0;
            foreach (var i in model.InventoryItems)
            {
                var count = model.GetInventoryCount(i);
                if (count <= 0) continue;
                displayCount++;
                var e = Instantiate(elementPrototype);
                e.transform.SetParent(transform);
                e.transform.localPosition = cursor;
                e.transform.GetChild(0).GetComponent<Image>().sprite = model.GetInventorySprite(i);
                e.transform.GetChild(1).GetComponent<TMP_Text>().text = $"x {count}";
                e.gameObject.SetActive(true);
                cursor.y -= stepSize;
            }

            if (displayCount > 0)
                _image.color = new Color(1, 1, 1, 1);
            else
                _image.color = new Color(1, 1, 1, 0);
        }
    }
}