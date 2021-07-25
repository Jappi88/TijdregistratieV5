namespace Rpm.Connection
{
    public delegate void ClientHandler(object sender, ClientInfo client);

    public delegate void RequestMessageHandler(object sender, RequestMessage message);

    public delegate void RespondMessageHandler(object sender, RespondMessage message);
}