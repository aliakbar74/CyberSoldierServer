using System.Numerics;

namespace CyberSoldierServer.Models {
	public class Transform {
		public int InstanceId { get; set; }
		public int ParentInstanceId { get; set; }
		public string PrefabName { get; set; }
		public Position Position { get; set; }
		public Quaternion Rotation { get; set; }
		public Vector3 Scale { get; set; }
	}
}
