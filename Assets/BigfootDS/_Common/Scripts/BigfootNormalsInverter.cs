using UnityEngine;
using System.Collections;
using BigfootDS;
namespace BigfootDS {

	public class BigfootNormalsInverter : MonoBehaviour {

		[Tooltip("Leave this blank to automatically fetch the MeshFilter component on the object that this script is placed on.")]
        /// <summary>
        /// The specified mesh that will have its normals inverted. The script will automatically fetch this component on the same object if this field is left blank.
        /// </summary>
		public MeshFilter meshToInvert;
		[Tooltip("Automatically invert the normals when the game starts.")]
        /// <summary>
        /// If enabled, this will invert the normals of the desired mesh as soon as physically possible.
        /// </summary>
		public bool autoInverter = true;

		void Start () {
			if (autoInverter)
				InvertMeshNormals ();
		}

		/// <summary>
		/// Inverts the normals of a mesh as specified by the BigfootNormalsInverter component.
		/// </summary>
		[ContextMenu("Invert Normals Now")]
		public void InvertMeshNormals () {
			MeshFilter filter = new MeshFilter ();
			if (meshToInvert != null) {
				filter = meshToInvert;
			} else {
				filter = GetComponent (typeof(MeshFilter)) as MeshFilter;
			}

			if (filter != null)
			{
				Mesh mesh = filter.mesh;

				Vector3[] normals = mesh.normals;
				for (int i=0;i<normals.Length;i++)
					normals[i] = -normals[i];
				mesh.normals = normals;

				for (int m=0;m<mesh.subMeshCount;m++)
				{
					int[] triangles = mesh.GetTriangles(m);
					for (int i=0;i<triangles.Length;i+=3)
					{
						int temp = triangles[i + 0];
						triangles[i + 0] = triangles[i + 1];
						triangles[i + 1] = temp;
					}
					mesh.SetTriangles(triangles, m);
				}
			}
		}
	}
}