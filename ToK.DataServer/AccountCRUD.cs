using System;
using System.IO;
using System.Threading.Tasks;
using ToK.Common;
using ToK.Common.Persistency;

using static ToK.Common.Persistency.PersistencyBasics;

namespace ToK.DataServer
{
    public static class AccountCRUD
    {
        public enum EErrorMsg
        {
            NO_ERROR,
            /// <summary>
            /// The requested account does not exist in the database server.
            /// </summary>
            ACC_NOT_FOUND,
            /// <summary>
            /// Some unknown error occour when processing the account.
            /// The caller must treat this error as a CRITICAL error, close all the interactions with the requesting player
            /// and save/log the error.
            /// </summary>
            UNKNOWN,
        }

        public static EErrorMsg TryRead(String accName, out MAccountFile? accFile)
        {
            EErrorMsg err = EErrorMsg.NO_ERROR;
            accFile = null;
            
            try
            {
                byte[] rawAcc = File.ReadAllBytes(String.Format("{0}/{1}/{2}.bin",
                    DB_ROOT_PATH, accName.Substring(0, 1).ToUpper(), accName.ToUpper()));

                accFile = MyMarshal.GetStructure<MAccountFile>(rawAcc);
            }
            catch(FileNotFoundException)
            {
                err = EErrorMsg.ACC_NOT_FOUND;
            }
            catch(DirectoryNotFoundException)
            {
                err = EErrorMsg.ACC_NOT_FOUND;
            }
            catch(Exception)
            {
                err = EErrorMsg.UNKNOWN;
            }

            return err;
        }
    }
}