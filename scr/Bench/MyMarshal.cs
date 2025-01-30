using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Bench
{
	/// <summary>
	/// Необходим для реализации метода <see cref="GetActiveObject(string)"/>,
	/// который доступен только в .Net Framework.
	/// </summary>
	public static class MyMarshal
	{
		/// <summary>
		/// Получает обработчик программы по ID.
		/// </summary>
		/// <param name="progID">ID программы.</param>
		/// <returns>Обработчик программы.</returns>
		[SecurityCritical]
		public static object GetActiveObject(string progID)
		{
			object? ppunk = null;
			Guid clsid;
			try
			{
				CLSIDFromProgIDEx(progID, out clsid);
			}
			catch (Exception)
			{
				CLSIDFromProgID(progID, out clsid);
			}

			GetActiveObject(ref clsid, IntPtr.Zero, out ppunk);
			return ppunk;
		}

		[DllImport("ole32.dll", PreserveSig = false)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private static extern void CLSIDFromProgIDEx(
			[MarshalAs(UnmanagedType.LPWStr)] string progId,
			out Guid clsid);

		[DllImport("ole32.dll", PreserveSig = false)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private static extern void CLSIDFromProgID(
			[MarshalAs(UnmanagedType.LPWStr)] string progId,
			out Guid clsid);

		[DllImport("oleaut32.dll", PreserveSig = false)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private static extern void GetActiveObject(
			ref Guid rclsid,
			IntPtr reserved,
			[MarshalAs(UnmanagedType.Interface)] out object ppunk);
	}
}
