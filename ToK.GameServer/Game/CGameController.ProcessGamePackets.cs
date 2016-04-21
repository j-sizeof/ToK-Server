using ToK.Common;
using ToK.Common.Game;
using ToK.Common.Network;
using ToK.Common.Network.PacketStructures;
using ToK.Common.Persistency;
using ToK.DataServer;

namespace ToK.GameServer.Game
{
    /// <summary>
    /// This class implements all the game-related actions.
    /// </summary>
    public partial class CGameController
    {
        private unsafe EPacketError ProcessAccountLogin(CPlayer player)
        {
            if (PlayerState[player.Index] != EPlayerState.UNITIALIZED)
                return EPacketError.PLAYER_INCONSISTENT_STATE;

            MAccountLoginPacket packet = MyMarshal.GetStructure<MAccountLoginPacket>(player.RecvPacket.RawBuffer);

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
                    answer.SelChar.Coin[i] = accFile.MobCore[i].Coin;
                    answer.SelChar.Equip[i] = accFile.MobCore[i].Equip;
                    answer.SelChar.Exp[i] = accFile.MobCore[i].Exp;
                    answer.SelChar.Guild[i] = accFile.MobCore[i].Guild;
                    answer.SelChar.Name[i] = accFile.MobCore[i].Name;
                    answer.SelChar.Score[i] = accFile.MobCore[i].BaseScore;
                    answer.SelChar.SPosX[i] = accFile.MobCore[i].StellarGemPosition.X;
                    answer.SelChar.SPosY[i] = accFile.MobCore[i].StellarGemPosition.Y;
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
                return EPacketError.UNKNOWN;
            }

            return EPacketError.NO_ERROR;
        }
    }
}