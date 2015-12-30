// Clases para el envio de emails
// Autor original: Eduardo A. Morcillo
// E-Mail: emorcillo@mvps.org
// Pagina: http://www.mvps.org/emorcillo

//Convertidas desde Visual Basic .NET a C# por aNTRaX




using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;


namespace System.aTxLiB.Mail
{
	/// <summary>
	/// Contiene funciones para hacer búsquedas DNS.
	/// </summary>
	public sealed class Dns
	{
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// DNS Api
		/// </summary>
		/// -----------------------------------------------------------------------------
		[System.Security.SuppressUnmanagedCodeSecurity()]private sealed class UnsafeNativeMethods
		{
			
			private UnsafeNativeMethods() 
			{
			}
			
			[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]public struct DNS_A_DATA
			{
				public int IpAddress;
			}
			
			[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]public struct DNS_MX_DATA
			{
				public IntPtr pNameExchange;
				public short wPreference;
				public short Pad;
			}
			
			[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]public struct DNS_PTR_DATA
			{
				public IntPtr pNameHost;
			}
			
			[StructLayout(LayoutKind.Explicit, CharSet=CharSet.Auto)]public struct DnsRecord
			{
				
				[FieldOffset(0)]public IntPtr pNext;
				[FieldOffset(4)]public string pName;
				[FieldOffset(8)]public short wType;
				[FieldOffset(10)]public short wDataLength;
				[FieldOffset(12)]public int flags;
				[FieldOffset(16)]public int dwTtl;
				[FieldOffset(20)]public int dwReserved;
				[FieldOffset(24)]public DNS_A_DATA A;
				[FieldOffset(24)]public DNS_PTR_DATA NS;
				[FieldOffset(24)]public DNS_PTR_DATA PTR;
				[FieldOffset(24)]public DNS_MX_DATA MX;
				
			}
			
			public enum DnsQueryOptions
			{
				DNS_QUERY_STANDARD = 0x0,
				DNS_QUERY_ACCEPT_TRUNCATED_RESPONSE = 0x1,
				DNS_QUERY_USE_TCP_ONLY = 0x2,
				DNS_QUERY_NO_RECURSION = 0x4,
				DNS_QUERY_BYPASS_CACHE = 0x8,
				DNS_QUERY_NO_WIRE_QUERY = 0x10,
				DNS_QUERY_NO_LOCAL_NAME = 0x20,
				DNS_QUERY_NO_HOSTS_FILE = 0x40,
				DNS_QUERY_NO_NETBT = 0x80,
				DNS_QUERY_WIRE_ONLY = 0x100,
				DNS_QUERY_RETURN_MESSAGE = 0x200,
				DNS_QUERY_TREAT_AS_FQDN = 0x1000,
				DNS_QUERY_DONT_RESET_TTL_VALUES = 0x100000,
				DNS_QUERY_RESERVED =  0xFF0000
			}
			
			public enum DnsQueryTypes
			{
				DNS_TYPE_A = 0x1,
				DNS_TYPE_NS = 0x2,
				DNS_TYPE_MD = 0x3,
				DNS_TYPE_MF = 0x4,
				DNS_TYPE_CNAME = 0x5,
				DNS_TYPE_SOA = 0x6,
				DNS_TYPE_MB = 0x7,
				DNS_TYPE_MG = 0x8,
				DNS_TYPE_MR = 0x9,
				DNS_TYPE_NULL = 0xA,
				DNS_TYPE_WKS = 0xB,
				DNS_TYPE_PTR = 0xC,
				DNS_TYPE_HINFO = 0xD,
				DNS_TYPE_MINFO = 0xE,
				DNS_TYPE_MX = 0xF,
				DNS_TYPE_TEXT = 0x10,
				DNS_TYPE_RP = 0x11,
				DNS_TYPE_AFSDB = 0x12,
				DNS_TYPE_X25 = 0x13,
				DNS_TYPE_ISDN = 0x14,
				DNS_TYPE_RT = 0x15,
				DNS_TYPE_NSAP = 0x16,
				DNS_TYPE_NSAPPTR = 0x17,
				DNS_TYPE_SIG = 0x18,
				DNS_TYPE_KEY = 0x19,
				DNS_TYPE_PX = 0x1A,
				DNS_TYPE_GPOS = 0x1B,
				DNS_TYPE_AAAA = 0x1C,
				DNS_TYPE_LOC = 0x1D,
				DNS_TYPE_NXT = 0x1E,
				DNS_TYPE_EID = 0x1F,
				DNS_TYPE_NIMLOC = 0x20,
				DNS_TYPE_SRV = 0x21,
				DNS_TYPE_ATMA = 0x22,
				DNS_TYPE_NAPTR = 0x23,
				DNS_TYPE_KX = 0x24,
				DNS_TYPE_CERT = 0x25,
				DNS_TYPE_A6 = 0x26,
				DNS_TYPE_DNAME = 0x27,
				DNS_TYPE_SINK = 0x28,
				DNS_TYPE_OPT = 0x29,
				DNS_TYPE_UINFO = 0x64,
				DNS_TYPE_UID = 0x65,
				DNS_TYPE_GID = 0x66,
				DNS_TYPE_UNSPEC = 0x67,
				DNS_TYPE_ADDRS = 0xF8,
				DNS_TYPE_TKEY = 0xF9,
				DNS_TYPE_TSIG = 0xFA,
				DNS_TYPE_IXFR = 0xFB,
				DNS_TYPE_AXFR = 0xFC,
				DNS_TYPE_MAILB = 0xfd,
				DNS_TYPE_MAILA = 0xFE,
				DNS_TYPE_ALL = 0xFF,
				DNS_TYPE_ANY = 0xFF
			}
			
			[DllImport("dnsapi",EntryPoint="DnsQuery_W", ExactSpelling=true, CharSet=CharSet.Unicode, SetLastError=true)]
			public static extern int DnsQuery(string pszName, DnsQueryTypes wType, DnsQueryOptions options, int aipServers, ref IntPtr ppQueryResults, int pReserved);
			
			[DllImport("dnsapi", ExactSpelling=true, CharSet=CharSet.Auto, SetLastError=true)]
			public static extern void DnsRecordListFree(IntPtr pRecordList, int FreeType);
			
		}
		
		static Dns() 
		{
			
			// Check if the machine is running Win2K, XP, 2003 Server
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw (new NotSupportedException());
			}
			else if (Environment.OSVersion.Version.Major < 5)
			{
				throw (new NotSupportedException());
			}
			
		}
		
