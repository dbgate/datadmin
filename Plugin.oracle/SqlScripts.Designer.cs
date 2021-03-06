﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Plugin.oracle {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SqlScripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SqlScripts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Plugin.oracle.SqlScripts", typeof(SqlScripts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE FUNCTION &lt;Name&gt;
        ///BEGIN
        ///END;
        ///.
        /// </summary>
        internal static string createfunction {
            get {
                return ResourceManager.GetString("createfunction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE PROCEDURE &lt;Name&gt;
        ///BEGIN
        ///END;
        ///.
        /// </summary>
        internal static string createprocedure {
            get {
                return ResourceManager.GetString("createprocedure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE SEQUENCE  &lt;Sequence name&gt;  
        ///MINVALUE 1 MAXVALUE 999999999999999999999999999 
        ///INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE
        ///.
        /// </summary>
        internal static string createsequence {
            get {
                return ResourceManager.GetString("createsequence", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE OR REPLACE TRIGGER &lt;Name&gt;
        ///AFTRE/BEFORE INSERT/DELETE/UPDATE ON &lt;Table&gt;
        ///FOR EACH ROW
        ///BEGIN
        ///END;
        ///.
        /// </summary>
        internal static string createtrigger {
            get {
                return ResourceManager.GetString("createtrigger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT col.OWNER, col.CONSTRAINT_NAME, con.CONSTRAINT_TYPE, col.TABLE_NAME, col.POSITION, col.COLUMN_NAME  FROM USER_CONSTRAINTS con
        ///	INNER JOIN USER_CONS_COLUMNS col ON 
        ///		con.OWNER = col.OWNER
        ///		AND con.CONSTRAINT_NAME = col.CONSTRAINT_NAME
        ///		AND con.TABLE_NAME = col.TABLE_NAME
        ///WHERE con.GENERATED=&apos;USER NAME&apos; AND con.CONSTRAINT_TYPE IN (&apos;P&apos;, &apos;U&apos;) AND (#RETURNALL#=1 OR con.TABLE_NAME=&apos;#TABLE#&apos;)
        ///.
        /// </summary>
        internal static string getconstraintcols {
            get {
                return ResourceManager.GetString("getconstraintcols", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME, SEARCH_CONDITION FROM USER_CONSTRAINTS 
        ///WHERE GENERATED=&apos;USER NAME&apos; AND CONSTRAINT_TYPE IN (&apos;P&apos;, &apos;C&apos;, &apos;U&apos;) AND (#RETURNALL#=1 OR TABLE_NAME=&apos;#TABLE#&apos;)
        ///.
        /// </summary>
        internal static string getconstraints {
            get {
                return ResourceManager.GetString("getconstraints", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ux.INDEX_NAME, ux.COLUMN_NAME, ux.COLUMN_POSITION, u.USERNAME FROM USER_IND_COLUMNS ux, USER_USERS u
        ///WHERE NOT EXISTS(SELECT * FROM USER_CONSTRAINTS uc WHERE uc.CONSTRAINT_NAME = ux.INDEX_NAME)
        ///.
        /// </summary>
        internal static string getindexcols {
            get {
                return ResourceManager.GetString("getindexcols", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ux.INDEX_NAME, ux.TABLE_OWNER, ux.TABLE_NAME, ux.UNIQUENESS FROM USER_INDEXES ux
        ///WHERE (NOT EXISTS(SELECT * FROM USER_CONSTRAINTS uc WHERE uc.CONSTRAINT_NAME = ux.INDEX_NAME)) AND ux.GENERATED = &apos;N&apos;
        ///.
        /// </summary>
        internal static string getindexes {
            get {
                return ResourceManager.GetString("getindexes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT fk.OWNER AS FK_OWNER, fk.CONSTRAINT_NAME AS FK_CONSTRAINT, fk.TABLE_NAME AS FK_TABLE, 
        ///	   relcol.OWNER AS R_OWNER, relcol.CONSTRAINT_NAME AS R_CONSTRAINT,
        ///	   fkcol.POSITION, fkcol.COLUMN_NAME as FK_COLUMN_NAME, relcol.COLUMN_NAME AS R_COLUMN_NAME
        ///	FROM USER_CONSTRAINTS fk
        ///	INNER JOIN USER_CONS_COLUMNS fkcol ON fk.OWNER = fkcol.OWNER AND fk.CONSTRAINT_NAME = fkcol.CONSTRAINT_NAME AND fk.TABLE_NAME = fkcol.TABLE_NAME
        ///	INNER JOIN USER_CONS_COLUMNS relcol ON fk.R_OWNER = relcol.OWNER AND fk.R_CONS [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string getrefcols {
            get {
                return ResourceManager.GetString("getrefcols", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT fk.OWNER AS FK_OWNER, fk.CONSTRAINT_NAME AS FK_CONSTRAINT, fk.TABLE_NAME AS FK_TABLE, 
        ///	   rel.OWNER AS R_OWNER, rel.CONSTRAINT_NAME AS R_CONSTRAINT, rel.TABLE_NAME AS R_TABLE, 
        ///	   fk.DELETE_RULE
        ///	FROM USER_CONSTRAINTS fk
        ///	INNER JOIN USER_CONSTRAINTS rel ON fk.R_OWNER = rel.OWNER AND fk.R_CONSTRAINT_NAME = rel.CONSTRAINT_NAME
        ///	WHERE fk.GENERATED=&apos;USER NAME&apos; AND fk.CONSTRAINT_TYPE = &apos;R&apos;
        ///.
        /// </summary>
        internal static string getrefs {
            get {
                return ResourceManager.GetString("getrefs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml  version=&quot;1.0&quot;?&gt;
        ///&lt;SyntaxDefinition name=&quot;Sql_oracle&quot; extensions=&quot;.sql&quot;&gt;
        ///    &lt;Digits name=&quot;Digits&quot; bold=&quot;false&quot; italic=&quot;false&quot; color=&quot;DarkBlue&quot;/&gt;
        ///
        ///    &lt;RuleSets&gt;
        ///        &lt;RuleSet ignorecase=&quot;true&quot;&gt;
        ///            &lt;Delimiters&gt;&amp;amp;&amp;lt;&amp;gt;~!%^*()-+=|\#/{}[]:;&quot;&apos; , .?&lt;/Delimiters&gt;
        ///        
        ///			&lt;Span name=&quot;LineComment&quot; stopateol=&quot;true&quot; bold=&quot;false&quot; italic=&quot;false&quot; color=&quot;Gray&quot; &gt;
        ///				&lt;Begin &gt;--&lt;/Begin&gt;
        ///			&lt;/Span&gt;
        ///
        ///			&lt;Span name=&quot;BlockComment&quot; stopateol=&quot;false&quot; bold=&quot;false&quot; italic=&quot;false&quot; color=&quot;Gray&quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string syntax {
            get {
                return ResourceManager.GetString("syntax", resourceCulture);
            }
        }
    }
}
