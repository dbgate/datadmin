using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DatAdmin
{
    public class TableAnalyser
    {
        public bool AllowDeduceFromColumns = true;

        public class Key
        {
            public string keyschema;
            public string keyname;
            public string tblname;
            public string tblschema;

            public string dsttblschema;
            public string dsttblname;

            public string dstpkschema;
            public string dstpkname;

            public string keytype;

            public string updaterule;
            public string deleterule;

            public bool? keyisunique = null;

            public string checkexpr;

            public Dictionary<string, string> keyspecdata;

            public bool IsForTable(TableStructure table)
            {
                return table.Name == tblname && table.SchemaName == tblschema;
            }
            public bool IsForTableDest(TableStructure table)
            {
                return table.Name == dsttblname && table.SchemaName == dsttblschema;
            }
            public NameWithSchema FullTableName
            {
                get
                {
                    if (tblname == null) return null;
                    return new NameWithSchema(tblschema, tblname);
                }
            }
            public NameWithSchema FullKeyName
            {
                get
                {
                    if (keyname == null) return null;
                    return new NameWithSchema(keyschema, keyname);
                }
            }

            public void MergeInstance(Key src)
            {
                if (keyschema == null) keyschema = src.keyschema;
                if (keyname == null) keyname = src.keyname;
                if (tblname == null) tblname = src.tblname;
                if (tblschema == null) tblschema = src.tblschema;

                if (dsttblschema == null) dsttblschema = src.dsttblschema;
                if (dsttblname == null) dsttblname = src.dsttblname;
                if (dstpkschema == null) dstpkschema = src.dstpkschema;
                if (dstpkname == null) dstpkname = src.dstpkname;

                if (keytype == null) keytype = src.keytype;

                if (updaterule == null) updaterule = src.updaterule;
                if (deleterule == null) deleterule = src.deleterule;

                if (keyisunique == null) keyisunique = src.keyisunique;

                if (checkexpr == null) checkexpr = src.checkexpr;
            }
        }

        public class Col
        {
            public string keyschema;
            public string keyname;
            public string keytype;
            public string tblname;
            public string tblschema;
            public string colname;

            public string dsttblschema;
            public string dsttblname;
            public string dstcolname;
            public string ordinal;

            public bool keyisunique;

            public int OrdinalAsInt
            {
                get
                {
                    int res;
                    if (Int32.TryParse(ordinal, out res)) return res;
                    return 0;
                }
            }
            public bool IsForTable(TableStructure table)
            {
                return table.Name == tblname && table.SchemaName == tblschema;
            }
            public bool IsForTableDest(TableStructure table)
            {
                return table.Name == dsttblname && table.SchemaName == dsttblschema;
            }

            public Dictionary<string, string> keyspecdata;
            public Dictionary<string, string> colspecdata;
            public Dictionary<string, string> dstcolspecdata;

            public IColumnReference getcolref()
            {
                ColumnReference res = new ColumnReference(colname);
                res.SpecificData.AddAll(colspecdata);
                return res;
            }

            public IColumnReference getdstcolref()
            {
                ColumnReference res = new ColumnReference(dstcolname);
                res.SpecificData.AddAll(dstcolspecdata);
                return res;
            }
        }

        public List<Key> keys = new List<Key>();
        public List<Col> cols = new List<Col>();

        //public class Fk
        //{
        //    public string fkname;
        //    public string tblname;
        //    public string tblschema;
        //    public string dsttblname;
        //    public string dsttblschema;
        //    public string dstpkname;
        //    public string dstpkschema;
        //    public static NameWithSchema ExtractTable(Fk fk) { return new NameWithSchema(fk.tblschema, fk.tblname); }
        //    public static NameWithSchema ExtractDestTable(Fk fk) { return new NameWithSchema(fk.dsttblschema, fk.dsttblname); }
        //}
        //public class Pk
        //{
        //    public string pkname;
        //    public string tblname;
        //    public string tblschema;
        //}
        //public class Col
        //{
        //    public string tblname;
        //    public string tblschema;
        //    public string colname;
        //    public int ordinal;
        //}
        //public class PkCol : Col
        //{
        //    public string pkname;
        //}
        //public class FkCol : Col
        //{
        //    public string fkname;
        //    public string dsttblname;
        //    public string dsttblschema;
        //    public string dstcolname;
        //}
        //public List<Pk> pks = new List<Pk>();
        //public List<Fk> fks = new List<Fk>();
        //public List<Fk> refs = new List<Fk>();
        //public List<PkCol> pkcols = new List<PkCol>();
        //public List<FkCol> fkcols = new List<FkCol>();
        //public List<FkCol> refcols = new List<FkCol>();

        static int CompareCols(Col a, Col b)
        {
            return a.OrdinalAsInt - b.OrdinalAsInt;
        }

        //delegate NameWithSchema TableNameExtractor(Fk fk);

        public IEnumerable<Col> GetCols(Key key)
        {
            foreach (Col col in cols)
            {
                if (col.keyname == key.keyname && col.keyschema == key.keyschema && (col.tblname == null || col.tblname == key.tblname) && (col.tblschema == null || col.tblschema == key.tblschema))
                {
                    yield return col;
                }
            }
        }

        //private void SaveFks(List<Fk> src,List<FkCol> cols,  IList dst, NameWithSchema tableName, TableNameExtractor extract)
        //{
        //    foreach (Fk fk in src)
        //    {
        //        if (extract(fk) != tableName) continue;
        //        ForeignKey cnt = new ForeignKey();
        //        cnt.Name = fk.fkname;
        //        cnt.Table = new NameWithSchema(fk.tblschema, fk.tblname);
        //        if (fk.dsttblname != null) cnt.PrimaryKeyTable = new NameWithSchema(fk.dsttblschema, fk.dsttblname);
        //        Pk dstpk = null;
        //        if (fk.dstpkname != null)
        //        {
        //            foreach (Pk pk in pks)
        //            {
        //                if (fk.dstpkname == pk.pkname && fk.dstpkschema == pk.tblschema)
        //                {
        //                    dstpk = pk;
        //                    cnt.PrimaryKeyTable = new NameWithSchema(pk.tblschema, pk.tblname);
        //                }
        //            }
        //        }
        //        foreach (FkCol col in cols)
        //        {
        //            if (col.fkname == fk.fkname && col.tblname == fk.tblname && col.tblschema == fk.tblschema)
        //            {
        //                cnt.Columns.Add(col.colname);
        //                if (col.dstcolname != null) cnt.Columns.Add(col.dstcolname);
        //                if (col.dsttblname != null) cnt.PrimaryKeyTable = new NameWithSchema(col.dsttblschema, col.dsttblname);
        //            }
        //        }
        //        if (dstpk != null && cnt.PrimaryKeyColumns.Count < cnt.Columns.Count)
        //        {
        //            foreach (PkCol col in GetPkCols(dstpk))
        //            {
        //                cnt.PrimaryKeyColumns.Add(col.colname);
        //            }
        //        }
        //        dst.Add(cnt);
        //    }
        //}

        private void FillFk(Key key, ForeignKey cnt)
        {
            cnt.Name = key.keyname;
            cnt.SetDummyTable(new NameWithSchema(key.tblschema, key.tblname));
            cnt.PrimaryKeyTable = new NameWithSchema(key.dsttblschema, key.dsttblname);
            cnt.OnDeleteAction = ForeignKeyActionExtension.FromSqlName(key.deleterule);
            cnt.OnUpdateAction = ForeignKeyActionExtension.FromSqlName(key.updaterule);
            foreach (Col col in GetCols(key))
            {
                cnt.Columns.Add(col.getcolref());
                if (col.dsttblname != null) cnt.PrimaryKeyTable = new NameWithSchema(col.dsttblschema, col.dsttblname);
                if (col.dstcolname != null)
                {
                    cnt.PrimaryKeyColumns.Add(col.getdstcolref());
                }
            }
        }

        public void SaveConstraints(TableStructure table, DatabaseAnalyser analyser)
        {
            MergeKeys();
            cols.Sort(CompareCols);
            bool refsAdded = false;

            foreach (Key key in keys)
            {
                if (key.IsForTable(table))
                {
                    switch (key.keytype)
                    {
                        case "PRIMARY KEY":
                            {
                                PrimaryKey cnt = new PrimaryKey();
                                cnt.Name = key.keyname;
                                //cnt.Table = new NameWithSchema(key.tblschema, key.tblname);
                                foreach (Col col in GetCols(key))
                                {
                                    cnt.Columns.Add(col.getcolref());
                                }
                                if (!analyser.SkipConstraint(cnt)) table._Constraints.Add(cnt);

                            } break;
                        case "FOREIGN KEY":
                            {
                                ForeignKey cnt = new ForeignKey();
                                FillFk(key, cnt);
                                if (!analyser.SkipConstraint(cnt)) table._Constraints.Add(cnt);
                            } break;
                        case "UNIQUE":
                            {
                                UniqueConstraint cnt = new UniqueConstraint();
                                cnt.Name = key.keyname;
                                //cnt.Table = new NameWithSchema(key.tblschema, key.tblname);
                                foreach (Col col in GetCols(key))
                                {
                                    cnt.Columns.Add(col.getcolref());
                                }
                                if (!analyser.SkipConstraint(cnt)) table._Constraints.Add(cnt);
                            } break;
                        case "CHECK":
                            {
                                CheckConstraint cnt = new CheckConstraint();
                                cnt.Name = key.keyname;
                                //cnt.Table = new NameWithSchema(key.tblschema, key.tblname);
                                cnt.Expression = key.checkexpr;
                                if (!analyser.SkipConstraint(cnt)) table._Constraints.Add(cnt);
                            } break;
                        case "INDEX":
                            {
                                IndexConstraint cnt = new IndexConstraint();
                                cnt.Name = key.keyname;
                                cnt.IsUnique = key.keyisunique ?? false;
                                //cnt.Table = new NameWithSchema(key.tblschema, key.tblname);
                                foreach (Col col in GetCols(key))
                                {
                                    cnt.Columns.Add(col.getcolref());
                                }
                                if (!analyser.SkipConstraint(cnt)) table._Constraints.Add(cnt);
                            } break;
                    }
                }
                if (key.IsForTableDest(table) && key.keytype == "FOREIGN KEY")
                {
                    ForeignKey cnt = new ForeignKey();
                    FillFk(key, cnt);
                    refsAdded = true;
                    table.AddReference(cnt);
                    //table._ReferencedFrom.Add(cnt);
                }
            }

            // deduce keys from columns
            if (keys.Count == 0 && AllowDeduceFromColumns)
            {
                var pks = new Dictionary<string, PrimaryKey>();
                var fks = new Dictionary<string, ForeignKey>();
                var uqs = new Dictionary<string, UniqueConstraint>();
                var idxs = new Dictionary<string, IndexConstraint>();
                foreach (Col col in cols)
                {
                    if (!col.IsForTable(table)) continue;
                    if (col.keytype == null) continue;
                    switch (col.keytype)
                    {
                        case "PRIMARY KEY":
                            {
                                if (!pks.ContainsKey(col.keyname))
                                {
                                    pks[col.keyname] = new PrimaryKey();
                                    table._Constraints.Add(pks[col.keyname]);
                                }
                                // zde NESMI byt cnt.SetDummyTable
                                //pks[col.keyname].Table = table.FullName;
                                pks[col.keyname].Name = col.keyname;
                                pks[col.keyname].Columns.Add(col.getcolref());
                            } break;
                        case "FOREIGN KEY":
                            {
                                if (!fks.ContainsKey(col.keyname))
                                {
                                    fks[col.keyname] = new ForeignKey();
                                    table._Constraints.Add(fks[col.keyname]);
                                }
                                // zde NESMI byt cnt.SetDummyTable
                                //fks[col.keyname].Table = table.FullName;
                                fks[col.keyname].Name = col.keyname;
                                fks[col.keyname].Columns.Add(col.getcolref());
                                fks[col.keyname].PrimaryKeyColumns.Add(col.getdstcolref());
                                fks[col.keyname].PrimaryKeyTable = analyser.NewNameWithSchema(col.dsttblschema, col.dsttblname);
                            } break;
                        case "UNIQUE":
                            {
                                if (!uqs.ContainsKey(col.keyname))
                                {
                                    uqs[col.keyname] = new UniqueConstraint();
                                    table._Constraints.Add(uqs[col.keyname]);
                                }
                                // zde NESMI byt cnt.SetDummyTable
                                //uqs[col.keyname].Table = table.FullName;
                                uqs[col.keyname].Name = col.keyname;
                                uqs[col.keyname].Columns.Add(col.getcolref());
                            } break;
                        case "INDEX":
                            {
                                if (!idxs.ContainsKey(col.keyname))
                                {
                                    idxs[col.keyname] = new IndexConstraint();
                                    table._Constraints.Add(idxs[col.keyname]);
                                }
                                // zde NESMI byt cnt.SetDummyTable
                                //idxs[col.keyname].Table = table.FullName;
                                idxs[col.keyname].Name = col.keyname;
                                idxs[col.keyname].Columns.Add(col.getcolref());
                                idxs[col.keyname].IsUnique = col.keyisunique;
                            } break;
                    }
                }
            }

            if (!refsAdded)
            {
                Dictionary<string, ForeignKey> refs = new Dictionary<string, ForeignKey>();

                foreach (Col col in cols)
                {
                    if (col.IsForTableDest(table))
                    {
                        if (!refs.ContainsKey(col.keyname)) refs[col.keyname] = new ForeignKey();
                        ForeignKey cnt = refs[col.keyname];
                        cnt.Name = col.keyname;
                        cnt.SetDummyTable(new NameWithSchema(col.tblschema, col.tblname));
                        cnt.PrimaryKeyTable = new NameWithSchema(col.dsttblschema, col.dsttblname);
                        cnt.Columns.Add(col.getcolref());
                        cnt.PrimaryKeyColumns.Add(col.getdstcolref());
                    }
                }

                foreach (ForeignKey fk in refs.Values) table.AddReference(fk);
            }

            //pkcols.Sort(CompareCols);
            //fkcols.Sort(CompareCols);
            //refcols.Sort(CompareCols);

            //foreach (Pk pk in pks)
            //{
            //    if (pk.tblname != table.Name && pk.tblschema != table.SchemaName) continue;
            //    PrimaryKey cnt = new PrimaryKey();
            //    cnt.Name = pk.pkname;
            //    cnt.Table = new NameWithSchema(pk.tblschema, pk.tblname);
            //    foreach (PkCol col in GetPkCols(pk))
            //    {
            //        cnt.Columns.Add(col.colname);
            //    }
            //    table.Constraints.Add(cnt);
            //}

            //SaveFks(fks, fkcols, table.Constraints, table.FullName, Fk.ExtractTable);
            //SaveFks(fks, fkcols, table.ReferencedFrom, table.FullName, Fk.ExtractDestTable);
        }

        bool m_isMerged = false;
        // merges information about keys from more records into one
        private void MergeKeys()
        {
            if (m_isMerged) return;
            List<Key> res = new List<Key>();
            foreach (Key key in keys)
            {
                bool merged = false;
                foreach (Key ek in res)
                {
                    if (ek.FullTableName == null || key.FullTableName == null)
                    {
                        if (ek.FullKeyName == key.FullKeyName)
                        {
                            ek.MergeInstance(key);
                            merged = true;
                            break;
                        }
                    }
                    else
                    {
                        if (ek.FullKeyName == key.FullKeyName && ek.FullTableName == key.FullTableName)
                        {
                            ek.MergeInstance(key);
                            merged = true;
                            break;
                        }
                    }
                }
                if (!merged) res.Add(key);
            }
            keys.Clear();
            keys.AddRange(res);
            m_isMerged = true;
        }

        public Key FindKey(string schema, string keyname)
        {
            foreach (Key key in keys)
            {
                if (key.tblschema == schema && key.keyname == keyname) return key;
            }
            return null;
        }

        public Key FindKey(string schema, string tblname, string keyname)
        {
            foreach (Key key in keys)
            {
                if (key.tblschema == schema && key.keyname == keyname && key.tblname == tblname) return key;
            }
            return null;
        }
    }
}
