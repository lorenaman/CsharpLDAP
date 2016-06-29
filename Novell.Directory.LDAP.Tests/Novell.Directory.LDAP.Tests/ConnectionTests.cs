using System;
using Xunit;
using Novell.Directory.Ldap;

namespace Novell.Directory.LDAP.Tests
{
    public class ConnectionTests
    {
        [Fact]
        public void Ldap_Connection_Should_Connect()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;

            var ldap = new LdapConnection();
            ldap.Connect(ldapHost, ldapPort);

            Assert.True(ldap.Connected);
        }

        [Fact]
        public void Ldap_Connection_Should_Not_Connect()
        {
            var ldapHost = "0.0.0.0";
            var ldapPort = 389;

            var ldap = new LdapConnection();
            try
            {
                ldap.Connect(ldapHost, ldapPort);
            }
            catch (Exception){}
           
            Assert.False(ldap.Connected);
        }

        [Fact]
        public void Ldap_Connection_Should_Connect_And_Disconnect()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;

            var ldap = new LdapConnection();
            ldap.Connect(ldapHost, ldapPort);

            Assert.True(ldap.Connected);

            ldap.Disconnect();

            Assert.False(ldap.Connected);
        }
    }
}