		private Dns() 
		{
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the authoritative name servers of a domain.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static string[] GetAuthoritativeNameServers(string domain)
		{
			
			IntPtr ptr=new IntPtr(0);
			int result;
			ArrayList servers = new ArrayList();
			
			// Query the DNS server for the NS records
			result = UnsafeNativeMethods.DnsQuery(domain, UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_NS, UnsafeNativeMethods.DnsQueryOptions.DNS_QUERY_BYPASS_CACHE, 0, ref ptr, 0);
			
			if (result == 0)
			{
				
				IntPtr ptrNext = ptr;
				
				// Enumerate all returned records
				while (!(ptrNext.Equals(IntPtr.Zero)))
				{
					
					// Get the record from the pointer
					UnsafeNativeMethods.DnsRecord record = ((UnsafeNativeMethods.DnsRecord) Marshal.PtrToStructure(ptrNext, typeof(UnsafeNativeMethods.DnsRecord)));
					
					// Get only the NS records
					if (record.wType == (short)UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_NS)
					{
						
						// Add the host name to the list
						servers.Add(Marshal.PtrToStringAuto(record.NS.pNameHost));
						
					}
					
					// Get the pointer to the next record
					ptrNext = record.pNext;
					
				}
				
				// Release the record list
				UnsafeNativeMethods.DnsRecordListFree(ptr, 0);
				
			}
			else
			{
				
				// Throw the exception
				throw (new Win32Exception(result));
				
			}
			
			// Convert the list to an array and return it
			return ((string[]) servers.ToArray(typeof(string)));
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the canonical name of a host.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static string GetCanonicalName(string host)
		{
			
			IntPtr ptr=new IntPtr(0);
			int result;
			string hostName=string.Empty;
			
			// Query the DNS hostName for the CNAME record
			result = UnsafeNativeMethods.DnsQuery(host, UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_CNAME, UnsafeNativeMethods.DnsQueryOptions.DNS_QUERY_BYPASS_CACHE, 0, ref ptr, 0);
			
			if (result == 0)
			{
				
				IntPtr ptrNext = ptr;
				
				// Enumerate all returned records
			endOfDoLoop:
				while (!(ptrNext.Equals(IntPtr.Zero)))
				{
					
					// Get the record from the pointer
					UnsafeNativeMethods.DnsRecord record = ((UnsafeNativeMethods.DnsRecord) Marshal.PtrToStructure(ptrNext, typeof(UnsafeNativeMethods.DnsRecord)));
					
					// Check if the record type is CNAME
					if (record.wType == (short)UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_CNAME)
					{
						
						// Store the host name
						hostName = Marshal.PtrToStringAuto(record.PTR.pNameHost);
						
						goto endOfDoLoop;
						
					}
					
					// Get the pointer to the next record
					ptrNext = record.pNext;
					
				}
				
				// Release the record list
				UnsafeNativeMethods.DnsRecordListFree(ptr, 0);
				
			}
			else
			{
				
				// Throw the exception
				throw (new Win32Exception(result));
				
			}
			
			// Return the host name
			return hostName;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the host name from a IP (reverse lookup)
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static string GetHostName(System.Net.IPAddress ip)
		{
			
			IntPtr ptr=new IntPtr(0);
			int result;
			string hostName=null;
			byte[] addrByte = ip.GetAddressBytes();
			string address;
			
			// Convert the IP to IN-ADDR.ARPA format
			address = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}.IN-ADDR.ARPA", addrByte[3], addrByte[2], addrByte[1], addrByte[0]);
			
			// Query the PTR records for the host
			result = UnsafeNativeMethods.DnsQuery(address, UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_PTR, UnsafeNativeMethods.DnsQueryOptions.DNS_QUERY_BYPASS_CACHE | UnsafeNativeMethods.DnsQueryOptions.DNS_QUERY_NO_LOCAL_NAME, 0, ref ptr, 0);
			
			if (result == 0)
			{
				
				IntPtr ptrNext = ptr;
				
				// Enumerate all returned records
				while (!(ptrNext.Equals(IntPtr.Zero)))
				{
					
					// Get the record from the pointer
					UnsafeNativeMethods.DnsRecord record = ((UnsafeNativeMethods.DnsRecord) Marshal.PtrToStructure(ptrNext, typeof(UnsafeNativeMethods.DnsRecord)));
					
					if (record.wType == (short)UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_PTR)
					{
						
						// Return the host name
						hostName = Marshal.PtrToStringAuto(record.PTR.pNameHost);
						
						break;
						
					}
					
					// Get the pointer to the next record
					ptrNext = record.pNext;
					
				}
				
				// Release the record list
				UnsafeNativeMethods.DnsRecordListFree(ptr, 0);
				
			}
			else
			{
				
				// Throw the exception
				throw (new Win32Exception(result));
				
			}
			
			// Return the host name
			return hostName;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the  IP addresses of a host
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static System.Net.IPAddress[] GetIPAddresses(string host)
		{
			
			IntPtr ptr=new IntPtr(0);
			int result;
			ArrayList servers = new ArrayList();
			
			// Query the DNS server for the A records
			result = UnsafeNativeMethods.DnsQuery(host, UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_A, UnsafeNativeMethods.DnsQueryOptions.DNS_QUERY_BYPASS_CACHE, 0, ref ptr, 0);
			
			if (result == 0)
			{
				
				IntPtr ptrNext = ptr;
				
				// Enumerate all returned records
				while (!(ptrNext.Equals(IntPtr.Zero)))
				{
					
					// Get the record from the pointer
					UnsafeNativeMethods.DnsRecord record = ((UnsafeNativeMethods.DnsRecord) Marshal.PtrToStructure(ptrNext, typeof(UnsafeNativeMethods.DnsRecord)));
					
					// Check if the record type is A
					if (record.wType == (short)UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_A)
					{
						
						// Add the ip address to the list
						servers.Add(new System.Net.IPAddress(record.A.IpAddress));
						
					}
					
					// Get the pointer to the next record
					ptrNext = record.pNext;
					
				}
				
				// Release the record list
				UnsafeNativeMethods.DnsRecordListFree(ptr, 0);
				
			}
			else
			{
				
				// Throw the exception
				throw (new Win32Exception(result));
				
			}
			
			// Return the ips
			return ((System.Net.IPAddress[]) servers.ToArray(typeof(System.Net.IPAddress)));
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the mail exchanger servers of a domain.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static MailExchanger[] GetMailExchangers(string domain)
		{
			
			IntPtr ptr=new IntPtr(0);
			int result;
			ArrayList servers = new ArrayList();
			
			// Query the DNS server for the MX records
			result = UnsafeNativeMethods.DnsQuery(domain, UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_MX, UnsafeNativeMethods.DnsQueryOptions.DNS_QUERY_BYPASS_CACHE, 0, ref ptr, 0);
			
			if (result == 0)
			{
				
				IntPtr ptrNext = ptr;
				
				// Enumerate all returned records
				while (!(ptrNext.Equals(IntPtr.Zero)))
				{
					
					// Get the record from the pointer
					UnsafeNativeMethods.DnsRecord record;
					record = ((UnsafeNativeMethods.DnsRecord) Marshal.PtrToStructure(ptrNext, typeof(UnsafeNativeMethods.DnsRecord)));
					
					// Check if the record is a MX record
					if (record.wType == (short)UnsafeNativeMethods.DnsQueryTypes.DNS_TYPE_MX)
					{
						
						// Add the server to the list
						servers.Add(new MailExchanger(Marshal.PtrToStringAuto(record.MX.pNameExchange), record.MX.wPreference));
						
					}
					
					// Get the pointer to the next record
					ptrNext = record.pNext;
					
				}
				
				// Release the record list
				UnsafeNativeMethods.DnsRecordListFree(ptr, 0);
				
			}
			else
			{
				
				// Throw the exception
				throw (new Win32Exception(result));
				
			}
			
			// Sort the servers in preference order
			servers.Sort();
			
			// Return the server array
			return ((MailExchanger[]) servers.ToArray(typeof(MailExchanger)));
			
		}
		
	}
	/// <summary>
	/// Envía correos electrónicos (MailMessage) directamente a servidores MX o a servidores relay. La clase soporta autenticación CRAM-MD5 en el servidor SMTP.
	/// </summary>
	public sealed class SmtpMail
	{
		
		private const string SMTP_AUTH_OK = "334";
		private const string SMTP_AUTHENTICATED = "235";
		private const string SMTP_OK = "250";
		private const string SMTP_OK_CONNECT = "220";
		private const string SMTP_START_DATA = "354";
		
		private static readonly Regex _mailRegEx = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", RegexOptions.Compiled);
		
		private static NetworkCredential _credential = new NetworkCredential();
		private static MailSendMode _mode = MailSendMode.ToMailExchanger;
		private static int _port = 25;
		private static string _server = "localhost";
		
		private SmtpMail() 
		{
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Authenticates on the server using CRAM-MD5
		/// </summary>
		/// -----------------------------------------------------------------------------
		private static bool AuthCramMd5(TcpClient smtp, string user, string password)
		{
			
			string recv;
			System.Security.Cryptography.HMACMD5 hmacmd5 = new System.Security.Cryptography.HMACMD5
                (Encoding.ASCII.GetBytes(password));
			byte[] hash;
			
			SendData(smtp, "AUTH CRAM-MD5");
			
			recv = ReceiveData(smtp);
			
			if (recv.StartsWith(SMTP_AUTH_OK))
			{
				
				try
				{
					
					// Get the base64 challenge and decode it
					byte[] challenge = Convert.FromBase64String(recv.Substring(4, recv.IndexOf(Environment.NewLine) - 4));
					
					// Hash the challenge using hmacmd5-MD5
					hash = hmacmd5.ComputeHash(challenge);
					
					// Create the response
					StringBuilder response = new StringBuilder();
					
					// Add the user name and a white space
					response.Append(user);
					response.Append(" ");
					
					// Add the hash in HEX format
					for (int i = 0; i <= hash.Length - 1; i++)
					{
						response.AppendFormat("{0:x2}", hash[i]);
					}
					
					// Convert the response to base64 and send it
					SendData(smtp, Convert.ToBase64String(Encoding.ASCII.GetBytes(response.ToString())));
					
					recv = ReceiveData(smtp);
					if (! recv.StartsWith(SMTP_AUTHENTICATED))
					{
						throw (new MailException(recv));
					}
					
				}
				finally
				{
					
					((IDisposable) hmacmd5).Dispose();
					
				}
				
			}
			else
			{
				
				throw (new MailException(recv));
				
			}
			return true;
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns whether the mail format is correct.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static bool CheckMailFormat(string mailAddress)
		{
			
			return _mailRegEx.IsMatch(mailAddress);
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns whether a mail address has correct format and exists on the domain.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static bool IsValidMailAddress(string mailAddress)
		{
			
			// Check mail format first
			if (SmtpMail.CheckMailFormat(mailAddress))
			{
				
				// Get the domain from the mail address
				string domain = mailAddress.Split('@')[1];
				
				// Get the mail exchanger servers for the domain
				MailExchanger[] mx = Dns.GetMailExchangers(domain);
				
				for (int i = 0; i <= mx.Length - 1; i++)
				{
					
					TcpClient smtp=null;
					string recv;
					
					try
					{
						
						// Connect to the server
						smtp = new TcpClient(mx[i].HostName, 25);
						smtp.ReceiveTimeout = 15000;
						
						// Check connection
						recv = ReceiveData(smtp);
						if (recv.StartsWith(SMTP_OK_CONNECT))
						{
							
							// Say HELLO to server
							SendData(smtp, "HELO " + System.Net.Dns.GetHostName());
							
							// OK?
							recv = ReceiveData(smtp);
							if (recv.StartsWith(SMTP_OK))
							{
								
								// Send a fake address as the mail sender
								SendData(smtp, "MAIL FROM: <test@test.com>");
								
								// OK?
								recv = ReceiveData(smtp);
								if (recv.StartsWith(SMTP_OK))
								{
									
									// Send the mail address to check
									SendData(smtp, "RCPT TO: <" + mailAddress + ">");
									
									// OK?
									recv = ReceiveData(smtp);
									if (recv.StartsWith(SMTP_OK))
									{
										
										// The mail address is OK!
										return true;
										
									}
									
								}
								
							}
							
						}
						
					}
					catch
					{
						
						// Ignoro cualquier excepcion
						
					}
					finally
					{
						
						// Cierro la conexion
						if (smtp != null)
						{
							
							// Tell the server to close the connection
							SendData(smtp, "QUIT");
							
							// Close the connection
							smtp.GetStream().Close();
							smtp.Close();
							
						}
						
						smtp = null;
						
					}
					
				}
				
			}
			
			// The mail address doesn't exists in the domain
			return false;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Reads data from the connection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		private static string ReceiveData(TcpClient cliente)
		{
			
			NetworkStream stream = cliente.GetStream();
			byte[] data = new byte[cliente.ReceiveBufferSize-1 + 1];
			
			stream.Read(data, 0, cliente.ReceiveBufferSize);
			
			return Encoding.ASCII.GetString(data).TrimEnd(Environment.NewLine.ToCharArray(0,1)[0]);
			
		}

		private static string GetString(string name)
		{
			
			System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources", System.Reflection.Assembly.GetCallingAssembly());
			
			return manager.GetString(name, System.Globalization.CultureInfo.CurrentUICulture);
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Envía un MailMessage
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static void Send (MailMessage message)
		{
			
			if (message.Sender == null)
			{
				throw (new InvalidOperationException(GetString("MailNoSender")));
			}
			if (message.@To.Count == 0 && message.Bcc.Count == 0)
			{
				throw (new InvalidOperationException(GetString("MailNoRecipient")));
			}
			
			string messageBody = message.GetMessageBody(true).ToString();
			MailRecipientCollection recipients = new MailRecipientCollection();
			
			recipients.Add(message.@To);
			recipients.Add(message.CC);
			recipients.Add(message.Bcc);
			
			if (_mode == MailSendMode.ToMailExchanger)
			{
				
				for (int j = 0; j <= recipients.Count - 1; j++)
				{
					
					// Get the domain from the mail
					string domain = recipients[j].MailAddress.Split('@')[1];
					
					// Get the SMTP servers for the domain
					MailExchanger[] mx = Dns.GetMailExchangers(domain);
					
					for (int i = 0; i <= mx.Length - 1; i++)
					{
						
						try
						{
							
							SendMx(mx[i].HostName, message.Sender, recipients[j], messageBody);
							goto endOfForLoop;
							
						}
						catch
						{
							
							// Rethrow the exception only on the last sever
							if (i == mx.Length - 1)
							{
								throw;
							}
							
						}
						
					}
				endOfForLoop:
					1.GetHashCode() ; //nop
					
				}
				
			}
			else
			{
				
				SendRelay(_server, _port, _credential, message.Sender, recipients, messageBody);
				
			}
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Sends a MailMessage to the specified relay server.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static void Send (MailMessage message, string server, int port, NetworkCredential credential)
		{
			
			if (message.Sender == null)
			{
				throw (new InvalidOperationException(GetString("MailNoSender")));
			}
			if (message.@To.Count == 0 && message.Bcc.Count == 0)
			{
				throw (new InvalidOperationException(GetString("MailNoRecipient")));
			}
			
			string messageBody = message.GetMessageBody(true).ToString();
			MailRecipientCollection recipients = new MailRecipientCollection();
			
			recipients.Add(message.@To);
			recipients.Add(message.CC);
			recipients.Add(message.Bcc);
			
			SendRelay(server, port, credential, message.Sender, recipients, messageBody);
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Sends a MailMessage.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static void Send (MailMessage message, NetworkCredential credential)
		{
			
			Send(message, _server, _port, credential);
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Sends a MailMessage.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static void Send (MailMessage message, string server, int port)
		{
			
			Send(message, server, port, _credential);
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Sends data over the connection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		private static void SendData (TcpClient cliente, string texto)
		{
			
			NetworkStream stream = cliente.GetStream();
			byte[] data = Encoding.UTF8.GetBytes(texto + Environment.NewLine);
			
			stream.Write(data, 0, data.Length);
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Sends a mail to a mail exchanger.
		/// </summary>
		/// -----------------------------------------------------------------------------
		private static void SendMx (string smtpServer, MailRecipient sender, MailRecipient recipient, string message)
		{
			
			TcpClient smtpClient=null;
			string recv;
			
			try
			{				
				// Open the connection to the smtpClient smtpServer
				smtpClient = new TcpClient(smtpServer, 25);
				
				// Set the receive timeout to 15 secs
				smtpClient.ReceiveTimeout = 15000;
				
				// Check for response from the smtpServer
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK_CONNECT))
				{
					throw (new MailException(recv));
				}
				
				// Send HELO command to smtpServer
				SendData(smtpClient, "HELO " + System.Net.Dns.GetHostName());
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK))
				{
					throw (new MailException(recv));
				}
				
				// Send the sender to the smtpServer
				SendData(smtpClient, "MAIL FROM: <" + sender.MailAddress + ">");
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK))
				{
					throw (new MailException(recv));
				}
				
				// Send the recipient to the smtpServer
				SendData(smtpClient, "RCPT TO: <" + recipient.MailAddress + ">");
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK))
				{
					throw (new MailException(recv));
				}
				
				// Start sending the mail data
				SendData(smtpClient, "DATA");
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_START_DATA))
				{
					throw (new MailException(recv));
				}
				
				// Send the mail body
				SendData(smtpClient, message);
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK))
				{
					throw (new MailException(recv));
				}
				
				// Quit
				SendData(smtpClient, "QUIT");
				
			}
			finally
			{
				
				// Close the connection
				if (smtpClient != null)
				{
					smtpClient.GetStream().Close();
					smtpClient.Close();
				}
				
				smtpClient = null;
				
			}
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Sends a mail to a smtp relay server
		/// </summary>
		/// -----------------------------------------------------------------------------
		private static void SendRelay (string smtpServer, int port, NetworkCredential credential, MailRecipient sender, MailRecipientCollection recipient, string message)
		{
			
			TcpClient smtpClient=null;
			string recv;
			
			try
			{
				
				// Open the connection to the smtpClient smtpServer
				smtpClient = new TcpClient(smtpServer, port);
				
				// Set the receive timeout to 15 secs
				smtpClient.ReceiveTimeout = 15000;
				
				// Check for response from the smtpServer
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK_CONNECT))
				{
					throw (new MailException(recv));
				}
				
				// Send HELO command to smtpServer
				SendData(smtpClient, "EHLO " + System.Net.Dns.GetHostName());
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK))
				{
					throw (new MailException(recv));
				}
				
				// Try to login if we have an user name
				if (credential != null)
				{
					
					// Authenticate with CRAM-MD5
					AuthCramMd5(smtpClient, credential.UserName, credential.Password);
					
				}
				
				// Send the sender to the smtpServer
				SendData(smtpClient, "MAIL FROM: <" + sender.MailAddress + ">");
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK))
				{
					throw (new MailException(recv));
				}
				
				// Send the recipients to the smtpServer
				for (int i = 0; i <= recipient.Count - 1; i++)
				{
					
					SendData(smtpClient, "RCPT TO: <" + recipient[i].MailAddress + ">");
					
					recv = ReceiveData(smtpClient);
					if (! recv.StartsWith(SMTP_OK))
					{
						throw (new MailException(recv));
					}
					
				}
				
				// Try to send the message body
				SendData(smtpClient, "DATA");
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_START_DATA))
				{
					throw (new MailException(recv));
				}
				
				// Send the mail body
				SendData(smtpClient, message);
				
				recv = ReceiveData(smtpClient);
				if (! recv.StartsWith(SMTP_OK))
				{
					throw (new MailException(recv));
				}
				
				// Quit
				SendData(smtpClient, "QUIT");
				
			}
			finally
			{
				
				// Close the connection
				if (smtpClient != null)
				{
					smtpClient.GetStream().Close();
					smtpClient.Close();
				}
				
				smtpClient = null;
				
			}
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the network credential used to authenticate
		/// on the smtp relay server.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static NetworkCredential Credential
		{
			get
			{
				return _credential;
			}
			set
			{
				_credential = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the smtp relay server port.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static int Port
		{
			get
			{
				return _port;
			}
			set
			{
				_port = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the mode used to send the mails.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static MailSendMode SendMode
		{
			get
			{
				return _mode;
			}
			set
			{
				_mode = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the host name of the smtp relay server.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static string Server
		{
			get
			{
				return _server;
			}
			set
			{
				_server = value;
			}
		}
		
	}
	/// <summary>
	/// Representa un archivo adjunto de un correo electrónico.
	/// </summary>
	public sealed class MailAttachment
	{
		
		private ContentDisposition _contentDisposition;
		private string _contentType;
		private string _filename;
		private string _id;
		
		/// <summary>
		/// Inicializa una nueva instancia con el archivo especificado
		/// </summary>
		public MailAttachment(string path) 
		{
			_contentDisposition = ContentDisposition.Attachment;
			
			
			if (! File.Exists(path))
			{
				throw (new FileNotFoundException());
			}
			
			this.Path = path;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inicializa una nueva instancia con el archivo especificado
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailAttachment(string path, ContentDisposition disposition) : this(path) 
		{
			_contentDisposition = ContentDisposition.Attachment;
			
			
			
			_contentDisposition = disposition;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inicializa una nueva instancia con el archivo especificado
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailAttachment(string path, string contentType) : this(path) 
		{
			_contentDisposition = ContentDisposition.Attachment;
			
			
			
			_contentType = contentType;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		///  Inicializa una nueva instancia con el archivo especificado
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailAttachment(string path, string contentType, string id) : this(path) 
		{
			_contentDisposition = ContentDisposition.Attachment;
			
			
			
			_contentType = contentType;
			_id = id;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		///  Inicializa una nueva instancia con el archivo especificado
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailAttachment(string path, ContentDisposition disposition, string id) : this(path) 
		{
			_contentDisposition = ContentDisposition.Attachment;
			
			
			
			_contentDisposition = disposition;
			_id = id;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		///  Inicializa una nueva instancia con el archivo especificado
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailAttachment(string path, ContentDisposition disposition, string id, string contentType) : this(path) 
		{
			_contentDisposition = ContentDisposition.Attachment;
			
			
			
			_contentDisposition = disposition;
			_contentType = contentType;
			_id = id;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene el contenido del archivo
		/// </summary>
		/// -----------------------------------------------------------------------------
		internal byte[] GetData()
		{
			
			// Open the file for reading
			System.IO.FileStream file = new System.IO.FileStream(_filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
			
			try
			{
				
				// Initialize the buffer
				byte[] fileData = new byte[Convert.ToInt32(file.Length)-1 + 1];
				
				// Read the file
				file.Read(fileData, 0, Convert.ToInt32(file.Length));
				
				return fileData;
				
			}
			finally
			{
				
				// Close the file
				file.Close();
				
			}
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene o establece el contenido-disposición de la cabecera del adjunto
		/// </summary>
		/// -----------------------------------------------------------------------------
		public ContentDisposition ContentDisposition
		{
			get
			{
				return _contentDisposition;
			}
			set
			{
				_contentDisposition = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the Content-Id header of the attachement that can be
		/// used in a HTML message to embed the attachment.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string ContentId
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the attachment MIME format.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string ContentType
		{
			get
			{
				return _contentType;
			}
			set
			{
				_contentType = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the attachment file name.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string FileName
		{
			get
			{
				return System.IO.Path.GetFileName(_filename);
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the attachment full path.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string Path
		{
			get
			{
				return _filename;
			}
			set
			{
				
				if (! File.Exists(value))
				{
					throw (new FileNotFoundException());
				}
				
				_filename = value;
				_contentType = GetFileContentType(value);
				
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Retrieves the attachment MIME type from the registry.
		/// </summary>
		/// -----------------------------------------------------------------------------
		private static string GetFileContentType(string fileName)
		{
			
			RegistryKey key=null;
			
			try
			{
				
				key = Registry.ClassesRoot.OpenSubKey(System.IO.Path.GetExtension(fileName), false);
				
				return key.GetValue("Content Type", "application/octet-stream").ToString();
				
			}
			catch
			{
				
				return "application/octet-stream";
				
			}
			finally
			{
				
				if (key != null)
				{
					key.Close();
				}
				
			}
			
		}
		
	}
	/// <summary>
	/// Representa una coleccion de objetos de tipo MailAttachment
	/// </summary>
	public sealed class MailAttachmentCollection : ReadOnlyCollectionBase
	{
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Return wheter at least one attachment has a ContentDispotion = None.
		/// </summary>
		/// <remarks>
		/// This method is used in the message to know if the message type has to
		/// be set to multipart/related.
		/// </remarks>
		/// -----------------------------------------------------------------------------
		internal bool IsRelated()
		{
			for (int i = 0; i <= InnerList.Count - 1; i++)
			{
				if (this[i].ContentDisposition == ContentDisposition.None)
				{
					return true;
				}
			}
			return false;
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Adds an attachment to the collection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public void Add (MailAttachment attachment)
		{
			InnerList.Add(attachment);
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Adds a file to the collection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public void Add (string fileName)
		{
			InnerList.Add(new MailAttachment(fileName));
		}
		
		public void CopyTo (MailAttachment[] array, int index)
		{
			InnerList.CopyTo(array, index);
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Removes an attachment from the collection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public void Remove (int index)
		{
			InnerList.Remove(index);
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets an attachment.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailAttachment this[int index]
		{
			get
			{
				return ((MailAttachment) InnerList[index]);
			}
			set
			{
				InnerList[index] = value;
			}
		}
		
		public void Clear ()
		{
			InnerList.Clear();
		}
		
	}
	public enum ContentDisposition
	{
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Prevents the Content-Disposition header to be added to the message.
		/// </summary>
		/// -----------------------------------------------------------------------------
		None,
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// The file will be shown as an attachment.
		/// </summary>
		/// -----------------------------------------------------------------------------
		Attachment,
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// The file will be shown inside the message.
		/// </summary>
		/// -----------------------------------------------------------------------------
		Inline
	}
	
	public enum MailFormat
	{
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Message body is plain text format.
		/// </summary>
		/// -----------------------------------------------------------------------------
		Plaintext,
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Message body is HTML.
		/// </summary>
		/// -----------------------------------------------------------------------------
		Html,
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Message format is specified by the Content-Type header.
		/// </summary>
		/// -----------------------------------------------------------------------------
		Other
	}
	
	public enum MailPriority
	{
		Normal,
		Low,
		High
	}
	
	public enum MailSendMode
	{
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// The mail is sent directly to its domain mail exchanger server.
		/// </summary>
		/// -----------------------------------------------------------------------------
		ToMailExchanger,
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// The mail is sent to a smtp relay server.
		/// </summary>
		/// -----------------------------------------------------------------------------
		ToSmtpRelay
	}
	
	[Serializable()]public sealed class MailException : System.Exception
	{
		
		private int _errorCode;
		
		public MailException(string message) : base(message.Substring(4)) 
		{
			
			
			_errorCode = int.Parse(message.Substring(0, 3), CultureInfo.InvariantCulture);
			
		}
		
		public MailException(string message, Exception innerException) : base(message.Substring(4), innerException) 
		{
			
			
			_errorCode = int.Parse(message.Substring(0, 3), CultureInfo.InvariantCulture);
			
		}
		
		public MailException() 
		{
		}
		
		private MailException(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
			
			
			_errorCode = info.GetInt32("ErrorCode");
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets the error code returned by the smtp server
		/// </summary>
		/// -----------------------------------------------------------------------------
		public int ErrorCode
		{
			get
			{
				return _errorCode;
			}
		}
		
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]public override void GetObjectData (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			
			base.GetObjectData(info, context);
			
			info.AddValue("ErrorCode", _errorCode);
			
		}
		
	}
	/// ----------------------------------------------------------------------------------------
	/// <summary>
	/// Represents a mail exchanger server
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	public struct MailExchanger:IComparable
	{
		
		private string _hostName;
		private int _preference;
		
		internal MailExchanger(string hostName, int preference) 
		{
			_hostName = hostName;
			_preference = preference;
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets the server name.
		/// </summary>
		/// <history>
		/// 	[Eduardo Morcillo]	06/05/2005	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public string HostName
		{
			get
			{
				return _hostName;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets the server preference.
		/// </summary>
		/// <history>
		/// 	[Eduardo Morcillo]	06/05/2005	Created
		/// </history>
		/// -----------------------------------------------------------------------------
		public int Preference
		{
			get
			{
				return _preference;
			}
		}
		
		public int CompareTo(MailExchanger obj)
		{
			
			int hostCompare;
			
			hostCompare = _hostName.CompareTo(obj._hostName);
			
			if (hostCompare == 0)
			{
				return _preference.CompareTo(obj._preference);
			}
			else
			{
				return hostCompare;
			}
			
		}
		
		public int CompareTo(object obj)
		{
			
			if (obj is MailExchanger)
			{
				return CompareTo((MailExchanger) obj);
			}
			
			throw (new ArgumentException(GetString("MX_InvalidCompareToArgument")));
			
		}

		private static string GetString(string name)
		{
			
			System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources", System.Reflection.Assembly.GetCallingAssembly());
			
			return manager.GetString(name, System.Globalization.CultureInfo.CurrentUICulture);
			
		}
		
		public override string ToString()
		{
			
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", _hostName, _preference);
			
		}
		
		/// <summary>
		/// Compara el objeto actual con otro
		/// </summary>
		public bool Equals(MailExchanger obj)
		{
			return this.CompareTo(obj) == 0;
		}
		
		/// <summary>
		/// Compara el objeto actual con otro
		/// </summary>
		public override bool Equals(object obj)
		{
			
			if (obj is MailExchanger)
			{
				return Equals((MailExchanger) obj);
			}
			
			return false;
			
		}
		
		/// <summary>
		/// Obtiene el codigo hash para el objeto actual
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _hostName.GetHashCode() ^ _preference.GetHashCode();
		}
	}
	/// <summary>
	/// Representa una colección de objetos MailHeader
	/// </summary>
	public sealed class MailHeaderCollection : NameValueCollection
	{
		
		private const string HEADER_CONTENT_TRANSFER_ENCODING = "Content-Transfer-Encoding";
		private const string HEADER_CONTENT_TYPE = "Content-Type";
		private const string HEADER_DATE = "Date";
		private const string HEADER_MIME_VERSION = "Mime-Version";
		private const string HEADER_SUBJECT = "Subject";
		private const string HEADER_X_MSMAIL_PRIORITY = "X-MSMail-Priority";
		
		/// <summary>
		/// Constructor de la clase MailHeaderCollection
		/// </summary>
		public MailHeaderCollection() 
		{
			
			Add(HEADER_CONTENT_TYPE, "text/plain; charset=utf8");
			Add(HEADER_MIME_VERSION, "1.0");
			Add(HEADER_CONTENT_TRANSFER_ENCODING, "8bit");
			Add(HEADER_X_MSMAIL_PRIORITY, "Normal");
			this.@DateTime = DateTime.Now;
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the Content-Transfer-Encoding header.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string ContentTransferEncoding
		{
			get
			{
				return this.Get(HEADER_CONTENT_TRANSFER_ENCODING);
			}
			set
			{
				this.Set(HEADER_CONTENT_TRANSFER_ENCODING, value);
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the Content-Type header.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string ContentType
		{
			get
			{
				return this.Get(HEADER_CONTENT_TYPE);
			}
			set
			{
				this.Set(HEADER_CONTENT_TYPE, value);
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the Date header.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public DateTime @DateTime
		{
			get
			{
				return DateTime.Parse(this.Get(HEADER_DATE), CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AllowWhiteSpaces).ToLocalTime();
			}
			set
			{
				this.Set(HEADER_DATE, value.ToUniversalTime().ToString("dd MMM yyyy hh:mm:ss\' +0000\'", CultureInfo.InvariantCulture));
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the Mime-Version header.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string MimeVersion
		{
			get
			{
				return this.Get(HEADER_MIME_VERSION);
			}
			set
			{
				this.Set(HEADER_MIME_VERSION, value);
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the Priority headers.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailPriority Priority
		{
			get
			{
				return ((MailPriority)(@Enum.Parse(typeof(MailPriority), this.Get(HEADER_X_MSMAIL_PRIORITY))));
			}
			set
			{
				this.Set(HEADER_X_MSMAIL_PRIORITY, DBNull.Value.ToString());
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the Subject header.
		/// </summary>
		/// -----------------------------------------------------------------------------
		internal string Subject
		{
			get
			{
				return this.Get(HEADER_SUBJECT);
			}
			set
			{
				this.Set(HEADER_SUBJECT, value);
			}
		}
		
		/// <summary>
		/// Copia el contenido de la coleccion a otro array
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo (string[] array, int index)
		{
			((ICollection) this).CopyTo(array, index);
		}
		
	}
	
	/// <summary>
	/// Representa un correo electrónico con soporte de html y archivos adjuntos.
	/// </summary>
	public sealed class MailMessage
	{
		
		private MailAttachmentCollection _attachments;
		private MailHeaderCollection _headers;
		private string _message;
		private MailRecipientCollection _to;
		private MailRecipientCollection _cc;
		private MailRecipientCollection _bcc;
		private MailRecipient _sender;
		private MailFormat _bodyFormat;

		/// <summary>
		/// Contructor de la clase MailMessage
		/// </summary>
		public MailMessage() 
		{
			_attachments = new MailAttachmentCollection();
			_headers = new MailHeaderCollection();
			_to = new MailRecipientCollection();
			_cc = new MailRecipientCollection();
			_bcc = new MailRecipientCollection();
			
		}
		/// <summary>
		/// Contructor de la clase MailMessage
		/// </summary>
		public MailMessage(MailRecipient sender, MailRecipient[] recipients, string subject, string body, string[] fileAttachments) : this() 
		{
			_attachments = new MailAttachmentCollection();
			_headers = new MailHeaderCollection();
			_to = new MailRecipientCollection();
			_cc = new MailRecipientCollection();
			_bcc = new MailRecipientCollection();
			
			
			
			_sender = sender;
			_to.AddRange(recipients);
			this.Headers.Subject = subject;
			_message = body;
			
			if (fileAttachments != null)
			{
				for (int i = 0; i <= fileAttachments.Length - 1; i++)
				{
					_attachments.Add(fileAttachments[i]);
				}
			}
			
		}
		
		/// <summary>
		/// Contructor de la clase MailMessage
		/// </summary>
		public MailMessage(MailRecipient sender, MailRecipient recipient, string subject, string body) : this(sender, new MailRecipient[] {recipient}, subject, body, null) 
		{
			_attachments = new MailAttachmentCollection();
			_headers = new MailHeaderCollection();
			_to = new MailRecipientCollection();
			_cc = new MailRecipientCollection();
			_bcc = new MailRecipientCollection();
			
			
			
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Creates the email body.
		/// </summary>
		/// -----------------------------------------------------------------------------
		internal System.Text.StringBuilder GetMessageBody(bool toSend)
		{
			
			System.Text.StringBuilder msg = new System.Text.StringBuilder();
			bool hasAttachments = _attachments.Count > 0;
			string messageContentType=null;
			string boundaryName = "NextPart_" + _sender.MailAddress.GetHashCode().ToString("X", CultureInfo.InvariantCulture);
			
			try
			{
				
				// Add some headers to the collection
				_headers["From"] = _sender.ToString();
				if (_to.Count > 0)
				{
					_headers["To"] = _to.ToString();
				}
				if (_cc.Count > 0)
				{
					_headers["Cc"] = _cc.ToString();
				}
				_headers["X-Mailer"] = System.Reflection.Assembly.GetExecutingAssembly().FullName;
				
				
				// The the mail has attachments change
				// the content type to multipart/mixed
				if (hasAttachments)
				{
					
					// Get the message content type
					switch (_bodyFormat)
					{
						case MailFormat.Plaintext:
							
							messageContentType = "text/plain; charset=utf-8";
							break;
						case MailFormat.Html:
							
							messageContentType = "text/html; charset=utf-8";
							break;
						case MailFormat.Other:
							
							messageContentType = _headers.ContentType;
							break;
					}
					
					if (_attachments.IsRelated())
					{
						_headers.ContentType = "multipart/related; boundary=\"" + boundaryName + "\"";
					}
					else
					{
						_headers.ContentType = "multipart/mixed; boundary=\"" + boundaryName + "\"";
					}
					
				}
				else
				{
					
					switch (_bodyFormat)
					{
						case MailFormat.Plaintext:
							
							_headers.ContentType = "text/plain; charset=utf-8";
							break;
						case MailFormat.Html:
							
							_headers.ContentType = "text/html; charset=utf-8";
							break;
					}
					
				}
				
				// Write the mail headers
				for (int i = 0; i <= _headers.Count - 1; i++)
				{
					msg.AppendFormat("{0}: {1}{2}", _headers.Keys[i], _headers[i], Environment.NewLine);
				}
				msg.Append(Environment.NewLine);
				
				// If the mail has attachments add
				// the first boundaryName for the message
				if (hasAttachments)
				{
					
					// Write the message preamble
					msg.AppendFormat("This is a multi-part message in MIME format.{0}", Environment.NewLine);
					msg.Append(Environment.NewLine);
					
					// Start the message boundaryName
					msg.AppendFormat("--{0}{1}", boundaryName, Environment.NewLine);
					
					// Write the message content type
					msg.AppendFormat("Content-Type: {0}{1}", messageContentType, Environment.NewLine);
					msg.Append(Environment.NewLine);
					
				}
				
				// Write the message body
				if (_message != null)
				{
					msg.Append(_message.Replace(Environment.NewLine + "." + Environment.NewLine, Environment.NewLine + ". " + Environment.NewLine));
					msg.Append(Environment.NewLine);
				}
				
				// Add the attachments
				if (hasAttachments)
				{
					
					for (int i = 0; i <= _attachments.Count - 1; i++)
					{
						
						byte[] data;
						System.Text.StringBuilder attachment=null;
						
						try
						{
							
							// Get the attachment data
							data = _attachments[i].GetData();
							
							attachment = new Text.StringBuilder();
							
							// Start the attachment boundaryName
							attachment.Append(Environment.NewLine);
							attachment.AppendFormat("--{0}{1}", boundaryName, Environment.NewLine);
							
							
							// Add the attachment headers
							
							// Content-Transfer-Encoding
							attachment.AppendFormat("Content-Transfer-Encoding: base64{0}", Environment.NewLine);
							
							// Content-Disposition
							if (_attachments[i].ContentDisposition != ContentDisposition.None)
							{
								attachment.AppendFormat("Content-Disposition: {0}; filename=\"{1}\"{2}", _attachments[i].ContentDisposition.ToString().ToLower(CultureInfo.InvariantCulture), _attachments[i].FileName, Environment.NewLine);
							}
							
							// Content-Type
							if (_attachments[i].ContentType != null&& _attachments[i].ContentType.Length > 0)
							{
								attachment.AppendFormat("Content-Type: {0}; name=\"{1}\"{2}", _attachments[i].ContentType, _attachments[i].FileName, Environment.NewLine);
							}
							
							// Content-Id
							if (_attachments[i].ContentId != null)
							{
								attachment.AppendFormat("Content-Id: <{0}>{1}", _attachments[i].ContentId, Environment.NewLine);
							}
							
							attachment.Append(Environment.NewLine);
							
							
							// Write the attachment in Base64
							attachment.Append(Convert.ToBase64String(data));
							attachment.Append(Environment.NewLine);
							
							
						}
						catch (Exception ex)
						{
							
							// Write an "error attachment"
							attachment = new Text.StringBuilder();
							
							// Start the attachment boundary
							attachment.Append(Environment.NewLine);
							attachment.AppendFormat("--{0}{1}", boundaryName, Environment.NewLine);
							
							// Add the attachment headers
							attachment.AppendFormat("Content-Type: text/plain{0}", Environment.NewLine);
							attachment.AppendFormat("Content-Disposition: inline; filename=\"error.txt\"{0}", Environment.NewLine);
							attachment.Append(Environment.NewLine);
							
							// Write the attachment text
							attachment.Append("There was an error adding the attachment: ");
							attachment.Append(ex.Message);
							attachment.Append(Environment.NewLine);
							
						}
						finally
						{
							
							// Append the attachment to the message
							msg.Append(attachment.ToString());
							attachment = null;
							
						}
						
					}
					
					// Add the closing boundaryName
					msg.Append(Environment.NewLine);
					msg.AppendFormat("--{0}--{1}", boundaryName, Environment.NewLine);
					
				}
				
				if (toSend)
				{
					msg.Append(".");
				}
				
				return msg;
				
			}
			finally
			{
				
				if (messageContentType != null)
				{
					_headers["Content-Type"] = messageContentType;
				}
				_headers.Remove("From");
				_headers.Remove("To");
				
			}
			
		}
		
		/// <summary>
		/// Obtiene la representación en cadena de la instancia actual
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return GetMessageBody(false).ToString();
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the attachment collection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailAttachmentCollection Attachments
		{
			get
			{
				return _attachments;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the header collection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailHeaderCollection Headers
		{
			get
			{
				return _headers;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the message body.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string Body
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the recipient collection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailRecipientCollection @To
		{
			get
			{
				return _to;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Retursn the CC recipient collection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailRecipientCollection CC
		{
			get
			{
				return _cc;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Returns the BCC recipient collection.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailRecipientCollection Bcc
		{
			get
			{
				return _bcc;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the sender recipient.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailRecipient Sender
		{
			get
			{
				return _sender;
			}
			set
			{
				_sender = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the mail subject.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string Subject
		{
			get
			{
				return _headers.Subject;
			}
			set
			{
				_headers.Subject = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the message format.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailFormat BodyFormat
		{
			get
			{
				return _bodyFormat;
			}
			set
			{
				_bodyFormat = value;
			}
		}
		
	}
	
	/// <summary>
	/// Representa una casilla de correo electrónico.
	/// </summary>
	public sealed class MailRecipient
	{
		
		private string _displayName;
		private string _email;
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance with a display name and mail address.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public MailRecipient(string displayName, string mail) 
		{
			
			_displayName = displayName;
			this.MailAddress = mail;
			
		}
		
		/// <summary>
		/// Obtiene la representación en cadena de la instancia actual
		/// </summary>
		public override string ToString()
		{
			return "\"" + _displayName + "\" <" + _email + ">";
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string DisplayName
		{
			get
			{
				return _displayName;
			}
			set
			{
				_displayName = value;
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the mail address.
		/// </summary>
		/// -----------------------------------------------------------------------------
		public string MailAddress
		{
			get
			{
				return _email;
			}
			set
			{
				
				if (! SmtpMail.CheckMailFormat(value))
				{
					throw (new FormatException(GetString("MailFormatException")));
				}
				
				_email = value;
				
			}
		}

		private static string GetString(string name)
		{
			
			System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources", System.Reflection.Assembly.GetCallingAssembly());
			
			return manager.GetString(name, System.Globalization.CultureInfo.CurrentUICulture);
			
		}
		
	}
	
	/// <summary>
	/// Representa una colección de objetos MailRecipient
	/// </summary>
	public sealed class MailRecipientCollection : ReadOnlyCollectionBase
	{
		/// <summary>
		/// Obtiene la representación en cadena de la instancia actual de MailRecipientCollection
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			
			System.Text.StringBuilder recps = new System.Text.StringBuilder();
			
			for (int i = 0; i <= Count - 1; i++)
			{
				recps.Append(this[i].ToString());
				if (i < Count - 1)
				{
					recps.Append(", ");
				}
			}
			
			return recps.ToString();
			
		}
		
		/// <summary>
		/// Añade un elemento MailRecipient a la coleccion
		/// </summary>
		/// <param name="recipient"></param>
		public void Add (MailRecipient recipient)
		{
			InnerList.Add(recipient);
		}
		
		/// <summary>
		/// Añade un elemento mail a la coleccion
		/// </summary>
		/// <param name="mail"></param>
		public void Add (string mail)
		{
			InnerList.Add(new MailRecipient(mail, mail));
		}
		
		/// <summary>
		/// Añade varios elementos MailRecipient a la coleccion
		/// </summary>
		/// <param name="recipients"></param>
		internal void Add (MailRecipientCollection recipients)
		{
			this.InnerList.AddRange(recipients);
		}
		
		/// <summary>
		/// Añade varios elementos MailRecipient a la coleccion
		/// </summary>
		/// <param name="recipient"></param>
		public void AddRange (MailRecipient[] recipient)
		{
			InnerList.AddRange(recipient);
		}
		
		/// <summary>
		/// Copia la colección a otro array
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo (MailRecipient[] array, int index)
		{
			InnerList.CopyTo(array, index);
		}
		
		/// <summary>
		/// Elimina el objeto de la coleccion situado en el indice especificado
		/// </summary>
		/// <param name="index"></param>
		public void Remove (int index)
		{
			InnerList.Remove(index);
		}
		
		/// <summary>
		/// Obtiene o establece el elemento MailRecipient del indice especificado
		/// </summary>
		public MailRecipient this[int index]
		{
			get
			{
				return ((MailRecipient) InnerList[index]);
			}
			set
			{
				InnerList[index] = value;
			}
		}
		
		/// <summary>
		/// Limpia y vacía la coleccion
		/// </summary>
		public void Clear ()
		{
			this.InnerList.Clear();
		}
		
	}
	
	
}