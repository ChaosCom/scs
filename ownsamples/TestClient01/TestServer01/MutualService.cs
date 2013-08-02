using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hik.Communication.ScsServices.Service;
using CommonLib;
using System.Security.Cryptography;

namespace TestServer01
{
    class MutualService : ScsService, ICommonMutualService
    {

        public void Login()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void Register()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostedSession> DiscoverHostedSessions()
        {
            return announcedSessions.Values;
        }

        public void JoinHostedSession()
        {
            throw new NotImplementedException();
        }

        public void LeaveHostedSession()
        {
            throw new NotImplementedException();
        }


        //List<HostedSessions> announcedSessions = new List<HostedSessions>();
        Dictionary<string, HostedSession> announcedSessions = new Dictionary<string, HostedSession>();

        public string AnnounceService(string name)
        {
            string token = HelperFunctions.GenerateToken();
            announcedSessions.Add(
                token,
                new HostedSession { Name = name }
            );


            return token;
        }

        public int DeannounceService(string hostingToken)
        {
            if (announcedSessions.ContainsKey(hostingToken))
            {
                announcedSessions.Remove(hostingToken);
                return 0;
            }

            return -1;
        }
    }
}
