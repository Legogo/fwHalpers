using UnityEngine;

static public class ExtObject
{

    /// <summary>
    /// Encapsule (obj != null && obj.GetType() == typeof())
    /// </summary>
    /// <param name="obj">Le UnityEngine.Object cible.</param>
    /// <param name="type">Le Type de l'objet.</param>
    /// <returns>True si le Type est bon.</returns>
    static public bool isTypeOf(this Object obj, System.Type type)
    {
        return obj != null && obj.GetType() == type;

    }// isTypeOf()

}
