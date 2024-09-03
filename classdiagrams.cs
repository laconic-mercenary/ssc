public class NetCommunicationsClient: IDisposable
	+ NetCommunicationsClient(NetCommunicationsClientParameters parameters)
		if parameters == null
			throw ArgumentNullException
		this.parameters = parameters
		
	+ ConnectAsync(ConnectionAttemptParameters connectionParameters)
		if (client == null)
			throw Exception("client socket is not bound")
		if connectionParameters == null
			throw ArgumentNullException
		ReliableSocket client = null
		lock {
			client = new
			client.OnConnectionSuccess subscribe
			client.OnAsyncSocketError subscribe
			connectorQueue.Add(client)
		}
		client.ConnectAsync()
		
	- SendCredentials(INetCredentials clientCredentials)
	- SendProfile(ConnectedClientInformation localInformation)
	+ Dispose()
	
	+ event OnDisconnect(object sender, IPEndPoint server)	
	// fires when we conencted or failed to connect to a server	
	+ event OnConnectionResult(object sender, ConnectionResultEventArgs e)	
	// fired when we know for sure that we are connected to a server, on receipt of an ACK
	+ event OnNetConnectionEstablished(object sender, NetConnectionEstablishedEventArgs e)	
	+ event OnCredentialsSent(object sender, CredentialsSentEventArgs e)
	+ event OnProfileSent(object sender, ProfileSentEventArgs e)
	
	- HandleSendClientData()
		queue user work item = SendClientData()

	- SendClientData()
		// use try catches, fire the OnConnectionResult event if a failure occurs
		// and free up the socket
		- SendCredentials(clientCredentials)
		- fire OnCredentialsSent
		- SendProfile(localProfile)
		- fire OnProfileSent
		- fire OnNetConnectionEsablished

	- client_OnConnectionSuccess(SocketConnectionEventArgs e)
		fire OnConnectionResult(succeeded)
		HandleSendClientData()
		
	- client_OnAsyncSocketError(Exception ex, ReliableSocket sock)
		FreeSocket(sock)
		fire OnConnectionResult(failed)
		
	- BindSocket(ReliableSocket socket)
		
	- FreeSocket(ReliableSocket socket)
		lock {
		if socket != null
			socket.Close()
			unsubscribe all events
			connectorQueue.Remove(socket)
			socket = null
		}
	
	- List<ReliableSocket> connectorQueue
	- NetCommunicationsClientParameters parameters

sealed public class ConnectedClient:
	+ ConnectedClient(ConnectedClientInformation info, SocketProfile profile)
	+ SendImageAsync(Bitmap image) : void
	+ SendAudioAsync(byte[] audioRawData) : void
	+ Disconnect() : void
	// will fire when there is an async error, or a local disconnect occurs
	+ event OnDisconnect(object sender, ConnectedClient disconnector)
	+ event OnImageConnectionSuccess(object senderObject, ConnectedClient sender)
	+ event OnAudioConnectionSuccess(object senderObject, ConnectedClient sender)
	+ event OnImageRx(object sender, Bitmap image)
	+ event OnAudioRx(object sender, byte[] data)
	+ event OnImagePreRx(object sender, ref byte[] rawImageData)
	+ event OnAudioPreRx(object sender, ref byte[] rawAudioData)
	+ IPEndPoint EndPointInformation {get;}
	- DisconnectLocally() : void
	- BindImageClient() : void
	- BindImageServer() : void
	- FreeImageClient() : void
	- FreeImageServer() : void
	- BindAudioClient() : void
	- BindAudioServer() : void
	- FreeAudioClient() : void
	- FreeAudioServer() : void
	- ConnectedClientInformation clientInformation
	- SocketProfile profile
	- IPEndPoint endPointInformation
	
NetCommunicationServer : IDisposable
	+ NetCommunicationsServer(NetCommunicationServerParameters parameters)
	+ event OnConnectedClientReady(object sender, ConnectedClientReadyEventArgs e)
	+ event OnConnectionReceived(object sender, IPEndPoint connector)
	+ event OnConnectionRejected(object sender, ConnectionRejectedEventArgs e)
	+ IsAccepting {get;set;} : bool
	+ UseAuthentication {get;set;} : bool
	+ MaximumConnectedClients {get;set;} : int
	+ ConnectionTimeOutInterval {get;set;} : int
	+ TimeOutAttempts {get;set;} : int
	+ Dispose()
	- HandleConnectionRequest(IPEndPoint conenctor)
	- List<ConnectedClient> connectedClients
	- List<ConnectedClient> approvedClients

