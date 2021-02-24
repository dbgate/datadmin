﻿using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

partial class MySQLParser
{
    string m_errors = null;
    public override void EmitErrorMessage(string msg)
    {
        base.EmitErrorMessage(msg);
        if (m_errors != null)
        {
            m_errors += "; " + msg;
        }
        else
        {
            m_errors = msg;
        }
    }

    public string Errors { get { return m_errors; } }

    private string UnquoteName(string name)
    {
        if (name.StartsWith("`")) return name.Substring(1, name.Length - 2);
        return name;
    }

    private string StringValue(string value)
    {
        StringBuilder sb = new StringBuilder(value.Length - 2);
        char ch = value[0];
        for (int i = 1; i < value.Length - 1; i++)
        {
            if (ch == '\\')
            {
                i++;
                switch (value[i])
                {
                    case 'n':
                        sb.Append('\n');
                        break;
                    case 'r':
                        sb.Append('\r');
                        break;
                    case 't':
                        sb.Append('\t');
                        break;
                    case '0':
                        sb.Append('\0');
                        break;
                    default:
                        sb.Append(value[i]);
                        break;
                }
            }
            else
            {
                sb.Append(value[i]);
            }
        }
        return sb.ToString();
    }

    private long ParseBinNum(string val)
    {
        if (val.StartsWith("0b")) return DoParseBinNum(val.Substring(2));
        if (val.StartsWith("b'")) return DoParseBinNum(val.Substring(2, val.Length - 3));
        return 0;
    }

    private long DoParseBinNum(string val)
    {
        long res = 0;
        foreach (char c in val)
        {
            res *= 2;
            if (c == 1) res++;
        }
        return res;
    }

}