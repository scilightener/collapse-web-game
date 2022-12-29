﻿using System;

namespace XProtocol.Serializer
{
    [AttributeUsage(AttributeTargets.Field)]
    public class XFieldAttribute : Attribute
    {
        public byte FieldID { get; }

        public XFieldAttribute(byte fieldId)
        {
            FieldID = fieldId;
        }
    }
}