public class NetCommunicationsServer: IDisposable
	+ NetCommunicationsServer(NetBindingInformation parameters)
	// Server
	+ readonly List<ConnectedClient> ConnectedClients {get;}
	// occurs when a client is fully ready to communicate (and is authenticated)
	+ event OnConnectedClientReady(object sender, ConnectedClientReadyEventArgs e)
	// received when any plain old connection is received
	+ event OnConnectionReceived(object sender, IPEndPoint connector)
	// fires when a client is rejected for authentication or whatever reasons (like timeout)
	+ event OnConnectionRejected(object sender, ConnectionRejectedEventArgs e)
	+ IsAccepting {get;set;} : bool
	+ UseAuthentication {get;set;} : bool 
	+ MaximumConnectedClients {get;set;} : int
	+ ConnectionTimeOutInterval {get;set;} : int
	+ Dispose()
	// uses a while loop to process client information, 
	// 1) gets the ip endpoint information
	// 2) waits until authentication information arrives
	// 3) if authenticated, waits for endpoint socket information to arrive
	// 4) if all goes well, fires the OnConnectedClientReady event
	// try using Thread.Sleep() calls, and a counter for time outs
	- HandleNewConnection(IPEndPoint connector)
		- while (true)
			- if (INetCredentials != null)
				- if (is authenticated)
					- authenticated = true
			- if (UserInformation != null)
				- haveInfo = true	
			- if (haveInfo && authenticated)
				- fire OnConnectedClient ready
				- break
			- if (timeoutCounter >= MaxTimeOuts)
				- fire OnConnectionRejected event
			- else
				- ++timeoutCounter
	// this will subscribe to the necessary events and will hold a list of currently connected clients
	- RegisterNewClient(ConnectedClientInformation newClientInformation)
	- SendAckAsync(IPEndPoint connector)
	- Bind()
	- Free()
	- List<ConnectedClient> registeredClients
	- NetBindingInformation serverData
	- ReliableSocket server
	- ReliableSocket ackSocket

sealed public class ConnectedClientInformation:
	+ IPEndPoint RemoteEndPoint {get;set;}
	+ string UserName {get;set;}
	+ int ImageRx
	+ int AudioRx
	+ int ImageTx
	+ int AudioTx
	+ int UserMessageRx
	+ int UserMessageTx
	+ int ConnectionRx
	+ byte[] ToData()
	+ static bool TryParse(byte[] data, out ConnectedClientInformation information)
	
sealed public class SocketProfile : IDisposable
	+ SocketProfile(ConnectedClientInformation bindingInformation)
	+ ClientData {get;} : ConnectedClientInformation
	+ TcpImageSender ImageTxSocket {get;}	
	+ TcpImageReceiver ImageRxSocket {get;}
	+ ReliableSocket AudioRxSocket {get;}
	+ ReliableSocket AudioTxSocket {get;}
	+ ReliableSocket MessageRxSocket {get;}
	+ ReliableSocket MessageTxSocket {get;}
	+ ReliableSocket ConnectionRxSocket {get;}
	+ Dispose()

public class ConnectedClientReadyEventArgs:
	+ ConnectedClientInformation clientInformation {get;set;}
	+ ConnectedClient client {get;set;}

public interface INetAuthenticator:
	+ IAuthenticationResult Authenticate(IPEndPoint endPointData, INetCredentials parameters)

public interface IAuthenticationResult
	+ bool IsAuthenticated {get;}

public class INetCredentials:
	+ bool IsEqual(INetCredentials comparison)
	+ static operator ==(INetCredentials params1, INetCredentials params2)
	+ static operator !=(INetCredentials params1, INetCredentials params2)
	+ byte[] ToData()
	+ static bool TryParse(byte[] data, out INetCredentials credentials)