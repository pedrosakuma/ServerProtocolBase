using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Connection
{
    public enum State
    {
        Connecting,
        Connected,
        Disconnected
    }
    public State state = State.Disconnected;
    public static int dataBufferSize = 4096;
    public int myId = 0;
    public TcpClient socket;
    private NetworkStream stream;
    private byte[] receiveBuffer;
    public delegate void ConnectionCallback(string message);
    public event ConnectionCallback OnConnectionDropped;
    public event ConnectionCallback OnConnectionStateChanged;
    public State GetState() => state;

    public void Connect(string ip, int port)
    {
        try
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };
            receiveBuffer = new byte[dataBufferSize];
            state = State.Connecting;
            this.OnConnectionStateChanged("Connecting...");
            socket.BeginConnect(ip, port, ConnectCallback, socket);
        }
        catch (Exception _ex)
        {
            this.OnConnectionDropped(_ex.Message);
        }
    }

    private void ConnectCallback(IAsyncResult _result)
    {
        try
        {
            socket.EndConnect(_result);

            if (!socket.Connected)
            {
                Disconnect("Unable to connect to server."); // Desconectar se a conexão não for bem-sucedida
                return;
            }

            stream = socket.GetStream();
            state = State.Connected; // Atualizar o estado para Connected aqui
            this.OnConnectionStateChanged("Connected");
            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }
        catch (Exception _ex)
        {
            Disconnect(_ex.Message);
        }
    }

    private void ReceiveCallback(IAsyncResult _result)
    {
        try
        {
            int _byteLength = stream.EndRead(_result);
            if (_byteLength <= 0)
            {
                return;
            }

            byte[] _data = new byte[_byteLength];
            Array.Copy(receiveBuffer, _data, _byteLength);

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }
        catch (Exception _ex)
        {
            Disconnect(_ex.Message);
        };
    }
    public void Disconnect(string message)
    {
        state = State.Disconnected;
        if (socket != null)
        {
            socket.Close();
            socket = null;
        }
        if (this.OnConnectionDropped != null)
        {
            this.OnConnectionDropped(message);
        }
    }
}
