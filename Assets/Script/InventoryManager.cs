using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Image[] InventarioIcono = new Image[12];

    // La lista principal que almacena los objetos del inventario
    public List<Item> objetosEnInventario = new List<Item>();
    public Sprite spriteVacio;
    public GameObject botonBorrar;

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
    
    public void MostrarInventario()
    {
        int contador = 0;
        for(int i = 0; i < InventarioIcono.Length; i++)
        {
            InventarioIcono[i].sprite = spriteVacio; // Limpiar iconos anteriores
        }
        foreach (Item item in objetosEnInventario)
        {

           InventarioIcono[contador].sprite = item.icono;
           contador++;
        }
    }
    public void BuscarItemPorIcono(Image iconoBuscado)
    {
        Debug.Log("Buscando objeto con icono: " + iconoBuscado.sprite.name);
        Item itemEncontrado = objetosEnInventario.Find(item => item.icono == iconoBuscado.sprite);
        if (itemEncontrado != null)
        {
            EncenderBorrado(itemEncontrado);
        }
        else
        {
            Debug.Log("Objeto no encontrado en el inventario.");
        }
    }

    public void EncenderBorrado(Item itemseleccionado)
    {
        botonBorrar.SetActive(true);
        botonBorrar.GetComponent<Button>().onClick.RemoveAllListeners();
        botonBorrar.GetComponent<Button>().onClick.AddListener(() => RemoverItem(itemseleccionado));
    }

    public void RemoverItem(Item itemRemover)
    {
        if (objetosEnInventario.Contains(itemRemover))
        {
            botonBorrar.SetActive(false);
            
            objetosEnInventario.Remove(itemRemover);
            MostrarInventario();
        }
    }
}
