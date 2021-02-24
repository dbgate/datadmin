import os, os.path, stat


#DEVENV = r'c:\Program Files\Microsoft Visual Studio 8\Common7\IDE\devenv'
DEVENV = r'c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv'
MAKENSIS = r'c:\Program files\NSIS\makensis'
#SVNBASE = 'https://subversion.savana.cz/datadmin' 
#SVNBASE = 'file:///c:/svn/datadmin2'
SVNBASE = 'https://svn.datadmin.com/datadmin'
SIGNTOOL = r'c:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\signtool'

# FTPHOST = 'ftp.datadmin.com'
# FTPLOGIN = 'datadmincom'
# FTPPASSWORD = 'Jv6TkZTM3q'

FTPHOST = 'www.datadmin.com'
FTPLOGIN = 'jenasoft'
FTPPASSWORD = 'ekAcZoofs4'
# FTPLOGIN = 'datadmin_com'
# FTPPASSWORD = 'draklesu'

FTPHOST2 = 'www.jenasoft.com'
FTPLOGIN2 = 'datadmin_jenasoft_com'
FTPPASSWORD2 = 'draklesu'

WINROOT = {'install': '$INSTDIR', 'appdata': '$APPDATA'}
LINROOT = {'install': 'usr/lib/datadmin', 'appdata': 'etc/datadmin/.config', 'root':''}

UNIX_PLATFORMS = ['linux', 'debian']
BRANDS = ['effiproz']

def loadbrand(bname):
    obj = __import__('brands.' + bname)
    res = getattr(obj, bname)
    res.BrandName = bname
    return res

def emptydir(dirname):
    if os.path.isdir(dirname):
        print "Removing directory "+dirname
        for root, dirs, files in os.walk(dirname,topdown=False):
            for name in files:
                os.chmod(os.path.join(root, name),stat.S_IWRITE);
                os.remove(os.path.join(root, name))
            for name in dirs:
                os.rmdir(os.path.join(root, name))
        os.rmdir(dirname);
