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
// LdapAttributeSetTests.cs
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
    public class LdapAttributeSetTests
    {
        [Fact]
        public void Ldap_Attribute_Set_Should_Be_Empty()
        {
            LdapAttributeSet attributeSet = new LdapAttributeSet();
            Assert.True(attributeSet.IsEmpty());
        }

        [Fact]
        public void Ldap_Attribute_Set_Should_Not_Be_Empty()
        {
            LdapAttributeSet attributeSet = new LdapAttributeSet();
            attributeSet.Add(new LdapAttribute("objectclass", "inetOrgPerson"));
            Assert.False(attributeSet.IsEmpty());
        }

        [Fact]
        public void Ldap_Attribute_Set_Count_Should_One()
        {
            LdapAttributeSet attributeSet = new LdapAttributeSet();
            attributeSet.Add(new LdapAttribute("objectclass", "inetOrgPerson"));
            Assert.Equal(1, attributeSet.Count);
        }

        [Fact]
        public void Ldap_Attribute_Set_Should_Contain_Attribute()
        {
            LdapAttributeSet attributeSet = new LdapAttributeSet();
            var attr = new LdapAttribute("objectclass", "inetOrgPerson");
            attributeSet.Add(attr);
            Assert.True(attributeSet.Contains(attr));
        }

        [Fact]
        public void Ldap_Attribute_Set_Should_Be_Cleared()
        {
            LdapAttributeSet attributeSet = new LdapAttributeSet();
            var attr = new LdapAttribute("objectclass", "inetOrgPerson");
            attributeSet.Add(attr);
            attributeSet.Clear();
            Assert.True(attributeSet.IsEmpty());
        }

        [Fact]
        public void Ldap_Attribute_Set_Attribute_Should_Be_Removed()
        {
            LdapAttributeSet attributeSet = new LdapAttributeSet();
            var attr = new LdapAttribute("objectclass", "inetOrgPerson");
            attributeSet.Add(attr);
            attributeSet.Remove(attr);
            Assert.False(attributeSet.Contains(attr));
        }

        [Fact]
        public void Ldap_Attribute_Set_Attribute_Should_Be_Taken_By_Name()
        {
            var attrName = "objectclass";
            LdapAttributeSet attributeSet = new LdapAttributeSet();
            var attr = new LdapAttribute(attrName, "inetOrgPerson");
            attributeSet.Add(attr);
            var attrFromContainer = attributeSet.getAttribute(attrName);
            Assert.Equal(attrName, attrFromContainer.Name);
        }

        [Fact]
        public void Ldap_Attribute_Set_Should_Be_Cloned()
        {
            var attrName = "objectclass";
            LdapAttributeSet attributeSet = new LdapAttributeSet();
            var attr = new LdapAttribute(attrName, "inetOrgPerson");
            attributeSet.Add(attr);
            
            var attributeSetClone = (LdapAttributeSet)attributeSet.Clone();

            bool equals = attributeSet == attributeSetClone;
            Assert.False(equals);

            var attrFromContainer = attributeSet.getAttribute(attrName);
            var attrFromCloneContainer = attributeSetClone.getAttribute(attrName);
            bool equalsAttrs = attrFromContainer == attrFromCloneContainer;
            Assert.True(equalsAttrs);
        }
    }
}
