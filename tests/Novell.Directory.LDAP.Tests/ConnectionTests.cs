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
// ConnectionTests.cs
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
    public class ConnectionTests
    {
        [Fact]
        public void Ldap_Connection_Should_Connect()
        {
            var ldap = new LdapConnection();
            ldap.Connect(Globals.Host, Globals.DefaultPort);

            Assert.True(ldap.Connected);
        }

        [Fact]
        public void Ldap_Connection_Should_Not_Connect()
        {
            var fakeHost = "0.0.0.0";

            var ldap = new LdapConnection();
            try
            {
                ldap.Connect(fakeHost, Globals.DefaultPort);
            }
            catch (Exception){}
           
            Assert.False(ldap.Connected);
        }

        [Fact]
        public void Ldap_Connection_Should_Connect_And_Disconnect()
        {
            var ldap = new LdapConnection();
            ldap.Connect(Globals.Host, Globals.DefaultPort);

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
            var ldap = new LdapConnection();
            ldap.Connect(Globals.Host, Globals.DefaultPort);
            ldap.Bind(Globals.LoginDN, Globals.Password);

            Assert.Equal("simple", ldap.AuthenticationMethod);
        }

        [Fact]
        public void Ldap_Connection_Should_Return_Right_Host()
        {
            var ldap = new LdapConnection();
            ldap.Connect(Globals.Host, Globals.DefaultPort);

            Assert.Equal(Globals.Host, ldap.Host);
        }

        [Fact]
        public void Ldap_Connection_Should_Return_Right_Port()
        {
            var ldap = new LdapConnection();
            ldap.Connect(Globals.Host, Globals.DefaultPort);

            Assert.Equal(Globals.DefaultPort, ldap.Port);
        }

        [Fact]
        public void Ldap_Connection_Should_Be_Authenticated()
        {
            var ldap = new LdapConnection();
            ldap.Connect(Globals.Host, Globals.DefaultPort);
            ldap.Bind(Globals.LoginDN, Globals.Password);

            Assert.True(ldap.Bound);
        }
    }
}
