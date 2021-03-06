﻿using System;
using System.Collections.Generic;
using System.Text;
#if NET_40_OR_GREATER
using System.Numerics;
#endif
using Cassandra;
using System.IO;

namespace Cassandra.MSTest
{
    internal class Randomm : Random
    {
        public Randomm()
            : base(5)
        {
        }
        public Randomm(int mili)
            : base(mili)
        {                 
        }
        
        internal static object RandomVal(Type tp)
        {
            Randomm rndm = new Randomm();
            if (tp != null)
                return rndm.GetType().GetMethod("Next" + tp.Name).Invoke(rndm, new object[] { });
            else
                return "";
        }


        public float NextSingle()
        {
            double numb = this.NextDouble();
            numb -= 0.5;
            numb *= 2;
            return float.MaxValue * (float)numb;
        }
        public UInt16 NextUInt16()
        {
            return (ushort)this.Next(0, 65535); 
        }
        public int NextInt32()
        {
            return this.Next();
        }
        public Int64 NextInt64()
        {
            var buffer = new byte[sizeof(Int64)];
            this.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public decimal NextDecimal()
        {
            byte scale = (byte)this.Next(29);
            bool sign = this.Next(2) == 1;

            return new decimal(this.NextInt32(),
                               this.NextInt32(),
                               this.NextInt32(),
                               sign,
                               scale);
        }

#if NET_40_OR_GREATER
        public BigInteger NextBigInteger()  	
        {	  	
            return new BigInteger(Int64.MaxValue) * 10;	  	
        }
#endif
        public string NextString()
        {
            return NextChar();
        }
        public string NextChar()
        {            
            string asciiString = string.Empty;
            for (int i = 0; i < 128; i++)
                if (i == 34 || i == 39)
                    continue;
                else 
                    asciiString += (char)i;

            return asciiString;
        }
        public DateTimeOffset NextDateTimeOffset()
        {
            var now = DateTimeOffset.Now.UtcDateTime;
            return new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond, TimeSpan.Zero);
        }
        
        public byte[] NextByte()
        {            
            byte[] btarr = new byte[this.NextUInt16()];            
            this.NextBytes(btarr);
            return btarr;
        }

        public bool NextBoolean()
        {
            return this.NextUInt16() > 127 ? true : false;
        }

        public Guid NextGuid()
        {
            return Guid.NewGuid();
        }
    }
}
