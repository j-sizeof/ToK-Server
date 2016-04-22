using System;
using System.Runtime.InteropServices;
using ToK.Common;
using ToK.DataServer;
using ToK.GameState;

namespace ToK.HandlePlayerRequest
{
    /// <summary>
    /// Packet sent by game client requesting login.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = ProjectBasics.DEFAULT_PACK)]
    public struct MAccountLoginPacket : IGamePacket
    {
        public const ushort Opcode = 0x20D;

        public MPacketHeader Header { get; set; }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MLoginInfo.MAXL_PSW)]
        public String Password;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MLoginInfo.MAXL_ACCNAME)]
        public String AccName;

        public unsafe fixed byte Zero[52];

        public int ClientVersion;

        public int DBNeedSave;

        public unsafe fixed int AdapterName[4];

        public static ERequestResult HandleRequest(CGameStateController gs, CPlayer player)
        {
            if (gs.PlayerState[player] != EPlayerState.UNITIALIZED)
                return ERequestResult.PLAYER_INCONSISTENT_STATE;

            MAccountLoginPacket packet = MyMarshal.GetStructure<MAccountLoginPacket>(player.RecvPacket);

            MAccountFile? nAccFile;
            AccountCRUD.EErrorMsg accErr = AccountCRUD.TryRead(packet.AccName, out nAccFile);

            if (accErr == AccountCRUD.EErrorMsg.NO_ERROR)
            {
                MLoginSuccessfulPacket answer =
                    PacketHelper.GetEmptyValid<MLoginSuccessfulPacket>(MLoginSuccessfulPacket.Opcode);

                MAccountFile accFile = nAccFile.Value;

                answer.AccName = accFile.Info.LoginInfo.AccName;
                answer.Cargo = accFile.Cargo;
                answer.CargoCoin = accFile.CargoCoin;

                for (int i = 0; i < GameBasics.MAXL_ACC_MOB; i++)
                {
                    unsafe
                    {
                        answer.SelChar.Coin[i] = accFile.MobCore[i].Coin;
                        answer.SelChar.Equip[i] = accFile.MobCore[i].Equip;
                        answer.SelChar.Exp[i] = accFile.MobCore[i].Exp;
                        answer.SelChar.Guild[i] = accFile.MobCore[i].Guild;
                        answer.SelChar.Name[i] = accFile.MobCore[i].Name;
                        answer.SelChar.Score[i] = accFile.MobCore[i].BaseScore;
                        answer.SelChar.SPosX[i] = accFile.MobCore[i].StellarGemPosition.X;
                        answer.SelChar.SPosY[i] = accFile.MobCore[i].StellarGemPosition.Y;
                    }
                }

                player.SendPacket(MyMarshal.GetBytes(answer));
            }
            else if (accErr == AccountCRUD.EErrorMsg.ACC_NOT_FOUND)
            {
                MTextMessagePacket answer =
                    PacketHelper.GetEmptyValid<MTextMessagePacket>(MTextMessagePacket.Opcode);

                answer.Message = "Esta conta não foi encontrada.";

                player.SendPacket(MyMarshal.GetBytes(answer));
            }
            else
            {
                return ERequestResult.UNKNOWN;
            }

            return ERequestResult.NO_ERROR;
        }
    }
}