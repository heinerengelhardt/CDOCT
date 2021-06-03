using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CDOCT
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Form1.
	/// </summary>
	public class Form : System.Windows.Forms.Form
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Form
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 270);
			this.Name = "Form";
			this.Text = "Form";

		}
		#endregion

		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			RegistryKey myKey = Registry.CurrentUser.CreateSubKey("Software\\CDOCT");
			string strCDOpenCloseStatus = (string) myKey.GetValue("CDOpenCloseStatus");
			if (strCDOpenCloseStatus == "" || strCDOpenCloseStatus == null) 
			{
				myKey.SetValue("CDOpenCloseStatus", "CLOSED");
			}
	
			if (strCDOpenCloseStatus == "CLOSED" ) 
			{
				mciSendString("set cdaudio door open", null, 0, IntPtr.Zero);
				myKey.SetValue("CDOpenCloseStatus", "OPEN");
			}

			if (strCDOpenCloseStatus == "OPEN" )  
			{
				mciSendString("set cdaudio door closed", null, 0, IntPtr.Zero);
				myKey.SetValue("CDOpenCloseStatus", "CLOSED");
			}
		}

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi)]
		protected static extern int mciSendString(string lpstrCommand,
			                                      string lpstrReturnString,
			                                      int uReturnLength,
			                                      IntPtr hwndCallback);
	}
}
