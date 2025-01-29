using Kompas6API5;

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
		/// Свойство для экземпляра класса KompasObject для взаимодействия с API Kompas-3D.
		/// </summary>
		public KompasObject Kompas => _kompas;

		/// <summary>
		/// Выполняет подключение к САПР.
		/// </summary>
		public void ConnectToKompas()
		{
			if (_kompas != null)
			{
				return;
			}
			
			var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
			_kompas = (KompasObject)Activator.CreateInstance(kompasType);
			_kompas.Visible = true;
		}
	}
}
