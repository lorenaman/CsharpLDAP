﻿/******************************************************************************
* The MIT License
* Copyright (c) 2014 VQ Communications Ltd.  www.vqcomms.com
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
// Novell.Directory.Ldap.Controls.LdapPagedResultsResponse.cs
//
// Author:
//   Igor Shmukler
//
// (C) 2014 VQ Communications Ltd. (http://www.vqcomms.com)
//

using System;
using Novell.Directory.LDAP.VQ.Asn1;

namespace Novell.Directory.LDAP.VQ.Controls
{
    public class LdapPagedResultsResponse : LdapControl
    {
        virtual public int Size
        {
            get
            {
                return m_size;
            }

        }

        virtual public String Cookie
        {
            get
            {
                return m_cookie;
            }

        }

        /* The parsed fields are stored in these private variables */
        private int m_size;
        private String m_cookie;

        [CLSCompliant(false)]
        public LdapPagedResultsResponse(String oid, bool critical, sbyte[] values) : base(oid, critical, values)
        {

            /* Create a decoder object */
            LBERDecoder decoder = new LBERDecoder();
            if (decoder == null)
                throw new System.IO.IOException("Decoding error");

            /* We should get back an ASN.1 Sequence object */
            Asn1Object asnObj = decoder.decode(values);
            if ((asnObj == null) || (!(asnObj is Asn1Sequence)))
                throw new System.IO.IOException("Decoding error");

            /* 
			 * Get the 1st element which should be an integer containing the
			 * size (RFC 2696).
			 */
            Asn1Object asn1Size = ((Asn1Sequence)asnObj).get_Renamed(0);
            if ((asn1Size != null) && (asn1Size is Asn1Integer))
                m_size = ((Asn1Integer)asn1Size).intValue();
            else
                throw new System.IO.IOException("Decoding error");

            /*
			 * Get the 2nd element which should be an octet string containing the
			 * cookie (RFC 2696).
			 */
            Asn1Object asn1Cookie = ((Asn1Sequence)asnObj).get_Renamed(1);
            if ((asn1Cookie != null) && (asn1Cookie is Asn1OctetString))
                m_cookie = DecodeCookie((Asn1OctetString)asn1Cookie);
            else
                throw new System.IO.IOException("Decoding error");

            return;
        }

        //
        //  We need to use this decode method instead of the Asn1OctetString one
        //  See this issue: https://github.com/VQComms/CsharpLDAP/issues/10
        //
        private string DecodeCookie(Asn1OctetString cookie)
        {
            var s = string.Empty;

            try
            {
                var i = 0;
                var content = cookie.byteValue();
                byte[] bytes = new byte[content.Length];
                foreach (var item in content)
                {
                    bytes[i++] = unchecked((byte)item);
                }

                s = "";
                foreach (byte b in bytes)
                {
                    s += (char)b;
                }
            }
            catch (System.IO.IOException e)
            {
                throw new System.SystemException(e.ToString());
            }

            return s;
        }
    }
}