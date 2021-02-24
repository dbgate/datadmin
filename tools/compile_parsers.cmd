preprocess_sql_gram.py ..\Plugin.mysql\AntlrParser\MySQL.gsrc ..\Plugin.mysql\AntlrParser\MySQL.g
preprocess_sql_gram.py ..\Plugin.mssql\AntlrParser\MSSQL.gsrc ..\Plugin.mssql\AntlrParser\MSSQL.g
preprocess_sql_gram.py ..\Plugin.oracle\AntlrParser\Oracle.gsrc ..\Plugin.oracle\AntlrParser\Oracle.g
preprocess_sql_gram.py ..\Plugin.sqlite\AntlrParser\SQLite.gsrc ..\Plugin.sqlite\AntlrParser\SQLite.g
preprocess_sql_gram.py ..\Plugin.access\AntlrParser\Access.gsrc ..\Plugin.access\AntlrParser\Access.g
preprocess_sql_gram.py ..\Plugin.effiproz\AntlrParser\EffiProz.gsrc ..\Plugin.effiproz\AntlrParser\EffiProz.g
java -cp antlr-3.2.jar org.antlr.Tool ..\Plugin.mysql\AntlrParser\MySQL.g  
java -cp antlr-3.2.jar org.antlr.Tool ..\Plugin.mssql\AntlrParser\MSSQL.g
java -cp antlr-3.2.jar org.antlr.Tool ..\Plugin.postgre\AntlrParser\PostgreSQL.g
java -cp antlr-3.2.jar org.antlr.Tool ..\Plugin.oracle\AntlrParser\Oracle.g                                                                                
java -cp antlr-3.2.jar org.antlr.Tool ..\Plugin.sqlite\AntlrParser\SQLite.g                                                                                
java -cp antlr-3.2.jar org.antlr.Tool ..\Plugin.access\AntlrParser\Access.g                                                                                
java -cp antlr-3.2.jar org.antlr.Tool ..\Plugin.EffiProz\AntlrParser\EffiProz.g                                                                                
