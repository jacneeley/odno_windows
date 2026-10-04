using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using OdnoWindowsApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Core
{
    internal class OdnoException : Exception
    {

		private string _message;
		private Exception cause;

		public Exception Cause
		{
			get { return cause; }
			set { cause = value; }
		}

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}


		public OdnoException() { }

        public OdnoException(Exception e) { 
			_message = e.Message;
			cause = e;
		}

        public OdnoException(string message, Exception e) {
			_message = message;
			cause = e;
		}

		public static void HandleException(OdnoException oe, string desc, string caller) {
			string? msg;
			if (oe == null)
			{
				oe = new OdnoException();
				oe.Message = desc;
				msg = desc;
			}
			else {
                msg = $"ERROR: {oe._message} with cause: {oe.cause}. Description: {desc} @ {caller}";
            }

			OdnoLogger.LogError(oe, msg);
		}

		public static void CriticalException(OdnoException oe, string desc, string caller) {
			if (oe != null)
			{
				string msg = $"ERROR: {oe._message} with cause: {oe.cause}. Description: {desc} @ {caller}";
                OdnoLogger.LogError(oe, msg);

            }
			else {
                OdnoLogger.LogError(new OdnoException("Something bad and unexpected happened so bad, a proper exception was not captured...", new Exception(desc)), desc);
            }

			MessageBox.Show("Catastrophic Error Occurred. This program will exit to preserve any data.");
			Environment.Exit(0);
        }

		public override string ToString() {
			return $"OdnoException: {Message} - caused by {cause}";
		}
    }
}
