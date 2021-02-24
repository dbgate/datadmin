using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public class DXDriver
    {
        IPhysicalConnection m_conn;
        enum Mode { Detached, Clear, Created };
        Mode m_mode = Mode.Detached;


        public DXDriver(IPhysicalConnection conn)
        {
            m_conn = conn;
        }

        private void WantStructure(bool write)
        {
            if (write)
            {
                if (m_mode != Mode.Created)
                {
                    DXDbCreator.CreateDbStructure(m_conn.SystemConnection, m_conn.Dialect);
                    m_mode = Mode.Created;
                }
            }
            else
            {
                if (m_mode == Mode.Clear) return;
                if (m_mode == Mode.Created) return;
                if (m_mode == Mode.Detached)
                {
                    if (DXDbCreator.HasDXStructure(m_conn.SystemConnection))
                    {
                        DXDbCreator.CreateDbStructure(m_conn.SystemConnection, m_conn.Dialect);
                        m_mode = Mode.Created;
                    }
                    else
                    {
                        m_mode = Mode.Clear;
                    }
                }
            }
        }

        public List<OnServerFile> LoadFiles(string dbname, int folderid)
        {
            List<OnServerFile> res = new List<OnServerFile>();
            m_conn.SystemConnection.SafeChangeDatabase(dbname);
            WantStructure(false);
            if (m_mode == Mode.Clear) return res;
            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                cmd.CommandText = String.Format("SELECT file_name FROM d2dx_file WHERE folder_id={0} ORDER BY file_name", folderid);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OnServerFile f = new OnServerFile(this, reader[0].ToString(), dbname, folderid);
                        res.Add(f);
                    }
                }
            }
            return res;
        }

        public bool ExistsFile(string dbname, int folderid, string filename)
        {
            m_conn.SystemConnection.SafeChangeDatabase(dbname);
            WantStructure(false);
            if (m_mode == Mode.Clear) return false;
            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                cmd.CommandText = String.Format("SELECT id FROM d2dx_file WHERE folder_id={0} AND file_name='{1}' ORDER BY file_name", folderid, filename);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        private void SaveFile(string dbname, int folderid, string filename, string data, string type)
        {
            m_conn.SystemConnection.SafeChangeDatabase(dbname);
            WantStructure(true);
            RemoveFile(dbname, folderid, filename);

            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                int? lastid = DbConnectionExtension.ExecuteScalar<int?>(m_conn.SystemConnection, "SELECT MAX(id) FROM d2dx_file");
                int id = lastid == null ? 1 : lastid.Value + 1;
                string sqlpar, formpar;
                m_conn.Dialect.CreateNamedParameter("p1", out sqlpar, out formpar);
                cmd.CommandText = String.Format("INSERT INTO d2dx_file (id, folder_id, file_name, data_type, file_data) VALUES ({0}, {1}, '{2}', '{3}', {4})", id, folderid, filename, type, sqlpar);
                DbParameter dbpar = m_conn.DbFactory.CreateParameter();
                dbpar.ParameterName = formpar;
                dbpar.Value = data;
                cmd.Parameters.Add(dbpar);
                cmd.ExecuteNonQuery();
            }

            //using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            //{
            //    DbParameter par = m_conn.DbFactory.CreateParameter();
            //    string sqlpar, formpar;
            //    m_conn.Dialect.CreateNamedParameter("p1", out sqlpar, out formpar);
            //    cmd.CommandText = String.Format("UPDATE d2dx_file SET data_type='{0}', file_data={1} WHERE folder_id={2} AND file_name='{3}'", type, sqlpar, folderid, filename);
            //    DbParameter dbpar = m_conn.DbFactory.CreateParameter();
            //    dbpar.ParameterName = formpar;
            //    dbpar.Value = data;
            //    cmd.Parameters.Add(dbpar);
            //    cmd.ExecuteNonQuery();
            //}
        }

        public void SaveTextFile(string dbname, int folderid, string filename, string data)
        {
            SaveFile(dbname, folderid, filename, data, "text");
        }

        public void SaveBinaryFile(string dbname, int folderid, string filename, byte[] data)
        {
            SaveFile(dbname, folderid, filename, Convert.ToBase64String(data), "base64");
        }

        public void RemoveFile(string dbname, int folderid, string filename)
        {
            m_conn.SystemConnection.SafeChangeDatabase(dbname);
            WantStructure(true);
            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                cmd.CommandText = String.Format("DELETE FROM d2dx_file WHERE folder_id={0} AND file_name='{1}'", folderid, filename);
                cmd.ExecuteNonQuery();
            }
        }

        //public IVirtualFile CreateFile(string dbname, int folderid, string name)
        //{
        //    DbConnectionExtension.SafeChangeDatabase(m_conn.SystemConnection, dbname);
        //    WantStructure(true);
        //    using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
        //    {
        //        int? lastid = DbConnectionExtension.ExecuteScalar<int?>(m_conn.SystemConnection, "SELECT MAX(id) FROM d2dx_file");
        //        int id = lastid == null ? 1 : lastid.Value + 1;
        //        cmd.CommandText = String.Format("INSERT INTO d2dx_file (id, folder_id, file_name) VALUES ({0}, {1}, '{2}')", id, folderid, name);
        //        cmd.ExecuteNonQuery();
        //        return new OnServerFile(this, name, dbname, folderid);
        //    }
        //}

        //public IVirtualFile OpenFile(string dbname, int folderid, string name)
        //{
        //    DbConnectionExtension.SafeChangeDatabase(m_conn.SystemConnection, dbname);
        //    WantStructure(false);
        //    using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
        //    {
        //        cmd.CommandText = String.Format("SELECT id, file_name FROM d2dx_file WHERE folder_id={0} AND file_name='{1}'", folderid, name);
        //        using (DbDataReader reader = cmd.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                int id = Int32.Parse(reader[0].ToString());
        //                string fn = reader[1].ToString();
        //                return new OnServerFile(this, fn, dbname, folderid);
        //            }
        //            throw new Exception("OnServer file does not exist:" + name);
        //        }
        //    }
        //}

        public IPhysicalConnection Connection { get { return m_conn; } }

        //public bool HasFile(string dbname, int folderid, int fileid)
        //{
        //    DbConnectionExtension.SafeChangeDatabase(m_conn.SystemConnection, dbname);
        //    WantStructure(false);
        //    if (m_mode == Mode.Clear) return false;
        //    using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
        //    {
        //        cmd.CommandText = String.Format("SELECT id FROM d2dx_file WHERE folder_id={0} AND id='{1}'", folderid, fileid);
        //        using (DbDataReader reader = cmd.ExecuteReader())
        //        {
        //            return reader.Read();
        //        }
        //    }
        //}

        private void LoadFileData(string dbname, int folderid, string filename, out string data, out string type)
        {
            DbConnectionExtension.SafeChangeDatabase(m_conn.SystemConnection, dbname);
            WantStructure(false);
            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                cmd.CommandText = String.Format("SELECT data_type, file_data FROM d2dx_file WHERE folder_id={0} AND file_name='{1}'", folderid, filename);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        type = reader[0].SafeToString();
                        object val = reader[1];
                        if (val is byte[]) data = Encoding.UTF8.GetString((byte[])val);
                        else data = val.SafeToString();
                        return;
                    }
                    throw new InternalError("DAE-00027 File not found:" + filename);
                }
            }
        }

        public string LoadTextFile(string dbname, int folderid, string filename)
        {
            string data, type;
            LoadFileData(dbname, folderid, filename, out data, out type);
            if (type == "text") return data;
            if (type == "base64") return Encoding.UTF8.GetString(Convert.FromBase64String(data));
            return data;
        }

        public byte[] LoadBinaryFile(string dbname,int folderid, string filename)
        {
            string data, type;
            LoadFileData(dbname, folderid, filename , out data, out type);
            if (type == "text") return Encoding.UTF8.GetBytes(data);
            if (type == "base64") return Convert.FromBase64String(data);
            return Encoding.UTF8.GetBytes(data);
        }

        public void RenameFile(string dbname, int folderid, string filename, string newname)
        {
            DbConnectionExtension.SafeChangeDatabase(m_conn.SystemConnection, dbname);
            WantStructure(true);
            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                cmd.CommandText = String.Format("UPDATE d2dx_file SET file_name='{0}' WHERE folder_id={1} AND file_name='{2}'", newname, folderid, filename);
                cmd.ExecuteNonQuery();
            }
        }

        public IVirtualFolder GetFolder(string folder, string dbname)
        {
            int folderid;
            switch (folder)
            {
                case VirtualFileExtension.DIAGRAMS_FOLDER_NAME: 
                    folderid = 1;
                    break;
                case VirtualFileExtension.SQLSCRIPTS_FOLDER_NAME:
                    folderid = 2;
                    break;
                default:
                    throw new InternalError("DAE-00028 Server folder not defined:" + folder);
            }
            //DbConnectionExtension.SafeChangeDatabase(m_conn.SystemConnection, dbname);
            //WantStructure(false);
            //int folderid = DbConnectionExtension.ExecuteScalar<int>(m_conn.SystemConnection, "SELECT id FROM d2dx_folder WHERE folder_name='" + folder + "'");
            return new OnServerFolder(this, folderid, folder, dbname);
        }
    }
}
