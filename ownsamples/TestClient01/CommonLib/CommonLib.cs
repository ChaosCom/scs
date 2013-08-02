using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hik.Communication.ScsServices.Service;
using System.Net;
using System.Security.Cryptography;
using System.Diagnostics;

namespace CommonLib
{

    [ScsService(Version="1.0.0")]
    public interface ICommonMutualService
    {

        void Login();
        void Logout();
        void Register();


        /// <summary>
        /// Contacts the lobby server and retrieves a list of currently available hosted sessions.
        /// </summary>
        IEnumerable<HostedSession> DiscoverHostedSessions();

        /// <summary>
        /// Allows a client to join a discovered session.
        /// </summary>
        void JoinHostedSession();

        /// <summary>
        /// Allows a client to leave a sessions it is currently connected to.
        /// </summary>
        void LeaveHostedSession();

        /// <summary>
        /// Allows a server to announce its services with the lobby server. Returns a hostingToken that is required when de-announcing.
        /// </summary>
        /// <returns></returns>
        string AnnounceService(string name);

        /// <summary>
        /// Allows a server to de-announce its service on the lobby server. Returns an resultCode specifying whether the operation was successful.
        /// </summary>
        /// <param name="hostingToken"></param>
        int DeannounceService(string hostingToken);
    }

    [Serializable]
    public class HostedSession
    {
        public string Name { get; set; }
        public int NumParticipants { get; set; }
        public IPEndPoint Ip { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Name, Ip);
        }
    }


    public static class HelperFunctions
    {
        public static string GenerateToken()
        {
            MD5 md5 = MD5.Create();
            DateTime input = DateTime.Now;
            byte[] inputBytes = BitConverter.GetBytes(input.ToBinary());
            byte[] tokenArray = md5.ComputeHash(inputBytes);
            //return Encoding.ASCII.GetString(tokenArray);
            //return ByteToHexBitFiddle(tokenArray);
            Debug.WriteLine(ByteToHexBitFiddle(tokenArray));

            return BitConverter.ToString(tokenArray);
            
        }

        public static string ByteToHexBitFiddle(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            int b;
            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i] >> 4;
                c[i * 2] = (char) (55 + b + (((b-10)>>31)&-7));
                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char) (55 + b + (((b-10)>>31)&-7));
            }
            return new string(c);
        }
    }

    public static class Consts
    {
        public const int DefaultPort = 10048;


    }

}
