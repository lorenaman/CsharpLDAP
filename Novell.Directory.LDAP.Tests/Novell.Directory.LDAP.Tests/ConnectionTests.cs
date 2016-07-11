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

        [Fact]
        public void Ldap_Connection_Clone_Method_Should_Return_Another_Instance_Of_Object()
        {
            var ldapConnection = new LdapConnection();
            var ldapConnectionClone = ldapConnection.Clone();

            Assert.NotEqual(ldapConnection, ldapConnectionClone);
        }

        [Fact]
        public void Ldap_Connection_Should_Return_Simple_Authentication_Method()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;
            var loginDN = "dc=example,dc=com";
            var password = "";

            var ldap = new LdapConnection();
            ldap.Connect(ldapHost, ldapPort);
            ldap.Bind(loginDN, password);
            Assert.Equal("simple", ldap.AuthenticationMethod);
        }

        [Fact]
        public void Ldap_Connection_Should_Return_Right_Host()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;

            var ldap = new LdapConnection();
            ldap.Connect(ldapHost, ldapPort);
            Assert.Equal(ldapHost, ldap.Host);
        }

        [Fact]
        public void Ldap_Connection_Should_Return_Right_Port()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;

            var ldap = new LdapConnection();
            ldap.Connect(ldapHost, ldapPort);
            Assert.Equal(ldapPort, ldap.Port);
        }

        [Fact]
        public void Ldap_Connection_Should_Be_Authenticated()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;
            var loginDN = "cn=igor.shmukler,dc=ldap,dc=vqcomms,dc=com";
            var password = "abc123";

            var ldap = new LdapConnection();
            ldap.Connect(ldapHost, ldapPort);
            ldap.Bind(loginDN, password);
            Assert.True(ldap.Bound);
        }
    }
}
