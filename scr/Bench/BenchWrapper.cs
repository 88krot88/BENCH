using Kompas6API5;
using System.Runtime.InteropServices;

namespace Bench
{
	/// <summary>
	/// Выполняет подключение к Kompas-3D.
	/// </summary>
	public class BenchWrapper
	{
		/// <summary>
		/// Экземпляр класса KompasObject для взаимодействия с API Kompas-3D.
		/// </summary>
		private KompasObject _kompas;

		/// <summary>
		/// Имя идентификатора Kompas-3D.
		/// </summary>
		private const string CompassProgramId = "KOMPAS.Application.5";

		/// <summary>
		/// Свойство для экземпляра класса KompasObject для взаимодействия с API Kompas-3D.
		/// </summary>
		public KompasObject Kompas => _kompas;

		/// <summary>
		/// Выполняет подключение к САПР.
		/// </summary>
		public void ConnectToKompas()
		{
			try
			{
				_kompas = (KompasObject)MyMarshal.GetActiveObject(CompassProgramId);
			}
			catch
			{
				var kompasType = Type.GetTypeFromProgID(CompassProgramId);
				_kompas = (KompasObject)Activator.CreateInstance(kompasType);
			}

			_kompas.Visible = true;
		}
	}
}
