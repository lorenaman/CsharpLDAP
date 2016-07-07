using Novell.Directory.Ldap;
using System;
using Xunit;

namespace Novell.Directory.LDAP.Tests
{
    public class SecurityProtocolsTests
    {
        [Fact]
        public void Ldap_Connection_Should_Bind_Login_And_Password()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;
            var loginDN = "dc=example,dc=com";
            var password = "";

            var ldap = new LdapConnection();
            ldap.Connect(ldapHost, ldapPort);
            ldap.Bind(loginDN, password);

            Assert.True(ldap.Connected);
        }

        [Fact]
        public void Ldap_Connection_Should_Start_TLS()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;

            var ldap = new LdapConnection();
            ldap.UserDefinedServerCertValidationDelegate += (certificate, certificateErrors) => true;
            ldap.Connect(ldapHost, ldapPort);
            ldap.startTLS();
            Assert.True(ldap.TLS);
        }

        [Fact]
        public void Ldap_Connection_Should_Start_and_Stop_TLS()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;

            var ldap = new LdapConnection();
            ldap.UserDefinedServerCertValidationDelegate += (certificate, certificateErrors) => true;
            ldap.Connect(ldapHost, ldapPort);
            ldap.startTLS();
            ldap.stopTLS();

            Assert.False(ldap.TLS);
        }

        [Fact]
        public void Ldap_Connection_Should_Not_Start_TLS_With_Invalid_Certificate_That_Is_Processed_By_Default_Certificate_Validation_Handler()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 636;

            var ldap = new LdapConnection();
            ldap.Connect(ldapHost, ldapPort);

            Assert.Throws(typeof(LdapException), () => { ldap.startTLS(); });
        }

        [Fact]
        public void Ldap_Connection_Should_Connect_SSL()
        {
            var ldapHost = "192.168.1.32";
            var ldapPort = 389;
            var loginDN = "dc=example,dc=com";
            var password = "";

            var ldap = new LdapConnection();
            ldap.SecureSocketLayer = true;
            ldap.UserDefinedServerCertValidationDelegate += (certificate, certificateErrors) => true;
            ldap.Connect(ldapHost, ldapPort);
            ldap.Bind(loginDN, password);
            
            Assert.True(ldap.Connected);
        }
    }
}
