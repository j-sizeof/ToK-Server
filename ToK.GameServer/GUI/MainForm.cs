using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToK.Common;
using ToK.Common.Game;
using ToK.Common.Game.MarshalableTypes;
using ToK.Common.Network;
using ToK.Common.Network.PacketStructures;
using ToK.Common.Persistency;
using ToK.GameServer.Game;
using ToK.GameServer.Utility;

namespace ToK.GameServer.GUI
{
    public partial class MainForm : Form
    {
        private CGameController gameController;

        public MainForm()
        {
            InitializeComponent();

            gameController = new CGameController();

            CLog.Initialize(rtbMainLog);

            StartServer_Channel1();
        }

        /// <summary>
        /// Process the newly connected client.
        /// </summary>
        /// <param name="client">The newly connected client.</param>
        private async void ProcessClient_Channel1(TcpClient client)
        {
            using (client)
            using (NetworkStream stream = client.GetStream())
            {
                CPlayer player = new CPlayer(stream);
                CRecvPacket packet = player.RecvPacket;

                try
                {
                    // Iterate processing incoming player packets until he disconnects.
                    while (gameController.GetPlayerState(player) != EPlayerState.CLOSED)
                    {
                        int readCount = await stream.ReadAsync(packet.Buffer, 0, HNetworkBasics.MAXL_PACKET);

                        if (readCount != 4 && (readCount < 12 || readCount > HNetworkBasics.MAXL_PACKET)) // Invalid game packet.
                        {
                            gameController.DisconnectPlayer(player);
                            break;
                        }
                        else // Possible valid game packet chunk.
                        {
                            CLog.WriteLine($"New data block (len: {readCount}) received from {client.Client.RemoteEndPoint}");

                            unsafe
                            {
                                packet.Offset = 0;
                                fixed (byte* packetPtr = &packet.Buffer[packet.Offset])
                                {
                                    // Check for the init code.
                                    if (*(uint*)&packetPtr[packet.Offset] == HNetworkBasics.INIT_CODE)
                                    {
                                        packet.Offset = 4;

                                        // If a valid index can't be assigned to the player, disconnect him 
                                        if (!gameController.TryInsertPlayer(player))
                                        {
                                            player.SendPacket(BTextMessagePacket.Create("O servidor está lotado. Tente novamente mais tarde."));
                                            gameController.DisconnectPlayer(player);
                                            continue;
                                        }

                                        // If all the received chunk resumes to the INIT_CODE, read the next packet.
                                        if (readCount == 4)
                                            continue;
                                    }

                                    // Process all possible packets that were possibly sent together.
                                    while (packet.Offset < readCount && gameController.GetPlayerState(player) != EPlayerState.CLOSED)
                                    {
                                        BPacketHeader pHeader = packet.Header;

                                        // Check if the game packet size is bigger than the remaining received chunk.
                                        if (pHeader.Size > readCount - packet.Offset || pHeader.Size < 12)
                                            throw new Exception("Invalid received packet. Reading packet is bigger than the remaining chunk.");

                                        // Process the incoming packet.
                                        CGameController.EPacketError packErr = gameController.TryProcessPacket(player);
                                        
                                        // Treat the processing packet return.
                                        switch (packErr)
                                        {
                                            case CGameController.EPacketError.NO_ERROR:
                                                CLog.WriteLine(string.Format("Valid packet received (0x{0:X}/{1}) from {2}",
                                                    pHeader.Opcode, pHeader.Size, client.Client.RemoteEndPoint));
                                                break;

                                            case CGameController.EPacketError.PACKET_NOT_HANDLED:
                                                CLog.WriteLine(string.Format("An unhandled packet was received (opcode: 0x{0:X} & Size: 0x{1:X}).",
                                                                            pHeader.Opcode, pHeader.Size));

                                                byte[] rawPacket = new byte[pHeader.Size];
                                                for (int i = 0; i < rawPacket.Length; i++)
                                                    rawPacket[i] = packetPtr[i + packet.Offset];

                                                File.WriteAllBytes($@"C:\WYD2\Tales of Kersef\ToK Server\GameServer\Dumped Packets\Inner Packets\{pHeader.Opcode}.bin",
                                                    rawPacket);
                                                break;

                                            case Game.CGameController.EPacketError.CHECKSUM_FAIL:
                                                CLog.WriteLine("Received packet checksum failed.");
                                                gameController.DisconnectPlayer(player);
                                                break;

                                            default:
                                                CLog.WriteLine($"An unknown error have happened by a packet sent from {client.Client.RemoteEndPoint}.");
                                                gameController.DisconnectPlayer(player);
                                                break;
                                        }
                                        
                                        // Correct the offset to process the next packet in the received chunk.
                                        player.RecvPacket.Offset += pHeader.Size;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // TODO: log the unhandled exception.
                }
                finally
                {
                    gameController.DisconnectPlayer(player);
                    CLog.WriteLine($"The client at {client.Client.RemoteEndPoint} have disconnected.");
                }
            }
        }

        /// <summary>
        /// Start listenning the server connection.
        /// </summary>
        private async void StartServer_Channel1()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("192.168.1.69"), 8281);

            listener.Start();

            CLog.WriteLine($"The Game Server is listenning at {listener.Server.LocalEndPoint}.");

            try
            {
                while(true)
                {
                    TcpClient thisClient = await listener.AcceptTcpClientAsync();

                    CLog.WriteLine($"New client connected {thisClient.Client.RemoteEndPoint}.");

                    ProcessClient_Channel1(thisClient);
                }
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
