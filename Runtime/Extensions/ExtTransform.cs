using UnityEngine;

static public class ExtTransform
{
	public static Vector3 TransformPointUnscaled(this Transform transform, Vector3 position)
	{
		var localToWorldMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		return localToWorldMatrix.MultiplyPoint3x4(position);
	}

	public static Vector3 InverseTransformPointUnscaled(this Transform transform, Vector3 position)
	{
		var worldToLocalMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one).inverse;
		return worldToLocalMatrix.MultiplyPoint3x4(position);
	}

	static public string getFullTransformHierarchyPathToString(this Transform tr)
	{
		string path = "";
		while (tr != null)
		{
			path = tr.name + "/" + path;
			tr = tr.parent;
		}
		return path;
	}

	/// <summary>
	/// Retourne le nombre de parent du Transform.
	/// </summary>
	/// <param name="transform">Le Transform cibl�.</param>
	/// <returns>Le nombre de Transform parent, 0 si ce Transform n'a pas de parent (le pauvre).</returns>
	static public int getHierarchyDepth(this Transform transform)
	{
		int depth = 0;

		Transform parent = transform.parent;

		while (parent != null)
		{
			depth++;

			parent = parent.parent;
		}

		return depth;

	}// getHierarchyDepth()

	/// <summary>
	/// Retourne une chaine de la hierarchie du Transform dans la sc�ne.
	/// </summary>
	/// <param name="transform">Le Transform cible.</param>
	/// <returns>Le chemin d'acc�s au Transform dans la sc�ne.</returns>
	static public string getHierarchyPathToString(this Transform transform)
	{
		string path = transform.name;

		Transform parent = transform.parent;

		while (parent != null)
		{
			path = parent.name + "/" + path;

			parent = parent.parent;
		}

		return path;
	}


	/// <summary>
	/// Permet de remonter a un parent pr�cis dans la hi�rarchie.
	/// </summary>
	/// <param name="transform">Le Transform de d�part</param>
	/// <param name="reverseHierarchyStep">Le nombre d'�tape pour remontrer la hi�rarchie.</param>
	/// <returns>La r�f�rence du Transform parent.</returns>
	static public Transform getTransformParent(this Transform transform, int reverseHierarchyStep = -1)
	{
		if (reverseHierarchyStep > 0)
		{
			Debug.LogWarning("reverseHierarchyStep doit toujours �tre inf�rieur ou �gal � 0 !");

			return null;
		}

		while (reverseHierarchyStep < 0 && transform != null)
		{
			transform = transform.parent;

			reverseHierarchyStep++;
		}

		return transform;
	}

	/// <summary>
	/// returns first child containing param name
	/// </summary>
	/// <param name="warning">G�n�re-t-on un warning si on ne trouve rien ?</param>
	static public Transform looselyFindParent(this Transform transform, string name, bool strict = false, bool warning = true)
	{
		Transform parent = transform.parent;

		if (!string.IsNullOrEmpty(name))
		{
			while (parent != null)
			{
				if (strict && parent.name == name) return parent;
				else if (parent.name.Contains(name))
				{
					return parent;
				}
				else parent = parent.parent;
			}
		}

		if (warning) Debug.LogWarning("Can't find Transform's parent with name \"" + name + "\"", transform);

		return parent;

	}// looselyFindParent()


}
