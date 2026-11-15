using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // La lista principal que almacena los objetos del inventario
    public List<Item> objetosEnInventario = new List<Item>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AgregarItem(Item nuevoItem)
    {
        // Verificar si el objeto ya está en la lista (si es apILABLE)
        Item itemExistente = objetosEnInventario.Find(i => i.id == nuevoItem.id);

        if (itemExistente != null)
        {
            itemExistente.cantidad += nuevoItem.cantidad;
        }
        else
        {
            // Creamos una instancia o añadimos directamente
            objetosEnInventario.Add(nuevoItem);
        }

        Debug.Log("Añadido: " + nuevoItem.nombreItem);
    }

    public void RemoverItem(Item itemRemover)
    {
        if (objetosEnInventario.Contains(itemRemover))
        {
            objetosEnInventario.Remove(itemRemover);
        }
    }
}
