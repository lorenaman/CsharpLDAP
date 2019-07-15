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
// SearchTests.cs
//
// Author:
//   Igor Shmukler
//
// (C) 2014-2019 VQ Communications Ltd (http://www.vqcomms.com)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novell.Directory.LDAP.VQ;
using Xunit;

namespace Novell.Directory.LDAP.Tests
{
    public class SearchTests
    {
        [Fact]
        public void Ldap_Search_Should_Return_Correct_Results_Count()
        {
            var loginDN = "dc=example,dc=com";
            var password = "";

            LdapConnection conn = new LdapConnection();
            conn.Connect(Globals.Host, Globals.DefaultPort);
            conn.Bind(loginDN, password);
            LdapSearchResults lsc = conn.Search(
                    "dc=example,dc=com",
                    LdapConnection.SCOPE_SUB,
                    "objectclass=*",
                    null,
                    false);

            int resultsCount = lsc.Count;
            int counter = 0;
            while (lsc.hasMore())
            {
                LdapEntry nextEntry = lsc.next();
                ++counter;
            }

            Assert.Equal(resultsCount, counter);

            conn.Disconnect();
        }

        [Fact]
        public void Ldap_Search_Should_Return_Not_Null_Entries()
        {
            var loginDN = "dc=example,dc=com";
            var password = "";

            LdapConnection conn = new LdapConnection();
            conn.Connect(Globals.Host, Globals.DefaultPort);
            conn.Bind(loginDN, password);
            LdapSearchResults lsc = conn.Search(
                    "dc=example,dc=com",
                    LdapConnection.SCOPE_SUB,
                    "objectclass=*",
                    null,
                    false);

            while (lsc.hasMore())
            {
                LdapEntry nextEntry = lsc.next();
                Assert.NotEqual(nextEntry, (LdapEntry)null);
            }

            conn.Disconnect();
        }

        [Fact]
        public void Ldap_Entry_Should_Return_Dn_Property()
        {
            var loginDN = "dc=example,dc=com";
            var password = "";

            LdapConnection conn = new LdapConnection();
            conn.Connect(Globals.Host, Globals.DefaultPort);
            conn.Bind(loginDN, password);
            LdapSearchResults lsc = conn.Search(
                    "dc=example,dc=com",
                    LdapConnection.SCOPE_SUB,
                    "objectclass=*",
                    null,
                    false);

            while (lsc.hasMore())
            {
                LdapEntry nextEntry = lsc.next();
                Assert.False(string.IsNullOrEmpty(nextEntry.DN));
            }

            conn.Disconnect();
        }

        [Fact]
        public void Ldap_Search_Should_Return_Not_More_Results_Than_Defined_In_Ldap_Search_Constraints()
        {
            var loginDN = "dc=example,dc=com";
            var password = "";
            int maxResults = 1;

            LdapConnection conn = new LdapConnection();
            conn.Connect(Globals.Host, Globals.DefaultPort);
            conn.Bind(loginDN, password);
            LdapSearchResults lsc = conn.Search(
                    "dc=example,dc=com",
                    LdapConnection.SCOPE_SUB,
                    "objectclass=*",
                    null,
                    false,
                    new LdapSearchConstraints { MaxResults = maxResults});

            int counter = 0;
            var exception = Record.Exception(() =>
            {
                while (lsc.hasMore())
                {
                    LdapEntry nextEntry = lsc.next();
                    ++counter;
                }
            });

            Assert.IsType<LdapException>(exception);
            Assert.Equal(exception.Message, "Sizelimit Exceeded");
            Assert.InRange(counter, 0, maxResults);

            conn.Disconnect();
        }
    }
}
