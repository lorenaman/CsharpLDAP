/******************************************************************************
* The MIT License
* Copyright (c) 2003 Novell Inc.  www.novell.com
*
* Permission is hereby granted, free of charge, to any person obtaining  a copy
* of this software and associated documentation files (the Software), to deal
* in the Software without restriction, including  without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to  permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*******************************************************************************/
//
// SecurityProtocolTests.cs
//
// Author:
//   Igor Shmukler
//
// (C) 2014-2019 VQ Communications Ltd (http://www.vqcomms.com)
//

using System;
using Novell.Directory.LDAP.VQ;
using Xunit;

namespace Novell.Directory.LDAP.Tests
{
    public class SecurityProtocolsTests
    {
        [Fact]
        public void Ldap_Connection_Should_Bind_Login_And_Password()
        {
            var ldap = new LdapConnection();
            ldap.Connect(Globals.Host, Globals.DefaultPort);
            ldap.Bind(Globals.LoginDN, Globals.Password);

            Assert.True(ldap.Connected);
        }

        [Fact]
        public void Ldap_Connection_Should_Start_TLS()
        {
            var ldap = new LdapConnection();
            ldap.UserDefinedServerCertValidationDelegate += (certificate, certificateErrors) => true;
            ldap.Connect(Globals.Host, Globals.DefaultPort);
            ldap.startTLS();

            Assert.True(ldap.TLS);
        }

        [Fact]
        public void Ldap_Connection_Should_Start_and_Stop_TLS()
        {
            var ldap = new LdapConnection();
            ldap.UserDefinedServerCertValidationDelegate += (certificate, certificateErrors) => true;
            ldap.Connect(Globals.Host, Globals.DefaultPort);
            ldap.startTLS();
            ldap.stopTLS();

            Assert.False(ldap.TLS);
        }

        [Fact]
        public void Ldap_Connection_Should_Not_Start_TLS_With_Invalid_Certificate_That_Is_Processed_By_Default_Certificate_Validation_Handler()
        {
            var ldap = new LdapConnection();
            ldap.Connect(Globals.Host, Globals.DefaultPort);

            Assert.Throws(typeof(LdapException), () => { ldap.startTLS(); });
        }

        [Fact]
        public void Ldap_Connection_Should_Connect_SSL()
        {
            var ldap = new LdapConnection();
            ldap.SecureSocketLayer = true;
            ldap.UserDefinedServerCertValidationDelegate += (certificate, certificateErrors) => true;
            ldap.Connect(Globals.Host, Globals.SslPort);
            ldap.Bind(Globals.LoginDN, Globals.Password);
            
            Assert.True(ldap.Connected);
        }
    }
}
